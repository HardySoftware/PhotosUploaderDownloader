namespace HardySoft.PhotosUploaderDownloader.Abstraction.PhotoService
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// A photo album object.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// The photos in this album.
        /// </summary>
        private List<Photo> photos;

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
            this.photos = new List<Photo>();
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

        /// <summary>
        /// Gets all the photos of this album.
        /// </summary>
        public ReadOnlyCollection<Photo> Photos => new ReadOnlyCollection<Photo>(this.photos);

        /// <summary>
        /// Adds a photo to this album.
        /// </summary>
        /// <param name="photo">The photo object to add to the album.</param>
        public void AddPhoto(Photo photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException(nameof(photo));
            }

            if (this.photos.Any(x => x.Id == photo.Id))
            {
                throw new InvalidOperationException($"The photo with Id {photo.Id} has already been added to the album.");
            }

            this.photos.Add(photo);
        }
    }
}
