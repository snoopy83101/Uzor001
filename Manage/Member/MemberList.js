/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../ZYUiPub.js" />

var CurrentMemberId = 0;
var Order = "";
$(function () {
    SearchMemberList(1);

});

function SearchMemberList(CurrentPage) {
    AjaxPost("/amb/", "SearchMemberList",
                        {
                            MemberCode: $("#txt_MemberCode").val(),
                            MemberId: $("#txt_MemberId").val(),
                            MemberName: $("#txt_MemberName").val(),
                            Phone: $("#txt_MemberPhone").val(),

                            CurrentPage: CurrentPage,
                            HasPhone: ConvertToBool($("#cb_hasPhone").attr("checked")),
                            Order: Order

                        }, function (data) {
                            if (data.re == "ok") {
                                var w = new Array();
                                if (data.list.length > 0) {

                                    for (var i = 0; i < data.list.length; i++) {
                                        var j = data.list[i];

                                        w.push("<tr MemberId='" + j.MemberId + "' Phone='" + j.Phone + "' MemberName='" + j.MemberName + "' Invalid='" + j.Invalid + "' Status='" + j.Status + "'  MemberCode='" + j.MemberCode + "'  ondblclick='ToMemberInfo(this)'  >");
                                        w.push("<td>");
                                        w.push(j.MemberId);
                                        w.push("</td>")
                                        w.push("<td>");
                                        w.push(j.Phone);
                                        w.push("</td>")
                                        w.push("<td>");
                                        w.push(j.RealName);
                                        w.push("</td>");
                                        w.push("<td>");
                                        w.push(j.ProvinceName + " " + j.CityName + " " + j.AreaName + " " + j.Address);
                                        w.push("</td>")
                                        w.push("<td>");
                                        w.push("[" + j.ProcessLvTitle + "]" + j.ProcessLvName);
                                        w.push("</td>");

                                        w.push("<td>");
                                        w.push(j.Memo);
                                        w.push("</td>");
                                        w.push("<td >");
                                        w.push("<span  ");

                                        switch (j.ProcessLvStatusId) {
                                            case 10:

                                                //提交认证

                                                w.push(" class='shanshuo' ");
                                                break;
                                            default:

                                        }

                                        w.push(" >");

                                        w.push(j.ProcessLvStatusName);
                                        w.push("</span>");
                                        w.push("</td>");

                 

                                        w.push("<td >");
                                        w.push("<span  ");

                                        switch (j.AuthenticationStatusId) {
                                            case 10:

                                                //提交认证

                                                w.push(" class='shanshuo' ");
                                                break;
                                            default:

                                        }

                                        w.push(" >");

                                        w.push(j.AuthenticationStatusName);
                                        w.push("</span>");
                                        w.push("</td>");



                                        w.push("<td>");
                                        w.push(DateFormat(j.LastTime, "yyyy/MM/dd"));
                                        w.push("</td>")
                                        w.push("</tr>");
                                    }


                                }
                                $("#tb_memberList>tbody").html(w.join(""));
                                $("#tb_memberList>tbody>tr").each(function () {

                                    RightMenu(this, [
                                              {
                                                  evName: "ToMemberInfo",
                                                  Title: "用户信息(新窗口)",
                                                  evIcon: ""

                                              },


                                                  {
                                                      evName: "DongJieMember",
                                                      Title: "禁止登陆(冻结)",
                                                      evIcon: ""

                                                  }


                                    ]);
                                });
                                ZyPagerSetting("SearchMemberList", CurrentPage, data.t, "1");

                            }
                            else {

                                alert(data.re);
                            }

                        });

}

function DongJieMember(obj) {
    var MemberId = $(obj).attr("MemberId");

    var Status = ConvertToInt($(obj).attr("Status"));
    if (Status < 0) {
        Status = 0
    }
    else {
        Status = -1;
    }



    AjaxPost("/amb/", "ChangeMemberStatus",
                                   {
                                       Status: Status,
                                       MemberId: MemberId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           if (Status >= 0) {
                                               alert("用户已经解除冻结");
                                               $(obj).find(".td_Status").html("正常");
                                           }
                                           else {
                                               alert("用户已经冻结!");
                                               $(obj).find(".td_Status").html("冻结");
                                           }

                                           $(obj).attr("Status", Status);
                                       }
                                       else {
                                           alert(data.re);

                                       }


                                   }, false);

}

//作废用户
function InvalidMember(obj) {
    var MemberId = $(obj).attr("MemberId");
    var Invalid = !ConvertToBool($(obj).attr("Invalid"));
    AjaxPost("/amb/", "InvalidMember",
                                   {
                                       MemberId: MemberId,
                                       Invalid: Invalid
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           if (Invalid) {
                                               alert("用户已经作废!");
                                               $(obj).find(".td_Invalid").html("是");
                                           }
                                           else {
                                               alert("用户已经取消作废!");
                                               $(obj).find(".td_Invalid").html("否");
                                           }
                                           $(obj).attr("Invalid", Invalid);
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);
}

function AddJiFen(obj) {

    CurrentMemberId = $(obj).attr("MemberId");
    Phone = $(obj).attr("Phone");
    MemberName = $(obj).attr("MemberName");
    MemberCode = $(obj).attr("MemberCode");

    var w = [];
    w.push("手机号:" + Phone + "  ");
    w.push("<br />");
    w.push("用户编号:" + MemberCode + "  ");
    w.push("<br />");
    w.push("用户名:" + MemberName + "");
    w.push("<br />");
    w.push("<input id='txt_jifen'  type='text'  value='0' />");
    w.push("<br />");
    w.push("<div class='clr_10px'></div>");
    w.push("<textarea id='txt_memo' placeholder='原因'  ></textarea>");

    ZyPopWindow(w.join(""), { 确认操作: "AddJifen", 关闭窗口: "cls" }, 300, 400, "AddJiFenCallBack", 100);

    $("#txt_jifen").select().focus();

}


function AddJiFenCallBack(m, v) {
    switch (v) {
        case "AddJifen":
            var b = false;

            if (!b) {
                b == true;
                AjaxPost("/am/", "AddJifen",
                                    {
                                        JiFenChangeNum: $("#txt_jifen").val(),
                                        MemberId: CurrentMemberId,
                                        JiFenChangeMemo: $("#txt_memo").val()
                                    }, function (data) {
                                        if (data.re == "ok") {


                                            ZyPopClose();
                                        }
                                        else {

                                            alert(data.re);
                                        }

                                    });

            }

            break;
        default:
            ZyPopClose();
    }
}

function ToMemberInfo(obj) {

    window.open("MemberInfo.aspx?MemberId=" + $(obj).attr("MemberId") + "");

}


function ChangeOrder(obj) {
    var ziduan = $(obj).attr("ziduan");
    var desc = $(obj).attr("desc");

    if ($.trim(desc).toLowerCase() == "desc") {
        desc = "asc";
    }
    else {
        desc = "desc";
    }
    Order = ziduan + " " + desc;
    SearchMemberList(1);

    $(obj).attr("desc", desc);
}