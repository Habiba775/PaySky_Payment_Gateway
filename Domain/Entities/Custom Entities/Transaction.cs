using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities.Custom_Entities;

public class Transaction : BaseEntity<int>
{
    
    //public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public  TransactionType transactiontype { get; set; } 
    public DateTime Timestamp { get; set; }
    public int ReceiptId { get; set; }
    public Receipt? Receipt { get; set; }
    public int? CheckingAccountId { get; set; }
    public CheckingAccount? CheckingAccount { get; set; }

    public int? SavingsAccountId { get; set; }
    public SavingsAccount? SavingsAccount { get; set; }
   
}
