using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Result
    {
        public object Data { get; set; }

        public List<string> DevMsg { get; set; }

        public List<string> UserMsg { get; set; }

        public bool Flag { get; set; }

        public Result(object data, List<string> devMsg, List<string> userMsg, bool flag)
        {
            Data = data;
            DevMsg = devMsg;
            UserMsg = userMsg;
            Flag = flag;
        }

        public Result()
        {

        }
    }
}
