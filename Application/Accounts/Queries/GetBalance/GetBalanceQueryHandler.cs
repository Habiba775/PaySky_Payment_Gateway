using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Accounts.Queries.Application.Accounts.Queries;
using Application.Interfaces.Repos;
using Domain.Entities;
using MediatR;

namespace Application.Accounts.Queries.GetBalance
{
    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, decimal?>
    {
        private readonly IAccountRepository _repository;

        public GetAccountBalanceQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal?> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBalanceByIdAsync(request.AccountId);
        }
    }
}
