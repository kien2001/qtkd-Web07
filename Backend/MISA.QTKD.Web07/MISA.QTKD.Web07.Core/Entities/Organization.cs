using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Ánh xạ với bảng Organization
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Organization
    {
        #region Organization

        public int? OrganizationId { get; set; }// Id tổ chức
        public string? OrganizationName { get; set; } // Tên tổ chức

        public string? BankAccount { get; set; }// Tên tài khoản

        public string? BankName { get; set; }// Tên Ngân hàng

        public DateTime? CreatedAccountAt { get; set; }// Ngày thành lập tài khoản

        public int? TypeId { get; set; }// Id loại hình

        public string? Domain { get; set; } // Tên lĩnh vực


        public string? Career { get; set; }// tên nghề nghiệp

        public int? RevenueId { get; set; }// Id tiềm năng

        #endregion

        public Organization(int? organizationId, string? organizationName, string? bankAccount, string? bankName, DateTime? createdAccountAt, int? typeId, string? domain, string? career, int? revenueId)
        {
            OrganizationId = organizationId;
            OrganizationName = organizationName;
            BankAccount = bankAccount;
            BankName = bankName;
            CreatedAccountAt = createdAccountAt;
            TypeId = typeId;
            Domain = domain;
            Career = career;
            RevenueId = revenueId;
        }

        public Organization()
        {
        }
    }
}
