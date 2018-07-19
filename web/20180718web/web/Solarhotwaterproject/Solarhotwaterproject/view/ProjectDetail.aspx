<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectDetail.aspx.cs" Inherits="Solarhotwaterproject.view.ProjectDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>云南师范大学光伏制冷计量与监测平台</title>
    <%--<link href="https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../vendor/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
                    <li><a class="nav1 active" href="ProjectDetail.aspx"></a></li>
                    <%--<li><a class="nav2" href="RealTime.aspx"></a></li> --%>
                    <li><a class="nav3" href="QueryAnalysis.aspx"></a></li>
                    <li><a class="nav4" href="Configuration.aspx"></a></li>
                    <li><a class="nav5" href="Alarm.aspx"></a></li>
                </ul>
            </div>
        </div>

        <div class="content">
            <div class="main-content pull-left">
                <div class="main-content-header">
                    <div class="main-content-tab">
                        工程信息
                    </div>
                    <div class="main-content-tab active">
                        系统原理图
                    </div>
                </div>
                <div class="main-content-body">
                    <div class="table-white-bg ">
                        <table class="detail-table pull-left">
                            <tr>
                                <td class="table-title">项目编号</td>
                                <td class="table-content" id="ProjectCode">001</td>
                            </tr>
                            <tr>
                                <td class="table-title">项目所在省市</td>
                                <td class="table-content" id="AreaName">云南省昆明市</td>
                            </tr>
                            <tr>
                                <td class="table-title">项目名称</td>
                                <td class="table-content" id="ProjectName">光伏制冷系统</td>
                            </tr>
                            <tr>
                                <td class="table-title">项目地址</td>
                                <td class="table-content" id="ProjectAddress">云南师范大学</td>
                            </tr>
                            <tr>
                                <td class="table-title">技术类型</td>
                                <td class="table-content">106</td>
                            </tr>
                            <tr>
                                <td class="table-title">集热面积</td>
                                <td class="table-content" id="HeatingArea1">100㎡</td>
                            </tr>
                            <tr>
                                <td class="table-title">工程经度</td>
                                <td class="table-content" id="XAxis">102.69</td>
                            </tr>
                            <tr>
                                <td class="table-title">工程纬度</td>
                                <td class="table-content" id="YAxis">25.05</td>
                            </tr>
                            <tr>
                                <td class="table-title last-table-title">项目描述</td>
                                <td class="table-content"></td>
                            </tr>
                        </table>
                        <img class="project-pic" src="../img/331100001.jpg" />
                        
                        <div class="total-count pull-left">
                            <div class="heat-quantity ">
                                <div class="heat-quantity-title"><span class="glyphicon glyphicon-leaf"></span> 耗能统计</div>
                                <div class="heat-quantity-row clearfix">
                                    <img class="count-pic pull-left" src="../img/light_2.png" />
                                    <div class="count-detail pull-left">
                                        <div class="count-name">实验总市电电量：</div>
                                        <div class="count-value"><span id="total-electric"></span>kWh</div>
                                    </div>
                                </div>
                                <div class="heat-quantity-row clearfix">
                                    <img class="count-pic pull-left" src="../img/light_1.png" />
                                    <div class="count-detail pull-left">
                                        <div class="count-name">实验压缩机用电量：</div>
                                        <div class="count-value"><span id="CMCelectric"></span>kWh</div>
                                    </div>
                                </div>
                                <div class="heat-quantity-row clearfix">
                                    <img class="count-pic pull-left" src="../img/water.png" />
                                    <div class="count-detail pull-left">
                                        <div class="count-name">实验光伏供电量：</div>
                                        <div class="count-value"><span id="Solarelectric"></span>kWh</div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        
                    </div>
                    <div id="Flash" class="active "></div>
                </div>
            </div>

            <div class="right-nav right-nav1 pull-left">
                <a class="nav-btn nav4 active" id="nav1" href="javascript:;"></a>
                <a class="nav-btn nav5" id="nav2" href="javascript:;"></a>
            </div>

        </div>
        
    </div>
    </form>
    
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <%--<script src="https://cdn.bootcss.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <script src="../vendor/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <script src="../js/bootstrap-select.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <script src="../js/common.js" type="text/javascript"></script>
    <script src="../js/ProjectDetail.js"></script>
</body>
</html>
