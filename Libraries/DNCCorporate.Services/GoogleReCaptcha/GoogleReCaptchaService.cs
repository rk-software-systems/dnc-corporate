using System.Text.Json;
using DNCCorporate.ViewModels;
using Microsoft.Extensions.Options;

namespace DNCCorporate.Services;

public class GoogleReCaptchaService(HttpClient httpClient, IOptions<GoogleReCaptchaSettings> options) : IGoogleReCaptchaService
{
    #region fields

    private readonly GoogleReCaptchaSettings _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    private readonly HttpClient _httpClient = httpClient;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    #endregion

    #region properties

    public string SiteKey => _settings.SiteKey;

    #endregion

    #region methods

    public async Task<bool> Verify(string token)
    {
        var url = new Uri("https://www.google.com/recaptcha/api/siteverify");
        using var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("secret", _settings.SecretKey),
                new KeyValuePair<string, string>("response", token)
            ]);
        using var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var json = await response.Content.ReadAsStringAsync();
        var verifyResponse = JsonSerializer.Deserialize<VerifyResponseViewModel>(json, _jsonOptions);

        return verifyResponse?.Success == true && verifyResponse.Score >= _settings.MinimumScore;
    }

    #endregion
}
