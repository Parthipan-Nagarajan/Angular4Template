using ECommerce.Common;
using ECommerce.Data.Interface;
using ECommerce.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Business
{
    public class CustomerBusinessService
    {
        private IEntityBaseRepository<Customer> _customerDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerBusinessService(IEntityBaseRepository<Customer> customerDataService)
        {
            _customerDataService = customerDataService;
        }

        /// <summary>
        /// Register Customer
        /// </summary>
        /// <param name="customerInformation"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public void RegisterCustomer(CustomerModel customerModel, out Guid custID, out TransactionalBase transaction)
        {
            transaction = new TransactionalBase();
            Customer customer = new Customer();

            try
            {
                customer.EmailAddress = customerModel.EmailAddress;
                customer.FirstName = customerModel.FirstName;
                customer.LastName = customerModel.LastName;
                customer.Password = customerModel.Password;
                customer.MobileNumber = customerModel.MobileNumber;
                customer.CreatedByIp = customerModel.IpAddress;
                customer.CreatedOn = DateTime.Now;
                customer.UpdatedByIp = null;
                customer.UpdatedOn = null;
                customer.VisitCount = 1;
                customer.IsEmailVerified = false;
                customer.TransCount = 0;
                customer.LastLogin = DateTime.Now;


                CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules(_customerDataService, customer);
                ValidationResult results = customerBusinessRules.Validate(customer);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                }

                _customerDataService.Add(customer);
                _customerDataService.Commit();

                custID = customer.Id;

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Customer successfully Added.");
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
        }

        public CustomerModel GetCustomer(Guid Id, out CustomerModel customer)
        {
            customer = new CustomerModel();
            Customer customerEntity = _customerDataService.GetSingle(Id);
            if (customerEntity == null)
            {
                customer.ReturnStatus = false;
                customer.ReturnMessage.Add("Customer not found");
            }
            else
            {
                customer.ReturnStatus = true;
            }

            return customer;
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="userInformation"></param>
        /// <param name="transaction"></param>
        public void UpdateProfile(CustomerModel customerInformation, out TransactionalBase transaction)
        {
            transaction = new TransactionalBase();
            Customer existingUser = _customerDataService.GetAll().Where(a => a.Id == customerInformation.Id).FirstOrDefault();

            try
            {
                existingUser.FirstName = customerInformation.FirstName;
                existingUser.LastName = customerInformation.LastName;
                existingUser.MobileNumber = customerInformation.MobileNumber;
                existingUser.UpdatedByIp = customerInformation.IpAddress;
                existingUser.UpdatedOn = DateTime.Now;

                CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();
                ValidationResult results = customerBusinessRules.Validate(existingUser);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _customerDataService.Update(existingUser);
                _customerDataService.Commit();

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Your profile was successfully updated.");
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

            return;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Customer Login(string emailAddress, string password, out TransactionalBase transaction)
        {
            transaction = new TransactionalBase();
            Customer customer = new Customer();
            try
            {
                customer = _customerDataService.GetAll().Where(a => a.EmailAddress == emailAddress).FirstOrDefault();
                if (customer == null || customer.Password != password)
                {
                    transaction.ReturnMessage.Add("Invalid Login .... Please Try Again");
                    transaction.ReturnStatus = false;
                    return customer;
                }

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Successfully Logged In ....");
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

            return customer;
        }

        /// <summary>
        /// Authenicate
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Customer Authenticate(Guid customerID, out TransactionalBase transaction)
        {
            transaction = new TransactionalBase();
            Customer customer = new Customer();
            try
            {
                customer = _customerDataService.GetAll().Where(a => a.Id == customerID).FirstOrDefault();
                if (customer == null)
                {
                    transaction.ReturnMessage.Add("Invalid session");
                    transaction.ReturnStatus = false;
                    return customer;
                }

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Valid session.");
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

            return customer;
        }
    }
}