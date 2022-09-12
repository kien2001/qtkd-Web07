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

namespace Repository
{
    public class DistrictRepository:IDistrictRepository
    {
        /// <summary>
        /// Lấy dữ liệu các huyện của 1 thành phố
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/05/2022
        public Result Get(int cityId)
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
                string query = "Select DistrictId, DistrictName from district where cityId = @cityId group by DistrictId";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@cityId", cityId);
                var districtArray = mySqlConnection.Query<District>(sql: query, param: dynamicParams);
                if (!districtArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add(FailMessage.CodeError.NotValue);
                    result.UserMsg.Add(FailMessage.MessageError.NotValue);
                    result.Flag = false;
                }
                else
                {
                    result.Data = districtArray;
                    result.DevMsg.Add(SuccessMessage.CodeSuccess.GetSuccess);
                    result.UserMsg.Add(SuccessMessage.MessageSuccess.GetSuccess);
                    result.Flag = true;
                }

            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
                result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                result.Flag = false;
            }
            return result;
        }

        /// <summary>
        /// Lấy dữ liệu tất cả các huyện 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                string query = "Select DistrictId, DistrictName from district group by DistrictId";
                var districtArray = mySqlConnection.Query<District>(sql: query);
                if (!districtArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add(FailMessage.CodeError.NotValue);
                    result.UserMsg.Add(FailMessage.MessageError.NotValue);
                    result.Flag = false;
                }
                else
                {
                    result.Data = districtArray;
                    result.DevMsg.Add(SuccessMessage.CodeSuccess.GetSuccess);
                    result.UserMsg.Add(SuccessMessage.MessageSuccess.GetSuccess);
                    result.Flag = true;
                }

            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
                result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                result.Flag = false;
            }
            return result;
        }
    }
}
