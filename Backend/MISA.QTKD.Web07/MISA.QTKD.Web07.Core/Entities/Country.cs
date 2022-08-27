using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng Country
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Country
    {
        public int CountryId { get; set; } // Id quốc gia
        public string? CountryName { get; set; } // Tên quốc gia

        public Country(int countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
        }
    }
}
