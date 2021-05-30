using EduPortalV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortalV2.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager; // injection
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager
                                      )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                string isStudent = string.Empty;

                if (model.Role)
                {
                    isStudent = "Educator";
                    IdentityResult roleResult;
                    bool adminRoleExists = await _roleManager.RoleExistsAsync(isStudent);
                    if (!adminRoleExists)
                    {
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(isStudent));
                    }

                    if (!await _userManager.IsInRoleAsync(user, isStudent))
                    {
                        var userResult = await _userManager.AddToRoleAsync(user, isStudent);
                    }
                }
                else
                {
                    isStudent = "Student";
                    IdentityResult roleResult;
                    bool adminRoleExists = await _roleManager.RoleExistsAsync(isStudent);
                    if (!adminRoleExists)
                    {
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(isStudent));
                    }

                    if (!await _userManager.IsInRoleAsync(user, isStudent))
                    {
                        var userResult = await _userManager.AddToRoleAsync(user, isStudent);
                    }
                }
                
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    
    }
}
