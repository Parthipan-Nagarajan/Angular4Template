using ECommerce.Data.Interface;
using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.EntityFramework
{
    public class AddressRepository : EntityBaseRepository<Address>
    {
        public AddressRepository(ECommerceContext context)
            : base(context)
        { }
    }
}
