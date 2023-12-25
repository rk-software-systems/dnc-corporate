﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DNCCorporate.Public.Web.Framework.Localizations;

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
            model.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel
                {
                    Order = -1,
                    Template = AttributeRouteModel.CombineTemplates("{culture?}", selector.AttributeRouteModel.Template),
                }
            });
        }
    }
}