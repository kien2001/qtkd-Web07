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
    public class PotentialsController : ControllerBase
    {
        IPotentialService _potentialService;
        IPotentialRepository _potentialRepository;

        // tiêm vào _customerService một reference
        public PotentialsController(IPotentialService potentialService, IPotentialRepository potentialRepository)
        {
            _potentialService = potentialService;
            _potentialRepository = potentialRepository;
        }
        /// <summary>
        /// Api lấy thông tin bảng potential
        /// </summary>
        /// <returns></returns>
        /// Created by LVKien 17/05/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result potentialResult = _potentialRepository.Get();
            if (!potentialResult.Flag)
            {
                return BadRequest(potentialResult);
            }
            return Ok(potentialResult);
        }
        /// <summary>
        /// Api thêm 1 tiềm năng vào bảng potential
        /// </summary>
        /// <param name="potential"></param>
        /// <returns></returns>
        /// Created by LVKien
        [HttpPost]
        public IActionResult Post(Potential potential)
        {
            Result potentialResult = _potentialService.InsertService(potential);
            if (!potentialResult.Flag)
            {
                return BadRequest(potentialResult);
            }
            return Ok(potentialResult);
        }

        /// <summary>
        /// Tạo mã tiềm năng mới duy nhất, tăng dần
        /// </summary>
        /// <returns></returns>
        /// Created by LVKien 17/05/2022
        [HttpGet("MaxCode")]
        public IActionResult GetMaxCode()
        {
            Result potentialResult = _potentialRepository.GetMaxCode();
            if (!potentialResult.Flag)
            {
                return BadRequest(potentialResult);
            }
            return Ok(potentialResult);
        }
        /// <summary>
        /// Lấy các tên tiềm năng
        /// </summary>
        /// <returns></returns>
        /// Created by LVKien 22/08/2022
        [HttpGet("PotentialNames")]
        public IActionResult GetPotentialName()
        {
            Result potentialResult = _potentialRepository.GetPotentialName();
            if (!potentialResult.Flag)
            {
                return BadRequest(potentialResult);
            }
            return Ok(potentialResult);
        }

    }
}
