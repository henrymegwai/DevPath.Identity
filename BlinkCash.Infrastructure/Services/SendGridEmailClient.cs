using BlinkCash.Core.Configs;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class SendGridEmailClient : IEmailClient
    {
        private readonly SendGridClient smtpClient;
        private readonly AppSettings _appSettings;
        public SendGridEmailClient(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            smtpClient = new SendGridClient(_appSettings.SendGridApiKey);            
        }



        public string Send(EmailModel emailModel)
        {
            try
            {
                var mail = new SendGridMessage();
                var to = new List<EmailAddress>();

                mail.From = new EmailAddress(emailModel.From, "Blink Cash");
                foreach (var recipient in emailModel.To)
                {
                    mail.AddTo(recipient);
                    to.Add(new EmailAddress(recipient));

                }

                if (emailModel.Cc != null)
                {

                    foreach (var recipient in emailModel.Cc)
                    {
                        mail.AddCc(new EmailAddress(recipient));
                    }
                }

                if (emailModel.Bcc != null)
                {
                    foreach (var recipient in emailModel.Bcc)
                    {
                        mail.AddBcc(new EmailAddress(recipient));
                    }
                }

                mail.Subject = emailModel.Subject;


                if (emailModel.IsBodyHtml)
                {
                    mail.HtmlContent = emailModel.Body;
                }
                else
                {
                    mail.PlainTextContent = emailModel.Body;
                }
                if (!string.IsNullOrEmpty(emailModel.HtmlAttachment))
                {
                    var byteArray = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(emailModel.HtmlAttachment);
                    var memoryStream = new System.IO.MemoryStream(byteArray);
                    //var attachment = new Attachment(memoryStream, emailModel.FileName);
                    var attachment = new Attachment();
                    attachment.Filename = emailModel.FileName;
                    attachment.Content = memoryStream.ToString();
                    mail.Attachments.Add(attachment);
                }

                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(mail.From, to, mail.Subject, mail.PlainTextContent, mail.HtmlContent);
                // var a = this.smtpClient.SendEmailAsync(mail).Result;
                this.smtpClient.SendEmailAsync(msg);
                // mail.Dispose();
            }
            catch (Exception exp)
            {
                return exp.Message;
            }

            return string.Empty;
        }

        public string Validate(EmailModel emailModel)
        {
            RequiredAttribute requiredAttribute = new RequiredAttribute();
            EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();
            if (!requiredAttribute.IsValid(emailModel.From))
            {

            }

            if (!emailAddressAttribute.IsValid(emailModel.From.Trim()))
            {
            }

            if (emailModel.To == null || emailModel.To.Count == 0)
            {
            }

            foreach (var to in emailModel.To)
            {
                if (!string.IsNullOrEmpty(to) && !requiredAttribute.IsValid(to))
                {
                }
                if (!string.IsNullOrEmpty(to))
                {
                    if (!emailAddressAttribute.IsValid(to.Trim()))
                    {
                    }
                }

            }

            if (emailModel.Cc != null && emailModel.Cc.Count > 0)
            {
                foreach (var cc in emailModel.Cc)
                {
                    if (cc == null || !emailAddressAttribute.IsValid(cc))
                    {
                    }
                }
            }

            if (emailModel.Bcc != null && emailModel.Bcc.Count > 0)
            {
                foreach (var bcc in emailModel.Bcc)
                {
                    if (bcc == null || !emailAddressAttribute.IsValid(bcc))
                    {
                    }
                }
            }

            if (!requiredAttribute.IsValid(emailModel.Subject))
            {
            }

            if (!requiredAttribute.IsValid(emailModel.Body))
            {
            }

            return string.Empty;
        }
    }
}
