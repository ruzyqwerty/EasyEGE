using EasyEGE.DAL.Entities;
using EasyEGE.Models;
using System.Collections.Generic;

namespace EasyEGE.ViewModels
{
    public class AddOptionViewModel
    {
        public string? Subject { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<int> ProblemIds { get; set; }

        public List<Problem> Problems { get; set; }

        public List<Picture> Pictures { get; set; }

        public int? Number { get; set; }

        public AddOptionViewModel()
        {
            ProblemIds = new List<int>();
            Subjects = new List<Subject>();
            Problems = new List<Problem>();
            Pictures = new List<Picture>();
        }
    }
}
