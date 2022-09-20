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
    public class DepartmentsController : ControllerBase
    {
        IDepartmentRepository _departmentRepository;

        // tiêm vào _customerService một reference
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        /// <summary>
        /// Api lấy dữ liệu của bảng department
        /// </summary>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpGet]
        public IActionResult Get()
        {
            Result departmentResult = _departmentRepository.Get();
            
            return StatusCode(departmentResult.StatusCode, departmentResult);

        }
    }
}
