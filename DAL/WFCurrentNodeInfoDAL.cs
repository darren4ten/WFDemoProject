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
    public class WFCurrentNodeInfoDAL
    {
        private SqlSugarClient _sqlClient = SqlSugarDao.GetInstance();
        public WFCurrentNodeInfo Add(WFCurrentNodeInfo info)
        {
            var obj = _sqlClient.Insert(info);
            return obj == null ? null : obj as WFCurrentNodeInfo;
        }

        public bool UpdateExitTime(string uid, DateTime exitTime)
        {
            //string sql = string.Format("update [WFCurrentNodeInfo] set ExitTime='{0}' where WFinstId='{1}'", exitTime, uid);
            //return SqlHelper.ExecuteNonQuery(sql);
            return _sqlClient.Update<WFCurrentNodeInfo>(new { ExitTime = exitTime }, p => p.WFinstId.ToString() == uid);
        }

        public Queryable<WFCurrentNodeInfo> Get(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return _sqlClient.Queryable<WFCurrentNodeInfo>();
            }
            else
            {
                return _sqlClient.Queryable<WFCurrentNodeInfo>().Where(p => p.WFinstId.ToString() == id);
            }
        }

        public Queryable<WFCurrentNodeInfo> GetAllPendingNodes(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return _sqlClient.Queryable<WFCurrentNodeInfo>();
            }
            else
            {
                return _sqlClient.Queryable<WFCurrentNodeInfo>().Where(p => p.WFinstId.ToString() == id);
            }
        }

        /// <summary>
        /// 获取当前节点信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CurrentNodeInfoDTO> GetCurrentPendingNodesDTO(string id = "")
        {
            string sql = "select * from [vwCurrentNodeAndUserInfo] ";
            if (!string.IsNullOrEmpty(id))
            {
                sql += " where WFinstId='" + id + "' and ExitTime<'1990/1/2'";
            }
            else
            {
                sql += " where ExitTime<'1990/1/2'";
            }

            return _sqlClient.GetList<CurrentNodeInfoDTO>(sql);

        }


    }
}
