using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.EntityFramework
{
    public class ECommerceDbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            ECommerceContext context = (ECommerceContext)serviceProvider.GetService(typeof(ECommerceContext));
            context.Database.EnsureCreated();
        }
    }
}
