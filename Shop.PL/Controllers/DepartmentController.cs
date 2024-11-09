using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Services.DepartmentServices;
using Shop.BLL.Services.Dtos.Department;

namespace Shop.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentCreateDto> _logger;

        public DepartmentController(IDepartmentServices departmentServices
            ,ILogger<DepartmentCreateDto> logger)
        {
            _departmentServices = departmentServices;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var Department=_departmentServices.GetAllDepartment();
            return View(Department);
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var Department = _departmentServices.GetDepartmentById(id.Value);
                if (Department is not null)
                    return View(Department);
                
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentCreateDto department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Department = _departmentServices.Creat(department);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Departmen Not Created");
                return View(department);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return View(department);
            }
        }
    }
}
