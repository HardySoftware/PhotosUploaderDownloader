namespace HardySoft.PhotosUploaderDownloader.Abstraction.PhotoService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Security;

    /// <summary>
    /// An interface to define operations how to interact with source on-line photo services.
    /// </summary>
    public interface IPhotoServiceClient
    {
        /// <summary>
        /// Gets user info.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with user information.</returns>
        Task<string> GetUserInfo(OAuthToken oauthToken);

        /// <summary>
        /// List all the albums of the user.
        /// </summary>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with albums information.</returns>
        Task<IEnumerable<Album>> GetAlbums(OAuthToken oauthToken);

        /// <summary>
        /// Create a new album.
        /// </summary>
        /// <param name="title">The title of the album.</param>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with album information.</returns>
        Task<Album> CreateAlbum(string title, OAuthToken oauthToken);
    }
}
