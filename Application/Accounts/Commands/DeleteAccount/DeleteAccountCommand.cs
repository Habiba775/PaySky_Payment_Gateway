using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommand : IRequest<bool>
    {
        public int AccountId { get; set; }
    }
}
