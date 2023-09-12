using PersonalProfile.Model;
using PersonalProfile.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Interface
{
    public interface IProfile
    {
        Task<WebResponse> GetProfileById(string Id);
        Task<WebResponse> CreateProfileAsync(string Id, ProfileRequest profileRequest);
        Task<WebResponse> UpdateProfileAsync(string Id, ProfileRequest profileRequest);
        Task<WebResponse> DeleteProfileAsync(string Id);
    }
}
