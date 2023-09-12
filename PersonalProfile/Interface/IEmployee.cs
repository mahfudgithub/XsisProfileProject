using PersonalProfile.Model;
using PersonalProfile.Model.Employee;
using PersonalProfile.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Interface
{
    public interface IEmployee
    {
        Task<WebResponse> Register(EmployeeRequest employeeRequest);
        Task<WebResponse> Login(LoginRequest loginRequest);
    }
}
