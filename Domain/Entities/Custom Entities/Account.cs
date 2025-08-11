using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Entities;
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Custom_Entities;

namespace Domain.Entities
{
    public abstract class Account:BaseEntity<int>

    {
        
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        
        public string Currency { get; set; } 
        public double InterestRate { get; set; } = 0.0;
        public decimal DailyLimit { get; set; } = 0;
        public int UserId { get; set; }

        
        public APPUser User { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public string Type => AccountType.ToString();
    }

}

