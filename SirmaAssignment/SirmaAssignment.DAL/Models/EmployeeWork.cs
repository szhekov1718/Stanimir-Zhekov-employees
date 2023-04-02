namespace SirmaAssignment.DAL.Models
{
    public class EmployeeWork
    {
        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
