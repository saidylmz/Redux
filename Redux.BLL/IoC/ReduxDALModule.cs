using Ninject.Modules;
using Redux.DAL.Abstract;
using Redux.DAL.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.BLL.IoC
{
    public class ReduxDALModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryDAL>().To<CategoryRepository>();
            Bind<ICommentDAL>().To<CommentRepository>();
            Bind<IMediaDAL>().To<MediaRepository>();
            Bind<IModDAL>().To<ModRepository>();
            Bind<IUserDAL>().To<UserRepository>();
        }
    }
}
