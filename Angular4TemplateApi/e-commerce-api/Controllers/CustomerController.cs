using ECommerce.Data.Interface;
using ECommerce.Entities;
using ECommerce.Api.TokenManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using ECommerce.Business;
using ECommerce.Api.Core;

namespace ECommerce.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        public IEntityBaseRepository<Customer> _customerDataService { get; set; }

        public CustomerController(IEntityBaseRepository<Customer> customerDataService)
        {
            this._customerDataService = customerDataService;
        }

        /// <summary>
        /// Register Customer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [Route("RegisterCustomer")]
        [HttpPost]
        public IActionResult RegisterCustomer(HttpRequestMessage request, [FromBody] CustomerModel customerInformation)
        {
            TransactionalBase transaction = new TransactionalBase();
            Guid customerID = new Guid();
            customerInformation.IpAddress = request.GetClientIpAddress();
            CustomerBusinessService userBusinessService = new CustomerBusinessService(_customerDataService);
            userBusinessService.RegisterCustomer(customerInformation, out customerID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                return new ObjectResult(transaction);
            }
            customerInformation.ReturnStatus = true;
            customerInformation.ReturnMessage = transaction.ReturnMessage;
            customerInformation.Token = TokenManager.CreateToken(customerID);
            customerInformation.IsAuthenticated = true;

            return new ObjectResult(customerInformation);
        }

        [Route("GetCustomer")]
        [HttpPost]
        public IActionResult GetCustomer(HttpRequestMessage request, [FromQuery] string Id)
        {
            CustomerModel customer = new CustomerModel();
            CustomerBusinessService userBusinessService = new CustomerBusinessService(_customerDataService);
            userBusinessService.GetCustomer(Guid.Parse(Id), out customer);
            return new ObjectResult(customer);
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userInformation"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(HttpRequestMessage request, [FromBody] CustomerModel customerModel)
        {
            TransactionalBase transaction = new TransactionalBase();

            string emailAddress = customerModel.EmailAddress;
            string password = customerModel.Password;

            CustomerBusinessService customerBusinessService = new CustomerBusinessService(_customerDataService);
            Customer customer = customerBusinessService.Login(emailAddress, password, out transaction);
            if (transaction.ReturnStatus == false)
            {
                return new ObjectResult(transaction);
            }

            string tokenString = TokenManager.CreateToken(customer.Id);

            customerModel.Id = customer.Id;
            customerModel.EmailAddress = customer.EmailAddress;
            customerModel.FirstName = customer.FirstName;
            customerModel.LastName = customer.LastName;
            customerModel.IsAuthenticated = true;
            customerModel.Token = tokenString;
            customerModel.ReturnStatus = true;
            customerModel.ReturnMessage = transaction.ReturnMessage;
            customerModel.Token = tokenString;
            return new ObjectResult(customerModel);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userInformation"></param>
        /// <returns></returns>

        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout(HttpRequestMessage request)
        {
            CustomerModel customerInformation = new CustomerModel
            {
                IsAuthenticated = false,
                Token = string.Empty,
                ReturnStatus = true
            };
            customerInformation.ReturnMessage.Add("logout success !");
            return new ObjectResult(customerInformation);
        }


        /// <summary>
        /// Authenicate
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userInformation"></param>
        /// <returns></returns>
        [Route("Authenticate")]
        [HttpGet]
        public IActionResult Authenticate(HttpRequestMessage request)
        {
            CustomerModel transaction = new CustomerModel();

            if (request.Headers.Authorization == null)
            {
                transaction.ReturnMessage.Add("Your are not authorized.");
                transaction.ReturnStatus = false;
                transaction.IsAuthenticated = false;
                return new ObjectResult(transaction);
            }

            string tokenString = request.Headers.Authorization.ToString();

            ClaimsPrincipal principal = TokenManager.ValidateToken(tokenString);

            if (principal == null)
            {
                transaction.ReturnMessage.Add("Your are not authorized");
                transaction.ReturnStatus = false;
                transaction.IsAuthenticated = false;
                return new ObjectResult(transaction);
            }

            transaction.IsAuthenticated = true;
            transaction.Token = tokenString;
            return new ObjectResult(transaction);

        }


        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userInformation"></param>
        /// <returns></returns>
        [Route("UpdateProfile")]
        [HttpPost]
        public IActionResult UpdateProfile(HttpRequestMessage request, [FromBody] CustomerModel customerInformation)
        {
            TransactionalBase transaction = new TransactionalBase();

            if (request.Headers.Authorization == null)
            {
                transaction.ReturnMessage.Add("Your are not authorized");
                transaction.ReturnStatus = false;
                return new ObjectResult(transaction);
            }

            string tokenString = request.Headers.Authorization.ToString();

            ClaimsPrincipal principal = TokenManager.ValidateToken(tokenString);

            if (principal == null)
            {

                transaction.ReturnMessage.Add("Your are not authorized");
                transaction.ReturnStatus = false;
                return new ObjectResult(transaction);
            }

            Guid userID = TokenManager.GetUserID(Request.Headers["Authorization"].ToString());
            if (userID == Guid.Empty)
            {
                transaction.ReturnMessage.Add("Your are not authorized");
                transaction.ReturnStatus = false;
                return new ObjectResult(transaction);
            }

            customerInformation.Id = userID;

            CustomerBusinessService userBusinessService = new CustomerBusinessService(_customerDataService);
            userBusinessService.UpdateProfile(customerInformation, out transaction);
            if (transaction.ReturnStatus == false)
            {
                return new ObjectResult(transaction);
            }

            customerInformation.ReturnStatus = transaction.ReturnStatus;
            customerInformation.ReturnMessage = transaction.ReturnMessage;
            customerInformation.Token = tokenString;

            return new ObjectResult(customerInformation);
        }
    }
}