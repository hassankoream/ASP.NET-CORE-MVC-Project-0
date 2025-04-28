using Demo.BLL.DTOs.Employee;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Employee;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        #endregion

        //CRUD Operations    :   GetAll, Get, Update, Delete
        //Actions and Views  :   Index, Details, Edit, Delete 

        #region Services
        public UserController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            this._userManager = userManager;
            this._env = env;
        }

        #endregion




        #region Index


        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            //users
            var UserQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
            {
                UserQuery = UserQuery.Where(U => U.Email.ToLower().Contains(SearchValue.ToLower()));
            }
            var userList = await UserQuery.Select(
                U => new UserViewModel
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    Email = U.Email,
                    //Roles = U.Roles
                }).ToListAsync();
            foreach (var user in userList)
                user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
            return View(userList);
        }
        #endregion


        #region Details
        //Request[Get]: baseUrl/User/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var userviewmodel = new UserViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result,
            };
            return View(userviewmodel);
        }
        #endregion



        #region Delete
        //Show client what is going to be deleted
        //baseUrl/Employee/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest(); //400
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();//404

            return View(new UserViewModel
            {
                Id = id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result,
            });
        }
        //Delete what is the client want
        //baseUrl/Employee/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id) //Change name because they have the same parameters and name
        {
            var user = await _userManager.FindByIdAsync(id);

            var message = string.Empty;
            try
            {
                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                message = "An Error Happened When Deleting the User";



            }
            catch (Exception ex)
            {
                message = ex.Message;
                //_logger.LogError(ex, message);

                message = _env.IsDevelopment() ? ex.Message : "An Error Happened When Deleting the User";

            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Edit
        //Edit: Request[Get]: baseUrl/User/Edit/{id}
        //
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest(); //400
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound(); //404

            //ViewData["Department"] = departmentServices.GetAllDeparments();

            return View(new UserViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result,
            });

        }


        //Edit: request[Post]: baseUrl/User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return View(userViewModel);
            var message = string.Empty;
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    return NotFound();

                }
                user.FName = userViewModel.FName;
                user.LName = userViewModel.LName;
                user.Email = userViewModel.Email;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "User Cannot be Updated";

            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "User Cannot be Updated";

            }
            //ViewData["Department"] = departmentServices.GetAllDeparments();

            return View(userViewModel);
        }


        #endregion



    }
}
