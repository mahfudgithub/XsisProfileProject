using Microsoft.AspNetCore.Identity;
using PersonalProfile.Model.Profile;
using System.Collections.Generic;

namespace PersonalProfile.DataContext
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<Profile> Profiles { get; set; }
    }
}
