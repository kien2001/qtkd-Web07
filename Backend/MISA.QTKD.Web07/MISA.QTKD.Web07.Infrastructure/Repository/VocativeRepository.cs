using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;
using Infrastructure;
using Entities;
using Constants;
using Resources.Vi;
namespace Repository
{
    public class VocativeRepository : IVocativeRepository
    {
        /// <summary>
        /// Lấy dữ liệu trường xưng hô trong bảng customer
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
                string query = "Select vocative from customer WHERE Vocative is not null group by vocative order by vocative";
                var vocativeArray = mySqlConnection.Query<Vocatives>(query);
                if (!vocativeArray.Any())
                {
                    result.Data = new {};
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag= false;

                }
                else
                {
                    result.Data = vocativeArray;
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
