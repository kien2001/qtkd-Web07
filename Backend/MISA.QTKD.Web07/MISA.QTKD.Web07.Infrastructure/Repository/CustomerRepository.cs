using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.QTKD.Web07.Core.Entities;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.Numerics;
using Infrastructure;
using Exceptions;
using Entities;
using Constants;
using System.Collections;
using System.Reflection;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Check trùng mã tiềm năng
        /// </summary>
        /// <param name="potentialId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEn 18/05/2022
        public Result CheckDuplicateCode(Guid potentialId)
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);

                var sqlCheck = "Select PotentialId from customer where PotentialId = @PotentialId";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@PotentialId", potentialId);
                var potentialIdDuplicate = mySqlConnection.QueryFirstOrDefault<string>(sql: sqlCheck, param: dynamicParams);
                if (potentialIdDuplicate == null)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;
                }
                else
                {
                    result.Data = potentialIdDuplicate;
                    result.DevMsg = SuccessMessage.CodeSuccess.GetSuccess;
                    result.UserMsg = SuccessMessage.MessageSuccess.GetSuccess;
                    result.Flag = true;
                }
            }
            catch (Exception ex)
            {

                result.Data = ex.Message;
                result.DevMsg = FailMessage.CodeError.ProcessError;
                result.UserMsg = FailMessage.MessageError.ProcessError;
                result.Flag = false;
            }
            return result;

        }


        /// <summary>
        /// Xoá các bản ghi trong Customer
        /// </summary>
        /// <param name="listCustomerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEn 25/08/2022
        public Result Delete(IEnumerable<Guid> listCustomerId)
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                bool checkSuccess = true;
                int count = 0;
                foreach (var item in listCustomerId)
                {
                    string processQuery = "DELETE FROM customer WHERE CustomerId =  @CustomerId";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@CustomerId", item);
                    var res = mySqlConnection.Execute(processQuery, dynamicParams);
                    if (res == 0)
                    {
                        checkSuccess = false;
                        break;
                    }
                    count++;
                }

                if (checkSuccess == false)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.DeletedFail;
                    result.UserMsg = FailMessage.MessageError.DeleteFail;
                    result.Flag = false;
                }
                else
                {
                    result.Data = count;
                    result.DevMsg = SuccessMessage.CodeSuccess.DeleteSuccess;
                    result.UserMsg = SuccessMessage.MessageSuccess.DeleteSuccess;
                    result.Flag = true;
                }
            }
            catch (Exception ex)
            {

                result.Data = ex.Message;
                result.DevMsg = FailMessage.CodeError.ProcessError;
                result.UserMsg = FailMessage.MessageError.ProcessError;
                result.Flag = false;
            }
            return result;
        }
        /// <summary>
        /// Sửa nhiều bản ghi trong bảng Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/05/2022
        public Result UpdateMultiple(UpdatedMultiple<Guid> updatedMultiple)
        {
            Result result = new();
            try
            {
                var stringIdList = updatedMultiple.ListId.ConvertAll<string>(g => g.ToString());
                var updateQuery = "";
                object parameters ;

                if (updatedMultiple.FieldUpdateName == "FullName")
                {
                    NameCustomer name = HandleGetName(updatedMultiple.FieldUpdateValue);

                    updateQuery = @"Update customer set FirstName = @FirstNameValue,LastMiddleName = @LastMiddleName where CustomerId in @ListId ;";
                    parameters = new
                    {
                        FirstNameValue = name.FirstName,
                        LastMiddleName = name.LastMiddleName,
                        ListId = stringIdList
                    };
                }
                else
                {
                    updateQuery = $@"Update customer set {updatedMultiple.FieldUpdateName} = @FieldUpdateValue where CustomerId in @ListId ;";
                    var resultString = String.Join(", ", stringIdList);
                    parameters = new
                    {
                        FieldUpdateValue = updatedMultiple.FieldUpdateValue,
                        ListId = stringIdList
                    };
                }
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                var res = mySqlConnection.Execute(updateQuery, parameters);

                if (res == 0)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.UpdateFailed;
                    result.UserMsg = FailMessage.MessageError.UpdateFail;
                    result.Flag = false;
                }
                else
                {

                    result.Data = res;
                    result.DevMsg = SuccessMessage.CodeSuccess.UpdateSuccess;
                    result.UserMsg = SuccessMessage.MessageSuccess.UpdateSuccess;
                    result.Flag = true;
                }

            }
            catch (Exception ex)
            {

                result.Data = ex.Message;
                result.DevMsg = FailMessage.CodeError.ProcessError;
                result.UserMsg = FailMessage.MessageError.ProcessError;
                result.Flag = false;
            }
            return result;

        }

        public Result GetPaging(int ?pageSize, int ?pageIndex, string? keyword)
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                var pagingProc = "Proc_Customer_GetPaging";
                var orConditions = new List<string>();
                string whereClause = "";
                if(keyword != "null")
                {
                    orConditions.Add($"FullName LIKE '%{keyword}%'");
                    orConditions.Add($"CustomerPhoneNum LIKE '%{keyword}%'");
                    orConditions.Add($"CustomerEmail LIKE '%{keyword}%'");

                }
                if(orConditions.Count > 0)
                {
                    whereClause = $"({string.Join(" OR ", orConditions)})";
                }
                var parameters = new
                {
                    v_Offset = pageIndex,
                    v_Limit = pageSize,
                    v_Sort = default(Plane[]),
                    v_Where = whereClause

                };

                var multipleResults = mySqlConnection.QueryMultiple(sql: pagingProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (multipleResults == null)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;
                }
                else
                {
                    var customerTable = multipleResults.Read<Customer>();
                    var totalCount = multipleResults.Read<long>().Single();

                    result.Data = new { customers = customerTable, totalCount };
                    result.DevMsg = SuccessMessage.CodeSuccess.GetSuccess;
                    result.UserMsg = SuccessMessage.MessageSuccess.GetSuccess;
                    result.Flag = true;
                }
            }
            catch (Exception ex)
            {

                result.Data = ex.Message;
                result.DevMsg = FailMessage.CodeError.ProcessError;
                result.UserMsg = FailMessage.MessageError.ProcessError;
                result.Flag = false;
            }
            return result;
        }

        /// <summary>
        /// Thêm 1 bản ghi mới vào bảng Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// Created by LVKIEN 19/08/2022
        public Result Insert(Customer customer)
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                var command = "INSERT INTO customer ( CustomerId, PotentialId, LastMiddleName, " +
                    "FirstName, CompanyPhoneNum, CustomerPhoneNum, CustomerEmail, " +
                    "CompanyEmail, TaxCode, Zalo, OrganizationId, AddressId, Description, SharingUse, " +
                    "Vocative, Layout, Owner, DepartmentId, PositionId, SourceId, CreatedAt, ModifiedAt)" +
                    "values (@CustomerId, @PotentialId, @LastMiddleName, @FirstName, @CompanyPhoneNum," +
                    " @CustomerPhoneNum, @CustomerEmail, @CompanyEmail, @TaxCode, @Zalo, " +
                    "@OrganizationId, @AddressId, @Description, @SharingUse, @Vocative, @Layout, @Owner, " +
                    "@DepartmentId, @PositionId, @SourceId, @CreatedAt, @ModifiedAt)";

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@CustomerId", Guid.NewGuid());
                dynamicParams.Add("@PotentialId", customer.PotentialId);
                dynamicParams.Add("@LastMiddleName", customer.LastMiddleName);
                dynamicParams.Add("@FirstName", customer.FirstName);
                dynamicParams.Add("@CompanyPhoneNum", customer.CompanyPhoneNum);
                dynamicParams.Add("@CustomerPhoneNum", customer.CustomerPhoneNum);
                dynamicParams.Add("@CustomerEmail", customer.CustomerEmail);
                dynamicParams.Add("@CompanyEmail", customer.CompanyEmail);
                dynamicParams.Add("@TaxCode", customer.TaxCode);
                dynamicParams.Add("@Zalo", customer.Zalo);
                dynamicParams.Add("@OrganizationId", customer.OrganizationId);
                dynamicParams.Add("@AddressId", customer.AddressId);
                dynamicParams.Add("@Description", customer.Description);
                dynamicParams.Add("@SharingUse", customer.SharingUse);
                dynamicParams.Add("@Vocative", customer.Vocative);
                dynamicParams.Add("@Owner", customer.Owner);
                dynamicParams.Add("@DepartmentId", customer.DepartmentId);
                dynamicParams.Add("@Layout", customer.Layout);
                dynamicParams.Add("@PositionId", customer.PositionId);
                dynamicParams.Add("@SourceId", customer.SourceId);
                dynamicParams.Add("@CreatedAt", DateTime.Now);
                dynamicParams.Add("@ModifiedAt", DateTime.Now);

                var res = mySqlConnection.Execute(sql: command, param: dynamicParams);
                if (res == 0)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.InsertFailed;
                    result.UserMsg = FailMessage.MessageError.InsertFail;
                    result.Flag = false;

                }
                else
                {
                    result.Data = res;
                    result.DevMsg = SuccessMessage.CodeSuccess.InsertSuccess;
                    result.UserMsg = SuccessMessage.MessageSuccess.InsertSuccess;
                    result.Flag = true;
                }
            }
            catch (Exception ex)
            {

                result.Data = ex.Message;
                result.DevMsg = FailMessage.CodeError.ProcessError;
                result.UserMsg = FailMessage.MessageError.ProcessError;
                result.Flag = false;
            }
            return result;
        }

        /// <summary>
        /// Sửa đổi 1 bản ghi trong bản Customer và các bảng liên quan
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// Created by LVKIEN 25/08/2022
        public Result Update(CustomerUpdate customer, Guid customerId)
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                var updateCommand = "UPDATE customer set LastMiddleName = @LastMiddleName, FirstName = @FirstName, CompanyPhoneNum = @CompanyPhoneNum," +
                    "CustomerPhoneNum = @CustomerPhoneNum, OtherPhoneNum = @OtherPhoneNum, CustomerEmail = @CustomerEmail, CompanyEmail = @CompanyEmail, " +
                    "TaxCode = @TaxCode, Zalo = @Zalo, DisableCall = @DisableCall, DisableMail = @DisableMail, Gender = @Gender, DateOfBirth = @DateOfBirth," +
                    " Facebook = @Facebook, Vocative = @Vocative, DepartmentId =@DepartmentId, PositionId = @PositionId, SourceId = @SourceId," +
                    " ModifiedAt = @ModifiedAt WHERE CustomerId = @CustomerId";

                var parameters = new DynamicParameters();

                parameters.Add("@CustomerId", customerId);
                parameters.Add("@LastMiddleName", customer.LastMiddleName);
                parameters.Add("@FirstName", customer.FirstName);
                parameters.Add("@CompanyPhoneNum", customer.CompanyPhoneNum);
                parameters.Add("@CustomerPhoneNum", customer.CustomerPhoneNum);
                parameters.Add("@OtherPhoneNum", customer.OtherPhoneNum);
                parameters.Add("@CustomerEmail", customer.CustomerEmail);
                parameters.Add("@CompanyEmail", customer.CompanyEmail);
                parameters.Add("@TaxCode", customer.TaxCode);
                parameters.Add("@Zalo", customer.Zalo);
                parameters.Add("@DisableCall", customer.DisableCall);
                parameters.Add("@DisableMail", customer.DisableMail);
                parameters.Add("@Gender", customer.Gender);
                parameters.Add("@DateOfBirth", customer.DateOfBirth);
                parameters.Add("@Facebook", customer.Facebook);
                parameters.Add("@Vocative", customer.Vocative);
                parameters.Add("@DepartmentId", customer.DepartmentId);
                parameters.Add("@PositionId", customer.PositionId);
                parameters.Add("@SourceId", customer.SourceId);
                parameters.Add("@ModifiedAt", DateTime.Now);

                int updateCustomerRes = mySqlConnection.Execute(sql: updateCommand, param: parameters);

                if (updateCustomerRes == 0)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.UpdateFailed;
                    result.UserMsg = FailMessage.MessageError.UpdateFail;
                    result.Flag = false;
                }
                else
                {
                    var updatePotOrgaProc = "Proc_Customer_UpdateOrgaPotential";

                    var param = new
                    {
                        v_PotentialId = customer.PotentialId,
                        v_PotentialName = customer.PotentialName,
                        v_OrganizationId = customer.OrganizationId,
                        v_OrganizatonName = customer.OrganizationName,
                        v_ModifiedAt = DateTime.Now
                    };



                    int updateOrgaPotentialRes = mySqlConnection.QueryFirstOrDefault<int>(sql: updatePotOrgaProc, param: param, commandType: System.Data.CommandType.StoredProcedure);

                    if (updateOrgaPotentialRes == 0)
                    {
                        result.Data = new { };
                        result.DevMsg = FailMessage.CodeError.UpdateFailed;
                        result.UserMsg = FailMessage.MessageError.UpdateFail;
                        result.Flag = false;
                    }
                    else
                    {
                        result.Data = updateOrgaPotentialRes;
                        result.DevMsg = SuccessMessage.CodeSuccess.UpdateSuccess;
                        result.UserMsg = SuccessMessage.MessageSuccess.UpdateSuccess;
                        result.Flag = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
                result.DevMsg = FailMessage.CodeError.ProcessError;
                result.UserMsg = FailMessage.MessageError.ProcessError;
                result.Flag = false;
            }
            return result;

        }
        /// <summary>
        /// Xử lý lấy được FirstName và LastName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static NameCustomer HandleGetName(string name)
        {
            NameCustomer result = new ();
            string[] nameArr = name.Split(" ");
            if (nameArr.Length == 1)
            {
                result.FirstName = nameArr[0];
                result.LastMiddleName = null;
                return result;

            }
            else if (nameArr.Length == 2)
            {

                result.LastMiddleName = nameArr[0];
                result.FirstName = nameArr[1];
                return result;
            }
            else
            {
                string lastMiddleName = "";
                for (var i = 0; i < nameArr.Length - 1; i++)
                {
                    lastMiddleName += nameArr[i] + " ";
                }
                result.LastMiddleName = lastMiddleName.Trim();
                result.FirstName = nameArr[nameArr.Length - 1];

                return result;
            }
        }
    }
}
