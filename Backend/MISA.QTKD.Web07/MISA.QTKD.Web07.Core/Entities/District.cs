using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng District
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class District
    {
        public int DistrictId { get; set; } // Id Huyện
        public string? DistrictName { get; set; }// TÊn huyện

        public District(int districtId, string districtName)
        {
            DistrictId = districtId;
            DistrictName = districtName;
        }
    }
}
