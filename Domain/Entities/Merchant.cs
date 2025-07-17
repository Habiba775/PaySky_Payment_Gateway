using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Merchant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }

        public Merchant(string name, string email)
        {
            Name = name;
            Email = email;
        }

        // Required by EF Core
        protected Merchant() { }
    }
}