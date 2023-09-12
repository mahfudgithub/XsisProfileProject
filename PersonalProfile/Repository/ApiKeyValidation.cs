using Microsoft.Extensions.Configuration;
using PersonalProfile.Interface;
using PersonalProfile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Repository
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidApiKey(string userApiKey)
        {
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;
            string? apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);
            if (apiKey == null || apiKey != userApiKey)
                return false;
            return true;
        }
    }
}
