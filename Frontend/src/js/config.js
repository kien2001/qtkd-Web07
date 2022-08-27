export const rootApi = 'http://localhost:5057/api/v1/'

// các trường dùng để cập nhật nhiều
export const fieldOptions = ["Xưng hô", "Họ và tên", "Chức danh", "ĐT di động", "ĐT cơ quan", "Email cơ quan", "Email cá nhân", "Tổ chức", "Địa chỉ", "Tỉnh/Thành phố", "Quận/Huyện", "Phường/Xã", "Nguồn gốc", "Loại hình", "Lĩnh vực", "Mô tả", "Bố cục", "Chủ sở hữu", "Doanh thu", "Dùng chung", "Facebook"]

//các trường tiếng việt mapping sang tiếng anh
export const fieldMappingOptions =
    [{
        id: "Vocative",
        name: "Xưng hô"
    }, {
        id: "FullName",
        name: "Họ và tên"
    },
    {
        id: "Postion",
        name: "Chức danh"
    },
    {
        id: "CustomerPhoneNum",
        name: "ĐT di động"
    },
    {
        id: "CompanyPhoneNum",
        name: "ĐT cơ quan"
    },
    {
        id: "CompanyEmail",
        name: "Email cơ quan"
    }
        ,
    {
        id: "CustomerEmail",
        name: "Email cá nhân"
    },
    {
        id: "OrganizationName",
        name: "Tổ chức"
    },
    {
        id: "AddressName",
        name: "Địa chỉ"
    },
    {
        id: "CityName",
        name: "Tỉnh/Thành phố"
    },
    {
        id: "DistrictName",
        name: "Quận/Huyện"
    },
    {
        id: "WardName",
        name: "Phường/Xã"
    },
    {
        id: "SourceName",
        name: "Nguồn gốc"
    },
    {
        id: "TypeName",
        name: "Loại hình"
    },
    {
        id: "Domain",
        name: "Lĩnh vực"
    },
    {
        id: "Description",
        name: "Mô tả"
    },
    {
        id: "Layout",
        name: "Bố cục"
    },
    {
        id: "Owner",
        name: "Chủ sở hữu"
    },
    {
        id: "Revenue",
        name: "Doanh thu"
    },

    {
        id: "SharingUse",
        name: "Dùng chung"
    },
    {
        id: "Facebook",
        name: "Facebook"
    }
    ]

// các trường có kiểu input
export const fieldInputOptions = ["Họ và tên", "ĐT di động", "ĐT cơ quan", "Email cơ quan", "Email cá nhân", "Địa chỉ", "Mô tả", "Bố cục", "Chủ sở hữu", "Dùng chung", "Facebook", "Tổ chức"]

// các trường kiểu dropdown
export const fieldDropdownOptions = ["Xưng hô", "Chức danh", "Tỉnh/Thành phố", "Quận/Huyện", "Phường/Xã", "Nguồn gốc", "Loại hình", "Doanh thu"]

// các trường trong bảng Customer phần cập nhật nhiều 
export const customerFields = ["Xưng hô", "Họ và tên", "Chức danh", "ĐT di động", "ĐT cơ quan", "Email cơ quan", "Email cá nhân", "Mô tả", "Bố cục", "Chủ sở hữu", "Dùng chung", "Facebook", "Nguồn gốc"]

// các trường trong bảng Address phần cập nhật nhiều 
export const addressFields = ["Địa chỉ", "Tỉnh/Thành phố", "Quận/Huyện", "Phường/Xã"]

// các trường trong bảng organization phần cập nhật nhiều 
export const organizationFields = ["Loại hình", "Lĩnh vực", "Tổ chức"]
