

/// <reference path="/Script/jquery-1.8.2.js" />
/// <reference path="/Script/ZYUiPub.js" />
/// <reference path="/script/common.js" />

$(function () {

    BindPageSetting();

})

function BindPageSetting() {
    BindDateInput("txt_PendingTime1", GetDateStr(-30));
    BindDateInput("txt_PendingTime2", GetDateStr(1));
    GetWaitForPendingOrderToWorkList(1);
}



function GetWaitForPendingOrderToWorkList(CurrentPage) {



    AjaxPost("/ao/", "GetWaitForPendingOrderToWorkList",
                                         {
                                             CurrentPage: CurrentPage,
                                             dtm1: $("#txt_PendingTime1").val(),
                                             dtm2: $("#txt_PendingTime2").val()

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 if (data.list.length > 0) {
                                                     for (var i = 0; i < data.list.length; i++) {
                                                         var j = data.list[i];
                                                         w.push("<tr class='s" + j.OrderToWorkStatusId + "' OrderToWorkId='" + j.OrderToWorkId + "' OrderToWorkStatusId='" + j.OrderToWorkStatusId + "' OrderId='" + j.OrderId + "'  >");
                                                         w.push("<td>");
                                                         w.push(j.OrderName);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(accMul(j.Wages, j.Wages));
                                                         w.push("</td>");

                                                         w.push("<td>");
                                                         w.push(DateFormat(j.NeedPendingTime, "yyyy-MM-dd"));
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.OrderToWorkStatusName);
                                                         w.push("</td>");
                                                         w.push("<td class='c' >");

                                                         if (j.OrderToWorkStatusId < 70) {
                                                             w.push("-");
                                                         }
                                                         else {
                                                             w.push(DateFormat(j.PendingTime, "yyyy-MM-dd"));
                                                         }


                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.Phone + "[" + j.RealName + "]");
                                                         w.push("</td>");
                                                         w.push("</tr>");

                                                     }
                                                 }


                                             }
                                             else {
                                                 alert(data.re)
                                             }

                                             $("#tbody_OrderToWork").html(w.join(""));

                                             $("#tbody_OrderToWork>tr").each(function () {

                                                 var OrderToWorkStatusId = ConvertToInt($(this).attr("OrderToWorkStatusId"));



                                                 /// <reference path="/Script/jquery-1.8.2.js" />
                                                 /// <reference path="/Script/ZYUiPub.js" />
                                                 /// <reference path="/script/common.js" />
                                                 r = [];
                                                 r.push({
                                                     evName: "ToOrderInfo",
                                                     Title: "查看订单",
                                                     evIcon: ""
                                                 });
                                                 if (OrderToWorkStatusId <= 60) {

                                            
                                                     r.push({
                                                         evName: "PendingOrderToWork",
                                                         Title: "立即结算",
                                                         evIcon: ""
                                                     });


                                                 }

                                          

                                                 RightMenu(this, r);




                                             });
                                             ZyPagerSetting("GetWaitForPendingOrderToWorkList", CurrentPage, data.t, "1");
                                         }, false);


}

function PendingOrderToWork(obj) {

    var OrderToWorkId = $(obj).attr("OrderToWorkId");




    AjaxPost("/ao/", "PendingOrderToWork",
                                         {
                                             OrderToWorkId: OrderToWorkId

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 GetWaitForPendingOrderToWorkList(1);


                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);




}


function ToOrderInfo(obj)
{
    var OrderId = $(obj).attr("OrderId");

    window.open("/Order/OrderInfo.aspx?OrderId=" + OrderId + "");

}
