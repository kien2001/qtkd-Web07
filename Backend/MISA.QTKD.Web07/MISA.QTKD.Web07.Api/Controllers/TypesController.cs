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
    public class TypesController : ControllerBase
    {
        ITypeRepository _typeRepository;

        // tiêm vào _customerService một reference
        public TypesController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }
        /// <summary>
        /// Api lấy thông tin bảng loại hình
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEn 17/05/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result typeResult = _typeRepository.Get();
            return StatusCode(typeResult.StatusCode, typeResult);

        }
    }
}
