using EasyEGE.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEGE.WEB.Util
{
    public class SubjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<SubjectService>().To<SubjectService>();
        }
    }
}
