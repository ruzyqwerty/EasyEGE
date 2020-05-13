using Microsoft.AspNetCore.Mvc;
using EasyEGE.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using EasyEGE.ViewModels;
using EasyEGE.BLL.Interfaces;
using EasyEGE.BLL.Services;
using EasyEGE.DAL.Entities;

namespace EasyEGE.Controllers
{
    public class HomeController : Controller
    {
        private ISubjectService subjectService;
        private readonly UserManager<User> _userManager;

        public HomeController(ISubjectService service, UserManager<User> userManager)
        {
            subjectService = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            HomePageViewModel model = new HomePageViewModel
            {
                MathId = subjectService.GetSubject("Математика").Id,
                ComputerScienceId = subjectService.GetSubject("Информатика").Id,
                PhysicsId = subjectService.GetSubject("Физика").Id,
                RussianId = subjectService.GetSubject("Русский язык").Id
            };
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel model)
        {
            return View(model);
        }
    }
}
