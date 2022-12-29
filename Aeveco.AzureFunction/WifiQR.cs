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
using static QRCoder.QRCodeGenerator;
using static QRCoder.PngByteQRCode;
namespace Aeveco.AzureFunction
{
    public static class WifiQR
    {
        [FunctionName("WifiQR")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            // prefer the body or grab the name
            string? wifiName = data?.name ?? req.Query["wifiname"];
            string? passcode = data?.name ?? req.Query["passcode"];

            if (string.IsNullOrEmpty(wifiName) || string.IsNullOrEmpty(passcode))
            {
                return new BadRequestObjectResult("Please supply a wifiname and a passcode");
            }


            PayloadGenerator.WiFi generator = new(wifiName, passcode, PayloadGenerator.WiFi.Authentication.WPA);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);
            var qrCodeAsPng = qrCode.GetGraphic(20);


            return new FileContentResult(qrCodeAsPng, "image/png");
           
        }
    }
}
