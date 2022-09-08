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
        public Customer Customer { get; set; }

        public Address? Address { get; set; }

        public Organization? Organization { get; set; }

        public Potential? Potential { get; set; }

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
