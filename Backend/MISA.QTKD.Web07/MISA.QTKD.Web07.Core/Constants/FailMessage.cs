using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    public class FailMessage
    {
        /// <summary>
        /// Lưu thông tin về message khi lỗi
        /// </summary>
        /// Created by LVKIEN 17/05/2022
        public class MessageError
        {
            public const string EmptyFirstName = "Tên không được phép để trống!";
            public const string DuplicateName = "Tên đã bị trùng. Vui lòng đổi tên khác!";
            public const string NotValidEmail = "Email không hợp lệ";
            public const string NotValidPhone = "Số điện thoại không hợp lệ";
            public const string DuplicateTaxCode = "Mã số thuế đã bị trùng. Vui lòng đổi mã khác!";
            public const string DuplicatePotentialCode = "Mã tiềm năng bị trùng";
            public const string DuplicateBankAccount = "Tài khoản này đã có người sử dụng. Vui lòng đổi tài khoản khác!";
            public const string NotExists = "không được bỏ trống!";
            public const string NotFound = "Bản ghi đã chọn không tồn tại!";
            public const string DuplicateValue = "Bản ghi đã tồn tại. Không thể thêm!";
            public const string NotValue = "Không có dữ liệu";
            public const string ProcessError = "Có lỗi trong quá trình xử lý.";
            public const string InsertFail = "Thêm mới thất bại.";
            public const string DeleteFail = "Xóa bản ghi thất bại.";
            public const string UpdateFail = "Cập nhật thông tin thất bại.";
            public const string ExportExcelError = "Xuất khẩu excel thất bại.";

        }

        /// <summary>
        /// Lưu thông tin về mã lỗi 
        /// </summary>
        /// Created by LVKIEN 17/05/2022
        public class CodeError
        {
            public const string DuplicateName = "E001";
            public const string DuplicatePotentialCode = "E002";
            public const string NotFound = "E003";
            public const string NotValue = "E004";
            public const string ProcessError = "E005";
            public const string InsertFailed = "E006";
            public const string UpdateFailed = "E007";
            public const string NotExists = "E008";
            public const string NotValidEmail = "E009";
            public const string NotValidPhone = "E010";
            public const string DuplicateTaxCode = "E011";
            public const string DuplicateBankAccount = "E012";
            public const string DeletedFail = "E013";
            public const string DuplicateValue = "E014";
            public const string EmptyFirstName = "E015";
            public const string ExportExcelError = "E016";

        }
    }
}
