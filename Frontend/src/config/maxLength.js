import $ from 'jquery'
/**
 * TODO: định nghĩa giới hạn số ký tự của input
 * !Created by LVKIEN 20/09/2022
 */
const maxLengthArr = [
    { lastMiddleName: 50 },
    { firstName: 50 },
    { companyPhoneNum: 50 },
    { customerPhoneNum: 50 },
    { otherPhoneNum: 50 },
    { customerEmail: 100 },
    { companyEmail: 100 },
    { taxCode: 20 },
    { zalo: 50 },
    { organizationName: 255 },
    { bankAccount: 25 },
    { bankName: 255 },
    { homeNumber: 50 },
    { addressName: 255 },
    { description: 1000 },
    { potentialCode: 20 },
    { facebook: 255 },
]
const setMaxLengthInput = (container) => {
    try {
        $(container)
            .find("input")
            .toArray()
            .forEach((input) => {
                const idInput = $(input).attr("id");
                const maxLengthObj = maxLengthArr.find((item) => {
                    if (Object.keys(item)[0] === idInput) return item;
                });
                if (maxLengthObj) {
                    $(input).attr("maxlength", Object.values(maxLengthObj)[0]);
                }
            });
    } catch (error) {
        console.log(error);
    }
}
export default setMaxLengthInput