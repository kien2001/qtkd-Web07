using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MISA.QTKD.Web07.Core.Entities;

namespace Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// Created by: LVKien (11/08/2022)

        Result InsertService(CustomerInsert customer);

        /// <summary>
        /// Sửa đổi dữ liệu
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// Created by: LVKien (11/08/2022)
        Result UpdateService(CustomerUpdate customer, Guid customerId);   
    }
}
