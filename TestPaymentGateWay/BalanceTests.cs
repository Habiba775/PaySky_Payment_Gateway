using Domain.Entities;

public class BalanceTests
{
    [Fact]
    public void Balance_ShouldReturnCurrentBalance()
    {
        var account = new CheckingAccount { Balance = 250m };
        Assert.Equal(250m, account.Balance);
    }
}
