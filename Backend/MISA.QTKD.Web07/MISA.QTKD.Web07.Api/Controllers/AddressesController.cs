using Entities;
using Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Web07.Core.Entities;

namespace MISA.QTKD.Web07.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        IAddressRepository _addressRepository;

        // tiêm vào _customerService một reference
        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        /// <summary>
        /// Api lấy dữ liệu về bảng Address
        /// </summary>
        /// <returns></returns>
        /// Created by LVKien 17/8/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result addressResult = _addressRepository.Get();
            if (!addressResult.Flag)
            {
                return BadRequest(addressResult);
            }
            return Ok(addressResult);
        }
        /// <summary>
        /// Api thêm 1 bản ghi về bảng Address
        /// </summary>
        /// <returns></returns>
        /// Created by LVKien 17/8/2022

        [HttpPost]
        public IActionResult Post(Address address)
        {
            Result addressResult = _addressRepository.Insert(address);
            if (!addressResult.Flag)
            {
                return BadRequest(addressResult);
            }
            return Ok(addressResult);
        }

        /// <summary>
        /// Update dữ liệu của nhiều address
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost("MultipleRows")]
        public IActionResult UpdateMultiple(UpdatedMultiple<int> updatedMultiple)
        {
            Result addressResult = _addressRepository.UpdateMultiple(updatedMultiple);
            if (addressResult.Flag == false)
            {
                return BadRequest(addressResult);
            }
            return Ok(addressResult);
        }
    }
}
