using Entities;
using Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QTKD.Web07.Core.Entities;
using MISA.QTKD.Web07.Core.Interfaces.Services;
using Services;
using Enums;
using ClosedXML.Excel;
using System.Text.Json;
using System.Drawing;

namespace MISA.QTKD.Web07.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;

        // tiêm vào _customerService một reference
        public CustomersController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// Lấy phân trang cho bảng customer
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPut]
        public IActionResult Get([FromQuery] int? pageSize, [FromQuery] int? pageIndex, [FromQuery] string? keyword, [FromBody] FilterObject[]? listFilter)
        {
            Result customerResult = _customerRepository.GetPaging(pageSize, pageIndex, keyword, listFilter);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);

        }
        /// <summary>
        /// Update dữ liệu của nhiều customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost("MultipleRows")]
        public IActionResult UpdateMultiple(UpdatedMultiple<Guid> updatedMultiple)
        {
            Result customerResult = _customerRepository.UpdateMultiple(updatedMultiple);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);
        }


        /// <summary>
        /// Api thêm 1 bản ghi trong bảng customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost]
        public IActionResult Post(CustomerInsert customerInsert)
        {
            Result customerResult = _customerService.InsertService(customerInsert);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);
        }
        /// <summary>
        /// Api sửa 1 bản ghi trong bảng customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPut("{customerId}")]
        public IActionResult Put(Guid customerId, [FromBody] CustomerUpdate customer)
        {
            Result customerResult = _customerService.UpdateService(customer, customerId);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);

        }
        /// <summary>
        /// Api xoá 1 bản ghi trong customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        ///  Created by LVKien 17/8/2022
        [HttpPost("ListCustomerId")]
        public IActionResult Delete(IEnumerable<Guid> listCustomerId)
        {
            Result customerResult = _customerRepository.Delete(listCustomerId);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }
            return Ok(customerResult);

        }
        /// <summary>
        /// Api export các bản ghi thành file excel
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by LVKIEN 7/9/2022
        [HttpPost("CustomerExcel")]
        public IActionResult ExportExcel([FromBody] List<Guid> customerId)
        {
            Result customerResult = _customerService.ExportExcel(customerId);
            if (customerResult.Flag == false)
            {
                return BadRequest(customerResult);
            }

            byte[] content = (byte[])customerResult.Data;
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Customer.xlsx");
            //try
            //{
            //    using var workbook = new XLWorkbook();
            //    var worksheet = workbook.Worksheets.Add("Potential");
            //    var currentRow = 1;
            //    int[] widthArr = {35, 15, 30, 20, 20, 20, 30, 30, 30, 30, 25, 25, 20, 20, 20, 35, 40, 30, 20, 30, 20, 35 };

            //    #region Header
            //    foreach (TableField.ListField field in Enum.GetValues(typeof(TableField.ListField)))
            //    {
            //        worksheet.Cell(currentRow, (int)field).Value = field.ToDescription();

            //        // Set PatternType
            //        worksheet.Cell(currentRow, (int)field).Style.Fill.PatternType = XLFillPatternValues.Gray0625;
            //        // Set Màu cho Background
            //        worksheet.Cell(currentRow, (int)field).Style.Fill.BackgroundColor = XLColor.FromName("Green");
            //        // Set Font cho text  trong Range hiện tại
            //        worksheet.Cell(currentRow, (int)field).Style.Font.FontName = "Arial";
            //        worksheet.Cell(currentRow, (int)field).Style.Font.FontSize = 16;

            //        worksheet.Row(currentRow).Height = 30;

            //        worksheet.Cell(currentRow, (int)field).Style.Font.Bold = true;
            //        worksheet.Column((int)field).Width = widthArr[(int)field - 1];
            //        worksheet.Cell(currentRow, (int)field).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            //        worksheet.Cell(currentRow, (int)field).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            //        worksheet.Cell(currentRow, (int)field).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            //        worksheet.Cell(currentRow, (int)field).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            //    }
            //    #endregion

            //    #region Body
            //    List<CustomerTable> customerTableList = (List<CustomerTable>)customerResult.Data;
            //    if (customerTableList != null && customerTableList.Any())
            //    {
            //        foreach (var customer in customerTableList)
            //        {
            //            currentRow++;
            //            Type customerType = customer.GetType();
            //            foreach (int field in Enum.GetValues(typeof(TableField.ListField)))
            //            {
            //                var key = Enum.GetName(typeof(TableField.ListField), field);
            //                if (key != null)
            //                {
            //                    var keyCustomer = customerType.GetProperty(key);
            //                    if (keyCustomer != null)
            //                    {
            //                        int index = customerTableList.IndexOf(customer) + 2;
            //                        worksheet.Cell(currentRow, field).SetValue(keyCustomer.GetValue(customer));
            //                        worksheet.Row(index).Height = 20;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    #endregion

            //    using var stream = new MemoryStream();
            //    workbook.SaveAs(stream);
            //    var content = stream.ToArray();

            //    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //        "Customer.xlsx");
            //}
            //catch (Exception)
            //{
            //    throw;
            //}


        }
    }
}
