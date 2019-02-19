﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mail.Models;
using Microsoft.IdentityModel.Tokens;
using mail.Models;

namespace mail.Interfaces
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
