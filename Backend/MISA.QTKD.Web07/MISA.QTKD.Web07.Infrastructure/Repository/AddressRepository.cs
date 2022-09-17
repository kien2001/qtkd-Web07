using Resources.Vi;
using Constants;
using Dapper;
using Entities;
using Exceptions;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using MISA.QTKD.Web07.Core.Entities;
using MySqlConnector;

namespace Repository
{
    public class AddressRepository : IAddressRepository
    {
        /// <summary>
        /// Xử lý lấy dữ liệu bảng Address từ database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 17/05/2022
        public Result Get()
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
                    string query = "Select * from address group by AddressId";
                    var addressArray = mySqlConnection.Query<Address>(query);
                    if (!addressArray.Any())
                    {
                        result.Data = new { };
                        result.DevMsg.Add(CodeError.NotValue);
                        result.UserMsg.Add(MessageError.NotValue);
                        result.StatusCode = StatusCode.Status404NotFound;
                        result.Flag = false;
                    }
                    else
                    {
                        result.Data = addressArray;
                        result.DevMsg.Add(CodeSuccess.GetSuccess);
                        result.UserMsg.Add(MessageSuccess.GetSuccess);
                        result.StatusCode = StatusCode.Status200OK;
                        result.Flag = true;
                    }
                }
                catch (Exception ex)
                {
                    result.Data = ex.Message;
                    result.DevMsg.Add(CodeError.ProcessError);
                    result.UserMsg.Add(MessageError.ProcessError);
                    result.StatusCode = StatusCode.Status500InternalServerError;
                    result.Flag = false;
                }
            }
            return result;

        }
        /// <summary>
        /// Xử lý insert 1 bản ghi mới vào bảng Address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 17/05/2022


        public Result Insert(Address address)
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
                    var command = "INSERT INTO address(HomeNumber, AddressName, CountryId, CityId, " +
                        "DistrictId, WardId, PostalCode, CreatedAt, ModifiedAt) VALUES(@HomeNumber, @AddressName" +
                        ",@CountryId, @CityId, @DistrictId, @WardID, @PostalCode, @CreatedAt, @ModifiedAt);SELECT LAST_INSERT_ID()";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@HomeNumber", address.HomeNumber);
                    dynamicParams.Add("@AddressName", address.AddressName);
                    dynamicParams.Add("@CountryId", address.CountryId);
                    dynamicParams.Add("@CityId", address.CityId);
                    dynamicParams.Add("@DistrictId", address.DistrictId);
                    dynamicParams.Add("@WardID", address.WardId);
                    dynamicParams.Add("@PostalCode", address.PostalCode);
                    dynamicParams.Add("@CreatedAt", DateTime.Now);
                    dynamicParams.Add("@ModifiedAt", DateTime.Now);


                    var res = Convert.ToInt32(mySqlConnection.ExecuteScalar(sql: command, param: dynamicParams, transaction: mySqlTransaction));
                    if (res == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(CodeError.InsertFailed);
                        result.UserMsg.Add(MessageError.InsertFail);
                        result.StatusCode = StatusCode.Status400BadRequest;
                        result.Flag = false;
                    }
                    else
                    {
                        result.Data = res;
                        result.DevMsg.Add(CodeSuccess.InsertSuccess);
                        result.UserMsg.Add(MessageSuccess.InsertSuccess);
                        result.StatusCode = StatusCode.Status201Created;
                        result.Flag = true;
                    }
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    result.Data = ex.Message;
                    result.DevMsg.Add(CodeError.ProcessError);
                    result.UserMsg.Add(MessageError.ProcessError);
                    result.StatusCode = StatusCode.Status500InternalServerError;

                    result.Flag = false;
                    mySqlTransaction.Rollback();
                }
            }

            return result;
        }

        /// <summary>
        /// Update nhiều bản ghi trong bảng Address
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
                    var resultString = String.Join(", ", stringIdList);

                    var updateQuery = $@"Update address set {updatedMultiple.FieldUpdateName} = @FieldUpdateValue" +
                  ", ModifiedAt = @ModifiedAt where AddressId in @ListId ;";
                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add("@FieldUpdateValue", updatedMultiple.FieldUpdateValue);
                    dynamicParameters.Add("@ModifiedAt", DateTime.Now);
                    dynamicParameters.Add("@ListId", stringIdList);

                    var res = mySqlConnection.Execute(updateQuery, dynamicParameters, transaction: mySqlTransaction);

                    if (res == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(CodeError.UpdateFailed);
                        result.UserMsg.Add(MessageError.UpdateFail);
                        result.StatusCode = StatusCode.Status400BadRequest;
                        result.Flag = false;
                    }
                    else
                    {

                        result.Data = res;
                        result.DevMsg.Add(CodeSuccess.UpdateSuccess);
                        result.UserMsg.Add(MessageSuccess.UpdateSuccess);
                        result.StatusCode = StatusCode.Status200OK;

                        result.Flag = true;
                    }
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    result.Data = ex.Message;
                    result.DevMsg.Add(CodeError.ProcessError);
                    result.UserMsg.Add(MessageError.ProcessError);
                    result.StatusCode = StatusCode.Status500InternalServerError;

                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
            }

            return result;

        }
    }

}
