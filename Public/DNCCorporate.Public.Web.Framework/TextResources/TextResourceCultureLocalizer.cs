using System.Reflection;
using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Framework.TextResources;

public class TextResourceCultureLocalizer
{
	private readonly IStringLocalizer _localizer;

	public TextResourceCultureLocalizer(IStringLocalizerFactory factory)
	{
		ArgumentNullException.ThrowIfNull(factory, nameof(factory));

		var fullName = Assembly.GetEntryAssembly()?.FullName ?? "DNCCorporate.Public.Web";
		var assemblyName = new AssemblyName(fullName);
		_localizer = factory.Create(nameof(TextResources), assemblyName.Name);
	}

	// if we have formatted string we can provide arguments         
	// e.g.: @Localizer.Text("Hello {0}", User.Name)
	public LocalizedString Text(string key, params string[] arguments)
	{
		return arguments == null
			? _localizer[key]
			: _localizer[key, arguments];
	}

	public string CurrentCulture => System.Globalization.CultureInfo.CurrentCulture.Name;
}
