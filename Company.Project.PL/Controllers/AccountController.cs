using Azure.Identity;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Company.Project.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        #region Sign Up
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

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

            #endregion

            #region SignOut

            #endregion

        }
}
