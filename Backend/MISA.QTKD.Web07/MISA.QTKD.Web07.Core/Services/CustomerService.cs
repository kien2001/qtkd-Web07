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
        IAddressRepository _addressRepository;
        IOrganizationRepository _organizationRepository;
        IPotentialRepository _potentialRepository;

        public CustomerService(ICustomerRepository customerRepository, IAddressRepository addressRepository, IOrganizationRepository organizationRepository, IPotentialRepository potentialRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _organizationRepository = organizationRepository;
            _potentialRepository = potentialRepository;

        }
        /// <summary>
        /// Xử lý nghiệp vụ validate dữ liệu khi insert bảng customer
        /// </summary>
        /// <param name="customerInsert"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        public Result InsertService(CustomerInsert customerInsert)
        {
            bool check = true;
            Result result = new();
            result.UserMsg = new List<string>();
            result.DevMsg = new List<string>();
            //1.2 Thông tin tên không được trống
            if (string.IsNullOrEmpty(customerInsert.Customer.FirstName.Trim()))
            {
                check = false;
                result.Data = new { };
                result.DevMsg.Add(FailMessage.CodeError.EmptyFirstName);
                result.UserMsg.Add(FailMessage.MessageError.EmptyFirstName);
                result.Flag = false;
            }
            if ((customerInsert.Customer.CompanyEmail != null && !IsValidEmail(email: customerInsert.Customer.CompanyEmail)) || (customerInsert.Customer.CustomerEmail != null && !IsValidEmail(email: customerInsert.Customer.CustomerEmail)))
            {
                check = false;


                result.Data = new { };
                result.DevMsg.Add(FailMessage.CodeError.NotValidEmail);
                result.UserMsg.Add(FailMessage.MessageError.NotValidEmail);
                result.Flag = false;
            }
            if (customerInsert.Potential != null &&  _potentialRepository.CheckDuplicateCode(customerInsert.Potential.PotentialCode))
            {
                check = false;

                result.Data = new { };
               result.DevMsg.Add(FailMessage.CodeError.DuplicatePotentialCode);
               result.UserMsg.Add(FailMessage.MessageError.DuplicatePotentialCode);
                result.Flag = false;
            }
            if (check)
            {
                var resultAddress = _addressRepository.Insert(customerInsert.Address);
                if (!resultAddress.Flag)
                {
                    result.Data = new { };
                    result.DevMsg.Add(resultAddress.DevMsg.FirstOrDefault());
                    result.UserMsg.Add(resultAddress.UserMsg.FirstOrDefault());
                    result.Flag = false;
                }
                else
                {
                    customerInsert.Customer.AddressId = Convert.ToInt32(resultAddress.Data);
                }

                var resultOrganization = _organizationRepository.Insert(customerInsert.Organization);
                if (!resultOrganization.Flag)
                {
                    result.Data = new { };
                    result.DevMsg.Add(resultOrganization.DevMsg.FirstOrDefault());
                   result.UserMsg.Add(resultOrganization.UserMsg.FirstOrDefault());
                    result.Flag = false;
                }
                else
                {
                    customerInsert.Customer.OrganizationId = Convert.ToInt32(resultOrganization.Data);
                }

                var resultPotential = _potentialRepository.Insert(customerInsert.Potential);
                if (!resultPotential.Flag)
                {
                    result.Data = new { };
                    result.DevMsg.Add(resultPotential.DevMsg.FirstOrDefault());
                    result.UserMsg.Add(resultPotential.UserMsg.FirstOrDefault());
                    result.Flag = false;
                }
                else
                {
                    customerInsert.Customer.PotentialId = (Guid?)resultPotential.Data;
                }

                var resultCustomer = _customerRepository.Insert(customerInsert.Customer);
                if (!resultCustomer.Flag)
                {
                    result.Data = new { };
                    result.DevMsg.Add(resultCustomer.DevMsg.FirstOrDefault());
                     result.UserMsg.Add(resultCustomer.UserMsg.FirstOrDefault());
                    result.Flag = false;
                }
                else
                {
                    result.Data = resultCustomer.Data;
                    result.DevMsg.Add(resultCustomer.DevMsg.FirstOrDefault());
                    result.UserMsg.Add(resultCustomer.UserMsg.FirstOrDefault());
                    result.Flag = true;
                }
            }



            return result;
        }

        public Result UpdateService(CustomerUpdate customer, Guid customerId)
        {
            bool check = true;
            Result result = new();
            result.UserMsg = new List<string>();
            result.DevMsg = new List<string>();
            //1.2 Thông tin tên không được trống
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                check = false;
                result.Data = new { };
                result.DevMsg.Add(FailMessage.CodeError.EmptyFirstName);
                result.UserMsg.Add(FailMessage.MessageError.EmptyFirstName);
                result.Flag = false;
            }
            if ((customer.CompanyEmail != null && !IsValidEmail(email: customer.CompanyEmail)) 
                || (customer.CustomerEmail != null && !IsValidEmail(email: customer.CustomerEmail))
               )
            {
                check = false;
                result.Data = new { };
                result.DevMsg.Add(FailMessage.CodeError.NotValidEmail);
                result.UserMsg.Add(FailMessage.MessageError.NotValidEmail);
                result.Flag = false;
            }
            if (check)
            {
                result = _customerRepository.Update(customer, customerId);
            }
            return result;
        }
        /// <summary>
        /// Kiểm tra email có hợp lệ hay ko
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// Created by LVKIEN 1/9/2022
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
