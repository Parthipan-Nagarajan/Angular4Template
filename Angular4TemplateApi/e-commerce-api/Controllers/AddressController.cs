using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Data.Interface;
using System.Net.Http;
using ECommerce.Entities;
using ECommerce.Business;
using ECommerce.Api.Core;
using ECommerce.Api.TokenManagement;

namespace ECommerce.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        public IEntityBaseRepository<Address> _addressDataService { get; set; }

        public AddressController(IEntityBaseRepository<Address> addressDataService)
        {
            this._addressDataService = addressDataService;
        }

        [Route("UpdateAddress")]
        [HttpPost]
        public IActionResult UpdateAddress(HttpRequestMessage request, [FromBody] AddressModel addressInformation)
        {
            TransactionalBase transaction = new TransactionalBase();
            addressInformation.IpAddress = request.GetClientIpAddress();
            AddressBusinessService addressBusinessService = new AddressBusinessService(_addressDataService);
            Address address = addressBusinessService.ManipulateAddress(addressInformation, out transaction);
            if (transaction.ReturnStatus == false)
            {
                return new ObjectResult(transaction);
            }

            addressInformation.Id = address.Id;
            addressInformation.ReturnStatus = true;
            addressInformation.ReturnMessage = transaction.ReturnMessage;
            return new ObjectResult(addressInformation);
        }

        [Route("UpdateAddress")]
        [HttpPost]
        public IActionResult DeleteAddress(HttpRequestMessage request, [FromBody] AddressModel addressInformation)
        {
            TransactionalBase transaction = new TransactionalBase();
            AddressBusinessService addressBusinessService = new AddressBusinessService(_addressDataService);
            addressBusinessService.DeleteAddress(addressInformation, out transaction);
            return new ObjectResult(transaction);
        }
    }
}
