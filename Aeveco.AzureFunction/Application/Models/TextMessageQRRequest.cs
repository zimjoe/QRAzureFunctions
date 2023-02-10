using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction.Application.Models
{
    public class TextMessageQRRequest
    {
        public string? ToNumber { get; set; }
        public string? Message { get; set; }
    }
}
