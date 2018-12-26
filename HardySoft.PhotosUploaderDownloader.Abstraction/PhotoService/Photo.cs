namespace HardySoft.PhotosUploaderDownloader.Abstraction.PhotoService
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A photo object.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Photo"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of a photo in target on-line photo service.</param>
        /// <param name="photoLink">The link to the photo.</param>
        public Photo(string id, Uri photoLink)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Id = id;
            this.PhotoLink = photoLink ?? throw new ArgumentNullException(nameof(photoLink));
        }

        /// <summary>
        /// Gets the identifier of an album.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the link of the album.
        /// </summary>
        public Uri PhotoLink { get; }

        /// <summary>
        /// Gets or sets the base Url to access the raw data of the photo for direct download.
        /// </summary>
        public Uri BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the photo.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the title of the photo.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the date/time when the photo was created.
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}
