using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Lưu những thông tin cần hiển thị ở trong bảng ở frontend 
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class CustomerTable
    {
        public Guid CustomerId { get; set; } // Id khách hàng

        public int? Vocative { get; set; } // xưng hô(1- ông, 2- bà, 3- anh, 4-chị, 5- cô, 6- chú, 7- bác, 8- em)

        public string? VocativeName
        {
            get
            {
                return Vocative switch
                {
                    0 => "",
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
        public int? PositionId { set; get; } // Id Chức danh

        public string? PositionName { set; get; } // Tên Chức danh
        public string? LastMiddleName { get; set; } // họ và đệm

        public string FirstName { get; set; } // tên

        public string? CompanyPhoneNum { get; set; } // Điện thoại cơ quan

        public string? CustomerPhoneNum { get; set; } // Điện thoại di động

        public string? CustomerEmail { get; set; } // Email cá nhân

        public string? CompanyEmail { get; set; } // Email cơ quan

        public string? OrganizationName { get; set; } // Tên tổ chức

        public string? AddressName { get; set; } // địa chỉ

        public string? CityName { get; set; } // thành phố

        public string? DistrictName { get; set; } // quận / huyên

        public string? WardName { get; set; } // xã

        public string? SourceName { get; set; } // tên tiềm năng

        public string? SourceId { get; set; } // id tiềm năng

        public string? DepartmentName { get; set; } // tên phòng ban

        public string? DepartmentId { get; set; } // id tiềm năng


        public string? TypeName { get; set; } // Tên loại hình(thuộc Organization)

        public string? Domain { get; set; } // tên lĩnh vực
        public string? Description { get; set; } // Mô tả

        public string? Layout { get; set; } // Bố cục

        public string? Owner { get; set; } // Chủ sở hữu

        public string? RevenueName { get; set; } // Tên Doanh số (thuộc Organizaiton)
        public bool? SharingUse { get; set; } // Dùng chung (0- không dùng chung, 1- dùng chung)

        public bool? DisableCall { get; set; } // Không gọi (0- cho gọi, 1- không gọi)

        public bool? DisableMail { get; set; } // Không gửi email (0- cho gửi, 1- không gửi)

        public string? Facebook { get; set; } // Facebook

        public Guid? PotentialId { get; set; } // Id tiềm năng

        public int? OrganizationId { get; set; } // Id tổ chức

        public DateTime? DateOfBirth { get; set; } // Ngày sinh
        public int? Gender { get; set; } // Giới tính (0- nam, 1- nữ, 2- khác)

        public string? GenderName
        {
            get
            {
                return Gender switch
                {
                    0 => "Nam",
                    1 => "Nữ",
                    2 => "Khác",
                    _ => ""
                };
            }
            set { }
        }

        public CustomerTable(Guid customerId, int? vocative, string? vocativeName, int? positionId, string? positionName, string? lastMiddleName, string firstName, string? companyPhoneNum, string? customerPhoneNum, string? customerEmail, string? companyEmail, string? organizationName, string? addressName, string? cityName, string? districtName, string? wardName, string? sourceName, string? sourceId, string? departmentName, string? departmentId, string? typeName, string? domain, string? description, string? layout, string? owner, string? revenueName, bool? sharingUse, bool? disableCall, bool? disableMail, string? facebook, Guid? potentialId, int? organizationId, DateTime? dateOfBirth, int? gender, string? genderName)
        {
            CustomerId = customerId;
            Vocative = vocative;
            VocativeName = vocativeName;
            PositionId = positionId;
            PositionName = positionName;
            LastMiddleName = lastMiddleName;
            FirstName = firstName;
            CompanyPhoneNum = companyPhoneNum;
            CustomerPhoneNum = customerPhoneNum;
            CustomerEmail = customerEmail;
            CompanyEmail = companyEmail;
            OrganizationName = organizationName;
            AddressName = addressName;
            CityName = cityName;
            DistrictName = districtName;
            WardName = wardName;
            SourceName = sourceName;
            SourceId = sourceId;
            DepartmentName = departmentName;
            DepartmentId = departmentId;
            TypeName = typeName;
            Domain = domain;
            Description = description;
            Layout = layout;
            Owner = owner;
            RevenueName = revenueName;
            SharingUse = sharingUse;
            DisableCall = disableCall;
            DisableMail = disableMail;
            Facebook = facebook;
            PotentialId = potentialId;
            OrganizationId = organizationId;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            GenderName = genderName;
        }

        public CustomerTable()
        {
        }
    }
}
