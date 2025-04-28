using AutoMapper;
using Demo.BLL.DTOs.Department;
using Demo.BLL.DTOs.Employee;
using Demo.BLL.Services.Deparment;
using Demo.BLL.Services.Employee;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Departments;
using Demo.PL.ViewModels.Department;
using Demo.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.PL.Controllers
{
    //EmployeeController: Inhertiance [is a Controller]
    //EmployeeController: Composation [has a Employee service]
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _autoMapper;

        //private readonly IDepartmentService departmentServices;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeService employeeService/*,IDepartmentService departmentServices*/, IMapper AutoMapper, ILogger<EmployeeController> logger, IWebHostEnvironment env)
        {
            //_EmployeeService = new EmployeeService(new EmployeeRepository(new DAL.Presistance.Data.ApplicationDbContext(new DbContextOptions<ApplicationDbContext>())));
            _employeeService = employeeService;
            _autoMapper = AutoMapper;
            //this.departmentServices = departmentServices;
            this._logger = logger;
            _env = env;
        }
        #endregion


        #region Index
        //Request[Get]: baseUrl/Employee/Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(SearchValue);
            return View(employees);
        }
        #endregion

        #region Details
        //Request[Get]: baseUrl/Employee/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee =await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
        #endregion

        #region Create
        //Request[Get]: baseUrl/Employee/Index/{id}

        //Show Client the form to create the object
        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartmentService departmentServices*/)
        {
            //ViewData["Department"] = departmentServices.GetAllDeparments(); 
            return View();
        }


        //Post what the client submitting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeEditViewModel EmployeeVM)
        {
            if (!ModelState.IsValid)
                return View(EmployeeVM);
            var message = string.Empty;
            try
            {
                var departmentToCreated = _autoMapper.Map<EmployeeEditViewModel, EmployeeToCreateDto>(EmployeeVM);

                //with AutoMApper 
                
                var result =await _employeeService.CreateEmployeeAsync(departmentToCreated);
                if (result > 0)
                {
                    message = $"Employee {EmployeeVM.Name} Created";
                    return RedirectToAction(nameof(Index));



                }
                else
                    message = "Employee Can not be Created";
                ModelState.AddModelError(string.Empty, message);

                return View(EmployeeVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(EmployeeVM);

                }
                else
                {
                    message = "Department Can not be Created";
                    return View("Error", message);
                }
            }
        }

        #endregion

        #region Delete
        //Show client what is going to be deleted
        //baseUrl/Employee/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
        //Delete what is the client want
        //baseUrl/Employee/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            var message = string.Empty;
            try
            {
                if (result)
                    return RedirectToAction(nameof(Index));
                message = "An Error Happend When Deleting the Employee";


            }
            catch (Exception ex)
            {
                message = ex.Message;
                _logger.LogError(ex, message);

                message = _env.IsDevelopment() ? ex.Message : "An Error Happend When Deleting the Employee";

            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Edit
        //Edit: Request[Get]: baseUrl/Employee/Edit/{id}
        //
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            //ViewData["Department"] = departmentServices.GetAllDeparments();

            return View(new EmployeeToUpdateDto()
            {
                Id = id.Value,
                Name = employee.Name,
                Email = employee.Email,
                Address = employee.Address,
                Age = employee.Age,
                Gender = Enum.TryParse<Gender>(employee.Gender, out var empgender) ? empgender : default,
                EmployeeType = Enum.TryParse<EmployeeType>(employee.EmployeeType, out var emptype) ? emptype : default,
                HiringDate = employee.HiringDate,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
            });

        }


        //Edit: request[Post]: baseUrl/Employee/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeToUpdateDto employeeToUpdateDto)
        {
            if (!ModelState.IsValid)
                return View(employeeToUpdateDto);
            var message = string.Empty;
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(employeeToUpdateDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Employee Cannot be Updated";

            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Employee Cannot be Updated";

            }
            //ViewData["Department"] = departmentServices.GetAllDeparments();

            return View(employeeToUpdateDto);
        }


        #endregion




























































































        ////Action => Master Action
        ////GET => baseUrl/Employee/Index
        //[HttpGet] //Defualt
        //public IActionResult Index()
        //{
        //    var employees = _employeeService.GetAllEmployees();
        //    return View(employees);
        //}

        ////Show the Form with Get the Form
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        ////Post the Data from View Form to Controller 
        //[HttpPost]
        //public IActionResult Create(EmployeeToCreateDto EmployeeToCreateDto)
        //{
        //    if (!ModelState.IsValid)
        //        return View(EmployeeToCreateDto);
        //    var message = string.Empty;
        //    try
        //    {
        //        var result = _employeeService.CreateEmployee(EmployeeToCreateDto);
        //        if (result > 0)
        //            return RedirectToAction(nameof(Index));
        //        else
        //            message = "Employee Can not be Created";
        //        ModelState.AddModelError(string.Empty, message);

        //        return View(EmployeeToCreateDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Execption
        //        _logger.LogError(ex, ex.Message);
        //        if (_env.IsDevelopment())
        //        {
        //            message = ex.Message;
        //            return View(EmployeeToCreateDto);
        //        }
        //        else
        //        {
        //            message = "Employee Can not be Created";
        //            return View("Error", message);
        //        }

        //    }
        //}


        ////GetDeatils
        //[HttpGet] //GET: base/Employee/Details/{Id}
        //public IActionResult Details(int? Id)
        //{
        //    if (Id is null)
        //        return BadRequest();//400
        //    var employee = _employeeService.GetEmployeeById(Id.Value);
        //    if (employee is null)
        //        return NotFound();//404
        //    return View(employee);
        //}


        //////Edit Form
        ////[HttpGet]
        ////public IActionResult Edit(int? Id)
        ////{
        ////    if (Id is null)
        ////    {
        ////        return BadRequest();
        ////    }
        ////    var Employee = _employeeService.GetEmployeeById(Id.Value);
        ////    if (Employee is null)
        ////        return NotFound();
        ////    return View(new EmployeeEditViewModel()
        ////    {
        ////        Code = Employee.Code,
        ////        Name = Employee.Name,
        ////        Description = Employee.Description,
        ////        CreationDate = Employee.CreationDate,
        ////    });


        ////}


        ////[HttpPost]
        ////public IActionResult Edit(int id, EmployeeEditViewModel EmployeeEditViewModel)
        ////{
        ////    if (!ModelState.IsValid)
        ////        return View(EmployeeEditViewModel);
        ////    var message = string.Empty;
        ////    try
        ////    {

        ////        var result = _employeeService.UpdateEmployee(new EmployeeToUpdateDto()
        ////        {
        ////            Id = id,
        ////            Code = EmployeeEditViewModel.Code,
        ////            Name = EmployeeEditViewModel.Name,
        ////            Description = EmployeeEditViewModel.Description,
        ////            CreationDate = EmployeeEditViewModel.CreationDate,
        ////        });
        ////        if (result > 0)
        ////            return RedirectToAction(nameof(Index));
        ////        else
        ////            message = "Employee Cannot be Updated";

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        message = _env.IsDevelopment() ? ex.Message : "Employee Cannot be Updated";

        ////    }
        ////    return View(EmployeeEditViewModel);


        ////}


        ////Get to show client what he is going to delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id is null)
        //        return BadRequest();
        //    var employee = _employeeService.GetEmployeeById(id.Value);
        //    if (employee == null)
        //        return NotFound();

        //    return View(employee);
        //}
        ////Post the Action Delete
        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    //id will never be null, we came from Delete view get!
        //    //if (id is null)
        //    //    return BadRequest();
        //    var result = _employeeService.DeleteEmployee(id);
        //    var message = string.Empty;
        //    try
        //    {
        //        if(result)
        //            return RedirectToAction(nameof(Index));
        //        message = "An Error Happend When Deleting the Employee";

        //    }
        //    catch(Exception ex)
        //    {
        //        message = ex.Message;
        //        _logger.LogError(ex, message);

        //        message = _env.IsDevelopment()? ex.Message: "An Error Happend When Deleting the Employee";

        //    }
        //    //var Employee = _EmployeeService.GetEmployeeById(id);
        //    //return View(nameof(Index));
        //    ModelState.AddModelError(string.Empty, message);
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
