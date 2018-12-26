namespace HardySoft.PhotosUploaderDownloader.Abstraction.PhotoService
{
    /// <summary>
    /// Geographical location.
    /// </summary>
    public class GeoCoordinate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoCoordinate"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        public GeoCoordinate(decimal latitude, decimal longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Gets the latitude of the location.
        /// </summary>
        public decimal Latitude { get; }

        /// <summary>
        /// Gets the longitude of the location.
        /// </summary>
        public decimal Longitude { get; }
    }
}
