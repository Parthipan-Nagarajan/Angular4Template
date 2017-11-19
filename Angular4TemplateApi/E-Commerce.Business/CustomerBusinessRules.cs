using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.Configuration;
using ECommerce.Entities;
using ECommerce.Data.Interface;

namespace ECommerce.Business
{
    public class CustomerBusinessRules : AbstractValidator<Customer>
    {
        private Customer _user;
        private Boolean _emailAddressIsValid = true;

        /// <summary>
        /// Account Business Rules
        /// </summary>
        public CustomerBusinessRules(IEntityBaseRepository<Customer> customerDataService, Customer customer)
        {
            if (customer.EmailAddress.Length > 0)
            {
                _user = customerDataService.GetAll().Where(a => a.EmailAddress == customer.EmailAddress).FirstOrDefault();
                if (_user != null) _emailAddressIsValid = false;
            }       

            RuleFor(a => a.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(a => a.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(a => a.EmailAddress).NotEmpty().WithMessage("Email Address is required.");
            RuleFor(a => a.EmailAddress).EmailAddress().WithMessage("Invalid Email Address.");
            RuleFor(a => a.EmailAddress).Must(ValidateDuplicateEmailAddress).WithMessage("Email Address already exists.");          
        }

        public CustomerBusinessRules()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(a => a.LastName).NotEmpty().WithMessage("Last Name is required.");
        }

        /// <summary>
        /// Validate Duplicate Email Address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private bool ValidateDuplicateEmailAddress(string emailAddress)
        {
            return _emailAddressIsValid;

        }
    }
}

