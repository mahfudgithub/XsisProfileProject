using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalProfile.DataContext;
using PersonalProfile.Exceptions;
using PersonalProfile.Interface;
using PersonalProfile.Model;
using PersonalProfile.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Repository
{
    public class ProfileRepository : IProfile
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<WebResponse> CreateProfileAsync(string Id, ProfileRequest profileRequest)
        {
            WebResponse webResponse = new WebResponse();
            var profileResponse = new ProfileResponse();
            bool isValid = true;

            var user = await FindRegistrationUser(Id);

            if (user == null)
            {
                isValid = false;
                webResponse.status = false;
                webResponse.message = "There is no user with that Id";
                webResponse.data = null;
            }

            var searchProfile = await FindById(Id);
            if (isValid)
            {
                if (searchProfile != null)
                {
                    isValid = false;
                    webResponse.status = false;
                    webResponse.message = "user already exist";
                    webResponse.data = null;
                }
            }

            if (isValid)
            {
                Profile profile = new Profile()
                {
                    Id = Id,
                    IdType = profileRequest.IdType,
                    IdNo = profileRequest.IdNo,
                    IsActive = false,
                    CreatedBy = user.UserName,
                    CreatedAt = DateTime.Now
                };

                profileResponse.Id = profile.Id;
                profileResponse.IdType = profile.IdType;
                profileResponse.IdNo = profile.IdNo;
                profileResponse.IsActive = profile.IsActive;

                try
                {
                    _appDbContext.Profiles.Add(profile);
                    await _appDbContext.SaveChangesAsync();

                    webResponse.status = true;
                    webResponse.message = "Insert Data Successfully";
                    webResponse.data = profileResponse;
                }
                catch (Exception ex)
                {
                    throw new BadRequestException($"Something went wrong : {ex.Message}");
                }
            }

            return webResponse;
        }

        public async Task<WebResponse> DeleteProfileAsync(string Id)
        {
            WebResponse webResponse = new WebResponse();

            bool isValid = true;

            var user = await FindRegistrationUser(Id);

            if (user == null)
            {
                isValid = false;
                webResponse.status = false;
                webResponse.message = "There is no user with that Id";
                webResponse.data = null;
            }

            var searchProfile = await FindById(Id);
            if (isValid)
            {                
                if (searchProfile == null)
                {
                    isValid = false;
                    webResponse.status = false;
                    webResponse.message = "There is no have profile Id";
                    webResponse.data = null;
                }
            }

            if (isValid)
            {
                try
                {
                    _appDbContext.Profiles.Remove(searchProfile);
                    await _appDbContext.SaveChangesAsync();

                    webResponse.status = true;
                    webResponse.message = "Delete Data Successfully";
                    webResponse.data = null;
                }
                catch (Exception ex)
                {
                    throw new BadRequestException($"Something went wrong : {ex.Message}");
                }
            }

            return webResponse;
        }

        public async Task<WebResponse> GetProfileById(string Id)
        {
            WebResponse webResponse = new WebResponse();
            var profileResponse = new ProfileResponse();
            bool isValid = true;

            var user = await FindRegistrationUser(Id);

            if (user == null)
            {
                isValid = false;
                webResponse.status = false;
                webResponse.message = "There is no user with that Id";
                webResponse.data = null;
            }

            var profile = await FindById(Id);
            if (isValid)
            {
                if (profile == null)
                {
                    isValid = false;
                    webResponse.status = false;
                    webResponse.message = "No Data Found";
                    webResponse.data = null;
                }
            }

            if (isValid)
            {
                profileResponse.Id = profile.Id;
                profileResponse.IdType = profile.IdType;
                profileResponse.IdNo = profile.IdNo;
                profileResponse.IsActive = profile.IsActive;

                webResponse.status = true;
                webResponse.message = "Get Data Successfully";
                webResponse.data = profileResponse;
            }

            return webResponse;
        }


        public async Task<WebResponse> UpdateProfileAsync(string Id, ProfileRequest profileRequest)
        {
            WebResponse webResponse = new WebResponse();
            var profileResponse = new ProfileResponse();
            bool isValid = true;

            var user = await FindRegistrationUser(Id);

            if (user == null)
            {
                isValid = false;
                webResponse.status = false;
                webResponse.message = "There is no user with that Id";
                webResponse.data = null;
            }

            var searchProfile = await FindById(Id);
            if (isValid)
            {
                if (searchProfile == null)
                {
                    isValid = false;
                    webResponse.status = false;
                    webResponse.message = "No Data Found";
                    webResponse.data = null;
                }
            }

            if (isValid)
            {
                Profile profile = new Profile()
                {
                    Id = Id,
                    IdType = profileRequest.IdType,
                    IdNo = profileRequest.IdNo,
                    IsActive = false,
                    CreatedBy = searchProfile.CreatedBy,
                    CreatedAt = searchProfile.CreatedAt,
                    UpdatedAt = DateTime.Now
                };

                profileResponse.Id = profile.Id;
                profileResponse.IdType = profile.IdType;
                profileResponse.IdNo = profile.IdNo;
                profileResponse.IsActive = profile.IsActive;

                try
                {
                    _appDbContext.Profiles.Update(profile);
                    await _appDbContext.SaveChangesAsync();

                    webResponse.status = true;
                    webResponse.message = "Update Data Successfully";
                    webResponse.data = profileResponse;
                }
                catch (Exception ex)
                {
                    throw new BadRequestException($"Something went wrong : {ex.Message}");
                }
            }

            return webResponse;
        }

        public async Task<Profile> FindById(string Id)
        {
            var profile = new Profile();
            try
            {
                profile = await _appDbContext.Profiles.FindAsync(Id);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"something went wrong : {ex.Message}");
            }

            return profile;
        }

        public async Task<ApplicationUser> FindRegistrationUser(string Id)
        {
            var user = new ApplicationUser();
            try
            {
                user = await _userManager.FindByIdAsync(Id);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"something went wrong : {ex.Message}");
            }

            return user;
        }
    }
}
