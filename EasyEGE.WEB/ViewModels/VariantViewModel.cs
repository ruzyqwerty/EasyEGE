using EasyEGE.DAL.Entities;
using EasyEGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEGE.ViewModels
{
    public class VariantViewModel
    {
        public List<Picture> Pictures { get; set; }
        public List<Problem> Problems { get; set; }
        public int SubjectId { get; set; }

        public string UserName { get; set; }

        public VariantViewModel()
        {
            Pictures = new List<Picture>();
            Problems = new List<Problem>();
        }
    }
}
