namespace HardySoft.PhotosUploaderDownloader.Abstractions.Security
{
    using System.Threading.Tasks;

    /// <summary>
    /// An interface to define the OAuth client operations.
    /// </summary>
    public interface IOAuthClient
    {
        /// <summary>
        /// Perform OAuth authentication.
        /// </summary>
        /// <returns>The asynchronous operation task.</returns>
        Task<OAuthToken> PerformAuthentication();
    }
}
