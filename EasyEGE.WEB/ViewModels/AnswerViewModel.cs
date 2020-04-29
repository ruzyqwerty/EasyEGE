using EasyEGE.DAL.Entities;
using EasyEGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEGE.ViewModels
{
    public class AnswerViewModel
    {
        public string[] Answers { get; set; }
        public List<int> ProblemIds { get; set; }
        public int SubjectId { get; set; }
        public List<Problem> Problems {get ;set;}
        public AnswerViewModel()
        {
            Problems = new List<Problem>();
        }
    }
}
