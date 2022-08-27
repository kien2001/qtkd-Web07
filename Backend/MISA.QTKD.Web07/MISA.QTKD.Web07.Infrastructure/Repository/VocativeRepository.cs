﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;
using Infrastructure;
using static Constants.FailMessage;
using Entities;
using Constants;

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
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select vocative from customer WHERE Vocative is not null group by vocative order by vocative";
                var vocativeArray = mySqlConnection.Query<Vocatives>(query);
                if (!vocativeArray.Any())
                {
                    result.Data = new {};
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag= false;

                }
                else
                {
                    result.Data = vocativeArray;
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