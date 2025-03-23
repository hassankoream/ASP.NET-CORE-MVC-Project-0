using Demo.BLL.Services.Deparment;
using Demo.DAL.Presistance.Data;
<<<<<<< Updated upstream
using Demo.DAL.Repositories;
=======
using Demo.PL.ViewModels.Department;
>>>>>>> Stashed changes
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

        public DepartmentController(IDepartmentService departmentService)
        {
            //_departmentService = new DepartmentService(new DepartmentRepository(new DAL.Presistance.Data.ApplicationDbContext(new DbContextOptions<ApplicationDbContext>())));
            _departmentService = departmentService;
        }

        //Action => Master Action

        [HttpGet] //Defualt
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDeparments();
            return View(departments);
        }
    }
}
