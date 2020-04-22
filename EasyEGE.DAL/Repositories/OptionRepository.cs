using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyEGE.DAL.Repositories
{
    public class OptionRepository : IRepository<Option>
    {
        private ApplicationContext db;

        public OptionRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Option item)
        {
            db.Options.Add(item);
        }

        public void Delete(int id)
        {
            Option option = db.Options.Find(id);
            if (option != null)
                db.Options.Remove(option);
        }

        public IEnumerable<Option> Find(Func<Option, bool> predicate)
        {
            return db.Options.Where(predicate).ToList();
        }

        public Option Get(int id)
        {
            return db.Options.Find(id);
        }

        public IEnumerable<Option> GetAll()
        {
            return db.Options;
        }

        public void Update(Option item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
