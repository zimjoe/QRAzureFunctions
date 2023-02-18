using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction
{
    public static class StringExtensions
    {
        /// <summary>
        /// for some string, I want null not the possible whitespace or empty string
        /// </summary>
        public static string? NullIfEmpty(this string? s) {
            if (string.IsNullOrWhiteSpace(s)) {
                return default;
            }
            return s;
        }
    }
}
