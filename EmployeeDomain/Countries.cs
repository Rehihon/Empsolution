using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EmployeeDomain;

public class Countries
{       
      [Key]
      public int CountryID { get; set; }
      public string? Country_Name { get; set; }
}

