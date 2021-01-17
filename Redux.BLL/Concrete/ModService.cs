using Redux.BLL.Abstract;
using Redux.DAL.Abstract;
using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.BLL.Concrete
{
    public class ModService : IModService
    {
        IModDAL serviceDAL;
        public ModService(IModDAL modDAL)
        {
            serviceDAL = modDAL;
        }
        public void Delete(Mod entity)
        {
            serviceDAL.Remove(entity);
        }

        public void DeleteByID(int entityID)
        {
            serviceDAL.Remove(Get(entityID));
        }

        public Mod Get(int entityID)
        {
            return serviceDAL.Get(x => x.ID == entityID);
        }

        public ICollection<Mod> GetAll()
        {
            return serviceDAL.GetAll();
        }

        public void Insert(Mod entity)
        {
            serviceDAL.Add(entity);
        }

        public void Update(Mod entity)
        {
            serviceDAL.Update(entity);
        }
    }
}
