using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public int ID { get; set; }

        [DisplayName("账号名称")]
        public string Name { get; set; }

        [DisplayName("年龄")]
        public int Age { get; set; }
        [DisplayName("部门")]
        public string Department { get; set; }

        [DisplayName("全名")]
        public string FullName { get; set; }

        public Guid WorkflowInstId { get; set; }

    }
}
