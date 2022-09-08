using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FilterObject
    {
      

        /// <summary>
        /// Tên cột 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Loại lọc
        /// </summary>
        public int Type { get; set; }

        public dynamic? Value { get; set; }

        public FilterObject(string name, int type, dynamic? value)
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
