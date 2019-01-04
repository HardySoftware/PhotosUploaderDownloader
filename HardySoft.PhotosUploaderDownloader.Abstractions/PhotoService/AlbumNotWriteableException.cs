namespace HardySoft.PhotosUploaderDownloader.Abstractions.PhotoService
{
    using System;

    /// <summary>
    /// An exception to throw when the album is not write-able to this application while photos are to be uploaded to it.
    /// </summary>
    public class AlbumNotWriteableException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumNotWriteableException"/> class.
        /// </summary>
        /// <param name="albumTitle">The title of the album which already exists in target photo service.</param>
        public AlbumNotWriteableException(string albumTitle)
            : base($"The album {albumTitle} does not allow this application to write to it.")
        {
            if (string.IsNullOrWhiteSpace(albumTitle))
            {
                throw new ArgumentNullException(nameof(albumTitle));
            }

            this.AlbumTitle = albumTitle;
        }

        /// <summary>
        /// Gets the title of the album.
        /// </summary>
        public string AlbumTitle { get; }
    }
}
