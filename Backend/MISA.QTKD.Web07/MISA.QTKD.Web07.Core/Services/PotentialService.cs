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
using static Constants.FailMessage;

namespace Services
{
    public class PotentialService : IPotentialService
    {
        IPotentialRepository _potentialRepository;

        public PotentialService(IPotentialRepository potentialRepository)
        {
            _potentialRepository = potentialRepository;
        }
        public Result InsertService(Potential potential)
        {
            Result result;
            // nếu ko có mã tiềm năng, thêm vào luôn ko cần check
            if(potential.PotentialCode == null || potential.PotentialCode.Length == 0)
            {
                result = _potentialRepository.Insert(potential);
            }
            else
            {
                // 1.3 Check trùng mã
                bool checkDuplicateCode = _potentialRepository.CheckDuplicateCode(potential.PotentialCode);
                if (checkDuplicateCode == true)
                {
                    result = new Result(data: null,
                           devMsg: FailMessage.CodeError.DuplicatePotentialCode,
                           userMsg: FailMessage.MessageError.DuplicatePotentialCode,
                           flag: false
                           );
                }
                else
                {
                    result = _potentialRepository.Insert(potential);
                }
            }
           
            return result;
        }
    }

}
