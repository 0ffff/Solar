$('#StartTime, #EndTime').datetimepicker({
    language: 'zh-CN',
    autoclose: true,
    startView: 4,
    maxView: 'decade',
    minView: 'month',
    viewSelect: 'month',
    todayBtn: 'true',
    pickerPosition: 'bottom-right'
});

setDay();
loadTable();
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
    if (month< 10)
    {
        month = "0" + month;
    }
    if (bfmonth < 10)
    {
        bfmonth = "0" + bfmonth;
    }
    if (day < 10)
    {
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


// 方法说明：加载历史日报表列表
// 创 建 人：李沈杰
// 创建时间：2018/06/02
// 修 订 人：
// 修订时间：
// <参数名称>参数说明
// <returns>返回值说明
var pageSize1 = 30;
var pageNumber1 = 1;
function loadTable() {
    $("#bsTable").bootstrapTable('destroy').bootstrapTable({
        dataType: "json",
        pageNumber: pageNumber1,
        pageSize: pageSize1,
        pagination: true, //分页
        pageList: [15, 20, 25, 30, 50],
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
                 field: "Timestamp", title: "时间", align: 'center', valign: 'middle', width: 120,
                 formatter: function (value, row, index) {
                     return value.toString().substring(0,10)+' '+value.toString().substring(11,19);
                 }
             },
             { field: "AreaName", title: "行政区划名称", align: 'center', valign: 'middle', width: 120},
             { field: "ProjectName", title: "项目名称", align: 'center', valign: 'middle', width: 120 },
             { field: "CollectorName", title: "采集器名称", align: 'center', valign: 'middle', width: 120 },
             { field: "Info", title: "详情", align: 'center', valign: 'middle', width: 200 },
             { field: "State", title: "状态", align: 'center', valign: 'middle', width: 50 }
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
    $.post(commonUrl + "api/Alarm/Get_tb_Alarm", {
        ProjectCode: "001",
        startm: $("#startm").val(),
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
            var rows = eval('(' + data.message.rows + ')');
            msg.rows = rows;
            msg.total = msg.message.total;
           $("#bsTable").bootstrapTable("load", msg);
           // $("#bsTable").bootstrapTable('refresh', options);
        },
        "json");
   
}
loadTable();
