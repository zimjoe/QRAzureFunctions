using Aeveco.AzureFunction.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction.Application.Validation;
    public class PlainTextQRRequestValidator : AbstractValidator<PlainTextQRRequest>
    {
    public PlainTextQRRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(500);
    }
}

