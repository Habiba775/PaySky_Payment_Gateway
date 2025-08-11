using System.Threading.Tasks;
using MediatR;
using Domain.Entities.Custom_Entities;
using Application.Interfaces.Repos;

namespace Application.CurrencyExchange
{
    public class AddCurrencyExchangeRateCommandHandler : IRequestHandler<AddCurrencyExchangeRateCommand, int>
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public AddCurrencyExchangeRateCommandHandler(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task<int> Handle(AddCurrencyExchangeRateCommand request, CancellationToken cancellationToken)
        {
            // Create new exchange rate entity
            var exchangeRate = new CurrencyExchangeRate
            {
                SourceCurrency = request.SourceCurrency.ToUpper(),
                TargetCurrency = request.TargetCurrency.ToUpper(),
                Rate = request.Rate,
                LastUpdated = request.LastUpdated
            };

            // Add to repository
            await _currencyExchangeRepository.InsertAsync(exchangeRate);

            // Persist changes to the database
            await _currencyExchangeRepository.SaveChangesAsync();

            // Return generated ID after saving
            return exchangeRate.Id;
        }

    }
}

