using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace mail.Models
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            //Email = new string(Email);
            To = new List<EmailAddress>();
            Cc = new List<EmailAddress>();
            Bcc = new List<EmailAddress>();
        }
        [Required(ErrorMessage = "Niste unijeli Name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Niste unijeli Email!")]
        public string Email { get; set; }
        public List<EmailAddress> To { get; set; }
        public List<EmailAddress> Cc { get; set; }
        public List<EmailAddress> Bcc { get; set; }
        [Required(ErrorMessage = "Niste unijeli Subject!")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Niste unijeli Message!")]
        public string Message { get; set; }
    }
}
