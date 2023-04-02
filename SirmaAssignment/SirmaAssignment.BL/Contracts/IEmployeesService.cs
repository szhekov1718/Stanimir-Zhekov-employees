using SirmaAssignment.DAL.Models;

namespace SirmaAssignment.BL.Contracts
{
    public interface IEmployeesService
    {
        List<EmployeePairs> FindEmployeePairs(List<EmployeeWork> employeeProjects);
    }
}
