using Application.Interfaces.Repos;
using Domain.Entities.Custom_Entities;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Persistence;
using Application.Interfaces.Repos;
using InfraStructure.Persistence.Repositories;
using Application.Interfaces.Repositories;

namespace InfraStructure.Persistence.Repositaries
{
    
        public class ReceiptRepository : GenericRepository<Receipt, int>, IReceiptRepository
        {
            private readonly AppDbContext _context;

            public ReceiptRepository(AppDbContext context) : base(context)
            {
                _context = context;
            }

            public async Task<Receipt?> GetByTransactionIdAsync(int transactionId)
            {
                return await _context.Receipts
                    .FirstOrDefaultAsync(r => r.TransactionId == transactionId);
            }
        }
    }


