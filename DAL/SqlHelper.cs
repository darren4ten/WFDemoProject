using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlHelper
    {
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        public static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteDatatable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlDataAdapter ad = new SqlDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    return ds == null ? null : ds.Tables[0];
                }
            }

        }
    }
}
