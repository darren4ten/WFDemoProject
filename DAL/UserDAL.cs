using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        private SqlSugarClient _sqlClient = SqlSugarDao.GetInstance();
        public User Add(User user)
        {
            int id = Convert.ToInt32(_sqlClient.Insert(user));
            user.ID = id;
            return user;
        }

        public bool Update(User user)
        {
            return _sqlClient.Update<User>(user, p => p.ID == user.ID);
        }

        public List<User> Get(int id = -1)
        {
            if (id == -1)
            {
                return _sqlClient.Queryable<User>().ToList();
            }
            else
            {
                return _sqlClient.Queryable<User>().Where(p => p.ID == id).ToList();
            }
        }

        public Queryable<User> GetByWfId(string uid)
        {
            return _sqlClient.Queryable<User>().Where(p => p.WorkflowInstId.ToString() == uid);
        }
    }
}
