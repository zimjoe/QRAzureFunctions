using Aeveco.AzureFunction.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction.Application.Validation
{
    public class UrlQRRequestValidator:AbstractValidator<UrlQRRequest>
    {
        public UrlQRRequestValidator()
        {
            RuleFor(x => x.Url).NotEmpty().MinimumLength(10);
        }
    }
}
