using Application.CurrencyExchange;
using FluentValidation;
using System;

namespace Application.CurrencyExchange.Validators
{
    public class AddCurrencyExchangeRateValidator : AbstractValidator<AddCurrencyExchangeRateCommand>
    {
        public AddCurrencyExchangeRateValidator()
        {
            RuleFor(x => x.SourceCurrency)
                .NotEmpty().WithMessage("Source currency is required.")
                .Length(3).WithMessage("Source currency must be a 3-letter code.");

            RuleFor(x => x.TargetCurrency)
                .NotEmpty().WithMessage("Target currency is required.")
                .Length(3).WithMessage("Target currency must be a 3-letter code.");

            RuleFor(x => x.Rate)
                .GreaterThan(0).WithMessage("Exchange rate must be positive.");

            RuleFor(x => x.LastUpdated)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Last updated date cannot be in the future.");
        }
    }
}

