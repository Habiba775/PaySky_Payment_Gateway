using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Custom_Entities
{
    public class APPUser : IdentityUser<int>, IEntity<int>
    {
        
    }

}