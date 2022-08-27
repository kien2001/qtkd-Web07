using Constants;
using Dapper;
using Entities;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using MISA.QTKD.Web07.Core.Entities;
using MySqlConnector;

namespace Repository
{
    public class CityRepository : ICityRepository
    {
        /// <summary>
        /// Xử lý lấy dữ liệu từ bảng city từ  id Country
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        /// Created by LVKIEN 18/05/2022
        public Result Get(int countryId)
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select CityId, CityName from city where countryId = @countryId group by CityId";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@countryId", countryId);
                var cityArray = mySqlConnection.Query<City>(sql: query, param: dynamicParams);
                if (!cityArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;
                }
                else
                {
                    result.Data = cityArray;
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
        /// Xử lý lấy tất cả dữ liệu từ bảng city
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        /// Created by LVKIEN 18/05/2022

        public Result GetAll()
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select CityId, CityName from city group by CityId";
                var cityArray = mySqlConnection.Query<City>(sql: query);
                if (!cityArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;
                }
                else
                {
                    result.Data = cityArray;
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
    }
}
