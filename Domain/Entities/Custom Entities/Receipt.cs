using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;

namespace Domain.Entities.Custom_Entities;

public class Receipt : BaseEntity<int>
{
   
    public int TransactionId { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string FilePath { get; set; }

    public Transaction Transaction { get; set; }
}
