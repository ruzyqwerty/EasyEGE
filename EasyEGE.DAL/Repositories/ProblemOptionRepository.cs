using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyEGE.DAL.Repositories
{
    public class ProblemOptionRepository : IRepository<ProblemOption>
    {
        private ApplicationContext db;

        public ProblemOptionRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(ProblemOption item)
        {
            db.ProblemOptions.Add(item);
        }

        public void Delete(int id)
        {
            ProblemOption picture = db.ProblemOptions.Find(id);
            if (picture != null)
                db.ProblemOptions.Remove(picture);
        }

        public IEnumerable<ProblemOption> Find(Func<ProblemOption, bool> predicate)
        {
            return db.ProblemOptions.Where(predicate).ToList();
        }

        public ProblemOption Get(int id)
        {
            return db.ProblemOptions.Find(id);
        }

        public IEnumerable<ProblemOption> GetAll()
        {
            return db.ProblemOptions;
        }

        public void Update(ProblemOption item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
