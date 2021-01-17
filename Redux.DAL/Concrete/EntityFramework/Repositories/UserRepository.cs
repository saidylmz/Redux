using Redux.CORE.DAL.EntityFramework;
using Redux.DAL.Abstract;
using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete.EntityFramework.Repositories
{
    public class UserRepository : EFRepositoryBase<User, ReduxDbContext>, IUserDAL
    {
    }
}
