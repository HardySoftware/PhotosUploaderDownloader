﻿namespace HardySoft.PhotosUploaderDownloader.Security
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// An OAuth client to authenticate Google services.
    /// </summary>
    public class OAuthClient
    {
        /// <summary>
        /// The Google API OAuth Uri.
        /// </summary>
        private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

        /// <summary>
        /// The Google OAuth request challenge method.
        /// </summary>
        private const string CodeChallengeMethod = "S256";

        /// <summary>
        /// Perform OAuth authentication.
        /// </summary>
        /// <returns>The asynchronous operation task.</returns>
        public async Task PerformAuthentication()
        {
            // Generates state and PKCE values.
            string state = RandomDataBase64url(32);
            string codeVerifier = RandomDataBase64url(32);
            string codeChallenge = Base64UrlEncodeNoPadding(HashWithSha256(codeVerifier));

            // Creates a redirect URI using an available port on the loop-back address.
            string redirectUri = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
            Output("redirect URI: " + redirectUri);

            // Creates an HttpListener to listen for requests on that redirect URI.
            var http = new HttpListener();
            http.Prefixes.Add(redirectUri);
            Output("Listening..");
            http.Start();

            // Define the scopes needed for the app, refer to https://developers.google.com/photos/library/guides/authentication-authorization#OAuth2Authorizing for more info of photos library scope.
            var scope = $"openid%20profile%20{Uri.EscapeDataString("https://www.googleapis.com/auth/photoslibrary")}";

            // Creates the OAuth 2.0 authorization request. Refer to https://developers.google.com/identity/protocols/OAuth2InstalledApp#step-2-send-a-request-to-googles-oauth-20-server for more details.
            string authorizationRequest = $"{AuthorizationEndpoint}?response_type=code&scope={scope}&redirect_uri={Uri.EscapeDataString(redirectUri)}&client_id={Secrets.ClientId}&state={state}&code_challenge={codeChallenge}&code_challenge_method={CodeChallengeMethod}";

            // Opens request in the browser.
            await Task.Run(() => OpenBrowser(authorizationRequest));

            // Waits for the OAuth authorization response.
            var context = await http.GetContextAsync();

            // Sends an HTTP response to the browser.
            var response = context.Response;
            string responseString = "<html><head></head><body>Please return to the application's window to continue.</body></html>";
            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
                Console.WriteLine("HTTP server stopped.");
            });

            // Checks for errors.
            if (context.Request.QueryString.Get("error") != null)
            {
                throw new InvalidOperationException($"OAuth authorization error: {context.Request.QueryString.Get("error")}.");
            }

            if (context.Request.QueryString.Get("code") == null || context.Request.QueryString.Get("state") == null)
            {
                throw new InvalidOperationException($"Malformed authorization response. {context.Request.QueryString}");
            }

            // extracts the code
            var authorizationCode = context.Request.QueryString.Get("code");
            var incomingState = context.Request.QueryString.Get("state");

            // Compares the received state to the expected value, to ensure that
            // this app made the request which resulted in authorization.
            if (incomingState != state)
            {
                throw new InvalidOperationException($"Received request with invalid state ({incomingState})");
            }

            Output("Authorization code: " + authorizationCode);

            // Starts the code exchange at the Token Endpoint.
            await PerformCodeExchange(authorizationCode, codeVerifier, redirectUri);
        }

        /// <summary>
        /// Opens a Url in different platforms.
        /// </summary>
        /// <param name="url">The Url to open</param>
        private static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Performs code OAuth exchange.
        /// </summary>
        /// <param name="authorizationCode">The authorization code obtained from initial request.</param>
        /// <param name="codeVerifier">The code verifier used during the initial request.</param>
        /// <param name="redirectUri">The redirect (callback) Uri from local host.</param>
        /// <returns>The asynchronous operation task with user information.</returns>
        private static async Task<string> PerformCodeExchange(string authorizationCode, string codeVerifier, string redirectUri)
        {
            Output("Exchanging code for tokens...");

            // builds the request, refer to https://developers.google.com/identity/protocols/OAuth2InstalledApp#exchange-authorization-code for more info.
            string tokenRequestURI = "https://www.googleapis.com/oauth2/v4/token";
            string tokenRequestBody = $"code={authorizationCode}&redirect_uri={System.Uri.EscapeDataString(redirectUri)}&client_id={Secrets.ClientId}&code_verifier={codeVerifier}&client_secret={Secrets.ClientSecret}&scope=&grant_type=authorization_code";

            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            byte[] byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = byteVersion.Length;

            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(byteVersion, 0, byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    string responseText = await reader.ReadToEndAsync();
                    Output(responseText);

                    // converts to dictionary
                    Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);
                    string access_token = tokenEndpointDecoded["access_token"];
                    var userInfo = await GetUserInfo(access_token);

                    return userInfo;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Output("HTTP: " + response.StatusCode);
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            string responseText = await reader.ReadToEndAsync();
                            Output(responseText);
                        }
                    }
                }

                throw;
            }
        }

        /// <summary>
        /// Gets a random unused port number on local host.
        /// </summary>
        /// <returns>The port number which is not being used yet.</returns>
        /// <remarks>Reference http://stackoverflow.com/a/3978040 for more details.</remarks>
        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        /// <summary>
        /// Gets user info by using access token.
        /// </summary>
        /// <param name="accesToken">The access token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with user information.</returns>
        private static async Task<string> GetUserInfo(string accesToken)
        {
            Output("Making API Call to Userinfo...");

            // builds the  request
            string userInfoRequestUri = "https://www.googleapis.com/oauth2/v3/userinfo";

            // sends the request
            HttpWebRequest userinfoRequest = (HttpWebRequest)WebRequest.Create(userInfoRequestUri);
            userinfoRequest.Method = "GET";
            userinfoRequest.Headers.Add($"Authorization: Bearer {accesToken}");
            userinfoRequest.ContentType = "application/x-www-form-urlencoded";
            userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            // gets the response
            WebResponse userinfoResponse = await userinfoRequest.GetResponseAsync();
            using (StreamReader userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
            {
                // reads response body
                string userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();

                Output(userinfoResponseText);
                return userinfoResponseText;
            }
        }

        /// <summary>
        /// Appends the given string to the on-screen log, and the debug console.
        /// </summary>
        /// <param name="output">string to be appended</param>
        private static void Output(string output)
        {
            Console.WriteLine(output);
        }

        /// <summary>
        /// Returns URI-safe data with a given input length.
        /// </summary>
        /// <param name="length">Input length (nb. output will be longer)</param>
        /// <returns>A URI friendly base64 encoded string.</returns>
        private static string RandomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            byte[] bytes = new byte[length];

            rng.GetBytes(bytes);

            return Base64UrlEncodeNoPadding(bytes);
        }

        /// <summary>
        /// Returns the SHA256 hash of the input string.
        /// </summary>
        /// <param name="inputStirng">The string to be hashed.</param>
        /// <returns>The hashed value.</returns>
        private static byte[] HashWithSha256(string inputStirng)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(bytes);
        }

        /// <summary>
        /// Base64url no-padding encodes the given input buffer.
        /// </summary>
        /// <param name="buffer">The byte array to encode with base64.</param>
        /// <returns>The base64 encoded string value.</returns>
        private static string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");

            // Strips padding.
            base64 = base64.Replace("=", string.Empty);

            return base64;
        }
    }
}
