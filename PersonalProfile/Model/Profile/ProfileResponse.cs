using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Model.Profile
{
    public class ProfileResponse
    {
        public string Id { get; set; }
        public string IdType { get; set; }
        public string IdNo { get; set; }
        public bool IsActive { get; set; }
    }
}
