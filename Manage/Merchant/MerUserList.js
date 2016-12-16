/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../ZYUiPub.js" />

var MerRoleList = [];
var BranchList = [];

$(function () {

    BindPageSetting();
});




function BindPageSetting() {


    BindUserList(1);
    GetUserMerRoleList();

    GetMerRoleList();

}


function GetUserMerRoleList() {
    AjaxPost("/am/", "GetUserMerRole",
                                   {
                                       uid: $("#ul_userList>li.select").eq(0).attr("UserId"),
                                       MerId: localStorage.MerId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];
                                               w.push("<li " + JsonToParaStr(j) + " >");
                                               w.push(j.MerRoleName);

                                               if (j.BranchId == "") {

                                               }
                                               else {
                                                   w.push("(");
                                                   w.push(j.BranchName);
                                                   w.push(")");
                                               }


                                               w.push("");
                                               w.push("");
                                               w.push("</li>");
                                           }
                                           $("#ul_merRoleList").html(w.join(""));

                                           $("#ul_merRoleList>li").each(function () {

                                               RightMenu(this, [
                                                                                           {
                                                                                               evName: "RemoveUserMerRole",
                                                                                               Title: "删除角色",
                                                                                               evIcon: ""

                                                                                           }
                                               ]);
                                           })

                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);

}

function RemoveUserMerRole(obj) {
    var UserId = $(obj).attr("UserId");
    var MerRoleId = $(obj).attr("MerRoleId");
    var BranchId = $(obj).attr("BranchId");

    AjaxPost("/am/", "RemoveUserMerRole",
                                   {
                                       uid: UserId,
                                       MerRoleId: MerRoleId,
                                       BranchId: BranchId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           $(obj).remove();
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);
}

function GetMerRoleList() {
    AjaxPost("/am/", "GetMerRoleList",
                                   {
                                       MerId: localStorage.MerId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           MerRoleList = data.list;
                                           BranchList = data.BranchList;

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);

}

//绑定左侧用户列表
function BindUserList(CurrentPage) {

    AjaxPost("/am/", "GetMerRoleUsers",
     {

         MerId: $.cookie("CurrentMerId"),
         CurrentPage: CurrentPage

     }, function (data) {

         if (data.re == "ok") {

             if (data.list.length == 0) {
                 throw "没有任何用户!";
             }
             var w = new Array();
             for (var i = 0; i < data.list.length; i++) {

                 var j = data.list[i];
                 w.push("<li UserId='" + j.UserId + "'  onclick='LoadUserInfo(this)' >");

                 w.push(j.UserId);
                 w.push("</li>");

             }

             $("#ul_userList").html(w.join(""));
             $("#ul_userList>li").eq(0).click();
         }
         else {

         }


     });
}


function ToAddUser() {
    var w = [];

    AjaxPost("/am/", "GetMerRoleList",
                                   {
                                       MerId: localStorage.MerId
                                   }, function (data) {

                                       if (data.re == "ok") {
                                           w.push("<input id='txt_AddUserId' type='text' value=''  placeholder='用户名' /><br />");
                                           w.push("<div class='clr_10px' ></div>");
                                           w.push("<div class='clr' ></div>");
                                           w.push("<h4>角色选择:</h4>");

                                           w.push("<div class='clr' ></div>");
                                           w.push("<ul id=\"ul_merRoleList2\" class=\"ul_merRoleList\">");
                                           for (var i = 0; i < data.list.length; i++) {

                                               var j = data.list[i];
                                               w.push("<li " + JsonToParaStr(j) + " >" + j.MerRoleName + "</li>");

                                           }


                                           w.push("</ul>");
                                           w.push("<div class='clr' ></div>");
                                           w.push("<h4>分部选择:</h4>");

                                           w.push("<div class='clr' ></div>");
                                           w.push("<ul id=\"ul_Branch\" class=\"ul_merRoleList\">");
                                           for (var i = 0; i < data.BranchList.length; i++) {

                                               var j = data.BranchList[i];
                                               w.push("<li " + JsonToParaStr(j) + " >" + j.BranchName + "</li>");

                                           }


                                           w.push("</ul>");

                                           w.push("<div class='clr' ></div>");
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);


    ZyPopWindow(w.join(""), { 添加用户: "add", 关闭窗口: "cls" }, 400, 300, "AddUser", 100);

    $("#ul_merRoleList2>li,#ul_Branch>li").click(function () {


        $(this).siblings().removeClass("select");
        $(this).addClass("select");
    });


    $("#ul_merRoleList2>li").eq(0).click();
    $("#ul_Branch>li").eq(0).click();
}


function AddUser(m, v) {

    switch (v) {
        case "cls":
            ZyPopClose();
            break;
        case "add":
            AjaxPost("/au/", "AddUser",
                                {
                                    uid: $("#txt_AddUserId").val(),
                                    MerId: $.cookie("CurrentMerId"),
                                    MerRoleId: $("#ul_merRoleList2>li.select").eq(0).attr("MerRoleId"),
                                    BranchId: $("#ul_Branch>li.select").eq(0).attr("BranchId")
                                }, function (data) {
                                    if (data.re == "ok") {


                                        BindUserList();
                                        ZyPopClose();
                                    }
                                    else {

                                        alert(data.re);
                                    }

                                });
            break;

    }


}
function LoadUserInfo(obj) {

    $(obj).siblings().removeClass("select");

    $(obj).addClass("select");

    var uid = $(obj).attr("UserId");

    AjaxPost("/au/", "GetUserData",
        {

            uid: uid

        }, function (data) {

            if (data.re == "ok") {
                var j = data.UserInfo;
                $("#txt_UserId").val(j.UserId);
                $("#txt_RealName").val(j.RealName);
                $("#txt_Email").val(j.Email);
                $("#txt_qq").val(j.qq);
                $("#txt_Pwd").val(j.Pwd);
                $("#txt_tell").val(j.Tell);
                $("#txt_phone").val(j.Phone);
                $("#txt_WxOpenID").val(j.WxOpenID);
                $("#txt_Memo").val(j.Memo);
                GetUserMerRoleList();

            }

        }
        );


}

function SaveMerRoleVsUser() {


    if ($(".div_MerRole>a.select").length != 1) {
        alert("角色必须选择!");
        return;
    }
    if ($(".div_Branch>ul>li.select").length != 1) {
        alert("分部必须选择!");
        return;
    }

    var MerRoleId = $(".div_MerRole>a.select").attr("MerRoleId");
    var uid = $("#ul_userList>li.select").eq(0).attr("UserId")
    var BranchId = $(".div_Branch>ul>li.select").attr("BranchId");



    AjaxPost("/am/", "SaveMerRoleVsUser",
      {

          uid: uid,
          MerRoleId: MerRoleId,
          BranchId: BranchId

      }, function (data) {

          if (data.re == "ok") {
              alert("添加角色成功!");
              //  $(obj).addClass("select");
              GetUserMerRoleList();
          }
          else {
              alert(data.re);
              //    throw data.re;
          }

      });

}



function SaveUser() {
    AjaxPost("/au/", "SaveUser",
                        {


                            uid: $("#txt_UserId").val(),

                            RealName: $("#txt_RealName").val(),
                            Email: $("#txt_Email").val(),
                            Memo: $("#txt_Memo").val(),

                            Pwd: $("#txt_Pwd").val(),

                            Tell: $("#txt_tell").val(),
                            Phone: $("#txt_phone").val(),


                            qq: $("#txt_qq").val()




                        }, function (data) {
                            if (data.re == "ok") {
                                alert("已保存!");


                            }
                            else {

                                alert(data.re);
                            }

                        });

}

function AddUserRole() {




    var w = [];
    w.push("<div class='div_UserRolePop' >");
    w.push("<div class=\"div_abstract\">");
    w.push("<p>");
    w.push("必须同时选择分部名称和角色名才能添加");
    w.push("</p>");
    w.push("</div>");
    w.push("<h4>角色名:</h4>");
    w.push("<div class='div_MerRole' >");

    for (var i = 0; i < MerRoleList.length; i++) {
        var j = MerRoleList[i];
        w.push("<a  MerRoleId= " + j.MerRoleId + " >" + j.MerRoleName + "</a>");

    }


    w.push("</div>")
    w.push("<h4>分部列表:</h4>");
    w.push("<div class='div_Branch' >");
    w.push("<ul>");
    for (var i = 0; i < BranchList.length; i++) {
        var j = BranchList[i];
        w.push("<li  BranchId= " + j.BranchId + " >" + j.BranchName + "</li>");

    }
    w.push("</ul>");
    w.push("</div>")
    w.push("<input type='button' value='添 加' class='btn_addUserRole' onclick='SaveMerRoleVsUser()' />");
    w.push("</div>");


    PopWindow({
        title: "添加新角色",
        html: w.join("")
    }, 500, 500);

    $(".div_MerRole>a").click(function () {

        $(this).siblings().removeClass("select");
        $(this).addClass("select");
    });
    $(".div_Branch>ul>li").click(function () {

        $(this).siblings().removeClass("select");
        $(this).addClass("select");
    });
}