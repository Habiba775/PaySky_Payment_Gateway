using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMerchantService
    {
        Task<Merchant> CreateMerchantAsync(string name, string email);
    }
}

