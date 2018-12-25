namespace HardySoft.PhotosUploaderDownloader
{
    using System;
    using System.Threading.Tasks;
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
            var album = await api.CreateAlbum("Test1", token);
            Console.WriteLine($"Hello World! {album}");
        }
    }
}
