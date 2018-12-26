namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using System;
    using Abstraction.PhotoService;
    using Newtonsoft.Json;

    /// <summary>
    /// An album DTO object matching with Google's definition.
    /// </summary>
    internal class AlbumResponseDto
    {
        /// <summary>
        /// Gets or sets the Id of the album.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Url of the album.
        /// </summary>
        [JsonProperty("productUrl")]
        public string AlbumUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the album is write-able to this application.
        /// </summary>
        [JsonProperty("isWriteable")]
        public bool IsWriteable { get; set; }

        /// <summary>
        /// Maps this current album DTO object to domain object.
        /// </summary>
        /// <returns>Domain album object mapped from this DTO.</returns>
        public Album Map()
        {
            var album = new Album(this.Id, new Uri(this.AlbumUrl))
            {
                IsWriteable = this.IsWriteable,
                Title = this.Title,
            };

            return album;
        }
    }
}
