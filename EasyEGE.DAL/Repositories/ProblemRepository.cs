using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyEGE.DAL.Repositories
{
    public class ProblemRepository : IRepository<Problem>
    {
        private ApplicationContext db;

        public ProblemRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Problem item)
        {
            db.Problems.Add(item);
        }

        public void Delete(int id)
        {
            Problem picture = db.Problems.Find(id);
            if (picture != null)
                db.Problems.Remove(picture);
        }

        public IEnumerable<Problem> Find(Func<Problem, bool> predicate)
        {
            return db.Problems.Where(predicate).ToList();
        }

        public Problem Get(int id)
        {
            return db.Problems.Find(id);
        }

        public IEnumerable<Problem> GetAll()
        {
            return db.Problems;
        }

        public void Update(Problem item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
