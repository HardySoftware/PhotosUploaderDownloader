namespace HardySoft.PhotosUploaderDownloader.Abstraction.PhotoService
{
    using System;

    /// <summary>
    /// A exception to throw when the application tries to create a duplication album.
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
        }
    }
}
