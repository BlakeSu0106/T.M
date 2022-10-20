using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Web;
using Telligent.Core.Infrastructure.Services;
using Telligent.Member.Application.Dtos.ThirdPartyLogin;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.Application.AppServices;

public class ThirdPartyLoginAppService : IAppService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ThirdPartyLoginConfigAppService _thirdPartyLoginConfigAppService;

    public ThirdPartyLoginAppService(
        IHttpClientFactory httpClientFactory,
        ThirdPartyLoginConfigAppService thirdPartyLoginConfigAppService)
    {
        _httpClientFactory = httpClientFactory;
        _thirdPartyLoginConfigAppService = thirdPartyLoginConfigAppService;
    }

    /// <summary>
    /// Get Line UserId
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="code"></param>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    /// https://developers.line.biz/zh-hant/docs/line-login/integrate-line-login/#verify-id-token
    public async Task<string> GetLineUserIdAsync(string companyId, string code, string redirectUri)
    {
        var lineConfig =
            await _thirdPartyLoginConfigAppService.GetThirdPartyLoginConfigAsync(Guid.Parse(companyId),
                ThirdPartyChannelType.Line);

        var lineTokenDto = await GetLineTokenAsync(code, redirectUri, lineConfig.ThirdPartyChannelId,
            lineConfig.ThirdPartyChannelSecret);

        if (string.IsNullOrEmpty(lineTokenDto.IdToken))
            throw new Exception("無法取得id_token，請確認Line Login的scope有包含openid");

        var jwt = new JwtSecurityToken(lineTokenDto.IdToken);

        return jwt.Claims.Where(x => x.Type == "sub").Select(x => x.Value).FirstOrDefault();
    }

    /// <summary>
    /// Get Google UserId
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="code"></param>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    public async Task<string> GetGoogleUserIdAsync(string companyId, string code, string redirectUri)
    {
        var googleConfig =
            await _thirdPartyLoginConfigAppService.GetThirdPartyLoginConfigAsync(Guid.Parse(companyId),
                ThirdPartyChannelType.Google);

        var googleTokenDto = await GetGoogleTokenAsync(code, redirectUri, googleConfig.ThirdPartyChannelId,
            googleConfig.ThirdPartyChannelSecret);

        var jwt = new JwtSecurityToken(googleTokenDto.IdToken);

        return jwt.Claims.Where(x => x.Type == "sub").Select(x => x.Value).FirstOrDefault();
    }

    /// <summary>
    /// Get Facebook UserId
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="code"></param>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    /// https://developers.facebook.com/docs/facebook-login/guides/advanced/oidc-token#read-token
    public async Task<string> GetFacebookUserIdAsync(string companyId, string code, string redirectUri)
    {
        var facebookConfig =
            await _thirdPartyLoginConfigAppService.GetThirdPartyLoginConfigAsync(Guid.Parse(companyId),
                ThirdPartyChannelType.Line);

        var facebookDto = await GetFacebookTokenAsync(code, redirectUri, facebookConfig.ThirdPartyChannelId,
            facebookConfig.ThirdPartyChannelSecret);

        if (string.IsNullOrEmpty(facebookDto.IdToken))
            throw new Exception("無法取得id_token，請確認Facebook Login有傳入code_challenge且scope有包含openid");

        var jwt = new JwtSecurityToken(facebookDto.IdToken);

        return jwt.Claims.Where(x => x.Type == "sub").Select(x => x.Value).FirstOrDefault();
    }

    /// <summary>
    /// Get Line Token
    /// </summary>
    /// <param name="code"></param>
    /// <param name="redirectUri"></param>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <returns></returns>
    /// https://developers.line.biz/zh-hant/docs/line-login/integrate-line-login/#get-access-token
    private async Task<LineTokenDto> GetLineTokenAsync(string code, string redirectUri, string clientId,
        string clientSecret)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var parameters = HttpUtility.ParseQueryString("");
        parameters.Add("grant_type", "authorization_code");
        parameters.Add("code", code);
        parameters.Add("redirect_uri", redirectUri);
        parameters.Add("client_id", clientId);
        parameters.Add("client_secret", clientSecret);

        var content = new StringContent(parameters.ToString() ?? throw new InvalidOperationException(), Encoding.UTF8,
            "application/x-www-form-urlencoded");

        var response = await httpClient.PostAsync("https://api.line.me/oauth2/v2.1/token", content);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"取得Line Token失敗, status code: {response.StatusCode}, content: {responseContent}");

        return JsonSerializer.Deserialize<LineTokenDto>(responseContent);
    }

    /// <summary>
    /// Get tGoogle Token
    /// </summary>
    /// <param name="code"></param>
    /// <param name="redirectUri"></param>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <returns></returns>
    /// https://developers.google.com/identity/protocols/oauth2/openid-connect#exchangecode
    private async Task<GoogleTokenDto> GetGoogleTokenAsync(string code, string redirectUri, string clientId,
        string clientSecret)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var parameters = HttpUtility.ParseQueryString("");
        parameters.Add("grant_type", "authorization_code");
        parameters.Add("code", code);
        parameters.Add("redirect_uri", redirectUri);
        parameters.Add("client_id", clientId);
        parameters.Add("client_secret", clientSecret);

        var content = new StringContent(parameters.ToString() ?? throw new InvalidOperationException(), Encoding.UTF8,
            "application/x-www-form-urlencoded");

        var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"取得Google Token失敗, status code: {response.StatusCode}, content: {responseContent}");

        return JsonSerializer.Deserialize<GoogleTokenDto>(responseContent);
    }

    /// <summary>
    /// Get Facebook Token
    /// </summary>
    /// <param name="code"></param>
    /// <param name="redirectUri"></param>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <returns></returns>
    /// https://developers.facebook.com/docs/facebook-login/guides/advanced/oidc-token#exchange-code
    private async Task<FacebookTokenDto> GetFacebookTokenAsync(string code, string redirectUri, string clientId,
        string clientSecret)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var parameters = HttpUtility.ParseQueryString("");
        parameters.Add("grant_type", "authorization_code");
        parameters.Add("code", code);
        parameters.Add("redirect_uri", redirectUri);
        parameters.Add("client_id", clientId);
        parameters.Add("client_secret", clientSecret);

        var content = new StringContent(parameters.ToString() ?? throw new InvalidOperationException(), Encoding.UTF8,
            "application/x-www-form-urlencoded");

        var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"取得Facebook Token失敗, status code: {response.StatusCode}, content: {responseContent}");

        return JsonSerializer.Deserialize<FacebookTokenDto>(responseContent);
    }
}