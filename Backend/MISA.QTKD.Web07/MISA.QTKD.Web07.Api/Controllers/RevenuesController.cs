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
    public class RevenuesController : ControllerBase
    {
        IRevenueRepository _revenueRepository;

        // tiêm vào _customerService một reference
        public RevenuesController(IRevenueRepository revenueRepository)
        {
            _revenueRepository = revenueRepository;
        }
        /// <summary>
        /// Api lấy thông tin về bảng doanh số
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEn 17/05/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result revenueResult = _revenueRepository.Get();
            return StatusCode(revenueResult.StatusCode, revenueResult);

        }
    }
}
