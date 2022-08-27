using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NameCustomer
    {
        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Họ và đệm
        /// </summary>
        public string? LastMiddleName { get; set; }

        public NameCustomer(string firstName, string? lastMiddleName)
        {
            FirstName = firstName;
            LastMiddleName = lastMiddleName;
        }

        public NameCustomer()
        {
        }
    }
}
