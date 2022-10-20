using System.Text.Json.Serialization;

namespace Telligent.Member.Application.Dtos.ThirdPartyLogin;

public class LineTokenDto
{
    /// <summary>
    /// Access token. Valid for 30 days
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// Number of seconds until the access token expires
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// JSON Web Token (JWT) (opens new window)with information about the user
    /// </summary>
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }

    /// <summary>
    /// Token used to get a new access token (refresh token). Valid for 90 days after the access token is issued
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    /// <summary>
    /// Permissions granted to the access token
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    /// <summary>
    /// Bearer
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}