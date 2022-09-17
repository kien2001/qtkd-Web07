using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class WardRepository : IWardRepository
    {
        /// <summary>
        /// Lấy dữ liệu về bảng xã theo id huyện
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/08/2022
        public Result Get(int districtId)
        {
            Result result = new();
            result.UserMsg = new List<string>();
            result.DevMsg = new List<string>();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select WardId, WardName from ward where districtId = @districtId group by WardId";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@districtId", districtId);
                var wardArray = mySqlConnection.Query<Ward>(sql: query, param: dynamicParams);
                if (!wardArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;
                }
                else
                {
                    result.Data = wardArray;
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
        /// Lấy tất cả dữ liệu về bảng xã
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/08/2022
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
                string query = "Select WardId, WardName from ward group by WardId";
                
                var wardArray = mySqlConnection.Query<Ward>(sql: query);
                if (!wardArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;
                }
                else
                {
                    result.Data = wardArray;
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
