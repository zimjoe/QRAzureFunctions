using Aeveco.AzureFunction.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.FunctionTests
{
    public class TextMessageQRRequestValidatorTests
    {
        readonly TextMessageQRRequestValidator validator = new();

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        public void FieldsEmpty_ReturnsValidationErrors(string wifiName, string passcode)
        {
            // Arrange
            TextMessageQRRequest request = new()
            {
                ToNumber = wifiName,
                Message = passcode
            };

            // Act
            var response = validator.TestValidate(request);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.ToNumber);
            response.ShouldNotHaveValidationErrorFor(x => x.Message);
        }
        [Fact]
        public void ToNumberUnder2Chars_ReturnsValidationErrors()
        {
            // Arrange
            TextMessageQRRequest request = new()
            {
                ToNumber = new string('a', 1)
            };
            // Act
            var response = validator.TestValidate(request);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.ToNumber);
        }
        [Fact]
        public void ToNumberOver25Chars_ReturnsValidationErrors()
        {
            // Arrange
            TextMessageQRRequest request = new()
            {
                ToNumber = new string('a', 26)
            };
            // Act
            var response = validator.TestValidate(request);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.ToNumber);
        }
        [Fact]
        public void MessageOver144Chars_ReturnsValidationErrors()
        {
            // Arrange
            TextMessageQRRequest request = new()
            {
                ToNumber = new string('a', 12),
                Message = new string('a', 145)
            };
            // Act
            var response = validator.TestValidate(request);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.Message);
        }
    }
}
