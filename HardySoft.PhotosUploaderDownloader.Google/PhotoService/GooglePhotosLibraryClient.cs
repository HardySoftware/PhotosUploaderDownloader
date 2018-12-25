namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Abstraction.PhotoService;
    using Abstraction.Security;

    /// <summary>
    /// The Google Photos Library client class.
    /// </summary>
    public class GooglePhotosLibraryClient : IPhotoServiceClient
    {
        /// <inheritdoc />
        public async Task<string> GetUserInfo(OAuthToken oauthToken)
        {
            // builds the  request
            string userInfoRequestUri = "https://www.googleapis.com/oauth2/v3/userinfo";

            return await GetApiCall(oauthToken, new Uri(userInfoRequestUri));
        }

        /// <inheritdoc />
        public async Task<string> GetAlbums(OAuthToken oauthToken)
        {
            // builds the  request
            string albumRequestUri = "https://photoslibrary.googleapis.com/v1/albums";

            return await GetApiCall(oauthToken, new Uri(albumRequestUri));
        }

        /// <inheritdoc />
        public async Task<string> CreateAlbum(string title, OAuthToken oauthToken)
        {
            var albumCreationRequestUrl = "https://photoslibrary.googleapis.com/v1/albums";

            var jsonBody = $"{{\"album\": {{ \"title\": \"{title}\"}} }}";

            return await PostApiCall(oauthToken, new Uri(albumCreationRequestUrl), jsonBody);
        }

        /// <summary>
        /// Make Http Get calls to Google Api.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <param name="apiUri">The Uri of the Api.</param>
        /// <returns>The asynchronous operation task with Api response.</returns>
        private static async Task<string> GetApiCall(OAuthToken oauthToken, Uri apiUri)
        {
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

        /// <summary>
        /// Make Http Post calls to Google Api.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <param name="apiUri">The Uri of the Api.</param>
        /// <param name="jsonBody">The JSON string body.</param>
        /// <returns>The asynchronous operation task with Api response.</returns>
        private static async Task<string> PostApiCall(OAuthToken oauthToken, Uri apiUri, string jsonBody)
        {
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
    }
}
