//验证是否正整数格式
function CheckNum(obj) {
    if (!obj.value.match(/^[0-9]*[1-9][0-9]*$/))
        obj.value = 999;
}
//检查是否为空
function isEmpty(obj) {
    if (typeof obj == "undefined" || obj == null || obj == "") {
        return true;
    } else {
        return false;
    }
}