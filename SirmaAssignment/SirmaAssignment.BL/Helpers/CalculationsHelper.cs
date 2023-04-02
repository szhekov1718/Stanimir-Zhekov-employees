using SirmaAssignment.DAL.Models;

namespace SirmaAssignment.BL.Helpers
{
    public static class CalculationsHelper
    {
        public static int CalculateCommonProjectsWork(EmployeeWork employee1, EmployeeWork employee2)
        {
            // Calculating the number of days that employees spent working together on common projects

            int daysWorkingTogether = 0;

            if (employee1.StartDate <= employee2.StartDate && employee1.EndDate <= employee2.EndDate)
            {
                daysWorkingTogether = CalculateDifference(employee2.StartDate, employee1.EndDate);
            }
            else if (employee1.StartDate >= employee2.StartDate && employee1.EndDate >= employee2.EndDate)
            {
                daysWorkingTogether = CalculateDifference(employee1.StartDate, employee2.EndDate);
            }
            else if (employee1.StartDate >= employee2.StartDate && employee1.EndDate <= employee2.EndDate)
            {
                daysWorkingTogether = CalculateDifference(employee1.StartDate, employee2.EndDate);
            }
            else if (employee1.StartDate <= employee2.StartDate && employee1.EndDate >= employee2.EndDate)
            {
                daysWorkingTogether = CalculateDifference(employee2.StartDate, employee2.EndDate);
            }
            return daysWorkingTogether;
        }

        // Calculating the difference in days from the start date to the end date
        private static int CalculateDifference(DateTime startDate, DateTime endDate)
        {
            return (int)(endDate - startDate).TotalDays;
        }
    }
}
