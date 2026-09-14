using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeData;
using EmployeeDomain;

namespace MyApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Jobs>> GetJobList()
        {
            return await _context.Jobs.ToListAsync();
        }

 [HttpGet("{JobID}")]
public async Task<ActionResult<Jobs>> GetJobRecord(int JobID)
{
    var  job = await _context.Jobs.FindAsync(JobID);

    if (job == null)
    {
        return NotFound();
    }

    return job;
}

        [HttpPost]
        public async Task<IActionResult> InsertJobRecord(Jobs job)
        {
            _context.Jobs.Add(job);

            await _context.SaveChangesAsync();

            return Ok(job);
        }

        [HttpPut("{JobID}")]
        public async Task<ActionResult<Jobs>> updateJobRecord(int JobID, Jobs request)
        {
            var job = await _context.Jobs.FindAsync(JobID);

            if (job == null)
            {
                return NotFound();
            }
            job.Job_Title = request.Job_Title ?? job.Job_Title;
            if (request.Min_salary != 0)  {job.Min_salary = job.Min_salary;  }
            if (request.Max_Salary != 0) { job.Max_Salary = job.Min_salary; }

            await _context.SaveChangesAsync();
           
            return Ok(job);
        }


        [HttpDelete("{JobID}")] 
        public async Task<IActionResult> DeleteJoRecord(int JobID)
        {
            var job = await _context.Jobs.FindAsync(JobID);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
    

 