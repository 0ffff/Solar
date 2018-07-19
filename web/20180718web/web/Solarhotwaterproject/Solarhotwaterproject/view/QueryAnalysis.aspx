<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryAnalysis.aspx.cs" Inherits="Solarhotwaterproject.view.QueryAnalysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>云南师范大学光伏制冷计量与监测平台</title>
    <%--<link href="https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../vendor/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../css/newStyle.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <div class="main query-analysis">
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
                    <li><a class="nav3 active" href="QueryAnalysis.aspx"></a></li>
                    <li><a class="nav4" href="Configuration.aspx"></a></li>
                    <li><a class="nav5" href="Alarm.aspx"></a></li>
                </ul>
            </div>
        </div>

        <div class="content clearfix content1">
            <%-- 监测数据 --%>
           <div class="left-part pull-left left-part1">
                <div class="mypanel-box m-t-0">
                    <div class="mypanel-header">
                         <span class="glyphicon glyphicon-calendar"></span> 监测数据查询
                    </div>
                    <div class="form-horizontal mypanel-body">
                        <div class="row m-b-15 without-padding m-t-0">
                            <div class="col-md-12">
                                <div class="input-group input-group-sm date " id="StartTime" data-date-format="yyyy-mm-dd">
                                    <span class="input-group-addon">开始时间</span>
                                    <input id="startm" type="text" class="form-control input-sm" size="13" onchange="showChart2(),loadTable()" placeholder=""/>
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
                                    <input id="endtm" type="text" class="form-control input-sm" size="13" onchange="showChart2(),loadTable()" placeholder=""/>
                                    <div class="input-group-addon last">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row without-padding m-t-10">
                            <div class="col-md-12">
                                <button class="btn btn-success btn-sm pull-right" >确定</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="left-bottom-panel clearfix">
                    <div class="panel-tab pull-left">
                        <div class="panel-tab1 active">
                            <span class="glyphicon glyphicon-stats"></span>
                            <span class="glyphicon-text">图形数据</span>
                        </div>
                        <div class="panel-tab2">
                            <span class="glyphicon glyphicon-list-alt"></span>
                            <span class="glyphicon-text">表格数据</span>
                        </div>
                    </div>
                    <div class="panel-content pull-left">
                        <div class="panel-dark-title">
                            <span>监测指标 ></span>
                            <a href="javascript:;" onclick="checkAll()">全选</a>
                            <a href="javascript:;" onclick="NonecheckAll()">全不选</a>
                        </div>
                        <div class="left-panel-content" id="chooseBox">
                            <%--<div class="checkbox"><label><input type="checkbox" />集热器出口温度</label></div>
                            <div class="checkbox"><label><input type="checkbox" />水箱温度</label></div>
                            <div class="checkbox"><label><input type="checkbox" />供水温度</label></div>
                            <div class="checkbox"><label><input type="checkbox" />环境温度</label></div>
                            <div class="checkbox"><label><input type="checkbox" />水箱液位高度</label></div>--%>
                             <ul class="site-tree" id="site-tree"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="middle-content pull-left middle-content1">
                <div class="white-bg active" id="chart1"></div>
                <div class="white-bg no-padding"  style="overflow:auto;">
                    <table class="table table-striped table-bordered table-responsive" id="bsTable"></table>
                </div>
            </div>
            <%-- 统计数据 --%>
            
          
            <div class="right-nav right-nav2 pull-left">
                <a class="nav-btn nav3 active" id="nav1" href="javascript:;"></a>
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
    <script src="../js/bootstrap-table.min.js"></script>
    <script src="../js/bootstrap-table-zh-CN.min.js"></script>
    <script src="../js/FusionCharts.js" type="text/javascript"></script>
    <script src="../js/fusioncharts.charts.js"></script>
    <script src="../js/fusioncharts.widgets.js"></script>
    <script src="../js/highcharts.js"></script>
    <script src="../js/highcharts-zh_CN.js"></script>
    <script src="../js/highcharts-more.js"></script>
    <script src="../js/common.js" type="text/javascript"></script>
    <script src="../js/QueryAnalysis.js"></script>

<%--    <script>
        $(".form_date").datetimepicker({
            format: "yyyy-mm-dd",
            language: 'zh-CN',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
        });
    </script>--%>
</body>
</html>
