using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UpdatedMultiple<T>
    {
        /// <summary>
        /// Danh sách Id các trường cần sửa
        /// </summary>
        public List<T> ListId { get; set; }
        /// <summary>
        /// Tên trường cần sửa
        /// </summary>
        public string FieldUpdateName { get; set; }
        /// <summary>
        /// Giá trị cần sửa
        /// </summary>
        public string FieldUpdateValue { get; set; }

        public UpdatedMultiple(List<T> listId, string fieldUpdateName, string fieldUpdateValue)
        {
            ListId = listId;
            FieldUpdateName = fieldUpdateName;
            FieldUpdateValue = fieldUpdateValue;
        }

        //public UpdatedMultiple()
        //{
        //}
    }
}
