using SirmaAssignment.BL.Contracts;
using SirmaAssignment.BL.Helpers;
using SirmaAssignment.DAL.Models;

namespace SirmaAssignment.BL
{
    public class EmployeesService : IEmployeesService
    {
        public List<EmployeePairs> FindEmployeePairs(List<EmployeeWork> allEmployees)
        {
            var employeePairs = new List<EmployeePairs>();

            var employeesGroupedByProject = allEmployees
                .GroupBy(e => e.ProjectID)
                .Select(gr => gr.OrderBy(o => o.StartDate)
                .ToList())
                .Where(list => list.Count > 1)
                .ToList();

            // Going through all employees grouped by projects

            foreach (List<EmployeeWork> employeesPerProject in employeesGroupedByProject)
            {
                var employeesGroupedById = employeesPerProject
                    .GroupBy(e => e.EmpID)
                    .Select(gr => gr.ToList())
                    .ToList();

                // Going through the list of employees grouped by their Id in a project

                for (int i = 0; i < employeesGroupedById.Count; i++)
                {
                    var employeesList = employeesGroupedById[i];

                    foreach (var employee1 in employeesList)
                    {
                        for (int j = i + 1; j < employeesGroupedById.Count; j++)
                        {
                            var secondEmployeesList = employeesGroupedById[j];

                            foreach (var employee2 in secondEmployeesList)
                            {
                                // Checking if the employees actually worked together on overlapping days

                                if (employee1.StartDate > employee2.EndDate || employee1.EndDate < employee2.StartDate)
                                {
                                    continue;
                                }

                                var daysWorkingTogether = CalculationsHelper.CalculateCommonProjectsWork(employee1, employee2);

                                // If the common working days are 1 or more, then the employees are a Pair

                                if (daysWorkingTogether >= 1)
                                {
                                    employeePairs.Add(new EmployeePairs()
                                    {
                                        FirstEmployeeID = employee1.EmpID,
                                        SecondEmployeeID = employee2.EmpID,
                                        ProjectID = employee1.ProjectID,
                                        DaysWorkingTogether = daysWorkingTogether
                                    });
                                }
                            }
                        }
                    }
                }
            }

            // Grouping the Pairs and ordering them in a descending order to show the most days they worked together first

            var result = employeePairs
                .GroupBy(gr => new { gr.FirstEmployeeID, gr.SecondEmployeeID, gr.ProjectID })
                .Select(pair => new EmployeePairs()
                {
                    FirstEmployeeID = pair.Key.FirstEmployeeID,
                    SecondEmployeeID = pair.Key.SecondEmployeeID,
                    DaysWorkingTogether = pair.Sum(s => s.DaysWorkingTogether),
                    ProjectID = pair.First().ProjectID
                })
                .OrderByDescending(pair => pair.DaysWorkingTogether)
                .ToList();

            return result;
        }
    }
}
