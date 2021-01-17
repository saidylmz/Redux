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
    public class CommentService : ICommentService
    {
        ICommentDAL serviceDAL;
        public CommentService(ICommentDAL comDAL)
        {
            serviceDAL = comDAL;
        }
        public void Delete(Comment entity)
        {
            serviceDAL.Remove(entity);
        }

        public void DeleteByID(int entityID)
        {
            serviceDAL.Remove(Get(entityID));
        }

        public Comment Get(int entityID)
        {
            return serviceDAL.Get(x => x.ID == entityID);
        }

        public ICollection<Comment> GetAll()
        {
            return serviceDAL.GetAll();
        }

        public void Insert(Comment entity)
        {
            serviceDAL.Add(entity);
        }

        public void Update(Comment entity)
        {
            serviceDAL.Update(entity);
        }
    }
}
