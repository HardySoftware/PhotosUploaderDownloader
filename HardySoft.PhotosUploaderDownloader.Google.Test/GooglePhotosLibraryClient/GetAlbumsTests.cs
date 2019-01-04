namespace HardySoft.PhotosUploaderDownloader.Google.Test.GooglePhotosLibraryClient
{
    using System;
    using System.Threading.Tasks;
    using Abstraction.Security;
    using ApiClient;
    using AutoFixture;
    using Moq;
    using PhotoService;
    using Xunit;

    public class GetAlbumsTests
    {
        private Mock<IApiClient> apiClientMock;

        public GetAlbumsTests()
        {
            this.apiClientMock = new Mock<IApiClient>();
        }

        [Fact]
        public async Task Null_OAuthToken_ThrowsAsync()
        {
            var client = new GooglePhotosLibraryClient(this.apiClientMock.Object);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.GetUserInfo(null));

            Assert.Equal("oauthToken", ex.ParamName);
        }

        [Fact]
        public async Task User_Info_Returned()
        {
            var client = new GooglePhotosLibraryClient(this.apiClientMock.Object);
            var fixture = new Fixture();
            var token = fixture.Create<OAuthToken>();

            this.apiClientMock.Setup(x => x.GetApiCall(token, It.IsAny<Uri>())).Returns(Task.FromResult("my json response"));

            var actual = await client.GetUserInfo(token);

            Assert.Equal("my json response", actual);
        }
    }
}
