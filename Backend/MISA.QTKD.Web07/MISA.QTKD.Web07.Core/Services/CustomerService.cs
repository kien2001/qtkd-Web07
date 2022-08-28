using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants;
using Entities;
using Exceptions;
using Infrastructure;
using MISA.QTKD.Web07.Core.Entities;
using MISA.QTKD.Web07.Core.Interfaces.Services;

namespace Services
{
    public class CustomerService : ICustomerService
    {

        ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// Xử lý nghiệp vụ validate dữ liệu khi insert bảng customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        public Result InsertService(Customer customer)
        {
            Result result = new();
            //1.2 Thông tin tên không được trống
            if (string.IsNullOrEmpty(customer.FirstName))
            {

                result.Data = new { };
                result.DevMsg= FailMessage.CodeError.EmptyFirstName;
                result.UserMsg= FailMessage.MessageError.EmptyFirstName;
                result.Flag= false;
            }
            else
            {
                result = _customerRepository.Insert(customer);
            }

            
            return result;
        }   

        public Result UpdateService(CustomerUpdate customer, Guid customerId)
        {
            Result result = new();
            //1.2 Thông tin tên không được trống
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                result.Data = new { };
                result.DevMsg = FailMessage.CodeError.EmptyFirstName;
                result.UserMsg = FailMessage.MessageError.EmptyFirstName;
                result.Flag = false;
            }
            else
            {
                result = _customerRepository.Update(customer, customerId);
            }


            return result;
        }
    }
}
