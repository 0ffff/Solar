/* 标签切换 */
//$(".my-subnav li").click(function () {
//    $(this).addClass("current").siblings().removeClass("current");
//    $(".cont").eq($(this).index()).addClass("cont-show").siblings().removeClass("cont-show");
//})
//WEB API链接
var commonUrl = "http://localhost:19188/";
//设置cookie
function setCookie(name, value, day) {
    var date = new Date();
    date.setDate(date.getDate() + day);
    document.cookie = name + '=' + value + ';expires=' + date;
};
//获取cookie
function getCookie(name) {
    var reg = RegExp(name + '=([^;]+)');
    var arr = document.cookie.match(reg);
    if (arr) {
        return arr[1];
    } else {
        return '';
    }
};
//删除cookie
function delCookie(name) {
    setCookie(name, null, -1);
};

//时间显示
function showDateString () {
    var dateTime = new Date();
    var time = dateTime.toTimeString().split(" ")[0];
    var date = dateTime.toLocaleDateString();
    var day = dateTime.getDay();
    //var weekday = ["Sunday", "Monday", "Tuesday", "Thursday", "Friday", "Saturday"];
    var weekday = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
    return date + " " + weekday[day] + " " + time;
}

$(document).ready(function () {
    setInterval(function () {
        $("#dateTime").html(showDateString());
    }, 1000);
})

//全选与全不选
function chooseAll(flag) {
    var checkbox = document.getElementById("chooseBox").getElementsByTagName('input')
    if (flag) {//flag==1时全选
        for (var i = 0; i < checkbox.length; i++) {
            checkbox[i].checked = true;
        }
    } else {
        for (var i = 0; i < checkbox.length; i++) {
            checkbox[i].checked = false;
        }
    }
}