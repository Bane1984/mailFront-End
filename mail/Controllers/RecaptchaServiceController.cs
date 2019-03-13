//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using mail.Interfaces;
//using mail.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using reCAPTCHA.AspNetCore;
//using RecaptchaSettings = reCAPTCHA.AspNetCore.RecaptchaSettings;
//using IRecaptchaService = mail.Interfaces.IRecaptchaService;

//namespace mail.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RecaptchaServiceController : IRecaptchaService
//    {
//        public static HttpClient Client { get; set; }
//        public readonly RecaptchaSettings RecaptchaSettings;
//        private IRecaptchaService _recaptcha;

//        public RecaptchaServiceController(IRecaptchaService recaptcha)
//        {
//            _recaptcha = recaptcha;
//        }
//        public RecaptchaServiceController(IOptions<RecaptchaSettings> options)
//        {
//            RecaptchaSettings = options.Value;

//            if (Client == null)
//                Client = new HttpClient();
//        }

//        public RecaptchaServiceController(IOptions<RecaptchaSettings> options, HttpClient client)
//        {
//            RecaptchaSettings = options.Value;
//            Client = client;
//        }

//        /// <summary>
//        /// ReCaptcha Validation.
//        /// </summary>
//        /// <param name="request"></param>
//        /// <param name="antiForgery"></param>
//        /// <returns></returns>
//        [HttpPost("validate")]
//        public async Task<bool> Validate(string gRecaptchaResponse, string secret)
//        {
//            HttpClient httpClient = new HttpClient();
//            var content = new FormUrlEncodedContent(new[]
//            {
//                new KeyValuePair<string, string>("SecretKey", secret), 
//                new KeyValuePair<string, string>("response", gRecaptchaResponse)
//            });
//            var res = await httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify", content);
//            if (res.StatusCode != HttpStatusCode.OK)
//            {
//                return false;
//            }

//            string JSONres = res.Content.ReadAsStringAsync().Result;
//            dynamic JSONdata = JObject.Parse(JSONres);

//            if (JSONdata.success != "true")
//            {
//                return false;
//            }

//            return true;
//        }
//    }
//}