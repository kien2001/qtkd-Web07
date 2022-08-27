using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Lưu thông tin cập nhật bảng Customer và các bảng liên quan
    /// </summary>
    /// Created by LVKIEN 25/08/2022
    public class CustomerUpdate
    {

        public Guid? PotentialId { get; set; } // Id tiềm năng

        public string? PotentialName { get; set; } // Tên tiềm năng


        public string? LastMiddleName { get; set; } // họ và đệm

        public string FirstName { get; set; } // tên

        public string? CompanyPhoneNum { get; set; } // Điện thoại cơ quan

        public string? CustomerPhoneNum { get; set; } // Điện thoại di động

        public string? OtherPhoneNum { get; set; } // Điện thoại khác

        public string? CustomerEmail { get; set; } // Email cá nhân

        public string? CompanyEmail { get; set; } // Email cơ quan

        public string? TaxCode { get; set; } // Mã số thuế

        public string? Zalo { get; set; } // Zalo

        public int? OrganizationId { get; set; } // Id tổ chức

        public string? OrganizationName { get; set; } // Tên tổ chức

        public bool? DisableCall { get; set; }// Không gọi điện (0- có thể gọi điện, 1- không thể gọi điện)

        public bool? DisableMail { get; set; }// Không gửi mai(0- có thể gửi mail, 1- không thể gửi mail)

        public int? Gender { get; set; }// GIới tính(0- nam, 1- nữ, 2- khác)

        public DateTime? DateOfBirth { get; set; }// Ngày sinh

        public string? Facebook { get; set; } // Facebook

        public int? Vocative { get; set; } // xưng hô(0- ông, 1- bà, 2- anh, 3-chị, 4- cô, 5- chú, 6- bác, 7- em)

        public int? DepartmentId { get; set; } // Id phòng ban

        public int? PositionId { get; set; } // Id vị trí

        public int? SourceId { get; set; } // Id nguồn gốc

        public DateTime? ModifiedAt { get; set; } // Ngày sửa đổi gần nhất

        public CustomerUpdate( Guid? potentialId, string? potentialName, string? lastMiddleName, string firstName, string? companyPhoneNum, string? customerPhoneNum, string? otherPhoneNum, string? customerEmail, string? companyEmail, string? taxCode, string? zalo, int? organizationId, string? organizationName, bool? disableCall, bool? disableMail, int? gender, DateTime? dateOfBirth, string? facebook, int? vocative, int? departmentId, int? positionId, int? sourceId, DateTime? modifiedAt)
        {
            PotentialId = potentialId;
            PotentialName = potentialName;
            LastMiddleName = lastMiddleName;
            FirstName = firstName;
            CompanyPhoneNum = companyPhoneNum;
            CustomerPhoneNum = customerPhoneNum;
            OtherPhoneNum = otherPhoneNum;
            CustomerEmail = customerEmail;
            CompanyEmail = companyEmail;
            TaxCode = taxCode;
            Zalo = zalo;
            OrganizationId = organizationId;
            OrganizationName = organizationName;
            DisableCall = disableCall;
            DisableMail = disableMail;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Facebook = facebook;
            Vocative = vocative;
            DepartmentId = departmentId;
            PositionId = positionId;
            SourceId = sourceId;
            ModifiedAt = modifiedAt;
        }

        public CustomerUpdate()
        {
        }
    }
}
