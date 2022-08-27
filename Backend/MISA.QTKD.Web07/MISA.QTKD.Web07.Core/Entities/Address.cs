using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng Address
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Address
    {
        public int AddressId { get; set; } // ID địa chỉ
        public string? AddressName { get; set; } // Tên địa chỉ

        public string? HomeNumber { get; set; } // Số nhà

        public string? PostalCode { get; set; } // Mã vùng

        public int? CountryId { get; set; }// Id Quốc gia

        public int? CityId { get; set; }// Id thành phố

        public int? DistrictId { get; set; } // Id huyện

        public int? WardId { get; set; }// Id Xã

        public Address(int addressId, string addressName, string homeNumber, string postalCode, int countryId, int cityId, int districtId, int wardId)
        {
            AddressId = addressId;
            AddressName = addressName;
            HomeNumber = homeNumber;
            PostalCode = postalCode;
            CountryId = countryId;
            CityId = cityId;
            DistrictId = districtId;
            WardId = wardId;
        }

        public Address()
        {

        }
    }
}
