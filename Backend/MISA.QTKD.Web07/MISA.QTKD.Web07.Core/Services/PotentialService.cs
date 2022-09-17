using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants;
using Entities;
using Exceptions;
using Infrastructure;
using MISA.QTKD.Web07.Core.Entities;
using MISA.QTKD.Web07.Core.Interfaces.Services;
using Resources.Vi;
namespace Services
{
    public class PotentialService : IPotentialService
    {
        IPotentialRepository _potentialRepository;

        public PotentialService(IPotentialRepository potentialRepository)
        {
            _potentialRepository = potentialRepository;
        }

        /// <summary>
        /// Xử lý khi thêm 1 tiềm năng
        /// </summary>
        /// <param name="potential"></param>
        /// <returns></returns>
        /// created by LVKIEN 28/08/2022
        public Result InsertService(Potential potential)
        {
            Result result;
            try
            {
                // nếu ko có mã tiềm năng, thêm vào luôn ko cần check
                if (potential.PotentialCode == null || potential.PotentialCode.Length == 0)
                {
                    result = _potentialRepository.Insert(potential);
                }
                else
                {
                    // 1.3 Check trùng mã
                    bool checkDuplicateCode = _potentialRepository.CheckDuplicateCode(potential.PotentialCode);
                    if (checkDuplicateCode == true)
                    {
                        //result = new Result(data: null,
                        //       devMsg:  CodeError.DuplicatePotentialCode,
                        //       userMsg:  MessageError.DuplicatePotentialCode,
                        //       flag: false
                        //       );
                    }
                    else
                    {
                        result = _potentialRepository.Insert(potential);
                    }
                }
            }
            catch (Exception ex)
            {
                //result = new Result(data: ex.Message,
                //               devMsg: result.DevMsg.Append( CodeError.ProcessError).ToArray(),
                //               userMsg: result.UserMsg.Append( MessageError.ProcessError).ToArray(),
                //               flag: false
                //               );
            }


            //return result;
            return null;
        }
    }

}
