using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Infrastructure
{
    /// <summary>
    /// ĐỊnh nghĩa interface cho VOcativeRepository
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public interface IVocativeRepository
    {
        Result Get();
    }
}
