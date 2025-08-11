using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;

namespace Domain.Entities.Custom_Entities
{
    public class AuditLog : BaseEntity<int>
    {
        public int UserId { get; set; }

        public APPUser User { get; set; }
       
        public string Action { get; set; }
        public string Entity { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }

        
    }

}
