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
                form.Value.NickName,
                form.Value.Phone,
                form.Value.MobilePhone,
                form.Value.WorkPhone,
                form.Value.Email, 
                form.Value.Birthday, 
                form.Value.Website, 
                form.Value.Street, 
                form.Value.HouseNumber, 
                form.Value.City, 
                form.Value.ZipCode, 
                form.Value.Country, 
                form.Value.Note, 
                form.Value.StateRegion, 
                form.Value.AddressOrderType, 
                form.Value.Organization, 
                form.Value.Title
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
