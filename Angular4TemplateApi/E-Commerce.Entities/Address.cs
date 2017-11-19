using System;
using System.Collections.Generic;

namespace ECommerce.Entities
{
    public class Address : EntityBase
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Customer Customer { get; set; }
    }

    public class AddressModel : TransactionalBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public bool IsPrimary { get; set; }
        public Guid CustomerId { get; set; }
    }
}
