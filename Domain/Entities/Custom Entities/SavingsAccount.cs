using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Custom_Entities;

namespace Domain.Entities
{
    public class SavingsAccount : Account
    {
        public int WithdrawalCount { get; set; } = 0;
        public double InterestRate { get; set; } = 0.025;
       
    }
}
