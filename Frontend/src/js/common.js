/**
    * TODO: Xử lý chuyển 1 object thành dữ liệu đầu vào cho dropdown : {id:, name:}
    * @param {*} objectArr 
    * !Created by LVKIEN 10/09/2022
    */
export const handleTransferObject = (objectArr) => {
    const objectKeys = Object.keys(objectArr[0])
    console.log(objectArr);
    let initResult = [{ id: 0, name: '-Không chọn-' }]
    const result = objectArr.map(item => {
        const resultObject = { id: item[objectKeys[0]], name: item[objectKeys[1]] }

        return resultObject
    })
    initResult = [...initResult, ...result]
    return initResult
}
// viết hoa chữ cái đầu
export const capitalizeFirstLetter = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1);
}
// viết thường chữ cái đầu
export const lowerCaseFirstLetter = (string) => {
    return string.charAt(0).toLowerCase() + string.slice(1);
}
/**
 * TODO: lấy về dữ liệu cho Combobox
 * @param {*} prefix
 * !Created by LVKIEN 10/09/2022
 */
export const getDataComboBox = async (prefix) => {
    let result = null;
    try {
        const response = await axios
            .get(`${rootApi}${prefix}`)
            .then((res) => res.data)
            .catch((error) => error.response.data);
        if (response.flag) {
            result = this.formatDataComboBox(response.data);
        } else {
            console.log(response.userMsg[0]);
        }
    } catch (error) {
        console.log(error);
        result = null;
    }
    return result;
}