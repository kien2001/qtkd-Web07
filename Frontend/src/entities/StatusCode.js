const StatusCode = {
    ErrorCode: {
        DuplicateName: "E001",
        DuplicatePotentialCode: "E002",
        NotFound: "E003",
        NotValue: "E004",
        ProcessError: "E005",
        InsertFailed: "E006",
        UpdateFailed: "E007",
        NotExists: "E008",
        NotValidEmail: "E009",
        DuplicatePhone: "E010",
        DuplicateTaxCode: "E011",
        DuplicateBankAccount: "E012",
        DeletedFail: "E013",
        DuplicateValue: "E014",
        EmptyFirstName: "E015",
    },
    SuccessCode: {
        GetSuccess: "S01",
        UpdateSuccess: "S02",
        DeleteSuccess: "S03",
        SearchSuccess: "S04",
        InsertSuccess: "S05",
    },
    MessageError:
    {
        EmptyFirstName: "Tên không được phép để trống!",
        DuplicateName: "Tên đã bị trùng. Vui lòng đổi tên khác!",
        NotValidEmail: "Email không hợp lệ",
        DuplicatePhone: "Số điện thoại đã đăng ký. Vui lòng đổi số khác!",
        DuplicateTaxCode: "Mã số thuế đã bị trùng. Vui lòng đổi mã khác!",
        DuplicatePotentialCode: "Mã tiềm năng bị trùng",
        DuplicateBankAccount: "Tài khoản này đã có người sử dụng. Vui lòng đổi tài khoản khác!",
        NotExists: "không được bỏ trống!",
        NotFound: "Bản ghi đã chọn không tồn tại!",
        DuplicateValue: "Bản ghi đã tồn tại. Không thể thêm!",
        NotValue: "Không có dữ liệu",
        ProcessError: "Có lỗi trong quá trình xử lý.",
        InsertFail: "Thêm mới thất bại.",
        DeleteFail: "Xóa bản ghi thất bại.",
        UpdateFail: "Cập nhật thông tin thất bại."
    },
    MessageSuccess:
    {
        GetSuccess : "Lấy bản ghi thành công!",
        UpdateSuccess : "Cập nhật bản ghi thành công!",
        DeleteSuccess : "Xóa bản ghi thành công!",
        SearchSuccess : "Tìm kiếm bản ghi thành công!",
        InsertSuccess : "Tạo bản ghi thành công!"
    }
}
export default StatusCode