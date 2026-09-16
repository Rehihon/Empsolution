using System;
namespace EmployeeslnModel
{
    public class Jobhistory
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int JobID { get; set; }
        public int DepID { get; set; }
        public string ManagerID { get; set; }
    }
}