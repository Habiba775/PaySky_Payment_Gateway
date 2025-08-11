using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Queries
{
    using MediatR;

    namespace Application.Accounts.Queries
    {
        public class GetAccountBalanceQuery : IRequest<decimal?>
        {
            public int AccountId { get; set; }

            public GetAccountBalanceQuery(int accountId)
            {
                AccountId = accountId;
            }
        }
    }

}
