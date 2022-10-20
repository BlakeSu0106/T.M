using System.Text.Json.Serialization;

namespace Telligent.Member.Application.Dtos.ThirdPartyLogin;

public class FacebookTokenDto
{
    /// <summary>
    /// access-token
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// type
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    /// <summary>
    /// seconds-til-expiration
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// id-token
    /// </summary>
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }
}