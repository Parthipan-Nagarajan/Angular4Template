using ECommerce.Common;
using ECommerce.Data.Interface;
using ECommerce.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Business
{
    public class AddressBusinessService
    {
        private IEntityBaseRepository<Address> _addressDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public AddressBusinessService(IEntityBaseRepository<Address> addressDataService)
        {
            _addressDataService = addressDataService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addressInformation"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Address ManipulateAddress(AddressModel addressInformation, out TransactionalBase transaction)
        {
            transaction = new TransactionalBase();

            Address address = _addressDataService.GetAll().Where(a => a.Id == addressInformation.Id).FirstOrDefault();
            try
            {
                if (address == null)
                {
                    address = new Address();
                    address.CreatedByIp = addressInformation.IpAddress;
                    address.CreatedOn = DateTime.Now;
                }
                else
                {
                    address.UpdatedByIp = addressInformation.IpAddress;
                    address.UpdatedOn = DateTime.Now;
                }

                address.Name = addressInformation.Name;
                address.AddressLine1 = addressInformation.AddressLine1;
                address.AddressLine2 = addressInformation.AddressLine2;
                address.City = addressInformation.City;
                address.State = addressInformation.State;
                address.Country = addressInformation.Country;
                address.ZipCode = addressInformation.ZipCode;
                address.MobileNumber = addressInformation.MobileNumber;
                address.Customer = new Customer() { Id = addressInformation.CustomerId };

                AddressBusinessRules customerBusinessRules = new AddressBusinessRules(_addressDataService, address);
                ValidationResult results = customerBusinessRules.Validate(address);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return address;
                }

                if (address.Id == Guid.Empty)
                {
                    _addressDataService.Add(address);
                }

                _addressDataService.Commit();

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Address successfully updated.");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
            }

            return address;
        }

        public void DeleteAddress(AddressModel addressInformation, out TransactionalBase transaction)
        {
            transaction = new TransactionalBase();
            try
            {
                _addressDataService.DeleteWhere(a => a.Id == addressInformation.Id);
                _addressDataService.Commit();
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Address Deletee updated.");
            }
            catch (Exception ex)
            {
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(ex.Message);
            }

        }
    }
}
