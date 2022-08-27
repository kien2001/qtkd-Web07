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
    public class WardsController : ControllerBase
    {
        IWardRepository _wardRepository;

        // tiêm vào _customerService một reference
        public WardsController(IWardRepository wardRepository)
        {
            _wardRepository = wardRepository;
        }
        /// <summary>
        /// Api lấy thông tin bảng phường xã
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        /// Created by LVKIEN 17/05/2022
        [HttpGet]
        public IActionResult Get([FromQuery] int districtId)
        {
            Result wardResult = _wardRepository.Get(districtId);
            if (!wardResult.Flag)
            {
                return BadRequest(wardResult);
            }
            return Ok(wardResult);
        }

        /// <summary>
        /// Api lấy dữ liệu về các xã 
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet("AllWards")]
        public IActionResult GetAll()
        {
            Result wardResult = _wardRepository.GetAll();
            if (!wardResult.Flag)
            {
                return BadRequest(wardResult);
            }
            return Ok(wardResult);
        }
    }
}
