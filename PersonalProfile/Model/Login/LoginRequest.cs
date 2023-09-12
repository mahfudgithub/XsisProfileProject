using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Model.Login
{
    public class LoginRequest
    {
        [Required, StringLength(100)]
        public string Username { get; set; }
        [Required, StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
