namespace HardySoft.PhotosUploaderDownloader.Abstraction.PhotoService
{
    using System;

    /// <summary>
    /// An photo album object.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="id">The identifier of an album.</param>
        /// <param name="albumLinkUri">The Http link to the album.</param>
        public Album(string id, Uri albumLinkUri)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Id = id;
            this.AlbumLinkUri = albumLinkUri ?? throw new ArgumentNullException(nameof(albumLinkUri));
        }

        /// <summary>
        /// Gets the identifier of an album.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the photo album is write-able by the application.
        /// </summary>
        public bool IsWriteable { get; set; }

        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the link of the album.
        /// </summary>
        public Uri AlbumLinkUri { get; }
    }
}
