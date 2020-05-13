using EasyEGE.BLL.Interfaces;
using EasyEGE.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyEGE.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database;

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public string GetUserNameById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new Exception("Не указан id пользователя");
            var user = Database.Users.Find(u => u.Id == id).FirstOrDefault();
            return user.Name;
        }
    }
}
