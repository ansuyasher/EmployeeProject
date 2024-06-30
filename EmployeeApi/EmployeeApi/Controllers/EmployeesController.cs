using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllEmployees()
        {
            var employees = await _context.Employees.Include(e => e.Person).ToListAsync();
            var supervisors = await _context.Supervisors.Include(s => s.Person).ToListAsync();
            var managers = await _context.Managers.Include(m => m.Person).ToListAsync();

            var allEmployees = employees.Select(e => new
            {
                Type = "Employee",
                e.EmployeeID,
                e.Person.FirstName,
                e.Person.LastName,
                e.Person.Address1,
                e.PayPerHour
            })
            .Union(supervisors.Select(s => new
            {
                Type = "Supervisor",
                s.SupervisorID,
                FirstName = s.Person?.FirstName,
                LastName = s.Person?.LastName,
                Address1 = s.Person?.Address1,
                AnnualSalary = s.AnnualSalary
            }))
            .Union(managers.Select(m => new
            {
                Type = "Manager",
                m.ManagerID,
                FirstName = m.Person?.FirstName,
                LastName = m.Person?.LastName,
                Address1 = m.Person?.Address1,
                AnnualSalary = m.AnnualSalary,
                m.MaxExpenseAmount
            }));

            return Ok(allEmployees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAllEmployees), new { id = employee.EmployeeID }, employee);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
