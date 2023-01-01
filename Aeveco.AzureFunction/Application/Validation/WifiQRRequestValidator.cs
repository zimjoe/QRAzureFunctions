using Aeveco.AzureFunction.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction.Application.Validation
{
    public class WifiQRRequestValidator : AbstractValidator<WifiQRRequest>
    {
        public WifiQRRequestValidator()
        {
            RuleFor(x => x.WifiName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Passcode).NotEmpty().MaximumLength(200);
        }
    }
}
