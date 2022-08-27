using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MISA.QTKD.Web07.Core.Entities;

namespace MISA.QTKD.Web07.Core.Interfaces.Services
{
    public interface IPotentialService
    {
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="potential"></param>
        /// <returns></returns>
        /// Created by: LVKien (11/08/2022)

        Result InsertService(Potential potential);
    }
}
