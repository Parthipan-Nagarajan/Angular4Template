using ECommerce.Data.Interface;
using ECommerce.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business
{
    public class AddressBusinessRules : AbstractValidator<Address>
    {
        public AddressBusinessRules(IEntityBaseRepository<Address> addressDataService, Address address)
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(a => a.AddressLine1).NotEmpty().WithMessage("Address is required.");
            RuleFor(a => a.ZipCode).NotEmpty().WithMessage("Zipcode is required.");
            RuleFor(a => a.MobileNumber).NotEmpty().WithMessage("Mobile Number is required.");
        }

        public AddressBusinessRules()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(a => a.AddressLine1).NotEmpty().WithMessage("Address is required.");
            RuleFor(a => a.ZipCode).NotEmpty().WithMessage("Zipcode is required.");
            RuleFor(a => a.MobileNumber).NotEmpty().WithMessage("Mobile Number is required.");
        }
    }
}
