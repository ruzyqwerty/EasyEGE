using EasyEGE.BLL.Infrastructure;
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
    public class OptionService : IOptionService
    {
        private IUnitOfWork Database;

        public OptionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddOptionAndLink(Option option, IEnumerable<int> problemsIds)
        {
            Database.Options.Create(option);
            Database.Save();
            foreach (var id in problemsIds)
            {
                var problemOption = new ProblemOption
                {
                    OptionId = option.Id,
                    ProblemId = id
                };
                Database.ProblemOptions.Create(problemOption);
            }
            Database.Save();
        }

        public async Task AddOptionAndLinkAsync(Option option, IEnumerable<int> problemsIds)
        {
            await Database.Options.CreateAsync(option);
            await Database.SaveAsync();
            foreach (var id in problemsIds)
            {
                var problemOption = new ProblemOption
                {
                    OptionId = option.Id,
                    ProblemId = id
                };
                await Database.ProblemOptions.CreateAsync(problemOption);
            }
            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<Option> GetAllUserOptions(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ValidationException("", "");
            var options = Database.Options.Find(o => o.UserId == userId);
            return options;
        }

        public Option GetOption(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан id варианта", "");
            var option = Database.Options.Get(id.Value);
            if (option == null)
                throw new ValidationException("Нет варианта с таким id", "");
            return option;
        }

        public IEnumerable<ProblemOption> GetProblemOptions(int? optionId)
        {
            if (optionId == null)
                throw new ValidationException("Не указан id варианта", "");
            var problemOptions = Database.ProblemOptions.Find(po => po.OptionId == optionId);
            if (problemOptions.Count() == 0)
                throw new ValidationException("Нет связей с вариантом с таким id", "");
            return problemOptions;
        }
    }
}
