using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Model.Login
{
    public class LoginResponse
    {
        public string Uid { get; set; }
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
}
