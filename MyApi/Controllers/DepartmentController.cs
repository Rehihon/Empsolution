using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeData;
using EmployeeDomain;

namespace MyApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartmentLList()
        {
            return await _context.Department.ToListAsync();
        }

        [HttpGet("{DepID}")]
        public async Task<ActionResult<Department>> GetDepartmentRecord(int DepID)
        {
            var department = await _context.Department.FindAsync(DepID);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        [HttpPost]
        public async Task<IActionResult> InsertDertmentRecord(Department department)
        {
            _context.Department.Add(department);

            await _context.SaveChangesAsync();

            return Ok(department);
        }

        [HttpPut("{DepID}")]
        public async Task<IActionResult> UpdateDepartmentRecord(int depID, Department request )
        {
            var  Product = await _context.Department.FindAsync(depID);
            if (Product == null)
            {
                return NotFound();
            }
            Product.DepName = request.DepName ?? Product.DepName;
            Product.Description = request.Description ?? Product.Description;
             Product.MangerID = request.MangerID ?? Product.MangerID;
            await _context.SaveChangesAsync();
            var department = await _context.Department.FindAsync(depID);
            return Ok(Product);

        }

        [HttpDelete("{DepID}")]
        public async Task<IActionResult> DeleteDepartmentRecord(int depID)
        {
            var department = await _context.Department.FindAsync(depID);
            if (department == null)
            {
                return NotFound();
            }
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
    
}