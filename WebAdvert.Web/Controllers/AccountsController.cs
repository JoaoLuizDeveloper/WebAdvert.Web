using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.AspNetCore.Identity.Cognito;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.Models.Accounts;

namespace WebAdvert.Web.Controllers
{
    public class AccountsController : Controller
    {
        //pool == Group
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;

        public AccountsController( SignInManager<CognitoUser> signInManager, UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;
        }

        public async Task<IActionResult> Signup()
        {
            var model = new SignUp();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignUp model)
        {
            if (ModelState.IsValid)
            {
                var user = _pool.GetUser(model.Email);
                if(user.Status != null)
                {
                    ModelState.AddModelError("User Exist", "User with this email alredy exist");
                    return View(model);
                }


                user.Attributes.Add(CognitoAttribute.Name.ToString() , model.Email);
                var created = await _userManager.CreateAsync(user, model.Password);

                if (created.Succeeded)
                {
                    RedirectToAction("Confirm");
                }
            }

            return View();
        }


        public async Task<IActionResult> Confirm()
        {
            var model = new ConfirmModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Confirm")]
        public async Task<IActionResult> Confirm(ConfirmModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user == null)
                {
                    ModelState.AddModelError("Not Found", "A user with this email address was not found");
                    return View(model);
                }

                var result = await (_userManager as CognitoUserManager<CognitoUser>).ConfirmSignUpAsync(user, model.Code , true).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var items in result.Errors)
                    {
                        ModelState.AddModelError(items.Code, items.Description);
                    }
                    return View(model);
                }
            }
            
            return View(model);
        }
    }
}
