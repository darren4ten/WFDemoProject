using DAL;
using Models;
using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.DurableInstancing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WFLibs
{
    public class WFEngine
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        #region 工作流属性
        WorkflowApplication instance = null;
        SqlWorkflowInstanceStore instanceStore = null;
        InstanceView view = null;
        AutoResetEvent idleEvent = new AutoResetEvent(false);
        #endregion

        public void InitWorkflowApplication()
        {

            instanceStore = new SqlWorkflowInstanceStore(connStr);
            view = instanceStore.Execute(instanceStore.CreateInstanceHandle(), new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));
            instanceStore.DefaultInstanceOwner = view.InstanceOwner;
            instance.InstanceStore = instanceStore;

            instance.Idle = delegate(WorkflowApplicationIdleEventArgs e)
            {
                idleEvent.Set();
            };

            instance.Completed = delegate(WorkflowApplicationCompletedEventArgs e)
            {
                idleEvent.Set();
            };

        }

        public string ExecuteWF(User user, bool reject = false)
        {
            string retMsg = "SuccessWF";
            if (user == null || user.WorkflowInstId == Guid.Empty)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("model", user);
                instance = new WorkflowApplication(new AdminRights(), dic);

                this.InitWorkflowApplication();
                instance.Run();
            }
            else
            {
                AdminRights rights = new AdminRights();
                instance = new WorkflowApplication(rights);
                this.InitWorkflowApplication();
                instance.Load(user.WorkflowInstId);
                string approver = "SuperAdmin";
                if (reject)
                {
                    //更新状态
                    WFinstanceDAL wfDal = new WFinstanceDAL();
                    wfDal.Add(user.WorkflowInstId.ToString(), approver, "驳回");
                    instance.Cancel();
                    retMsg = "驳回成功";
                }
                if (instance.GetBookmarks().Count > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("model", user);
                    dic.Add("curApproveUser", approver);
                    instance.ResumeBookmark("BookmarkTest", dic);
                }

            }

            user.WorkflowInstId = instance.Id;
            //等待工作线程结束
            idleEvent.WaitOne();
            instance.Unload();
            return retMsg;
        }
    }
}
