namespace HardySoft.PhotosUploaderDownloader.Google.Test.GooglePhotosLibraryClient
{
    using System;
    using PhotoService;
    using Xunit;

    public class ConstructorTests
    {
        [Fact]
        public void Null_ApiClient_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new GooglePhotosLibraryClient(null));
            Assert.Equal("apiClient", ex.ParamName);
        }
    }
}
