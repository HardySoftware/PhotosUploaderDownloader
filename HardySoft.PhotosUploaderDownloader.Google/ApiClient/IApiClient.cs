namespace HardySoft.PhotosUploaderDownloader.Google.ApiClient
{
    using System;
    using System.Threading.Tasks;
    using Abstraction.Security;

    /// <summary>
    /// An interface to define the supported Google Api client operations.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Make Http Get calls to Google Api.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <param name="apiUri">The Uri of the Api.</param>
        /// <returns>The asynchronous operation task with Api response.</returns>
        Task<string> GetApiCall(OAuthToken oauthToken, Uri apiUri);

        /// <summary>
        /// Make Http Post calls to Google Api.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <param name="apiUri">The Uri of the Api.</param>
        /// <param name="jsonBody">The JSON string body.</param>
        /// <returns>The asynchronous operation task with Api response.</returns>
        Task<string> PostApiCall(OAuthToken oauthToken, Uri apiUri, string jsonBody);
    }
}
