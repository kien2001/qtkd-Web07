using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    public class SuccessMessage
    {
        /// <summary>
        /// Lưu thông tin về thông điệp thành công
        /// </summary>
        /// Created by LVKIEN 19/05/2022
        public class MessageSuccess
        {
            public const string GetSuccess = "Lấy bản ghi thành công!";
            public const string UpdateSuccess = "Cập nhật bản ghi thành công!";
            public const string DeleteSuccess = "Xóa bản ghi thành công!";
            public const string SearchSuccess = "Tìm kiếm bản ghi thành công!";
            public const string InsertSuccess = "Tạo bản ghi thành công!";
        }

        /// <summary>
        /// Lưu thông tin về mã thành công 
        /// </summary>
        /// Created by LVKIEN 19/05/2022
        public class CodeSuccess
        {
            public const string GetSuccess = "S01";
            public const string UpdateSuccess = "S02";
            public const string DeleteSuccess = "S03";
            public const string SearchSuccess = "S04";
            public const string InsertSuccess = "S05";
        }
    }
}
