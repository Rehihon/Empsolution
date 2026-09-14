using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeData;
using EmployeeDomain;

namespace MyApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployeeList()
        {
            return await _context.Employees.ToListAsync();
        }

    [HttpGet("{EmployeeId}")]
        public async Task<ActionResult<Employee>> GetEmployeeRecord(int EmployeeId)
        {
            var employee = await _context.Employees.FindAsync(EmployeeId);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployeeRecord(Employee employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut("{EmployeeId}")]
        public async Task<IActionResult> UpdateEmployeeRecord(int EmployeeId,  Employee request)
        {
            var employee = await _context.Employees.FindAsync(EmployeeId);
            var _request = request;
            
            if (employee == null)
            {
                return NotFound();
            }
            employee.DepID = _request.DepID ?? employee.DepID;
            employee.JobID = _request.JobID ?? employee.JobID;
            employee.FirstName = _request.FirstName ?? employee.FirstName;
            employee.Surename = _request.Surename ?? employee.Surename;
            employee.DOB = _request.DOB ?? employee.DOB;
            employee.HireDate =_request.HireDate ?? employee.HireDate;
            await _context.SaveChangesAsync();
 
            return NoContent();
        }

        // [HttpPost({List<Employee>}) ]
        // public async Task<IActionResult> Create(List<Employee> employee)
        // {
        //     var entities= employee.Select (x=>new Employee
        //     { EmployeeId= x.EmployeeId,
        //         FirstName = x.FirstName,
        //         Surename   = x.Surename,
        //         HireDate =  x.HireDate,
        //         JobID    = x.JobID,
        //         CountactID=x.CountactID,
        //         DepID     = x.DepID
        //     }).ToList();
        //     _context.Employees.AddRange(entities);
        //     await _context.SaveChangesAsync();
        //     return Ok(employee);
        // }

        [HttpDelete("{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployeeRecord(int EmployeeId)
        {
            var employee = await _context.Employees.FindAsync(EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }   
}