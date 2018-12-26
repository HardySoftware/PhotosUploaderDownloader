namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// A data transfer object to match with Google's new media response definition.
    /// </summary>
    internal class NewMediaItemResponseDto
    {
        /// <summary>
        /// Gets or sets the upload token used while the media was uploaded successfully.
        /// </summary>
        [JsonProperty("uploadToken")]
        public string UploadToken { get; set; }

        /// <summary>
        /// Gets or sets the media creation status.
        /// </summary>
        [JsonProperty("status")]
        public MediaCreationStatusResponseDto Status { get; set; }

        /// <summary>
        /// Gets or sets the details of the media item created.
        /// </summary>
        [JsonProperty("mediaItem")]
        public MediaItemResponseDto MediaItem { get; set; }
    }
}
