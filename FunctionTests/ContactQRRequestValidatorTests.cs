using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;
using static QRCoder.PayloadGenerator.ContactData;

namespace Aeveco.FunctionTests;
public class ContactQRRequestValidatorTests
{
    readonly ContactQRRequestValidator validator = new();

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public void FieldsEmpty_ReturnsValidationErrors(string firstName, string lastName)
    {
        // Arrange
        ContactQRRequest request = new()
        {
            FirstName = firstName,
            LastName = lastName
        };

        // Act
        var response = validator.TestValidate(request);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.FirstName);
        response.ShouldHaveValidationErrorFor(x => x.LastName);
        response.ShouldNotHaveValidationErrorFor(x => x.ContactOutput);
        response.ShouldNotHaveValidationErrorFor(x => x.AddressOrderType);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(4, 2)]
    [InlineData(6, 6)]
    public void OutOfRange_ReturnsValidationErrors(int contactOutput, int addressOrderType)
    {
        // Arrange
        ContactQRRequest request = new()
        {
            ContactOutput = (ContactOutputType)contactOutput,
            AddressOrderType = (AddressOrder)addressOrderType
        };

        // Act
        var response = validator.TestValidate(request);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.ContactOutput);
        response.ShouldHaveValidationErrorFor(x => x.AddressOrderType);
    }
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    public void InRange_ReturnsValidationErrors(int contactOutput, int addressOrderType)
    {
        // Arrange
        ContactQRRequest request = new()
        {
            ContactOutput = (ContactOutputType)contactOutput,
            AddressOrderType = (AddressOrder)addressOrderType
        };

        // Act
        var response = validator.TestValidate(request);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.ContactOutput);
        response.ShouldNotHaveValidationErrorFor(x => x.AddressOrderType);
    }
}

