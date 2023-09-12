using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Model.Profile
{
    public class ProfileRequest
    {
        [StringLength(2)]
        public string IdType { get; set; }
        [StringLength(100)]
        public string IdNo { get; set; }
    }
}
