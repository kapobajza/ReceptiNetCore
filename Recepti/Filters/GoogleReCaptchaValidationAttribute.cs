using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Recepti.Filters
{
    public class GoogleReCaptchaValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = value?.ToString();
            var errorMsg = ErrorMessage ?? "Validacija nije izvršena";

            if (value == null || string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult(errorMsg);
            }

            var configuration = (IConfiguration)validationContext.GetService(typeof(IConfiguration));
            var reCaptchaSecret = configuration.GetValue<string>("GoogleReCaptcha:SecretKey");

            var httpClient = new HttpClient();

            var httpResponse = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={stringValue}").Result;

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ValidationResult(errorMsg);
            }

            string jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
            dynamic jsonData = JObject.Parse(jsonResponse);

            if (jsonData.success != true.ToString().ToLower())
            {
                return new ValidationResult(errorMsg);
            }

            return ValidationResult.Success;
        }
    }
}
