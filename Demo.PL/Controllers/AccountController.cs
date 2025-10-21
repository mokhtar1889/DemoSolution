using Demo.DAL.Models.IdentityModels;
using Demo.PL.utilities;
using Demo.PL.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model) { 
            
            if(!ModelState.IsValid) return View(model);

            var userToAdd = new ApplicationUser()
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,

            };
            var res = _userManager.CreateAsync(userToAdd, model.Password).Result;
            if (res.Succeeded) return RedirectToAction("Login");
            else {

                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);

            }



        }



        [HttpGet]
        public IActionResult Login() {
        return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model) {

            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                var isCorrext = _userManager.CheckPasswordAsync(user,model.Password).Result;
                if (isCorrext) {
                   var res =  _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if (res.IsNotAllowed) ModelState.AddModelError(string.Empty, "Your account is not allowed");
                    if (res.IsLockedOut) ModelState.AddModelError(string.Empty, "Your account is Locked");
                    if (res.Succeeded) return RedirectToAction(nameof(HomeController.Index),"Home") ;

                }
            }
            else {

                ModelState.AddModelError(string.Empty, "invalid Login");

            }

                return View();
        }

        [HttpGet]
        public IActionResult LogOut() {

            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        
        }


        [HttpGet]
        public IActionResult ForgetPassword() {

            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid) { 
            
                var user = _userManager.FindByEmailAsync(model.Email).Result;
                if (user is not null)
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action("ResetPasswordLink","Account",new {email = model.Email , token} , Request.Scheme);
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = resetPasswordLink
                    };
                    var result = MailSittings.SendEmail(email);

                    if (result) return RedirectToAction("CheckYourInbox");
                    
                }
            }

            ModelState.AddModelError(string.Empty, "invalid email");
            return View(nameof(ForgetPassword) , model);
            
        }


        [HttpGet]
        public IActionResult CheckYourInbox() {

            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordLink(string email , string token)
        {

            return View();
        }

        [HttpPost]
        public IActionResult ResetPasswordLink(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var email = TempData["email"] as string;
            var token = TempData["token"] as string;

            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                var res = _userManager.ResetPasswordAsync(user, token, model.Password).Result;
                if (res.Succeeded) return RedirectToAction(nameof(Login));
                else
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
            }
       
            
                return View(model);
       
        }
    }
}
