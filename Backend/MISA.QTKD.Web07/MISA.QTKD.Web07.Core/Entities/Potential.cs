using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Ánh xạ với bảng Potential
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class Potential
    {
        #region Potential

        public Guid? PotentialId { get; set; } // id Tiềm năng
        public string? PotentialName { get; set; }// tên tiềm năng
        public string? PotentialCode { get; set; }// Mã tiềm năng

        #endregion


        public Potential(Guid? potentialId, string? potentialName, string? potentialCode)
        {
            PotentialId = potentialId;
            PotentialName = potentialName;
            PotentialCode = potentialCode;
        }

        public Potential()
        {
        }
    }
}
