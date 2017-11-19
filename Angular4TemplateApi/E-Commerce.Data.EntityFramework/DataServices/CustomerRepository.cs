using ECommerce.Data.Interface;
using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.EntityFramework
{
    public class CustomerRepository : EntityBaseRepository<Customer>
    {
        public CustomerRepository(ECommerceContext context)
            : base(context)
        { }
    }

}
