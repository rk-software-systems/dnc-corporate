using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Framework.TextResources;

public class TextResourceCultureLocalizer
{
	private readonly IStringLocalizer _localizer;

	public TextResourceCultureLocalizer(IStringLocalizerFactory factory)
	{
		ArgumentNullException.ThrowIfNull(factory, nameof(factory));

		_localizer = factory.Create(string.Empty, string.Empty);
	}

	// if we have formatted string we can provide arguments         
	// e.g.: @Localizer.Text("Hello {0}", User.Name)
	public LocalizedString Text(string key, params string[] arguments)
	{
		return arguments == null
			? _localizer[key]
			: _localizer[key, arguments];
	}

#pragma warning disable CA1822 // Mark members as static
    public string CurrentCulture => CurrentCultureHelper.CurrentCulture;
#pragma warning restore CA1822 // Mark members as static
}
