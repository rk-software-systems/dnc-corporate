using Microsoft.AspNetCore.Mvc.Razor;

namespace DNCCorporate.Public.Web.Framework.ThemeCustomization
{
    /// <summary>
    /// This class is used to override default view search.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        private readonly ThemeSettings _settings;

        public ViewLocationExpander(ThemeSettings settings)
        {
            _settings = settings;
        }

        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            string[] locations = new string[]
            {
                $"/Themes/{_settings.CurrentTheme}/Shared/{{0}}.cshtml",
                $"/Themes/{_settings.CurrentTheme}/{{2}}/{{1}}/{{0}}.cshtml",
                "/Themes/Default/{2}/{1}/{0}.cshtml"
            };

            return locations.Union(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Values["themeviewlocations"] = nameof(ViewLocationExpander);
        }
    }
}
