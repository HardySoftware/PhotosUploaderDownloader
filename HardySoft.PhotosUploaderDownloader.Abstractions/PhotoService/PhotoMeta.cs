namespace HardySoft.PhotosUploaderDownloader.Abstractions.PhotoService
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The meta data of a photo.
    /// </summary>
    public class PhotoMeta
    {
        /// <summary>
        /// A list of support file extensions.
        /// </summary>
        private static readonly string[] SupportedFileTypes = new string[] { ".jpg", ".gif", ".bmp", ".png" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoMeta"/> class.
        /// </summary>
        /// <param name="photoData">The byte array data of the photo.</param>
        /// <param name="fileName">The file name of the photo.</param>
        public PhotoMeta(byte[] photoData, string fileName)
        {
            if (photoData == null || !photoData.Any())
            {
                throw new ArgumentNullException(nameof(photoData));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fi = new FileInfo(fileName);

            if (!SupportedFileTypes.Contains(fi.Extension.ToLower()))
            {
                throw new ArgumentException($"Only supported file types {string.Join(", ", SupportedFileTypes)} are allowed.", nameof(fileName));
            }

            this.PhotoData = photoData;
            this.FileName = fileName;
        }

        /// <summary>
        /// Gets the file name of the photo.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the byte array of the photo data.
        /// </summary>
        public byte[] PhotoData { get; }

        /// <summary>
        /// Gets or sets the title of the photo.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the photo.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location where the photo was taken.
        /// </summary>
        public GeoCoordinate Location { get; set; }
    }
}
