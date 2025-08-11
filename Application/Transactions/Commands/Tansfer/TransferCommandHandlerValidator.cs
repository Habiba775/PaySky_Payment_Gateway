using FluentValidation;
using Application.Transactions.Commands.Tansfer.Application.Transactions.Commands;

public class TransferCommandValidator : AbstractValidator<TransferCommand>
{
    public TransferCommandValidator()
    {
        RuleFor(x => x.FromAccountId)
            .GreaterThan(0).WithMessage("FromAccountId must be greater than zero.");

        RuleFor(x => x.ToAccountId)
            .GreaterThan(0).WithMessage("ToAccountId must be greater than zero.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Transfer amount must be greater than zero.");

        RuleFor(x => x)
            .Must(cmd => cmd.FromAccountId != cmd.ToAccountId)
            .WithMessage("FromAccountId and ToAccountId cannot be the same.");
    }
}
