using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustomerTable
    {
        public Guid CustomerId { get; set; } // Id khách hàng

        public string? FullName { get; set; } // Họ và tên


        public string? CompanyPhoneNum { get; set; } // Điện thoại cơ quan

        public string? CustomerPhoneNum { get; set; } // Điện thoại di động

        public string? CustomerEmail { get; set; } // Email cá nhân

        public string? CompanyEmail { get; set; } // Email cơ quan

        public string? OrganizationName { get; set; } // Tên tổ chức

        public string? Description { get; set; } // Mô tả

        public bool? SharingUse { get; set; } // Dùng chung (0- không dùng chung, 1- dùng chung)
        public string? SharingUseName
        {
            get
            {
                return SharingUse switch
                {
                    false => "Không dùng chung",
                    true => "Dùng chung",
                    _ => "",
                };
            }
            set { }
        }
        public string? Facebook { get; set; } // Facebook

        public string? Layout { get; set; } // Bố cục

        public string? Owner { get; set; } // Chủ sở hữu

        public int? Vocative { get; set; } // xưng hô(0- ông, 1- bà, 2- anh, 3-chị, 4- cô, 5- chú, 6- bác, 7- em)

        public string? VocativeName
        {
            get
            {
                return Vocative switch
                {
                    0 => "Không chọn",
                    1 => "Ông",
                    2 => "Bà",
                    3 => "Anh",
                    4 => "Chị",
                    5 => "Cô",
                    6 => "Chú",
                    7 => "Bác",
                    8 => "Em",
                    _ => "",
                };
            }
            set { }
        }

        public string? DepartmentName { get; set; } // tên phòng ban

        public string? PositionName { get; set; } // Tên vị trí

        public string? SourceName { get; set; } // Tên nguồn gốc


        public string? AddressName { get; set; } // địa chỉ

        public string? CityName { get; set; } // thành phố

        public string? DistrictName { get; set; } // quận / huyên

        public string? WardName { get; set; } // xã


        public string? TypeName { get; set; } // Tên loại hình(thuộc Organization)

        public string? Domain { get; set; } // tên lĩnh vực


        public string? RevenueName { get; set; } // Tên Doanh số (thuộc Organizaiton)

        public CustomerTable(Guid customerId, string? fullName, string? companyPhoneNum, string? customerPhoneNum, string? customerEmail, string? companyEmail, string? organizationName, string? description, bool? sharingUse, string? facebook, string? layout, string? owner, int? vocative, string? vocativeName, string? departmentName, string? positionName, string? sourceName, string? addressName, string? cityName, string? districtName, string? wardName, string? typeName, string? domain, string? revenueName)
        {
            CustomerId = customerId;
            FullName = fullName;
            CompanyPhoneNum = companyPhoneNum;
            CustomerPhoneNum = customerPhoneNum;
            CustomerEmail = customerEmail;
            CompanyEmail = companyEmail;
            OrganizationName = organizationName;
            Description = description;
            SharingUse = sharingUse;
            Facebook = facebook;
            Layout = layout;
            Owner = owner;
            Vocative = vocative;
            VocativeName = vocativeName;
            DepartmentName = departmentName;
            PositionName = positionName;
            SourceName = sourceName;
            AddressName = addressName;
            CityName = cityName;
            DistrictName = districtName;
            WardName = wardName;
            TypeName = typeName;
            Domain = domain;
            RevenueName = revenueName;
        }

        public CustomerTable()
        {
        }
    }
}
