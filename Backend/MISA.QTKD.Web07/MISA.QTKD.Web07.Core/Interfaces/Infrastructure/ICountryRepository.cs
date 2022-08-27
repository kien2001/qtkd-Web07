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
    /// ĐỊnh nghĩa interface cho CountryRepository
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public interface ICountryRepository
    {
        Result Get();
    }
}
