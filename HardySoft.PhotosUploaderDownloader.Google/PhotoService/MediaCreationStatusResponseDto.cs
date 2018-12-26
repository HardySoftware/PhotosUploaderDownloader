namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// A data transfer object to match with Google's media creation status definition.
    /// </summary>
    internal class MediaCreationStatusResponseDto
    {
        /// <summary>
        /// Gets or sets the message of the status.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the code of the status.
        /// </summary>
        [JsonProperty("code")]
        public int? Code { get; set; }
    }
}
