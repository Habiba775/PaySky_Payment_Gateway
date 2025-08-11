using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
    {
        private readonly IAccountRepository _repository;

        public UpdateAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
           
            if (string.IsNullOrWhiteSpace(request.Currency) || request.Currency.Length != 3)
                throw new ArgumentException("Currency must be exactly 3 characters.");

            if (request.DailyLimit < 0)
                throw new ArgumentException("Daily limit must be non-negative.");

            if (request.InterestRate < 0)
                throw new ArgumentException("Interest rate must be non-negative.");

            if (!Enum.IsDefined(typeof(AccountType), request.AccountType))
                throw new ArgumentException("Invalid account type.");

            if (request.UserId <= 0)
                throw new ArgumentException("UserId must be greater than zero.");

            var existingAccount = await _repository.GetByIdAsync(request.AccountId);
            if (existingAccount == null)
                throw new KeyNotFoundException("Account not found.");

            // Update the entity using a lambda
            await _repository.UpdateByIdAsync(request.AccountId, account =>
            {
                account.AccountType = request.AccountType;
                account.Currency = request.Currency;
                account.UserId = request.UserId;
                account.InterestRate = request.InterestRate;
                account.DailyLimit = request.DailyLimit;
            });

            await _repository.SaveChangesAsync();

            return true; 
        }
    }
}



