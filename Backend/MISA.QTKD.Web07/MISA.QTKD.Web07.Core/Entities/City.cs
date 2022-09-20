using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng City
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class City
    {
        #region CityFields

        public int CityId { get; set; } // Id thành phố
        public string? CityName { get; set; } // Tên thành phố

        #endregion

        public City(int cityId, string cityName)
        {
            CityId = cityId;
            CityName = cityName;
        }
    }
}
