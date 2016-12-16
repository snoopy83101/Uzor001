/// <reference path="C:\anli\快工场\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Manage\Script/ZYUiPub.js" />


$(function () {
    BindPageSetting();

});

function BindPageSetting() {
    BindTab();
    $(".ul_MerRole>li").click(function () {

        $(this).siblings().removeClass("select");
        $(this).addClass("select");

    });

    BindMenuEvent();
    BindMsgTypeEvent();

}


function BindMenuEvent() {

    $("#ul_MerRole>li").each(function () {


        //绑定左键事件
        $(this).click(function () {

            $(this).addClass("select");

            GetMenuVsMerRole(this);


            GetMerRoleVsMsgType();
        });

        //绑定右键事件
        RightMenu(this, [
            { Title: "人员管理", evName: "PopUserSetting" },
            { Title: "编辑角色", evName: "PopMerRole" }
        ])


    });



    //sdfdsdsddsdsds
    //$("#div_RoleMenu>dl>dt,,#div_AppRoleMenu>dl>dt").each(function () {



    //    var cbObj = $(this).find(".cb_sel").eq(0);


    //    cbObj.click(function () {

    //        var b = ConvertToBool(cbObj.attr("checked"));

    //        var dlObj = $(this).parent().parent();
    //        switch (b) {

    //            case true:

    //                dlObj.find("dd>.cb_sel").attr("checked", true);

    //                break;

    //            case false:
    //                dlObj.find("dd>.cb_sel").attr("checked", false);
    //                break;

    //        }



    //    });





    //});





    //$("#div_RoleMenu>dl>dd,#div_AppRoleMenu>dl>dd").each(function () {


    //    var cbObj = $(this).find(".cb_sel").eq(0);

    //    cbObj.click(function () {

    //        var b = ConvertToBool(cbObj.attr("checked"));

    //        var dlObj = $(this).parent().parent();
    //        switch (b) {

    //            case true:

    //                dlObj.find("dt>.cb_sel").attr("checked", true);

    //                break;

    //            case false:

    //                break;

    //        }

    //    });

    //});


}


function PopMerRole(obj) {

    var url = "";
    if (obj == null) {
        url = "popWindow/SaveMerRole.aspx";
    }
    else {

        var MerRoleId = $(obj).attr("MerRoleId");
        url = "popWindow/SaveMerRole.aspx?MerRoleId=" + MerRoleId + "";
    }

    OpIfmWindow("ifm", url, {}, 500, 240, "", 50);
}


//取得当前角色的权限,并赋值
function GetMenuVsMerRole(obj) {
    $(".cb_sel").attr("checked", false);
    var MerRoleId = $(obj).attr("MerRoleId");
    AjaxPost("/am/", "GetMenuVsMerRole",

        {
            MerRoleId: MerRoleId


        }, function (data) {

            if (data.list.length > 0) {

                //这个人还是有权限的
                for (var i = 0; i < data.list.length; i++) {

                    var j = data.list[i];

                    $("#div_RoleMenu>dl").find("dd,dt").each(function () {

                        var MenuId = $(this).attr("MenuId");

                        if (ConvertToString(j.MenuId) == MenuId.toString()) {

                            var cb = $(this).find("input[type='checkbox']");
                            cb.attr("checked", true);

                        }

                    });


                }


            }

            //移动端开始


            if (data.applist.length > 0) {

                //这个人还是有权限的
                for (var i = 0; i < data.applist.length; i++) {

                    var j = data.applist[i];

                    $("#div_AppRoleMenu>dl").find("dd,dt").each(function () {

                        var AppMenuId = $(this).attr("AppMenuId");

                        if (ConvertToString(j.AppMenuId) == AppMenuId.toString()) {

                            var cb = $(this).find("input[type='checkbox']");
                            cb.attr("checked", true);

                        }

                    });


                }


            }
        });

}

function SaveMenuPower() {

    var MerRoleId = $("#ul_MerRole>li.select").attr("MerRoleId");

    //开始PC端
    var MenuIdArray = [];

    $("#div_RoleMenu").find(".cb_sel:checked").each(function () {


        var MenuId = $(this).parent().attr("MenuId");
        MenuIdArray.push({

            MenuId: MenuId


        })

    });



    //开始移动端
    var AppMenuIdArray = [];

    $("#div_AppRoleMenu").find(".cb_sel:checked").each(function () {


        var AppMenuId = $(this).parent().attr("AppMenuId");
        AppMenuIdArray.push({

            AppMenuId: AppMenuId


        })

    });



    AjaxPost("/am/", "SaveMenuPower",
        {

            MerRoleId: MerRoleId,
            MenuSelectedArray: JArrayToXmlStr(MenuIdArray),
            AppMenuSelectedArray: JArrayToXmlStr(AppMenuIdArray)


        }, function (data) {

            if (data.re == "ok") {
                alert("权限成功保存!");
            }
            else {
                alert(data.re);
            }


        });


    SaveMsgTypeVsMerRole();

}


function GetMerRoleVsMsgType() {

    $(".a_MsgType").removeClass("select");
    var MerRoleId = $("#ul_MerRole>li.select").attr("MerRoleId");
    AjaxPost("/au/", "GetMerRoleVsMsgType",
                                             {
                                                 MerRoleId: MerRoleId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {


                                                     if (data.list.length > 0) {


                                                         for (var i = 0; i < data.list.length; i++) {

                                                             var j = data.list[i];


                                                             $("#a_" + j.MsgTypeId + "").addClass("select");


                                                         }


                                                     }


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, true);

}



function BindMsgTypeEvent() {

    $(".a_MsgType").click(function () {

        var b = $(this).hasClass("select");

        if (b) {
            $(this).removeClass("select");
        }
        else {
            $(this).addClass("select");
        }


    });
}
function SaveMsgTypeVsMerRole() {

    var MerRoleId = $("#ul_MerRole>li.select").attr("MerRoleId");


    var MsgTypeArray = [];

    $(".a_MsgType").each(function () {

        var b = $(this).hasClass("select");

        if (b) {
            MsgTypeArray.push({
                MsgTypeId: $(this).attr("MsgTypeId")
            });
        }
        else {
            $(this).addClass("select");
        }


    });




    AjaxPost("/au/", "SaveMsgTypeVsMerRole",
                                             {
                                                 MerRoleId: MerRoleId,
                                                 MsgTypeArray: JArrayToXmlStr(MsgTypeArray)

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     GetMerRoleVsMsgType();



                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);

}