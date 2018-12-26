namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// A data transfer object to match with Google's media creation response definition.
    /// </summary>
    internal class MediaMetadataResponseDto
    {
        /// <summary>
        /// Gets or sets the width of the media.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the media.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the creation time of the media.
        /// </summary>
        [JsonProperty("creationTime")]
        public DateTime? CreationTime { get; set; }
    }
}
