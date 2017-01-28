/// <reference path="C:\anli\快工场\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Manage\Script/ZYUiPub.js" />


$(function () {

    BindPageSetting();
});


function BindPageSetting() {
    BindDateInput("txt_CreateTime1", GetDateStr(-30));
    BindDateInput("txt_CreateTime2", GetDateStr(1));
    GetSubjectCashList(1);
}

function GetSubjectCashList(CurrentPage) {

    AjaxPost("/amb/", "GetSubjectCashList",
                                             {
                                                 CurrentPage: CurrentPage,
                                                 dtm1: $("#txt_CreateTime1").val(),
                                                 dtm2: $("#txt_CreateTime2").val()
                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     if (data.list.length > 0) {
                                                         for (var i = 0; i < data.list.length; i++) {
                                                             var j = data.list[i];
                                                             w.push("<tr  SubjectCashId='" + j.SubjectCashId + "' SubjectCashStatusId='" + j.SubjectCashStatusId + "'  >");
                                                             w.push("<td>");
                                                             w.push(j.BankCardCode + " " + j.BankCardName + " " + j.BankName);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.Amount);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.SubjectCashStatusName);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.RealName + " " + j.Phone);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.CreateTime);
                                                             w.push("</td>");

                                                             w.push("</tr>");

                                                         }
                                                     }


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                                 $("#t_1").html(w.join(""));
                                                 ZyPagerSetting("GetSubjectCashList", CurrentPage, data.t, "1");


                                                 $("#t_1>tr").each(function () {
                                                  
                                                     $(this).dblclick(function () {

                                                         var SubjectCashId = $(this).attr("SubjectCashId");


                                                         window.open("SubjectCashInfo.aspx?SubjectCashId=" + SubjectCashId + "");

                                                     });
                                                 

                                                     r = [];
                                                     var SubjectCashStatusId = ConvertToInt($(this).attr("SubjectCashStatusId"));

                                                     if (SubjectCashStatusId < 0) {


                                                     }
                                                     else if (SubjectCashStatusId == 10) {

                                                         //等待处理
                                                         //r.push({
                                                         //    evName: "AllowSubjectCash",
                                                         //    Title: "通过申请",
                                                         //    evIcon: ""
                                                         //});
                                                         r.push({
                                                             evName: "RefuseSubjectCash",
                                                             Title: "打回申请",
                                                             evIcon: ""
                                                         });



                                                     }
                                                     else if (SubjectCashStatusId == 20) {


                                                     }
                                                     RightMenu(this, r);



                                                 });



                                             }, false);
}




function RefuseSubjectCash(obj) {
    var SubjectCashId = $(obj).attr("SubjectCashId");
    AjaxPost("/amb/", "RefuseSubjectCash",
                                             {
                                                 SubjectCashId: SubjectCashId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     GetSubjectCashList(1);

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                             }, false);

}