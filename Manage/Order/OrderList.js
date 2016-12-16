


/// <reference path="/Script/jquery-1.8.2.js" />
/// <reference path="/Script/ZYUiPub.js" />
/// <reference path="/script/common.js" />






$(function () {

    BindPageSetting();
})


function BindPageSetting() {
    BindDateInput("txt_CreateTime1", GetDateStr(-60));
    BindDateInput("txt_CreateTime2", GetDateStr(1));
    BindStatus();
    GetOrderList(1);
}


function ToOrderInfo(obj) {


    var url = "OrderInfo.aspx";

    if (obj) {

        var OrderId = $(obj).attr("OrderId");

        url = url + "?OrderId=" + OrderId + "";
    }


    tiaozhuan(url);

}

function BindStatus() {
    var w = [];

    w.push("<a OrderStatusId='All'  class='select'  >");
    w.push("全部(<span id='sp_Status_All' >0</span>)");
    w.push("</a>");

    w.push("<a OrderStatusId='-10'    >");
    w.push("作废(<span id='sp_Status_-10' >0</span>)");
    w.push("</a>");
    w.push("<a OrderStatusId='5'  >");
    w.push("发布(<span id='sp_Status_5'   >0</span>)");
    w.push("</a>");
    w.push("<a OrderStatusId='20'  >");
    w.push("抢单(<span id='sp_Status_20'   >0</span>)");
    w.push("</a>");
    w.push("<a OrderStatusId='30'   >");
    w.push("生产(<span id='sp_Status_30'  >0</span>)");
    w.push("</a>");
    w.push("<a OrderStatusId='100'  >");
    w.push("完成(<span id='sp_Status_100'  >0</span>)");
    w.push("</a>");

    $("#div_Status").html(w.join(""));
    $("#div_Status>a").click(function () {

        

        var b = $(this).hasClass("select");

        if (b) {

        }
        else {
            $(this).siblings().removeClass("select");
            $(this).addClass("select");
            GetOrderList(1);
           
        }

    });
}


function GetOrderList(CurrentPage) {


    var OrderStatusIdArray = [];

    $("#div_Status>a").each(function () {

        var b = $(this).hasClass("select");
        $(this).find("span").html("0");
        if (b)
        {

            OrderStatusIdArray.push($(this).attr("OrderStatusId"))
        }

    });


    if (OrderStatusIdArray.length==0)
    {

        alert("至少选择一种订单状态");
        return;
    }



    AjaxPost("/ao/", "GetOrderList",
                                   {
                                       CurrentPage: CurrentPage,
                                       CreateTime1: $("#txt_CreateTime1").val(),
                                       CreateTime2: $("#txt_CreateTime2").val(),
                                       OrderId: $("#txt_OrderId").val(),
                                       CountByStatus: true,
                                       OrderStatusIdArray:OrderStatusIdArray.join(","),

                                       col: "OrderId,OrderImgUrl,OrderName,OutPlaces,OnlyPlaces,OrderQuantity,ProcessLvTitle,ProcessLvName,OrderWages,OrderStatusName,CreateTime,PlanningTime,OrderStatusId,DoneTime,ReceivedTime"
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];
                                               w.push("<tr ondblclick='ToOrderInfo(this)' OrderId='" + j.OrderId + "' OrderStatusId='" + j.OrderStatusId + "'  >");
                                               w.push("<td class='c'>");

                                               imgReSize
                                               w.push("<img src='" + FormatImgTo200Px(j.OrderImgUrl) + "'  class='img_OrderImg' />");
                                               w.push("</td>");
                                               w.push("<td>");
                                               w.push(j.OrderName);
                                               w.push("</td>");
                                               w.push("<td class='c'>");
                                               w.push(accSubtr(j.OutPlaces, j.OnlyPlaces) + "/" + j.OutPlaces);
                                               w.push("</td>");


                                               w.push("<td class='c'>");
                                               w.push(j.OrderQuantity);
                                               w.push("</td>");
                                               w.push("<td class='c' >");
                                               w.push("[" + j.ProcessLvTitle + "]" + j.ProcessLvName);
                                               w.push("</td>");
                                               w.push("<td class='c'>");
                                               w.push(j.OrderWages);
                                               w.push("</td>");
                                               w.push("<td class='c'>");
                                               w.push(j.OrderStatusName);
                                               w.push("</td>");

                                               w.push("<td class='c'>");
                                               w.push(DateFormat(j.ReceivedTime, "MM月dd日"));
                                               w.push("</td>");
                                               w.push("<td class='c'>");
                                               w.push(DateFormat(j.PlanningTime, "MM月dd日"));
                                               w.push("</td>");
                                               w.push("<td class='c'>");

                                               if (j.OrderStatusId < 70) {
                                                   w.push("未完成");
                                               }
                                               else {
                                                   w.push(DateFormat(j.DoneTime, "MM月dd日"));
                                               }


                                               w.push("</td>");
                                               w.push("</tr>");
                                           }




                                           for (var i = 0; i < data.by.Table.length; i++) {

                                               var j = data.by.Table[i];

                                               $("#sp_Status_" + j.OrderStatusId + "").html(j.OrderStatusNum);



                                           }
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                       $("#t_1").html(w.join(""));
                                       ZyPagerSetting("GetOrderList", CurrentPage, data.t, "1");
                                       $("#t_1>tr").each(function () {

                                           r = [];
                                           var OrderStatusId = ConvertToInt($(this).attr("OrderStatusId"));


                                           if (OrderStatusId != 10 && OrderStatusId < 30) {   //生产状态了, 就没法撤回了
                                               r.push({
                                                   evName: "OrderToFaBu",
                                                   Title: "发布订单",
                                                   evIcon: ""
                                               });
                                           }

                                           if (OrderStatusId >= 10 && OrderStatusId < 30) {  //生产状态了, 就没法撤回了
                                               r.push({
                                                   evName: "OrderToQiangDan",
                                                   Title: "抢单状态",
                                                   evIcon: ""
                                               });
                                           }

                                           if (OrderStatusId >= 10) {   //生产状态, 释放所有登记过但是没有分派过的用户
                                               r.push({
                                                   evName: "OrderToProduction",
                                                   Title: "生产状态",
                                                   evIcon: ""
                                               });
                                           }



                                           if (OrderStatusId >= 30) {  //在生产状态之后
                                               r.push({
                                                   evName: "OrderToDone",
                                                   Title: "完成订单",
                                                   evIcon: ""
                                               });
                                           }

                                           if (OrderStatusId != -10) {
                                               r.push({
                                                   evName: "OrderToZuoFei",
                                                   Title: "作废订单",
                                                   evIcon: ""
                                               });
                                           }





                                           RightMenu(this, r);
                                       });





                                   }, false);
}
// #region 右键菜单事件

function OrderToQiangDan(obj) {

    var OrderId = $(obj).attr("OrderId");


    AjaxPost("/ao/", "ChangeOrderStatus",
                                   {
                                       OrderStatusId: 20,
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           window.parent.BodyTip({

                                               text: "状态修改为抢单中!",
                                               s: 5000

                                           });
                                           GetOrderList(1);
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}

function OrderToFaBu(obj) {
    var OrderId = $(obj).attr("OrderId");


    AjaxPost("/ao/", "ChangeOrderStatus",
                                   {
                                       OrderStatusId: 10,
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           window.parent.BodyTip({

                                               text: "状态修改为已发布!",
                                               s: 5000

                                           });
                                           GetOrderList(1);
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}

function OrderToProduction(obj) {

    var OrderId = $(obj).attr("OrderId");


    AjaxPost("/ao/", "ChangeOrderStatus",
                                   {
                                       OrderStatusId: 30,
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           window.parent.BodyTip({

                                               text: "状态修改为已完成!",
                                               s: 5000

                                           });
                                           GetOrderList(1);
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}


function OrderToDone(obj) {
    var OrderId = $(obj).attr("OrderId");


    AjaxPost("/ao/", "ChangeOrderStatus",
                                   {
                                       OrderStatusId: 100,
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           window.parent.BodyTip({

                                               text: "状态修改为已完成!",
                                               s: 5000

                                           });
                                           GetOrderList(1);
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}

function OrderToZuoFei(obj) {
    var OrderId = $(obj).attr("OrderId");


    AjaxPost("/ao/", "ChangeOrderStatus",
                                   {
                                       OrderStatusId: -10,
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           window.parent.BodyTip({

                                               text: "状态修改为已作废!",
                                               s: 5000

                                           });
                                           GetOrderList(1);
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}



// #endregion

function ToOrderInfo(obj) {


    var OrderId = $(obj).attr("OrderId");


    tiaozhuan("OrderInfo.aspx?OrderId=" + OrderId + "");

}