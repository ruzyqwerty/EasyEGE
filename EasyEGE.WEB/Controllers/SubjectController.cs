using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using EasyEGE.BLL.Interfaces;
using EasyEGE.DAL.Entities;
using EasyEGE.Models;
using EasyEGE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyEGE.Controllers
{
    public class SubjectController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _db;
        private readonly ISubjectService _subjectService;
        private readonly IProblemService _problemService;
        private readonly IPictureService _pictureService;
        private readonly IOptionService _optionService;
        private readonly IUserService _userService;

        public SubjectController(UserManager<User> userManager, IUserService userService, ApplicationContext context, ISubjectService subjectService, IProblemService problemService, IPictureService pictureService, IOptionService optionService)
        {
            _userManager = userManager;
            _db = context;
            _subjectService = subjectService;
            _problemService = problemService;
            _pictureService = pictureService;
            _optionService = optionService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int subjectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            var subject = _subjectService.GetSubject(subjectId);
            ViewBag.Subject = subject.Name;
            return View("Index", subjectId);
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public async Task<IActionResult> AddTask(int? subjectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
                ProblemViewModel model = new ProblemViewModel
                {
                    Subjects = _subjectService.GetSubjectsNames().ToList()
                };
                if (subjectId != null)
                {
                    var subject = _subjectService.GetSubject(subjectId);
                    model.Subject = subject.Name;
                }
                return View("AddTask", model);
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public async Task<IActionResult> AddTask(ProblemViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            if (ModelState.IsValid)
            {
                var subject = _subjectService.GetSubject(model.Subject);
                Problem problem = new Problem
                {
                    SubjectId = subject.Id,
                    Number = model.Number,
                    Answer = model.Answer
                };
                await _problemService.AddProblemAsync(problem);
                foreach (var imageData in model.Condition)
                {
                    using var binaryReader = new BinaryReader(imageData.OpenReadStream());
                    var content = binaryReader.ReadBytes((int)imageData.Length);
                    var picture = new Picture { Content = content, Alt = $"{model.Subject} - {model.Number}", IsSolve = false, ProblemId = problem.Id };
                    await _pictureService.AddPictureAsync(picture);
                }
                foreach (var imageData in model.Solve)
                {
                    using var binaryReader = new BinaryReader(imageData.OpenReadStream());
                    var content = binaryReader.ReadBytes((int)imageData.Length);
                    var picture = new Picture { Content = content, Alt = $"{model.Subject} - {model.Number}", IsSolve = true, ProblemId = problem.Id };
                    await _pictureService.AddPictureAsync(picture);
                }
                return RedirectToAction("Index", "Subject", new { subjectId = subject.Id });
            }
            ModelState.AddModelError("Condition", ModelState.ErrorCount.ToString());
            return View(model);
        }

        public async Task<IActionResult> AllTask(int subjectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            var subject = _subjectService.GetSubject(subjectId);
            ViewBag.Subject = subject.Name;
            AllTaskViewModel model = new AllTaskViewModel
            {
                Problems = _problemService.GetProblemsBySubjectId(subjectId).ToList()
            };
            foreach (var problem in model.Problems)
            {
                model.Pictures.AddRange(_pictureService.GetPicturesByProblemId(problem.Id));
            }
            return View(model);
        }

        [Authorize(Roles = "Администратор")]
        public IActionResult Delete(int? id)
        {
            _problemService.RemoveProblem(id);
            return RedirectToAction("Home", "Index");
        }

        [HttpGet]
        public async Task<IActionResult> RandomTest(int subjectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            var subject = _subjectService.GetSubject(subjectId);
            ViewBag.Subject = subject.Name;
            var model = new RandomTestViewModel();
            var problemsOfSubject = _problemService.GetProblemsBySubjectId(subjectId);
            for (int i = 1; i <= subject.MaxNumberInTest; i++)
            {
                var problemsOfNumber = problemsOfSubject.Where(p => p.Number == i).ToList();
                if (problemsOfNumber.Count > 0)
                {
                    var problem = problemsOfNumber[new Random().Next(0, problemsOfNumber.Count)];
                    model.ProblemIds.Add(problem.Id);
                    model.Pictures.AddRange(_pictureService.GetPicturesByProblemId(problem.Id));
                    model.Problems.Add(problem);
                    model.SubjectId = subjectId;
                }
            }
            model.Answers = new string[model.ProblemIds.Count];
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RandomTest(RandomTestViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            var subject = _subjectService.GetSubject(model.SubjectId);
            ViewBag.Subject = subject.Name;
            if (ModelState.IsValid)
            {
                return RedirectToAction("Answers", "Subject", new AnswerViewModel { Answers = model.Answers, ProblemIds = model.ProblemIds, SubjectId = model.SubjectId });
            }

            return View(model);
        }

        public async Task<IActionResult> Answers(AnswerViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            var subject = _subjectService.GetSubject(model.SubjectId);
            ViewBag.Subject = subject.Name;
            foreach (var id in model.ProblemIds)
            {
                var problem = _problemService.GetProblem(id);
                model.Problems.Add(problem);
            }
            return View(model);
        }

        [Authorize(Roles = "Администратор, Учитель")]
        [HttpGet]
        public async Task<IActionResult> AddOption(string subjectName, int? number)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
                AddOptionViewModel model = new AddOptionViewModel
                {
                    Subjects = _subjectService.GetSubjects().ToList(),
                    Problems = _problemService.GetProblems().ToList(),
                    Pictures = _pictureService.GetPictures().ToList()
                };

                if (!(String.IsNullOrWhiteSpace(subjectName)))
                {
                    var subject = _subjectService.GetSubject(subjectName);
                    model.Problems = model.Problems.Where(p => p.SubjectId == subject.Id).ToList();
                    model.SubjectName = subject.Name;
                }

                if (number != null)
                {
                    model.Problems = model.Problems.Where(p => p.Number == number).ToList();
                }
                return View("AddOption", model);
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Администратор, Учитель")]
        [HttpPost]
        public async Task<IActionResult> AddOption(List<int> usingProblemsId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
                if (usingProblemsId.Count > 0)
                {
                    var problem = _problemService.GetProblem(usingProblemsId[0]);
                    var option = new Option
                    {
                        SubjectId = problem.SubjectId,
                        UserId = user.Id
                    };
                    _optionService.AddOptionAndLink(option, usingProblemsId);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Variant(int optionId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
            }
            var option = _optionService.GetOption(optionId);
            var subject = _subjectService.GetSubject(option.SubjectId);
            var whoAdd = _userService.GetUserNameById(option.UserId);
            ViewBag.Subject = subject.Name;
            var problemOptions = _optionService.GetProblemOptions(option.Id);
            var model = new VariantViewModel
            {
                UserName = whoAdd,
                SubjectId = subject.Id
            };
            foreach (var po in problemOptions)
            {
                var problem = _problemService.GetProblem(po.ProblemId);
                model.Problems.Add(problem);
                model.Pictures.AddRange(_pictureService.GetPicturesByProblemId(problem.Id));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Variant(string[] answers, List<int> problemsId, int subjectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.Name = user.Name;
                Subject subject = new Subject();
                if (problemsId.Count > 0)
                {
                    subject = _subjectService.GetSubject(subjectId);
                    ViewBag.Subject = subject.Name;
                }
                return RedirectToAction("Answers", "Subject", new AnswerViewModel { Answers = answers, ProblemIds = problemsId, SubjectId = subject.Id });
            }
            return RedirectToAction("Login", "Account");
        }
    }
}