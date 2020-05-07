using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task CreateAsync(ProblemOption item)
        {
            await db.ProblemOptions.AddAsync(item);
        }

        public void Delete(int id)
        {
            ProblemOption problemOption = db.ProblemOptions.Find(id);
            if (problemOption != null)
                db.ProblemOptions.Remove(problemOption);
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
