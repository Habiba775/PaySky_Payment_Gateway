
using Domain.Entities.Custom_Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos
{
    public interface ICurrencyExchangeRepository : IGenericRepository<CurrencyExchangeRate, int>
    {

        Task<CurrencyExchangeRate> GetByCurrenciesAsync(string sourceCurrency, string targetCurrency);
        Task<CurrencyExchangeRate?> GetRateAsync(string sourceCurrency, string targetCurrency);
    }
}

