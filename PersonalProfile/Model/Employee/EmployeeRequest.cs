using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Model.Employee
{
    public class EmployeeRequest
    {
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
