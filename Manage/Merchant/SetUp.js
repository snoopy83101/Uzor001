/// <reference path="C:\anli\快工场\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Manage\Script/ZYUiPub.js" />


$(function () {
    BindPageSetting();
});

function BindPageSetting() {

    GetMerRoleVsMsgType();
}

function GetMerRoleVsMsgType() {


    var MerRoleId = localStorage.MerRoleId
    AjaxPost("/au/", "GetMerRoleVsMsgType",
                                             {
                                                 uid: localStorage.UserId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     var w = [];
                                                     if (data.list.length > 0) {


                                                         for (var i = 0; i < data.list.length; i++) {

                                                             var j = data.list[i];


                                                             w.push("<a id='a_" + j.MsgTypeId + "'  MsgTypeId='" + j.MsgTypeId + "' class='a_MsgType' >");
                                                             w.push(j.MsgTypeName);
                                                             w.push("</a>");
                                                         }


                                                     }


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }
                                                 $("#div_MsgType").html(w.join(""));
                                                 GetDevicePush();


                                             }, true);

}



function GetDevicePush() {
    AjaxPost("/amsg/", "GetDevicePush",
                                             {
                                                 DeviceId: localStorage.UserId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     if (data.list.length > 0) {
                                                         for (var i = 0; i < data.list.length; i++) {
                                                             var j = data.list[i];
                                                             $("#a_" + j.MsgTypeId + "").addClass("select");

                                                         }
                                                     }
                                                     BindDevicePushEvent();

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }



                                             }, false);
}


function BindDevicePushEvent() {

    $(".a_MsgType").click(function () {

        var b = $(this).hasClass("select");


        var PushType = $(this).attr("MsgTypeId");
        var aobj = this;
        if (b) {
            //如果已经有了. 删除


            AjaxPost("/amsg/", "RemoveDevicePush",
                                                     {
                                                         DeviceId: localStorage.UserId,
                                                         PushType: PushType

                                                     }, function (data) {
                                                         var w = [];
                                                         if (data.re == "ok") {
                                                             $(aobj).removeClass("select");


                                                         }
                                                         else {
                                                             alert(data.re)
                                                         }


                                                     }, false);

        }
        else {

            //如果没有, 新增

            AjaxPost("/amsg/", "SaveDevicePush",
                                                     {
                                                         DeviceId: localStorage.UserId,
                                                         PushType: PushType

                                                     }, function (data) {
                                                         var w = [];
                                                         if (data.re == "ok") {
                                                             $(aobj).addClass("select");


                                                         }
                                                         else {
                                                             alert(data.re)
                                                         }


                                                     }, false);
        }


    });

}


