<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="Solarhotwaterproject.view.Configuration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>云南师范大学光伏制冷计量与监测平台</title>
    <%--<link href="https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../vendor/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/newStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="header">
            <div class="header-bar clearfix">
                <div class="user-info pull-left"><span class="glyphicon glyphicon-user"></span>你好，<span id="userName">SolarMCool</span> 用户！<a class="log-out" href="login.aspx">[退出登录]</a></div>
                <div class="dateTime pull-right" id="dateTime"></div>
            </div>
            <div class="header-cont">
                <div class="header-left pull-left">
                    <img src="../img/logo.png" class="logo" alt="云南师范大学光伏制冷计量与监测平台" />
                    <select class="form-control input-sm unit-select">
                        <option value="云南师范大学">云南师范大学</option>
                    </select>
                </div>
                <ul class="nav-box pull-left">
                    <li><a class="nav1" href="ProjectDetail.aspx"></a></li>
                    <%--<li><a class="nav2" href="RealTime.aspx"></a></li> --%>
                    <li><a class="nav3" href="QueryAnalysis.aspx"></a></li>
                    <li><a class="nav4 active" href="Configuration.aspx"></a></li>
                    <li><a class="nav5" href="Alarm.aspx"></a></li>
                </ul>
            </div>
        </div>

        <div class="content">
            <div class="mypanel-box">
                <div class="mypanel-header">
                     <span class="glyphicon glyphicon-user"></span>登入密码修改
                </div>
                <div class="form-horizontal mypanel-body">
                    <div class="mypanel-row clearfix">
                        <label class="control-label">原密码</label>
                        <input id="Oldpassword" class="form-control input-sm" type="password" />
                    </div>
                    <div class="mypanel-row clearfix">
                        <label class="control-label">新密码</label>
                        <input id="newpassword" class="form-control input-sm" type="password" />
                    </div>
                    <div class="mypanel-row clearfix">
                        <label class="control-label">重复密码</label>
                        <input id="newpassword2" class="form-control input-sm" type="password" />
                    </div>
                    <a class="btn btn-success btn-sm pull-right" href="javscript:;" onclick="save()">确定</a>
                </div>
            </div>
        </div>
        
    </div>
    </form>

<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">修改成功</h4>
      </div>
      <div class="modal-body">
        <p>修改密码成功！</p>
      </div>
      <div class="modal-footer">
        <button  type="button" class="btn btn-primary btn-sm" data-dismiss="modal">确定</button>
        <%--<button type="button" class="btn btn-primary">确定</button>--%>
      </div>
    </div>
  </div>
</div>
    
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdn.bootcss.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="../js/bootstrap-select.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <script src="../js/common.js" type="text/javascript"></script>
    <script>
        function save() {
            //if (remember.checked) {
            //    setCookie('user', oUser.value, 7); //保存帐号到cookie，有效期7天
            //    setCookie('password', oPswd.value, 7); //保存密码到cookie，有效期7天
            //}
            if ($("#newpassword").val() != $("#newpassword2").val()) {
                alert("两次密码不一致，请重新输入！");
                $("#newpassword").val("");
                $("#newpassword2").val("");
                
            } else {
                $.post(commonUrl + "api/Login/UpdateProjectInfo",
           {
               UserID: 'gfzl',//[备注人]：张超 [说明]：此处已经写死,正常逻辑这样写是不对的。
               OldUserPassword: $("#Oldpassword").val(),
               newUserPassword: $("#newpassword").val()
           },
               function (data, status) {
                   if (0 != data.status) {
                       alert(data.message); //提示框
                       return;
                   }
                   var msg = data.message.rows;
                   if (msg == true) {
                       //setCookie('user', oUser.value, 7); //保存帐号到cookie，有效期7天
                       //setCookie('password', oPswd.value, 7); //保存密码到cookie，有效期7天
                       //$(window).attr('location', 'ComprehensiveMap.aspx');
                       alert("修改成功！");
                       window.location.href="login.aspx";
                       
                   } else {
                       alert("密码错误！"); //提示框
                      
                   }


               },
   "json");
            }
           
        }


    </script>
</body>
</html>
