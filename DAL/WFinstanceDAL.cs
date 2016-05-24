using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WFinstanceDAL
    {
        public int Add(WFInstance inst)
        {
            return SqlHelper.ExecuteNonQuery(string.Format("insert into [WFInstances] values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');", inst.WfInstanceId, inst.User, inst.State, inst.SubmitTime.ToString(), inst.ApproveTime.ToString(), inst.ApproveUser, inst.ShareUsers, inst.CurrentNode));
        }

        public int Update(WFInstance inst)
        {
            string sql = string.Format("update WFInstances set State='{0}',ApproveTime='{1}',ApproveUser='{2}' where WfInstanceId='{3}'", inst.State, inst.ApproveTime, inst.ApproveUser, inst.WfInstanceId);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public List<WFInstance> Get(string id = "")
        {

            string sql = "select * from [WFInstances] ";
            if (!string.IsNullOrEmpty(id))
            {
                sql += " where WfInstanceId='" + Guid.Parse(id) + "'";
            }
            sql += " order by ApproveTime desc";
            DataTable dt = SqlHelper.ExecuteDatatable(sql);
            List<WFInstance> list = new List<WFInstance>();
            foreach (DataRow item in dt.Rows)
            {
                WFInstance inst = new WFInstance();
                inst.WfInstanceId = Guid.Parse(item[0].ToString());
                inst.User = Convert.ToString(item[1]);
                inst.State = Convert.ToString(item[2]);
                inst.SubmitTime = Convert.ToDateTime(item[3]);
                inst.ApproveTime = Convert.ToDateTime(item[4]);
                inst.ApproveUser = Convert.ToString(item[5]);
                inst.ShareUsers = Convert.ToString(item[6]);
                inst.CurrentNode = Convert.ToString(item[7]);
                list.Add(inst);
            }
            return list;
        }
        /// <summary>
        /// 添加新的历史记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="approver"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public int Add(string uid, string optUser, string state, string nodeName = "")
        {
            WFinstanceDAL wfDal = new WFinstanceDAL();
            WFInstance inst = wfDal.Get(uid).FirstOrDefault();
            if (inst == null)
            {
                inst = new WFInstance();
                inst.WfInstanceId = Guid.Parse(uid);
                inst.SubmitTime = DateTime.Now;
            }

            inst.State = state;
            inst.ApproveTime = DateTime.Now;
            inst.ApproveUser = optUser;
            inst.CurrentNode = string.IsNullOrEmpty(nodeName) ? inst.CurrentNode : nodeName;
            return wfDal.Add(inst);
        }
    }
}
