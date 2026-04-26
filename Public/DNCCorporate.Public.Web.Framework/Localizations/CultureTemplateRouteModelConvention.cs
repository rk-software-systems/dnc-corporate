using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DNCCorporate.Public.Web.Framework;

///<summary>
/// Configure {lang?} as first route parameter in the request path
///</summary>
public class CultureTemplateRouteModelConvention : IPageRouteModelConvention
{        
    public void Apply(PageRouteModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var selectorCount = model.Selectors.Count;
        for (var i = 0; i < selectorCount; i++)
        {
            var selector = model.Selectors[i];
            if (!string.IsNullOrEmpty(selector.AttributeRouteModel?.Template))
            {
                model.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Name = selector.AttributeRouteModel.Name,
                        Order = -1,
                        Template = AttributeRouteModel.CombineTemplates("{culture?}", selector.AttributeRouteModel.Template),
                    }
                });
            }
        }
    }
}
