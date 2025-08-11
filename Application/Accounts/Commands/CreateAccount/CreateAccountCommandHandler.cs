using Application.Interfaces.Repos;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateAccountCommandHandler(
            IAccountRepository accountRepository,
            IAuditLogRepository auditLogRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _auditLogRepository = auditLogRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Account account;

            if (request.AccountType == AccountType.Savings)
            {
                account = new SavingsAccount
                {
                    AccountNumber = request.AccountNumber,
                    Balance = request.Balance,
                    InterestRate = request.InterestRate,
                    Currency = request.Currency,
                    UserId = request.UserId,
                    AccountType = AccountType.Savings
                };
            }
            else
            {
                account = new CheckingAccount
                {
                    AccountNumber = request.AccountNumber,
                    Balance = request.Balance,
                    DailyLimit = request.DailyLimit,
                    Currency = request.Currency,
                    UserId = request.UserId,
                    AccountType = AccountType.Checking
                };
            }

            await _accountRepository.InsertAccountAsync(account);

            // Get user ID from JWT claims
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

            int userId = 0;
            if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var parsedUserId))
            {
                userId = parsedUserId;
            }
            else
            {
                userId = request.UserId; // fallback, if needed
            }

            // Log audit entry
            await _auditLogRepository.LogAsync(userId, "Create", "Account", $"Account {account.Id} created by user {userId}");

            return account.Id;
        }
    }
}

