using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface INotificationService
    {
        bool SendEmail(string[] to, string subject, string message,
          bool isBodyHtml, string[] attachments, string[] bcc, string from = "", string[] cc = null);

        //bool SendSms(string phoneNumber, string smsText);
    }
}
