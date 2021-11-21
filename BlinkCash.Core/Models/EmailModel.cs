using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class EmailModel
    {
        public string From { get; set; }

        public IList<string> To { get; set; }

        public IList<string> Cc { get; set; }

        public IList<string> Bcc { get; set; }

        public IList<object> Attachments { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }

        public bool IsBodyHtml { get; set; }

        public string HtmlAttachment { get; set; }

        public string FileName { get; set; }
    }
}
