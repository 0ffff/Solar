///各种减排量
var AllQuse;
var AllQss;
var Allmco2;
var Allmso2;
var Allmnox;
var Allmfc;
//时间量
var startm //今日开始时间
var endtm //今日现在时间
gettime();
function gettime() {
    var myDate = new Date();

    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var bfday = date - 1;
    //获取当前小时数(0-23)
    var h = myDate.getHours();
    //获取当前分钟数(0-59)
    var m = myDate.getMinutes();
    if (m < 10) {
        m = '0' + m;
    }
    var bfday = date - 1;
    endtm = year + '-' + month + '-' + date + ' ' + h + ':' + m;
    startm = year + '-' + month + '-' + date + ' ' + "00" + ':' + "00";
}
$(document).ready(function () {
    $('#select1, #select2, #select3, #select4').select2();
})

FusionCharts.ready(function () {
    var bigThermometer = new FusionCharts({
        type: 'thermometer',
        renderAt: 'thermometer1',
        width: '100',
        height: '218',
        dataFormat: 'json',
        containerBackgroundOpacity: "0",
        dataSource: {
            "chart": {
                "lowerLimit": "-5",
                "upperLimit": "45",
                "decimals": "1",
                "numberSuffix": "°C",
                "showhovereffect": "1",
                "showToolTip": "0",
                "thmFillColor": "#008ee4",
                "showGaugeBorder": "1",
                "gaugeBorderColor": "#008ee4",
                "gaugeBorderThickness": "2",
                "gaugeBorderAlpha": "30",
                "thmOriginX": "30",
                "chartBottomMargin": "0",
                "valueFontSize": "0",
                "theme": "fint",
                "borderAlpha": "0",
                "bgAlpha": '0',
                "canvasBgAlpha": "0",

                //"hoverCapBgColor": "#ff0000"
            },
            "value": temperature_curr
        }
    })
     .render();
    //风速计
    var windSpeed = new FusionCharts({
        type: 'angulargauge',
        renderAt: 'windSpeed',
        width: '280',
        height: '140',
        dataFormat: 'json',
        containerBackgroundOpacity: "0",
        dataSource: {
            "chart": {
                "lowerLimit": "0",
                "upperLimit": "12",
                "showValue": "0",
                "valueBelowPivot": "1",
                "tickValueDecimals": "1",
                "forceTickValueDecimals": "1",
                "chartBottomMargin": "0",
                "chartLeftMargin": "0",
                "showToolTip": "0",
                "gaugeFillMix": "{dark-40},{light-40},{dark-20}",
                "theme": "fint",
                "minorTMNumber": "2",
                "gaugeInnerRadius": "50%",
                "borderAlpha": "0",
                "bgAlpha": '0',
                "canvasBgAlpha": "0",
            },
            "colorRange": {
                "color": [
                    {
                        "minValue": "0",
                        "maxValue": "6",
                        "code": "#6baa01"
                    },
                    {
                        "minValue": "6",
                        "maxValue": "9",
                        "code": "#f8bd19"
                    },
                    {
                        "minValue": "10",
                        "maxValue": "12",
                        "code": "#e44a00"
                    }
                ]
            },
            "dials": {
                "dial": [{
                    "value": "6"
                }]
            }
        }
    }).render();
    //左侧高温温度计
    var thmHigh = new FusionCharts({
        type: 'thermometer',
        renderAt: 'thmHigh',
        width: '40',
        height: '100',
        dataFormat: 'json',
        containerBackgroundOpacity: "0",
        dataSource: {
            "chart": {
                "lowerLimit": "-5",
                "upperLimit": "45",
                "decimals": "1",
                "numberSuffix": "°C",
                "showhovereffect": "1",
                "showToolTip": "0",
                "thmFillColor": "#e44a00",
                "showGaugeBorder": "1",
                "gaugeBorderColor": "#e44a00",
                "gaugeBorderThickness": "1",
                "gaugeBorderAlpha": "30",
                "thmOriginX": "30",
                "chartBottomMargin": "0",
                "valueFontSize": "0",
                "theme": "fint",
                "showTickMarks": "0",
                "showTickValues": "0",
                "borderAlpha": "0",
                "bgAlpha": '0',
                "canvasBgAlpha": "0",

                //"hoverCapBgColor": "#ff0000"
            },
            "value": "0"
        }
    })
    .render();
    //左侧低温温度计
    var thmLow = new FusionCharts({
        type: 'thermometer',
        renderAt: 'thmLow',
        width: '40',
        height: '100',
        dataFormat: 'json',
        containerBackgroundOpacity: "0",
        dataSource: {
            "chart": {
                "lowerLimit": "-5",
                "upperLimit": "45",
                "decimals": "1",
                "numberSuffix": "°C",
                "showhovereffect": "1",
                "showToolTip": "0",
                "thmFillColor": "#008ee4",
                "showGaugeBorder": "1",
                "gaugeBorderColor": "#008ee4",
                "gaugeBorderThickness": "1",
                "gaugeBorderAlpha": "30",
                "thmOriginX": "30",
                "chartBottomMargin": "0",
                "valueFontSize": "0",
                "theme": "fint",
                "showTickMarks": "0",
                "showTickValues": "0",
                "borderAlpha": "0",
                "bgAlpha": '0',
                "canvasBgAlpha": "0",
            },
            "value": "0"
        }
    })
    .render();
    //左侧大温度计
    $.ajax({
        url: "http://api.k780.com:88/?app=weather.today&weaid=101290101&appkey=10003&sign=b59bc3ef6191eb9f747dd4e83c99f2a4&format=json&jsoncallback=?",
        dataType: "jsonp",
        type: "Post",
        jsonpCallback: "jsonpCallback",
        success: function (data) {
            var msg = data;
            var winp;//风速
            if (msg.result.winp.length==2) {
                winp = msg.result.winp.substring(0,1);//风速
            } else {
                winp = msg.result.winp.substring(0, 2);//风速
            }

            var temperature_curr = msg.result.temperature_curr;//当前温度
            var temp_high = msg.result.temp_high;//最高温度
            var temp_low = msg.result.temp_low;//最低温度
            $("#temperature_curr").html(temperature_curr);
            $("#thmHighNum").html("H:" + temp_high + "℃");
            $("#thmLowNum").html("L:" + temp_low + "℃");
            var bigThermometer = new FusionCharts({
                type: 'thermometer',
                renderAt: 'thermometer1',
                width: '100',
                height: '218',
                dataFormat: 'json',
                containerBackgroundOpacity: "0",
                dataSource: {
                    "chart": {
                        "lowerLimit": "-5",
                        "upperLimit": "45",
                        "decimals": "1",
                        "numberSuffix": "°C",
                        "showhovereffect": "1",
                        "showToolTip": "0",
                        "thmFillColor": "#008ee4",
                        "showGaugeBorder": "1",
                        "gaugeBorderColor": "#008ee4",
                        "gaugeBorderThickness": "2",
                        "gaugeBorderAlpha": "30",
                        "thmOriginX": "30",
                        "chartBottomMargin": "0",
                        "valueFontSize": "0",
                        "theme": "fint",
                        "borderAlpha": "0",
                        "bgAlpha": '0',
                        "canvasBgAlpha": "0",

                        //"hoverCapBgColor": "#ff0000"
                    },
                    "value": temperature_curr
                }
            })
  .render();
            //风速计
            var windSpeed = new FusionCharts({
                type: 'angulargauge',
                renderAt: 'windSpeed',
                width: '280',
                height: '140',
                dataFormat: 'json',
                containerBackgroundOpacity: "0",
                dataSource: {
                    "chart": {
                        "lowerLimit": "0",
                        "upperLimit": "12",
                        "showValue": "0",
                        "valueBelowPivot": "1",
                        "tickValueDecimals": "1",
                        "forceTickValueDecimals": "1",
                        "chartBottomMargin": "0",
                        "chartLeftMargin": "0",
                        "showToolTip": "0",
                        "gaugeFillMix": "{dark-40},{light-40},{dark-20}",
                        "theme": "fint",
                        "minorTMNumber": "2",
                        "gaugeInnerRadius": "50%",
                        "borderAlpha": "0",
                        "bgAlpha": '0',
                        "canvasBgAlpha": "0",
                    },
                    "colorRange": {
                        "color": [
                            {
                                "minValue": "0",
                                "maxValue": "6",
                                "code": "#6baa01"
                            },
                            {
                                "minValue": "6",
                                "maxValue": "9",
                                "code": "#f8bd19"
                            },
                            {
                                "minValue": "10",
                                "maxValue": "12",
                                "code": "#e44a00"
                            }
                        ]
                    },
                    "dials": {
                        "dial": [{
                            "value": winp
                        }]
                    }
                }
            }).render();
            //左侧高温温度计
            var thmHigh = new FusionCharts({
                type: 'thermometer',
                renderAt: 'thmHigh',
                width: '40',
                height: '100',
                dataFormat: 'json',
                containerBackgroundOpacity: "0",
                dataSource: {
                    "chart": {
                        "lowerLimit": "-5",
                        "upperLimit": "45",
                        "decimals": "1",
                        "numberSuffix": "°C",
                        "showhovereffect": "1",
                        "showToolTip": "0",
                        "thmFillColor": "#e44a00",
                        "showGaugeBorder": "1",
                        "gaugeBorderColor": "#e44a00",
                        "gaugeBorderThickness": "1",
                        "gaugeBorderAlpha": "30",
                        "thmOriginX": "30",
                        "chartBottomMargin": "0",
                        "valueFontSize": "0",
                        "theme": "fint",
                        "showTickMarks": "0",
                        "showTickValues": "0",
                        "borderAlpha": "0",
                        "bgAlpha": '0',
                        "canvasBgAlpha": "0",

                        //"hoverCapBgColor": "#ff0000"
                    },
                    "value": temp_high
                }
            })
            .render();
            //左侧低温温度计
            var thmLow = new FusionCharts({
                type: 'thermometer',
                renderAt: 'thmLow',
                width: '40',
                height: '100',
                dataFormat: 'json',
                containerBackgroundOpacity: "0",
                dataSource: {
                    "chart": {
                        "lowerLimit": "-5",
                        "upperLimit": "45",
                        "decimals": "1",
                        "numberSuffix": "°C",
                        "showhovereffect": "1",
                        "showToolTip": "0",
                        "thmFillColor": "#008ee4",
                        "showGaugeBorder": "1",
                        "gaugeBorderColor": "#008ee4",
                        "gaugeBorderThickness": "1",
                        "gaugeBorderAlpha": "30",
                        "thmOriginX": "30",
                        "chartBottomMargin": "0",
                        "valueFontSize": "0",
                        "theme": "fint",
                        "showTickMarks": "0",
                        "showTickValues": "0",
                        "borderAlpha": "0",
                        "bgAlpha": '0',
                        "canvasBgAlpha": "0",
                    },
                    "value": temp_low
                }
            })
            .render();
        },

    });
  

var Nodes = [
    { Station_Code: "Num01", Station_Name: "压缩机排气温度" },
    { Station_Code: "Num02", Station_Name: "冷凝器出口温度" },
    { Station_Code: "Num03", Station_Name: "蒸发器出口" },
    { Station_Code: "Num04", Station_Name: "蒸发器入口" },
    { Station_Code: "Num05", Station_Name: "冰块温度" },
    { Station_Code:"Num06",Station_Name:"风机出口温度"},
    {Station_Code:"Num07",Station_Name:"房间温度"},
   
];
var checkList = [];
var field = [];
/* 默认勾选前五个 */
for (var i = 0; i < 5; i++) {
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
//折线图(第一页)
function showChart2() {

    var stcd = "";
    for (var i = 0; i < checkList.length; i++) {
        stcd += checkList[i] + ",";
    }
    var length = stcd.length;
    var stcdstr = stcd.substring(0, length - 1)
    $.post(commonUrl + "api/RealTime/GetMonitoringcurve", {
        stcd: stcdstr
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
           if (data.message.rows!="") {
               rows = eval('(' + data.message.rows + ')');
               msg.rows = rows;
               for (var i = 0; i < msg.rows.length; i++) {
                   Time[i] = msg.rows[i].TimeStamp.substring(0, 10) + " " + msg.rows[i].TimeStamp.substring(11, 19);
               }
               for (var j = 0; j < checkList.length; j++) {
                   if (checkList[j] == "Num01") {
                       var arr1 = {}
                       arr1.name = "集热器出口温度";
                       arr1.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr1.data.push(parseFloat(msg.rows[k].Num01));

                       }
                       datajson[j] = arr1;
                   } else if (checkList[j] == "Num02") {
                       var arr2 = {}
                       arr2.name = "水箱温度";
                       arr2.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr2.data.push(parseFloat(msg.rows[k].Num02));
                       }
                       datajson[j] = arr2;
                   } else if (checkList[j] == "Num03") {
                       var arr3 = {}
                       arr3.name = "供水温度";
                       arr3.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr3.data.push(parseFloat(msg.rows[k].Num03));
                       }
                       datajson[j] = arr3;
                   } else if (checkList[j] == "Num04") {
                       var arr4 = {}
                       arr4.name = "环境温度";
                       arr4.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr4.data.push(parseFloat(msg.rows[k].Num04));
                       }
                       datajson[j] = arr4;
                   } else if (checkList[j] == "Num05") {
                       var arr5 = {}
                       arr5.name = "水箱液位高度";
                       arr5.data = [];
                       for (var k = 0; k < msg.rows.length; k++) {
                           arr5.data.push(parseFloat(msg.rows[k].Num05));
                       }
                       datajson[j] = arr5;
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
    for (var i = 0; i < 5; i++) {
        Nodes[i].checked = false;
        //checkList.push(Nodes[i].Station_Code);
        checkList = [];
    }
    treeInit(Nodes, "#site-tree");

}
//全选
function checkAll() {
    for (var i = 0; i < 5; i++) {
        Nodes[i].checked = true;
        checkList.push(Nodes[i].Station_Code);
    }
    treeInit(Nodes, "#site-tree");

}


//第二页指标柱形图与曲线图
//左上角
function partone() {
    var selectone = $("#select1 option:selected").val();
    //请求数据
    $.post(commonUrl + "api/RealTime/GetTemperatureandswitch", {
        starttm: startm,
        endtm: endtm,
        stcd: selectone
    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }
            var msg = data;
            $("#lasttime1").html(msg.message.rows[msg.message.rows.length - 1].time.substring(11, 19));
            $("#lastday1").html(msg.message.rows[msg.message.rows.length - 1].time.substring(0, 10))
            var lastvlue = msg.message.rows[msg.message.rows.length - 1].Num;
            var datajson = [];
            for (var i = 0; i < msg.message.rows.length - 1;i++){
                var arr1 = {}
                arr1.label = msg.message.rows[i].time.substring(11, 16);
                arr1.value = parseFloat(msg.message.rows[i].Num)
                datajson[i] = arr1;
            }
            var fuelWidget1 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'middleChart1',
                width: '120',
                height: '195',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#00ff00"
                    },
                    "value": lastvlue
                }
            }).render();
            //曲线
            var Chart1 = new FusionCharts({
                type: 'line',
                dataFormat: 'json',
                renderAt: 'middleChart5',
                width: '320',
                height: '195',
                dataSource: {
                    "chart": {
                        "caption": "指标变化趋势图",
                        "showValues": "0",
                        "lineThickness": "5",
                        "paletteColors": "#00ff00",
                        "theme": "fint",
                        "divLineDashed": "1",
                        "divLineDashLen": "5",
                        "divLineDashGap": "6"
                    },
                    "data": datajson
                }
            }).render();

        },
        "json");
    
}
//右上角
function parttwo() {
    var selecttwo = $("#select2 option:selected").val();
    //请求数据
    $.post(commonUrl + "api/RealTime/GetTemperatureandswitch", {
        starttm: startm,
        endtm: endtm,
        stcd: selecttwo
    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }
            var msg = data;
            $("#lasttime2").html(msg.message.rows[msg.message.rows.length - 1].time.substring(11, 19));
            $("#lastday2").html(msg.message.rows[msg.message.rows.length - 1].time.substring(0, 10))
            var lastvlue = msg.message.rows[msg.message.rows.length - 1].Num;
            var datajson = [];
            for (var i = 0; i < msg.message.rows.length - 1; i++) {
                var arr1 = {}
                arr1.label = msg.message.rows[i].time.substring(11, 16);
                arr1.value = parseFloat(msg.message.rows[i].Num)
                datajson[i] = arr1;
            }
            var fuelWidget1 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'middleChart2',
                width: '120',
                height: '195',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#EE30A7"
                    },
                    "value": lastvlue
                }
            }).render();
            var Chart2 = new FusionCharts({
                type: 'line',
                dataFormat: 'json',
                renderAt: 'middleChart6',
                width: '320',
                height: '195',
                dataSource: {
                    "chart": {
                        "caption": "指标变化趋势图",
                        "showValues": "0",
                        "lineThickness": "5",
                        "paletteColors": "#EE30A7",
                        "theme": "fint",
                        "divLineDashed": "1",
                        "divLineDashLen": "5",
                        "divLineDashGap": "6"
                    },
                    "data": datajson
                }
            }).render();

        },
        "json");

}
//左下角
function partthree() {
    var selectthree = $("#select3 option:selected").val();
    //请求数据
    $.post(commonUrl + "api/RealTime/GetMeasurementIndex", {
        starttm: startm,
        endtm: endtm,
        stcd: selectthree
    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }
            var msg = data;
            $("#lasttime3").html(msg.message.rows[msg.message.rows.length - 1].time.substring(11, 19));
            $("#lastday3").html(msg.message.rows[msg.message.rows.length - 1].time.substring(0, 10))
            var lastvlue = msg.message.rows[msg.message.rows.length - 1].Num;
            var datajson = [];
            for (var i = 0; i < msg.message.rows.length - 1; i++) {
                var arr1 = {}
                arr1.label = msg.message.rows[i].time.substring(11, 16);
                arr1.value = parseFloat(msg.message.rows[i].Num)
                datajson[i] = arr1;
            }
            var fuelWidget1 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'middleChart3',
                width: '120',
                height: '195',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#8B0A50"
                    },
                    "value": lastvlue
                }
            }).render();
            var Chart3 = new FusionCharts({
                type: 'line',
                dataFormat: 'json',
                renderAt: 'middleChart7',
                width: '320',
                height: '195',
                dataSource: {
                    "chart": {
                        "caption": "指标变化趋势图",
                        "showValues": "0",
                        "lineThickness": "5",
                        "paletteColors": "#8B0A50",
                        "theme": "fint",
                        "divLineDashed": "1",
                        "divLineDashLen": "5",
                        "divLineDashGap": "6"
                    },
                    "data": datajson
                }
            }).render();

        },
        "json");

}
//右下角
function partfour() {
    var selectfour = $("#select4 option:selected").val();
    //请求数据
    $.post(commonUrl + "api/RealTime/GetMeasurementIndex", {
        starttm: startm,
        endtm: endtm,
        stcd: selectfour
    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }
            var msg = data;
            $("#lasttime4").html(msg.message.rows[msg.message.rows.length - 1].time.substring(11, 19));
            $("#lastday4").html(msg.message.rows[msg.message.rows.length - 1].time.substring(0, 10))
            var lastvlue = msg.message.rows[msg.message.rows.length - 1].Num;
            var datajson = [];
            for (var i = 0; i < msg.message.rows.length - 1; i++) {
                var arr1 = {}
                arr1.label = msg.message.rows[i].time.substring(11, 16);
                arr1.value = parseFloat(msg.message.rows[i].Num)
                datajson[i] = arr1;
            }
            var fuelWidget1 = new FusionCharts({
                type: 'cylinder',
                dataFormat: 'json',
                renderAt: 'middleChart4',
                width: '120',
                height: '195',
                dataSource: {
                    "chart": {
                        "theme": "fint",
                        "lowerLimit": "0",
                        "showValue": "0",
                        "chartBottomMargin": "-10",
                        "cylFillColor": "#3A5FCD"
                    },
                    "value": lastvlue
                }
            }).render();
            var Chart4 = new FusionCharts({
                type: 'line',
                dataFormat: 'json',
                renderAt: 'middleChart8',
                width: '320',
                height: '195',
                dataSource: {
                    "chart": {
                        "caption": "指标变化趋势图",
                        "showValues": "0",
                        "lineThickness": "5",
                        "paletteColors": "#3A5FCD",
                        "theme": "fint",
                        "divLineDashed": "1",
                        "divLineDashLen": "5",
                        "divLineDashGap": "6"
                    },
                    "data": datajson
                }
            }).render();

        },
        "json");

}

///初期实例（弃用）
function initPart2() {
   

    //第2页左上
    var fuelWidget1 = new FusionCharts({
        type: 'cylinder',
        dataFormat: 'json',
        renderAt: 'middleChart1',
        width: '120',
        height: '195',
        dataSource: {
            "chart": {
                "theme": "fint",
                "lowerLimit": "0",
                "showValue": "0",
                "chartBottomMargin": "-10",
                "cylFillColor": "#00ff00"
            },
            "value": "40.2"
        }
    }).render();
    var Chart1 = new FusionCharts({
        type: 'line',
        dataFormat: 'json',
        renderAt: 'middleChart5',
        width: '320',
        height: '195',
        dataSource: {
            "chart": {
                "caption": "指标变化趋势图",
                "showValues": "0",
                "lineThickness": "5",
                "paletteColors": "#00ff00",
                "theme": "fint",
                "divLineDashed": "1",
                "divLineDashLen": "5",
                "divLineDashGap": "6"
            },
            "data": [
                {
                    "label": "Mon",
                    "value": "5123"
                },
                {
                    "label": "Tue",
                    "value": "4233"
                },
                {
                    "label": "Wed",
                    "value": "5507"
                },
                {
                    "label": "Thu",
                    "value": "4110"
                },
                {
                    "label": "Fri",
                    "value": "5529"
                },
                {
                    "label": "Sat",
                    "value": "5803"
                },
                {
                    "label": "Sun",
                    "value": "6202"
                }
            ]
        }
    }).render();

    //第2页右上
    var fuelWidget1 = new FusionCharts({
        type: 'cylinder',
        dataFormat: 'json',
        renderAt: 'middleChart2',
        width: '120',
        height: '195',
        dataSource: {
            "chart": {
                "theme": "fint",
                "lowerLimit": "0",
                "showValue": "0",
                "chartBottomMargin": "-10",
                "cylFillColor": "#EE30A7"
            },
            "value": "40.2"
        }
    }).render();
    var Chart2 = new FusionCharts({
        type: 'line',
        dataFormat: 'json',
        renderAt: 'middleChart6',
        width: '320',
        height: '195',
        dataSource: {
            "chart": {
                "caption": "指标变化趋势图",
                "showValues": "0",
                "lineThickness": "5",
                "paletteColors": "#EE30A7",
                "theme": "fint",
                "divLineDashed": "1",
                "divLineDashLen": "5",
                "divLineDashGap": "6"
            },
            "data": [
                {
                    "label": "Mon",
                    "value": "5123"
                },
                {
                    "label": "Tue",
                    "value": "4233"
                },
                {
                    "label": "Wed",
                    "value": "5507"
                },
                {
                    "label": "Thu",
                    "value": "4110"
                },
                {
                    "label": "Fri",
                    "value": "5529"
                },
                {
                    "label": "Sat",
                    "value": "5803"
                },
                {
                    "label": "Sun",
                    "value": "6202"
                }
            ]
        }
    }).render();

    //第2页左下
    var fuelWidget1 = new FusionCharts({
        type: 'cylinder',
        dataFormat: 'json',
        renderAt: 'middleChart3',
        width: '120',
        height: '195',
        dataSource: {
            "chart": {
                "theme": "fint",
                "lowerLimit": "0",
                "showValue": "0",
                "chartBottomMargin": "-10",
                "cylFillColor": "#8B0A50"
            },
            "value": "40.2"
        }
    }).render();
    var Chart3 = new FusionCharts({
        type: 'line',
        dataFormat: 'json',
        renderAt: 'middleChart7',
        width: '320',
        height: '195',
        dataSource: {
            "chart": {
                "caption": "指标变化趋势图",
                "showValues": "0",
                "lineThickness": "5",
                "paletteColors": "#8B0A50",
                "theme": "fint",
                "divLineDashed": "1",
                "divLineDashLen": "5",
                "divLineDashGap": "6"
            },
            "data": [
                {
                    "label": "Mon",
                    "value": "5123"
                },
                {
                    "label": "Tue",
                    "value": "4233"
                },
                {
                    "label": "Wed",
                    "value": "5507"
                },
                {
                    "label": "Thu",
                    "value": "4110"
                },
                {
                    "label": "Fri",
                    "value": "5529"
                },
                {
                    "label": "Sat",
                    "value": "5803"
                },
                {
                    "label": "Sun",
                    "value": "6202"
                }
            ]
        }
    }).render();

    //第2页右下
    var fuelWidget1 = new FusionCharts({
        type: 'cylinder',
        dataFormat: 'json',
        renderAt: 'middleChart4',
        width: '120',
        height: '195',
        dataSource: {
            "chart": {
                "theme": "fint",
                "lowerLimit": "0",
                "showValue": "0",
                "chartBottomMargin": "-10",
                "cylFillColor": "#3A5FCD"
            },
            "value": "40.2"
        }
    }).render();
    var Chart4 = new FusionCharts({
        type: 'line',
        dataFormat: 'json',
        renderAt: 'middleChart8',
        width: '320',
        height: '195',
        dataSource: {
            "chart": {
                "caption": "指标变化趋势图",
                "showValues": "0",
                "lineThickness": "5",
                "paletteColors": "#3A5FCD",
                "theme": "fint",
                "divLineDashed": "1",
                "divLineDashLen": "5",
                "divLineDashGap": "6"
            },
            "data": [
                {
                    "label": "Mon",
                    "value": "5123"
                },
                {
                    "label": "Tue",
                    "value": "4233"
                },
                {
                    "label": "Wed",
                    "value": "5507"
                },
                {
                    "label": "Thu",
                    "value": "4110"
                },
                {
                    "label": "Fri",
                    "value": "5529"
                },
                {
                    "label": "Sat",
                    "value": "5803"
                },
                {
                    "label": "Sun",
                    "value": "6202"
                }
            ]
        }
    }).render();

    
}

var initFlag = 0;
$(".nav-btn").click(function () {
    if(!$(this).hasClass("active")) {
        $(this).addClass("active").siblings().removeClass("active");
        $(".white-bg").eq($(this).index()).addClass("active").siblings().removeClass("active");
        if ($(this).attr('id') == "nav2") {
            if (initFlag) {//防止重复初始化
                return;
            }
            initFlag++;
            partone();
            parttwo();
            partthree();
            partfour();
        }
    }
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
})
