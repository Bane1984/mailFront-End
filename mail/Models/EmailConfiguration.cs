﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mail.Interfaces;

namespace mail.Models
{
    public class EmailConfiguration:IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
