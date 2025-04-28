using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Roles;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        #region Fields

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        //CRUD Operations    :   GetAll, Get, Update, Delete
        //Actions and Views  :   Index, Details, Edit, Delete 

        #region Services
        public RoleController(RoleManager<IdentityRole> _roleManager, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = _roleManager;
            this._env = env;
            this._userManager = userManager;
        }

        #endregion




        #region Index


        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            //Roles
            var RoleQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
            {
                RoleQuery = RoleQuery.Where(R => R.Name.ToLower().Contains(SearchValue.ToLower()));
            }
            var rolesList = await RoleQuery.Select(
                R => new RoleViewModel
                {
                    Id = R.Id,
                    Name = R.Name,



                }).ToListAsync();

            return View(rolesList);


        }
        #endregion


        #region Details
        //Request[Get]: baseUrl/Role/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var roleviewmodel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                
            };
            return View(roleviewmodel);
        }
        #endregion



        #region Delete
        //Show client what is going to be deleted
        //baseUrl/Role/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest(); //400
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();//404

            return View(new RoleViewModel
            {
                Id = id,
                Name = role.Name,
           
            });
        }
        //Delete what is the client want
        //baseUrl/Role/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id) //Change name because they have the same parameters and name
        {
            var role = await _roleManager.FindByIdAsync(id);

            var message = string.Empty;
            try
            {
                if (role is not null)
                {
                    var result = await _roleManager.DeleteAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                message = "An Error Happened When Deleting the Role";



            }
            catch (Exception ex)
            {
                message = ex.Message;
                //_logger.LogError(ex, message);

                message = _env.IsDevelopment() ? ex.Message : "An Error Happened When Deleting the Role";

            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Edit
        //Edit: Request[Get]: baseUrl/Role/Edit/{id}
        //
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest(); //400
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound(); //404

            //ViewData["Department"] = departmentServices.GetAllDeparments();
            var users = await _userManager.Users.ToListAsync();
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Users = users.Select(user => new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList(),
             
            });

        }


        //Edit: request[Post]: baseUrl/User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
                return View(roleViewModel);
            var message = string.Empty;
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                {
                    return NotFound();

                }
                role.Name = roleViewModel.Name;
                
                var result = await _roleManager.UpdateAsync(role);

                foreach(var userRole in roleViewModel.Users)
                {
                    var user = await _userManager.FindByIdAsync(userRole.UserId);
                    if(user is not null)
                    {
                        //Add role if it is not in the user database
                        if (userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                        //remove the role if it is in the user database
                        else if (!userRole.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                    }
                }

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Role Cannot be Updated";

            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Role Cannot be Updated";

            }
            //ViewData["Department"] = departmentServices.GetAllDeparments();

            return View(roleViewModel);
        }


        #endregion


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleViewModel.Name });
                return RedirectToAction(nameof(Index));

            }
            return View(roleViewModel);
        }
        #endregion
    }
}
