using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WFCurrentNodeInfo
    {
        public Guid WFinstId { get; set; }
        public string CurrentNode { get; set; }

        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }
    }
}
