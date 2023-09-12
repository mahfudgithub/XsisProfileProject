using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalProfile.Interface;
using PersonalProfile.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalProfile.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiKeyPolicy")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfile _profile;

        public ProfileController(IProfile profile)
        {
            _profile = profile;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProfileById([FromRoute] string Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _profile.GetProfileById(Id);
                return Ok(result);
            }
            return BadRequest("Some Properties are not valid ");
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> CreateProfile([FromRoute] string Id, [FromBody] ProfileRequest profileRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _profile.CreateProfileAsync(Id, profileRequest);
                return Ok(result);
            }
            return BadRequest("Some Properties are not valid ");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] string Id, [FromBody] ProfileRequest profileRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _profile.UpdateProfileAsync(Id, profileRequest);
                return Ok(result);
            }
            return BadRequest("Some Properties are not valid ");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMovies([FromRoute] string Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _profile.DeleteProfileAsync(Id);
                return Ok(result);
            }
            return BadRequest("Some Properties are not valid ");
        }

    }
}
