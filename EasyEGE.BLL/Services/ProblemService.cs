using EasyEGE.BLL.Infrastructure;
using EasyEGE.BLL.Interfaces;
using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.BLL.Services
{
    public class ProblemService : IProblemService
    {
        private IUnitOfWork Database { get; set; }

        public ProblemService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddProblem(Problem problem)
        {
            Database.Problems.Create(problem);
            Database.Save();
        }

        public Problem GetProblem(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указано id задачи", "");
            var problem = Database.Problems.Get(id.Value);
            if (problem == null)
                throw new ValidationException("Не найдена задача с указанным id", "");
            return problem;
        }

        public IEnumerable<Problem> GetProblemsBySubjectId(int? subjectId)
        {
            if (subjectId == null)
                throw new ValidationException("Не указан id предмета", "");
            var problem = Database.Problems.Find(p => p.SubjectId == subjectId);
            if (problem.Count() == 0)
                throw new ValidationException("Нет заданий по данному предмету", "");
            return problem;
        }

        public void RemoveProblem(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан id задачи", "");
            Database.Problems.Delete(id.Value);
            var pictures = Database.Pictures.Find(p => p.ProblemId == id);
            foreach (var picture in pictures)
            {
                Database.Pictures.Delete(picture.Id);
            }
            Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<Problem> GetProblems()
        {
            return Database.Problems.GetAll();
        }

        public async Task AddProblemAsync(Problem problem)
        {
            Database.Problems.Create(problem);
            await Database.SaveAsync();
        }
    }
}
