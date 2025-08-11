using Application.Interfaces.Repos;
using Domain.Entities.Custom_Entities;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Persistence;
using Application.Interfaces.Repos;
using InfraStructure.Persistence.Repositories;
using Application.Interfaces.Repositories;

namespace InfraStructure.Persistence.Repositaries
{
    public class AuditLogRepository : GenericRepository<AuditLog, int>, IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId)
        {
            return await _context.AuditLogs
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }
        public async Task LogAsync(int userId, string action, string entity, string details)
        {
            var auditLog = new AuditLog
            {
                UserId = userId,
                Action = action,
                Entity = entity,
                Timestamp = DateTime.UtcNow,
                Details = details
            };

            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}