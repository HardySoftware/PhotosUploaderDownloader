namespace HardySoft.PhotosUploaderDownloader.Abstractions.PhotoService
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
        /// <param name="title">The albumTitle of the album.</param>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with album information.</returns>
        Task<Album> CreateAlbum(string title, OAuthToken oauthToken);

        /// <summary>
        /// Uploads brand new photos to a album.
        /// </summary>
        /// <param name="albumTitle">The photo album albumTitle to have the photo.</param>
        /// <param name="photoMetas">The meta data of all photos to upload.</param>
        /// <param name="oauthToken">The token returned from OAuth authentication.</param>
        /// <returns>The asynchronous operation task with album and all photos uploaded within this album information.</returns>
        Task<Album> UploadPhotosToAlbum(string albumTitle, List<PhotoMeta> photoMetas, OAuthToken oauthToken);
    }
}
