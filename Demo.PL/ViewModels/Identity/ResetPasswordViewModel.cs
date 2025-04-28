using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        //[Compare(nameof(Password), ErrorMessage ="Password doesn't Match")]
        [Compare("Password", ErrorMessage = "Password doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}
