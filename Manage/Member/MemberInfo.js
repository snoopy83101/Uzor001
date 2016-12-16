/// <reference path="/Script/jquery-1.8.2.js" />
/// <reference path="/Script/ZYUiPub.js" />
/// <reference path="/script/common.js" />









var MemberId = 0;
var MemberInfo = {};
$(function () {

    BindTab();
    MemberId = ConvertToInt(GetQueryString("MemberId", "0"));

    BindPageSetting();
});

function BindPageSetting() {


    GetProcessLv();
    GetMemberInfo();
    GetMemberAmountDetail(1);
    MemberVsPlateVsFabric();
    GetMyTeamMemberList();
    GetOrderToWorkPageList(1);
}




function GetProcessLv() {
    AjaxPost("/am/", "GetProcessLv",
                                  {
                                      CurrentPage: 1
                                  }, function (data) {
                                      var w = [];
                                      if (data.re == "ok") {

                                          w.push("<option value='0' >未认证</option>");

                                          for (var i = 0; i < data.list.length; i++) {
                                              var j = data.list[i];
                                              w.push("<option value='" + j.ProcessLvId + "' >");
                                              w.push(j.ProcessLvTitle + "(" + j.ProcessLvName + ")");
                                              w.push("</option>");

                                          }
                                      }
                                      else {
                                          alert(data.re)
                                      }

                                      $("#sel_ProcessLv").html(w.join(""));

                                  }, false);
}




function GetMemberInfo() {
    AjaxPost("/amb/", "GetMemberInfo",
                                   {
                                       MemberId: MemberId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           MemberInfo = data;
                                           BindAddress();
                                           document.title = "用户信息:" + data.info.Phone + "";

                                           $("#img_PicImg").attr("src", data.info.PicImgUrl);
                                           $("#img_PicImg").attr("PicImgId", data.info.PicImgId);
                                           $("#txt_RealName").val(data.info.RealName);
                                           $("#txt_Phone").val(data.info.Phone);
                                           $("#sel_ProcessLv").val(data.info.ProcessLvId);
                                           $("#txt_SfzNo").val(data.info.SfzNo);
                                           $("#img_SfzImg1").attr("src", data.info.SfzImgUrl1);
                                           $("#img_SfzImg2").attr("src", data.info.SfzImgUrl2);
                                           $("#txt_Address").val(data.info.Address);
                                           $("#b_Amount").html(data.info.Amount);
                                           $("#sel_Sex").val(data.info.Sex);

                                           $("#sp_MaxOrderPlanningTime").html(data.info.MaxOrderPlanningTime);
                                           //$("#txt_").val(data.info.);
                                           //$("#txt_").val(data.info.);
                                           //$("#txt_").val(data.info.);
                                           //$("#txt_").val(data.info.);
                                           //$("#txt_").val(data.info.);
                                           //$("#txt_").val(data.info.);


                                           $("#sel_ProcessLvStatus").val(data.info.ProcessLvStatusId);

                                           if (data.info.TeamId == 0) {
                                               $("#b_tab_Team").hide();

                                           }
                                           else {

                                               $("#b_tab_Team").show();

                                           }
                                           if (data.Skill.length > 0) {

                                               for (var i = 0; i < data.Skill.length; i++) {

                                                   var j = data.Skill[i];

                                                   w.push("<span id='sp_Skill_" + j.SkillId + "' >" + j.SkillName + "</span>");


                                               }

                                               $("#div_Skill").html(w.join(""));
                                           }
                                           w = [];
                                           if (data.info.ProcessLvStatusId == 10) {//用户申请了技能认证


                                               w.push("<input type='button' value='通过技能认证' onclick='AcceptProcessLv()'  />");

                                               $("#sel_ProcessLv").after(w.join(""));

                                           }


                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);

}








function BindAddress() {


 //   alert(MemberInfo.info.AreaId)

    if (MemberInfo.info.AreaId == "") {
        GetProvince($("#div_ssx")[0], true);
    }
    else {
        GetAddressByAreaId($("#div_ssx")[0], MemberInfo.info.AreaId);
    }






}

//通过认证
function AcceptProcessLv() {
    AjaxPost("/amb/", "AcceptProcessLv",
                                   {
                                       MemberId: MemberId,
                                       ProcessLvId: $("#sel_ProcessLv").val()
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           alert("认证通过");

                                           shuaxin();
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);

}


function SaveMember() {
    AjaxPost("/amb/", "SaveMember",
                                   {
                                       MemberId: MemberId,
                                       Sex: $("#sel_Sex").val(),
                                       RealName: $("#txt_RealName").val(),
                                       Phone: $("#txt_Phone").val(),
                                       Address: $("#txt_Address").val(),
                                       ProcessLvId: $("#sel_ProcessLv").val(),
                                       SfzNo: $("#txt_SfzNo").val(),
                                       PicImgId: $("#img_PicImg").attr("PicImgId"),
                                       AreaId: $("#sel_area").val()
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           alert("保存成功!");
                                           shuaxin();

                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);


}

function GetMemberAmountDetail(CurrentPage) {

    AjaxPost("/amb/", "GetMemberAmountDetail",
                                   {
                                       CurrentPage: CurrentPage,
                                       MemberId: GetQueryString("MemberId")
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];
                                               w.push("<tr>");
                                               w.push("<td>");
                                               w.push(j.MemberAmountChangeTypeName);
                                               w.push("</td>");
                                               w.push("<td>");
                                               w.push(j.ChangeAmount);
                                               w.push("</td>");
                                               w.push("<td>");
                                               w.push(j.OldAmount);
                                               w.push("</td>");
                                               w.push("<td>");
                                               w.push(j.NewAmount);
                                               w.push("</td>");
                                               w.push("<td>");
                                               w.push(j.CreateTime);
                                               w.push("</td>");
                                               w.push("</tr>");
                                           }
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                       $("#tb_MemberAmountDetail").html(w.join(""));
                                       ZyPagerSetting("GetMemberAmountDetail", CurrentPage, data.t, "1");
                                   }, false);
}


function ProcessLvStatusChange(obj) {

    var ProcessLvStatusId = $(obj).val();

    AjaxPost("/amb/", "ProcessLvStatusChange",
                                             {
                                                 MemberId: MemberId,
                                                 ProcessLvStatusId: ProcessLvStatusId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {



                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);

}



//刷新生产档期


function ReSetMaxOrderPlanningTime() {




    AjaxPost("/amb/", "ReSetMaxOrderPlanningTime",
                                         {
                                             MemberId: MemberId

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 $("#sp_MaxOrderPlanningTime").html(data.MaxOrderPlanningTime);
                                             }
                                             else {
                                                 alert(data.re)
                                             }
                                         }, false);


}


// #region 用户技能类别


function SetSkill() {





    AjaxPost("/amb/", "GetSkillList",
                                         {
                                             CurrentPage: 1

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 if (data.list.length > 0) {
                                                     w.push("<div class='div_Skill' >");
                                                     for (var i = 0; i < data.list.length; i++) {
                                                         var j = data.list[i];



                                                         w.push("<a SkillId='" + j.SkillId + "' ");

                                                         for (var n = 0; n < MemberInfo.Skill.length; n++) {

                                                             var m = MemberInfo.Skill[n];

                                                             if (j.SkillId == m.SkillId) {

                                                                 w.push(" class='select' ");
                                                             }

                                                         }


                                                         w.push("onclick='SaveOneSkill(this)' >");
                                                         w.push(j.SkillName);
                                                         w.push("</a>");


                                                     }

                                                     w.push("</div>");




                                                     PopWindow({
                                                         title: "新弹窗",
                                                         html: w.join("")

                                                     }, 500, 200);






                                                 }


                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);













}


function SaveOneSkill(obj) {

    var SkillId = $(obj).attr("SkillId");

    AjaxPost("/amb/", "SaveOneSkill",
                                         {
                                             MemberId: MemberId,
                                             SkillId: SkillId

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 $(obj).siblings().removeClass("select");
                                                 $(obj).addClass("select");
                                                 shuaxin();

                                             }
                                             else {
                                                 alert(data.re)
                                             }

                                         }, false);





}


// #endregion


// #region  用户擅长



function MemberVsPlateVsFabric() {

    AjaxPost("/amb/", "MemberVsPlateVsFabric",
                                             {
                                                 MemberId: MemberId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     w = [];

                                                     w.push("<h3>擅长面料</h3>");

                                                     if (data.Fabric.length > 0) {


                                                         for (var i = 0; i < data.Fabric.length; i++) {   //面料
                                                             var j = data.Fabric[i];
                                                             w.push("<span id='sp_f" + j.FabricId + "'  class='sp_f'  FabricId='" + j.FabricId + "' tapmode='tap1'   >");
                                                             w.push("<i></i>");
                                                             w.push(j.FabricName);
                                                             w.push("</span>");

                                                         }
                                                         $("#div_Fabric").html(w.join(""));


                                                     }

                                                     w = [];

                                                     w.push("<h3>擅长品类</h3>");

                                                     if (data.Plate.length > 0) {


                                                         for (var i = 0; i < data.Plate.length; i++) {   //面料
                                                             var j = data.Plate[i];
                                                             w.push("<span  id='sp_p" + j.PlateId + "' class='sp_p' PlateId='" + j.PlateId + "' tapmode='tap1'  >");
                                                             w.push("<i></i>");
                                                             w.push(j.PlateName);
                                                             w.push("</span>");

                                                         }
                                                         $("#div_Plate").html(w.join(""));


                                                     }



                                                     if (data.MemberVsFabric.length > 0) {


                                                         for (var i = 0; i < data.MemberVsFabric.length; i++) {


                                                             var j = data.MemberVsFabric[i];

                                                             $("#sp_f" + j.FabricId + "").addClass("select");

                                                         }

                                                     }


                                                     if (data.MemberVsPlate.length > 0) {

                                                         for (var i = 0; i < data.MemberVsPlate.length; i++) {


                                                             var j = data.MemberVsPlate[i];

                                                             $("#sp_p" + j.PlateId + "").addClass("select");

                                                         }

                                                     }



                                                     $(".sp_f").click(function () {

                                                         var b = $(this).hasClass("select");

                                                         if (b) {

                                                             RemoveMemberVsFabric(this);


                                                         }
                                                         else {
                                                             SaveMemberVsFabric(this);

                                                         }


                                                     });

                                                     $(".sp_p").click(function () {

                                                         var b = $(this).hasClass("select");

                                                         if (b) {

                                                             RemoveMemberVsPlate(this);


                                                         }
                                                         else {
                                                             SaveMemberVsPlate(this);

                                                         }


                                                     });

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);
}
// #region 面料


function RemoveMemberVsFabric(obj) {
    var FabricId = $(obj).attr("FabricId");

    AjaxPost("/amb/", "RemoveMemberVsFabric",
                                             {
                                                 MemberId: MemberId,
                                                 FabricId: FabricId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     $(obj).removeClass("select");
                                                 }
                                                 else {
                                                     alert(data.re);
                                                 }

                                             }, false);

}

function SaveMemberVsFabric(obj) {
    var FabricId = $(obj).attr("FabricId");

    AjaxPost("/amb/", "SaveMemberVsFabric",
                                             {
                                                 MemberId: MemberId,
                                                 FabricId: FabricId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     $(obj).addClass("select");
                                                 }
                                                 else {
                                                     alert(data.re);
                                                 }

                                             }, false);

}


// #endregion


// #region 款式


function RemoveMemberVsPlate(obj) {
    var PlateId = $(obj).attr("PlateId");

    AjaxPost("/amb/", "RemoveMemberVsPlate",
                                             {
                                                 MemberId: MemberId,
                                                 PlateId: PlateId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     $(obj).removeClass("select");
                                                 }
                                                 else {
                                                     alert(data.re);
                                                 }

                                             }, false);

}

function SaveMemberVsPlate(obj) {

    var PlateId = $(obj).attr("PlateId");

    AjaxPost("/amb/", "SaveMemberVsPlate",
                                             {
                                                 MemberId: MemberId,
                                                 PlateId: PlateId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     $(obj).addClass("select");
                                                 }
                                                 else {
                                                     alert(data.re);
                                                 }

                                             }, false);
}



// #endregion



// #endregion


// #region 所在团队



function GetMyTeamMemberList() {



    AjaxPost("/amb/", "GetMyTeamMemberList",
                                         {
                                             MemberId: MemberId

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 if (data.list.length > 0) {
                                                     for (var i = 0; i < data.list.length; i++) {
                                                         var j = data.list[i];
                                                         w.push("<tr style='height:50px'  MemberId='" + j.MemberId + "' ProcessLvStatusId='" + j.ProcessLvStatusId + "' >");
                                                         w.push("<td  class='c' >");
                                                         w.push("<img src='" + j.PicImgUrl + "' class='img_pic'  />");
                                                         w.push("</td>");
                                                         w.push("<td  class='c' >");
                                                         w.push(j.RealName);
                                                         w.push("</td>");
                                                         w.push("<td  >");
                                                         w.push(j.SfzNo);
                                                         w.push("</td>");
                                                         w.push("<td  class='c' >");
                                                         w.push("<img src='" + j.SfzImgUrl1 + "' class='img_sfz2' onclick='OpenSfzImg(this)'  />");
                                                         w.push("<img src='" + j.SfzImgUrl2 + "' class='img_sfz2' onclick='OpenSfzImg(this)'  />");
                                                         w.push("</td>");
                                                         w.push("<td  class='c' >");
                                                         w.push(j.Phone);
                                                         w.push("</td>");
                                                         w.push("<td  class='c' >");
                                                         w.push("[" + j.ProcessLvTitle + "]" + j.ProcessLvName);
                                                         w.push("</td>");
                                                         w.push("<td  class='c' >");
                                                         w.push(j.ProcessLvStatusName);
                                                         w.push("</td>");
                                                         w.push("</tr>");

                                                     }
                                                 }

                                                 $("#tbody_Team").html(w.join(""));

                                                 $("#tbody_Team>tr").each(function () {


                                                     var ProcessLvStatusId = ConvertToInt($(this).attr("ProcessLvStatusId"));



                                                     var r = [];

                                                     r.push({
                                                         evName: "ToMemberInfo",
                                                         Title: "查看用户详情",
                                                         evIcon: ""
                                                     });


                                                     if (ProcessLvStatusId != 0) {

                                                         r.push({
                                                             evName: "ProcessLvStatusChange0",
                                                             Title: "未认证",
                                                             evIcon: ""
                                                         });
                                                     }
                                                     if (ProcessLvStatusId != 10) {

                                                         r.push({
                                                             evName: "ProcessLvStatusChange10",
                                                             Title: "待认证",
                                                             evIcon: ""
                                                         });

                                                     }

                                                     if (ProcessLvStatusId != 20) {

                                                         r.push({
                                                             evName: "ProcessLvStatusChange20",
                                                             Title: "通过认证",
                                                             evIcon: ""
                                                         });
                                                     }




                                                     r.push({
                                                         evName: "RemoveMemberTeam",
                                                         Title: "从团队中解绑",
                                                         evIcon: ""
                                                     });



                                                     RightMenu(this, r);







                                                 })





                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);



}


function RemoveMemberTeam(obj) {



    if (!confirm("要将用户从团队中剥离?")) {
        return;

    }


    var MemberId = $(obj).attr("MemberId");





    AjaxPost("/amb/", "RemoveMemberTeam",
                                         {
                                             MemberId: MemberId

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

function ToMemberInfo(obj) {
    var MemberId = $(obj).attr("MemberId");

    window.open("MemberInfo.aspx?MemberId=" + MemberId + "")
}



function ProcessLvStatusChange20(obj) {

    var MemberId = $(obj).attr("MemberId");

    AjaxPost("/amb/", "ProcessLvStatusChange",
                                             {
                                                 ProcessLvStatusId: 20,
                                                 MemberId: MemberId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {


                                                     GetMyTeamMemberList();

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                             }, false);

}

function ProcessLvStatusChange0(obj) {


    var MemberId = $(obj).attr("MemberId");

    AjaxPost("/amb/", "ProcessLvStatusChange",
                                             {
                                                 ProcessLvStatusId: 0,
                                                 MemberId: MemberId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {


                                                     GetMyTeamMemberList();

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                             }, false);





}

function ProcessLvStatusChange10(obj) {


    var MemberId = $(obj).attr("MemberId");

    AjaxPost("/amb/", "ProcessLvStatusChange",
                                             {
                                                 ProcessLvStatusId: 10,
                                                 MemberId: MemberId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {


                                                     GetMyTeamMemberList();

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                             }, false);





}



function OpenSfzImg(img) {
    var src = $(img).attr("src");


    var w = [];

    w.push("<div class='c' >");

    w.push("<img style='max-width:600px;max-height:600px' src=" + src + " />");

    w.push("</div>");
    PopWindow({
        title: "新弹窗",
        html: w.join("")

    }, 600, 600);





}

// #endregion


// #region 工单


function GetOrderToWorkPageList(CurrentPage) {

    AjaxPost("/ao/", "GetOrderToWorkPageList",
                                         {
                                             CurrentPage: CurrentPage,
                                             MemberId: MemberId,
                                             Order: " OrderToWorkStatusId asc, LimitTime desc "

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 if (data.list.length > 0) {
                                                     for (var i = 0; i < data.list.length; i++) {
                                                         var j = data.list[i];
                                                         w.push("<tr OrderId='" + j.OrderId + "' OrderToWorkId='" + j.OrderToWorkId + "' OrderToWorkStatusId ='" + j.OrderToWorkStatusId + "'>");
                                                         w.push("<td>");
                                                         w.push(j.OrderId);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.OrderName);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.ReceivedTime);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.LimitTime);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.OrderToWorkStatusName);
                                                         w.push("</td>");

                                                         w.push("</tr>");

                                                     }
                                                 }


                                             }
                                             else {
                                                 alert(data.re)
                                             }

                                             $("#tbody_OrderToWork").html(w.join(""));

                                             $("#tbody_OrderToWork>tr").each(function () {






                                                 r = [];

                                                 var OrderToWorkStatusId = ConvertToInt($(this).attr("OrderToWorkStatusId"));

                                                 if (OrderToWorkStatusId <= 15) {


                                                     r.push({
                                                         evName: "ClearMemberOrderToWork",
                                                         Title: "清理工单",
                                                         evIcon: ""
                                                     });
                                                 }
                                                 r.push({
                                                     evName: "ToOrderInfo",
                                                     Title: "查看订单",
                                                     evIcon: ""
                                                 });


                                                 RightMenu(this, r);





                                             })


                                             ZyPagerSetting("GetOrderToWorkPageList", CurrentPage, data.t, "OrderToWork");
                                         }, false);



}


function ClearMemberOrderToWork(obj) {

    var OrderToWorkId = $(obj).attr("OrderToWorkId");





    AjaxPost("/ao/", "ClearMemberOrderToWork",
                                         {
                                             OrderToWorkId: OrderToWorkId

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {


                                                 GetOrderToWorkPageList(1);


                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);




}

function ToOrderInfo(obj) {
    var OrderId = $(obj).attr("OrderId");
    window.open("/Order/OrderInfo.aspx?OrderId=" + OrderId + "");

}


// #endregion


