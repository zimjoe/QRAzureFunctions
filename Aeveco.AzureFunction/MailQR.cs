using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QRCoder;
using Aeveco.AzureFunction.Extensions;
using Aeveco.AzureFunction.Application.Models;
using Aeveco.AzureFunction.Application.Validation;

namespace Aeveco.AzureFunction
{
    public static class MailQR
    {
        [FunctionName("MailQR")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // grab the request form
            var form = await req.GetJsonBody<MailQRRequest, MailQRRequestValidator>();

            if (!form.IsValid || form.Value == null)
            {
                log.LogInformation("Invalid form data.");
                return form.ToBadRequest();
            }
            // this is a little hacky, but not sending the empty strings caused a generation error.  Didn't want to "require" fields
            PayloadGenerator.Mail generator = new(form.Value.MailReceiver, form.Value.Subject ?? "", form.Value.Message ?? "", MailQRRequest.GetEncoding(form.Value.Encoding));

            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);
            var qrCodeAsPng = qrCode.GetGraphic(20);

            return new FileContentResult(qrCodeAsPng, "image/png");
        }
    }
}
