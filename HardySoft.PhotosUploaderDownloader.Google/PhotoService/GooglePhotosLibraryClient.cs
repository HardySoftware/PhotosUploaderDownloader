﻿namespace HardySoft.PhotosUploaderDownloader.Google.PhotoService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstraction.PhotoService;
    using Abstraction.Security;
    using ApiClient;
    using Newtonsoft.Json;

    /// <summary>
    /// The Google Photos Library client class.
    /// </summary>
    public class GooglePhotosLibraryClient : IPhotoServiceClient
    {
        /// <summary>
        /// The Google Api client implementation.
        /// </summary>
        private readonly IApiClient apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GooglePhotosLibraryClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Google Api client implementation.</param>
        public GooglePhotosLibraryClient(IApiClient apiClient)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <exception cref="ArgumentNullException">If any of the input parameter is null.</exception>
        /// <inheritdoc />
        public async Task<string> GetUserInfo(OAuthToken oauthToken)
        {
            if (oauthToken == null)
            {
                throw new ArgumentNullException(nameof(oauthToken));
            }

            const string userInfoRequestUri = "https://www.googleapis.com/oauth2/v3/userinfo";

            return await this.apiClient.GetApiCall(oauthToken, new Uri(userInfoRequestUri));
        }

        /// <exception cref="ArgumentNullException">If any of the input parameter is null.</exception>
        /// <inheritdoc />
        public async Task<IEnumerable<Album>> GetAlbums(OAuthToken oauthToken)
        {
            if (oauthToken == null)
            {
                throw new ArgumentNullException(nameof(oauthToken));
            }

            var albumDtoList = new List<AlbumsResponseDto>();

            // builds the request, refer to https://developers.google.com/photos/library/reference/rest/v1/albums/list for further details.
#if DEBUG
            string albumRequestBaseUri = "https://photoslibrary.googleapis.com/v1/albums?pageSize=2";
#else
            string albumRequestBaseUri = "https://photoslibrary.googleapis.com/v1/albums?pageSize=50";
#endif

            AlbumsResponseDto albumDto;
            string nextPageToken = string.Empty;

            do
            {
                var uri = $"{albumRequestBaseUri}&pageToken={nextPageToken}";

                // Get all albums from the library.
                var jsonResponse = await this.apiClient.GetApiCall(oauthToken, new Uri(uri));

                albumDto = JsonConvert.DeserializeObject<AlbumsResponseDto>(jsonResponse);

                if (albumDto != null)
                {
                    albumDtoList.Add(albumDto);
                    nextPageToken = albumDto.NextPageToken;
                }
                else
                {
                    break;
                }
            }
            while (!string.IsNullOrWhiteSpace(albumDto.NextPageToken));

            // Find all album objects
            var allAlbums = from aList in albumDtoList
                from a in aList.Albums
                select a.Map();

            return allAlbums;
        }

        /// <exception cref="ArgumentNullException">If any of the input parameter is null.</exception>
        /// <exception cref="AlbumAlreadyExistsException">If the title already exists in target Google Photos Library.</exception>
        /// <inheritdoc />
        public async Task<Album> CreateAlbum(string title, OAuthToken oauthToken)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (oauthToken == null)
            {
                throw new ArgumentNullException(nameof(oauthToken));
            }

            var existingAlbums = await this.GetAlbums(oauthToken);

            if (existingAlbums.Any(x => string.Compare(x.Title, title, StringComparison.OrdinalIgnoreCase) == 0))
            {
                // In Google Photos Library the "title" is not unique, but this application would rather treat it unique.
                throw new AlbumAlreadyExistsException(title);
            }

            var albumCreationRequestUrl = "https://photoslibrary.googleapis.com/v1/albums";

            var jsonBody = $"{{\"album\": {{ \"title\": \"{title}\"}} }}";

            var jsonResponse = await this.apiClient.PostApiCall(oauthToken, new Uri(albumCreationRequestUrl), jsonBody);

            var album = JsonConvert.DeserializeObject<AlbumDto>(jsonResponse).Map();

            // Looks like a bug from Google https://issuetracker.google.com/issues/121998358
            album.IsWriteable = true;

            return album;
        }
    }
}
