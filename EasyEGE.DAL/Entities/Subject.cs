using System.Collections.Generic;

namespace EasyEGE.DAL.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxNumberInTest { get; set; }

        public int MaxNumber { get; set; }

        public List<Problem> Problems { get; set; }

        public List<Option> Options { get; set; }
    }
}
