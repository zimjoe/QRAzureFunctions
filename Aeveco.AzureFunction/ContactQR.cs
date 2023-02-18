using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Aeveco.AzureFunction.Application.Models;
using Aeveco.AzureFunction.Application.Validation;
using Aeveco.AzureFunction.Extensions;
using QRCoder;
using static QRCoder.PayloadGenerator.ContactData;

namespace Aeveco.AzureFunction
{
    public static class ContactQR
    {
        [FunctionName("ContactQR")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // grab the request form
            var form = await req.GetJsonBody<ContactQRRequest, ContactQRRequestValidator>();

            if (!form.IsValid || form.Value == null)
            {
                log.LogInformation("Invalid form data.");
                return form.ToBadRequest();
            }

            PayloadGenerator.ContactData generator = new(
                form.Value.ContactOutput,
                form.Value.FirstName,
                form.Value.LastName,
                form.Value.NickName.NullIfEmpty(),
                form.Value.Phone.NullIfEmpty(),
                form.Value.MobilePhone.NullIfEmpty(),
                form.Value.WorkPhone.NullIfEmpty(),
                form.Value.Email.NullIfEmpty(),
                form.Value.Birthday,
                form.Value.Website.NullIfEmpty(),
                form.Value.Street.NullIfEmpty(),
                form.Value.HouseNumber.NullIfEmpty(),
                form.Value.City.NullIfEmpty(),
                form.Value.ZipCode.NullIfEmpty(),
                form.Value.Country.NullIfEmpty(),
                form.Value.Note.NullIfEmpty(),
                form.Value.StateRegion.NullIfEmpty(),
                form.Value.AddressOrderType,
                form.Value.Organization.NullIfEmpty(),
                form.Value.Title.NullIfEmpty()
            );
            
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);
            var qrCodeAsPng = qrCode.GetGraphic(20);

            return new FileContentResult(qrCodeAsPng, "image/png");
        }
    }
}
