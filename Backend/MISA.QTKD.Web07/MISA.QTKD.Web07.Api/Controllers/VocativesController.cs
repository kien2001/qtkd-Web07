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
    public class VocativesController : ControllerBase
    {
        IVocativeRepository _vocativeRepository;

        // tiêm vào _customerService một reference
        public VocativesController(IVocativeRepository vocativeRepository)
        {
            _vocativeRepository = vocativeRepository;
        }
        /// <summary>
        /// Api lấy thông tin trường vocative của bảng customer
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEN 17/05/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result vocativeResult = _vocativeRepository.Get();
            return StatusCode(vocativeResult.StatusCode, vocativeResult);

        }

    }
}
