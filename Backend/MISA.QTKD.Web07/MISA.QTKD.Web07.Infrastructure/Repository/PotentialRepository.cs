using System;
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

    public class PotentialRepository : IPotentialRepository
    {
        /// <summary>
        /// Lấy dữ liệu về bảng tiềm năng
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEN 18/08/2022
        public Result Get()
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                string query = "Select PotentialId, PotentialName , PotentialCode from potential";
                var potentialArray = mySqlConnection.Query<Potential>(query);
                if (!potentialArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;
                }
                else
                {
                    result.Data = potentialArray;
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

        /// <summary>
        /// Check trùng mã tiềm năng
        /// </summary>
        /// <returns></returns>
        /// <param name="potentialCode"></param>
        /// Created by LVKIEN 23/08/2022

        public bool CheckDuplicateCode(string potentialCode)
        {
            bool checkDuplicate = true;
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);

                var sqlCheck = "Select PotentialCode from potential where PotentialCode = @PotentialCode limit 1";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@PotentialCode", potentialCode);
                var potentialCodeDuplicate = mySqlConnection.QueryFirstOrDefault<string>(sql: sqlCheck, param: dynamicParams);
                if (potentialCodeDuplicate == null)
                {
                    checkDuplicate =  false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return checkDuplicate;
        }
        /// <summary>
        /// Check trùng mã tiềm năng
        /// </summary>
        /// <returns></returns>
        /// <param name="potential"></param>
        /// Created by LVKIEN 23/08/2022

        public Result Insert(Potential potential)
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                var command = "INSERT INTO potential( PotentialId, PotentialName, PotentialCode, CreatedAt,ModifiedAt)" +
                    "values (@PotentialId, @PotentialName, @PotentialCode, @CreatedAt, @ModifiedAt)";
                var dynamicParams = new DynamicParameters();
                Guid potentialId = Guid.NewGuid();
                dynamicParams.Add("@PotentialId", potentialId);
                dynamicParams.Add("@PotentialName", potential.PotentialName);
                dynamicParams.Add("@PotentialCode", potential.PotentialCode);
                dynamicParams.Add("@CreatedAt", DateTime.Now);
                dynamicParams.Add("@ModifiedAt", DateTime.Now);

                var res = mySqlConnection.Execute(sql: command, param: dynamicParams);
                if (res == 0)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.InsertFailed;
                    result.UserMsg = FailMessage.MessageError.InsertFail;
                    result.Flag = false;
                }
                else
                {
                    result.Data = potentialId;
                    result.DevMsg = SuccessMessage.CodeSuccess.InsertSuccess;
                    result.UserMsg = SuccessMessage.MessageSuccess.InsertSuccess;
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
        /// <summary>
        /// Tạo mã tiềm năng mới bằng mã tiềm năng lớn nhất + 1
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 19/08/2022
        public Result GetMaxCode()
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                var pagingProc = "Proc_Potential_GetMaxCode";
                string maxCode = mySqlConnection.QueryFirstOrDefault<string>(sql: pagingProc, commandType: System.Data.CommandType.StoredProcedure);
                if (maxCode == null)
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;

                }
                else
                {
                    result.Data = maxCode;
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
        /// <summary>
        /// Lấy tên các tiềm năng trong bảng potential
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 23/08/2022
        public Result GetPotentialName()
        {
            Result result = new();
            try
            {
                using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
                var query = "SELECT PotentialName FROM potential p WHERE p.PotentialName IS NOT null GROUP BY p.PotentialName ";
                var listPotential = mySqlConnection.Query<string>(sql: query);
                if (!listPotential.Any())
                {
                    result.Data = new { };
                    result.DevMsg = FailMessage.CodeError.NotValue;
                    result.UserMsg = FailMessage.MessageError.NotValue;
                    result.Flag = false;

                }
                else
                {
                    IEnumerable<string> resultArray = FormatData(listPotential);
                    result.Data = resultArray;
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

        /// <summary>
        /// Xử lý lọc giá trị của các array thành duy nhất 1 giá trị
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEN 23/08/2022

        public static IEnumerable<string> FormatData(IEnumerable<string> array)
        {
            List<string> resultCareer = new();
            foreach (var item in array)
            {
                if (item.Contains('\n'))
                {
                    string[] splitArray = item.Trim().Split('\n');
                    foreach (var itemSplit in splitArray)
                    {
                        resultCareer.Add(itemSplit.Trim());
                    }
                }
                else
                {
                    resultCareer.Add(item.Trim());
                }
            }

            IEnumerable<string> resultArray = resultCareer.ToArray().Distinct();
            return resultArray;
        }
    }
}
