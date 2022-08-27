using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng doanh số
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Revenue
    {
        public int RevenueId { get; set; } // Id Doanh số (thuộc Organizaiton)
        public string? RevenueName { get; set; } // Tên Doanh số (thuộc Organizaiton)

        public Revenue(int revenueId, string revenueName)
        {
            RevenueId = revenueId;
            RevenueName = revenueName;
        }
    }
}
