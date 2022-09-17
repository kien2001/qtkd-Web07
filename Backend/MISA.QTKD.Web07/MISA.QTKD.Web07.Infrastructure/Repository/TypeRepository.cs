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
    public class TypeRepository:ITypeRepository
    {
        /// <summary>
        /// Lấy dữ liệu về bảng Loại hình
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
                string query = "Select TypeId, TypeName from type group by TypeId";
                var typeArray = mySqlConnection.Query<Types>(query);
                if (!typeArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add( CodeError.NotValue);
                    result.UserMsg.Add( MessageError.NotValue);
                    result.StatusCode = StatusCode.Status404NotFound;

                    result.Flag = false;

                }
                else
                {
                    result.Data = typeArray;
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
