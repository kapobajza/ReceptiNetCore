using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using Recepti.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.AttributeAdapters
{
    public class AttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is PasswordValidator)
            {
                return new PasswordValidatorAttributeAdapter(attribute as PasswordValidator, stringLocalizer);
            }
            else
            {
                return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
            }
        }
    }
}
