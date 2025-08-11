using Application.Transactions.Commands.Deposit;
using FluentValidation;

namespace Application.Transactions.Validators
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0).WithMessage("AccountId must be greater than zero.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Deposit amount must be greater than zero.");
        }
    }
}
