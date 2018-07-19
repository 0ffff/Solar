
function login() {
    //阻止默认事件
    var theEvent = window.event || arguments.callee.caller.arguments[0];
    if (theEvent.preventDefault) {
        theEvent.preventDefault();
    } else {
        theEvent.returnValue = false;
    }
    var userName = document.getElementById("loginuser").value;
    var userPassword = document.getElementById("loginpwd").value;

    //window.location = "Login.aspx?userName=" + userName + "&userPassword=" + userPassword;
    $.post(commonUrl + "api/Login/Login",
    {
        UserID: userName,
        UserPassword: userPassword
    },
        function (data, status) {
            if (0 != data.status) {
                alert(data.message); //提示框
                return;
            }
            var msg = data.message.rows;
            if (msg == true) {
                setCookie('user', userName.value, 7); //保存帐号到cookie，有效期7天
                setCookie('password', userPassword.value, 7); //保存密码到cookie，有效期7天
                $(window).attr('location', 'ProjectDetail.aspx');
                $("#userName").html(userName);
            } else {
                alert("密码错误！"); //提示框
            }


        },
"json");
}
$("#loginpwd").keydown(function () {
    var theEvent = window.event || arguments.callee.caller.arguments[0];
    if (theEvent.keyCode == 13) {//当按下回车键时触发
        console.log("Enter pressed!");
        login();
    }
})