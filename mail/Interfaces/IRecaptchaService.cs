using Microsoft.AspNetCore.Http;
using reCAPTCHA.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mail.Interfaces
{
    public interface IRecaptchaService
    {
        Task<RecaptchaResponse> Validate(HttpRequest request, bool antiForgery = true);
    }
}
