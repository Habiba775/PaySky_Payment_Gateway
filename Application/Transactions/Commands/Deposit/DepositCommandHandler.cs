using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Application.Interfaces.Repositories;
using Domain.Enums;
using Domain.Entities.Custom_Entities;
using MediatR;

namespace Application.Transactions.Commands.Deposit
{
    public class DepositCommandHandler : IRequestHandler<DepositCommand, int>
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IReceiptRepository _receiptRepo;

        public DepositCommandHandler(IAccountRepository accountRepo, ITransactionRepository transactionRepo, IReceiptRepository receiptRepo)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            _receiptRepo = receiptRepo;
        }

        public async Task<int> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepo.GetByIdAsync(request.AccountId);
            if (account == null)
                throw new Exception("Account not found");


            account.Balance += request.Amount;
            await _accountRepo.UpdateByIdAsync(request.AccountId, acc => acc.Balance = account.Balance);


            var transaction = new Transaction
            {
                Amount = request.Amount,
                transactiontype = TransactionType.Deposit,
                Timestamp = DateTime.UtcNow
            };


            switch (account.AccountType)
            {
                case AccountType.Checking:
                    transaction.CheckingAccountId = account.Id;
                    break;
                case AccountType.Savings:
                    transaction.SavingsAccountId = account.Id;
                    break;
                default:
                    throw new Exception("Unsupported account type");
            }

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

