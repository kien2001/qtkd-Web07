using Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA.QTKD.Web07.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        IGenderRepository _genderRepository;

        // tiêm vào _customerService một reference
        public GendersController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        /// <summary>
        /// Api lấy thông tin giới tính
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEn 17/05/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result typeResult = _genderRepository.Get();
            if (!typeResult.Flag)
            {
                return BadRequest(typeResult);
            }
            return Ok(typeResult);
        }
    }
}
