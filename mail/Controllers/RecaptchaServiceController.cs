using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using mail.Interfaces;
using mail.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using reCAPTCHA.AspNetCore;
using RecaptchaSettings = reCAPTCHA.AspNetCore.RecaptchaSettings;
using IRecaptchaService = mail.Interfaces.IRecaptchaService;

namespace mail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecaptchaServiceController : IRecaptchaService
    {
        public static HttpClient Client { get; set; }
        public readonly RecaptchaSettings RecaptchaSettings;
        private IRecaptchaService _recaptcha;

        public RecaptchaServiceController(IRecaptchaService recaptcha)
        {
            _recaptcha = recaptcha;
        }
        public RecaptchaServiceController(IOptions<RecaptchaSettings> options)
        {
            RecaptchaSettings = options.Value;

            if (Client == null)
                Client = new HttpClient();
        }

        public RecaptchaServiceController(IOptions<RecaptchaSettings> options, HttpClient client)
        {
            RecaptchaSettings = options.Value;
            Client = client;
        }

        /// <summary>
        /// ReCaptcha Validation.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="antiForgery"></param>
        /// <returns></returns>
        [HttpPost("validate")]
        public async Task<RecaptchaResponse> Validate(HttpRequest request, bool antiForgery = true)
        {
            if (!request.Form.ContainsKey("g-recaptcha-response")) // error if no reason to do anything, this is to alert developers they are calling it without reason.
                throw new ValidationException("Google recaptcha odgovor nije pronadjen u formi.");

            var response = request.Form["g-recaptcha-response"];
            var result = await Client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={RecaptchaSettings.SecretKey}&response={response}");
            var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(result);

            if (captchaResponse.success && antiForgery)
                if (captchaResponse.hostname?.ToLower() != request.Host.Host?.ToLower())
                    throw new ValidationException("Recaptcha host, and request host do not match. Forgery attempt?");

            return captchaResponse;
        }
    }
}