using System.ComponentModel.DataAnnotations;
namespace EmployeeDomain
{
    public class Contact
    {
        [Key]
       public int ContactId { get; set; }   
       public int EmployeeId { get; set; }
       public string Email { get; set; }
       public string? HomePhoneNumber { get; set; }
       public string ? MobilPhoneNumber { get; set; }
       public string ? EmergencyPhoneNumber { get; set; }
       public string ? EmergencyContactName { get; set; }
       public string ? Description { get; set; }
    }
}