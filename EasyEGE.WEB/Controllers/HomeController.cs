using Microsoft.AspNetCore.Mvc;
using EasyEGE.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using EasyEGE.ViewModels;
using EasyEGE.BLL.Interfaces;
using EasyEGE.BLL.Services;

namespace EasyEGE.Controllers
{
    public class HomeController : Controller
    {
        private ISubjectService subjectService;

        public HomeController(ISubjectService service)
        {
            subjectService = service;
        }

        public IActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel
            {
                MathId = subjectService.GetSubject("Математика").Id,
                ComputerScienceId = subjectService.GetSubject("Информатика").Id,
                PhysicsId = subjectService.GetSubject("Физика").Id,
                RussianId = subjectService.GetSubject("Русский язык").Id
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
