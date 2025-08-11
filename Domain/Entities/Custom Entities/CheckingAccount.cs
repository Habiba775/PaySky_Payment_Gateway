using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Custom_Entities;

namespace Domain.Entities
{
    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; set; } = 500;
       
    }
}
