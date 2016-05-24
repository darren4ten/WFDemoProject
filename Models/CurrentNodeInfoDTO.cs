using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CurrentNodeInfoDTO
    {
        public string UID { get; set; }
        public string User { get; set; }
        public string FullName { get; set; }
        public string CurrentNode { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }
    }
}
