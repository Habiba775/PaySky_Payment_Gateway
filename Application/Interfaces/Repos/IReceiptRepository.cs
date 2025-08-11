using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Custom_Entities;

namespace Application.Interfaces.Repos
{
    public interface IReceiptRepository : IGenericRepository<Receipt, int>
    {
        Task<Receipt?> GetByTransactionIdAsync(int transactionId);
    }
}
