
using EasyEGE.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyEGE.ViewModels
{
    public class ProblemViewModel
    {
        [Required(ErrorMessage = "Не указан предмет.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Не указан номер задания.")]
        public int Number { get; set; }

        public string Answer { get; set; }

        [Required(ErrorMessage = "Не указано условие задания.")]
        public List<IFormFile> Condition { get; set; }

        public List<IFormFile> Solve { get; set; }

        public List<string> Subjects { get; set; }

        public ProblemViewModel()
        {
            Condition = new List<IFormFile>();
            Solve = new List<IFormFile>();
            Subjects = new List<string>();
        }
    }
}
