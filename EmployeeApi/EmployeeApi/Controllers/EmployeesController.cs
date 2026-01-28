using EmployeeApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/employees/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeId == id)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.Name,
                    e.Designation
                })
                .FirstOrDefaultAsync();

            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

        // GET api/employees/by-name/Vaibhav
        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetEmployeeIdByName(string name)
        {
            var employee = await _context.Employees
                .Where(e => e.Name == name)
                .Select(e => new
                {
                    e.EmployeeId
                })
                .FirstOrDefaultAsync();

            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

    }
}
