using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MISA.QTKD.Web07.Core.Entities;

namespace Infrastructure
{
    /// <summary>
    /// ĐỊnh nghĩa interface cho DistrictRepository
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public interface IDistrictRepository
    {

        Result GetAll();

        Result Get(int cityId);
    }
}
