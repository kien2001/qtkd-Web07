using Entities;
using Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Web07.Core.Entities;
using MISA.QTKD.Web07.Core.Interfaces.Services;

namespace MISA.QTKD.Web07.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;

        // tiêm vào _customerService một reference
        public CustomersController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// Lấy phân trang cho bảng customer
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet]
        public IActionResult Get([FromQuery] int? pageSize, [FromQuery] int? pageIndex, [FromQuery] string? keyword)
        {
            Result customerResult = _customerRepository.GetPaging(pageSize, pageIndex, keyword);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);

        }
        /// <summary>
        /// Update dữ liệu của nhiều customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost("MultipleRows")]
        public IActionResult UpdateMultiple(UpdatedMultiple<Guid> updatedMultiple)
        {
            Result customerResult = _customerRepository.UpdateMultiple(updatedMultiple);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);
        }


        /// <summary>
        /// Api thêm 1 bản ghi trong bảng customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            Result customerResult = _customerService.InsertService(customer);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);
        }
        /// <summary>
        /// Api sửa 1 bản ghi trong bảng customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPut("{customerId}")]
        public IActionResult Put(Guid customerId, [FromBody] CustomerUpdate customer)
        {
            Result customerResult = _customerService.UpdateService(customer, customerId);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);

        }
        /// <summary>
        /// Api xoá 1 bản ghi trong customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost("ListCustomerId")]
        public IActionResult Delete(IEnumerable<Guid> listCustomerId)
        {
            Result customerResult = _customerRepository.Delete(listCustomerId);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);

        }
    }
}
