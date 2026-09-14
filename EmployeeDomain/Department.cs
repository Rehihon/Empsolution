namespace EmployeeDomain;
using System.ComponentModel.DataAnnotations;

public class Department
{   [Key]
    public int DepID { get; set; }
    public string? DepName { get; set; }
    public int? MangerID { get; set; }
    public int? AddressId { get; set; }
    public string? Description{ get; set; }
    
    
}