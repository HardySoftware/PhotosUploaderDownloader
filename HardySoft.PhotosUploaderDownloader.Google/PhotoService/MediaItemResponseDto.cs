namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// A data transfer object to match with Google's created media item response.
    /// </summary>
    internal class MediaItemResponseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the media item created.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Url of the media item.
        /// </summary>
        [JsonProperty("productUrl")]
        public string ProductUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the media item.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the base Url of the media item.
        /// </summary>
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the meta data of the media item.
        /// </summary>
        [JsonProperty("mediaMetadata")]
        public MediaMetadataResponseDto MediaMetadata { get; set; }
    }
}
