using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mail.Models
{
    public class RecaptchaResponse
    {
        public bool Success { get; set; }
        public decimal Score { get; set; }
        public string Action { get; set; }
        public DateTime ChallengeTs { get; set; }
        public string HostName { get; set; }
    }
}
