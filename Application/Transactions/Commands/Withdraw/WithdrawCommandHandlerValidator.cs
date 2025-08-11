using FluentValidation;
using Application.Transactions.Commands.Withdraw;

public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
{
    public WithdrawCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .GreaterThan(0).WithMessage("AccountId must be greater than zero.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Withdraw amount must be greater than zero.");
    }
}
