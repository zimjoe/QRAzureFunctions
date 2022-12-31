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

namespace Aeveco.AzureFunction
{
    public static class UrlQR
    {
        [FunctionName("UrlQR")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("URL HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            // prefer the body or grab the name
            string? urlValue = data?.url ?? req.Query["url"];

            if (string.IsNullOrEmpty(urlValue))
            {
                return new BadRequestObjectResult("Please supply a url to encode");
            }


            PayloadGenerator.Url generator = new(urlValue);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);
            var qrCodeAsPng = qrCode.GetGraphic(20);


            return new FileContentResult(qrCodeAsPng, "image/png");
        }
    }
}
