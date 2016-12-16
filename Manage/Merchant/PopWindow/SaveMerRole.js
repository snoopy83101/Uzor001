/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../ZYUiPub.js" />

$(function () {

    BindPageSetting();
});


function BindPageSetting() {

    if (GetQueryString("MerRoleId") != "") {
            
        $("#txt_MerRoleName").val(MerRoleJson.MerRoleName);
        $("#txt_MerRoleMemo").val(MerRoleJson.MerRoleMemo);
        $("#txt_Power").val(MerRoleJson.Power);
    }
}


function SaveMerRole() {

    AjaxPost("/am/", "SaveMerRole",
        {
            MerRoleId: GetQueryString("MerRoleId"),
            MerRoleName: $("#txt_MerRoleName").val(),
            MerRoleMemo: $("#txt_MerRoleMemo").val(),
            Power: $("#txt_Power").val()

        }, function (data) {

            if (data.re == "ok") {

                alert("保存成功!");


                window.parent.shuaxin();
            } else {
                alert(data.re);

            }

        });
}