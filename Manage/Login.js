/// <reference path="C:\anli\快工场\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Manage\Script/ZYUiPub.js" />


$(function () {
    ChangeTitle();
    BindList();
});



function ChangeTitle() {


    ConvertToBool("");

}

function thsidss() {

    $("").attr("dsf");

    $("").addClass("");

    ConvertToBool("");


}
function BindList() {

    $("#txt_UserPwd").keyup(function () {
        if (event.keyCode == 13) {
            var v = $(this).val();
            if ($.trim(v) != "") {
                $("#a_ckLogin").click();
            }
        }



    });

    if (localStorage["LoginStr"] != null) {

        $("#txt_inputStr").val(localStorage["LoginStr"]);
    }
    else {


    }


    if (localStorage["Pwd"] != null) {

        $("#txt_UserPwd").val(localStorage["Pwd"]);
    }
    else {


    }




}


function ToLogin() {

    AjaxPost("/au/", "UserLogin",
        {
            inputStr: $("#txt_inputStr").val(),
            Pwd: $("#txt_UserPwd").val()
        }, function (data) {

            if (data.re == "ok") {

                var Remeber = ConvertToBool($("#cb_Remeber").attr("checked"));
                try {

                    if (Remeber) {
                        localStorage["LoginStr"] = $("#txt_inputStr").val();
                        localStorage["Pwd"] = $("#txt_UserPwd").val();
                    }
                    else {
                        localStorage["LoginStr"] = "";
                        localStorage["Pwd"] = "";

                    }

                } catch (e) {

                }


                if (data.MerRoleList.length > 0) {

                    var w = new Array();
                    w.push("请选择您要登陆的角色:");
                    for (var i = 0; i < data.MerRoleList.length; i++) {
                        var j = data.MerRoleList[i];

                        w.push("<dd title ='" + j.MerRoleMemo + "'  MerId='" + j.MerId + "' MerRoleId='" + j.MerRoleId + "'  MerRoleName='" + j.MerRoleName + "' UserId='" + j.UserId + "' MerchantName='" + j.MerchantName + "' BranchId='" + j.BranchId + "' onclick='MerRoleSelected(this)' >");
                        w.push(j.MerchantName);
                        w.push("-");
                        w.push(j.MerRoleName);
                        if (j.BranchId != "") {
                            w.push("(" + j.BranchName + ")");
                        }
                        w.push("</dd>");



                    }
                    $("#div_Login").hide();
                    $("#dl_selMer").html(w.join(""));

                    $("#div_selMer").show(500);

                }
                else {
                    throw "您没有角色吗?";

                }




            }
            else {
                localStorage["LoginStr"] = "";
                localStorage["Pwd"] = "";
                alert(data.re);
            }

        });


}

function Exit() {
    shuaxin();
}

function MerRoleSelected(obj) {
    localStorage.MerRoleId = $(obj).attr("MerRoleId");
    localStorage.MerName = $(obj).attr("MerchantName");
    localStorage.MerId = $(obj).attr("MerId");
    localStorage.MerRoleName = $(obj).attr("MerRoleName");
    localStorage.UserId = $(obj).attr("UserId");
    localStorage.BranchId = $(obj).attr("BranchId");


    LoginCookie();
    tiaozhuan("/Main");
}