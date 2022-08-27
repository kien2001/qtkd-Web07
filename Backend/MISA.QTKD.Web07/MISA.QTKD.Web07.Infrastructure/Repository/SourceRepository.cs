using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Entities;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using MISA.QTKD.Web07.Core.Entities;
using MySqlConnector;
using  Constants;

namespace Repository
{
    public class SourceRepository : ISourceRepository
    {
        /// <summary>
        /// Lấy dữ liệu bảng nguồn gốc
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEn 18/08/2022
        public Result Get()
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select SourceId, SourceName from source group by SourceId";
                var sourceArray = mySqlConnection.Query<Source>(query);
                if (!sourceArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;

                }
                else
                {
                    result.Data = sourceArray;
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
