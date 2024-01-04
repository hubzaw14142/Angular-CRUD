using CRUD.API.Data;
using CRUD.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly ApiDbContext _apiDbContext;
        public EmployeesController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _apiDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();

            await _apiDbContext.Employees.AddAsync(employee);
            await _apiDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _apiDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee employeeToUpdate)
        {
            var employee = await _apiDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = employeeToUpdate.Name;
            employee.Email = employeeToUpdate.Email;
            employee.Phone = employeeToUpdate.Phone;
            employee.Salary = employeeToUpdate.Salary;
            employee.Department = employeeToUpdate.Department;

            await _apiDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _apiDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _apiDbContext.Employees.Remove(employee);
            await _apiDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
