using Constants;
using Dapper;
using Entities;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using MISA.QTKD.Web07.Core.Entities;
using MySqlConnector;
using Resources.Vi;

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
            result.UserMsg = new List<string>();
            result.DevMsg = new List<string>();

            using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
            mySqlConnection.Open();
            try
            {
                string query = "Select CityId, CityName from city where countryId = @countryId group by CityId";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@countryId", countryId);
                var cityArray = mySqlConnection.Query<City>(sql: query, param: dynamicParams);
                if (!cityArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;
                }
                else
                {
                    result.Data = cityArray;
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
        /// Xử lý lấy tất cả dữ liệu từ bảng city
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        /// Created by LVKIEN 18/05/2022

        public Result GetAll()
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
                string query = "Select CityId, CityName from city group by CityId";
                var cityArray = mySqlConnection.Query<City>(sql: query);
                if (!cityArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;
                }
                else
                {
                    result.Data = cityArray;
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
    }
}
