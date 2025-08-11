using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Queries
{
    using Domain.Entities;
    using MediatR;
    using System.Collections.Generic;

    namespace Application.Accounts.Queries
    {
        public class GetAllAccountsQuery : IRequest<IEnumerable<Account>>
        {
        }
    }
}
