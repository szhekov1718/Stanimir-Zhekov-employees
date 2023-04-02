using Microsoft.AspNetCore.Mvc;
using SirmaAssignment.BL.Contracts;

namespace SirmaAssignment.Controllers
{
    public class CSVFileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IEmployeesService _employeeService;
        public CSVFileController(IFileService fileService, IEmployeesService employeeService)
        {
            _fileService = fileService;
            _employeeService = employeeService;
        }

        [HttpPost("CSVFile")]
        public async Task<IActionResult> CSVFile(IFormFile file)
        {
            var newFilePath = string.Empty;

            if (file != null)
            {
                try
                {
                    // Creating a temporary file and using its path to copy the date from the CSV input file to the new file

                    if (file.Length > 0)
                    {
                        newFilePath = Path.GetTempFileName();

                        using var fileStream = new FileStream(newFilePath, FileMode.Create);

                        await file.CopyToAsync(fileStream);
                    }

                    var employeesListFromFile = _fileService.ReadFromCSV(newFilePath);

                    var teamViewModels = _employeeService.FindEmployeePairs(employeesListFromFile);

                    return View(teamViewModels);
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
