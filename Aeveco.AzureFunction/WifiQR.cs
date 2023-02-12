using Aeveco.AzureFunction.Application.Models;
using Aeveco.AzureFunction.Application.Validation;
using Aeveco.AzureFunction.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QRCoder;
using System.IO;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction
{
    public static class WifiQR
    {
        [FunctionName("WifiQR")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // grab the request form
            var form = await req.GetJsonBody<WifiQRRequest, WifiQRRequestValidator>();

            if (!form.IsValid || form.Value == null)
            {
                log.LogInformation("Invalid form data.");
                return form.ToBadRequest();
            }

            PayloadGenerator.WiFi generator = new(form.Value.WifiName, form.Value.Passcode, PayloadGenerator.WiFi.Authentication.WPA);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);
            var qrCodeAsPng = qrCode.GetGraphic(20);

            return new FileContentResult(qrCodeAsPng, "image/png");
        }
    }
}
