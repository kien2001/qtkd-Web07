// format dữ liệu theo định dạng của dropdown theo dạng mảng: {id:, name:}
export const handleTransferObject = (objectArr) =>{
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
export const lowerCaseFirstLetter = (string) =>{
    return string.charAt(0).toLowerCase() + string.slice(1);
}