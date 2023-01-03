using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.FunctionTests;
public class MailRequestValidatorTests
{
    readonly MailQRRequestValidator validator = new();

    [Theory]
    [InlineData(null, null, null, null)]
    [InlineData("", "", "", "")]
    [InlineData(" ", " ", " ", " ")]
    public void FieldsEmpty_ReturnsValidationErrors(string mailReceiver, string subject, string message, string encoding)
    {
        // Arrange
        MailQRRequest mailRequest = new()
        {
            MailReceiver = mailReceiver,
            Subject= subject,
            Message= message,
            Encoding = encoding
        };

        // Act
        var response = validator.TestValidate(mailRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.MailReceiver);
        response.ShouldHaveValidationErrorFor(x => x.Subject);
        response.ShouldHaveValidationErrorFor(x => x.Message);
        response.ShouldHaveValidationErrorFor(x => x.Encoding);
    }
}

