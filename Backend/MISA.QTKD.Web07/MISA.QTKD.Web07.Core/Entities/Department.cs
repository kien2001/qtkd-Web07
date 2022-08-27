using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng Department
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Department
    {
        public int DepartmentId { get; set; }    // Id phòng ban
        public string? DepartmentName { get; set; } // Tên phòng ban

        public Department(int departmentId, string departmentName)
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }

        public Department()
        {
        }
    }
}
