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
using MySqlConnector;

namespace Repository
{
    public class GenderRepository : IGenderRepository
    {
        /// <summary>
        /// Lấy dữ liệu giới tính
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEN 18/08/2022
        public Result Get()
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select Gender from customer where Gender is not null group by Gender order by Gender";
                var genderArray = mySqlConnection.Query<Genders>(query);
                if (!genderArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;
                }
                else
                {
                    result.Data = genderArray;
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
