using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common_Entities;

namespace Domain.Entities.Common
{
    public interface IEntity<TKey> 
    {
        TKey Id { get; set; }


        
    }
}
