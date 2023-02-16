using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.FunctionTests
{
    public class PlainTextQRRequestValidatorTests
    {
        readonly PlainTextQRRequestValidator validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void FieldsEmpty_ReturnsValidationErrors(string text)
        {
            // Arrange
            PlainTextQRRequest textRequest = new()
            {
                Text = text
            };

            // Act
            var response = validator.TestValidate(textRequest);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.Text);
        }

        [Fact]
        public void TextIsUnder2Chars_ReturnsValidationErrors()
        {
            // Arrange
            PlainTextQRRequest textRequest = new()
            {
                Text = new string('a', 1)
            };

            // Act
            var response = validator.TestValidate(textRequest);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.Text);
        }

        [Fact]
        public void TextIsOver500Chars_ReturnsValidationErrors()
        {
            // Arrange
            string val = new string('a', 501);
            PlainTextQRRequest textRequest = new()
            {
                Text = val
            };

            // Act
            var response = validator.TestValidate(textRequest);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.Text);
        }
    }
}
