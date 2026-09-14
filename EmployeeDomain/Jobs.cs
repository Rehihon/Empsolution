using System.ComponentModel.DataAnnotations;
namespace EmployeeDomain;

public class Jobs
{
  [Key]
  public int JobID { get; set; }  
  public string? Job_Title { get; set; }
  public decimal Min_salary { get; set; }
  public decimal Max_Salary { get; set; }
  public string? Description { get; set; }
}