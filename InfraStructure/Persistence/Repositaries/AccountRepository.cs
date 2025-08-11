
using Application.Interfaces.Repos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Persistence;
using InfraStructure.Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class AccountRepository : GenericRepository<Account, int>, IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal?> GetBalanceByIdAsync(int id)
        {
            return await _context.Set<Account>()
                .Where(a => a.Id == id)
                .Select(a => (decimal?)a.Balance)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateByIdAsync(int id, Action<Account> updateAction)
        {
            var account = await _context.Set<Account>().FindAsync(id);
            if (account != null)
            {
                updateAction(account);
                _context.Set<Account>().Update(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task InsertAccountAsync(Account account)
        {
            switch (account)
            {
                case SavingsAccount savings:
                    await _context.SavingsAccounts.AddAsync(savings);
                    break;

                case CheckingAccount checking:
                    await _context.CheckingAccounts.AddAsync(checking);
                    break;

                default:
                    throw new ArgumentException("Invalid account type.");
            }

            await _context.SaveChangesAsync();
        }
       

    }
}




