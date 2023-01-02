﻿using Aeveco.AzureFunction.Application.Models;
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
            // not sure that I should be forcing some validation
            RuleFor(x => x.Passcode).NotEmpty().MinimumLength(16).WithMessage("Use a minimum of 16 characters for your passcode. Don't make that easy to steal.").MaximumLength(200);
        }
    }
}