using EasyEGE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.BLL.Interfaces
{
    public interface IPictureService
    {
        void Dispose();
        void AddPicture(Picture picture);
        Task AddPictureAsync(Picture picture);
        IEnumerable<Picture> GetPicturesByProblemId(int? problemId);
        IEnumerable<Picture> GetPictures();
    }
}
