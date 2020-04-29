using EasyEGE.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace EasyEGE.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Problem> Problems { get; }
        IRepository<Option> Options { get; }
        IRepository<Picture> Pictures { get; }
        IRepository<ProblemOption> ProblemOptions { get; }
        IRepository<Subject> Subject { get; }
        void Save();
        Task SaveAsync();
    }
}
