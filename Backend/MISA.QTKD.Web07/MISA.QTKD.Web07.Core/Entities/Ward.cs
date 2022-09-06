using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Ánh xạ với bảng Xã
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Ward
    {
        public int WardId { get; set; }// Id xã

        public string? WardName { get; set; } // Tên xã

        public Ward(int wardId, string wardName)
        {
            WardId = wardId;
            WardName = wardName;
        }
    }
}
