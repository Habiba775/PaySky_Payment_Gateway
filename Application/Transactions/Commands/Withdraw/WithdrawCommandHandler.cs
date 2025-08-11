
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Application.Interfaces.Repositories;
using Domain.Enums;
using Domain.Entities.Custom_Entities;
using MediatR;
using Application.Transactions.Commands.Withdraw;
using Domain.Entities;

namespace Application.Transactions.Commands
{
    public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, int>
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IReceiptRepository _receiptRepo;

        public WithdrawCommandHandler(IAccountRepository accountRepo, ITransactionRepository transactionRepo, IReceiptRepository receiptRepo)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            _receiptRepo = receiptRepo;

        }

        public async Task<int> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepo.GetByIdAsync(request.AccountId);
            if (account == null)
                throw new Exception("Account not found");

            if (request.Amount <= 0)
                throw new ArgumentException("Withdraw amount must be greater than zero.");

            if (account is CheckingAccount checkingAccount)
            {
                var allowedLimit = checkingAccount.Balance + checkingAccount.OverdraftLimit;
                if (request.Amount > allowedLimit)
                    throw new Exception("Insufficient funds (overdraft limit exceeded)");

                checkingAccount.Balance -= request.Amount;
                await _accountRepo.UpdateAsync(checkingAccount);
            }
            else if (account is SavingsAccount savingsAccount)
            {
                if (request.Amount > savingsAccount.Balance)
                    throw new Exception("Insufficient funds in savings account");

                savingsAccount.Balance -= request.Amount;
                await _accountRepo.UpdateAsync(savingsAccount);
            }
            else
            {
                throw new InvalidOperationException("Unsupported account type.");
            }

            var transaction = new Transaction
            {
                Amount = request.Amount,
                transactiontype = TransactionType.Withdraw,
                Timestamp = DateTime.UtcNow,
                CheckingAccountId = account is CheckingAccount ? account.Id : null,
                SavingsAccountId = account is SavingsAccount ? account.Id : null
            };

            await _transactionRepo.InsertAsync(transaction);
            await _transactionRepo.SaveChangesAsync();

            var receipt = new Receipt
            {
                TransactionId = transaction.Id,
                GeneratedAt = DateTime.UtcNow,
                FilePath = $"receipts/{transaction.Id}_{DateTime.UtcNow.Ticks}.pdf"
            };

            await _receiptRepo.InsertAsync(receipt);
            await _receiptRepo.SaveChangesAsync();

            return transaction.Id;
        }
    }
}