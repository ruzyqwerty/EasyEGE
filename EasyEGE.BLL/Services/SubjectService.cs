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
        private IUnitOfWork Database { get; set; }

        public SubjectService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<string> GetSubjectsNames()
        {
            return (from Subject in Database.Subjects.GetAll()
                    select Subject.Name);
        }

        public Subject GetSubject(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан id предмета","");
            var subject = Database.Subjects.Get(id.Value);
            if (subject == null)
                throw new ValidationException($"Не найден предмет с указанным id - {id}", "");
            return subject;
        }

        public Subject GetSubject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Не указано название предмета", "");
            var subject = Database.Subjects.Find(s => s.Name == name).FirstOrDefault();
            if (subject == null)
                throw new ValidationException("Не найден предмет с указанным названием", "");
            return subject;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        IEnumerable<Subject> ISubjectService.GetSubjects()
        {
            return Database.Subjects.GetAll();
        }
    }
}
