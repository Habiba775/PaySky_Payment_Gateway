using MediatR;
using Domain.Entities;

namespace Application.Accounts.Queries.GetAccount
{
    public class GetAccountByIdQuery : IRequest<Account?>
    {
        public int Id { get; set; }
        public GetAccountByIdQuery(int id)
        {
            Id = id;
        }
    }
}