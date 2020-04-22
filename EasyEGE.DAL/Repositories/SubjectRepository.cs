using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyEGE.DAL.Repositories
{
    public class SubjectRepository : IRepository<Subject>
    {
        private ApplicationContext db;

        public SubjectRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Subject item)
        {
            db.Subjects.Add(item);
        }

        public void Delete(int id)
        {
            Subject picture = db.Subjects.Find(id);
            if (picture != null)
                db.Subjects.Remove(picture);
        }

        public IEnumerable<Subject> Find(Func<Subject, bool> predicate)
        {
            return db.Subjects.Where(predicate).ToList();
        }

        public Subject Get(int id)
        {
            return db.Subjects.Find(id);
        }

        public IEnumerable<Subject> GetAll()
        {
            return db.Subjects;
        }

        public void Update(Subject item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
