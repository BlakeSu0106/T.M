using System.Text.Json.Serialization;

namespace Telligent.Member.Application.Dtos.ThirdPartyLogin;

public class GoogleTokenDto
{
    /// <summary>
    /// A token that can be sent to a Google API
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// The remaining lifetime of the access token in seconds
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// A JWT that contains identity information about the user that is digitally signed by Google
    /// </summary>
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }

    /// <summary>
    /// The scopes of access granted by the access_token expressed as a list of space-delimited, case-sensitive strings
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    /// <summary>
    /// Identifies the type of token returned. At this time, this field always has the value Bearer
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    /// <summary>
    /// This field is only present if the access_type parameter was set to offline in the authentication request
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}