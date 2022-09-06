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
    public class PositionRepository : IPositionRepository
    {
        /// <summary>
        /// Lấy dữ liệu về bảng Position
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEN 26/08/2022
        public Result Get()
        {
            Result result = new();
            result.UserMsg = new List<string>();
            result.DevMsg = new List<string>();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select PositionId, PositionName from positions group by PositionId";
                var positionArray = mySqlConnection.Query<Position>(query);
                if (!positionArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add(FailMessage.CodeError.NotValue);
                    result.UserMsg.Add(FailMessage.MessageError.NotValue);
                    result.Flag = false;

                }
                else
                {
                    result.Data = positionArray;
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
