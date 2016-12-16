

/// <reference path="/Script/jquery-1.8.2.js" />
/// <reference path="/Script/ZYUiPub.js" />
/// <reference path="/script/common.js" />

$(function () {

    BindPageSetting();

})

function BindPageSetting() {






    AjaxPost("/amb/", "GetSkillList",
                                         {
                                             CurrentPage: 1,
                                             Invalid: $("#sel_Invalid").val()

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 if (data.list.length > 0) {
                                                     for (var i = 0; i < data.list.length; i++) {
                                                         var j = data.list[i];
                                                         w.push("<tr SkillId='" + j.SkillId + "' Invalid='" + j.Invalid + "' >");
                                                         w.push("<td  >");
                                                         w.push(j.SkillId);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push("<input type='text' class='txt_SkillName'  value='" + j.SkillName + "'  />");
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push("<input type='text'  class='txt_OrderNo' value='" + j.OrderNo + "'  />");
                                                         w.push("</td>");
                                                         w.push("</tr>");

                                                     }

                                                







                                                 }
                                                 $("#tbody_list").html(w.join(""));


                                                 /// <reference path="/Script/jquery-1.8.2.js" />
                                                 /// <reference path="/Script/ZYUiPub.js" />
                                                 /// <reference path="/script/common.js" />

                                                 $("#tbody_list>tr").each(function () {


                                                     var SkillId = $(this).attr("SkillId");
                                                     var Invalid = ConvertToBool($(this).attr("Invalid"));

                                                     if (Invalid) {
                                                         r = [];
                                                         r.push({
                                                             evName: "InvalidSkill",
                                                             Title: "取消作废",
                                                             evIcon: ""
                                                         });
                                                     }
                                                     else {
                                                         r = [];
                                                         r.push({
                                                             evName: "InvalidSkill",
                                                             Title: "作废",
                                                             evIcon: ""
                                                         });
                                                     }



                                                     RightMenu(this, r);


                                                     $(this).find("input[type='text']").blur(function () {


                                                         var trObj = $(this).parent().parent();

                                                         var SkillName = trObj.find(".txt_SkillName").val();



                                                         var OrderNo = trObj.find(".txt_OrderNo").val();

                                                         AjaxPost("/amb/", "SaveSkill",
                                                            {
                                                                SkillId: SkillId,
                                                                SkillName: SkillName,
                                                                OrderNo: OrderNo

                                                            }, function (data) {
                                                                var w = [];
                                                                if (data.re == "ok") {
                                                                    if (data.list.length > 0) {
                                                                        //返回
                                                                    }


                                                                }
                                                                else {
                                                                    alert(data.re)
                                                                }


                                                            }, false);



                                                     })



                                                 })

                                             }
                                             else {
                                                 alert(data.re)
                                             }

                                         }, false);



}




function InvalidSkill(obj) {


    Invalid = ConvertToBool($(obj).attr("Invalid"));

    AjaxPost("/amb/", "InvalidSkill",
                                         {
                                             SkillId: $(obj).attr("SkillId"),
                                             Invalid: !Invalid

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 BindPageSetting();

                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);



}