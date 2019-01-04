namespace HardySoft.PhotosUploaderDownloader.Abstractions.Security
{
    using System;

    /// <summary>
    /// A class represents the structure of OAuth token after successful authentication.
    /// </summary>
    public class OAuthToken
    {
        private DateTime authenticatedTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthToken"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="idToken">The Id token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="expiresInSeconds">The number of seconds the token is valid for.</param>
        public OAuthToken(string accessToken, string idToken, string refreshToken, int expiresInSeconds)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            if (expiresInSeconds < 0)
            {
                throw new ArgumentException("Must be a positive number", nameof(expiresInSeconds));
            }

            this.authenticatedTime = DateTime.Now;
            this.AccessToken = accessToken;
            this.IdToken = idToken;
            this.RefreshToken = refreshToken;
            this.ExpiresInSeconds = expiresInSeconds;
        }

        /// <summary>
        /// Gets the token that your application sends to authorize an OAuth request.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Gets a security token that contains Claims about the user being authenticated.
        /// </summary>
        public string IdToken { get; }

        /// <summary>
        /// Gets a token that you can use to obtain a new access token. Refresh tokens are valid until the user revokes access.
        /// </summary>
        public string RefreshToken { get; }

        /// <summary>
        /// Gets the remaining lifetime of the access token in seconds.
        /// </summary>
        public int ExpiresInSeconds { get; }

        /// <summary>
        /// Gets the token type.
        /// </summary>
        public string TokenType => "Bearer";

        /// <summary>
        /// Gets a value indicating whether the token has expired or not.
        /// </summary>
        public bool IsTokenExpired => DateTime.Now.AddSeconds(this.ExpiresInSeconds * -1) < this.authenticatedTime;

        /// <summary>
        /// Renew a token.
        /// </summary>
        public void RenewToken()
        {
            this.authenticatedTime = DateTime.Now;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return $"{this.AccessToken} - {this.IdToken} - {this.RefreshToken}".GetHashCode();
        }
    }
}
