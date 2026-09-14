namespace EmployeeDomain;
using System.ComponentModel.DataAnnotations;

public class Employee
{   
    [Key]
   public int EmployeeId { get; set; } 
    public string? FirstName { get; set; }
    public string? Surename { get; set; } 
     public int CountactId { get; set; } 
     public DateOnly? HireDate { get; set; }
     public int? JobID { get; set; } 
    public int? DepID { get; set; }
    public DateOnly? DOB { get; set; }
}