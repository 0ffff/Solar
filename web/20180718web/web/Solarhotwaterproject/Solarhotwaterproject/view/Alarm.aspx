<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alarm.aspx.cs" Inherits="Solarhotwaterproject.view.Alarm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>太阳能集热工程能量计量与监测平台</title>
    <%--<link href="https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../vendor/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-table.min.css" rel="stylesheet" />
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
                    <img src="../img/logo.png" class="logo" alt="太阳能集热工程能量计量与监测平台" />
                    <select class="form-control input-sm unit-select">
                        <option value="云南师范大学">云南师范大学</option>
                    </select>
                </div>
                <ul class="nav-box pull-left">
                    <li><a class="nav1" href="ProjectDetail.aspx"></a></li>
                    <%--<li><a class="nav2" href="RealTime.aspx"></a></li> --%>
                    <li><a class="nav3" href="QueryAnalysis.aspx"></a></li>
                    <li><a class="nav4" href="Configuration.aspx"></a></li>
                    <li><a class="nav5 active" href="Alarm.aspx"></a></li>
                </ul>
            </div>
        </div>

        <div class="content clearfix pageAlarm">
            <div class="left-part pull-left">
                <div class="mypanel-box">
                    <div class="mypanel-header">
                         <span class="glyphicon glyphicon-calendar"></span>监测数据查询
                    </div>
                    <div class="form-horizontal mypanel-body">
                        <div class="row m-b-15 without-padding">
                            <div class="col-md-12">
                                <div class="input-group input-group-sm date " id="StartTime" data-date-format="yyyy-mm-dd">
                                    <span class="input-group-addon">开始时间</span>
                                    <input id="startm" type="text" class="form-control input-sm" size="13" onchange="loadTable()" placeholder=""/>
                                    <div class="input-group-addon last">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row without-padding">
                            <div class="col-md-12">
                                <div class="input-group input-group-sm date " id="EndTime" data-date-format="yyyy-mm-dd">
                                    <span class="input-group-addon">结束时间</span>
                                    <input id="endtm"  type="text" class="form-control input-sm" size="13" onchange="loadTable()" placeholder=""/>
                                    <div class="input-group-addon last">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="middle-content pull-left">
                <div class="white-bg active no-padding">
                    <table class="table table-striped table-bordered" id="bsTable"></table>
                </div>
            </div>
        </div>
        
    </div>
    </form>
    
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdn.bootcss.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="../js/bootstrap-select.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <script src="../js/bootstrap-table.min.js"></script>
    <script src="../js/bootstrap-table-zh-CN.min.js"></script>
    <script src="../js/common.js" type="text/javascript"></script>
    <script src="../js/Alarm.js"></script>
</body>
</html>
