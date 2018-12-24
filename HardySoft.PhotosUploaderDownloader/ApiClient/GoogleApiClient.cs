namespace HardySoft.PhotosUploaderDownloader.ApiClient
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using HardySoft.PhotosUploaderDownloader.Security;

    /// <summary>
    /// The Google Api client class.
    /// </summary>
    internal class GoogleApiClient
    {
        /// <summary>
        /// Gets user info.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with user information.</returns>
        public async Task<string> GetUserInfo(OAuthToken oauthToken)
        {
            // builds the  request
            string userInfoRequestUri = "https://www.googleapis.com/oauth2/v3/userinfo";

            return await this.GetApiCall(oauthToken, new Uri(userInfoRequestUri));
        }

        /// <summary>
        /// List all the albums of the user.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with albums information.</returns>
        public async Task<string> GetAlbums(OAuthToken oauthToken)
        {
            // builds the  request
            string userInfoRequestUri = "https://photoslibrary.googleapis.com/v1/albums";

            return await this.GetApiCall(oauthToken, new Uri(userInfoRequestUri));
        }

        private async Task<string> GetApiCall(OAuthToken oauthToken, Uri apiUri)
        {
            // sends the request
            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(apiUri.ToString());
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
    }
}
