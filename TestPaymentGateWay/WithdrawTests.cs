using Domain.Entities;
using Domain.Entities.Custom_Entities;
using Application.Transactions.Commands.Withdraw;
using Application.Interfaces.Repos;
using Application.Transactions.Commands;
using Moq;
using Application.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class WithdrawTests
{
    [Fact]
    public async Task WithdrawCommand_ShouldDecreaseBalance()
    {
      
        var mockAccountRepo = new Mock<IAccountRepository>();
        var mockTransactionRepo = new Mock<ITransactionRepository>();
        var mockReceiptRepo = new Mock<IReceiptRepository>();

        var account = new CheckingAccount { Balance = 200m };
        mockAccountRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(account);

        var handler = new WithdrawCommandHandler(
            mockAccountRepo.Object,
            mockTransactionRepo.Object,
            mockReceiptRepo.Object
        );

        var command = new WithdrawCommand { AccountId = 1, Amount = 50m };

        
        await handler.Handle(command, CancellationToken.None);

        
        Assert.Equal(150m, account.Balance);
        mockAccountRepo.Verify(r => r.UpdateAsync(account), Times.Once);
     
    }
}
