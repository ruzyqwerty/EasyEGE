using EasyEGE.BLL.Interfaces;
using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.BLL.Services
{
    public class PictureService : IPictureService
    {
        private IUnitOfWork Database { get; set; }

        public PictureService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddPictureAsync(Picture picture)
        {
            Database.Pictures.Create(picture);
            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<Picture> GetPicturesByProblemId(int? problemId)
        {
            if (problemId == null)
                throw new Exception("Не указан id задачи");
            var pictures = Database.Pictures.Find(p => p.ProblemId == problemId);
            return pictures;
        }

        public IEnumerable<Picture> GetPictures()
        {
            return Database.Pictures.GetAll();
        }

        public void AddPicture(Picture picture)
        {
            Database.Pictures.Create(picture);
            Database.Save();
        }
    }
}
