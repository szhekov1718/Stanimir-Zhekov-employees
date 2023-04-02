using SirmaAssignment.DAL.Models;

namespace SirmaAssignment.BL.Contracts
{
    public interface IFileService
    {
        List<EmployeeWork> ReadFromCSV(string filePath);
        List<EmployeeWork> ParseData(List<string[]> rowsInFile);
    }
}
