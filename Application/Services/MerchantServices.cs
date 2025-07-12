using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class MerchantService : IMerchantService
    {
        public Merchant CreateMerchant(string name, string email)
        {
            return new Merchant(name, email);
        }
    }
}
