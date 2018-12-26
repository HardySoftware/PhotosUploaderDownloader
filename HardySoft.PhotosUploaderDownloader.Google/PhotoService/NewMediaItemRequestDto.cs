namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// A data transfer object to match with Google's media request item definition.
    /// </summary>
    internal class NewMediaItemRequestDto
    {
        /// <summary>
        /// Gets or sets the description of the media item.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the file name of the media item.
        /// </summary>
        /// <remarks>
        /// This property is neither sent to Api call nor returned from Api call,
        /// it is added as an identifier to track if the media has been uploaded successfully or not.
        /// </remarks>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the simple media item.
        /// </summary>
        [JsonProperty("simpleMediaItem")]
        public SimpleMediaItemRequestDto SimpleMediaItem { get; set; }
    }
}