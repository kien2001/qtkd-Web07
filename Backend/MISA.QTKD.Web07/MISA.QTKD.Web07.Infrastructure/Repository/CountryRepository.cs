﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants;
using Dapper;
using Entities;
using Exceptions;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using MISA.QTKD.Web07.Core.Entities;
using MySqlConnector;

namespace Repository
{
    public class CountryRepository : ICountryRepository
    {
        /// <summary>
        /// Lấy tất cả bản ghi trong bảng Country
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 18/05/2022
        public Result Get()
        {
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                mySqlConnection.Open();
                string query = "Select CountryId, CountryName from country group by CountryId";
                var sourceArray = mySqlConnection.Query<Country>(query);
                if (!sourceArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add(FailMessage.CodeError.NotValue);
                    result.UserMsg.Add(FailMessage.MessageError.NotValue);
                    result.Flag = false;
                }
                else
                {
                    result.Data = sourceArray;
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
