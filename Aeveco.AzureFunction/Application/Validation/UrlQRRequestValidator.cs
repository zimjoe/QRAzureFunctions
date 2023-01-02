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
            RuleFor(x => x.Url)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(2048)
                .Custom((val, context)=>{
                    // null is checked above and the cascade will not make it this far.
                    if (!(val.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || val.StartsWith("http://", StringComparison.OrdinalIgnoreCase)))
                    {
                        context.AddFailure("Must stat with https:// or https://");
                    }
                    if (!Uri.TryCreate(val, UriKind.RelativeOrAbsolute, out Uri? myUri))
                    {
                        context.AddFailure($"'{val}' is not a valid url.");
                    }
                });
        }
    }
}
