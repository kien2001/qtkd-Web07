using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTKD.Web07.Core.Entities
{
    /// <summary>
    /// Ánh xạ với bảng Position
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Position
    {
        public int PositionId { get; set; }// Id Vị trí
        public string? PositionName { get; set; }// Tên vị trí

        public Position(int positionId, string positionName)
        {
            PositionId = positionId;
            PositionName = positionName;
        }

        public Position()
        {
        }
    }
}
