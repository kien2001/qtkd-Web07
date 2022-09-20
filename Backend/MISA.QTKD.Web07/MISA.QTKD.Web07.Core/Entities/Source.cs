using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng Nguồn gốc
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Source
    {
        #region Source

        public int SourceId { get; set; } // Id nguồn gốc
        public string? SourceName { get; set; } // Tên nguồn gốc

        #endregion

        public Source(int sourceId, string sourceName)
        {
            SourceId = sourceId;
            SourceName = sourceName;
        }

        public Source()
        {
        }
    }
}
