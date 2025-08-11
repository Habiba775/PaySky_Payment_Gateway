using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using MediatR;

namespace Application.Accounts.Commands.CreateAccount
{


    public class CreateAccountCommand : IRequest<int>
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        public double InterestRate { get; set; } = 0.0;
        public decimal DailyLimit { get; set; } = 0;
        public string Currency { get; set; }

        public int UserId { get; set; }
    }
}

