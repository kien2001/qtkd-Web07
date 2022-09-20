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
    public class SourcesController : ControllerBase
    {
        ISourceRepository _sourceRepository;

        // tiêm vào _customerService một reference
        public SourcesController(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }
        /// <summary>
        /// Api lấy thông tin bảng source
        /// </summary>
        /// <returns></returns>
        /// Created by LVkien 17/05/2022
        [HttpGet]
        public IActionResult get()
        {
            Result sourceResult = _sourceRepository.Get();
            return StatusCode(sourceResult.StatusCode, sourceResult);

        }
    }
}
