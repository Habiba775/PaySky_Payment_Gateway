using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Custom_Entities;

namespace Application.Interfaces.Repos
{
    public interface IAuthRepository : IGenericRepository<APPUser, int>
    {
        Task<APPUser?> GetUserAsync(string email, string password);
        Task<APPUser> CreateUserAsync(APPUser user);
    }

}
