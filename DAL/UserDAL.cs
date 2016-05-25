using Models;
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
        public int Add(User user)
        {
            return SqlHelper.ExecuteNonQuery(string.Format("insert into [User](Name,Age,FullName,Department) values('{0}',{1},'{2}','{3}');", user.Name, user.Age, user.FullName, user.Department));
        }

        public int Update(User user)
        {
            string sql = string.Format("update [User] set Name='{0}',Age={1},FullName='{2}',Department='{3}',WorkflowInstId='{4}' where id={5}", user.Name, user.Age, user.FullName, user.Department, user.WorkflowInstId, user.ID);
            return SqlHelper.ExecuteNonQuery(sql);
        }
        public List<User> Get(int id = -1)
        {
            string sql = "select * from [user] ";
            if (id > -1)
            {
                sql += " where Id=" + id;
            }

            DataTable dt = SqlHelper.ExecuteDatatable(sql);
            List<User> list = new List<User>();
            foreach (DataRow item in dt.Rows)
            {
                User user = new User();
                user.ID = Convert.ToInt32(item[0]);
                user.Name = item[1] as string;
                user.Age = Convert.ToInt32(item[2]);
                user.FullName = Convert.ToString(item[3]);
                user.Department = Convert.ToString(item[4]);
                if (item[5] != null && !string.IsNullOrEmpty(Convert.ToString(item[5])))
                {
                    user.WorkflowInstId = Guid.Parse(Convert.ToString(item[5]));
                }

                list.Add(user);
            }
            return list;

        }

        public List<User> GetByWfId(string uid)
        {
            string sql = "select * from [user]  where WorkflowInstId='" + uid + "'";
         
            DataTable dt = SqlHelper.ExecuteDatatable(sql);
            List<User> list = new List<User>();
            foreach (DataRow item in dt.Rows)
            {
                User user = new User();
                user.ID = Convert.ToInt32(item[0]);
                user.Name = item[1] as string;
                user.Age = Convert.ToInt32(item[2]);
                user.FullName = Convert.ToString(item[3]);
                user.Department = Convert.ToString(item[4]);
                if (item[5] != null && !string.IsNullOrEmpty(Convert.ToString(item[5])))
                {
                    user.WorkflowInstId = Guid.Parse(Convert.ToString(item[5]));
                }

                list.Add(user);
            }
            return list;

        }
    }
}
