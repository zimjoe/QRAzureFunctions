using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.AzureFunction.Application.Models
{
    public class WifiQRRequest
    {
        public string? WifiName { get; set; }
        public string? Passcode { get; set; }
    }
}
