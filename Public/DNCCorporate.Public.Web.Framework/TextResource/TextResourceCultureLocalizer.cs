using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Framework.TextResource
{
    public class TextResourceCultureLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public TextResourceCultureLocalizer(IStringLocalizerFactory factory)
        {
            if(factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var fullName = Assembly.GetEntryAssembly()?.FullName ?? "DNCCorporate.Public.Web";
            var assemblyName = new AssemblyName(fullName);
            _localizer = factory.Create(nameof(TextResource), assemblyName.Name);
        }

        // if we have formatted string we can provide arguments         
        // e.g.: @Localizer.Text("Hello {0}", User.Name)
        public LocalizedString Text(string key, params string[] arguments)
        {
            return arguments == null
                ? _localizer[key]
                : _localizer[key, arguments];
        }
    }
}
