namespace DNCCorporate.Services;

public class GoogleReCaptchaSettings
{
    public required string SiteKey { get; set; }

    public required string SecretKey { get; set; }

    public required double MinimumScore { get; set; }
}
