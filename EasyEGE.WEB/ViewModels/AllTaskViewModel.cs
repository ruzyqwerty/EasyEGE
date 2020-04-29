using EasyEGE.DAL.Entities;
using EasyEGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEGE.ViewModels
{
    public class AllTaskViewModel
    {
        public List<Problem> Problems { get; set; }
        public List<Picture> Pictures { get; set; }

        public AllTaskViewModel()
        {
            Problems = new List<Problem>();
            Pictures = new List<Picture>();
        }
    }
}
