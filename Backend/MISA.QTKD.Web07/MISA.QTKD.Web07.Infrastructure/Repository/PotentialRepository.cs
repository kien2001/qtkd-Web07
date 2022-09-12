using Constants;
using Dapper;
using Entities;
using Infrastructure;
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
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
            mySqlConnection.Open();
            try
            {
                string query = "Select PotentialId, PotentialName , PotentialCode from potential";
                var potentialArray = mySqlConnection.Query<Potential>(query);
                if (!potentialArray.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add(FailMessage.CodeError.NotValue);
                    result.UserMsg.Add(FailMessage.MessageError.NotValue);
                    result.Flag = false;
                }
                else
                {
                    result.Data = potentialArray;
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
        /// Check trùng mã tiềm năng
        /// </summary>
        /// <returns></returns>
        /// <param name="potentialCode"></param>
        /// Created by LVKIEN 23/08/2022

        public bool CheckDuplicateCode(string potentialCode)
        {
            bool checkDuplicate = true;
            using MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString);
            mySqlConnection.Open();
            try
            {

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
            Result result = new()
            {
                UserMsg = new List<string>(),
                DevMsg = new List<string>()
            };
            using (MySqlConnection mySqlConnection = new(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
                try
                {
                    var command = "INSERT INTO potential( PotentialId, PotentialName, PotentialCode, CreatedAt,ModifiedAt)" +
                        "values (@PotentialId, @PotentialName, @PotentialCode, @CreatedAt, @ModifiedAt)";
                    var dynamicParams = new DynamicParameters();
                    Guid potentialId = Guid.NewGuid();
                    dynamicParams.Add("@PotentialId", potentialId);
                    dynamicParams.Add("@PotentialName", potential.PotentialName);
                    dynamicParams.Add("@PotentialCode", potential.PotentialCode);
                    dynamicParams.Add("@CreatedAt", DateTime.Now);
                    dynamicParams.Add("@ModifiedAt", DateTime.Now);

                    var res = mySqlConnection.Execute(sql: command, param: dynamicParams, transaction:mySqlTransaction);
                    if (res == 0)
                    {
                        result.Data = new { };
                        result.DevMsg.Add(FailMessage.CodeError.InsertFailed);
                        result.UserMsg.Add(FailMessage.MessageError.InsertFail);
                        result.Flag = false;
                    }
                    else
                    {
                        result.Data = potentialId;
                        result.DevMsg.Add(SuccessMessage.CodeSuccess.InsertSuccess);
                        result.UserMsg.Add(SuccessMessage.MessageSuccess.InsertSuccess);
                        result.Flag = true;
                    }
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    result.Data = ex.Message;
                    result.DevMsg.Add(FailMessage.CodeError.ProcessError);
                    result.UserMsg.Add(FailMessage.MessageError.ProcessError);
                    result.Flag = false;

                    mySqlTransaction.Rollback();
                }
                return result;
            }
        }
        /// <summary>
        /// Tạo mã tiềm năng mới bằng mã tiềm năng lớn nhất + 1
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 19/08/2022
        public Result GetMaxCode()
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
                var pagingProc = "Proc_Potential_GetMaxCode";
                string maxCode = mySqlConnection.QueryFirstOrDefault<string>(sql: pagingProc, commandType: System.Data.CommandType.StoredProcedure);
                if (maxCode == null)
                {
                    result.Data = new { };
                    result.DevMsg.Add(FailMessage.CodeError.NotValue);
                    result.UserMsg.Add(FailMessage.MessageError.NotValue);
                    result.Flag = false;

                }
                else
                {
                    string numberCodeStr = maxCode.Substring(3);
                    bool check = long.TryParse(numberCodeStr, out long numberCode);
                    if (check)
                    {
                        maxCode = $"NV-{numberCode + 1}";
                    }
                    result.Data = maxCode;
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
        /// Lấy tên các tiềm năng trong bảng potential
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LVKIEN 23/08/2022
        public Result GetPotentialName()
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
                var query = "SELECT PotentialName FROM potential p WHERE p.PotentialName IS NOT null GROUP BY p.PotentialName ";
                var listPotential = mySqlConnection.Query<string>(sql: query);
                if (!listPotential.Any())
                {
                    result.Data = new { };
                    result.DevMsg.Add(FailMessage.CodeError.NotValue);
                    result.UserMsg.Add(FailMessage.MessageError.NotValue);
                    result.Flag = false;

                }
                else
                {
                    IEnumerable<string> resultArray = CommonFunc.CommonFunc.FormatData(listPotential);
                    result.Data = resultArray;
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
