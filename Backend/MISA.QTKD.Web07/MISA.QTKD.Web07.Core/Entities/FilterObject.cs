using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Entities
{
    /// <summary>
    /// Dữ liệu của FrontEnd filter gửi lên
    /// </summary>
    /// Created by LVKIEN 13/08/2022
    public class FilterObject
    {

        #region FilterObject

        /// <summary>
        /// Tên cột 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Loại lọc
        /// </summary>
        public FilterMethod Type { get; set; }

        /// <summary>
        /// Dữ liệu
        /// </summary>
        public dynamic? Value { get; set; }

        #endregion


        public FilterObject(string name, FilterMethod type, dynamic? value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public FilterObject()
        {
        }


        public static dynamic ConvertToType(object obj)
        {
            //If you're unsure of the type you want to return.
            return Convert.ChangeType(obj, obj.GetType());
        }

        public static T ConvertToType<T>(object obj)
        {
            //If you definitely know the type you want to return.
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}
