using EasyEGE.DAL.Entities;
using EasyEGE.DAL.Interfaces;
using EasyEGE.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyEGE.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private DbContextOptions<ApplicationContext> _options;
        public ServiceModule(DbContextOptions<ApplicationContext> options)
        {
            _options = options;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_options);
        }
    }
}
