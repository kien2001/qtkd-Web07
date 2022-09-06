using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    /// <summary>
    /// Thông tin Customer
    /// </summary>
    /// Created By: LVKien (08/11/2022)
    public class Customer
    {
        // crtl K S
        public Guid CustomerId { get; set; } // Id khách hàng
        
        public Guid? PotentialId { get; set; } // Id tiềm năng

        public string? PotentialName { get; set; } // Tên tiềm năng


        public string? LastMiddleName { get; set; } // họ và đệm

        public string FirstName { get; set; } // tên

        public string? FullName { get; set; } // Họ và tên


        public string? CompanyPhoneNum { get; set; } // Điện thoại cơ quan

        public string? CustomerPhoneNum { get; set; } // Điện thoại di động

        public string? OtherPhoneNum { get; set; } // Điện thoại khác

        public string? CustomerEmail { get; set; } // Email cá nhân

        public string? CompanyEmail { get; set; } // Email cơ quan

        public string? TaxCode { get; set; } // Mã số thuế

        public string? Zalo { get; set; } // Zalo


        public int? AddressId { get; set; }

        public int? OrganizationId { get; set; } // Id tổ chức

        public string? OrganizationName { get; set; } // Tên tổ chức



        public string? Description { get; set; } // Mô tả

        public bool? SharingUse { get; set; } // Dùng chung (0- không dùng chung, 1- dùng chung)

        public bool? DisableCall { get; set; }// Không gọi điện (0- có thể gọi điện, 1- không thể gọi điện)

        public bool? DisableMail { get; set; }// Không gửi mai(0- có thể gửi mail, 1- không thể gửi mail)

        public int? Gender { get; set; }// GIới tính(0- nam, 1- nữ, 2- khác)

        public string? GenderName { 
            get
            {
                return Gender switch
                {
                    0 => "Nam",
                    1 => "Nữ",
                    _ => "Khác",
                };
            }
            set { }
        }

        public DateTime? DateOfBirth {get;set;}// Ngày sinh

        public string? Facebook { get; set; } // Facebook

        public string? Layout { get; set; } // Bố cục

        public string? Owner { get; set; } // Chủ sở hữu

        public int? Vocative { get; set; } // xưng hô(0- ông, 1- bà, 2- anh, 3-chị, 4- cô, 5- chú, 6- bác, 7- em)

        public string? VocativeName {
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
            set { } }

        public string? DepartmentName { get; set; } // tên phòng ban

        public int? DepartmentId { get; set; } // Id phòng ban

        public int? PositionId { get; set; } // Id vị trí

        public string? PositionName { get; set; } // Tên vị trí


        public int? SourceId { get; set; } // Id nguồn gốc

        public string? SourceName { get; set; } // Tên nguồn gốc

       

        public string? AddressName { get; set; } // địa chỉ

        public string? CityName { get; set; } // thành phố

        public string? DistrictName { get; set; } // quận / huyên

        public string? WardName { get; set; } // xã

    


        public string? TypeName { get; set; } // Tên loại hình(thuộc Organization)

        public string? Domain { get; set; } // tên lĩnh vực
       

        public string? RevenueName { get; set; } // Tên Doanh số (thuộc Organizaiton)
       
        public DateTime? CreatedAt { get; set; } // Ngày tạo

        public string? CreatedBy { get; set; } // Người tạo 

        public DateTime? ModifiedAt { get; set; } // Ngày sửa đổi gần nhất

        public string? ModifiedBy { get; set; } // Người sửa đổi gần nhất

        public Customer(Guid customerId, Guid? potentialId, string? fullName,  string? potentialName, string? lastMiddleName, string firstName, string? companyPhoneNum, string? customerPhoneNum, string? otherPhoneNum, string? customerEmail, string? companyEmail, string? taxCode, string? zalo, int? addressId, int? organizationId, string? organizationName, string? description, bool? sharingUse, bool? disableCall, bool? disableMail, int? gender, string? genderName, DateTime? dateOfBirth, string? facebook, string? layout, string? owner, int? vocative, string? vocativeName, string? departmentName, int? departmentId, int? positionId, string? positionName, int? sourceId, string? sourceName, string? addressName, string? cityName, string? districtName, string? wardName, string? typeName, string? domain, string? revenueName, DateTime? createdAt, string? createdBy, DateTime? modifiedAt, string? modifiedBy)
        {
            CustomerId = customerId;
            PotentialId = potentialId;
            PotentialName = potentialName;
            LastMiddleName = lastMiddleName;
            FirstName = firstName;
            FullName = fullName;
            CompanyPhoneNum = companyPhoneNum;
            CustomerPhoneNum = customerPhoneNum;
            OtherPhoneNum = otherPhoneNum;
            CustomerEmail = customerEmail;
            CompanyEmail = companyEmail;
            TaxCode = taxCode;
            Zalo = zalo;
            AddressId = addressId;
            OrganizationId = organizationId;
            OrganizationName = organizationName;
            Description = description;
            SharingUse = sharingUse;
            DisableCall = disableCall;
            DisableMail = disableMail;
            Gender = gender;
            GenderName = genderName;
            DateOfBirth = dateOfBirth;
            Facebook = facebook;
            Layout = layout;
            Owner = owner;
            Vocative = vocative;
            VocativeName = vocativeName;
            DepartmentName = departmentName;
            DepartmentId = departmentId;
            PositionId = positionId;
            PositionName = positionName;
            SourceId = sourceId;
            SourceName = sourceName;
            AddressName = addressName;
            CityName = cityName;
            DistrictName = districtName;
            WardName = wardName;
            TypeName = typeName;
            Domain = domain;
            RevenueName = revenueName;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            ModifiedAt = modifiedAt;
            ModifiedBy = modifiedBy;
        }

        public Customer()
        {
        }


    }
}
