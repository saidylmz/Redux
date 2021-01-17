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
    public class MediaService : IMediaService
    {
        IMediaDAL serviceDAL;
        public MediaService(IMediaDAL mediaDAL)
        {
            serviceDAL = mediaDAL;
        }
        public void Delete(Media entity)
        {
            serviceDAL.Remove(entity);
        }

        public void DeleteByID(int entityID)
        {
            serviceDAL.Remove(Get(entityID));
        }

        public Media Get(int entityID)
        {
            return serviceDAL.Get(x => x.ID == entityID);
        }

        public ICollection<Media> GetAll()
        {
            return serviceDAL.GetAll();
        }

        public void Insert(Media entity)
        {
            serviceDAL.Add(entity);
        }

        public void Update(Media entity)
        {
            serviceDAL.Update(entity);
        }
    }
}
