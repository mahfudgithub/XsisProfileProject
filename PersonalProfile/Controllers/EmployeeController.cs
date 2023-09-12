using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProfile.Interface;
using PersonalProfile.Model.Employee;
using PersonalProfile.Model.Login;
using System.Threading.Tasks;

namespace PersonalProfile.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]    
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] EmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _employee.Register(employeeRequest);
                return StatusCode(StatusCodes.Status201Created, result);
            }

            return BadRequest("Some Properties are not valid ");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _employee.Login(loginRequest);
                return Ok(result);
            }

            return BadRequest("Some Properties are not valid ");
        }
    }
}
