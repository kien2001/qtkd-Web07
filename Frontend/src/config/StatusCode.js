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
        NotValidPhone: "E010",
        DuplicateTaxCode: "E011",
        DuplicateBankAccount: "E012",
        DeletedFail: "E013",
        DuplicateValue: "E014",
        EmptyFirstName: "E015",
        ExportExcelError: "E016",
        NotValidDateError : "E017"
    },
    SuccessCode: {
        GetSuccess: "S01",
        UpdateSuccess: "S02",
        DeleteSuccess: "S03",
        SearchSuccess: "S04",
        InsertSuccess: "S05",
        ExportSuccess : "S06"
    },
    MessageError:
    {
        EmptyFirstName: "Tên không được phép để trống!",
        DuplicateName: "Tên đã bị trùng. Vui lòng đổi tên khác!",
        NotValidEmail: "Email không hợp lệ",
        NotValidPhone: "Số điện thoại không hợp lệ",
        DuplicateTaxCode: "Mã số thuế đã bị trùng. Vui lòng đổi mã khác!",
        DuplicatePotentialCode: "Giá trị của Mã tiềm năng bị trùng",
        DuplicateBankAccount: "Tài khoản này đã có người sử dụng. Vui lòng đổi tài khoản khác!",
        NotExists: "không được bỏ trống!",
        NotFound: "Bản ghi đã chọn không tồn tại!",
        DuplicateValue: "Bản ghi đã tồn tại. Không thể thêm!",
        NotValue: "Không có dữ liệu",
        ProcessError: "Có lỗi trong quá trình xử lý.",
        InsertFail: "Thêm mới thất bại.",
        DeleteFail: "Xóa bản ghi thất bại.",
        UpdateFail: "Cập nhật thông tin thất bại.",
        ExportExcelError: "Xuất khẩu excel thất bại.",
        NotValidDateError : "Ngày không được lớn hơn hiện tại.",
        NotChooseCountryError: "Bạn chưa chọn quốc gia",
        NotChooseCityError: "Bạn chưa chọn tỉnh/thành phố",
        NotChooseDistrictError: "Bạn chưa chọn quận/huyện",
    },
    MessageSuccess:
    {
        GetSuccess : "Lấy bản ghi thành công!",
        UpdateSuccess : "Cập nhật bản ghi thành công!",
        DeleteSuccess : "Xóa bản ghi thành công!",
        SearchSuccess : "Tìm kiếm bản ghi thành công!",
        InsertSuccess : "Tạo bản ghi thành công!",
        ExportSuccess : "Export excel thành công!"
    }
}
export default StatusCode