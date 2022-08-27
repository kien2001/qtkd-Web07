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
    public class CountriesController : ControllerBase
    {
        ICountryRepository _countryRepository;

        // tiêm vào _customerService một reference
        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        /// <summary>
        /// Api lấy dữ liệu về bảng country
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet]
        public IActionResult get()
        {
            Result countryResult = _countryRepository.Get();
            if (!countryResult.Flag)
            {
                return BadRequest(countryResult);
            }
            return Ok(countryResult);
        }
    }
}
