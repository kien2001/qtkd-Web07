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
    public class DistrictsController : ControllerBase
    {
        IDistrictRepository _districtRepository;

        // tiêm vào _customerService một reference
        public DistrictsController(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }
        /// <summary>
        /// Api lấy dữ liệu về bảng district của 1 thành phố
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet]
        public IActionResult Get([FromQuery] int cityId)
        {
            Result districtResult = _districtRepository.Get(cityId);
            return StatusCode(districtResult.StatusCode, districtResult);

        }

        /// <summary>
        /// Api lấy dữ liệu về các huyện 
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet("AllDistricts")]
        public IActionResult GetAll()
        {
            Result districtResult = _districtRepository.GetAll();
            return StatusCode(districtResult.StatusCode, districtResult);

        }
    }
}
