using EasyEGE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.BLL.Interfaces
{
    public interface ISubjectService
    {
        Subject GetSubject(int? id);
        Subject GetSubject(string name);
    }
}
