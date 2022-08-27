using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng Loại hình
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Types
    {
        public int TypeId { get; set; } // Id loại hình(thuộc Organization)
        public string? TypeName { get; set; } // Tên loại hình(thuộc Organization)

        public Types(int typeId, string typeName)
        {
            TypeId = typeId;
            TypeName = typeName;
        }
        public Types()
        {

        }
    }
}
