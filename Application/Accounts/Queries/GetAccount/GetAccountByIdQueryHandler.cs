using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Domain.Entities;
using MediatR;

namespace Application.Accounts.Queries.GetAccount
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Account>
    {
        private readonly IAccountRepository _repository;

        public GetAccountByIdQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByIdAsync(request.Id);


            if (account == null)
            {

                return null;
            }

            return account;
        }
    }
}


