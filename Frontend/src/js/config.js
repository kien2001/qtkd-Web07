export const rootApi = 'http://localhost:5057/api/v1/'

// các trường dùng để cập nhật nhiều
export const fieldOptions = ["Xưng hô", "Họ và tên", "Chức danh", "ĐT di động", "ĐT cơ quan", "Email cơ quan", "Email cá nhân", "Tổ chức", "Địa chỉ", "Tỉnh/Thành phố", "Quận/Huyện", "Phường/Xã", "Nguồn gốc", "Loại hình", "Lĩnh vực", "Mô tả", "Bố cục", "Chủ sở hữu", "Doanh thu",  "Facebook"]

export const dropdownField = [
    "vocative", "departmentId", "positionId", "sourceId", "typeId", "revenueId", "countryId", "cityId", "districtId", "wardId"
]

//các trường tiếng việt mapping sang tiếng anh
export const fieldMappingOptions =
    [{
        id: "Vocative",
        name: "Xưng hô",
        type: 1,
        url: "Vocatives"
    }, {
        id: "FullName",
        name: "Họ và tên",
        type: 1
    },
    {
        id: "PositionName",
        name: "Chức danh",
        type: 1,
        url: "Positions"
    },
    {
        id: "CustomerPhoneNum",
        name: "ĐT di động",
        type: 2
    },
    {
        id: "CompanyPhoneNum",
        name: "ĐT cơ quan",
        type: 2
    },
    {
        id: "CompanyEmail",
        name: "Email cơ quan",
        type: 2
    }
        ,
    {
        id: "CustomerEmail",
        name: "Email cá nhân",
        type: 2
    },
    {
        id: "OrganizationName",
        name: "Tổ chức",
        type: 1,
        url: "Organizations"
    },
    {
        id: "AddressName",
        name: "Địa chỉ",
        type: 2
    },
    {
        id: "CityName",
        name: "Tỉnh/Thành phố",
        type: 1,
        url: "Cities/AllCities"
    },
    {
        id: "DistrictName",
        name: "Quận/Huyện",
        type: 1,
        url: "Districts/AllDistricts"
    },
    {
        id: "WardName",
        name: "Phường/Xã",
        type: 1,
        url: "Wards/AllWards"
    },
    {
        id: "SourceName",
        name: "Nguồn gốc",
        type: 1,
        url: "Sources"
    },
    {
        id: "TypeName",
        name: "Loại hình",
        type: 1,
        url: "Types"
    },
    {
        id: "Domain",
        name: "Lĩnh vực",
        type: 1,
        url: "Organizations/Domains"
    },
    {
        id: "Description",
        name: "Mô tả",
        type: 2
    },
    {
        id: "Layout",
        name: "Bố cục",
        type: 2
    },
    {
        id: "Owner",
        name: "Chủ sở hữu",
        type: 1
    },
    {
        id: "RevenueName",
        name: "Doanh thu",
        type: 1,
        url: "Revenues"
    },

    {
        id: "Facebook",
        name: "Facebook",
        type: 2
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

export const optionType1 = ["Xưng hô", "Họ và tên", "Chức danh", "Tỉnh/Thành phố", "Quận/Huyện", "Phường/Xã", "Nguồn gốc", "Loại hình", "Lĩnh vực", "Bố cục", "Chủ sở hữu", "Doanh thu", "Tổ chức"]

export const optionType2 = ["ĐT di động", "ĐT cơ quan", "Email cơ quan", "Email cá nhân", "Mô tả", "Facebook"]

