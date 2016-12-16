/// <reference path="C:\anli\优做\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\优做\Manage\Script/ZYUiPub.js" />




var LoadingDingDan = false;

function alertCookie() {
    return;
    alert("Main:" + $.cookie("CurrentUserId") + "-" + $.cookie("CurrentMerId") + "localstroe" + localStorage.UserId);
}

$(function () {
    BindTopSetting();

    $("#img_logo").attr("src", localStorage.Logo);
    $("#img_logo").show();
    $("#footer").html(localStorage.FooterTitle);
    $("body").keyup(function () {

        if (event.keyCode == 81 && event.ctrlKey) {
            ShowKf();
        }
    });

    BindPageSetting();
    alertCookie();
    $("#ul_menu1>li").eq(0).click();

    var h = $("body").height();

    $("#ifm_sub,#ifm_kf,#ifm_jk").height(h - 150);
    $("#ifm_kf,#ifm_jk").width($("#ifm_sub").width() - 100);
    //  ReCurrentWxPt();
    // ReCurrentQyWxPt();
    document.title = localStorage.MerRoleName + " - " + localStorage.MerName;

    alertCookie();


});

function toLoadingDingDan() {

    LoadingDingDan = false;
}

function ToLoginOut() {

    tiaozhuan("Login");
}


function BindPageSetting() {



    var w = new Array();



    w.push("<li class=\"nav-header\">");
    w.push("<div class=\"dropdown profile-element\">");
    w.push("<span>");
    w.push("<img alt=\"image\" class=\"img-circle\" src=\"./Images/logo1999.png\" style='width:60px;'></span>");
    w.push("<a data-toggle=\"dropdown\" class=\"dropdown-toggle\">");
    w.push("<span class=\"clear\">");
    w.push("<span class=\"block m-t-xs\"><strong class=\"font-bold\">" + localStorage.UserId + "</strong></span>");
    w.push("<span class=\"text-muted text-xs block\">" + localStorage.MerRoleName + "<b class=\"caret\"></b></span>");
    w.push("</span>");
    w.push("</a>");
    w.push("<ul class=\"dropdown-menu animated fadeInRight m-t-xs\">");
    w.push("<li><a class=\"J_menuItem\" data-index=\"0\">修改头像</a>");
    w.push("</li>");
    w.push("<li><a class=\"J_menuItem\" data-index=\"1\">个人资料</a>");
    w.push("</li>");
    w.push("<li><a class=\"J_menuItem\"  data-index=\"2\">联系我们</a>");
    w.push("</li>");
    w.push("<li><a class=\"J_menuItem\" data-index=\"3\">信箱</a>");
    w.push("</li>");
    w.push("<li class=\"divider\"></li>");
    w.push("<li><a  onclick='ToLoginOut()' >安全退出</a>");
    w.push("</li>");
    w.push("</ul>");
    w.push("</div>");
    w.push("<div class=\"logo-element\">");
    w.push("H+");
    w.push("");
    w.push("</div>");
    w.push("</li>");

    for (var i = 0; i < MenuJson.length; i++) {

        var j = MenuJson[i];
        w.push("<li>");
        w.push("<a id='a_" + j.MenuId + "' >");
      //  w.push("<i class=\"fa fa-home\"></i>");
        w.push("<span class=\"nav-label\">" + j.MenuName + "</span>");
       // w.push("<span class=\"fa arrow\"></span>");
        w.push("</a>");

        var jMenus = ConvertToJson(j.Menus);

        if (jMenus.length > 0) {
            w.push("<ul class=\"nav nav-second-level collapse\" aria-expanded=\"false\" style=\"height: 0px;\">");
            for (var x = 0; x < jMenus.length; x++) {

                var y = jMenus[x];
                w.push("<li>");
                w.push("<a id='a_" + y.MenuId + "'  class=\"J_menuItem\"   onclick='OpenMenu(this)'   url=\"" + y.Url + "\" data-index=\"0\">" + y.MenuName + "</a>");

                w.push("</li>");


            }
            w.push("</ul>");
        }

        w.push("</li>");



        //w.push("<li i='" + i + "' onclick='LoadMenu2(this)' >");
        //w.push("<a class='a_menu1'>");
        //w.push("<span  class='sp_menu1'>");
        //w.push(j.MenuName);
        //w.push("</span>");
        //w.push("</a>");
        //w.push("</li>");


    }





    $("#side-menu").html(w.join(""));
    $('#side-menu').metisMenu();



    GetRongToken(); //融云

}

function OpenMenu(obj) {

    var url = $(obj).attr("url") + "?MerId=" + localStorage.MerId + "";

    if (url != "") {

        $("#ifm_info").attr("src", url);
    }


}






//#endregion



//清理融云的未读消息
function ClearRongMsg(sendRongUserId) {
    alert(sendRongUserId);
    //   var is_clear = RongIMClient.getInstance().clearMessagesUnreadStatus(RongIMClient.ConversationType.PRIVATE,
    //sendRongUserId);
    //    return is_clear;

}


function OpenWindow(obj) {

    ZyPopWindow("<center  ><h1>正在努力为您准备页面!</h1><br /><img src='/images/1G22RL3-2.gif' /> </center>", {}, 400, 200, "", 100);

    var MenuId = $(obj).attr("MenuId");

    var url = $(obj).attr("url");










    $("#a_menu2").html($(obj).text());
    // ifm_sub.Document.cookie = "CurrentUserId="+localStorage.UserId+"";
    $("#ifm_sub").attr("src", url);

}

function AppPoolRecycle() {

    AjaxPost("ac", "AppPoolRecycleByMerId",
                        {

                            MerId: CMerId

                        }, function (data) {
                            if (data.re == "ok") {

                                alert("缓存已经清理!");

                            }
                            else {

                                alert(data.re);
                            }

                        });
}

// #region 客服

var KfShowing = false;

var KfShow = true;
function ShowKf() {

    if (KfShowing == true) {
        return;
    }

    KfShowing = true;
    setTimeout(function () {
        KfShowing = false;
    }, 700);

    if (KfShow) {
        KfShow = false;
        $("#ifm_kf").animate({ left: "100px" });
    }
    else {
        KfShow = true;


        $("#ifm_kf").animate({ left: "2000px" });
    }




}

// #endregion


// #region 接口


var JkShowing = false;

var JkShow = true;
function ShowJk() {

    if (JkShowing == true) {
        return;
    }

    JkShowing = true;
    setTimeout(function () {
        JkShowing = false;
    }, 700);

    if (JkShow) {
        JkShow = false;
        $("#ifm_jk").animate({ left: "100px" });
    }
    else {
        JkShow = true;


        $("#ifm_jk").animate({ left: "2000px" });
    }




}


// #endregion


function GetRongToken(fun) {

    AjaxPost("/amb/", "GetRongToken",
                                     {
                                         RongUserId: localStorage.UserId,
                                         MemberId: -1,
                                         DeviceHardwareId: "客服电脑",
                                         RongName: localStorage.UserId,
                                         uid: localStorage.UserId,
                                         MerId: localStorage.MerId
                                     }, function (data) {
                                         var w = [];

                                         if (data.re == "ok") {
                                             GetTopMsgList();

                                             ShowMsgNum(data.MsgNum);

                                             // #region 融云绑定


                                             RongIMClient.init(data.RongAppKey);

                                             // 设置连接监听状态 （ status 标识当前连接状态）
                                             // 连接状态监听器
                                             RongIMClient.setConnectionStatusListener({
                                                 onChanged: function (status) {
                                                     switch (status) {
                                                         //链接成功
                                                         case RongIMLib.ConnectionStatus.CONNECTED:
                                                             console.log('链接成功');
                                                             break;
                                                             //正在链接
                                                         case RongIMLib.ConnectionStatus.CONNECTING:
                                                             console.log('正在链接');
                                                             break;
                                                             //重新链接
                                                         case RongIMLib.ConnectionStatus.DISCONNECTED:
                                                             console.log('断开连接');
                                                             break;
                                                             //其他设备登录
                                                         case RongIMLib.ConnectionStatus.KICKED_OFFLINE_BY_OTHER_CLIENT:
                                                             console.log('其他设备登录');
                                                             break;
                                                             //网络不可用
                                                         case RongIMLib.ConnectionStatus.NETWORK_UNAVAILABLE:
                                                             console.log('网络不可用');
                                                             break;
                                                     }
                                                 }
                                             });

                                             // 消息监听器
                                             RongIMClient.setOnReceiveMessageListener({
                                                 // 接收到的消息
                                                 onReceived: function (message) {
                                                     // 判断消息类型

                                                     if (message.content.extra.MsgNum) {
                                                         var MsgNum = message.content.extra.MsgNum;


                                                         ShowMsgNum(MsgNum);


                                                       //  ReadStr(message.content.extra.MsgTypeName);

                                                         ReadStr("叮咚");

                                                     }
                                                     else {


                                                     }

                                                     //switch (message.messageType) {
                                                     //    case RongIMClient.MessageType.TextMessage:
                                                     //        // 发送的消息内容将会被打印
                                                     //        console.log(message.content.content);
                                                     //        break;
                                                     //    case RongIMClient.MessageType.VoiceMessage:
                                                     //        // 对声音进行预加载                
                                                     //        // message.content.content 格式为 AMR 格式的 base64 码
                                                     //        RongIMLib.RongIMVoice.preLoaded(message.content.content);
                                                     //        break;
                                                     //    case RongIMClient.MessageType.ImageMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.DiscussionNotificationMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.LocationMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.RichContentMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.DiscussionNotificationMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.InformationNotificationMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.ContactNotificationMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.ProfileNotificationMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.CommandNotificationMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.CommandMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    case RongIMClient.MessageType.UnknownMessage:
                                                     //        // do something...
                                                     //        break;
                                                     //    default:
                                                     //        // 自定义消息
                                                     //        // do something...
                                                     //}
                                                 }
                                             });

                                             RongIMClient.connect(data.token, {
                                                 onSuccess: function (userId) {
                                                     console.log("Login successfully." + userId);
                                                 },
                                                 onTokenIncorrect: function () {
                                                     console.log('token无效');
                                                 },
                                                 onError: function (errorCode) {
                                                     var info = '';
                                                     switch (errorCode) {
                                                         case RongIMLib.ErrorCode.TIMEOUT:
                                                             info = '超时';
                                                             break;
                                                         case RongIMLib.ErrorCode.UNKNOWN_ERROR:
                                                             info = '未知错误';
                                                             break;
                                                         case RongIMLib.ErrorCode.UNACCEPTABLE_PaROTOCOL_VERSION:
                                                             info = '不可接受的协议版本';
                                                             break;
                                                         case RongIMLib.ErrorCode.IDENTIFIER_REJECTED:
                                                             info = 'appkey不正确';
                                                             break;
                                                         case RongIMLib.ErrorCode.SERVER_UNAVAILABLE:
                                                             info = '服务器不可用';
                                                             break;
                                                     }
                                                     console.log(errorCode);
                                                 }
                                             });


                                             // #endregion

                                         }
                                         else {

                                             alert(data.re);
                                         }
                                     })








}

function ShowMsgNum(MsgNum) {

    var i = ConvertToInt(MsgNum);


    if (i > 0) {
        $("#sp_MsgNum,#sp_MsgNum2").html(i);
        $("#sp_MsgNum").show();
    }
    else {
        $("#sp_MsgNum,#sp_MsgNum2").html(i);
        $("#sp_MsgNum").hide();
    }


}
//右上角消息按钮点击时
function MsgClick() {

    var b = $("#right-sidebar").hasClass("sidebar-open");
    if (b) {

        //打开
        GetTopMsgList();
    }
    else {
        //关闭
        //       alert("关闭?")
    }


}


function GetTopMsgList() {

    AjaxPost("/amsg/", "GetTopMsgList",
                                             {
                                                 top: 8,
                                                 TargetDeviceId: localStorage.UserId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     ShowMsgNum(data.MsgNum);

                                                     if (data.list.length > 0) {
                                                         for (var i = 0; i < data.list.length; i++) {
                                                             var j = data.list[i];
                                                             w.push("<div class=\"sidebar-message\"  rd='" + j.rd + "' >");
                                                             w.push("<a>");
                                                             //w.push("<div class=\"pull-left text-center\">");
                                                             //w.push("<img alt=\"image\" class=\"img-circle message-avatar\" src=\"./Frame/a1.jpg\">");
                                                             //w.push("");
                                                             //w.push("<div class=\"m-t-xs\">");
                                                             ////w.Append("<i class=\"fa fa-star text-warning\">11</i>");
                                                             ////w.Append("<i class=\"fa fa-star text-warning\">22</i>");
                                                             //w.push("</div>");
                                                             //w.push("</div>");

                                                             w.push("<h3>");

                                                             // #region 未读消息

                                                             if (!ConvertToBool(j.rd)) {

                                                                 w.push("<span class='sp_new'>New</span> ");


                                                             }

                                                             w.push("" + j.MsgTypeName + "</h3>");
                                                             // #endregion
                                                             w.push("<div class=\"media-body\">");
                                                             w.push("<span class='sp_msg' ></span>");
                                                             w.push("" + j.MsgTitle + "");
                                                             w.push("");
                                                             w.push("<br>");
                                                             w.push("<small class=\"text-muted\">" + DateFormat(j.CreateTime, "MM-dd hh:mm:ss") + "</small>");
                                                             w.push("</div>");
                                                             w.push("</a>");
                                                             w.push("</div>");

                                                         }
                                                     }


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                                 $("#div_MsgList").html(w.join(""));

                                             }, true);

}


function ReadAllMsg() {
    AjaxPost("/amsg/", "ReadAllMsg",
                                             {
                                                 DeviceId: localStorage.UserId
                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     GetTopMsgList();

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);

}


function NewDingDanPush() {

    //AjaxPost("/am/", "NewDingDanPush",
    //                               {
    //                                   MerId: localStorage.MerId,
    //                                   BranchId: localStorage.BranchId
    //                               }, function (data) {

    //                               }, true);
}


function BodyTip(o) {

    if (!o.s) {
        o.s = 2000;
    }
    var Rand = RndNum(4);
    var id = "sp_tip" + Rand + "";

    var w = [];
    w.push("<span id='" + id + "' class='sp_tip' >");
    w.push(o.text);

    w.push("</span>");
    $("body").append(w.join(""));

    $("#" + id + "").show(300);

    setTimeout(function () {

        $("#" + id + "").remove();


    }, o.s)






}

//绑定顶部导航  坑
function BindTopSetting() {



    $("#li_shuaxin").click(function () {
        var src = ifm_info.contentWindow.location.href;


        $("#ifm_info").attr("src", src);
        //$("#ifm_info").attr("src", src);
    });
};