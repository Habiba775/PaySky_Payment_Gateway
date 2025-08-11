using Application.Interfaces.Repos;
using Application.Interfaces.Repositories;
using Application.Transactions.Commands.Tansfer.Application.Transactions.Commands;
using Domain.Entities;
using Domain.Entities.Custom_Entities;
using Domain.Enums;
using MediatR;

public class TransferCommandHandler : IRequestHandler<TransferCommand, int>
{
    private readonly IAccountRepository _accountRepo;
    private readonly ITransactionRepository _transactionRepo;
    private readonly IReceiptRepository _receiptRepo;
    private readonly ICurrencyExchangeRepository _currencyExchangeRepo;

    public TransferCommandHandler(
        IAccountRepository accountRepo,
        ITransactionRepository transactionRepo,
        IReceiptRepository receiptRepo,
        ICurrencyExchangeRepository currencyExchangeRepo)
    {
        _accountRepo = accountRepo;
        _transactionRepo = transactionRepo;
        _receiptRepo = receiptRepo;
        _currencyExchangeRepo = currencyExchangeRepo;
    }

    public async Task<int> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        var fromAccount = await _accountRepo.GetByIdAsync(request.FromAccountId);
        var toAccount = await _accountRepo.GetByIdAsync(request.ToAccountId);

        if (fromAccount == null || toAccount == null)
            throw new Exception("One or both accounts not found");

        if (request.Amount <= 0)
            throw new ArgumentException("Transfer amount must be greater than zero.");

       
        decimal amountToCredit = request.Amount;

        if (!string.Equals(fromAccount.Currency, toAccount.Currency, StringComparison.OrdinalIgnoreCase))
        {
            var exchangeRate = await _currencyExchangeRepo.GetRateAsync(
                fromAccount.Currency.ToUpper(),
                toAccount.Currency.ToUpper()
            );

            if (exchangeRate == null)
                throw new Exception($"No exchange rate found for {fromAccount.Currency} → {toAccount.Currency}");

            amountToCredit = request.Amount * (decimal)exchangeRate.Rate;
        }

       
        if (fromAccount is CheckingAccount checkingAccount)
        {
            var allowedLimit = checkingAccount.Balance + checkingAccount.OverdraftLimit;
            if (request.Amount > allowedLimit)
                throw new Exception("Insufficient funds (overdraft limit exceeded)");

            checkingAccount.Balance -= request.Amount;
            await _accountRepo.UpdateAsync(checkingAccount);
        }
        else if (fromAccount is SavingsAccount savingsAccount)
        {
            if (request.Amount > savingsAccount.Balance)
                throw new Exception("Insufficient funds in savings account");

            savingsAccount.Balance -= request.Amount;
            await _accountRepo.UpdateAsync(savingsAccount);
        }
        else
        {
            throw new InvalidOperationException("Unsupported account type for transfer.");
        }

        
        toAccount.Balance += amountToCredit;
        await _accountRepo.UpdateByIdAsync(toAccount.Id, acc => acc.Balance = toAccount.Balance);

        
        var transaction = new Transaction
        {
            Amount = request.Amount, 
            transactiontype = TransactionType.Transfer,
            Timestamp = DateTime.UtcNow,
            CheckingAccountId = fromAccount is CheckingAccount ? fromAccount.Id : null,
            SavingsAccountId = fromAccount is SavingsAccount ? fromAccount.Id : null
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

