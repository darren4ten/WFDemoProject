using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlSugarDao
    {
        private static SqlSugarClient _instance = null;
        private SqlSugarDao()
        {

        }

        public static SqlSugarClient GetInstance()
        {
            if (_instance == null)
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings[@"connStr"].ToString(); //这里可以动态根据cookies或session实现多库切换
                _instance = new SqlSugarClient(connection);
            }
            return _instance;
        }
    }
}
