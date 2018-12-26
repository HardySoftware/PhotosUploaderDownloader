namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// A batch create request data transfer object.
    /// </summary>
    internal class BatchMediaItemCreateRequestDto
    {
        /// <summary>
        /// Gets or sets the album Id the uploaded photos belong to.
        /// </summary>
        [JsonProperty("albumId")]
        public string AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the media items to be created in the album.
        /// </summary>
        [JsonProperty("newMediaItems")]
        public NewMediaItemRequestDto[] MediaItems { get; set; }
    }
}
