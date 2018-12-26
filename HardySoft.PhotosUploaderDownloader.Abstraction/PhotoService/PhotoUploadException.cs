namespace HardySoft.PhotosUploaderDownloader.Abstraction.PhotoService
{
    using System;
    using System.Linq;

    /// <summary>
    /// An exception to indicate that one or multiple photos failed to be uploaded or added to an album.
    /// </summary>
    public class PhotoUploadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoUploadException"/> class.
        /// </summary>
        /// <param name="successfulAlbumAndPhotos">The album with successful photos uploaded.</param>
        /// <param name="failedFileNames">An array of failed file names.</param>
        public PhotoUploadException(Album successfulAlbumAndPhotos, string[] failedFileNames)
        {
            if (failedFileNames == null || !failedFileNames.Any())
            {
                throw new ArgumentNullException(nameof(failedFileNames));
            }

            this.SuccessfulAlbumAndPhotos = successfulAlbumAndPhotos ?? throw new ArgumentNullException(nameof(successfulAlbumAndPhotos));
            this.FailedFileNames = failedFileNames;
        }

        /// <summary>
        /// Gets the album with successful photos uploaded.
        /// </summary>
        public Album SuccessfulAlbumAndPhotos { get; }

        /// <summary>
        /// Gets an array of failed file names.
        /// </summary>
        public string[] FailedFileNames { get; }
    }
}
