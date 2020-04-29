using EasyEGE.BLL.Infrastructure;
using EasyEGE.BLL.Interfaces;
using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        public IUnitOfWork Database { get; set; }

        public SubjectService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<string> GetSubjects()
        {
            return (from Subject in Database.Subject.GetAll()
                    select Subject.Name);
        }

        public Subject GetSubject(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан id предмета","");
            var subject = Database.Subject.Get(id.Value);
            if (subject == null)
                throw new ValidationException("Не найден предмет с указанным id", "");
            return subject;
        }

        public Subject GetSubject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Не указано название предмета", "");
            var subject = Database.Subject.Find(s => s.Name == name).FirstOrDefault();
            if (subject == null)
                throw new ValidationException("Не найден предмет с указанным названием", "");
            return subject;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void AddProblem(Problem problem)
        {
            Database.Problems.Create(problem);
            Database.SaveAsync();
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

        public void AddPicture(Picture picture)
        {
            var IsExist = Database.Pictures.Find(p => p.Content == picture.Content && p.Alt == picture.Alt && p.IsSolve == picture.IsSolve);
            if (IsExist.Count() == 0)
            {
                Database.Pictures.Create(picture);
                Database.SaveAsync();
            }
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
        
        public IEnumerable<Picture> GetPictures(int? problemId)
        {
            if (problemId == null)
                throw new ValidationException("Не указан id задачи", "");
            var pictures = Database.Pictures.Find(p => p.ProblemId == problemId);
            return pictures;
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

        public void AddOptionAndLink(Option option, IEnumerable<int> problemsIds)
        {
            Database.Options.Create(option);
            foreach (var id in problemsIds)
            {
                var problemOption = new ProblemOption
                {
                    OptionId = option.Id,
                    ProblemId = id
                };
                Database.ProblemOptions.Create(problemOption);
            }
            Database.SaveAsync();
        }

        public Option GetOption(int? id)
        {
            if (id == null)
                throw new ValidationException("", "");
            var option = Database.Options.Get(id.Value);
            if (option == null)
                throw new ValidationException("", "");
            return option;
        }

        public IEnumerable<ProblemOption> GetProblemOptions(int? optionId)
        {
            if (optionId == null)
                throw new ValidationException("", "");
            var problemOptions = Database.ProblemOptions.Find(po => po.OptionId == optionId);
            if (problemOptions.Count() == 0)
                throw new ValidationException("", "");
            return problemOptions;
        }
    }
}
