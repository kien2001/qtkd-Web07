using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants;
using Dapper;
using Entities;
using Exceptions;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using MISA.QTKD.Web07.Core.Entities;
using MySqlConnector;
using Resources.Vi;
namespace Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        
        /// <summary>
        /// Lấy dữ liệu về bảng Organization
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/08/2022
        public Result Get()
        {
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
            mySqlConnection.Open();
            try
            {
                string query = "Select * from organization";
                var organizationArray = mySqlConnection.Query<Organization>(query);
                if (!organizationArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;

                }
                else
                {
                    result.Data = organizationArray;
                    result.DevMsg.Add( CodeSuccess.GetSuccess);
                    result.UserMsg.Add( MessageSuccess.GetSuccess);
                    result.StatusCode = StatusCode.Status200OK;

                    result.Flag = true;
                }
            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
                result.DevMsg.Add( CodeError.ProcessError);
                result.UserMsg.Add( MessageError.ProcessError);
                result.StatusCode = StatusCode.Status500InternalServerError;

                result.Flag = false;
            }
            return result;

        }


        /// <summary>
        /// Lấy dữ liệu ngành nghề của bảng Organization
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 23/08/2022
        public Result GetCareer()
        {
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
            mySqlConnection.Open();
            try
            {
                string query = "SELECT Career FROM organization o WHERE o.Career IS NOT null GROUP BY o.Career";
                var careerArray = mySqlConnection.Query<string>(query);
                if (!careerArray.Any())
                {
                    result.Data = new();
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;
                }
                else
                {

                    IEnumerable<string> resultArray = CommonFunc.CommonFunc.FormatData(careerArray);
                    result.Data = resultArray;
                    result.DevMsg.Add( CodeSuccess.GetSuccess);
                    result.UserMsg.Add( MessageSuccess.GetSuccess);
                    result.StatusCode = StatusCode.Status200OK;

                    result.Flag = true;
                }
            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
                result.DevMsg.Add( CodeError.ProcessError);
                result.UserMsg.Add( MessageError.ProcessError);
                result.StatusCode = StatusCode.Status500InternalServerError;

                result.Flag = false;
            }
            return result;

        }

        /// <summary>
        /// Lấy dữ liệu lĩnh vực của bảng Organization
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 23/08/2022
        public Result GetDomain()
        {
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
            mySqlConnection.Open();
            try
            {
                string query = "SELECT Domain FROM organization o WHERE o.Domain IS NOT null GROUP BY o.Domain";
                var domainArray = mySqlConnection.Query<string>(query);
                if (!domainArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;
                }
                else
                {
                    IEnumerable<string> resultArray = CommonFunc.CommonFunc.FormatData(domainArray);

                    result.Data = resultArray;
                    result.DevMsg.Add( CodeSuccess.GetSuccess);
                    result.UserMsg.Add( MessageSuccess.GetSuccess);
                    result.StatusCode = StatusCode.Status200OK;

                    result.Flag = true;
                }
            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
                result.DevMsg.Add( CodeError.ProcessError);
                result.UserMsg.Add( MessageError.ProcessError);
                result.StatusCode = StatusCode.Status500InternalServerError;

                result.Flag = false;
            }
            return result;

        }

        /// <summary>
        /// Thêm 1 bảng ghi vào bảng Organization và trả về ID bản ghi vừa mới thêm
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/08/2022
        public Result Insert(Organization organization)
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
                    var command = "INSERT INTO organization(OrganizationName,BankAccount,BankName," +
                        "CreatedAccountAt,TypeId,Domain,Career,RevenueId,CreatedAt,ModifiedAt)" +
                        "VALUES(@OrganizationName, @BankAccount, @BankName, @CreatedAccountAt, @TypeId, @Domain, " +
                        "@Career, @RevenueId, @CreatedAt, @ModifiedAt);SELECT LAST_INSERT_ID()";

                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@OrganizationName", organization.OrganizationName);
                    dynamicParams.Add("@BankAccount", organization.BankAccount);
                    dynamicParams.Add("@BankName", organization.BankName);
                    dynamicParams.Add("@CreatedAccountAt", organization.CreatedAccountAt);
                    dynamicParams.Add("@TypeId", organization.TypeId);
                    dynamicParams.Add("@Domain", organization.Domain);
                    dynamicParams.Add("@Career", organization.Career);
                    dynamicParams.Add("@RevenueId", organization.RevenueId);
                    dynamicParams.Add("@CreatedAt", DateTime.Now);
                    dynamicParams.Add("@ModifiedAt", DateTime.Now);


                    //var res = mySqlConnection.Execute(sql: command, param: dynamicParams);
                    var res = Convert.ToInt32(mySqlConnection.ExecuteScalar(sql: command, param: dynamicParams, transaction: mySqlTransaction));
                    if (res == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add( CodeError.NotValue);
                        result.UserMsg.Add( MessageError.NotValue);
                        result.StatusCode = StatusCode.Status400BadRequest;

                        result.Flag = false;
                    }
                    else
                    {
                        result.Data = res;
                        result.DevMsg.Add( CodeSuccess.InsertSuccess);
                        result.UserMsg.Add( MessageSuccess.InsertSuccess);
                        result.StatusCode = StatusCode.Status201Created;

                        result.Flag = true;

                    }

                    mySqlTransaction.Commit();

                }
                catch (Exception ex)
                {
                    result.Data = ex.Message;
                    result.DevMsg.Add( CodeError.ProcessError);
                    result.UserMsg.Add( MessageError.ProcessError);
                    result.StatusCode = StatusCode.Status500InternalServerError;

                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
            }

          
            return result;

        }

        /// <summary>
        /// Update nhiều bản ghi trong bảng Organization
        /// </summary>
        /// <param name="updatedMultiple"></param>
        /// <returns></returns>
        public Result UpdateMultiple(UpdatedMultiple<int> updatedMultiple)
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
                    var updateQuery = $@"Update organization set {updatedMultiple.FieldUpdateName} = @FieldUpdateValue where OrganizationId in @ListId ;";
                    var resultString = String.Join(", ", stringIdList);
                    var parameters = new
                    {
                        FieldUpdateValue = updatedMultiple.FieldUpdateValue,
                        ListId = stringIdList
                    };
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.AddDynamicParams(parameters);

                    var res = mySqlConnection.Execute(updateQuery, dynamicParameters, transaction:mySqlTransaction);

                    if (res == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add( CodeError.UpdateFailed);
                        result.UserMsg.Add( MessageError.UpdateFail);
                        result.StatusCode = StatusCode.Status400BadRequest;

                        result.Flag = false;
                    }
                    else
                    {

                        result.Data = res;
                        result.DevMsg.Add( CodeSuccess.UpdateSuccess);
                        result.UserMsg.Add( MessageSuccess.UpdateSuccess);
                        result.StatusCode = StatusCode.Status200OK;

                        result.Flag = true;
                    }
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    result.Data = ex.Message;
                    result.DevMsg.Add( CodeError.ProcessError);
                    result.UserMsg.Add( MessageError.ProcessError);
                    result.StatusCode = StatusCode.Status500InternalServerError;

                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
            }
          
            return result;

        }
    }
}
