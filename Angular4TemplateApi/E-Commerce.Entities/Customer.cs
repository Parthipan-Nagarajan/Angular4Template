using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public int VisitCount { get; set; }
        public int TransCount { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime? LastLogin { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }

    public class CustomerModel : TransactionalBase
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
    }
}
