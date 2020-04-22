using System.Collections.Generic;

namespace EasyEGE.DAL.Entities
{
    public class Option
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string UserId { get; set; }

        public Subject Subject { get; set; }

        public User User { get; set; }

        public List<ProblemOption> ProblemOptions { get; set; }
    }
}
