$('#StartTime,#EndTime').datetimepicker({
    language: 'zh-CN',
    autoclose: true,
    startView: 4,
    maxView: 'decade',
    minView: 'month',
    viewSelect: 'month',
    todayBtn: 'true',
    pickerPosition: 'bottom-right'
});
//设置时间
setDay();
function setDay() {
    var myDate = new Date();
    //获取昨天同一时刻的
    var t = myDate.getTime() - 1000 * 60 * 60 * 24*365;
    var yesDate = new Date(t);

    //获取当前年
    var year = myDate.getFullYear();
    //获取前一天的年
    var bfyear = yesDate.getFullYear();

    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取前一天的月
    var bfmonth = yesDate.getMonth() + 1;

    //获取当前日
    var day = myDate.getDate();
    //获取前一天日
    var bfday = yesDate.getDate();

    //个位数处理加0
    if (month < 10) {
        month = "0" + month;
    }
    if (bfmonth < 10) {
        bfmonth = "0" + bfmonth;
    }
    if (day < 10) {
        day = "0" + day;
    }
    if (bfday < 10) {
        bfday = "0" + bfday;
    }

    //当天
    day = year + '-' + month + '-' + day;
    //前一天
    Yesterday = bfyear + '-' + bfmonth + '-' + bfday;
    $("#startm").val(Yesterday);
    $("#endtm").val(day);
}
var Nodes = [
    { Station_Code: "房间温度", Station_Name: "房间温度" },
    { Station_Code: "冰块温度", Station_Name: "冰块温度" },
    { Station_Code: "蒸发器出口温度", Station_Name: "蒸发器出口温度" },
    { Station_Code: "蒸发器入口温度", Station_Name: "蒸发器入口温度" },
    { Station_Code: "压缩机排气温度", Station_Name: "压缩机排气温度" },
    { Station_Code: "冷凝器出口温度", Station_Name: "冷凝器出口温度" },
    { Station_Code: "风机盘管出风口温度", Station_Name: "风机盘管出风口温度" },
    { Station_Code: "压缩机排气压力", Station_Name: "压缩机排气压力" },
    { Station_Code: "蒸发器出口压力", Station_Name: "蒸发器出口压力" },
    { Station_Code: "供冷冷水质量流量", Station_Name: "供冷冷水质量流量" }



];
var checkList = [];
var field = [];
/* 默认勾选前五个 */
for (var i = 0; i < 3; i++) {
    Nodes[i].checked = true;
    checkList.push(Nodes[i].Station_Code);
}
treeInit(Nodes, "#site-tree");
/* 列初始化
* @param namelist 节点名单
* @param eleName  ul元素ID，例如 #tree
*/

function treeInit(namelist, eleName) {
    $(eleName).children('li').remove();
    for (var i = 0; i < namelist.length; i++) {
        //checklist[i] = (!!namelist[i].checked);
        if (!!namelist[i].checked) {
            $(eleName).append('<li><input type="checkbox" id="site' + (i + 1) + '" checked="true" onChange="checkChange(' + i + ',\'' + eleName + '\')" /><label for="site' + (i + 1) + '">' + namelist[i].Station_Name + '</label></li>');
        } else {
            $(eleName).append('<li><input type="checkbox" id="site' + (i + 1) + '" onChange="checkChange(' + i + ',\'' + eleName + '\')" /><label for="site' + (i + 1) + '">' + namelist[i].Station_Name + '</label></li>');
        }
    }
    showChart2();
}
function checkChange(num, eleName) {
    Nodes[num].checked = $(eleName).children("li").eq(num).children("input").is(":checked");
    checkList = [];
    for (var i = 0; i < Nodes.length; i++) {
        if (!!Nodes[i].checked) {
            checkList.push(Nodes[i].Station_Code);
        }
    }
    showChart2();
}
//
//折线图(第一页)
function showChart2() {

    var stcd = "";
    for (var i = 0; i < checkList.length; i++) {
        stcd += checkList[i] + ",";
    }
    var length = stcd.length;
    var stcdstr = stcd.substring(0, length - 1)
    $.post(commonUrl + "api/Query/GetMeasurementIndex", {
        stcd: stcdstr,
        starttm: $("#startm").val(),
        endtm: $("#endtm").val()
    },
       function (data, status) {
           if (0 != data.status) {
               alert(data.message); //提示框
               return;
           }
           var msg = data;
           var rows;
           var Time = [];
           var datajson = [];
           if (data.message.rows != "") {
               rows = data.message.rows;
               msg.rows = rows;
               for (var i = 0; i < msg.rows.length; i++) {
                   Time[i] = msg.rows[i].TimeStamp.substring(0, 10) + " " + msg.rows[i].TimeStamp.substring(11, 19);
               }
               for (var j = 0; j < checkList.length; j++) {
                   if (checkList[j] == "房间温度") {
                       var arr1 = {}
                       arr1.name = "房间温度";
                       arr1.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr1.data.push(parseFloat(msg.rows[k].房间温度));

                       }
                       datajson[j] = arr1;
                   } else if (checkList[j] == "冰块温度") {
                       var arr2 = {}
                       arr2.name = "冰块温度";
                       arr2.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr2.data.push(parseFloat(msg.rows[k].冰块温度));
                       }
                       datajson[j] = arr2;
                   } else if (checkList[j] == "蒸发器出口温度") {
                       var arr3 = {}
                       arr3.name = "蒸发器出口温度";
                       arr3.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr3.data.push(parseFloat(msg.rows[k].蒸发器出口温度));
                       }
                       datajson[j] = arr3;
                   } else if (checkList[j] == "蒸发器入口温度") {
                       var arr4 = {}
                       arr4.name = "蒸发器入口温度";
                       arr4.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr4.data.push(parseFloat(msg.rows[k].蒸发器入口温度));
                       }
                       datajson[j] = arr4;
                   } else if (checkList[j] == "压缩机排气温度") {
                       var arr5 = {}
                       arr5.name = "压缩机排气温度";
                       arr5.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr5.data.push(parseFloat(msg.rows[k].压缩机排气温度));
                       }
                       datajson[j] = arr5;
                   }
                   else if (checkList[j] == "冷凝器出口温度") {
                       var arr6 = {}
                       arr6.name = "冷凝器出口温度";
                       arr6.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr6.data.push(parseFloat(msg.rows[k].冷凝器出口温度));
                       }
                       datajson[j] = arr6;
                   } else if (checkList[j] == "风机盘管出风口温度") {
                       var arr7 = {}
                       arr7.name = "风机盘管出风口温度";
                       arr7.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr7.data.push(parseFloat(msg.rows[k].风机盘管出风口温度));
                       }
                       datajson[j] = arr7;
                   } else if (checkList[j] == "压缩机排气压力") {
                       var arr8 = {}
                       arr8.name = "压缩机排气压力";
                       arr8.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr8.data.push(parseFloat(msg.rows[k].压缩机排气压力));
                       }
                       datajson[j] = arr8;
                   } else if (checkList[j] == "蒸发器出口压力") {
                       var arr9 = {}
                       arr9.name = "蒸发器出口压力";
                       arr9.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr9.data.push(parseFloat(msg.rows[k].蒸发器出口压力));
                       }
                       datajson[j] = arr9;
                   } else if (checkList[j] == "供冷冷水质量流量") {
                       var arr10 = {}
                       arr10.name = "供冷冷水质量流量";
                       arr10.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr10.data.push(parseFloat(msg.rows[k].供冷冷水质量流量));
                       }
                       datajson[j] = arr10;
                   }
               }
           }




           //曲线图
           var chart3 = Highcharts.chart('chart1', {
               chart: {
                   type: 'spline',
                   zoomType: 'x'
               },
               title: {
                   text: ''
               },
               subtitle: {
                   text: ''
               },
               xAxis: {
                   categories: Time
               },
               yAxis: {
                   title: {
                       text: '温度℃'
                   },
                   labels: {
                       formatter: function () {
                           return this.value + '℃';
                       }
                   }
               },
               tooltip: {
                   headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                   pointFormat: '<tr><td style="padding:0"><b>{series.name}: {point.y:.1f}℃</b></td></tr>',
                   footerFormat: '</table>',
                   useHTML: true,
                   crosshairs: {
                       width: 1,
                       color: "#ccc",
                       dashStyle: 'solid'
                   },
                   shared: true
               },
               plotOptions: {
                   spline: {
                       marker: {
                           radius: 4,
                           lineColor: '#666666',
                           lineWidth: 1
                       }
                   }
               },
               series: datajson,
               credits: {
                   enabled: false
               }
           });


       },
       "json");

}

//全不选
function NonecheckAll() {
    for (var i = 0; i < 10; i++) {
        Nodes[i].checked = false;
        checkList.push(Nodes[i].Station_Code);
        checkList = [];
    }
    treeInit(Nodes, "#site-tree");

}
//全选
function checkAll() {
    for (var i = 0; i < 10; i++) {
        Nodes[i].checked = true;
        checkList.push(Nodes[i].Station_Code);
    }
    treeInit(Nodes, "#site-tree");

}



///翻页
$(".left-part1 .panel-tab > div").click(function () {
    if (!$(this).hasClass("active")) {
        $(this).addClass("active").siblings().removeClass("active");
        $(".white-bg").eq($(this).index()).addClass("active").siblings().removeClass("active");
    } else {
        //$(this).removeClass("active").siblings().addClass("active");
    }
    getindex3();
})
$(".left-part2 .panel-tab > div").click(function () {
    if (!$(this).hasClass("active")) {
        $(this).addClass("active").siblings().removeClass("active");
        $(".left-part2 .left-panel-content").eq($(this).index()).addClass("active").siblings(".left-panel-content").removeClass("active");
        $(".middle-content2 .white-bg").eq($(this).index()).addClass("active").siblings().removeClass("active");
    }
    getindex2();
    getindex3();
})

$("#nav1").click(function () {
    if (!$(this).hasClass("active")) {
        $(this).addClass("active").siblings().removeClass("active");
        $(".content").removeClass("content2").addClass("content1");
    }
})
$("#nav2").click(function () {
    if (!$(this).hasClass("active")) {
        $(this).addClass("active").siblings().removeClass("active");
        $(".content").removeClass("content1").addClass("content2");
    }
    setyeartime();
    getindex();
})
$(".time-interval").click(function () {
    if (!$(this).hasClass("active")) {
        $(this).addClass("active").siblings().removeClass("active");
        //console.log($(this).attr("id"));//从ID输出具体点的什么
        getindex();
    }
})
// 设置时分查询框格式
function sethourtime() {
    $('#hourtmdiv').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        maxView: 'year',
        minView: 'month',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    $('#hourtmdiv2').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        maxView: 'year',
        minView: 'month',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    $('#hourtmdiv3').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        maxView: 'year',
        minView: 'month',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    var myDate = new Date();

    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var bfday = date - 1;
    day = year + '-' + month + '-' + date;
    $("#hourtm").val(day);
    $("#hourtm2").val(day);
    //$("#hourtm3").val(day);
    //第一页
    document.getElementById("hourtmdiv").style.display = "";//显
    document.getElementById("daytmdiv").style.display = "none";//隐
    document.getElementById("yeartmdiv").style.display = "none"//隐
    document.getElementById("monthtmdiv").style.display = "none"//
    //第二页
    document.getElementById("hourtmdiv2").style.display = "";//显
    document.getElementById("daytmdiv2").style.display = "none";//隐
    document.getElementById("yeartmdiv2").style.display = "none"//隐
    document.getElementById("monthtmdiv2").style.display = "none"//
    //第三页
    //document.getElementById("hourtmdiv3").style.display = "none";//隐
    document.getElementById("daytmdiv3").style.display = "none";//隐
    document.getElementById("yeartmdiv3").style.display = "none"//隐
    document.getElementById("monthtmdiv3").style.display = "none"//
    //getindex();
    if ($("#panel-tab3").css("display") != "none") {
        $("#panel-tab3").css("display", "none");
        if ($("#panel-tab3").hasClass("active")) {
            $("#panel-tab3").removeClass("active").siblings(".panel-tab1").addClass("active");
            $("#chooseBox2").addClass("active");
        }

    }
}
//设置日期查询框格式
function setdaytime() {
    $('#daytmdiv').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 2,
        maxView: 'year',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    $('#daytmdiv2').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 2,
        maxView: 'year',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    $('#daytmdiv3').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        maxView: 'year',
        minView: 'month',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    var myDate = new Date();

    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var bfday = date - 1;
    day = year + '-' + month ; 
    $("#daytm").val(day);
    $("#daytm2").val(day);
    $("#daytm3").val(day + '-'+date);
    //第一页
    document.getElementById("daytmdiv").style.display = "";//显
    document.getElementById("yeartmdiv").style.display = "none"//隐
    document.getElementById("monthtmdiv").style.display = "none"//
    document.getElementById("hourtmdiv").style.display = "none";//显
    //第二页
    document.getElementById("daytmdiv2").style.display = "";//显
    document.getElementById("yeartmdiv2").style.display = "none"//隐
    document.getElementById("monthtmdiv2").style.display = "none"//
    document.getElementById("hourtmdiv2").style.display = "none";//显
    //第三页
    document.getElementById("daytmdiv3").style.display = "";//显
    document.getElementById("yeartmdiv3").style.display = "none"//隐
    document.getElementById("monthtmdiv3").style.display = "none"//
   // document.getElementById("hourtmdiv3").style.display = "none";//显
    //getindex();
    if ($("#panel-tab3").css("display") == "none") {
        $("#panel-tab3").css("display", "block")
    }
}
////设置月查询框格式
function setmonthtime() {

    $('#monthtmdiv').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 4,
        maxView: 'dacade',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    $('#monthtmdiv2').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 4,
        maxView: 'dacade',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    $('#monthtmdiv3').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 2,
        maxView: 'year',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });

    var myDate = new Date();
    var year = myDate.getFullYear();
    var month = myDate.getMonth() + 1;
    
    month = year + '-' + month;
    //获取当前年
  
    $("#monthtm").val(year);
    $("#monthtm2").val(year);
    $("#monthtm3").val(month);
    //第一页
    document.getElementById("hourtmdiv").style.display = "none";//显
    document.getElementById("daytmdiv").style.display = "none"//隐
    document.getElementById("yeartmdiv").style.display = "none"
    document.getElementById("monthtmdiv").style.display = "";//显
    //第二页
    document.getElementById("hourtmdiv2").style.display = "none";//显
    document.getElementById("daytmdiv2").style.display = "none"//隐
    document.getElementById("yeartmdiv2").style.display = "none"
    document.getElementById("monthtmdiv2").style.display = "";//显
    //第三页
  // document.getElementById("hourtmdiv3").style.display = "none";//显
    document.getElementById("daytmdiv3").style.display = "none"//隐
    document.getElementById("yeartmdiv3").style.display = "none"
    document.getElementById("monthtmdiv3").style.display = "";//显
    // getindex();
    if ($("#panel-tab3").css("display") == "none") {
        $("#panel-tab3").css("display", "block")
    }
}
////设置年查询框格式
function setyeartime() {

    $('#yeartmdiv').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 4,
        maxView: 'dacade',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });

    $('#yeartmdiv2').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 4,
        maxView: 'dacade',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });

    $('#yeartmdiv3').datetimepicker({
        language: 'zh-CN',
        autoclose: true,
        startView: 4,
        endView: 4,
        maxView: 'dacade',
        minView: 'year',
        viewSelect: 'decade',
        todayBtn: 'true',
        pickerPosition: 'bottom-left'
    });
    var myDate = new Date();

    //获取当前年
    var year = myDate.getFullYear();
    $("#yeartm").val(year);
    $("#yeartm2").val(year);
    $("#yeartm3").val(year);
    //第一页
    document.getElementById("hourtmdiv").style.display = "none";//显
    document.getElementById("daytmdiv").style.display = "none"
    document.getElementById("monthtmdiv").style.display = "none"//隐
    document.getElementById("yeartmdiv").style.display = "";//显
    //第二页
    document.getElementById("hourtmdiv2").style.display = "none";//显
    document.getElementById("daytmdiv2").style.display = "none"
    document.getElementById("monthtmdiv2").style.display = "none"//隐
    document.getElementById("yeartmdiv2").style.display = "";//显
    //第三页
     //document.getElementById("hourtmdiv3").style.display = "none";//显
    document.getElementById("daytmdiv3").style.display = "none"
    document.getElementById("monthtmdiv3").style.display = "none"//隐
    document.getElementById("yeartmdiv3").style.display = "";//显
    //getindex();
    if ($("#panel-tab3").css("display") == "none") {
        $("#panel-tab3").css("display", "block")
    }
}

//第二页表格

// 方法说明：加载历史日报表列表
// 创 建 人：李沈杰
// 创建时间：2018/06/02
// 修 订 人：
// 修订时间：
// <参数名称>参数说明
// <returns>返回值说明
var pageSize1 = 15;
var pageNumber1 = 1;
loadTable()

function loadTable() {
    $("#bsTable").bootstrapTable('destroy').bootstrapTable({
        dataType: "json",
        pageNumber: pageNumber1,
        pageSize: pageSize1,
        pagination: true, //分页
        pageList: [10, 20, 30, 40, 50],
        singleSelect: false,
        method: "post", //请求方式
        cache: false,
        queryParamsType: "normal",
        sidePagination: "server", //服务端处理分页
        contentType: "application/x-www-form-urlencoded",
        exportDataType: 'all',
        showExport: false,  //是否显示导出按钮  
        buttonsAlign: "right",  //按钮位置  
        exportTypes: ['excel'],  //导出文件类型  
        Icons: 'glyphicon-export',
        columns: [
            {
                title: '序号', field: '', align: 'center', valign: 'middle', width: 50,
                formatter: function (value, row, index) {
                    return (pageNumber1 - 1) * pageSize1 + index + 1;
                }
            },
             {
                 field: "TimeStamp", title: "时间", align: 'center', valign: 'middle', width:220,
                 formatter: function (value, row, index) {
                     return value.substring(0, 10) + ' ' + value.substring(11, 19);
                 }
             },
             { field: "光伏输出直流电压", title: "光伏输出直流电压", align: 'center', valign: 'middle', width: 120 },
             { field: "光伏输出直流电流", title: "光伏输出直流电流", align: 'center', valign: 'middle', width: 120 },
             { field: "光伏输出直流功率", title: "光伏输出直流功率", align: 'center', valign: 'middle', width: 120 },
             { field: "光伏输出电能", title: "光伏输出电能", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机ab电压", title: "压缩机ab电压", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机bc电压", title: "压缩机bc电压", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机ca电压", title: "压缩机ca电压", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机a相电流", title: "压缩机a相电流", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机b相电流", title: "压缩机b相电流", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机c相电流", title: "压缩机c相电流", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机视在功率", title: "压缩机视在功率", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机有功功率", title: "压缩机有功功率", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机无功功率", title: "压缩机无功功率", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机功率因素", title: "压缩机功率因素", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机电能", title: "压缩机电能", align: 'center', valign: 'middle', width: 120 },
             { field: "a相畸变电压", title: "a相畸变电压", align: 'center', valign: 'middle', width: 120 },
             { field: "a相畸变电流", title: "a相畸变电流", align: 'center', valign: 'middle', width: 120 },
             { field: "a相电压不平衡度", title: "a相电压不平衡度", align: 'center', valign: 'middle', width: 120 },
             { field: "a相电流不平衡度", title: "a相电流不平衡度", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机排气压力", title: "压缩机排气压力", align: 'center', valign: 'middle', width: 120 },
             { field: "蒸发器出口压力", title: "蒸发器出口压力", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机排气温度", title: "压缩机排气温度", align: 'center', valign: 'middle', width: 120 },
             { field: "冷凝器出口温度", title: "冷凝器出口温度", align: 'center', valign: 'middle', width: 120 },
             { field: "蒸发器出口温度", title: "蒸发器出口温度", align: 'center', valign: 'middle', width: 120 },
             { field: "蒸发器入口温度", title: "蒸发器入口温度", align: 'center', valign: 'middle', width: 120 },
             { field: "水泵变频运行电压", title: "水泵变频运行电压", align: 'center', valign: 'middle', width: 120 },
             { field: "水泵变频运行电流", title: "水泵变频运行电流", align: 'center', valign: 'middle', width: 120 },
             { field: "水泵变频运行功率", title: "水泵变频运行功率", align: 'center', valign: 'middle', width: 120 },
             { field: "水泵变频运行频率", title: "水泵变频运行频率", align: 'center', valign: 'middle', width: 120 },
             { field: "水泵电能", title: "水泵电能", align: 'center', valign: 'middle', width: 120 },
             { field: "供冷冷水质量流量", title: "供冷冷水质量流量", align: 'center', valign: 'middle', width: 120 },
             { field: "冰块温度", title: "冰块温度", align: 'center', valign: 'middle', width: 120 },
             { field: "水箱上层水温", title: "水箱上层水温", align: 'center', valign: 'middle', width: 120 },
             { field: "供冷循环中回水温度", title: "供冷循环中回水温度", align: 'center', valign: 'middle', width: 120 },
             { field: "水箱下层水温", title: "水箱下层水温", align: 'center', valign: 'middle', width: 120 },
             { field: "供冷循环中供水温度", title: "供冷循环中供水温度", align: 'center', valign: 'middle', width: 120 },
             { field: "风机盘管出风口温度", title: "风机盘管出风口温度", align: 'center', valign: 'middle', width: 120 },
             { field: "房间温度", title: "房间温度", align: 'center', valign: 'middle', width: 120 },
             { field: "太阳o光伏", title: "太阳o光伏", align: 'center', valign: 'middle', width: 120 },
             { field: "供冷循环中供水温度", title: "供冷循环中供水温度", align: 'center', valign: 'middle', width: 120 },
             { field: "总电能", title: "总电能", align: 'center', valign: 'middle', width: 120 },
             { field: "压缩机频率", title: "压缩机频率", align: 'center', valign: 'middle', width: 120 }





        ],
        onPageChange: function (number, size) {
            pageNumber1 = number;
            pageSize1 = size;
            Query();
        },
        onLoadError: function (status, res) {
            hint('数据加载失败');
        },
        onClickRow: function (row, value) {
        }
    });

    Query();
}
//查询
function Query() {

    var options = $('#bsTable').bootstrapTable('getOptions');
    //请求数据
    $.post(commonUrl + "api/Query/GetDateBytime", {
        starttm: $("#startm").val(),
        endtm: $("#endtm").val(),
        pageNumber: pageNumber1,
        pageSize: pageSize1
    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            var rows =data.message.rows;
            msg.rows = rows;
            msg.total = msg.message.total;
            $("#bsTable").bootstrapTable("load", msg);
            // $("#bsTable").bootstrapTable('refresh', options);
        },
        "json");

}


//*******************************以下为柱状图页面所用************************************************///

function getindex() {
   
    if ($("#year").hasClass("active")) {
        var year = $("#yeartm").val().substring(0, 4);
        var tm=year+'-'+'01-01 00:00:00'
        if (year=="") {
            return;
        }
        var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
        //请求数据
        $.post(commonUrl + "api/Query/GetIndexByyear", {
            stcd: stcd,
            time: year,
            tm: tm
        },
                       function (data, status) {
                           if (0 != data.status) {
                               alert("未查询到相关数据！"); //提示框
                               return;
                           }

                           var msg = data;
                           var rows = data.message.rows;
                           msg.rows = rows;
                           msg.total = msg.message.total;
                           var datajson = [];
                           var arr1 = {}
                           arr1.label = msg.rows[0].TimeStamp.substring(0, 4);
                           arr1.color = "#9b59b6";
                           arr1.value = [];
                           arr1.value.push(parseFloat(msg.rows[0].Index_tag));
                           datajson[0] = arr1;
                           //系统统计数据-柱状图
                           var revenueChart = new FusionCharts({
                               type: 'column3d',
                               renderAt: 'barChart1',
                               width: '970',
                               height: '360',
                               dataFormat: 'json',
                               dataSource: {
                                   "chart": {
                                       "theme": "fint"
                                   },
                                   "data": datajson
                               }
                           }).render();


                       },
                       "json");
    } else if ($("#month").hasClass("active")) {

        var month = $("#monthtm").val().substring(0, 4);
        var tm = month + '-' + '01-01 00:00:00'
        if (month == "") {
            return;
        }
        var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
        //请求数据
        $.post(commonUrl + "api/Query/GetIndexBymonth", {
            stcd: stcd,
            time: month,
            tm: tm
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = 0; i < data.message.rows.length; i++) {
                    var arr1 = {}
                    arr1.label = data.message.rows[i].TimeStamp.substring(5,7) + "月";
                    arr1.color = randomColor1();
                    arr1.value = [];
                     arr1.value.push(parseFloat(msg.rows[i].Index_tag));
                    
                    datajson[i] = arr1;
                }
                
                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart1',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    } else if ($("#day").hasClass("active")) {

        var day = $("#daytm").val().substring(0, 7);
        var tm = day + '-' + '01-01 00:00:00'
        if (day == "") {
            return;
        }
        var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
        //var starttm = day +' '+'00:00:00';
        //var endtm = day +' '+'23:59:59';
        //请求数据
        $.post(commonUrl + "api/Query/Get_tb_DayIndex", {
            stcd: stcd,
            time: day,
            tm: tm
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = 0; i < msg.rows.length ; i++) {
                    var arr1 = {}
                    arr1.label = msg.rows[i].TimeStamp.substring(8,10) + "日";
                    arr1.color = randomColor1();
                    arr1.value = [];
                    arr1.value.push(parseFloat(msg.rows[i].Index_tag));

                    datajson[i] = arr1;
                }

                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart1',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    }
    else if ($("#hour").hasClass("active")) {

        var hour = $("#hourtm").val().substring(0, 10);
        var tm = hour + ' 00:00:00'
        if (hour == "") {
            return;
        }
        var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
        //var starttm = day +' '+'00:00:00';
        //var endtm = day +' '+'23:59:59';
        //请求数据
        $.post(commonUrl + "api/Query/Get_tb_HourIndex", {
            stcd: stcd,
            time: hour,
            tm: tm
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = 0; i < msg.rows.length ; i++) {
                    var arr1 = {}
                    arr1.label = msg.rows[i].TimeStamp.substring(11, 13) + "点";
                    arr1.color = randomColor1();
                    arr1.value = [];
                    arr1.value.push(parseFloat(msg.rows[i].Index_tag));

                    datajson[i] = arr1;
                }

                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart1',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    }
   //左下角第一个柱状图
    fuelWidget1();
}
//产生随机颜色代码
function randomColor1() {
    var r = Math.floor(Math.random() * 256);
    var g = Math.floor(Math.random() * 256);
    var b = Math.floor(Math.random() * 256);
    if (r < 16) {
        r = "0" + r.toString(16);
    } else {
        r = r.toString(16);
    }
    if (g < 16) {
        g = "0" + g.toString(16);
    } else {
        g = g.toString(16);
    }
    if (b < 16) {
        b = "0" + b.toString(16);
    } else {
        b = b.toString(16);
    }
    return "#" + r + g + b;
}
//系统统计数据-柱状图
//var revenueChart = new FusionCharts({
//    type: 'column3d',
//    renderAt: 'barChart1',
//    width: '970',
//    height: '360',
//    dataFormat: 'json',
//    dataSource: {
//        "chart": {
//            "theme": "fint"
//        },
//        "data": [
//            {
//                "label": "Jan",
//                "value": "420000",
//                "color": "#008ee4"
//            },
//            {
//                "label": "Feb",
//                "value": "810000",
//                "color": "#008ee4"
//            },
//            {
//                "label": "Mar",
//                "value": "720000",
//                "color": "#008ee4"
//            },
//            {
//                "label": "Apr",
//                "value": "550000",
//                "color": "#9b59b6"
//            },
//            {
//                "label": "May",
//                "value": "910000",
//                "color": "#9b59b6"
//            },
//            {
//                "label": "Jun",
//                "value": "510000",
//                "color": "#9b59b6"
//            },
//            {
//                "label": "Jul",
//                "value": "680000",
//                "color": "#6baa01"
//            },
//            {
//                "label": "Aug",
//                "value": "620000",
//                "color": "#6baa01"
//            },
//            {
//                "label": "Sep",
//                "value": "610000",
//                "color": "#6baa01"
//            },
//            {
//                "label": "Oct",
//                "value": "490000",
//                "color": "#e44a00"
//            },
//            {
//                "label": "Nov",
//                "value": "900000",
//                "color": "#e44a00"
//            },
//            {
//                "label": "Dec",
//                "value": "730000",
//                "color": "#e44a00"
//            }
//        ]
//    }
//}).render();


var nowIndex_tag//现在的某项指标总累计量
var thisyearIndex_tag//现在的某项指标今年总累计量
//左下角第一个柱状图
function fuelWidget1() {
    var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
    //请求数据
    $.post(commonUrl + "api/Query/GetsomeoneLastNewIndex", {
        stcd:stcd

    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            //var rows = data.message.rows;
            nowIndex_tag = data.message.rows[0].Index_tag;
           
            $("#Index_tag1").html(nowIndex_tag);
            $("#Index_tag1").css("font-size", 14);
            if (stcd == "AllQs"||stcd == "AllQc"||stcd == "AllQhp"||stcd == "AllQsh"||stcd == "AllQuse"||stcd == "AllQu") {
                $("#unit1").html("兆焦");
            } else if (stcd == "AllEc" || stcd == "AllEhp" || stcd == "AllEsys") {
                $("#unit1").html("度");
            } else if (stcd == "Allc" || stcd == "AllMv") {
                $("#unit1").html("吨");
            } else {
                $("#unit1").html("千克");
            }
           

            //msg.rows = rows;
            //msg.total = msg.message.total;
            //下排第1个
            var fuelWidget1 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'mychart1',
                width: '80',
                height: '165',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#00F5FF"
                    },
                    "value": nowIndex_tag
                }
            }).render();

            fuelWidget2();
            fuelWidget3();
            fuelWidget4();
            Get_lastyearsomedataIndex();
        },
        "json");

}

//下排第2个
function fuelWidget2() {
    var myDate = new Date();

    //获取当前年
    var year = myDate.getFullYear();
    var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
    var tm=year+ '-01-01 00:00:00';
    //请求数据
    $.post(commonUrl + "api/Query/GetsomeoneLastyearlastdataIndex", {
        stcd: stcd,
        tm: tm

    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            //var rows = data.message.rows;
            var lastIndex_tag = data.message.rows[0].Index_tag;
            var Index_tag = (nowIndex_tag - lastIndex_tag).toFixed(2);
            thisyearIndex_tag = Index_tag;
            $("#Index_tag2").css("font-size", 14);
            $("#Index_tag2").html(Index_tag);
            if (stcd == "AllQs" || stcd == "AllQc" || stcd == "AllQhp" || stcd == "AllQsh" || stcd == "AllQuse" || stcd == "AllQu") {
                $("#unit2").html("兆焦");
            } else if (stcd == "AllEc" || stcd == "AllEhp" || stcd == "AllEsys") {
                $("#unit2").html("度");
            } else if (stcd == "Allc" || stcd == "AllMv") {
                $("#unit2").html("吨");
            } else {
                $("#unit2").html("千克");
            }


            //msg.rows = rows;
            //msg.total = msg.message.total;
            //下排第1个
            var fuelWidget2 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'mychart2',
                width: '80',
                height: '165',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#00FF00"
                    },
                    "value": Index_tag
                }
            }).render();
        },
        "json");

}

//下排第3个
function fuelWidget3() {
    var myDate = new Date();

    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
    var tm = year + '-' + month + '-' + '01 00:00:00';
    //请求数据
    $.post(commonUrl + "api/Query/GetsomeoneLastmonthlastdataIndex", {
        stcd: stcd,
        tm: tm

    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            //var rows = data.message.rows;
            var lastIndex_tag = data.message.rows[0].Index_tag;
            var Index_tag = (nowIndex_tag - lastIndex_tag).toFixed(2);
            Get_lastmonthsummaryindex(Index_tag)//与上月做对比
            $("#Index_tag3").html(Index_tag);
            $("#Index_tag3").css("font-size", 14);
            if (stcd == "AllQs" || stcd == "AllQc" || stcd == "AllQhp" || stcd == "AllQsh" || stcd == "AllQuse" || stcd == "AllQu") {
                $("#unit3").html("兆焦");
            } else if (stcd == "AllEc" || stcd == "AllEhp" || stcd == "AllEsys") {
                $("#unit3").html("度");
            } else if (stcd == "Allc" || stcd == "AllMv") {
                $("#unit3").html("吨");
            } else {
                $("#unit3").html("千克");
            }


            //msg.rows = rows;
            //msg.total = msg.message.total;
            var fuelWidget3 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'mychart3',
                width: '80',
                height: '165',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#8B0A50"
                    },
                    "value": Index_tag
                }
            }).render();
        },
        "json");

}

//下排第4个
function fuelWidget4() {
    var myDate = new Date();

    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
    var tm = year + '-' + month + '-' + date+' 00:00:00';
    //请求数据
    $.post(commonUrl + "api/Query/GetsomeoneLastDaylastIndex", {
        stcd: stcd,
        tm: tm

    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            //var rows = data.message.rows;
            var lastIndex_tag = data.message.rows[0].Index_tag;
            var Index_tag = (nowIndex_tag - lastIndex_tag).toFixed(2);
            Get_lastdayhsummaryindex(Index_tag);
            $("#Index_tag4").html(Index_tag);
            $("#Index_tag4").css("font-size", 14);
            if (stcd == "AllQs" || stcd == "AllQc" || stcd == "AllQhp" || stcd == "AllQsh" || stcd == "AllQuse" || stcd == "AllQu") {
                $("#unit4").html("兆焦");
            } else if (stcd == "AllEc" || stcd == "AllEhp" || stcd == "AllEsys") {
                $("#unit4").html("度");
            } else if (stcd == "Allc" || stcd == "AllMv") {
                $("#unit4").html("吨");
            } else {
                $("#unit4").html("千克");
            }


            //msg.rows = rows;
            //msg.total = msg.message.total;
            var fuelWidget4 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'mychart4',
                width: '80',
                height: '165',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#EE30A7"
                    },
                    "value": Index_tag
                }
            }).render();
        },
        "json");

}

//与去年同期相比
function Get_lastyearsomedataIndex() {
    var myDate = new Date();

    //获取去年年
    var year = myDate.getFullYear()-1;
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
    var endtm = year + '-' + month + '-' + date + ' 00:00:00';
    var starttm = year + '-01-01 00:00:00';
    //请求数据
    $.post(commonUrl + "api/Query/Get_lastyearsomedataIndex", {
        stcd: stcd,
        starttm: starttm,
        endtm: endtm

    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            //var rows = data.message.rows;
            var lastIndex_tag = data.message.rows[0].beforIndex_tag;
            var Index_tag = (thisyearIndex_tag - lastIndex_tag).toFixed(2);
            if (lastIndex_tag == 0) {
                $("#lastyearsamedate").html("缺少数据");
                
            } else {
                var Compared = (Index_tag *100/ lastIndex_tag).toFixed(2);
                if (Compared > 0) {
                    $("#lastyearsamedate").html(Compared.toString()+'%');
                    $("#arrow1").removeClass("arrow-down").addClass("arrow-up");
                } else {
                    $("#lastyearsamedate").html(Compared.toString() + '%');
                    $("#arrow1").removeClass("arrow-up").addClass("arrow-down");
                }
            }
          


        },
        "json");

}
//与上月相比
function Get_lastmonthsummaryindex(thismonthIndex_tag) {

    var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
    //请求数据
    $.post(commonUrl + "api/Query/Get_lastmonthsummaryindex", {
        stcd: stcd

    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            //var rows = data.message.rows;
            var lastIndex_tag = data.message.rows[0].Index_tag;
            var Index_tag = (thismonthIndex_tag - lastIndex_tag).toFixed(2);
            if (lastIndex_tag == 0) {
                $("#lastmonthsamedate").html("缺少数据");

            } else {
                var Compared = (Index_tag * 100 / lastIndex_tag).toFixed(2);
                if (Compared > 0) {
                    $("#lastmonthsamedate").html(Compared.toString() + '%');
                    $("#arrow2").removeClass("arrow-down").addClass("arrow-up");
                } else {
                    $("#lastmonthsamedate").html(Compared.toString() + '%');
                    $("#arrow2").removeClass("arrow-up").addClass("arrow-down");
                }
            }



        },
        "json");

}

//与昨天相比
function Get_lastdayhsummaryindex(thisdayIndex_tag) {

    var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
    //请求数据
    $.post(commonUrl + "api/Query/Get_lastdayhsummaryindex", {
        stcd: stcd

    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            //var rows = data.message.rows;
            var lastIndex_tag = data.message.rows[0].Index_tag;
            var Index_tag = (thisdayIndex_tag - lastIndex_tag).toFixed(2);
            if (lastIndex_tag == 0) {
                $("#lastmonthsamedate").html("缺少数据");

            } else {
                var Compared = (Index_tag * 100 / lastIndex_tag).toFixed(2);
                if (Compared > 0) {
                    $("#lastdaysamedate").html(Compared.toString() + '%');
                    $("#arrow3").removeClass("arrow-down").addClass("arrow-up");
                } else {
                    $("#lastdaysamedate").html(Compared.toString() + '%');
                    $("#arrow3").removeClass("arrow-up").addClass("arrow-down");
                }
            }



        },
        "json");

}
//系统性能指标-柱状图
//var revenueChart = new FusionCharts({
//    type: 'column3d',
//    renderAt: 'barChart3',
//    width: '970',
//    height: '520',
//    dataFormat: 'json',
//    dataSource: {
//        "chart": {
//            "theme": "fint"
//        },
//        "data": [
//            {
//                "label": "Jan",
//                "value": "420000",
//                "color": "#008ee4"
//            },
//            {
//                "label": "Feb",
//                "value": "810000",
//                "color": "#008ee4"
//            },
//            {
//                "label": "Mar",
//                "value": "720000",
//                "color": "#008ee4"
//            },
//            {
//                "label": "Apr",
//                "value": "550000",
//                "color": "#9b59b6"
//            },
//            {
//                "label": "May",
//                "value": "910000",
//                "color": "#9b59b6"
//            },
//            {
//                "label": "Jun",
//                "value": "510000",
//                "color": "#9b59b6"
//            },
//            {
//                "label": "Jul",
//                "value": "680000",
//                "color": "#6baa01"
//            },
//            {
//                "label": "Aug",
//                "value": "620000",
//                "color": "#6baa01"
//            },
//            {
//                "label": "Sep",
//                "value": "610000",
//                "color": "#6baa01"
//            },
//            {
//                "label": "Oct",
//                "value": "490000",
//                "color": "#e44a00"
//            },
//            {
//                "label": "Nov",
//                "value": "900000",
//                "color": "#e44a00"
//            },
//            {
//                "label": "Dec",
//                "value": "730000",
//                "color": "#e44a00"
//            }
//        ]
//    }
//}).render();

//性能指标柱状图
function getindex2() {

    if ($("#year").hasClass("active")) {
        var year = $("#yeartm2").val().substring(0, 4);
        var tm = year + '-' + '01-01 00:00:00'
        if (year == "") {
            return;
        }
        var stcd = $('#chooseBox3 [type=radio][name=radio2]:checked').val()
        //请求数据
        $.post(commonUrl + "api/Query/Get_yearavgIndesx", {
            stcd: stcd,
            tm: year
        },
                       function (data, status) {
                           if (0 != data.status) {
                               alert("未查询到相关数据！"); //提示框
                               return;
                           }

                           var msg = data;
                           var rows = data.message.rows;
                           msg.rows = rows;
                           msg.total = msg.message.total;
                           var datajson = [];
                           var arr1 = {}
                           arr1.label = year;
                           arr1.color = "#9b59b6";
                           arr1.value = [];
                           arr1.value.push(parseFloat(msg.rows[0].Index_tag).toFixed(2));
                           datajson[0] = arr1;
                           //系统统计数据-柱状图
                           var revenueChart = new FusionCharts({
                               type: 'column3d',
                               renderAt: 'barChart2',
                               width: '970',
                               height: '360',
                               dataFormat: 'json',
                               dataSource: {
                                   "chart": {
                                       "theme": "fint"
                                   },
                                   "data": datajson
                               }
                           }).render();


                       },
                       "json");
    } else if ($("#month").hasClass("active")) {

        var month = $("#monthtm2").val().substring(0, 4);
        if (month == "") {
            return;
        }
        var stcd = $('#chooseBox3 [type=radio][name=radio2]:checked').val()
        //请求数据
        $.post(commonUrl + "api/Query/Get_monthavgIndesx", {
            stcd: stcd,
            tm: month
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = 0; i < data.message.rows.length; i++) {
                    var arr1 = {}
                    arr1.label = data.message.rows[i].month + "月";
                    arr1.color = randomColor1();
                    arr1.value = [];
                    arr1.value.push(parseFloat(msg.rows[i].Index_tag).toFixed(2));

                    datajson[i] = arr1;
                }

                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart2',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    } else if ($("#day").hasClass("active")) {

        var year = $("#daytm2").val().substring(0, 4);
        var month = $("#daytm2").val().substring(5, 7);
        if (day == "") {
            return;
        }
        var stcd = $('#chooseBox3 [type=radio][name=radio2]:checked').val()
        //var starttm = day +' '+'00:00:00';
        //var endtm = day +' '+'23:59:59';
        //请求数据
        $.post(commonUrl + "api/Query/Get_dayavgIndesx", {
            stcd: stcd,
            month: month,
            tm: year
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = msg.rows.length; i >0 ; i--) {
                    var arr1 = {}
                    arr1.label = msg.rows[i-1].day + "日";
                    arr1.color = randomColor1();
                    arr1.value = [];
                    arr1.value.push(parseFloat(msg.rows[i-1].Index_tag).toFixed(2));

                    datajson[msg.rows.length-i] = arr1;
                }

                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart2',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    }
    else if ($("#hour").hasClass("active")) {

        var year = $("#hourtm2").val().substring(0, 4);
        var month = $("#hourtm2").val().substring(5, 7);
        var day = $("#hourtm2").val().substring(8, 10);
        if (hour == "") {
            return;
        }
        var stcd = $('#chooseBox3 [type=radio][name=radio2]:checked').val()
        //var starttm = day +' '+'00:00:00';
        //var endtm = day +' '+'23:59:59';
        //请求数据
        $.post(commonUrl + "api/Query/Get_hourfavgIndesx", {
            stcd: stcd,
            month: month,
            day:day,
            tm: year
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = msg.rows.length; i > 0 ; i--) {
                    var arr1 = {}
                    arr1.label = msg.rows[i - 1].hour + "点";
                    arr1.color = randomColor1();
                    arr1.value = [];
                    arr1.value.push(parseFloat(msg.rows[i - 1].Index_tag).toFixed(2));

                    datajson[msg.rows.length - i] = arr1;
                }

                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart2',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    }
}



function getindex3() {

    if ($("#year").hasClass("active")) {
        var year = $("#yeartm3").val().substring(0, 4);
        var tm = year+'-12-31 23:59:59'
        if (year == "") {
            return;
        }
        //请求数据
        $.post(commonUrl + "api/Query/Get_tb_YearcompareIndex", {
            stcd: stcd,
            tm: tm
        },
                       function (data, status) {
                           if (0 != data.status) {
                               alert("未查询到相关数据！"); //提示框
                               return;
                           }

                           var msg = data;
                           var rows = data.message.rows;
                           msg.rows = rows;
                           msg.total = msg.message.total;
                           var datajson = [];
                           //var arr1 = {}
                           //arr1.label = msg.rows[0].TimeStamp.substring(0, 4);
                           //arr1.color = "#9b59b6";
                           //arr1.value = [];
                           //arr1.value.push(parseFloat(msg.rows[0].Index_tag));
                           //datajson[0] = arr1;

                           for (var i = 0; i < 11; i++) {
                               if(i==0){
                                   var arr1 = {}
                                   arr1.label = "累计太阳辐射能";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllQs));

                                   datajson[i] = arr1;
                               }else if (i==1){
                                   var arr1 = {}
                                   arr1.label = "集热系统累计得热量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllQc));

                                   datajson[i] = arr1;
                               } else if (i == 2) {
                                   var arr1 = {}
                                   arr1.label = "热泵系统累计得热量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllQhp));

                                   datajson[i] = arr1;
                               } else if (i == 3) {
                                   var arr1 = {}
                                   arr1.label = "累计系统供热量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllQsh));

                                   datajson[i] = arr1;
                               } else if (i == 4) {
                                   var arr1 = {}
                                   arr1.label = "累计用户实际供热量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllQuse));

                                   datajson[i] = arr1;
                               } else if (i == 5) {
                                   var arr1 = {}
                                   arr1.label = "用户名义累计供热量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllQu));

                                   datajson[i] = arr1;
                               } else if (i == 6) {
                                   var arr1 = {}
                                   arr1.label = "集热循环水泵耗电量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllEc));

                                   datajson[i] = arr1;
                               } else if (i == 7) {
                                   var arr1 = {}
                                   arr1.label = "热泵累计耗电量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllEhp));

                                   datajson[i] = arr1;
                               } else if (i == 8) {
                                   var arr1 = {}
                                   arr1.label = "系统总累计耗电量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllEsys));

                                   datajson[i] = arr1;
                               } else if (i == 9) {
                                   var arr1 = {}
                                   arr1.label = "冷水累计耗水量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].Allc));

                                   datajson[i] = arr1;
                               } else if (i == 10) {
                                   var arr1 = {}
                                   arr1.label = "用户累计耗水量";
                                   arr1.color = randomColor1();
                                   arr1.value = [];
                                   arr1.value.push(parseFloat(msg.rows[0].AllMv));

                                   datajson[i] = arr1;
                               }
                              
                              
                              
                             
                              
                           }

                           //系统统计数据-柱状图
                           var revenueChart = new FusionCharts({
                               type: 'column3d',
                               renderAt: 'barChart3',
                               width: '970',
                               height: '360',
                               dataFormat: 'json',
                               dataSource: {
                                   "chart": {
                                       "theme": "fint"
                                   },
                                   "data": datajson
                               }
                           }).render();


                       },
                       "json");
    } else if ($("#month").hasClass("active")) {

        var month = $("#monthtm3").val().substring(0, 7);
        if (month == "") {
            return;
        }
        var tm
        if ($("#monthtm3").val().substring(5, 7)=="01"||$("#monthtm3").val().substring(5, 7)=="03"||
            $("#monthtm3").val().substring(5, 7)=="05"||$("#monthtm3").val().substring(5, 7)=="05"||$("#monthtm3").val().substring(5, 7)=="07"||
            $("#monthtm3").val().substring(5, 7)=="08"||$("#monthtm3").val().substring(5, 7)=="10"||
            $("#monthtm3").val().substring(5, 7)=="12") {
            tm = month + '-' + '31 23:59:59'
        } else if ($("#monthtm3").val().substring(5, 7) == "02") {
            tm = month + '-' + '28 23:59:59'
          
        
        } else {

            tm = month + '-' + '30 23:59:59'
        }

        //请求数据
        $.post(commonUrl + "api/Query/Get_tb_monthcompareIndex", {
            stcd: stcd,
            tm: tm
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = 0; i < 11; i++) {
                    if (i == 0) {
                        var arr1 = {}
                        arr1.label = "累计太阳辐射能";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQs));

                        datajson[i] = arr1;
                    } else if (i == 1) {
                        var arr1 = {}
                        arr1.label = "集热系统累计得热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQc));

                        datajson[i] = arr1;
                    } else if (i == 2) {
                        var arr1 = {}
                        arr1.label = "热泵系统累计得热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQhp));

                        datajson[i] = arr1;
                    } else if (i == 3) {
                        var arr1 = {}
                        arr1.label = "累计系统供热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQsh));

                        datajson[i] = arr1;
                    } else if (i == 4) {
                        var arr1 = {}
                        arr1.label = "累计用户实际供热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQuse));

                        datajson[i] = arr1;
                    } else if (i == 5) {
                        var arr1 = {}
                        arr1.label = "用户名义累计供热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQu));

                        datajson[i] = arr1;
                    } else if (i == 6) {
                        var arr1 = {}
                        arr1.label = "集热循环水泵耗电量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllEc));

                        datajson[i] = arr1;
                    } else if (i == 7) {
                        var arr1 = {}
                        arr1.label = "热泵累计耗电量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllEhp));

                        datajson[i] = arr1;
                    } else if (i == 8) {
                        var arr1 = {}
                        arr1.label = "系统总累计耗电量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllEsys));

                        datajson[i] = arr1;
                    } else if (i == 9) {
                        var arr1 = {}
                        arr1.label = "冷水累计耗水量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].Allc));

                        datajson[i] = arr1;
                    } else if (i == 10) {
                        var arr1 = {}
                        arr1.label = "用户累计耗水量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllMv));

                        datajson[i] = arr1;
                    }

                }

                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart3',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    } else if ($("#day").hasClass("active")) {

        var day = $("#daytm3").val().substring(0, 10);
        var tm = day + ' 23:59:59';
        //if ($("#daytm3").val().substring(5, 7) == "01" || $("#daytm3").val().substring(5, 7) == "03" ||
        //    $("#daytm3").val().substring(5, 7) == "05" || $("#daytm3").val().substring(5, 7) == "05" || $("#daytm3").val().substring(5, 7) == "07" ||
        //    $("#daytm3").val().substring(5, 7) == "08" || $("#daytm3").val().substring(5, 7) == "10" ||
        //    $("#daytm3").val().substring(5, 7) == "12") {
        //    tm = day + '-' + '23:59:59'
        //} else if ($("#daytm3").val().substring(5, 7) == "02") {
        //    tm = day + '-' + ' 23:59:59'


        //} else {

        //    tm = day + '-' + '30 23:59:59'
        //}
        if (day == "") {
            return;
        }
        //var starttm = day +' '+'00:00:00';
        //var endtm = day +' '+'23:59:59';
        //请求数据
        $.post(commonUrl + "api/Query/Get_tb_daycompareIndex", {
            tm: tm
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = 0; i < 11; i++) {
                    if (i == 0) {
                        var arr1 = {}
                        arr1.label = "累计太阳辐射能";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQs));

                        datajson[i] = arr1;
                    } else if (i == 1) {
                        var arr1 = {}
                        arr1.label = "集热系统累计得热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQc));

                        datajson[i] = arr1;
                    } else if (i == 2) {
                        var arr1 = {}
                        arr1.label = "热泵系统累计得热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQhp));

                        datajson[i] = arr1;
                    } else if (i == 3) {
                        var arr1 = {}
                        arr1.label = "累计系统供热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQsh));

                        datajson[i] = arr1;
                    } else if (i == 4) {
                        var arr1 = {}
                        arr1.label = "累计用户实际供热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQuse));

                        datajson[i] = arr1;
                    } else if (i == 5) {
                        var arr1 = {}
                        arr1.label = "用户名义累计供热量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllQu));

                        datajson[i] = arr1;
                    } else if (i == 6) {
                        var arr1 = {}
                        arr1.label = "集热循环水泵耗电量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllEc));

                        datajson[i] = arr1;
                    } else if (i == 7) {
                        var arr1 = {}
                        arr1.label = "热泵累计耗电量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllEhp));

                        datajson[i] = arr1;
                    } else if (i == 8) {
                        var arr1 = {}
                        arr1.label = "系统总累计耗电量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllEsys));

                        datajson[i] = arr1;
                    } else if (i == 9) {
                        var arr1 = {}
                        arr1.label = "冷水累计耗水量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].Allc));

                        datajson[i] = arr1;
                    } else if (i == 10) {
                        var arr1 = {}
                        arr1.label = "用户累计耗水量";
                        arr1.color = randomColor1();
                        arr1.value = [];
                        arr1.value.push(parseFloat(msg.rows[0].AllMv));

                        datajson[i] = arr1;
                    }

                }


                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart3',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    }
    else if ($("#hour").hasClass("active")) {

        var hour = $("#hourtm3").val().substring(0, 10);
        var tm = hour + ' 00:00:00'
        if (hour == "") {
            return;
        }
        var stcd = $('#chooseBox2 [type=radio][name=radio1]:checked').val()
        //var starttm = day +' '+'00:00:00';
        //var endtm = day +' '+'23:59:59';
        //请求数据
        $.post(commonUrl + "api/Query/Get_tb_HourIndex", {
            stcd: stcd,
            time: hour,
            tm: tm
        },
            function (data, status) {
                if (0 != data.status) {
                    alert("未查询到相关数据！"); //提示框
                    return;
                }

                var msg = data;
                var rows = data.message.rows;
                msg.rows = rows;
                msg.total = msg.message.total;
                var datajson = [];
                for (var i = 0; i < msg.rows.length ; i++) {
                    var arr1 = {}
                    arr1.label = msg.rows[i].TimeStamp.substring(11, 13) + "点";
                    arr1.color = randomColor1();
                    arr1.value = [];
                    arr1.value.push(parseFloat(msg.rows[i].Index_tag));

                    datajson[i] = arr1;
                }

                //系统统计数据-柱状图
                var revenueChart = new FusionCharts({
                    type: 'column3d',
                    renderAt: 'barChart3',
                    width: '970',
                    height: '360',
                    dataFormat: 'json',
                    dataSource: {
                        "chart": {
                            "theme": "fint"
                        },
                        "data": datajson
                    }
                }).render();


            },
            "json");


    }
    //左下角第一个柱状图
    fuelWidget1();
}