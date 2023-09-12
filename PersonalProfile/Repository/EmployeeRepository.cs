using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PersonalProfile.DataContext;
using PersonalProfile.Exceptions;
using PersonalProfile.Interface;
using PersonalProfile.Model;
using PersonalProfile.Model.Employee;
using PersonalProfile.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<WebResponse> Login(LoginRequest loginRequest)
        {
            WebResponse webResponse = new WebResponse();
            var response = new LoginResponse();
            ApplicationUser applicationUser = new ApplicationUser();

            if (loginRequest.Username.IndexOf('@') > -1)
            {
                applicationUser = await ValidateEmail(loginRequest.Username);
                if (applicationUser == null)
                {
                    throw new NotFoundException("There is no user with that Email address " + $" {loginRequest.Username}");
                }
            }
            else
            {
                applicationUser = await ValidateUserName(loginRequest.Username);
                if (applicationUser == null)
                {
                    throw new NotFoundException("There is no user with that Username " + $" {loginRequest.Username}");
                }
            }

            var result = await _signInManager.PasswordSignInAsync(applicationUser.UserName, loginRequest.Password, false, false);

            if (!result.Succeeded)
            {
                throw new InvalidException("Invalid Password.");
            }

            response.Uid = applicationUser.Id;
            response.Username = applicationUser.UserName;
            response.ApiKey = _configuration["ApiKey"];

            webResponse.status = true;
            webResponse.message = "Login Successfully";
            webResponse.data = response;

            return webResponse;
        }

        public async Task<WebResponse> Register(EmployeeRequest employeeRequest)
        {
            WebResponse webResponse = new WebResponse();
            var data = new EmployeeResponse();

            var user = new ApplicationUser()
            {
                Name = employeeRequest.Name,
                Email = employeeRequest.Email,
                UserName = employeeRequest.UserName
            };

            if (!employeeRequest.Password.Equals(employeeRequest.ConfirmPassword))
            {
                throw new InvalidException("Password and Confirm Password do not match.");
            }

            var existEmail = await ValidateEmail(employeeRequest.Email);
            if (existEmail != null)
            {
                throw new InvalidException("Email already exist");
            }

            var existUsername = await ValidateUserName(employeeRequest.UserName);
            if (existUsername != null)
            {
                throw new InvalidException("Username already exist");
            }

            var result = await _userManager.CreateAsync(user, employeeRequest.Password);

            if (result.Succeeded)
            {
                data.Name = employeeRequest.Name;
                data.Username = employeeRequest.Email;
                data.Email = employeeRequest.Email;
                webResponse.status = true;
                webResponse.message = "Register Successfully";
                webResponse.data = data;
            }
            else
            {
                throw new BadRequestException(String.Join(", ", result.Errors.Select(x => x.Description)));
            }

            return webResponse;
        }

        public async Task<ApplicationUser> ValidateEmail(string email)
        {
            var user = new ApplicationUser();
            try
            {
                user = await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"something went wrong : {ex.Message}");
            }

            return user;
        }

        public async Task<ApplicationUser> ValidateUserName(string username)
        {
            var user = new ApplicationUser();
            try
            {
                user = await _userManager.FindByNameAsync(username);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"something went wrong : {ex.Message}");
            }

            return user;
        }
    }
}
