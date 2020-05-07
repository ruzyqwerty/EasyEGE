using EasyEGE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyEGE.BLL.Interfaces
{
    public interface IOptionService
    {
        void AddOptionAndLink(Option option, IEnumerable<int> problemsIds);
        Task AddOptionAndLinkAsync(Option option, IEnumerable<int> problemsIds);
        Option GetOption(int? id);
        void Dispose();
        IEnumerable<ProblemOption> GetProblemOptions(int? optionId);
    }
}
