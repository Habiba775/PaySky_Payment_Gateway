using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using InfraStructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly AppDbContext _context;

        public MerchantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Merchant> CreateMerchantAsync(string name, string email)
        {
            var merchant = new Merchant(name, email);
            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();
            return merchant;
        }
    }
}

