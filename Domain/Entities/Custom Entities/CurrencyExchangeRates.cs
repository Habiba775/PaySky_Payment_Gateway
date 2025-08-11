using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;

namespace Domain.Entities.Custom_Entities
{
    public class CurrencyExchangeRate : BaseEntity<int>
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}



