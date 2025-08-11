using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;



    namespace Application.Transactions.Commands.Withdraw
    {
        public class WithdrawCommand : IRequest<int>
        {
            public int AccountId { get; set; }
            public decimal Amount { get; set; }
        }
    }

