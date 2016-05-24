using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WFCurrentNodeInfoDAL
    {
        public int Add(WFCurrentNodeInfo info)
        {
            return SqlHelper.ExecuteNonQuery(string.Format("insert into [WFCurrentNodeInfo] values('{0}','{1}','{2}','{3}');", info.WFinstId, info.CurrentNode, info.EnterTime, info.ExitTime));

        }

        public int UpdateExitTime(string uid, DateTime exitTime)
        {
            string sql = string.Format("update [WFCurrentNodeInfo] set ExitTime='{0}' where WFinstId='{1}'", exitTime, uid);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public List<WFCurrentNodeInfo> Get(int id = -1)
        {
            string sql = "select * from [WFCurrentNodeInfo] ";
            if (id > -1)
            {
                sql += " where WFinstId='" + id + "'";
            }

            DataTable dt = SqlHelper.ExecuteDatatable(sql);
            List<WFCurrentNodeInfo> list = new List<WFCurrentNodeInfo>();
            foreach (DataRow item in dt.Rows)
            {
                WFCurrentNodeInfo info = new WFCurrentNodeInfo();
                info.WFinstId = Guid.Parse(item[0].ToString());
                info.CurrentNode = item[1] as string;
                info.EnterTime = Convert.ToDateTime(item[2]);
                info.ExitTime = Convert.ToDateTime(item[3]);

                list.Add(info);
            }
            return list;

        }

        public List<WFCurrentNodeInfo> GetAllPendingNodes(int id = -1)
        {
            string sql = "select * from [WFCurrentNodeInfo] ";
            if (id > -1)
            {
                sql += " where WFinstId='" + id + "' and ExitTime<'1990/1/2'";
            }
            else
            {
                sql += " where ExitTime<'1990/1/2'";
            }

            DataTable dt = SqlHelper.ExecuteDatatable(sql);
            List<WFCurrentNodeInfo> list = new List<WFCurrentNodeInfo>();
            foreach (DataRow item in dt.Rows)
            {
                WFCurrentNodeInfo info = new WFCurrentNodeInfo();
                info.WFinstId = Guid.Parse(item[0].ToString());
                info.CurrentNode = item[1] as string;
                info.EnterTime = Convert.ToDateTime(item[2]);
                info.ExitTime = Convert.ToDateTime(item[3]);

                list.Add(info);
            }
            return list;

        }

        public List<CurrentNodeInfoDTO> GetCurrentPendingNodesDTO(int id = -1)
        {
            string sql = "select * from [vwCurrentNodeAndUserInfo] ";
            if (id > -1)
            {
                sql += " where WFinstId='" + id + "' and ExitTime<'1990/1/2'";
            }
            else
            {
                sql += " where ExitTime<'1990/1/2'";
            }

            DataTable dt = SqlHelper.ExecuteDatatable(sql);
            List<CurrentNodeInfoDTO> list = new List<CurrentNodeInfoDTO>();
            foreach (DataRow item in dt.Rows)
            {
                CurrentNodeInfoDTO info = new CurrentNodeInfoDTO();
                info.UID = item[0].ToString();
                info.CurrentNode = item[1] as string;
                info.EnterTime = Convert.ToDateTime(item[2]);
                info.ExitTime = Convert.ToDateTime(item[3]);
                info.User = Convert.ToString(item[4]);
                info.FullName = item[5].ToString();
                list.Add(info);
            }
            return list;

        }


    }
}
