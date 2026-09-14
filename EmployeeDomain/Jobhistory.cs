using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
namespace EmployeeDomain;

public class Jobhistory
{
  [Key]
  public int Id { get; set; }
  public int EmployeeId { get; set; }  
  public DateOnly? StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  public int JobID { get; set; }
  public int DepID { get; set; }
  public string ManagerID { get; set; }
  public string? Description { get; set; }
  public  Decimal Salary { get; set; }
}