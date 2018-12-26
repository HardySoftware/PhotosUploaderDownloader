namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction.PhotoService;
    using Newtonsoft.Json;

    /// <summary>
    /// A batch create response data transfer object.
    /// </summary>
    internal class BatchMediaItemCreateResponseDto
    {
        /// <summary>
        /// Gets or sets the list of result from Google media creation Api call.
        /// </summary>
        [JsonProperty("NewMediaItemResults")]
        public List<NewMediaItemResponseDto> NewMediaItemResults { get; set; }

        /// <summary>
        /// Maps all data transfer media response objects with successful status into domain photo object list.
        /// </summary>
        /// <returns>List of domain photo objects.</returns>
        public IEnumerable<Photo> Map()
        {
            if (this.NewMediaItemResults != null)
            {
                var photos = from media in this.NewMediaItemResults
                    where !media.Status.Code.HasValue
                    select new Photo(media.MediaItem.Id, new Uri(media.MediaItem.ProductUrl))
                    {
                        Description = media.MediaItem.Description,
                        CreationTime = media.MediaItem.MediaMetadata.CreationTime
                    };

                return photos;
            }

            return null;
        }
    }
}
