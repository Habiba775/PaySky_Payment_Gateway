using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Application.CurrencyExchange;
using Application.Interfaces.Repos;
using Domain.Entities.Custom_Entities;

public class AddCurrencyExchangeRateCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldInsertExchangeRateAndReturnId()
    {
      
        var mockRepo = new Mock<ICurrencyExchangeRepository>();

        
        mockRepo.Setup(r => r.InsertAsync(It.IsAny<CurrencyExchangeRate>()))
                .Returns(Task.CompletedTask);

        mockRepo.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

        var handler = new AddCurrencyExchangeRateCommandHandler(mockRepo.Object);

        var command = new AddCurrencyExchangeRateCommand
        {
            SourceCurrency = "usd",
            TargetCurrency = "eur",
            Rate = 1.2m,
            LastUpdated = System.DateTime.UtcNow
        };

        
        var result = await handler.Handle(command, CancellationToken.None);

        
        mockRepo.Verify(r => r.InsertAsync(It.IsAny<CurrencyExchangeRate>()), Times.Once);
        mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);

        
        Assert.Equal(0, result);
    }
}
