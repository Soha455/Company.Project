using Azure.Identity;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Company.Project.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Sign Up
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        // P@ssW0rd
        //PP@Sw0rd
        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree
                        };


                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {

                            return RedirectToAction("SignIn");
                        }

                        foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }

                ModelState.AddModelError("", "Invalid SignUp !!");

            }

            return View();
        }

        #endregion

        #region SignIn

        [HttpGet] //Account/SigIn 
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag is true)
                    {
                        // Sign In
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid Login !");
            }
            return View(model);
        }

        #endregion

        #region SignOut

        [HttpGet]
        public new async Task<ActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion

    }
}
