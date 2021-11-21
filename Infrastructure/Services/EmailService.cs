// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailInteraction.cs" company="Bankly.ng">
//   Copyright (c) Bankly.ng. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace BlinkCash.Infrastructure.Services
{
    using BlinkCash.Core.Interfaces.Services;
    using BlinkCash.Core.Models;
    using System.Collections.Generic;

    public class EmailService : IEmailService
    {
        private string from = "noreply@builditdigital.co";
        private readonly IEmailClient emailClient;
        private readonly EmailModel emailModel;

         //<add key = "fromEmail" value="payments@bankly.ng"/>

      /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class. 
        /// </summary>
        /// <param name="emailClient">Email client will be resposoncible for sending email</param>
        public EmailService(IEmailClient emailClient)
        {
            this.emailClient = emailClient;
            this.emailModel = new EmailModel();
        }

        /// <summary>
        /// Send the email to recipient, on the basis of passed parameters
        /// </summary>
        /// <param name="to">Array of recipients refers to "to"</param>
        /// <param name="subject">Subject text to be appeared in E-Mail</param>
        /// <param name="message">Mail body to be appeared in E-Mail</param>
        /// <param name="isBodyHtml">true if you want to send HTML in mail boay,false if you want to send the text in mail body</param>
        /// <param name="attachments">not null if you want to send the attachment in E-Mail</param>
        /// <param name="bcc">Array of recipients for bcc.</param>
        /// <param name="mailFrom">from address to which mail should trigger</param>
        /// <param name="cc">The cc.</param>
        /// <returns>
        /// true if E-Mail sent successfully else false
        /// </returns>
        public string Send(
            string[] to,
            string subject,
            string message,
            bool isBodyHtml,
            string[] attachments,
            string[] bcc,
            string mailFrom = "",
            string[] cc=null)
        {
            if (!string.IsNullOrWhiteSpace(mailFrom))
            {
                this.from = mailFrom;
            }

            this.emailModel.To = new List<string>();

            foreach (var recipient in to)
            {
                this.emailModel.To.Add(recipient);
            }

            if (bcc != null)
            {
                this.emailModel.Bcc = new List<string>();
                foreach (var recipient in bcc)
                {
                    if (recipient != null)
                    {
                        this.emailModel.Bcc.Add(recipient);
                    }
                }
            }

            if (cc != null)
            {
                this.emailModel.Cc = new List<string>();
                foreach (var recipient in cc)
                {
                    if (recipient != null)
                    {
                        this.emailModel.Cc.Add(recipient);
                    }
                }
            }

            this.emailModel.Subject = subject;
            this.emailModel.Body = message;
            this.emailModel.From = this.from;
            this.emailModel.IsBodyHtml = isBodyHtml;

            if (attachments?.Length > 0)
            {
                this.emailModel.HtmlAttachment = attachments[0];
                this.emailModel.FileName = "";

                    // TODO: setting up here due to time constraint. will do properly later
            }

            var validationError = this.emailClient.Validate(this.emailModel);
            if (!string.IsNullOrEmpty(validationError))
            {
                return validationError;
            }

            return this.emailClient.Send(this.emailModel);
        }
    }
}