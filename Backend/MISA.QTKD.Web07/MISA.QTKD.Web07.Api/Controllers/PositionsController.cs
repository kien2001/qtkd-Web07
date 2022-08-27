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
    public class PositionsController : ControllerBase
    {
        IPositionRepository _positionRepository;

        // tiêm vào _customerService một reference
        public PositionsController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        /// <summary>
        /// Api Lấy dữ liệu bảng position
        /// </summary>
        /// <returns></returns>
        /// Created by LVKien 17/08/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result positionResult = _positionRepository.Get();
            if (!positionResult.Flag)
            {
                return BadRequest(positionResult);
            }
            return Ok(positionResult);
        }
    }
}
