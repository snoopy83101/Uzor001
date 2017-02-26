/// <reference path="C:\anli\快工场\Web\script/common.js" />
/// <reference path="C:\anli\快工场\Web\script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Web\script/ZYUiPub.js" />

$(function () {

    BindPageSetting();

})






function BindPageSetting() {

    GetOrderInfo();
}

function GetOrderInfo() {



    AjaxPost("/ao/", "GetOrderInfo",
                                   {
                                       OrderId: GetQueryString("OrderId")
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           var IsMyOrder = false;





                                           //if (data.OrderVsMember.length > 0) {




                                           //    for (var i = 0; i < data.OrderVsMember.length; i++) {

                                           //        var j = data.OrderVsMember[i];

                                           //        if (j.MemberId == ConvertToInt(mj.MemberId)) {
                                           //            IsMyOrder = true;
                                           //            if (j.VsStatus >= 10 || j.VsStatus <= -10) {

                                           //                //   $("#main").prepend("<img src='../../image/Order/yijiedan2.png' class='img_yijiedan'  />");
                                           //                $("#a_SaveOrderVsMember").hide();
                                           //            }
                                           //        }




                                           //    }
                                           //}


                                           //if (data.Info.OrderStatusId != 20) {

                                           //    if (IsMyOrder) {

                                           //    }
                                           //    else {
                                           //        $("#a_SaveOrderVsMember").hide();
                                           //        //api.toast({
                                           //        //    msg: '此订单已关闭抢单!',
                                           //        //    duration: 2000,
                                           //        //    location: 'middle'
                                           //        //});
                                           //    }
                                           //}



                                           $("#b_ProcessLvName").html(data.Info.ProcessLvName);



                                           if (data.Info.ReleaseTypeId == 20) {

                                               $("#b_MinQuantity").html(data.Count.OrderQuantity + " " + data.Info.Unit);
                                           }
                                           else {

                                               $("#b_MinQuantity").html(data.Info.MinQuantity + " " + data.Info.Unit);
                                           }


                                           $("#b_OrderWages").html(ConverttoDecimal2f(data.Info.OrderWages));
                                           $("#sp_h_Num").html(data.Info.OrderQuantity);
                                           $("#sp_h_NoNum").html("剩余" + accSubtr(data.Count.OrderQuantity, data.Count.WorkQuantity) + "(" + data.Info.Unit + ")");

                                           if (data.OrderExpect.length > 0) {
                                               w = [];
                                               w.push("<li class='li_yggs' ><span class='sp_l' >预估工时:<b>全天10个小时</b></span></li>");
                                               for (var i = 0; i < data.OrderExpect.length; i++) {

                                                   var j = data.OrderExpect[i];
                                                   w.push("<li >");
                                                   w.push("<span class='sp_l' >第" + j.OrderExpectDay + "天:");
                                                   w.push("<b >" + j.Num + "</b>" + data.Info.Unit + " </span>");
                                                   w.push("</li>");


                                               }
                                               $("#ul_OrderExpect").html(w.join(""));
                                           }

                                           $("#sp_OrderTitle").html(data.Info.OrderTitle);
                                           $("#sp_OrderQuantity").html(data.Count.OrderQuantity);
                                           $("#sp_MinQuantity").html(data.Info.MinQuantity);
                                           $("#sp_OrderWages").html(ConverttoDecimal2f(data.Info.OrderWages));
                                           $("#sp_ReceivedTime").html(DateFormat(data.Info.ReceivedTime, "MM月dd日 h:mm"));
                                           $("#sp_PlanningTime").html(DateFormat(data.Info.PlanningTime, "MM月dd日 h:mm"));
                                           $("#sp_ProcessLocationType").html(data.Info.ProcessLocationTypeName);
                                           $("#sp_OrderContacts").html(data.Info.OrderContacts);
                                           $("#sp_OrderTel").html(data.Info.OrderTel);
                                           $("#li_Phone").attr("Phone", data.Info.OrderTel);
                                           $(".b_Unit").html(data.Info.Unit);
                                           $("#li_OrderAddress").attr("OrderAddress", data.Info.OrderAddress);
                                           $("#p_OrderAddress").html(data.Info.OrderAddress);



                                           if (mj.MemberId == 0 || mj.ProcessLvId < mj.ProcessLvId || mj.ProcessLvStatusId < 20) {
                                               $("#div_OrderContent").hide();

                                           }
                                           else {
                                               $("#div_OrderContent").html(data.Info.OrderContent);
                                               var n = 0;


                                               var imgArray = [];
                                               //$("#div_OrderContent img").each(function () {
                                               //    var src = $(this).attr("src");
                                               //    src = FormatImgUrl(src);

                                               //    api.imageCache({
                                               //        url: src
                                               //    }, function (ret, err) {
                                               //        var url = ret.url;

                                               //    });

                                               //    // alert(src);
                                               //    $(this).attr("src", src);

                                               //    imgArray.push(src);

                                               //    $(this).click(function () {
                                               //        OpenImg(this, imgArray);

                                               //    });

                                               //})
                                               $("#div_OrderContent").show();
                                           }







                                           $("#main").show();

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);

}

function OpenImg(obj, imgArray) {

    var url = $(obj).attr("src");
    var index = 0;

    for (var i = 0; i < imgArray.length; i++) {
        var j = imgArray[i];
        if (url == j) {
            index = i;
            break;
        }

    }
    //  alert(index);

    var photoBrowser = api.require('photoBrowser');
    photoBrowser.open({
        images: imgArray,
        placeholderImg: 'widget://image/001.gif',
        bgColor: '#000',
        activeIndex: index
    }, function (ret, err) {
        if (ret) {

            switch (ret.eventType) {
                case "click":
                    photoBrowser.close();
                    break;
                default:

            }
            // alert(JSON.stringify(ret));
        } else {
            // alert(JSON.stringify(err));
        }
    });
}

function PopOrderAddress(obj) {

    //var OrderAddress = $(obj).attr("OrderAddress");

    //api.alert({
    //    msg: OrderAddress,
    //    title: "联系地址:"

    //});

}
var loading = false;
//抢单
function SaveOrderVsMember() {

    var mj = GetCurrentMember();


    if (mj.MemberId == 0) {


        openW({

            name: "login",
            url: "../Login/login.html"

        })

        return;
    }



    if (mj.ProcessLvStatusId < 20) {


        openW({

            name: "ToMyProcessLvSubject",
            url: "../Member/ToMyProcessLvSubject.html"

        })

        return;
    }




    if (loading) {
        api.toast({
            msg: '请不要频繁点击 !',
            duration: 2000,
            location: 'middle'
        });
    }

    loading = true;
    AjaxPost("/ao/", "SaveOrderVsMember",
                                   {
                                       MemberId: mj.MemberId,
                                       OrderId: localStorage.OrderId,
                                       VsType: 10
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           //   alert(1);
                                           $("#a_SaveOrderVsMember").hide();
                                           api.sendEvent({
                                               name: 'GetOrderList',
                                               extra: {
                                                   key1: 'value1',
                                                   key2: 'value2'
                                               }
                                           });
                                           api.sendEvent({
                                               name: 'GetOrderToWorkList',  //重新绑定工单
                                               extra: {
                                                   key1: 'value1',
                                                   key2: 'value2'
                                               }
                                           });



                                           api.toast({
                                               msg: '抢单成功,我们会与您联系,请务必保持开机 !',
                                               duration: 2000,
                                               location: 'middle'
                                           });





                                           setTimeout(function () {
                                               api.closeWin();
                                               loading = false;
                                           }, 2000)

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}


function ToPhone(obj) {

    PhoneNow($(obj).attr("Phone"));
}




function OpenApp() {



    if (isWeiXin()) {
        //是微信浏览器
        var OpenImg = "image/guide-android.png";

        if (MobileUA.ANDROID) {


        }
        else if (MobileUA.IOS) {

            OpenImg = "image/guide-ios.png";
        }
        else {


        }

        var h = $(window).height();

        $("body").before("<div onclick='$(this).remove()' class='div_OpenImg'  style='height:" + h + "px; ' ><img src='" + OpenImg + "' class='img_OpenImg' /><div>");

    }
    else {
        //不是微信浏览器
        //  alert(1);
        var the_href = "http://a.app.qq.com/o/simple.jsp?pkgname=com.chuanfou.uzjob";//获得下载链接
        tiaozhuan("uzorjob://?OrderId=" + GetQueryString("OrderId") + "");
        setTimeout(function () {
            window.location.href = the_href;//如果超时就跳转到app下载页
        }, 1000);


    }

}