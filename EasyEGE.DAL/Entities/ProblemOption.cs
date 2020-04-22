using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.DAL.Entities
{
    public class ProblemOption
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }

        public int? OptionId { get; set; }

        public Problem Problem { get; set; }
        public Option Options { get; set; }
    }
}
