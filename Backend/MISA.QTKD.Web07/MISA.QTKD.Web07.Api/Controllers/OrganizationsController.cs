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
    public class OrganizationsController : ControllerBase
    {
        IOrganizationRepository _organizationRepository;

        // tiêm vào _customerService một reference
        public OrganizationsController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        /// <summary>
        /// Api lấy dữ liệu về bảng organization
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result organizationResult = _organizationRepository.Get();
            if (!organizationResult.Flag)
            {
                return BadRequest(organizationResult);
            }
            return Ok(organizationResult);
        }
        /// <summary>
        /// Api lấy dữ liệu ngành nghề của bảng organization
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 23/8/2022
        [HttpGet("Careers")]
        public IActionResult GetCareer()
        {
            Result organizationResult = _organizationRepository.GetCareer();
            if (!organizationResult.Flag)
            {
                return BadRequest(organizationResult);
            }
            return Ok(organizationResult);
        }

        /// <summary>
        /// Api lấy dữ liệu về bảng organization
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet("Domains")]
        public IActionResult GetDomain()
        {
            Result organizationResult = _organizationRepository.GetDomain();
            if (!organizationResult.Flag)
            {
                return BadRequest(organizationResult);
            }
            return Ok(organizationResult);
        }

        /// <summary>
        /// Api thêm 1 bản ghi vào bảng organization
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost]
        public IActionResult Post(Organization organization)
        {
            Result organizationResult = _organizationRepository.Insert(organization);
            if (!organizationResult.Flag)
            {
                return BadRequest(organizationResult);
            }
            return Ok(organizationResult);
        }


        /// <summary>
        /// Update dữ liệu của nhiều organization
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost("MultipleRows")]
        public IActionResult UpdateMultiple(UpdatedMultiple<int> updatedMultiple)
        {
            Result addressResult = _organizationRepository.UpdateMultiple(updatedMultiple);
            if (addressResult.Flag == false)
            {
                return BadRequest(addressResult);
            }
            return Ok(addressResult);
        }
    }
}
