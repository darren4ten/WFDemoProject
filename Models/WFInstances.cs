using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WFInstances
    {
        [DisplayName("WfInstanceId")]
        public Guid WfInstanceId { get; set; }

        [DisplayName("用户")]
        public string User { get; set; }

        [DisplayName("状态")]
        public string State { get; set; }

        [DisplayName("提交时间")]
        public DateTime SubmitTime { get; set; }

        [DisplayName("审批时间")]
        public DateTime ApproveTime { get; set; }

        [DisplayName("审批用户")]
        public string ApproveUser { get; set; }

        [DisplayName("共享用户")]
        public string ShareUsers { get; set; }

        [DisplayName("节点名称")]
        public string CurrentNode { get; set; }
    }
}
