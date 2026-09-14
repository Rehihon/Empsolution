using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeData;
using EmployeeDomain;

namespace MyApi.Controllers{

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly AppDbContext _context;

    public AddressController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Address>> GetAddressList()
    {
        return await _context.Address.ToListAsync();
    }

        [HttpPost]
        public async Task<IActionResult> InsertAddress(Address address)
        {
            _context.Address.Add(address);

            await _context.SaveChangesAsync();

            return Ok(address);
        }

        [HttpPut("{AddressId}")]
        public async Task<IActionResult> UpdateAddressRecord(int addressId, Address request)
        {
            var address = await _context.Address.FindAsync(addressId);
            if (address == null)
            {
                return NotFound();
            }
            address.Country = request.Country ?? address.Country;
            address.County = request.Country ?? address.County;
            address.Line1 = address.Line1 ?? address.Line1;
            address.Line2 = address.Line2 ?? address.Line1;
            address.Postcode = address.Postcode ?? address.Postcode;

            await _context.SaveChangesAsync();

            return Ok(address);
        }

        [HttpDelete("{AddressId}")]
        public async Task<IActionResult> DeleteAddressRecord(int addressId)
        {
            var address = await _context.Address.FindAsync(addressId);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
    
    // [HttpPost]
    //     public async Task<IActionResult> Create(List<Address> address)
    //     {
    //         var entities= address.Select (x=>new Address
    //         {
    //             AddressId =     x.AddressId,
    //             Line1     =         x.Line1,
    //             Line2     =         x.Line2,
    //             Postcode  =         x.Postcode,
    //             County    =         x.County,
    //             CountryID =         x.CountryID
    //         }).ToList();
    //         _context.Address.AddRange(entities);
    //         await _context.SaveChangesAsync();
    //         return Ok(entities);
    //     }
}

}