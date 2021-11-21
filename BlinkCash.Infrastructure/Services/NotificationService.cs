using BlinkCash.Core.Interfaces.Services;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class NotificationService: INotificationService
    {
        private readonly IEmailService _emailService;
        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;

        }

        public bool SendEmail(string[] to, string subject, string message,
            bool isBodyHtml, string[] attachments, string[] bcc, string from = "", string[] cc = null)
        {
            BackgroundJob.Enqueue(() => _emailService.Send(to, subject, message, isBodyHtml
                , attachments, bcc, from, cc));

            return true;
        }

        //public bool SendSms(string phoneNumber, string smsText)
        //{
        //    BackgroundJob.Enqueue(x => x.SendSms(phoneNumber, smsText));
        //    return true;
        //}

    }
}
