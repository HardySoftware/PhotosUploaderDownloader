namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// A data transfer object to match with Google's media item definition.
    /// </summary>
    internal class MediaItemDto
    {
        /// <summary>
        /// Gets or sets the description of the media item.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the simple media item.
        /// </summary>
        [JsonProperty("simpleMediaItem")]
        public SimpleMediaItemDto SimpleMediaItem { get; set; }
    }
}