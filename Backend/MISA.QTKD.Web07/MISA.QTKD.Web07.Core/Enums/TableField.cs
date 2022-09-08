using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public static class TableField
    {
        public enum ListField
        {
            [Description("Id Khách hàng")]
            CustomerId = 1,


            [Description("Xưng hô")]
            VocativeName,

            [Description("Họ và tên")]
            FullName,

            [Description("Chức danh")]
            PositionName,

            [Description("ĐT di động")]
            CustomerPhoneNum,

            [Description("ĐT cơ quan")]
            CompanyPhoneNum,

            [Description("Email cơ quan")]
            CompanyEmail,

            [Description("Email cá nhân")]
            CustomerEmail,

            [Description("Tổ chức")]
            OrganizationName,

            [Description("Địa chỉ")]
            AddressName,

            [Description("Tỉnh/Thành phố")]
            CityName,

            [Description("Quận/Huyện")]
            DistrictName,

            [Description("Phường/Xã")]
            WardName,

            [Description("Nguồn gốc")]
            SourceName,

            [Description("Loại hình")]
            TypeName,

            [Description("Lĩnh vực")]
            Domain,

            [Description("Mô tả")]
            Description,

            [Description("Bố cục")]
            Layout,

            [Description("Chủ sở hữu")]
            Owner,

            [Description("Doanh thu")]
            RevenueName,

            [Description("Dùng chung")]
            SharingUseName,

            [Description("Facebook")]
            Facebook

        }
        /// <summary>
        /// Given an enum value, if a <see cref="DescriptionAttribute"/> attribute has been defined on it, then return that.
        /// Otherwise return the enum name.
        /// </summary>
        /// <typeparam name="T">Enum type to look in</typeparam>
        /// <param name="value">Enum value</param>
        /// <returns>Description or name</returns>
        public static string ToDescription<T>(this T value) where T : struct
        {
            var fieldName = Enum.GetName(typeof(T), value);
            if (fieldName == null)
            {
                return string.Empty;
            }
            var fieldInfo = typeof(T).GetField(fieldName, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static);
            if (fieldInfo == null)
            {
                return string.Empty;
            }
            var descriptionAttribute = (DescriptionAttribute)fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            if (descriptionAttribute == null)
            {
                return fieldInfo.Name;
            }
            return descriptionAttribute.Description;
        }
    }
}
