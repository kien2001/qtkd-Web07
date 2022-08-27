using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Response
    {
        /// <summary>
        /// Định nghia thông tin trả về khi bị lỗi
        /// </summary>
        /// Created by LVKIEN 17/05/2022
        public string DevMsg { get; set; } // Message gửi cho dev
        public string UserMsg { get; set; }// Message gửi cho khách hàng
        public object Data { get; set; }// Dữ liệu

        public Response(string devMsg, string userMsg, object data)
        {
            DevMsg = devMsg;
            UserMsg = userMsg;
            Data = data;
        }

        public Response()
        {
        }

    }
}
