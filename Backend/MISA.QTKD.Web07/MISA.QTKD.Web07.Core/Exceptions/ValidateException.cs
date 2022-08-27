using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    /// <summary>
    /// Custom validating exception 
    /// </summary>
    /// Created by LVKIEN 17/05/2022
    public class ValidateException : Exception
    {
        string? MsgErrorValidate = null;
        public ValidateException(string msg)
        {
            this.MsgErrorValidate = msg;
        }
        public override string Message
        {
            get
            {
                return MsgErrorValidate;
            }
        }
    }
}
