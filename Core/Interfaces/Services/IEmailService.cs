using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Send the email to recipient, on the basis of passed parameters
        /// </summary>
        /// <param name="to">array of recipients refers to "to"</param>
        /// <param name="subject">Subject text to be appeared in E-Mail</param>
        /// <param name="message">Mail body to be appeared in E-Mail</param>
        /// <param name="isBodyHtml">true if you want to send HTML in mail boay,false if you want to send the text in mail body</param>
        /// <param name="attachments">not null if you want to send the attachment in E-Mail</param>
        /// <param name="bcc">Array of recipients for bcc</param>
        /// <param name="from">optional from address to which mail triggers</param>
        /// <param name="cc">Array of recipients for cc</param>
        /// <returns>
        /// true if E-Mail sent successfully else false
        /// </returns>
        string Send(
            string[] to,
            string subject,
            string message,
            bool isBodyHtml,
            string[] attachments,
            string[] bcc,
            string from = "",
            string[] cc = null);

    }
}
