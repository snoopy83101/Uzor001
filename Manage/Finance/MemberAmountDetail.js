/// <reference path="C:\anli\优做\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\优做\Manage\Script/ZYUiPub.js" />

$(function () {

    BindPageSetting();
})


function BindPageSetting() {
    BindDateInput("txt_CreateTime1", GetDateStr(-30));
    BindDateInput("txt_CreateTime2", GetDateStr(1));
    GetMemberDetailPageSetting(1);

}


function GetMemberDetailPageSetting(CurrentPage) {

    AjaxPost("/amb/", "GetMemberDetailPageSetting",
                                             {
                                                 CurrentPage: CurrentPage,
                                                 CreateTime1: $("#txt_CreateTime1").val(),
                                                 CreateTime2: $("#txt_CreateTime2").val()

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     if (data.list.length > 0) {
                                                         for (var i = 0; i < data.list.length; i++) {
                                                             var j = data.list[i];
                                                             w.push("<tr>");
                                                             w.push("<td>");
                                                             w.push(j.OldAmount);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.ChangeAmount);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.NewAmount);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.CreateTime);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.MemberAmountChangeTypeName);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.Phone);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.RealName);
                                                             w.push("</td>");
                                                             w.push("</tr>");

                                                         }
                                                     }


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                                 $("#tb_1").html(w.join(""));
                                          


                                                 ZyPagerSetting("GetMemberDetailPageSetting", CurrentPage, data.t, "1");
                                             }, false);
}