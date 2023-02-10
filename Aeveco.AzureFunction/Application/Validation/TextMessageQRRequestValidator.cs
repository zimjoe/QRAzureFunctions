using Aeveco.AzureFunction.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction.Application.Validation
{
    public class TextMessageQRRequestValidator : AbstractValidator<TextMessageQRRequest>
    {
        public TextMessageQRRequestValidator()
        {
            // not sure that I should be forcing some number validation
            RuleFor(x => x.ToNumber)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(25);
            RuleFor(x => x.Message).MaximumLength(144);
           

        }
    }
}
