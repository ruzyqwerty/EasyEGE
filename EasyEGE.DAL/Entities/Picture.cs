using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.DAL.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public bool IsSolve { get; set; }
        public int ProblemId { get; set; }
        public string Alt { get; set; }

        public Problem Problem { get; set; }
    }
}
