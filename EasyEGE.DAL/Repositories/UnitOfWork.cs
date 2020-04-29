using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private OptionRepository optionRepository;
        private PictureRepository pictureRepository;
        private ProblemOptionRepository problemOptionRepository;
        private ProblemRepository problemRepository;
        private SubjectRepository subjectRepository;

        public UnitOfWork(ApplicationContext context)
        {
            db = context;
        }

        public IRepository<Option> Options
        {
            get
            {
                if (optionRepository == null)
                    optionRepository = new OptionRepository(db);
                return optionRepository;
            }
        }

        public IRepository<Problem> Problems
        {
            get
            {
                if (problemRepository == null)
                    problemRepository = new ProblemRepository(db);
                return problemRepository;
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                if (pictureRepository == null)
                    pictureRepository = new PictureRepository(db);
                return pictureRepository;
            }
        }

        public IRepository<ProblemOption> ProblemOptions
        {
            get
            {
                if (problemOptionRepository == null)
                    problemOptionRepository = new ProblemOptionRepository(db);
                return problemOptionRepository;
            }
        }

        public IRepository<Subject> Subject
        {
            get
            {
                if (subjectRepository == null)
                    subjectRepository = new SubjectRepository(db);
                return subjectRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
