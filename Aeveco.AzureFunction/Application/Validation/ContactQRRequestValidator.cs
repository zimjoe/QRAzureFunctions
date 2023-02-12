using Aeveco.AzureFunction.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;
using static QRCoder.PayloadGenerator.ContactData;

namespace Aeveco.AzureFunction.Application.Validation;
public class ContactQRRequestValidator : AbstractValidator<ContactQRRequest>
{
    public ContactQRRequestValidator()
    {
        RuleFor(x => x.ContactOutput)
            .IsInEnum().WithMessage("Use 0:vCard 2.1, 1:vCard 3.0, 2:vCard 4.0 or 3:MeCard.");
        RuleFor(x => x.AddressOrderType)
            .IsInEnum().WithMessage("Use 0:The address format used in European countries and others or 1:The address format used in North America and others.");

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.NickName).MaximumLength(50);
        RuleFor(x => x.Phone).MaximumLength(25);
        RuleFor(x => x.MobilePhone).MaximumLength(25);
        RuleFor(x => x.WorkPhone).MaximumLength(25);

        // nothing for Birthday?

        RuleFor(x => x.Email).MaximumLength(200).EmailAddress();
        RuleFor(x => x.Website).MaximumLength(1048);

        RuleFor(x => x.HouseNumber).MaximumLength(25);
        RuleFor(x => x.Street).MaximumLength(100);
        RuleFor(x => x.City).MaximumLength(100);
        RuleFor(x => x.StateRegion).MaximumLength(50);
        RuleFor(x => x.ZipCode).MaximumLength(20);
        RuleFor(x => x.Country).MaximumLength(50);

        RuleFor(x => x.Organization).MaximumLength(100);
        RuleFor(x => x.Title).MaximumLength(50);

        RuleFor(x => x.Note).MaximumLength(500);
        

    }
}

