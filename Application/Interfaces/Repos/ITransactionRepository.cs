using Domain.Entities.Custom_Entities;
using Domain.Entities;
namespace Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction, int>
    {
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);
        Task<Transaction?> GetWithReceiptAsync(int transactionId);
    }
}
