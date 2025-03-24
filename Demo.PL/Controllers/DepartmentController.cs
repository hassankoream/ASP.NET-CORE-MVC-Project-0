using Demo.BLL.DTOs.Department;
using Demo.BLL.Services.Deparment;
using Demo.DAL.Presistance.Data;

using Demo.PL.ViewModels.Department;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.PL.Controllers
{
    //DepartmentController: Inhertiance [is a Controller]
    //DepartmentController: Composation [has a department service]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment env)
        {
            //_departmentService = new DepartmentService(new DepartmentRepository(new DAL.Presistance.Data.ApplicationDbContext(new DbContextOptions<ApplicationDbContext>())));
            _departmentService = departmentService;
            this._logger = logger;
            _env = env;
        }

        //Action => Master Action

        [HttpGet] //Defualt
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDeparments();
            return View(departments);
        }

        //Show the Form with Get the Form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post the Data from View Form to Controller 
        [HttpPost]
        public IActionResult Create(DepartmentToCreateDto departmentToCreateDto)
        {
            if (!ModelState.IsValid)
                return View(departmentToCreateDto);
            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(departmentToCreateDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Departemnt Can not be Created";
                ModelState.AddModelError(string.Empty, message);

                return View(departmentToCreateDto);
            }
            catch (Exception ex)
            {
                //Log Execption
                _logger.LogError(ex, ex.Message);
                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentToCreateDto);
                }
                else
                {
                    message = "Departemnt Can not be Created";
                    return View("Error", message);
                }

            }
        }


        //GetDeatils
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest();//400
            var department = _departmentService.GetDepartmentById(Id.Value);
            if (department is null)
                return NotFound();//404
            return View(department);
        }


        //Edit Form
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(Id.Value);
            if (department is null)
                return NotFound();
            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });


        }


        [HttpPost]
        public IActionResult Edit(int id, DepartmentEditViewModel departmentEditViewModel)
        {
            if (!ModelState.IsValid)
                return View(departmentEditViewModel);
            var message = string.Empty;
            try
            {

                var result = _departmentService.UpdateDepartment(new DepartmentToUpdateDto()
                {
                    Id = id,
                    Code = departmentEditViewModel.Code,
                    Name = departmentEditViewModel.Name,
                    Description = departmentEditViewModel.Description,
                    CreationDate = departmentEditViewModel.CreationDate,
                });
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Department Cannot be Updated";

            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Department Cannot be Updated";

            }
            return View(departmentEditViewModel);


        }


        //Get to show client what he is going to delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department == null)
                return NotFound();

            return View(department);
        }
        //Post the Action Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            //id will never be null, we came from Delete view get!
            //if (id is null)
            //    return BadRequest();
            var result = _departmentService.DeleteDepartment(id);
            var message = string.Empty;
            try
            {
                if(result)
                    return RedirectToAction(nameof(Index));
                message = "An Error Happend When Deleting the Department";

            }
            catch(Exception ex)
            {
                message = ex.Message;
                _logger.LogError(ex, message);

                message = _env.IsDevelopment()? ex.Message: "An Error Happend When Deleting the Department";

            }
            //var department = _departmentService.GetDepartmentById(id);
            //return View(nameof(Index));
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }

    }
}
