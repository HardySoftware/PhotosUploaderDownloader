namespace HardySoft.PhotosUploaderDownloader.Abstractions.PhotoService
{
    using System;

    /// <summary>
    /// An exception to throw when the application tries to create a duplication album.
    /// </summary>
    public class AlbumAlreadyExistsException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="albumTitle">The title of the album which already exists in target photo service.</param>
        public AlbumAlreadyExistsException(string albumTitle)
            : base($"The album with the same title {albumTitle} already exists.")
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
