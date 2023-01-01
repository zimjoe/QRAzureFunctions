using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aeveco.AzureFunction.Application.Validation;

namespace Aeveco.AzureFunction.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Creates a <see cref="BadRequestObjectResult"/> containing a collection
        /// of minimal validation error details.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static BadRequestObjectResult ToBadRequest<T>(this ValidatableRequest<T> request)
        {
            if (request.Errors == null) {
                return new BadRequestObjectResult("Unable to Validate Body JSON or the Querystring");
            }
            return new BadRequestObjectResult(request.Errors.Select(e => new {
                Field = e.PropertyName,
                Error = e.ErrorMessage
            }));
        }
    }
}
