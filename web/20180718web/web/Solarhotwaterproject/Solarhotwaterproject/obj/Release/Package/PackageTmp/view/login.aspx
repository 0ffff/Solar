<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Solarhotwaterproject.view.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>云南师范大学光伏制冷计量与监测平台</title>
    <link href="../css/newStyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="login-body">
    <form id="form1" runat="server">
        <div>
            <div id="mainBody">
                <div id="cloud1" class="cloud"></div>
                <div id="cloud2" class="cloud"></div>
            </div>
            <div class="loginbody">
                <img class="systemlogo" src="../img/loginlogo.png" alt="云南师范大学光伏制冷计量与监测平台" />
                <div class="loginbox">
                    <ul>
                        <li>
                            <input name="" type="text" class="loginuser" value="" id="loginuser"  onclick="JavaScript:this.value=''" />
                        </li>
                        <li>
                            <input name="" type="password" class="loginpwd" value="" id="loginpwd" onclick="JavaScript:this.value=''" />
                        </li>
                        <li>
                            <button class="loginbtn pull-right" onclick="login()">登录</button>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="loginbm">Copyright &copy;2018 云南师范大学</div>
        </div>
    </form>
    
    
    <script src="../js/jquery.min.js" type="text/javascript"></script>
   	
<%--<script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>--%>
    <script src="../js/common.js" type="text/javascript"></script>
    <script src="../js/cloud.js" type="text/javascript"></script>
    <script src="../js/login.js" type="text/javascript"></script>
</body>
</html>
