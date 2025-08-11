using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using MediatR;

namespace Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommand : IRequest<bool>
    {
        public int AccountId { get; private set; }

        public AccountType AccountType { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }
        public double InterestRate { get; set; }
        public decimal DailyLimit { get; set; }

        public void SetAccountId(int id) => AccountId = id;
    }

}
