using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using MediatR;

namespace Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly IAccountRepository _repository;

        public DeleteAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteByIdAsync(request.AccountId);
            return true;
        }
    }
}
