using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.IdentityViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
