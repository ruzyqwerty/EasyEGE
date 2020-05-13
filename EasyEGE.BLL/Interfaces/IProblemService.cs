using EasyEGE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.BLL.Interfaces
{
    public interface IProblemService
    {
        void AddProblem(Problem problem);
        Task AddProblemAsync(Problem problem);
        Problem GetProblem(int? id);
        IEnumerable<Problem> GetProblemsBySubjectId(int? subjectId);
        void RemoveProblem(int? id);
        void Dispose();
        IEnumerable<Problem> GetProblems();
    }
}
