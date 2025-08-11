using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Accounts.Queries.Application.Accounts.Queries;
using Application.Interfaces.Repos;
using Domain.Entities;
using MediatR;



namespace Application.Accounts.Queries.GetAllAccounts
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<Account>>
    {
        private readonly IAccountRepository _repository;

        public GetAllAccountsQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
