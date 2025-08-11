using Xunit;
using Moq;
using Application.Accounts.Commands.CreateAccount;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Application.Interfaces.Repos;

public class CreateAccountCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldInsertAccountAndLogAudit()
    {
        
        var mockAccountRepo = new Mock<IAccountRepository>();
        var mockAuditRepo = new Mock<IAuditLogRepository>();
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("sub", "1") }));
        mockHttpContextAccessor.Setup(_ => _.HttpContext.User).Returns(claimsPrincipal);

        var handler = new CreateAccountCommandHandler(mockAccountRepo.Object, mockAuditRepo.Object, mockHttpContextAccessor.Object);

        var command = new CreateAccountCommand
        {
            AccountType = AccountType.Savings,
            AccountNumber = "123456",
            Balance = 1000,
            InterestRate = 3.5,
            Currency = "USD",
            UserId = 1
        };

       
        var result = await handler.Handle(command, CancellationToken.None);

        
        mockAccountRepo.Verify(r => r.InsertAccountAsync(It.IsAny<Account>()), Times.Once);
        mockAuditRepo.Verify(r => r.LogAsync(1, "Create", "Account", It.IsAny<string>()), Times.Once);
    }
}
