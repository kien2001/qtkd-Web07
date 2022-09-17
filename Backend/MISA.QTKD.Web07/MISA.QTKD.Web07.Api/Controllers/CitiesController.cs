using Entities;
using Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Web07.Core.Entities;

namespace Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        ICityRepository _cityRepository;

        // tiêm vào _customerService một reference
        public CitiesController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        /// <summary>
        /// Api lấy dữ liệu về các huyện trong 1 tỉnh
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet]
        public IActionResult Get([FromQuery] int countryId)
        {
            Result cityResult = _cityRepository.Get(countryId);
            return StatusCode(cityResult.StatusCode, cityResult);


        }

        /// <summary>
        /// Api lấy dữ liệu về các tỉnh/thành phố 
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet("AllCities")]
        public IActionResult GetAll()
        {
            Result cityResult = _cityRepository.GetAll();
            return StatusCode(cityResult.StatusCode, cityResult);


        }
    }
}
