$(".main-content-tab").click(function () {
    if (!$(this).hasClass("acitve")) {
        $(this).addClass("active").siblings().removeClass("active");
        $(".main-content-body > div").eq($(this).index()).addClass("active").siblings().removeClass("active");
    }
})


//获取项目信息
GetProject();
function GetProject() {

    //请求数据
    $.post(commonUrl + "api/ProjectDetailInfo/GetProject", {
        ProjectCode: "001"
    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }

            var msg = data;
            $("#ProjectCode").html(msg.message.rows[0].ProjectCode);
            $("#AreaName").html(msg.message.rows[0].AreaName);
            $("#ProjectName").html(msg.message.rows[0].ProjectName);
            $("#ProjectAddress").html(msg.message.rows[0].ProjectAddress);
            $("#HeatingArea1").html(msg.message.rows[0].HeatingArea1);
            $("#XAxis").html(msg.message.rows[0].XAxis);
            $("#YAxis").html(msg.message.rows[0].YAxis);
        },
        "json");

}

//获取电量消耗值
GetLastQoeIndex();
function GetLastQoeIndex() {
    //请求数据
    $.post(commonUrl + "api/ProjectDetailInfo/GetLastQoeIndex", {
    },
        function (data, status) {
            if (0 != data.status) {
                alert("未查询到相关数据！"); //提示框
                return;
            }


            var msg = data;
            $("#total-electric").html(msg.message.rows[0].总电能);
            $("#CMCelectric").html(msg.message.rows[0].压缩机电能);
            $("#Solarelectric").html(msg.message.rows[0].光伏输出电能);
        },
        "json");

}




//获取Flash
var msg;
var msg1;
LoadFlashXML();
function LoadFlashXML() {
    //获取实时数据
    $.post(commonUrl + "api/ProjectDetailInfo/GetRealtimeFlash", {

    },
              function (data, status) {
                  if (0 != data.status) {
                      alert(data.message); //提示框
                      return;
                  }
                  var obj = eval(data);
                  msg = obj.message.rows;
                  InsertRealtimedata(msg);
              },
              "json");


}

//传递实时数据到后台
function InsertRealtimedata(datajson1) {
    var json = JSON.stringify(datajson1);
    $.ajax({
        contentType:"text/javascript;charset=utf-8",
        url: "../../DataServices/Handler1.ashx",
        data: {
            type: "Realtimedata",
            datajson: json
        },
        success: function (data) {
           

        },
    });

}



var num = "8";//云南光伏制冷
LoadFlash(num);
//加载flash动画
//flash动画的文件名称以项目名称命名
function LoadFlash(num) {
    document.getElementById("Flash").innerHTML = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0' width='1280' height='568'>" +
"<param name='movie' value='Flash/" + num + ".swf'>" +
"<param name='quality' value='high'>" +
"<param name='wmode' value='transparent'>" +
"<embed src='../Flash/" + num + ".swf' width='1280' height='568' quality='high' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' wmode='transparent'></embed>" +
"</object>";

}

//定时刷新
setInterval(function () {
    LoadFlashXML();
    LoadFlash(num);
    GetLastQoeIndex();
},
      60000);