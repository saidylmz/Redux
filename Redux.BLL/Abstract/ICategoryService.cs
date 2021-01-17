using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.BLL.Abstract
{
    public interface ICategoryService : IBaseService<Category>
    {
        Category GetByName(string name);
        Category GetByName(string name, bool ignoreCase);
    }
}
