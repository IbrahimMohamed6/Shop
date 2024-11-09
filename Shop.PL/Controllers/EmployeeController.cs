using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Services.DepartmentServices;
using Shop.BLL.Services.Dtos.Employee;
using Shop.BLL.Services.EmployeeServices;
using Shop.DAL.Entites.Employee;
using Shop.PL.Models;

namespace Shop.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<CreateUpdateEmployeeDto> _logger;

        public EmployeeController(IEmployeeService employeeService
           , IDepartmentServices departmentServices
            ,ILogger<CreateUpdateEmployeeDto> logger)
        {
            _employeeService = employeeService;
            _departmentServices = departmentServices;
            _logger = logger;
        }
        public IActionResult Index(string search)
        {
           var Employees= _employeeService.GetEmployee(search);

            return View(Employees);
        }

        public IActionResult Details(int? id)
        {
            ViewData["Departments"] = _departmentServices.GetAllDepartment();
            if (id == null)
                return BadRequest();
            var Employee= _employeeService.GetEmployeeById(id);
            if (Employee == null)
                return NotFound();
            return View(Employee);
        }

        public IActionResult Edit(int? id)
        {
            ViewData["Departments"] = _departmentServices.GetAllDepartment();
            if (id == null)
                return BadRequest();
            var Employee = _employeeService.GetEmployeeById(id);
            if (Employee == null)
                return NotFound();
            //return View(Employee);
            return View(new CreateUpdateEmployeeDto()
            {
                Address = Employee.Address,
                DepartmentId = Employee.DepartmentID,
                HiringDate = Employee.HiringDate,
                Age = Employee.Age,
                CreatedBy = Employee.CreatedBy,
                CreatedOn = Employee.CreatedOn,
                Email = Employee.Email,
                EmployeeType = Employee.EmployeeType,
                Gender = Employee.Gender,
                Id = Employee.Id,
                IsActive = Employee.IsActive,
                LastModifiedBy = Employee.LastModifiedBy,
                LastModifiedOn = Employee.LastModifiedOn,
                Name = Employee.Name,
                PhoneNumber = Employee.PhoneNumber,
                Salary = Employee.Salary,
            });

        }
        [HttpPost]
        public IActionResult Edit(int?id,CreateUpdateEmployeeDto employeeDto)
        {
            try
            {
                if(employeeDto.Id!=id)
                    return BadRequest();
                if (ModelState.IsValid)
                {
                   var Result= _employeeService.Update(employeeDto)>0;
                    if(Result)
                        TempData["SuccessMessage"] = "Employee is Updated";
                    else
                        TempData["ErrorMessage"] = "Something went wrong!";
                return RedirectToAction("Index");

                }
                ModelState.AddModelError("", "Employee Not Updated");
                TempData["ErrorMessage"] = "Something went wrong!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(employeeDto);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = _departmentServices.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUpdateEmployeeDto employeeDto)
        {
            try
            {
                ViewData["Departments"] = _departmentServices.GetAllDepartment();

                if (ModelState.IsValid)
                {
                    var Result = _employeeService.Creat(employeeDto)>0;
                    if( Result )
                        TempData["SuccessMessage"] = "Employee is Created";
                    else
                        TempData["ErrorMessage"] = "Something went wrong!";
                    return RedirectToAction("Index");

                }
                else
                ModelState.AddModelError("", "Employee Not Updated");
                  TempData["ErrorMessage"] = "Something went wrong!";
                  return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                _logger.LogError (ex, ex.Message);
                return View(employeeDto);
            }
        }

        public IActionResult Delete(int ?id)
        {
            var Message = string.Empty;
            if (id is null)
                return BadRequest();
            var Employee = _employeeService.GetEmployeeById(id.Value);
            if (Employee == null)
                return NotFound();
            var Deleted = _employeeService.Delete(id.Value);
            if (Deleted)
                TempData["DeleteMessage"] = "Department is deleted.";
            else
                TempData["ErrorMessage"] = "Something went wrong!";

            return RedirectToAction("Index");

        }
    }

}
