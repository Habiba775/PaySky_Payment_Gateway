using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Custom_Entities;


    namespace Application.Interfaces.Repositories
    {
        public interface IAuditLogRepository : IGenericRepository<AuditLog, int>
        {
        Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId);
        Task LogAsync(int UserID,string action, string entity, string details);
    }
    }

