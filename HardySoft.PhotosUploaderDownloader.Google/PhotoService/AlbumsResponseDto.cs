namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using Newtonsoft.Json;

    /// <summary>
    /// An album response DTO object to match with Google's definition.
    /// </summary>
    internal class AlbumsResponseDto
    {
        /// <summary>
        /// Gets or sets an array of album object returned from a service call.
        /// </summary>
        [JsonProperty("albums")]
        public AlbumDto[] Albums { get; set; }

        /// <summary>
        /// Gets or sets next page token if there are more albums to return beyond this single call.
        /// </summary>
        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }
    }
}
