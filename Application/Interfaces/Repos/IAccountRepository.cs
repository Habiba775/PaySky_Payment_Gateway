using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repos
{
    public interface IAccountRepository : IGenericRepository<Account, int>
    {
        Task InsertAccountAsync(Account account);
        Task<decimal?> GetBalanceByIdAsync(int id);
        Task UpdateByIdAsync(int id, Action<Account> updateAction);

        
    }
}

