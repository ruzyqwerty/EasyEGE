using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.DAL.Entities
{
    public class Problem
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int Number { get; set; }
        public string Answer { get; set; }

        public List<Picture> Pictures { get; set; }
        public Subject Subject { get; set; }
        public List<ProblemOption> ProblemOptions { get; set; }
    }
}
