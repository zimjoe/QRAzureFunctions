using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction.Application.Models
{
    public class MailQRRequest
    {
        public string? MailReceiver { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? Encoding { get; set; }
    }
}
