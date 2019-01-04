namespace HardySoft.PhotosUploaderDownloader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Abstractions.PhotoService;
    using Google.ApiClient;
    using Google.PhotoService;
    using Google.Security;

    /// <summary>
    /// Main class of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>The asynchronous operation task.</returns>
        public static async Task Main(string[] args)
        {
            // https://github.com/googlesamples/oauth-apps-for-windows/tree/master/OAuthConsoleApp
            var auth = new GoogleOAuthClient();
            var token = await auth.PerformAuthentication();

            var api = new GooglePhotosLibraryClient(new ApiClient());

            // var albums = await api.GetAlbums(token);
            // var album = await api.CreateAlbum("Test1", token);
            var photoMetas = new List<PhotoMeta>()
            {
                new PhotoMeta(
                    File.ReadAllBytes(
                        @"C:\Users\hardy\Pictures\Flickr\flickr-downloadr-[2015 Turkey - Sumela monastery]-2018-11-05_21-36-46\23738517934.jpg"),
                    "23738517934.jpg")
                {
                    Title = "Sumela monastery",
                    Description = "Sumela monastery description",
                    Location = new GeoCoordinate(40.690229m, 39.657554m)
                }
            };

            await api.UploadPhotosToAlbum("Test1", photoMetas, token);

            // Console.WriteLine($"Hello World! {album}");
        }
    }
}
