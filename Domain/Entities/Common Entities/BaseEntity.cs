using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common_Entities;


namespace Domain.Entities.Common
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

       

       
    }
}
