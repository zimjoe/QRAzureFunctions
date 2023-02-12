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

namespace Aeveco.AzureFunction
{
    public static class PlainTextQR
    {
        [FunctionName("PlainTextQR")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // grab the request form
            var form = await req.GetJsonBody<PlainTextQRRequest, PlainTextQRRequestValidator>();

            if (!form.IsValid || form.Value == null)
            {
                log.LogInformation("Invalid form data.");
                return form.ToBadRequest();
            }
          
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(form.Value.Text, QRCodeGenerator.ECCLevel.Q);

            PngByteQRCode qrCode = new(qrCodeData);
            var qrCodeAsPng = qrCode.GetGraphic(20);

            return new FileContentResult(qrCodeAsPng, "image/png");
        }
    }
}
