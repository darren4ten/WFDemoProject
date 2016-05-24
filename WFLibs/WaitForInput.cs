using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Models;
using DAL;

namespace WFLibs
{

    public sealed class WaitForInput : NativeActivity
    {

        public InArgument<string> OperationNames
        {
            get;
            set;
        }

        private string WorkInstId { get; set; }
        public InOutArgument<User> workflowModel { get; set; }

        public string NodeName { get; set; }

        /// <summary>
        /// 创建书签时，必须设置CanInduceIdle为true
        /// </summary>
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context)
        {


            SaveCurrentNodeInfoToDB(context.WorkflowInstanceId);
            context.CreateBookmark("BookmarkTest", new BookmarkCallback(bookmarkCallback));
        }

        void bookmarkCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Dictionary<string, object> dic = value as Dictionary<string, object>;
            //dic.Add("model", value);
            User data = (User)(dic["model"]);
            string approver = dic["curApproveUser"].ToString();
            context.SetValue(workflowModel, data);
            SaveApprovalHistoryToDB(data, approver);
            var dal = new WFCurrentNodeInfoDAL();
            dal.UpdateExitTime(data.WorkflowInstId.ToString(), DateTime.Now);

        }

        void SaveCurrentNodeInfoToDB(Guid uid)
        {
            WFCurrentNodeInfoDAL infoDal = new WFCurrentNodeInfoDAL();
            WFCurrentNodeInfo nodeInfo = new WFCurrentNodeInfo();
            nodeInfo.WFinstId = uid;
            nodeInfo.CurrentNode = NodeName;
            nodeInfo.EnterTime = DateTime.Now;
            nodeInfo.ExitTime = Convert.ToDateTime("1990/1/1");
            infoDal.Add(nodeInfo);
        }

        void SaveApprovalHistoryToDB(User userInfo, string approver)
        {
            //WFinstanceDAL wfDal = new WFinstanceDAL();
            //WFInstance inst = wfDal.Get(userInfo.WorkflowInstId.ToString()).FirstOrDefault();
            //if (inst == null)
            //{
            //    inst = new WFInstance();
            //    inst.WfInstanceId = userInfo.WorkflowInstId;
            //    inst.SubmitTime = DateTime.Now;
            //}

            //inst.State = "审核通过";
            //inst.ApproveTime = DateTime.Now;
            //inst.ApproveUser = approver;
            //inst.CurrentNode = NodeName;
            //wfDal.Add(inst);
            WFinstanceDAL wfDal = new WFinstanceDAL();
            wfDal.Add(userInfo.WorkflowInstId.ToString(), approver,"审核通过", NodeName);

            WFCurrentNodeInfoDAL infoDal = new WFCurrentNodeInfoDAL();
            infoDal.UpdateExitTime(userInfo.WorkflowInstId.ToString(), DateTime.Now);
        }
    }
}
