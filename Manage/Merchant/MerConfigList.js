/// <reference path="../ZYUiPub.js" />
/// <reference path="../jquery-1.8.2.js" />


$(function () {

    GetMerConfigList(1);
});


function PageReady() {

}


function GetMerConfigList(CurrentPage) {


    AjaxPost("/am/", "GetMerConfigList",
                        {
                            MerId: localStorage.MerId
                        }, function (data) {
                            if (data.re == "ok") {

                                if (data.list.length > 0) {
                                    var w = new Array();
                                    for (var i = 0; i < data.list.length; i++) {
                                        var j = data.list[i];
                                        w.push("<tr " + JsonToParaStr(j) + " onclick='PopSaveConfig(this)'  >");
                                        w.push("<th class='th_title' ><span class='sp_MerConfigTitle' >" + j.MerConfigTitle + "</span></th>");
                                        w.push("<td>");
                                        w.push("<span class='sp_MerConfigVal' >" + j.MerConfigVal + "</span>");

                                        w.push("</td>");
                                        w.push("<td>");
                                        w.push("[" + j.MerConfigId + "]");
                                        w.push("<span class='sp_Memo'>");
                                        w.push(j.Memo);
                                        w.push("</span>");
                                        w.push("</td>");
                                        w.push("</tr>");
                                    }


                                    $("#tbody_1").html(w.join(""));


                                    $("#tbody_1>tr").each(function () {

                                        RightMenu(this, [
                                               {
                                                   evName: "PopSaveConfig",
                                                   Title: "更改配置",
                                                   evIcon: ""

                                               },
                                                 {
                                                     evName: "DelMerConfig",
                                                     Title: "删除配置",
                                                     evIcon: ""

                                                 }

                                        ])
                                    });

                                }



                            }
                            else {

                                alert(data.re);
                            }

                        });

}





function PopSaveConfig(obj) {

    var w = [];

    w.push("<table class='t4' >");
    w.push("<tbody>");
    w.push("<tr>");

    w.push("<th>");
    w.push("配置ID");
    w.push("</th>");
    w.push("<td>");
    w.push("<input id='txt_MerConfigId' type='text' value='");
    w.push($(obj).attr("MerConfigId"));
    w.push("' />");
    w.push("</td>");
    w.push("</tr>");

    w.push("<th>");
    w.push("配置名称");
    w.push("</th>");
    w.push("<td>");
    w.push("<input id='txt_MerConfigTitle' type='text' value='");
    w.push($(obj).attr("MerConfigTitle"));
    w.push("' />");
    w.push("</td>");
    w.push("</tr>");


    w.push("<th>");
    w.push("值");
    w.push("</th>");
    w.push("<td>");
    w.push("<input id='txt_MerConfigVal' type='text' value='");
    w.push($(obj).attr("MerConfigVal"));
    w.push("' />");
    w.push("</td>");
    w.push("</tr>");


    w.push("<th>");
    w.push("备注");
    w.push("</th>");
    w.push("<td>");
    w.push("<input id='txt_Memo' type='text' value='");
    w.push($(obj).attr("Memo"));
    w.push("' />");
    w.push("</td>");
    w.push("</tr>");


    w.push("<th>");
    w.push("排序编号");
    w.push("</th>");
    w.push("<td>");
    w.push("<input id='txt_OrderNo' type='text' value='");
    w.push($(obj).attr("OrderNo"));
    w.push("' />");
    w.push("</td>");
    w.push("</tr>");


    w.push("</tbody>");
    w.push("</table>");

    ZyPopWindow(w.join(""), { 保存: "SaveMerConfig", 关闭: "cls" }, 300, 300, "SaveConfig", 20);
}
function SaveConfig(m, v) {
    switch (v) {
        case "cls":
            ZyPopClose();
            break;
        default:

            AjaxPost("/am/", "SaveMerConfig",
                                {
                                    MerId: localStorage.MerId,
                                    MerConfigId: $("#txt_MerConfigId").val(),
                                    MerConfigTitle: $("#txt_MerConfigTitle").val(),
                                    MerConfigVal: $("#txt_MerConfigVal").val(),
                                    Memo: $("#txt_Memo").val(),
                                    OrderNo: $("#txt_OrderNo").val()
                                }, function (data) {
                                    if (data.re == "ok") {

                                        alert("保存配置成功!");
                                        ZyPopClose();

                                        GetMerConfigList(1);


                                    }
                                    else {

                                        alert(data.re);
                                    }

                                });

    }

}

function DelMerConfig(obj) {

    if (!confirm("确定删除配置吗? 这可能造成无法挽回的损失!")) {
        return;
    }


    var MerConfigId = $(obj).attr("MerConfigId");



    AjaxPost("/am/", "DelMerConfig",
                        {

                            MerId: localStorage.MerId,
                            MerConfigId: MerConfigId


                        }, function (data) {
                            if (data.re == "ok") {

                                $(obj).remove();

                            }
                            else {

                                alert(data.re);
                            }

                        });

}