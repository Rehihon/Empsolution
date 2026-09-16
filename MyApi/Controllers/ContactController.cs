using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeData;
using EmployeeDomain;
using System.Diagnostics.Contracts;

namespace MyApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetContactList()
        {
            return await _context.Contact.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Contact>> GetContactRecord(int Id)
        {
            var contact = await _context.Contact.FindAsync(Id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }


        [HttpPost]
        public async Task<IActionResult> InsertContactRecord(Contact contact)
        {
            _context.Contact.Add(contact);

            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContactRecord( Contact request)
        {
            var Id = request.ContactId;
            var contact = await _context.Contact.FindAsync(Id);
            if (contact == null)
                return NotFound();
            contact.Email = request.Email ?? contact.Email;
            contact.Description = request.Description ?? contact.Description;
            contact.EmergencyContactName = request.EmergencyContactName ?? contact.EmergencyContactName;
            contact.EmergencyPhoneNumber = request.EmergencyPhoneNumber ?? contact.EmergencyPhoneNumber;
            if (request.EmployeeId != 0) { contact.EmployeeId = request.EmployeeId; }
            contact.HomePhoneNumber = request.HomePhoneNumber ?? contact.HomePhoneNumber;
            contact.MobilPhoneNumber = request.MobilPhoneNumber ?? contact.MobilPhoneNumber;
 
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteContactRecord(int Id)
        {
            var contact = await _context.Contact.FindAsync(Id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}