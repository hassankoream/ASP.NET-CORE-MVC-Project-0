using AutoMapper;
using Demo.BLL.DTOs.Department;
using Demo.BLL.Services.Deparment;
using Demo.DAL.Presistance.Data;

using Demo.PL.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.PL.Controllers
{
    //DepartmentController: Inhertiance [is a Controller]
    //DepartmentController: Composation [has a department service]

    //[AllowAnonymous]
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Services

        private readonly IDepartmentService _departmentService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentService departmentService, IMapper AutoMapper, ILogger<DepartmentController> logger, IWebHostEnvironment env)
        {
            //_departmentService = new DepartmentService(new DepartmentRepository(new DAL.Presistance.Data.ApplicationDbContext(new DbContextOptions<ApplicationDbContext>())));
            _departmentService = departmentService;
            _autoMapper = AutoMapper;
            this._logger = logger;
            _env = env;
        }
        #endregion

        #region Index
        //Action => Master Action

        [HttpGet] //Default
        public async Task<IActionResult> Index()
        {
            ViewData["Message01"] = "Hello from view data";
            ViewData["Message02"] = new DepartmentDetailsToReturnDto() { Name = "Dept02" };
            ViewBag.Message03 = new DepartmentDetailsToReturnDto() { Name = "Dept03" };
            //TempData["message"] = "Hello from Temp Data";
            var departments = await _departmentService.GetAllDeparmentsAsync();
            return View(departments);
        }
        #endregion

        #region Create
        //Show the Form with Get the Form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post the Data from View Form to Controller 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentMV)
        {
            if (!ModelState.IsValid)
                return View(departmentMV);
            var message = string.Empty;
            try
            {
                //with AutoMApper 
                var departmentToCreated = _autoMapper.Map<DepartmentViewModel, DepartmentToCreateDto>(departmentMV);

                var result = await _departmentService.CreateDepartmentAsync(departmentToCreated);
                    
                //Without AutoMapper
                //    (new DepartmentToCreateDto()
                //{
                //    Code = departmentMV.Code,
                //    Name = departmentMV.Name,
                //    Description = departmentMV.Description,
                //    CreationDate = departmentMV.CreationDate,
                //});
                if (result > 0)
                {
                    message = $"Department {departmentMV.Name} Created";
                    //return RedirectToAction(nameof(Index));
                }

                else
                    message = $"Department {departmentMV.Name} Can not be Created";
                ModelState.AddModelError(string.Empty, message);

                //return View(departmentMV);
                TempData["message"] = message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //Log Exception
                _logger.LogError(ex, ex.Message);
                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentMV);
                }
                else
                {
                    message = "Department Can not be Created";
                    return View("Error", message);
                }

            }
        }
        // public IActionResult Create(DepartmentToCreateDto departmentToCreateDto)
        //{
        //    if (!ModelState.IsValid)
        //        return View(departmentToCreateDto);
        //    var message = string.Empty;
        //    try
        //    {
        //        var result = _departmentService.CreateDepartment(departmentToCreateDto)

        //        if (result > 0)
        //            return RedirectToAction(nameof(Index));
        //        else
        //            message = "Department Can not be Created";
        //        ModelState.AddModelError(string.Empty, message);

        //        return View(departmentToCreateDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Exception
        //        _logger.LogError(ex, ex.Message);
        //        if (_env.IsDevelopment())
        //        {
        //            message = ex.Message;
        //            return View(departmentToCreateDto);
        //        }
        //        else
        //        {
        //            message = "Department Can not be Created";
        //            return View("Error", message);
        //        }

        //    }
        //}


        #endregion


        #region Details
        //GetDeatils
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id is null)
                return BadRequest();//400
            var department = await _departmentService.GetDepartmentByIdAsync(Id.Value);
            if (department is null)
                return NotFound();//404
            return View(department);
        }
        #endregion


        #region Edit

        //Edit Form
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id is null)
            {
                return BadRequest();
            }
            var department = await _departmentService.GetDepartmentByIdAsync(Id.Value);
            if (department is null)
                return NotFound();

            //With AutoMapper
            var departmentVM = _autoMapper.Map<DepartmentDetailsToReturnDto, DepartmentViewModel>(department);
            return View(departmentVM);
            //Without AutoMapper

            //return View(new DepartmentViewModel()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate,
            //});


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentViewModel departmentEditViewModel)
        {
            if (!ModelState.IsValid)
                return View(departmentEditViewModel);
            var message = string.Empty;
            try
            {
                //var departmentUpdated = _autoMapper.Map<DepartmentToUpdateDto>(departmentEditViewModel);
                var result = await _departmentService.UpdateDepartmentAsync(new DepartmentToUpdateDto()
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

        #endregion

        #region Delete
        //Get to show client what he is going to delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
                return NotFound();

            return View(department);
        }
        //Post the Action Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            //id will never be null, we came from Delete view get!
            //if (id is null)
            //    return BadRequest();
            var result = await _departmentService.DeleteDepartmentAsync(id);
            var message = string.Empty;
            try
            {
                if (result)
                    return RedirectToAction(nameof(Index));
                message = "An Error Happened When Deleting the Department";

            }
            catch (Exception ex)
            {
                message = ex.Message;
                _logger.LogError(ex, message);

                message = _env.IsDevelopment() ? ex.Message : "An Error Happened When Deleting the Department";

            }
            //var department = _departmentService.GetDepartmentById(id);
            //return View(nameof(Index));
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
