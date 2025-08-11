using Application.Interfaces.Repos;
using Domain.Entities.Custom_Entities;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Persistence;
using Application.Interfaces.Repos;
using InfraStructure.Persistence.Repositories;
using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction, int>, ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
    .Where(t => t.CheckingAccountId == accountId || t.SavingsAccountId == accountId)
    .ToListAsync();
        }

        public async Task<Transaction?> GetWithReceiptAsync(int transactionId)
        {
            return await _context.Transactions
                .Include(t => t.Receipt)
                .FirstOrDefaultAsync(t => t.Id == transactionId);
        }
    }
}
