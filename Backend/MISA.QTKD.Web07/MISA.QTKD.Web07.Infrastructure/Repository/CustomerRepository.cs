using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
using Enums;
using System.Collections;
using System.Reflection;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Xoá các bản ghi trong Customer
        /// </summary>
        /// <param name="listCustomerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEn 25/08/2022
        public Result Delete(IEnumerable<Guid> listCustomerId)
        {
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using (MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
                try
                {
                    bool checkSuccess = true;
                    int count = 0;
                    foreach (var item in listCustomerId)
                    {
                        string processQuery = "DELETE FROM customer WHERE CustomerId =  @CustomerId";
                        var dynamicParams = new DynamicParameters();
                        dynamicParams.Add("@CustomerId", item);
                        var res = mySqlConnection.Execute(processQuery, dynamicParams, transaction: mySqlTransaction);
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
                        result.DevMsg.Add(FailMessage.CodeError.DeletedFail);
                        result.UserMsg.Add(FailMessage.MessageError.DeleteFail);
                        result.Flag = false;
                    }
                    else
                    {
                        result.Data = count;
                        result.DevMsg.Add(SuccessMessage.CodeSuccess.DeleteSuccess);
                        result.UserMsg.Add(SuccessMessage.MessageSuccess.DeleteSuccess);
                        result.Flag = true;
                    }
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    result.Data = ex.Message;
                    result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                    result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
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
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using (MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
                try
                {
                    var stringIdList = updatedMultiple.ListId.ConvertAll<string>(g => g.ToString());
                    var updateQuery = "";
                    object parameters;
                    DynamicParameters dynamicParameters = new();


                    if (updatedMultiple.FieldUpdateName == "FullName")
                    {
                        NameCustomer name = HandleGetName(updatedMultiple.FieldUpdateValue);

                        updateQuery = @"Update customer set FirstName = @FirstNameValue,LastMiddleName = @LastMiddleName , ModifiedAt = @ModifiedAt where CustomerId in @ListId ;";
                        parameters = new
                        {
                            FirstNameValue = name.FirstName,
                            LastMiddleName = name.LastMiddleName,
                            ListId = stringIdList,
                            ModifiedAt = DateTime.Now
                        };
                        dynamicParameters.AddDynamicParams(parameters);
                    }
                    else
                    {
                        updateQuery = $@"Update customer set {updatedMultiple.FieldUpdateName} = @FieldUpdateValue " +
                    ", ModifiedAt = @ModifiedAt where CustomerId in @ListId ;";
                        var resultString = String.Join(", ", stringIdList);
                        parameters = new
                        {
                            FieldUpdateValue = updatedMultiple.FieldUpdateValue,
                            ListId = stringIdList,
                            ModifiedAt = DateTime.Now
                        };
                        dynamicParameters.AddDynamicParams(parameters);
                    }



                    var res = mySqlConnection.Execute(updateQuery, dynamicParameters, transaction: mySqlTransaction);

                    if (res == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(FailMessage.CodeError.UpdateFailed);
                        result.UserMsg.Add(FailMessage.MessageError.UpdateFail);
                        result.Flag = false;
                    }
                    else
                    {

                        result.Data = res;
                        result.DevMsg.Add(SuccessMessage.CodeSuccess.UpdateSuccess);
                        result.UserMsg.Add(SuccessMessage.MessageSuccess.UpdateSuccess);
                        result.Flag = true;
                    }
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    result.Data = ex.Message;
                    result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                    result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
                return result;
            }
        }

        public Result GetPaging(int? pageSize, int? pageIndex, string? keyword, FilterObject[]? listFilter)
        {
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using (MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                try
                {

                    var pagingProc = "Proc_Customer_GetPaging";
                    var orConditions = new List<string>();
                    var whereConditions = new List<string>();

                    string whereClause = "";
                    string andClause = "";
                    string orClause = "";

                    if (listFilter is not null && listFilter.Length > 0)
                    {
                        andClause = ConvertToSqlCommand(listFilter: listFilter);
                        whereConditions.Add(andClause);
                    }
                    if (keyword != null)
                    {
                        orConditions.Add($"FullName LIKE '%{keyword}%'");

                    }
                    if (orConditions.Count > 0)
                    {
                        orClause = $"({string.Join(" OR ", orConditions)})";
                        whereConditions.Add(orClause);

                    }
                    if (whereConditions.Count > 0)
                    {
                        whereClause = $"({string.Join(" AND ", whereConditions)})";

                    }
                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add("@v_Offset", pageIndex);
                    dynamicParameters.Add("@v_Limit", pageSize);
                    dynamicParameters.Add("@v_Sort", default(Plane[]));
                    dynamicParameters.Add("@v_Where", whereClause);


                    var multipleResults = mySqlConnection.QueryMultiple(sql: pagingProc, param: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);

                    if (multipleResults == null)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(FailMessage.CodeError.NotValue);
                        result.UserMsg.Add(FailMessage.MessageError.NotValue);
                        result.Flag = false;
                    }
                    else
                    {
                        var customerTable = multipleResults.Read<Customer>();
                        var totalCount = multipleResults.Read<long>().Single();

                        if (totalCount == 0)
                        {
                            result.Data = new { };
                            result.DevMsg.Add(FailMessage.CodeError.NotFound);
                            result.UserMsg.Add(FailMessage.MessageError.NotFound);
                            result.Flag = false;
                        }
                        else
                        {
                            result.Data = new { customers = customerTable, totalCount };
                            result.DevMsg.Add(SuccessMessage.CodeSuccess.GetSuccess);
                            result.UserMsg.Add(SuccessMessage.MessageSuccess.GetSuccess);
                            result.Flag = true;
                        }

                    }
                }
                catch (Exception ex)
                {

                    result.Data = ex.Message;
                    result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                    result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                    result.Flag = false;
                }
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
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using (MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
                try
                {
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

                    var res = mySqlConnection.Execute(sql: command, param: dynamicParams, transaction: mySqlTransaction);
                    if (res == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(FailMessage.CodeError.InsertFailed);
                        result.UserMsg.Add(FailMessage.MessageError.InsertFail);
                        result.Flag = false;

                    }
                    else
                    {
                        result.Data = res;
                        result.DevMsg.Add(SuccessMessage.CodeSuccess.InsertSuccess);
                        result.UserMsg.Add(SuccessMessage.MessageSuccess.InsertSuccess);
                        result.Flag = true;
                    }
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    result.Data = ex.Message;
                    result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                    result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
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
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using (MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
                try
                {
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

                    int updateCustomerRes = mySqlConnection.Execute(sql: updateCommand, param: parameters, transaction: mySqlTransaction);

                    if (updateCustomerRes == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(FailMessage.CodeError.UpdateFailed);
                        result.UserMsg.Add(FailMessage.MessageError.UpdateFail);
                        result.Flag = false;
                    }
                    else
                    {
                        var updatePotOrgaProc = "Proc_Customer_UpdateOrgaPotential";

                        DynamicParameters dynamicParameters = new();
                        dynamicParameters.Add("@v_PotentialId", customer.PotentialId);
                        dynamicParameters.Add("@v_PotentialName", customer.PotentialName);
                        dynamicParameters.Add("@v_OrganizationId", customer.OrganizationId);
                        dynamicParameters.Add("@v_OrganizatonName", customer.OrganizationName);
                        dynamicParameters.Add("@v_CustomerId", customerId);
                        dynamicParameters.Add("@v_ModifiedAt", DateTime.Now);

                        int updateOrgaPotentialRes = mySqlConnection.QueryFirstOrDefault<int>(sql: updatePotOrgaProc, param: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure, transaction:mySqlTransaction);

                        if (updateOrgaPotentialRes == 0)
                        {
                            result.Data = new { };
                            result.DevMsg.Add(FailMessage.CodeError.UpdateFailed);
                            result.UserMsg.Add(FailMessage.MessageError.UpdateFail);
                            result.Flag = false;
                        }
                        else
                        {
                            result.Data = updateOrgaPotentialRes;
                            result.DevMsg.Add(SuccessMessage.CodeSuccess.UpdateSuccess);
                            result.UserMsg.Add(SuccessMessage.MessageSuccess.UpdateSuccess);
                            result.Flag = true;
                        }
                        mySqlTransaction.Commit();
                    }

                }
                catch (Exception ex)
                {
                    result.Data = ex.Message;
                    result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                    result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
            }

             
            return result;
        }

        /// <summary>
        /// Lấy dữ liệu về cho export excel
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// Created by LvKIEn 7/9/2022
        public Result GetDataExcel(
            List<Guid> customerId)
        {
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using (MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                try
                {

                    var pagingProc = "Proc_Customer_GetPaging";
                    List<string> idStrList = new();
                    string idStr;
                    foreach (Guid item in customerId)
                    {
                        string guidStr = item.ToString();
                        idStrList.Add($"'{guidStr}'");
                    }

                    idStr = $"({string.Join(", ", idStrList)})";
                    string whereClause = $" CustomerId IN {idStr}";

                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add("@v_Offset", 0);
                    dynamicParameters.Add("@v_Limit", customerId.Count);
                    dynamicParameters.Add("@v_Sort", default(Plane[]));
                    dynamicParameters.Add("@v_Where", whereClause);


                    var multipleResults = mySqlConnection.QueryMultiple(sql: pagingProc, param: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);

                    if (multipleResults == null)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(FailMessage.CodeError.NotValue);
                        result.UserMsg.Add(FailMessage.MessageError.NotValue);
                        result.Flag = false;
                    }
                    else
                    {
                        List<CustomerTable> customerTable = (List<CustomerTable>)multipleResults.Read<CustomerTable>();
                        var totalCount = multipleResults.Read<long>().Single();

                        if (totalCount == 0)
                        {
                            result.Data = new { };
                            result.DevMsg.Add(FailMessage.CodeError.NotFound);
                            result.UserMsg.Add(FailMessage.MessageError.NotFound);
                            result.Flag = false;
                        }
                        else
                        {
                            result.Data = customerTable;
                            result.DevMsg.Add(SuccessMessage.CodeSuccess.GetSuccess);
                            result.UserMsg.Add(SuccessMessage.MessageSuccess.GetSuccess);
                            result.Flag = true;
                        }

                    }
                }
                catch (Exception ex)
                {

                    result.Data = ex.Message;
                    result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                    result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                    result.Flag = false;

                }
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
            NameCustomer result = new();
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
     


        /// <summary>
        /// Tạo câu lệnh andClause từ mảng ở frontend
        /// </summary>
        /// <param name="listFilter"></param>
        /// <returns></returns>
        /// Created by LVKIEn 6/9/2022
        public static string ConvertToSqlCommand(FilterObject[] listFilter)
        {

            var andConditions = new List<string>();
            string andClause = "";
            if (listFilter is not null && listFilter.Length > 0)
            {
                foreach (FilterObject item in listFilter)
                {
                    string query = "";

                    if (JsonValueKind.String == item.Value.ValueKind)
                    {
                        string value = JsonSerializer.Deserialize<string>(item.Value);
                        switch (item.Type)
                        {
                            // Là
                            case 1:
                                query = $"({item.Name} = '{value}')";
                                break;
                            // Chứa
                            case 2:
                                query = $"({item.Name} LIKE '%{value}%')";
                                break;
                            // Không là
                            case 3:
                                query = $"({item.Name} != '{value}')";
                                break;
                            // Không chứa
                            case 4:
                                query = $"({item.Name} NOT LIKE '%{value}%')";
                                break;
                            // Không Trống
                            case 5:
                                query = $"({item.Name} IS NOT NULL OR {item.Name} != ' ')";
                                break;
                            // Trống
                            case 6:
                                query = $"({item.Name} IS NULL OR {item.Name} = ' ')";
                                break;
                            default:
                                break;
                        }
                    }
                    else if (JsonValueKind.Array == item.Value.ValueKind)
                    {
                        var valueList = JsonSerializer.Deserialize<List<string>>(item.Value);
                        switch (item.Type)
                        {
                            // Là
                            case 1:
                                List<string> tempArr = new();
                                foreach (string itemValue in valueList)
                                {
                                    string tempValue = $"({item.Name} = '{itemValue}')";
                                    tempArr.Add(tempValue);
                                }
                                query = $"({string.Join(" OR ", tempArr)})";
                                break;
                            // Chứa
                            case 2:
                                query = $"({item.Name} LIKE '%{item.Value}%')";
                                break;
                            // Không là
                            case 3:
                                List<string> tempArrOther = new();
                                foreach (string itemValue in valueList)
                                {
                                    string tempValue = $"({item.Name} != '{itemValue}')";
                                    tempArrOther.Add(tempValue);
                                }
                                query = $"({string.Join(" OR ", tempArrOther)})";
                                break;
                            // Không chứa
                            case 4:
                                query = $"({item.Name} NOT LIKE '%{item.Value}%')";
                                break;
                            // Trống
                            case 5:
                                query = $"({item.Name} IS NULL OR {item.Name} = ' ')";
                                break;
                            // không trống
                            case 6:
                                query = $"({item.Name} IS NOT NULL OR {item.Name} != ' ')";
                                break;
                            default:
                                break;
                        }
                    }

                    andConditions.Add(query);
                }
            }
            if (andConditions.Count > 0)
            {
                andClause = $"({string.Join(" AND ", andConditions)})";
            }
            return andClause;
        }
    }
}
