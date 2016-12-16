/// <reference path="C:\anli\优做\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\优做\Manage\Script/ZYUiPub.js" />


$(function () {

   
    if (confirm("确定添加新的订单草稿吗?")) {
        AddOrderInfo();
    }
    else {
        tiaozhuan("OrderList.aspx");
    }

  
})


function AddOrderInfo()
{
    AjaxPost("/ao/", "AddOrderInfo",
                                   {
                                       CurrentPage: 1
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           tiaozhuan("OrderInfo.aspx?OrderId=" + data.OrderId + "");
                                       }
                                       else {
                                           alert(data.re)
                                       }

                
                                   }, false);
}