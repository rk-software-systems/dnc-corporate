using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace DNCCorporate.Public.Web.Framework.TextResources;

public class JsonStringLocalizer : IStringLocalizer
{

    #region consts                  
    #endregion

    #region fields                  
    #endregion

    #region props       

    public LocalizedString this[string name] => throw new NotImplementedException();

    public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

    #endregion

    #region ctors

    public JsonStringLocalizer()
    {
    }
    #endregion

    #region methods

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region helpers
    

    #endregion
}
