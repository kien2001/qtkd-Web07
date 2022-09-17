using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Lưu những thông tin cần hiển thị ở trong bảng ở frontend 
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class CustomerInsert
    {
        #region Customer

        public Customer Customer { get; set; }

        #endregion

        #region Address

        public Address? Address { get; set; }

        #endregion

        #region Organization

        public Organization? Organization { get; set; }

        #endregion


        #region Potential

        public Potential? Potential { get; set; }

        #endregion



        public CustomerInsert(Customer customer, Address? address, Organization? organization, Potential? potential)
        {
            Customer = customer;
            Address = address;
            Organization = organization;
            Potential = potential;
        }

        public CustomerInsert()
        {
        }
    }
}
