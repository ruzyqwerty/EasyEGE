using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.BLL.Interfaces
{
    public interface IUserService
    {
        void Dispose();
        string GetUserNameById(string id);
    }
}
