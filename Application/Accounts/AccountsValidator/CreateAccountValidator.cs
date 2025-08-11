using Application.Accounts.Commands.CreateAccount;
using FluentValidation;
using Domain.Enums;

namespace Application.Accounts.AccountsValidator
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountValidator()
        {
            ApplyValidationsRule();
        }

        public void ApplyValidationsRule()
        {
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account Number is required")
                .Length(10, 20).WithMessage("Account Number must be between 10 and 20 characters");

            RuleFor(x => x.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative");

            RuleFor(x => x.AccountType)
                .IsInEnum().WithMessage("Account Type is invalid");

            RuleFor(x => x.InterestRate)
                .GreaterThanOrEqualTo(0).WithMessage("Interest Rate cannot be negative")
                .LessThanOrEqualTo(100).WithMessage("Interest Rate cannot exceed 100%");

            RuleFor(x => x.DailyLimit)
                .GreaterThanOrEqualTo(0).WithMessage("Daily Limit cannot be negative");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be a 3-letter  code");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than zero");
        }
    }
}
