using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Kết quả trả về cho frontend
    /// </summary>
    /// Created by LVKIEN 13/09/2022
    public class Result
    {
        #region Result
        /// <summary>
        /// Dữ liệu
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public List<string> DevMsg { get; set; }

        /// <summary>
        /// Thông điệp
        /// </summary>
        public List<string> UserMsg { get; set; }

        /// <summary>
        /// Cờ check
        /// </summary>
        public bool Flag { get; set; }

        public int StatusCode { get; set; }
        #endregion

        public Result(object data, List<string> devMsg, List<string> userMsg, bool flag, int statusCode)
        {
            Data = data;
            DevMsg = devMsg;
            UserMsg = userMsg;
            Flag = flag;
            StatusCode = statusCode;
        }

        public Result()
        {

        }
    }
}
