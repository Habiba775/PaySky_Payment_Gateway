
using Application.Interfaces.Repos;
using Domain.Entities;
using Domain.Enums;
using Domain.Entities.Custom_Entities;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Persistence;
using InfraStructure.Persistence.Repositories;

namespace InfraStructure.Persistence.Repositaries
{


    public class CurrencyExchangeRateRepository : GenericRepository<CurrencyExchangeRate, int>, ICurrencyExchangeRepository
    {
        private readonly AppDbContext _context;

        public CurrencyExchangeRateRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CurrencyExchangeRate?> GetByCurrenciesAsync(string source, string target)
        {
            return await _context.CurrencyExchangeRates
                .FirstOrDefaultAsync(x => x.SourceCurrency == source && x.TargetCurrency == target);
        }
        public async Task<CurrencyExchangeRate?> GetRateAsync(string sourceCurrency, string targetCurrency)
        {
            return await _context.CurrencyExchangeRates
                .FirstOrDefaultAsync(r => r.SourceCurrency.ToUpper() == sourceCurrency.ToUpper()
                                       && r.TargetCurrency.ToUpper() == targetCurrency.ToUpper());
        }
    }

}