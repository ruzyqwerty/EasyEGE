using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.BLL.DTO
{
    public class PictureDTO
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public bool IsSolve { get; set; }
        public int ProblemId { get; set; }
        public string Alt { get; set; }
    }
}
