using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Constants;
using Entities;
using Enums;
using Exceptions;
using Infrastructure;
using Resources.Vi;
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
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            DateTime today = DateTime.Today;
            //1.2 Thông tin tên không được trống
            if (string.IsNullOrEmpty(customerInsert.Customer.FirstName.Trim()))
            {
                check = false;
                result.Data = new { };
                result.DevMsg.Add(CodeError.EmptyFirstName);
                result.UserMsg.Add(MessageError.EmptyFirstName);
                result.StatusCode = StatusCode.Status400BadRequest;

                result.Flag = false;
            }
            if ((customerInsert.Customer.CompanyEmail != null && !IsValidEmail(email: customerInsert.Customer.CompanyEmail)) || (customerInsert.Customer.CustomerEmail != null && !IsValidEmail(email: customerInsert.Customer.CustomerEmail)))
            {
                check = false;


                result.Data = new { };
                result.DevMsg.Add( CodeError.NotValidEmail);
                result.UserMsg.Add( MessageError.NotValidEmail);
                result.StatusCode = StatusCode.Status400BadRequest;

                result.Flag = false;
            }
            if ((customerInsert.Customer.CustomerPhoneNum != null && !IsValidPhoneNumber(customerInsert.Customer.CustomerPhoneNum)) || (customerInsert.Customer.Zalo != null && !IsValidPhoneNumber(customerInsert.Customer.Zalo)) || (customerInsert.Customer.CompanyPhoneNum != null && !IsValidPhoneNumber(customerInsert.Customer.CompanyPhoneNum)))
            {
                check = false;


                result.Data = new { };
                result.DevMsg.Add( CodeError.NotValidPhone);
                result.UserMsg.Add( MessageError.NotValidPhone);
                result.StatusCode = StatusCode.Status400BadRequest;

                result.Flag = false;
            }
            if (customerInsert.Potential != null && _potentialRepository.CheckDuplicateCode(customerInsert.Potential.PotentialCode))
            {
                check = false;

                result.Data = new { };
                result.DevMsg.Add( CodeError.DuplicatePotentialCode);
                result.UserMsg.Add( MessageError.DuplicatePotentialCode);
                result.StatusCode = StatusCode.Status400BadRequest;

                result.Flag = false;
            }
            // Nếu ngày đã nhập lớn hơn hiện tại, trả về lỗi
            if (customerInsert.Organization.CreatedAccountAt != null && DateTime.Compare(today, (DateTime)customerInsert.Organization.CreatedAccountAt) < 0)
            {
                check = false;

                result.Data = new { };
                result.DevMsg.Add( CodeError.NotValidDateError);
                result.UserMsg.Add( MessageError.NotValidDateError);
                result.StatusCode = StatusCode.Status400BadRequest;

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
                result.DevMsg.Add( CodeError.EmptyFirstName);
                result.UserMsg.Add( MessageError.EmptyFirstName);
                result.Flag = false;
            }
            if ((customer.CompanyEmail != null && !IsValidEmail(email: customer.CompanyEmail))
                || (customer.CustomerEmail != null && !IsValidEmail(email: customer.CustomerEmail))
               )
            {
                check = false;
                result.Data = new { };
                result.DevMsg.Add( CodeError.NotValidEmail);
                result.UserMsg.Add( MessageError.NotValidEmail);
                result.Flag = false;
            }
            if ((customer.CustomerPhoneNum != null && !IsValidPhoneNumber(customer.CustomerPhoneNum)) || (customer.OtherPhoneNum != null && !IsValidPhoneNumber(customer.OtherPhoneNum)) || (customer.CompanyPhoneNum != null && !IsValidPhoneNumber(customer.CompanyPhoneNum)))
            {
                check = false;


                result.Data = new { };
                result.DevMsg.Add( CodeError.NotValidPhone);
                result.UserMsg.Add( MessageError.NotValidPhone);
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


        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            var trimmedPhoneNumber = phoneNumber.Trim();
            try
            {
                Regex validatePhoneNumberRegex = new("^0[1-9][0-9]{7,14}$");
                return validatePhoneNumberRegex.IsMatch(trimmedPhoneNumber);
            }
            catch 
            {
                return false;
            }
            
        }

        /// <summary>
        /// Xử lý tạo ra file excel
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// Created by LVKIEN 11/09/2022
        public Result ExportExcel(List<Guid> customerId)
        {
            Result customerResult = _customerRepository.GetDataExcel(customerId);
            try
            {
                if (customerResult.Flag)
                {


                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Potential");
                    var currentRow = 1;
                    int[] widthArr = { 35, 15, 30, 20, 20, 20, 30, 30, 30, 30, 25, 25, 20, 20, 20, 35, 40, 30, 20, 30, 20, 35 };

                    #region Header
                    foreach (TableField.ListField field in Enum.GetValues(typeof(TableField.ListField)))
                    {
                        worksheet.Cell(currentRow, (int)field).Value = field.ToDescription();

                        // Set PatternType
                        worksheet.Cell(currentRow, (int)field).Style.Fill.PatternType = XLFillPatternValues.Gray0625;
                        // Set Màu cho Background
                        worksheet.Cell(currentRow, (int)field).Style.Fill.BackgroundColor = XLColor.FromName("Green");
                        // Set Font cho text  trong Range hiện tại
                        worksheet.Cell(currentRow, (int)field).Style.Font.FontName = "Arial";
                        worksheet.Cell(currentRow, (int)field).Style.Font.FontSize = 16;

                        worksheet.Row(currentRow).Height = 30;

                        worksheet.Cell(currentRow, (int)field).Style.Font.Bold = true;
                        worksheet.Column((int)field).Width = widthArr[(int)field - 1];
                        worksheet.Cell(currentRow, (int)field).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, (int)field).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, (int)field).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(currentRow, (int)field).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    }
                    #endregion

                    #region Body
                    List<CustomerTable> customerTableList = (List<CustomerTable>)customerResult.Data;
                    if (customerTableList != null && customerTableList.Any())
                    {
                        foreach (var customer in customerTableList)
                        {
                            currentRow++;
                            Type customerType = customer.GetType();
                            foreach (int field in Enum.GetValues(typeof(TableField.ListField)))
                            {
                                var key = Enum.GetName(typeof(TableField.ListField), field);
                                if (key != null)
                                {
                                    var keyCustomer = customerType.GetProperty(key);
                                    if (keyCustomer != null)
                                    {
                                        int index = customerTableList.IndexOf(customer) + 2;
                                        worksheet.Cell(currentRow, field).SetValue(keyCustomer.GetValue(customer));
                                        worksheet.Row(index).Height = 20;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    customerResult.Flag = true;
                    customerResult.DevMsg.Clear();
                    customerResult.DevMsg.Add( CodeSuccess.ExportSuccess);
                    customerResult.UserMsg.Clear();
                    customerResult.UserMsg.Add( MessageSuccess.ExportSuccess);
                    customerResult.Data = content;
                }
            }
            catch (Exception e)
            {
                customerResult.DevMsg.Add( CodeError.ExportExcelError);
                customerResult.UserMsg.Add( MessageError.ExportExcelError);
                customerResult.StatusCode = StatusCode.Status500InternalServerError;

            }
            return customerResult;
        }
    }
}
