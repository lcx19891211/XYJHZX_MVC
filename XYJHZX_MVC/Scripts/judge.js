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

function divFadeAlert(obj) {
    var hidvalue_str = obj;
    var divWidth = 150;
    var divHeight = 150;
    var iLeft = ($(window).width() - divWidth) / 2;
    var iTop = ($(window).height() - divHeight) / 2 + $(document).scrollTop();
    var divhtml = $("<div>" + hidvalue_str + "</div>").css({
        position: 'absolute', top: iTop + 'px', left: iLeft + 'px', display: 'none', width: divWidth + 'px', height: divHeight + 'px', 'background-color': '#FFFFFF', 'font-family': 'SimHei', 'font-size': '30px'
    });
    divhtml.appendTo('body').fadeIn();
    divhtml.appendTo('body').fadeOut(3000);
}
