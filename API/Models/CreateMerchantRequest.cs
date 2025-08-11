using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Server;

namespace API.Models
{
    public class CreateMerchantRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}




