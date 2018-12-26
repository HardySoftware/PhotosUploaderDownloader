namespace HardySoft.PhotosUploaderDownloader.Google.ApiClient
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Abstraction.Security;

    /// <summary>
    /// Supported Google Api client implementation.
    /// </summary>
    public class ApiClient : IApiClient
    {
        /// <exception cref="ArgumentNullException">Any input parameters are null.</exception>
        /// <inheritdoc />
        public async Task<string> GetApiCall(OAuthToken oauthToken, Uri apiUri)
        {
            if (oauthToken == null)
            {
                throw new ArgumentNullException(nameof(oauthToken));
            }

            if (apiUri == null)
            {
                throw new ArgumentNullException(nameof(apiUri));
            }

            // sends the request
            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(apiUri.AbsoluteUri);
            apiRequest.Method = "GET";
            apiRequest.Headers.Add($"Authorization: Bearer {oauthToken.AccessToken}");
            apiRequest.ContentType = "application/x-www-form-urlencoded";
            apiRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            // gets the response
            WebResponse apiResponse = await apiRequest.GetResponseAsync();
            using (StreamReader responseReader = new StreamReader(apiResponse.GetResponseStream() ?? throw new InvalidOperationException("Failed to get response from API call.")))
            {
                // reads response body
                string responseText = await responseReader.ReadToEndAsync();

                return responseText;
            }
        }

        /// <exception cref="ArgumentNullException">Any input parameters are null.</exception>
        /// <inheritdoc />
        public async Task<string> PostApiCall(OAuthToken oauthToken, Uri apiUri, string jsonBody)
        {
            if (oauthToken == null)
            {
                throw new ArgumentNullException(nameof(oauthToken));
            }

            if (apiUri == null)
            {
                throw new ArgumentNullException(nameof(apiUri));
            }

            if (string.IsNullOrWhiteSpace(jsonBody))
            {
                throw new ArgumentNullException(nameof(jsonBody));
            }

            var postData = Encoding.ASCII.GetBytes(jsonBody);

            // sends the request
            var apiRequest = (HttpWebRequest)WebRequest.Create(apiUri.AbsoluteUri);
            apiRequest.Method = "POST";
            apiRequest.Headers.Add($"Authorization: Bearer {oauthToken.AccessToken}");
            apiRequest.ContentType = "application/json";
            apiRequest.ContentLength = postData.Length;

            using (var stream = await apiRequest.GetRequestStreamAsync())
            {
                await stream.WriteAsync(postData, 0, postData.Length);
            }

            // gets the response
            var apiResponse = await apiRequest.GetResponseAsync();
            using (var responseReader = new StreamReader(apiResponse.GetResponseStream() ?? throw new InvalidOperationException("Failed to get response from API call.")))
            {
                // reads response body
                string responseText = await responseReader.ReadToEndAsync();

                return responseText;
            }
        }

        /// <exception cref="ArgumentNullException">Any input parameters are null.</exception>
        /// <inheritdoc />
        public async Task<string> UploadBinaryCall(OAuthToken oauthToken, Uri apiUri, string fileName, byte[] data)
        {
            if (oauthToken == null)
            {
                throw new ArgumentNullException(nameof(oauthToken));
            }

            if (apiUri == null)
            {
                throw new ArgumentNullException(nameof(apiUri));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (data == null || !data.Any())
            {
                throw new ArgumentNullException(nameof(data));
            }

            // sends the request
            var apiRequest = (HttpWebRequest)WebRequest.Create(apiUri.AbsoluteUri);
            apiRequest.Method = "POST";
            apiRequest.Headers.Add($"Authorization: Bearer {oauthToken.AccessToken}");
            apiRequest.Headers.Add($"X-Goog-Upload-File-Name: {fileName}");
            apiRequest.Headers.Add("X-Goog-Upload-Protocol: raw");
            apiRequest.ContentType = "application/octet-stream";
            apiRequest.ContentLength = data.Length;

            using (var stream = await apiRequest.GetRequestStreamAsync())
            {
                await stream.WriteAsync(data, 0, data.Length);
            }

            // gets the response
            var apiResponse = await apiRequest.GetResponseAsync();
            using (var responseReader = new StreamReader(apiResponse.GetResponseStream() ?? throw new InvalidOperationException("Failed to get response from API call.")))
            {
                // reads response body
                string responseText = await responseReader.ReadToEndAsync();

                return responseText;
            }
        }
    }
}
