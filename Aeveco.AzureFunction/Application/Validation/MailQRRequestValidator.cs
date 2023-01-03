using Aeveco.AzureFunction.Application.Models;
using FluentValidation;


namespace Aeveco.AzureFunction.Application.Validation
{
    public class MailQRRequestValidator : AbstractValidator<MailQRRequest>
    {
        public MailQRRequestValidator()
        {
            RuleFor(x => x.MailReceiver).NotEmpty().MaximumLength(200).EmailAddress();
            // not sure that I should be forcing some validation
            RuleFor(x => x.Subject).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.Encoding).NotEmpty().MaximumLength(200);
        }
    }
}
