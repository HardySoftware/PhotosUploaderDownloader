/*
 * This is a template file for HardySoft.PhotosUploaderDownloader.Google\Security\Secret.cs, which is not included in the version control system.
 * You need to get your own values from https://api.imgur.com/#registerapp, then copy the content to the file as described above.
 */

namespace HardySoft.PhotosUploaderDownloader.Imgur.Security
{
    /// <summary>
    /// A class to have the secrets used to communicate with Google Api.
    /// </summary>
    /// <remarks>Please visit https://api.imgur.com/#registerapp to create your own credential and replace values in this class.</remarks>
    internal static class Secrets
    {
        /// <summary>
        /// The Google credential client Id.
        /// </summary>
        public const string ClientId = "398102ddaf93d96";

        /// <summary>
        /// The Google credential client secret.
        /// </summary>
        public const string ClientSecret = "28d18dff0e77e21f4dda25a9302cfd2edf71f6a9";
    }
}
