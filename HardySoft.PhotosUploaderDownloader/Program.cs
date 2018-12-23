namespace HardySoft.PhotosUploaderDownloader
{
    using System;
    using System.Threading.Tasks;
    using HardySoft.PhotosUploaderDownloader.Security;

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
            var auth = new OAuthClient();
            await auth.PerformAuthentication();
            Console.WriteLine("Hello World!");
        }
    }
}
