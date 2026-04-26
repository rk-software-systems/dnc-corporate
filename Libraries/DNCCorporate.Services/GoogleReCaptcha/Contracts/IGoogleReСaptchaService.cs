namespace DNCCorporate.Services;

public interface IGoogleReCaptchaService
{
    string SiteKey { get; }

    Task<bool> Verify(string token);
}
