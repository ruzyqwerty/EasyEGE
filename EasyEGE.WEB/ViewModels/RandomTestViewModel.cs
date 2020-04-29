using EasyEGE.DAL.Entities;
using EasyEGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEGE.ViewModels
{
    public class RandomTestViewModel
    {
        public List<int> ProblemIds { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<Problem> Problems { get; set; }
        public int SubjectId { get; set; }
        public string[] Answers { get; set; }

        public RandomTestViewModel()
        {
            Pictures = new List<Picture>();
            Problems = new List<Problem>();
            ProblemIds = new List<int>();
        }
    }
}
