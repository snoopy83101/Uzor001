

/// <reference path="/Script/jquery-1.8.2.js" />
/// <reference path="/Script/ZYUiPub.js" />
/// <reference path="FinancePub.js" />
/// <reference path="/script/common.js" />

$(function () {

    BindPageSetting();

})

function BindPageSetting() {

    GetSubjectCashInfo();
}


function GetSubjectCashInfo() {



    AjaxPost("/amb/", "GetSubjectCashInfo",
                                         {
                                             SubjectCashId: GetQueryString("SubjectCashId")

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {

                                                 $("#sp_SubjectCashStatus").html(data.SubInfo.SubjectCashStatusName);
                                                 $("#sp_Amount").html(ConverttoDecimal2f(data.SubInfo.Amount));
                                                 $("#sp_Member").html(data.MemberInfo.RealName + " " + data.MemberInfo.Phone);
                                                 $("#sp_MemberAddress").html(data.MemberInfo.ProvinceName = " " + data.MemberInfo.CityName + " " + data.MemberInfo.AreaName);

                                                 $("#txt_Memo").html(data.SubInfo.Memo);


                                                 var j = data.BankCardInfo;
                                                 w.push("<a>");
                                                 w.push("<div class=\"c\">");
                                                 w.push("<img src=\"" + BankLogo(j.BankId) + "\" id=\"img_BankImgUrl\"  />");
                                                 w.push("");
                                                 w.push("</div>");
                                                 w.push("<span><b>银行:</b>" + j.BankName + "</span>");
                                                 w.push("<span><b>卡号:</b>" + j.BankCardCode + "</span>");
                                                 w.push("<span><b>开户行:</b>" + j.BankCardAccount + "</span>");
                                                 w.push("<span><b>持卡人:</b>" + j.BankCardName + "</span>");
                                                 w.push("<div class=\"clr_20px\"></div>");
                                                 w.push("<div class=\"c\">");
                                                 if (data.SubInfo.SubjectCashStatusId >= 20 || data.SubInfo.SubjectCashStatusId < 0) {


                                                 }
                                                 else {
                                                     w.push("<input type=\"button\" class=\"\" value=\"确认已转账\"  onclick='AllowSubjectCash(this)' />");

                                                 }


                                                 w.push("</div>");
                                                 w.push("");
                                                 w.push("</a>");



                                                 $("#div_BankCardList").html(w.join(""));


                                             }
                                             else {
                                                 alert(data.re)
                                             }



                                         }, false);



}


function AllowSubjectCash(obj) {

    AjaxPost("/amb/", "AllowSubjectCash",
                                             {
                                                 SubjectCashId: GetQueryString("SubjectCashId")

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     shuaxin();

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                             }, false);

}