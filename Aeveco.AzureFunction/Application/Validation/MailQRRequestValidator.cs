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
            RuleFor(x => x.Subject).MaximumLength(100);
            RuleFor(x => x.Message).MaximumLength(2000);
            RuleFor(x => x.Encoding)
                .GreaterThanOrEqualTo(0).WithMessage("Use 0:MAILTO, 1:MATMSG, or 2:SMTP depending on reader application.")
                .LessThanOrEqualTo(2).WithMessage("Use 0:MAILTO, 1:MATMSG, or 2:SMTP depending on reader application.");

        }
    }
}
