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
using static Constants.FailMessage;

namespace Repository
{
    public class RevenueRepository: IRevenueRepository
    {
        /// <summary>
        /// Lấy dữ liệu về bảng revenue
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/08/2022
        public Result Get()
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select RevenueId, RevenueName from revenue group by RevenueId order by RevenueId";
                var revenueArray = mySqlConnection.Query<Revenue>(query);
                if (!revenueArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;

                }
                else
                {
                    result.Data = revenueArray;
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
    }
}
