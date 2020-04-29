using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.BLL.DTO
{
    public class ProblemDTO
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int Number { get; set; }
        public string Answer { get; set; }
    }
}
