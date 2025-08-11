using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.Commands.Tansfer
{
    using MediatR;

    namespace Application.Transactions.Commands
    {
        public class TransferCommand : IRequest<int>
        {
            public int FromAccountId { get; set; }
            public int ToAccountId { get; set; }
            public decimal Amount { get; set; }
        }
    }

}
