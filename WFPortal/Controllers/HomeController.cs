using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFPortal.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult ApprovingList()
        {
            var dal = new WFCurrentNodeInfoDAL();
            var result = dal.GetCurrentPendingNodesDTO();

            return View(result);
        }

        public ActionResult ApprovalHistoryList()
        {
            var dal = new WFinstanceDAL();
            return View(dal.Get());
        }
    }
}
