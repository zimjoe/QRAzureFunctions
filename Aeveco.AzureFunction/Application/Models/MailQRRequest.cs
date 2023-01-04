using QRCoder;
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
        public int? Encoding { get; set; }

        public static PayloadGenerator.Mail.MailEncoding GetEncoding(int? encoding) {
            if (encoding.HasValue) {
                return encoding.Value switch
                {
                    (int)PayloadGenerator.Mail.MailEncoding.MAILTO => PayloadGenerator.Mail.MailEncoding.MAILTO,
                    (int)PayloadGenerator.Mail.MailEncoding.MATMSG => PayloadGenerator.Mail.MailEncoding.MATMSG,
                    (int)PayloadGenerator.Mail.MailEncoding.SMTP => PayloadGenerator.Mail.MailEncoding.SMTP,
                    _ => PayloadGenerator.Mail.MailEncoding.MAILTO,
                };
            }
            return PayloadGenerator.Mail.MailEncoding.MAILTO;
        }
    }
}
