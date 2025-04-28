using Demo.BLL.Services.EmailSettings;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSettings _emailSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSettings emailSettings)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailSettings = emailSettings;
        }
        [HttpGet] //Display Form
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    FName = registerViewModel.FName,
                    LName = registerViewModel.LName,
                    IsAgree = registerViewModel.IsAgree,

                };

                //Sending user and password to hash it
                //Interface and classes [Signature for methods, methods itself in classes]
                var Result = await _userManager.CreateAsync(User, registerViewModel.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var Error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, Error.Description);

                    }
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (flag) //Email exist and password correct
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else//Email exist and password incorrect
                    {
                        ModelState.AddModelError(string.Empty, "Password is Incorrect");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Not Found");
                }


            }
            return View(loginViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    //https://localhost:7156/Account/ResetPassword?email=Hassan@gmail.com&token=hjgjhbvnjgyrted56487kn


                    var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email, token = token }, Request.Scheme) ;
                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset your Password",
                        Body = url,
                    };
                    //Send Email step
                    _emailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }

                ModelState.AddModelError(string.Empty, "Invalid operation, Please try again");

            }
            return View(forgetPasswordViewModel);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            //this to end them from action to action and to avoid hacking
            TempData["email"] = email;
            TempData["token"] = token;
            //
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);

                if (user is not null)
                {
                   var result = await  _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                        
                    }

                }

            }

            ModelState.AddModelError(string.Empty, "Invalid Operation please try again");
            return View(resetPasswordViewModel);
        }

    }
}
