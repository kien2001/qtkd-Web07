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
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select * from address group by AddressId";
                var addressArray = mySqlConnection.Query<Address>(query);
                if (!addressArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;
                }
                else
                {
                    result.Data = addressArray;
                    result.DevMsg = SuccessMessage.CodeSuccess.GetSuccess;
                    result.UserMsg = SuccessMessage.MessageSuccess.GetSuccess;
                    result.Flag = true;
                }



            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
                result.DevMsg = FailMessage.MessageError.ProcessError;
                result.UserMsg = FailMessage.CodeError.ProcessError;
                result.Flag = false;
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
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
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


                var res = Convert.ToInt32(mySqlConnection.ExecuteScalar(sql: command, param: dynamicParams));
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
        /// Update nhiều bản ghi trong bảng Address
        /// </summary>
        /// <param name="updatedMultiple"></param>
        /// <returns></returns>
        public Result UpdateMultiple(UpdatedMultiple<int> updatedMultiple)
        {
            Result result = new();
            try
            {
                var stringIdList = updatedMultiple.ListId.ConvertAll<string>(g => g.ToString());
                var updateQuery = $@"Update address set {updatedMultiple.FieldUpdateName} = @FieldUpdateValue
                  , ModifiedAt = @ModifiedAt where AddressId in @ListId ;";
                var resultString = String.Join(", ", stringIdList);
                var parameters = new
                {
                    FieldUpdateValue = updatedMultiple.FieldUpdateValue,
                    ListId = stringIdList,
                    ModifiedAt = DateTime.Now
                };

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
    }

}
