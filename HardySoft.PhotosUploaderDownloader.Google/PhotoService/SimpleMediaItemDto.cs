namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// A data transfer object to match with Google's simple media item definition.
    /// </summary>
    internal class SimpleMediaItemDto
    {
        /// <summary>
        /// Gets or sets the upload token.
        /// </summary>
        [JsonProperty("uploadToken")]
        public string UploadToken { get; set; }
    }
}