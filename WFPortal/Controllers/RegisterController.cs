using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFLibs;

namespace WFPortal.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id = -1)
        {
            if (id == -1)
            {
                return View(new User());
            }
            else
            {
                var userDAL = new UserDAL();
                User user = userDAL.Get(id).FirstOrDefault();
                return View(user);
            }

        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            var userDAL = new UserDAL();
            //新增

            if (user.ID <1)
            {
                user = userDAL.Add(user);
                var engine = new WFEngine();
                engine.ExecuteWF(user);
                //更新wfid
                userDAL.Update(user);
            }
            else
            {
                //编辑
                userDAL.Update(user);
            }
           
            Redirect("~/Register/Edit");
            return View();
        }


        public JsonResult Approve(string wfInstId)
        {
            var engine = new WFEngine();
            var userDal = new UserDAL();
            User user = userDal.Get().Where(p => p.WorkflowInstId == Guid.Parse(wfInstId)).FirstOrDefault();
            string nodeName = engine.ExecuteWF(user);
            RetMsg msg = new RetMsg();
            if (string.IsNullOrEmpty(nodeName))
            {
                msg.Code = -1;
                msg.Msg = "流程" + wfInstId + "执行失败!";
            }
            else
            {
                msg.Code = 0;
                msg.Msg = "流程" + wfInstId + "执行成功！当前节点" + nodeName;
                msg.Tag = nodeName;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Reject(string wfInstId)
        {
            var engine = new WFEngine();
            var userDal = new UserDAL();
            User user = userDal.Get().Where(p => p.WorkflowInstId == Guid.Parse(wfInstId)).FirstOrDefault();
            string ret = engine.ExecuteWF(user, true);
            RetMsg msg = new RetMsg();
            if (string.IsNullOrEmpty(ret))
            {
                msg.Code = -1;
                msg.Msg = "流程" + wfInstId + "驳回失败!";
            }
            else
            {
                msg.Code = 0;
                msg.Msg = "流程" + wfInstId + "驳回成功！当前节点" + ret;
                msg.Tag = ret;
                //WFinstanceDAL wfDal = new WFinstanceDAL();
                //WFInstance inst = new WFInstance();
                //inst.WfInstanceId = Guid.Parse(wfInstId);
                //inst.State = nodeName + "，驳回";
                //inst.ApproveTime = DateTime.Now;
                //inst.ApproveUser = "SuperAdmin";
                //wfDal.Update(inst);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
    }
}
