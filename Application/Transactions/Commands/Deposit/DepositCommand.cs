using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Transactions.Commands.Deposit
{
    public class DepositCommand : IRequest<int>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
