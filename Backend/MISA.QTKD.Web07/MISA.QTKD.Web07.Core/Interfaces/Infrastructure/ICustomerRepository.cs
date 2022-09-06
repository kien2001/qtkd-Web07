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
    /// ĐỊnh nghĩa interface cho CustomerRepository
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public interface ICustomerRepository
    {
        Result UpdateMultiple(UpdatedMultiple<Guid> updatedMultiple);

        Result Insert(Customer customer);

        Result Update(CustomerUpdate customer, Guid customerId);

        Result Delete(IEnumerable<Guid> listCustomerId);

        Result GetPaging(int? pageSize, int? pageIndex, string? keyword, FilterObject[]? listFilter);

    }
}
