using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyEGE.ViewModels;
using EasyEGE.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using EasyEGE.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using EasyEGE.BLL.Interfaces;
using EasyEGE.WEB.ViewModels;
using System.Collections.Generic;

namespace EasyEGE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptionService _optionService;
        private readonly ISubjectService _subjectService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IOptionService optionService, ISubjectService subjectService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _optionService = optionService;
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var allRoles = _roleManager.Roles.Where(r => r.Name == "Ученик" || r.Name == "Учитель").ToList();
            RegisterViewModel model = new RegisterViewModel
            {
                AllRoles = allRoles
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string chooserole)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, chooserole);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                }
            }
            var allRoles = _roleManager.Roles.Where(r => r.Name == "Ученик" || r.Name == "Учитель").ToList();
            model.AllRoles = allRoles;
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AccessDenied()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
                var allOptions = _optionService.GetAllUserOptions(user.Id);
                List<OptionViewModel> options = new List<OptionViewModel>();
                foreach (var option in allOptions)
                {
                    var newOption = new OptionViewModel();
                    newOption.Id = option.Id;
                    newOption.Subject = _subjectService.GetSubject(option.SubjectId).Name;
                    options.Add(newOption);
                }
                return View("Profile", options);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}