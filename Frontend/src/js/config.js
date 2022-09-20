// các trường dùng để cập nhật nhiều
export const fieldOptions = ["Xưng hô", "Họ và tên", "Chức danh", "ĐT di động", "ĐT cơ quan", "Email cơ quan", "Email cá nhân", "Tổ chức", "Địa chỉ", "Tỉnh/Thành phố", "Quận/Huyện", "Phường/Xã", "Nguồn gốc", "Loại hình", "Lĩnh vực", "Mô tả", "Bố cục", "Chủ sở hữu", "Doanh thu", "Facebook"]

export const dropdownField = [
    "vocative", "departmentId", "positionId", "sourceId", "typeId", "revenueId", "countryId", "cityId", "districtId", "wardId"
]

//các trường tiếng việt mapping sang tiếng anh
export const fieldMappingOptions =
    [{
        id: "Vocative",
        name: "Xưng hô",
        type: 1,
        url: "Vocatives",
        table: "customer",
        fieldUpdate: "Vocative"
    }, {
        id: "FullName",
        name: "Họ và tên",
        type: 1,
        table: "customer",
        fieldUpdate: "FullName"
    },
    {
        id: "PositionName",
        name: "Chức danh",
        type: 1,
        url: "Positions",
        table: "customer",
        fieldUpdate: "PositionId"
    },
    {
        id: "CustomerPhoneNum",
        name: "ĐT di động",
        type: 2,
        table: "customer",
        fieldUpdate: "CustomerPhoneNum"

    },
    {
        id: "CompanyPhoneNum",
        name: "ĐT cơ quan",
        type: 2,
        table: "customer",
        fieldUpdate: "CompanyPhoneNum"
    },
    {
        id: "CompanyEmail",
        name: "Email cơ quan",
        type: 2,
        table: "customer",
        fieldUpdate: "CompanyEmail"

    }
    ,
    {
        id: "CustomerEmail",
        name: "Email cá nhân",
        type: 2,
        table: "customer",
        fieldUpdate: "CustomerEmail"

    },
    {
        id: "OrganizationName",
        name: "Tổ chức",
        type: 1,
        url: "Organizations",
        table: "organization",
        fieldUpdate: "OrganizationName"

    },
    {
        id: "AddressName",
        name: "Địa chỉ",
        type: 2,
        table: "address",
        fieldUpdate: "AddressName"
    },
    {
        id: "CityName",
        name: "Tỉnh/Thành phố",
        type: 1,
        url: "Cities/AllCities",
        table: "address",
        fieldUpdate: "CityId"
    },
    {
        id: "DistrictName",
        name: "Quận/Huyện",
        type: 1,
        url: "Districts/AllDistricts",
        table: "address",
        fieldUpdate: "DistrictId"
    },
    {
        id: "WardName",
        name: "Phường/Xã",
        type: 1,
        url: "Wards/AllWards",
        table: "address",
        fieldUpdate: "WardId"
    },
    {
        id: "SourceName",
        name: "Nguồn gốc",
        type: 1,
        url: "Sources",
        table: "customer",
        fieldUpdate: "SourceId"
    },
    {
        id: "TypeName",
        name: "Loại hình",
        type: 1,
        url: "Types",
        table: "organization",
        fieldUpdate: "TypeId"
    },
    {
        id: "Domain",
        name: "Lĩnh vực",
        type: 1,
        url: "Organizations/Domains",
        table: "organization",
        fieldUpdate: "Domain"
    },
    {
        id: "Description",
        name: "Mô tả",
        type: 2,
        table: "customer",
        fieldUpdate: "Description"
    },
    {
        id: "Layout",
        name: "Bố cục",
        type: 2,
        table: "customer",
        fieldUpdate: "Layout"
    },
    {
        id: "Owner",
        name: "Chủ sở hữu",
        type: 1,
        table: "customer",
        fieldUpdate: "Owner"
    },
    {
        id: "RevenueName",
        name: "Doanh thu",
        type: 1,
        url: "Revenues",
        table: "organization",
        fieldUpdate: "RevenueId"
    },

    {
        id: "Facebook",
        name: "Facebook",
        type: 2,
        table: "customer",
        fieldUpdate: "Facebook"
    }
    ]

// các trường có kiểu input
export const fieldInputOptions = ["Họ và tên", "ĐT di động", "ĐT cơ quan", "Email cơ quan", "Email cá nhân", "Địa chỉ", "Mô tả", "Bố cục", "Chủ sở hữu", "Dùng chung", "Facebook", "Tổ chức"]

// các trường kiểu dropdown
export const fieldDropdownOptions = ["Xưng hô", "Chức danh", "Tỉnh/Thành phố", "Quận/Huyện", "Phường/Xã", "Nguồn gốc", "Loại hình", "Doanh thu"]

// dữ liệu cho dropdown pageSize
export const numPerPage = [
    { id: 10, name: "10 Bản ghi trên trang" },
    { id: 20, name: "20 Bản ghi trên trang" },
    { id: 50, name: "50 Bản ghi trên trang" },
    { id: 100, name: "100 Bản ghi trên trang" },
]
// Dữ liệu cho option loại 1
export const optionType1 = [
    { id: 1, name: "Là" },
    { id: 2, name: "Chứa" },
    { id: 3, name: "Không là" },
    { id: 4, name: "Không chứa" },
]
// Dữ liệu cho option loại 2
export const optionType2 = [
    { id: 5, name: "Không trống" },
    { id: 6, name: "Trống" },
]
