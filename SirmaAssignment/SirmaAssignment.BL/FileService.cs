using SirmaAssignment.BL.Contracts;
using SirmaAssignment.DAL.Models;
using System.Globalization;

namespace SirmaAssignment.BL
{
    public class FileService : IFileService
    {
        private static readonly string[] dateFormats = new string[] {"yyyy-MM-dd", "yyyy/MM/dd", "yyyy.MM.dd", "yyyy-dd-MM", "yyyy/dd/MM", "yyyy.dd.MM",
            "MM-dd-yyyy", "MM/dd/yyyy", "MM.dd.yyyy", "MMM-dd-yyyy", "MMM/dd/yyyy", "MMM.dd.yyyy", "MMMM-dd-yyyy", "MMMM/dd/yyyy", "MMMM.dd.yyyy" };
        
        public List<EmployeeWork> ReadFromCSV(string path)
        {
            var rows = new List<string[]>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    rows.Add(reader.ReadLine().Split(','));
                }
            }

            // Reading the file with the StreamReader, making an array of string rows and then parsing them to EmployeeWork models

            var employeesWork = ParseData(rows);
            return employeesWork;
        }

        public List<EmployeeWork> ParseData(List<string[]> fileRows)
        {
            var employeeWorkModels = new List<EmployeeWork>();

            // Reading each row in the array and filling EmployeeWork with data, also setting DateTime properties to work with many data formats

            foreach (var row in fileRows)
            {
                var employeeWork = new EmployeeWork();

                employeeWork.EmpID = int.Parse(row[0].Trim());
                employeeWork.ProjectID = int.Parse(row[1].Trim());
                employeeWork.StartDate = DateTime.ParseExact(row[2].Trim(), dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                if (row[3].Trim() == "NULL")
                    employeeWork.EndDate = DateTime.Now.Date;
                else
                    employeeWork.EndDate = DateTime.ParseExact(row[3].Trim(), dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                employeeWorkModels.Add(employeeWork);
            }

            return employeeWorkModels;
        }
    }
}
