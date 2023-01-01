using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Aeveco.AzureFunction.Application.Validation;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Web;
using System.Linq;

namespace Aeveco.AzureFunction.Extensions
{
    /// <summary>
    /// Totally swiped from theis great article
    /// https://www.tomfaltesek.com/azure-functions-input-validation/
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Returns the deserialized request body with validation information.
        /// </summary>
        /// <typeparam name="T">Type used for deserialization of the request body.</typeparam>
        /// <typeparam name="V">
        /// Validator used to validate the deserialized request body.
        /// </typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<ValidatableRequest<T>> GetJsonBody<T, V>(this HttpRequest request)
            where V : AbstractValidator<T>, new()
        {
            var requestObject = await request.GetJsonBody<T>();

            // if the requestObject is null
            if (requestObject == null) {
                return new ValidatableRequest<T>
                {
                    Value = requestObject,
                    IsValid = false
                };
            }

            var validator = new V();
            var validationResult = validator.Validate(requestObject);

            if (!validationResult.IsValid)
            {
                return new ValidatableRequest<T>
                {
                    Value = requestObject,
                    IsValid = false,
                    Errors = validationResult.Errors
                };
            }

            return new ValidatableRequest<T>
            {
                Value = requestObject,
                IsValid = true
            };
        }

        /// <summary>
        /// Returns the deserialized request body.
        /// </summary>
        /// <typeparam name="T">Type used for deserialization of the request body.</typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<T?> GetJsonBody<T>(this HttpRequest request)
        {
            var requestBody = await request.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(requestBody)) {
                // check if the querystring can be used
                if (request.QueryString.HasValue) {
                    // convert to named value collection
                    var nvc = HttpUtility.ParseQueryString(request.QueryString.Value);

                    // convert to string that looks like json
                    var queryStringObj = JsonConvert.SerializeObject(nvc.AllKeys.ToDictionary(k => k??"", k => nvc[k]));
                    if (!string.IsNullOrWhiteSpace(queryStringObj)) {
                        // throw it at the deserializer and see what matches
                        return JsonConvert.DeserializeObject<T>(queryStringObj);
                    }
                }
                // basically return null
                return default;
            }

            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }
}
