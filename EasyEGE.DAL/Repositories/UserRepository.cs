using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private ApplicationContext db;

        public UserRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(User item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
