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
    public class WFinstanceDAL
    {
        private SqlSugarClient _sqlClient = SqlSugarDao.GetInstance();
        public WFInstances Add(WFInstances inst)
        {
            object obj = _sqlClient.Insert<WFInstances>(inst);
            return obj as WFInstances;
        }

        public bool Update(WFInstances inst)
        {
            return _sqlClient.Update<WFInstances>(inst, p => p.WfInstanceId == inst.WfInstanceId);
        }

        public List<WFInstances> Get(string id = "")
        {
            var instances = _sqlClient.Queryable<WFInstances>();
            if (!string.IsNullOrEmpty(id))
            {
                instances = instances.Where(p => p.WfInstanceId.ToString() == id);

            }
            return instances.OrderBy(p => p.ApproveTime, OrderByType.desc).ToList();
        }

        /// <summary>
        /// 添加新的历史记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="approver"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public WFInstances Add(string uid, string optUser, string state, string nodeName = "")
        {
            WFinstanceDAL wfDal = new WFinstanceDAL();
            WFInstances inst = wfDal.Get(uid).FirstOrDefault();
            if (inst == null)
            {
                inst = new WFInstances();
                inst.WfInstanceId = Guid.Parse(uid);
                inst.SubmitTime = DateTime.Now;
                inst.User = new UserDAL().GetByWfId(uid).FirstOrDefault().Name;
            }
            //inst.User = new UserDAL().;
            inst.State = state;
            inst.ApproveTime = DateTime.Now;
            inst.ApproveUser = optUser;
            inst.CurrentNode = string.IsNullOrEmpty(nodeName) ? inst.CurrentNode : nodeName;
            return wfDal.Add(inst);
        }
    }
}
