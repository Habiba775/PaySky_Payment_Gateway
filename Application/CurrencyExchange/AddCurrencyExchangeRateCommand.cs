using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.CurrencyExchange
{
    public class AddCurrencyExchangeRateCommand : IRequest<int>
    {
        public string SourceCurrency { get; set; }  
        public string TargetCurrency { get; set; }  
        public decimal Rate { get; set; }           
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }

}
