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
    public class PictureRepository : IRepository<Picture>
    {
        private ApplicationContext db;

        public PictureRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(Picture item)
        {
            db.Pictures.Add(item);
            db.SaveChanges();
        }

        public Task CreateAsync(Picture item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture != null)
                db.Pictures.Remove(picture);
            db.SaveChanges();
        }

        public IEnumerable<Picture> Find(Func<Picture, bool> predicate)
        {
            return db.Pictures.Where(predicate).ToList();
        }

        public Picture Get(int id)
        {
            return db.Pictures.Find(id);
        }

        public IEnumerable<Picture> GetAll()
        {
            return db.Pictures;
        }

        public void Update(Picture item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
