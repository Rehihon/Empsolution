
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeData;
using EmployeeDomain;

namespace MyApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JobhistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobhistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Jobhistory>> GetListItem()
        {
            return await _context.Jobhistory.ToListAsync();
        }

    [HttpGet("{Id}")]
        public async Task<ActionResult<Jobhistory>> GetListItem(int Id)
        {
            var jobhistory = await _context.Jobhistory.FindAsync(Id);

            if (jobhistory == null)
            {
                return NotFound();
            }

            return jobhistory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Jobhistory jobhistory)
        {
            _context.Jobhistory.Add(jobhistory);

            await _context.SaveChangesAsync();

            return Ok(jobhistory);
        }
        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var jobhistory = await _context.Jobhistory.FindAsync(Id);
            if (jobhistory == null)
            {
                return NotFound();
            }

            _context.Jobhistory.Remove(jobhistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }   
}