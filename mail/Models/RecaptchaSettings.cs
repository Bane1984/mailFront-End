using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mail.Models
{
    public class RecaptchaSettings
    {
        public string SecretKey { get; set; }
        public string SiteKey { get; set; }
    }
}
