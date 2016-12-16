/// <reference path="C:\anli\快工场\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Manage\Script/ZYUiPub.js" />



var OrderId = "";
var imgArray = [];

var ClothesSizeArray = [];
var OrderData = {};

var OrderToWorkData = {};
$(function () {
    OrderId = GetQueryString("OrderId", "");

    if (OrderId == "") {
        tiaozhuan("AddOrderInfo.aspx");
    }
    BindTab();
    BindPageSetting();
});


function BindPageSetting() {



    BindDateInput("txt_OrderReceivedTime", GetDateStr(2), true);


    GetProcessLocationType();
    GetProcessLv();
    GetOrderInfo();
    BindOrderReceivedTimeEvent();  //绑定领取裁片的快捷时间

}


// #region 订单


// #region 订单日志


function GetOrderLogList(CurrentPage) {

    AjaxPost("/ao/", "GetOrderLogList",
                                             {
                                                 CurrentPage: CurrentPage,
                                                 OrderId: OrderId

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     if (data.list.length > 0) {
                                                         for (var i = 0; i < data.list.length; i++) {
                                                             var j = data.list[i];
                                                             w.push("");
                                                             w.push("<tr>");
                                                             w.push("<td>");
                                                             w.push(j.OrderLogTitle);
                                                             w.push("</td>");

                                                             w.push("<td>");
                                                             w.push(j.OrderLogClassName);
                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             if (j.MemberId == 0) {
                                                                 w.push("<span class='sp_w' >-</span>");
                                                             }
                                                             else {
                                                                 w.push(j.Phone);
                                                                 if (j.MemberRealName != "") {
                                                                     w.push("(" + j.MemberRealName + ")");
                                                                 }

                                                             }


                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             if (j.UserId == "") {


                                                                 w.push("<span class='sp_w' >-</span>");
                                                             }
                                                             else {

                                                                 w.push(j.UserId);
                                                                 if (j.RealName != "") {
                                                                     w.push(j.RealName);
                                                                 }



                                                             }


                                                             w.push("</td>");
                                                             w.push("<td>");
                                                             w.push(j.CreateTime);
                                                             w.push("</td>");
                                                             w.push("");
                                                             w.push("");
                                                             w.push("");
                                                             w.push("</tr>");

                                                         }
                                                     }


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                                 $("#tb_OrderLog").html(w.join(""));
                                                 ZyPagerSetting("GetOrderLogList", CurrentPage, data.t, "2");
                                             }, false);

}


// #endregion
// #region 图片编辑


function AddImg(ci) {
    $("#div_imgs").append("<img src='" + ci.url + "' ImgId='" + ci.ImgId + "' /> ");

    var i = $("#div_imgs>img.select").length;

    if (i <= 0) {

        $("#div_imgs>img").eq(0).addClass("select");
    }


    BindImgRightMenu();
}

function BindImg() {


    var w = new Array();
    for (var i = 0; i < imgArray.length; i++) {


        var j = imgArray[i];
        w.push("<img src='" + j.ImgUrl + "' ImgId='" + j.ImgId + "' />");

    }

    $("#div_imgs").html(w.join(""));
    BindImgRightMenu();


}

function BindImgRightMenu() {
    $("#div_imgs>img").unbind("contextmenu");
    $("#div_imgs>img").unbind("click");
    $("#div_imgs>img").each(function () {


        RightMenu(this, [
                    {
                        evName: "delImg",
                        Title: "删除",
                        evIcon: ""

                    }

        ]);
        $(this).click(function () {
            $(this).siblings().removeClass("select");
            $(this).addClass("select");

        });

    })


}
function delImg(obj) {

    $(obj).remove();
}

// #endregion

// #region 订单公式
function CountOrderDetailTable(table) {


    var AllNum = 0;

    $(table).find(".tr_color").each(function () {

        var trObj = $(this);
        var num = 0;
        $(this).find(".td_ClothesSize").each(function () {

            var n = ConvertToInt($(this).find("input").eq(0).val());
            num = accAdd(num, n);

        });
        trObj.find(".sp_SumColor").eq(0).html(num);
        AllNum = accAdd(AllNum, num);
    });


    var cNum = $(table).find("th").length - 2;



    for (var i = 1; i <= cNum; i++) {
        var cSum = 0;
        $(table).find(".tr_color").each(function () {
            var n = ConvertToInt($(this).find("td").eq(i).find("input").val());
            cSum = accAdd(cSum, n);
        });

        $(table).find("tfoot td").eq(i).find(".sp_SumClothesSize").html(cSum);

    }


    $(".sp_SumALL").html(AllNum);

    CountMinQuantity();


}

function CountPlanningTime2() {

    AjaxPost("/ao/", "CountPlanningTime2",
                                             {
                                                 PlanningDay: $("#txt_PlanningDay").val(),
                                                 ReceivedTime: $("#txt_OrderReceivedTime").val()

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     CountPlanningTimeVal(data.PlanningTime);
                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                             }, false);

}





function CountMinQuantity() {
    AjaxPost("/ao/", "CountMinQuantity",
                                          {
                                              OrderQuantity: $("#sp_OrderSumALL").html(),
                                              Places: $("#txt_Places").val()

                                          }, function (data) {
                                              var w = [];
                                              if (data.re == "ok") {

                                                  $("#sp_MinQuantity").html(data.MinQuantity);
                                              }
                                              else {
                                                  alert(data.re)
                                              }

                                          }, false);

}



function CountPlanningTimeVal(txt) {

    $("#sp_PlanningTime").html(txt);
}
// #endregion



function BindOrderReceivedTimeEvent() {

    $(".btn_OrderReceivedTime").click(function () {


        var date = DateFormat($("#txt_OrderReceivedTime").val(), "yyyy-MM-dd");

        var datetime = date + " " + $(this).val() + ":00";

        $("#txt_OrderReceivedTime").val(datetime);
        $("#txt_OrderReceivedTime").change();
    });
}




function GetOrderInfo(o) {
    if (OrderId == "") {
        return;
    }
    if (!o) {
        o = {};
    }

    $("#tr_OrderId").show();

    $("#sp_OrderId").html(OrderId);

    AjaxPost("/ao/", "GetOrderInfo",
                                   {
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           CountOrder();
                                           OrderData = data;
                                           if (data.Info.OrderStatusId == 5) {
                                               $("#btn_ReleaseOrder").show();
                                           }
                                           else {
                                               $("#btn_ReleaseOrder").hide();

                                           }


                                           if (!o.NoInfo) {

                                               // #region 绑定订单主体





                                               //$("#sp_OrderQuantity").html(data.Count.OrderQuantity);
                                               //$("#sp_WorkQuantity").html(data.Count.WorkQuantity);
                                               //$("#sp_CheckQuantity").html(data.Count.CheckQuantity);
                                               //$("#sp_OrderToWorkNum").html(data.Count.OrderToWorkNum);
                                               //$("#sp_OrderVsMemberNum").html(data.Count.OrderVsMemberNum);


                                               imgArray = data.imgArray;
                                               BindImg();
                                               ed.ready(function () {

                                                   ed.setContent(data.Info.OrderContent);
                                               });
                                               $("#txt_OrderTitle").val(data.Info.OrderTitle);
                                               $("#txt_OrderCode").val(data.Info.OrderCode);
                                               $("#txt_ClientsCode").val(data.Info.ClientsCode);
                                               $("#sel_ProcessLv").val(data.Info.ProcessLvId);
                                               $("#sel_ProcessLocationType").val(data.Info.ProcessLocationTypeId);
                                               $("#sel_OrderClass").val(data.Info.OrderClassId);
                                               $("#txt_OrderReceivedTime").val(DateFormat(data.Info.ReceivedTime, "yyyy-MM-dd hh:mm:ss"));
                                               $("#txt_PlanningDay").val(data.Info.PlanningDay);


                                               $("#sel_ReleaseType").val(data.Info.ReleaseTypeId);

                                               $("#sp_MinQuantity").html(data.Info.MinQuantity);


                                               $("#sp_PlanningTime").html(DateFormat(data.Info.PlanningTime, "yyyy-MM-dd hh:mm:ss"));

                                               $("#sel_ReleaseType").change();
                                               if (data.Info.OrderContacts == "") {
                                                   $("#txt_OrderContacts").val(localStorage.OrderContacts);
                                               }
                                               else {
                                                   $("#txt_OrderContacts").val(data.Info.OrderContacts);
                                               }


                                               if (data.Info.OrderTel == "") {
                                                   $("#txt_OrderTel").val(localStorage.OrderTel);
                                               }
                                               else {
                                                   $("#txt_OrderTel").val(data.Info.OrderTel);
                                               }


                                               if (data.Info.OrderAddress == "") {
                                                   $("#txt_OrderAddress").val(localStorage.OrderAddress);
                                               }
                                               else {
                                                   $("#txt_OrderAddress").val(data.Info.OrderAddress);
                                               }

                                               $("#txt_OrderWages").val(data.Info.OrderWages);
                                               $("#sel_Unit").val(data.Info.Unit);
                                               $("#txt_Places").val(data.Info.Places);
                                               $("#div_imgs>img").removeClass("select");

                                               $("#div_imgs>img[ImgId='" + data.Info.OrderImgId + "']").addClass("select");


                                               // #endregion

                                           }

                                           // #region 工作量预估

                                           if (data.OrderExpect.length > 0) {
                                               w = [];
                                               for (var i = 0; i < data.OrderExpect.length; i++) {

                                                   var j = data.OrderExpect[i];
                                                   w.push("<span>第" + j.OrderExpectDay + "天:</span>");
                                                   w.push("<input type='text' value='" + j.Num + "' onkeyup='OnlyNumberForInput(this)'  class='txt_OrderExpect' OrderExpectDay='" + j.OrderExpectDay + "'  />;  ");


                                               }
                                               $("#div_OrderExpect").html(w.join(""));
                                               w = [];
                                               $("#div_OrderExpect .txt_OrderExpect").blur(function () {

                                                   AjaxPost("/ao/", "OrderExpect",
                                                                                  {
                                                                                      Num: $(this).val(),
                                                                                      OrderExpectDay: $(this).attr("OrderExpectDay"),
                                                                                      OrderId: OrderId
                                                                                  }, function (data) {

                                                                                      if (data.re == "ok") {

                                                                                      }
                                                                                      else {
                                                                                          alert(data.re)
                                                                                      }

                                                                                  }, false);


                                               });
                                           }


                                           // #endregion

                                           // #region 绑定订单明细

                                           if (data.Detail.length > 0) {

                                               w = [];
                                               w.push("<table class='t3' id='tb_OrderDetail' >");

                                               w.push("<thead>");
                                               w.push("<tr>");
                                               w.push("<th style='width:170px;'>")
                                               w.push("颜色");
                                               w.push("</th>");
                                               for (var i = 0; i < data.GroupClothesSize.length; i++) {

                                                   var j = data.GroupClothesSize[i];
                                                   w.push("<th  >");
                                                   w.push(j.ClothesSizeName);
                                                   w.push("</th>");
                                               }
                                               w.push("<th>合计</th>");
                                               w.push("</tr>");
                                               w.push("</thead>");


                                               w.push("<tbody>");

                                               for (var i = 0; i < data.Detail.length; i++) {

                                                   var j = data.Detail[i];

                                                   w.push("<tr class='tr_color' OrderDetailId='" + j.OrderDetailId + "'  Color='" + j.Color + "'  ondblclick='ToSaveDetail(this)'  >");
                                                   w.push("<td OrderDetailId='" + j.OrderDetailId + "' class='c'   >");
                                                   w.push(j.Color);
                                                   w.push("</td>");

                                                   for (var x = 0; x < data.GroupClothesSize.length; x++) {

                                                       var y = data.GroupClothesSize[x];



                                                       w.push("<td class='td_ClothesSize c'  OrderDetailId='" + j.OrderDetailId + "'  >");
                                                       var has = false;
                                                       for (var m = 0; m < data.DetailVsClothesSize.length; m++) {

                                                           var n = data.DetailVsClothesSize[m];
                                                           if (n.ClothesSizeId == y.ClothesSizeId && n.OrderDetailId == j.OrderDetailId) {


                                                               w.push(" <input type='text' value='" + n.Num + "' onkeyup=\"OnlyNumberForInput(this)\" onblur='SaveOrderDetailVsClothesSize(this)' class='txt_DetailVsClothesSize Order c'  OrderDetailId='" + j.OrderDetailId + "'  ClothesSizeId='" + n.ClothesSizeId + "'  ")
                                                               has = true;

                                                           }



                                                       }
                                                       if (!has) {
                                                           w.push(" <input type='text'  value='0' class='txt_DetailVsClothesSize Order c'  onkeyup=\"OnlyNumberForInput(this)\"   onblur='SaveOrderDetailVsClothesSize(this)'   OrderDetailId='" + j.OrderDetailId + "'  ClothesSizeId='" + y.ClothesSizeId + "' /> ")

                                                       }

                                                       w.push("</td>");
                                                   }
                                                   w.push("<td  class='c'  ><span class='sp_SumColor' ></span></td>");
                                                   w.push("</tr>");


                                               }

                                               w.push("<tfoot>");

                                               w.push("<tr>");
                                               w.push("<td class='c'  >合计</td>");
                                               for (var i = 0; i < data.GroupClothesSize.length; i++) {
                                                   w.push("<td  class='c' ><span class='sp_SumClothesSize' ></span> </td>");
                                               }
                                               w.push("<td class='c'   ><span class='sp_SumALL' id='sp_OrderSumALL' ></span></td>");
                                               w.push("</tr>");


                                               w.push("</tfoot>");

                                               w.push("</tbody>");


                                               w.push("</table>");


                                           }


                                           $("#div_DetailTable").html(w.join(""));
                                           CountOrderDetailTable(document.getElementById("tb_OrderDetail"));
                                           $("#tb_OrderDetail>tbody>tr").each(function () {
                                               r = [];
                                               r.push({
                                                   evName: "ToChangeOrderDetail",
                                                   Title: "修改颜色",
                                                   evIcon: ""
                                               });
                                               r.push({
                                                   evName: "RemoveOrderDetail",
                                                   Title: "删除颜色",
                                                   evIcon: ""
                                               });

                                               RightMenu(this, r);



                                           });

                                           // #endregion


                                           // #region 绑定工单列表

                                           GetOrderToWorkList();  //读取工单数据

                                           w = [];


                                           if (data.OrderVsMember.length > 0) {

                                               for (var i = 0; i < data.OrderVsMember.length; i++) {
                                                   var Quantity = 0;
                                                   var CheckQuantity = 0;
                                                   //登记人员输出开始
                                                   var j = data.OrderVsMember[i];

                                                   w.push("<a class=\"a_Member\" MemberId='" + j.MemberId + "' VsStatus='" + j.VsStatus + "' >");
                                                   w.push("<img src='" + j.PicImgUrl + "' class='img_MemberPic'  />");
                                                   w.push("<span class='sp_VsPlaces' ><b>" + j.VsPlaces + "</b>人</span>");
                                                   w.push("<span class='sp_VsStatus" + j.VsStatus + "' >" + j.VsStatusName + "</span>");
                                                   w.push("<span  class='sp_member'  >姓名:<b>" + j.RealName + "</b></span>");
                                                   w.push("<span>技能等级:<b>[" + j.ProcessLvTitle + "]" + j.ProcessLvName + "</b></span>");
                                                   w.push("<span>手机号:<b>" + j.Phone + "</b></span>");

                                                   if (data.Detail.length > 0) {   //开始输出颜色

                                                       for (var x = 0; x < data.Detail.length; x++) {
                                                           var y = data.Detail[x];

                                                           var NumVal = 0;
                                                           var CheckNumVal = 0;
                                                           for (var m = 0; m < OrderToWorkData.OrderToWorkDetailVsClothesSize.length; m++) {

                                                               var n = OrderToWorkData.OrderToWorkDetailVsClothesSize[m];

                                                               if (n.MemberId == j.MemberId && n.Color == y.Color) {

                                                                   NumVal = accAdd(NumVal, n.Num);
                                                                   CheckNumVal = accAdd(CheckNumVal, n.CheckNum);
                                                               }



                                                           }

                                                           w.push("<span>" + y.Color + ":<b class='b_Num_" + j.MemberId + "' Color='" + y.Color + "' >" + NumVal + "</b>" + data.Info.Unit + "  检:<b>" + CheckNumVal + "</b></span>");

                                                           Quantity = accAdd(Quantity, NumVal);
                                                           CheckQuantity = accAdd(CheckQuantity, CheckNumVal);


                                                       }
                                                   }
                                                   w.push("<span>总:<b class='b_1' >" + Quantity + "</b>  检:<b class='b_1'>" + CheckQuantity + "</b></span>");



                                                   var MyOrderToWorkNum = 0;

                                                   if (OrderToWorkData.OrderToWork.length > 0) {
                                                       w.push("<div class='div_workList' ><ul>");
                                                       for (var n = 0; n < OrderToWorkData.OrderToWork.length; n++) {

                                                           var m = OrderToWorkData.OrderToWork[n];

                                                           if (j.MemberId == m.MemberId) {
                                                               MyOrderToWorkNum = MyOrderToWorkNum + 1;


                                                               w.push("<li ondblclick='ToSaveOrderToWork(" + m.OrderToWorkId + ")' OrderToWorkId='" + m.OrderToWorkId + "' OrderToWorkStatusId='" + m.OrderToWorkStatusId + "' >");
                                                               w.push(m.OrderToWorkTitle);
                                                               // w.push("[" + DateFormat(m.CreateTime, "MM-dd h:mm") + "]");
                                                               w.push("<b class='sp_OrderToWorkStatus' >[" + m.OrderToWorkStatusName + "]</b>")
                                                               w.push("</li>");

                                                           }


                                                       }
                                                       w.push("</ul></div>");
                                                   }
                                                   if (MyOrderToWorkNum > 0) {
                                                       w.push("<div class='div_MyOrderToWorkNum' >×" + MyOrderToWorkNum + "</div>");

                                                   }



                                                   w.push("</a>");

                                                   //登记人员输出结束
                                               }





                                           }
                                           w.push("<a class=\"a_Member add\" onclick='AddMember()' ></a>");
                                           $("#div_OrderVsMember").html(w.join(""));
                                           $("#div_OrderVsMember>a").each(function () {

                                               var VsStatus = ConvertToInt($(this).attr("VsStatus"));


                                               r = [];
                                               r.push({
                                                   evName: "ToMemberInfo",
                                                   Title: "查看用户资料",
                                                   evIcon: ""
                                               });


                                               if (VsStatus >= 10 <= 15) {


                                                   r.push({
                                                       evName: "PopTaoTai",
                                                       Title: "淘汰",
                                                       evIcon: ""
                                                   });



                                               }

                                               if (VsStatus >= 10)   //10是已登记, 10以上是可以分派工单的
                                               {
                                                   r.push({
                                                       evName: "ToAddOrderToWork",
                                                       Title: "分派工单",
                                                       evIcon: ""
                                                   });
                                               }

                                               



                                               RightMenu(this, r);

                                               $(this).find(".div_workList ul li").each(function () {

                                      

                                               });

                                           });






                                           // #endregion

                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);
}

function ToChangeOrderDetail(obj) {

    var OrderDetailId = $(obj).attr("OrderDetailId");
    var Color = $(obj).attr("Color");



    var w = [];
    w.push("<input type='text' id='txt_ChangeColor'  value='" + Color + "'  />");
    w.push(" <input type='button'  value='更改'  onclick=\"ChangeOrderDetail(" + OrderDetailId + ")\"  />");


    PopWindow({
        html: w.join(""),
        title: "更改颜色"

    }, 300, 50)


}

function ChangeOrderDetail(OrderDetailId) {

    AjaxPost("/ao/", "ChangeOrderDetail",
                                           {
                                               OrderDetailId: OrderDetailId,
                                               Color: $("#txt_ChangeColor").val()

                                           }, function (data) {
                                               var w = [];
                                               if (data.re == "ok") {
                                                   ClearPopWindow();
                                                   GetOrderInfo();


                                               }
                                               else {
                                                   alert(data.re)
                                               }

                                           }, false);

}

function RemoveOrderDetail(obj) {

    if (!confirm("这将会删除整个颜色明细下的所有尺码数量,确定吗?")) {
        return;
    }

    var OrderDetailId = $(obj).attr("OrderDetailId");


    AjaxPost("/ao/", "RemoveOrderDetail",
                                             {
                                                 OrderDetailId: OrderDetailId


                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     ClearPopWindow();
                                                     GetOrderInfo();


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                             }, false);


}





//弹出淘汰窗口
function PopTaoTai(obj) {


    var MemberId = $(obj).attr("MemberId");

    var w = [];
    w.push("<textarea id=\"txt_taotai\" class='txt_taotai'   placeholder='输入淘汰原因'></textarea>");

    w.push("<h5>淘汰原因快捷输入:</h5>");
    w.push("<span class='sp_taotaiyy' >1.<b>人没有来</b></span>");
    w.push("<span class='sp_taotaiyy' >2.<b>生产质量未达标</b></span>");
    w.push("<span class='sp_taotaiyy' >3.<b>生产效率未达标</b></span>");
    w.push("<span class='sp_taotaiyy' >4.<b>泄露产品资料</b></span>");
    w.push("<span class='sp_taotaiyy' >5.<b>遗失或破坏生产资料</b></span>");
    w.push("<input   type='button' value=' 确 定 '  onclick='TaoTai(" + MemberId + ")'  />");

    PopWindow({
        title: "淘汰登记用户",
        html: w.join(""),
        shadow: true

    }, 300, 400);

    $(".sp_taotaiyy").click(function () {

        var t = $(this).find("b").html();

        $("#txt_taotai").val(t);

    });

}
//执行淘汰
function TaoTai(MemberId) {
    AjaxPost("/ao/", "RemoveOrderVsMember",
                                             {
                                                 MemberId: MemberId,
                                                 OrderId: OrderId,
                                                 RemoveType: "淘汰",
                                                 Memo: $("#txt_taotai").val()

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     ClearPopWindow();
                                                     GetOrderInfo();


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);

}

function AddMember() {


    var w = [];

    w.push("<div class='div_MemberList' >");
    w.push("<span class='sp_MemberTip' >15-18位数字检索身份证,11位检索手机号, 中文检索姓名</span>");
    w.push("<div class='clr' ></div>");
    w.push("<input type='text' id='txt_SMember' placeholder='姓名/身份证号/手机号'   />");
    w.push(" <select id='sel_MemberProcessLv'  ></select>");

    w.push(" <input id='btn_GetMemberList' type='button' value='Enter'  onclick='GetMemberList()'  />");
    w.push("<div class='clr_10px' ></div>");
    w.push("<table class='t3'  >");
    w.push("<thead>");
    w.push("<tr>");
    w.push("<th>");
    w.push("姓名");
    w.push("</th>");
    w.push("<th>");
    w.push("完成单数");
    w.push("</th>");
    w.push("<th>");
    w.push("品质等级");
    w.push("</th>");
    w.push("<th>");
    w.push("联系方式");
    w.push("</th>");
    w.push("<th>");
    w.push("生产档期");
    w.push("</th>");
    w.push("</tr>");
    w.push("</thead>");
    w.push("<tbody id='tb_Member' >");
    w.push("</tbody>");
    w.push("</table>");
    w.push("</div>");



    PopWindow({
        html: w.join(""),
        title: "主动派单登记"

    }, 600, 730);

    $("#sel_MemberProcessLv").html($("#sel_ProcessLv").html());
    $("#txt_SMember").focus();
    $("#txt_SMember").keyup(function () {

        if (event.keyCode == 13) {

            $("#btn_GetMemberList").click();
        }


    });
}

function GetMemberList() {

    AjaxPost("/amb/", "GetMemberList",
                                             {
                                                 top: 20,
                                                 MemberStr: $("#txt_SMember").val(),
                                                 ProcessLvId: $("#sel_MemberProcessLv").val(),
                                                 ProcessLvStatusId: 20


                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     if (data.list.length > 0) {
                                                         for (var i = 0; i < data.list.length; i++) {
                                                             var j = data.list[i];
                                                             w.push("<tr MemberId='" + j.MemberId + "' ondblclick='SaveOrderVsMember(this)' MaxOrderPlanningTime='" + j.MaxOrderPlanningTime + "' >");
                                                             w.push("<td class='c'>");
                                                             w.push(j.RealName);
                                                             w.push("</td>");

                                                             w.push("<td class='c' >");
                                                             w.push(j.CheckOrderNum);
                                                             w.push("</td>");

                                                             w.push("<td class='c'>");
                                                             w.push(j.ProcessLvName + "[" + j.ProcessLvTitle + "]");
                                                             w.push("</td>");

                                                             w.push("<td class='c'>");
                                                             w.push(j.Phone);
                                                             w.push("</td>");

                                                             w.push("<td class='c'>");
                                                             w.push(DateFormat(j.MaxOrderPlanningTime, "yyyy-MM-dd"));
                                                             w.push("</td>");

                                                             w.push("</tr>");

                                                         }
                                                     }


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                                 $("#tb_Member").html(w.join(""));

                                                 $("#tb_Member>tr").each(function () {



                                                     r = [];
                                                     r.push({
                                                         evName: "ToMemberInfo",
                                                         Title: "查看用户资料",
                                                         evIcon: ""
                                                     });



                                                     r.push({
                                                         evName: "SaveOrderVsMember",
                                                         Title: "主动派单(也可双击本行)",
                                                         evIcon: ""
                                                     });

                                                     RightMenu(this, r);

                                                 });

                                             }, false);
}


function SaveOrderVsMember(obj) {

    var MemberId = ConvertToInt($(obj).attr("MemberId"));
    var MaxOrderPlanningTime = $(obj).attr("MaxOrderPlanningTime");
    for (var i = 0; i < OrderData.OrderVsMember.length; i++) {

        var j = OrderData.OrderVsMember[i];

        if (j.MemberId == MemberId) {

            if (j.VsStatus == -10) {

                var b = confirm("该用户已被淘汰, 确定重新登记?");

                if (b) {

                }
                else {

                    return;
                }

            }



        }

    }
    switch (BiJiaoTime(MaxOrderPlanningTime, OrderData.Info.ReceivedTime)) {

        case 1: //档期大于本订单的裁片时间
            var b = confirm("该用户与本订单的档期有重叠, 确定登记?");

            if (!b) {
                return;
            }

            break;

        case 0:
        case 2:


            break;

        default:
            return;
    }



    AjaxPost("/ao/", "SaveOrderVsMember",
                                             {
                                                 MemberId: MemberId,
                                                 OrderId: OrderId,
                                                 VsType: 20

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     GetOrderInfo();


                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);


}


function ToMemberInfo(obj) {


    var MemberId = $(obj).attr("MemberId");
    window.open("/Member/MemberInfo.aspx?MemberId=" + MemberId + "");
}

// #region 获取字典


function GetProcessLocationType() {

    AjaxPost("/am/", "GetProcessLocationType",
                                   {
                                       CurrentPage: 1
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];
                                               w.push("<option value='" + j.ProcessLocationTypeId + "' >");
                                               w.push(j.ProcessLocationTypeName);
                                               w.push("</option>");

                                           }
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                       $("#sel_ProcessLocationType").html(w.join(""));

                                   }, false);
}


function GetProcessLv() {
    AjaxPost("/am/", "GetProcessLv",
                                  {
                                      CurrentPage: 1
                                  }, function (data) {
                                      var w = [];
                                      if (data.re == "ok") {
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

// #endregion


8888
//订单状态改为发布
function ReleaseOrder() {

    AjaxPost("/ao/", "ChangeOrderStatus",
                                   {
                                       OrderId: OrderId,
                                       OrderStatusId: 10
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           try {
                                               window.parent.BodyTip({

                                                   text: "发布成功!",
                                                   s: 2000

                                               });
                                           } catch (e) {

                                           }

                                           shuaxin();
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}

function ReleaseTypeChange(obj) {

    var ReleaseTypeId = ConvertToInt($(obj).val());
    if (ReleaseTypeId == 10) {
        $("#tr_MinQuantity").show();
    }
    else {
        //整单发布


        $("#tr_MinQuantity").hide();



        $("#txt_Places").val("1");
    }


}


//保存订单信息
function SaveOrderInfo() {

    localStorage.OrderContacts = $("#txt_OrderContacts").val();
    localStorage.OrderTel = $("#txt_OrderTel").val();
    localStorage.OrderAddress = $("#txt_OrderAddress").val();
    AjaxPost("/ao/", "SaveOrderInfo",
                                   {

                                       OrderId: OrderId,
                                       OrderTitle: $("#txt_OrderTitle").val(),
                                       OrderContent: encodeURIComponent(ed.getContent()),
                                       ProcessLvId: $("#sel_ProcessLv").val(),
                                       OrderCode: $("#txt_OrderCode").val(),
                                       ClientsCode: $("#txt_ClientsCode").val(),
                                       OrderWages: $("#txt_OrderWages").val(),
                                       OrderContacts: localStorage.OrderContacts,
                                       OrderTel: localStorage.OrderTel,
                                       OrderAddress: localStorage.OrderAddress,
                                       Unit: $("#sel_Unit").val(),
                                       ProcessLocationTypeId: $("#sel_ProcessLocationType").val(),
                                       OrderClassId: $("#sel_OrderClass").val(),
                                       OrderImgId: $("#div_imgs>.select").attr("ImgId"),
                                       OrderVsImg: JArrayToXmlStr(imgArray),
                                       // OrderStatusId: $("#txt_OrderStatusId").val(),
                                       ReleaseTypeId: $("#sel_ReleaseType").val(),
                                       LimitTime: $("#txt_LimitTime").val(),
                                       PlanningTime: $("#sp_PlanningTime").html(),

                                       Places: $("#txt_Places").val(),
                                       ReceivedTime: $("#txt_OrderReceivedTime").val(),
                                       PlanningDay: $("#txt_PlanningDay").val()
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           window.parent.BodyTip({

                                               text: "订单保存成功!",
                                               s: 5000

                                           });
                                           OrderId = data.OrderId;
                                           //  BindPageSetting();




                                           CountOrder();

                                           var OrderStatusId = ConvertToInt(data.OrderStatusId);

                                           if (OrderStatusId >= 5 && OrderStatusId < 20) {

                                               var w = [];
                                               w.push("<div class='div_afterSave' >");
                                               w.push("<h3>保存成功!</h3>");
                                               w.push("<h4>选择后续操作,留在本页请点击右上角关闭</h4>");



                                               w.push("<input type='button' value='返回列表' onclick=\"tiaozhuan('OrderList.aspx')\"  />");
                                               w.push("<input type='button' value='发布订单' onclick='OrderToFaBu()'  />");
                                               if (OrderStatusId == 10) {
                                                   //已经发布了. 但是还没有抢单
                                                   w.push("<input type='button' value='设置抢单'  onclick='OrderToQiangDan()'  />");
                                               }
                                               else {
                                                   w.push("<input type='button' value='发布并设置抢单'  onclick='OrderFaBuAndQiangDan()'  />");
                                               }

                                               w.push("<input type='button' value='定时抢单(开发中)'  disabled='disabled' />");

                                               w.push("</div>");

                                               PopWindow({
                                                   html: w.join(""),
                                                   title: "订单保存成功-后续操作",
                                                   shadow: true
                                               }, 670, 250);
                                           }





                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);

}



function OrderFaBuAndQiangDan() {

    OrderToFaBu(function () {

        OrderToQiangDan();
    });
}



function OrderToFaBu(fun) {


    AjaxPost("/ao/", "ChangeOrderStatus",
                                             {
                                                 OrderId: OrderId,
                                                 OrderStatusId: 10

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {

                                                     ClearPopWindow();
                                                     window.parent.BodyTip({

                                                         text: "订单成功发布!",
                                                         s: 2000

                                                     });

                                                     try {
                                                         fun();
                                                     } catch (e) {

                                                     }
                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);


}

function OrderToQiangDan() {
    AjaxPost("/ao/", "ChangeOrderStatus",
                                         {
                                             OrderId: OrderId,
                                             OrderStatusId: 20

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 window.parent.BodyTip({

                                                     text: "订单已经设置抢单!",
                                                     s: 2000

                                                 });


                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);

}


function ToOrderInfo() {
    if (OrderId == "") {
        window.parent.BodyTip({

            text: "请先保存订单信息!",
            s: 5000

        });

        $("#b_OrderInfo").click();
    }
    else {

        SaveOrderInfo();
    }

}


function ToSaveDetail(obj) {

    var OrderDetailId = 0;
    var ovcList = [];
    var info = {};
    if (obj) {


        var OrderDetailId = ConvertToInt($(obj).attr("OrderDetailId"));

        AjaxPost("/ao/", "GetOrderDetail",
                                       {
                                           OrderDetailId: OrderDetailId
                                       }, function (data) {
                                           var w = [];
                                           if (data.re == "ok") {

                                               info = data.info;
                                               ovcList = data.ovcList;

                                           }
                                           else {
                                               alert(data.re)
                                           }


                                       }, false);


    }



    AjaxPost("/ao/", "GetClothesSize",
                                   {
                                       CurrentPage: 1
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           ClothesSizeArray = data.list;
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);
    if (OrderId == "") {
        window.parent.BodyTip({

            text: "请先保存订单, 才能添加数量明细!",
            s: 5000

        });

        $("#b_OrderInfo").click();

    }
    else {
        var w = [];
        w.push("<div class='div_Detail' id='div_Detail' >");
        w.push("<ul>");
        w.push("<li>");
        w.push("<span class='sp_l' >颜色:</span>");

        var ColorVal = "";
        if (OrderDetailId != 0) {
            ColorVal = info.Color;
        }

        w.push("<input type='text' id='txt_Color' class='txt_Color'  placeholder=\"颜色\"  value='" + ColorVal + "'  />");
        w.push("</li>");
        for (var i = 0; i < ClothesSizeArray.length; i++) {

            var j = ClothesSizeArray[i];

            w.push("<li class='li_c' ClothesSizeId='" + j.ClothesSizeId + "' ClothesSizeName='" + j.ClothesSizeName + "'   >");
            w.push("<span class='sp_l' >" + j.ClothesSizeName + ":</span>");

            var NumVal = 0;
            var MemoVal = "";

            if (OrderDetailId != 0) {
                for (var x = 0; x < ovcList.length; x++) {

                    var y = ovcList[x];

                    if (y.ClothesSizeId == j.ClothesSizeId) {

                        NumVal = y.Num;
                        MemoVal = y.Memo
                        break;
                    }

                }

            }


            w.push("<input type='text' class='txt_Num' placeholder=\"数量\" value='" + NumVal + "'  ClothesSizeName='" + j.ClothesSizeName + "' onfocus='NumOnfocus(this)'  onkeyup=\"OnlyNumberForInput(this)\"  />");
            w.push("<input type='text' class='txt_Memo'  placeholder=\"备注\" value='" + MemoVal + "' />");
            w.push("</li>");
        }
        w.push("<li>");
        w.push("<span  class='sp_l' ></span>");


        w.push("<input type='button' value='确 定' onclick='SaveDetail(this)' OrderDetailId='" + OrderDetailId + "' style='  margin-left:10px;' />");
        w.push("</li>");

        w.push("</ul>");
        w.push("</div>");
        w.push("<div class='clr_10px'  ></div>");
        w.push("<div style='text-align:center;'>");




        w.push("</div>");
        PopWindow({
            html: w.join(""),
            title: "保存颜色"


        }, 500, 400);
        $("#div_Detail input").eq(0).focus();
        $("#div_Detail input").each(function () {


            $(this).keyup(function () {
                if (event.keyCode == 13) {

                    if ($(this).attr("type") == "button") {

                        $(this).click()
                    }
                    else {


                        var n = $(this).next().eq(0);

                        if (n.length > 0) {
                            n.focus();
                            n.select();
                        }
                        else {

                            n = $(this).parent().next().find("input").eq(0);
                            n.focus();
                            n.select();

                            if ($(n).attr("type") == "button") {

                                window.parent.BodyTip({

                                    text: "继续按下Enter,即将保存明细!",
                                    s: 5000

                                });
                            }

                        }
                    }


                }

            })




        });





    }






}

function NumOnfocus(obj) {
    $(obj).select();
}

function SaveOrderDetailVsClothesSize(obj) {
    var Num = ConvertToInt($(obj).val());
    var ClothesSizeId = $(obj).attr("ClothesSizeId");
    var OrderDetailId = $(obj).attr("OrderDetailId");

    AjaxPost("/ao/", "SaveOrderDetailVsClothesSize",
                                   {
                                       Num: Num,
                                       ClothesSizeId: ClothesSizeId,
                                       OrderDetailId: OrderDetailId,
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           window.parent.BodyTip({

                                               text: "数量已更改!",
                                               s: 1000

                                           });

                                           var OrderQuantity = 0;
                                           $(".txt_DetailVsClothesSize.Order").each(function () {

                                               Num = ConvertToInt($(this).val());

                                               OrderQuantity = accAdd(Num, OrderQuantity);

                                           });
                                           //   $("#sp_OrderQuantity").html(OrderQuantity);

                                           CountOrder();



                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);

}

function CountOrder() {

    AjaxPost("/ao/", "CountOrder",
        {
            OrderId: OrderId
        }, function (data) {
            var w = [];
            if (data.re == "ok") {
                $("#sp_OrderQuantity").html(data.Count.OrderQuantity);
                $("#sp_CheckQuantity").html(data.Count.CheckQuantity);
                $("#sp_WorkQuantity").html(data.Count.WorkQuantity);
                $("#sp_OrderVsMemberNum").html(data.Count.OrderVsMemberNum);
                $("#sp_OrderToWorkNum").html(data.Count.OrderToWorkNum);

                $("#sp_DoneQuantity").html(data.Count.DoneQuantity);
                $("#sp_MinQuantity").html("平均<b>" + data.Count.MinQuantity + "</b>" + OrderData.Info.Unit + "/人");

                CountOrderDetailTable(document.getElementById("tb_OrderDetail"));

            }
            else {
                alert(data.re)
            }


        }, false);
}

function CountPlaces() {

    //var ReleaseTypeId = ConvertToInt($("#sel_ReleaseType").val());

    //var Places = 0;

    //switch (ReleaseTypeId) {

    //    case 10: //拆单发布
    //        var OrderQuantity = ConvertToInt($("#sp_OrderQuantity").html());

    //        Places = accMul(Math.ceil(accDiv(OrderQuantity, 50)), 2);  //向上取整
    //        break;
    //    case 20:  //整单发布
    //        Places = 1
    //        break;
    //    default:

    //}

}

function SaveDetail(obj) {



    ClothesSizeArray = [];




    $(".li_c").each(function () {

        ClothesSizeArray.push({

            Num: $(this).find(".txt_Num").val(),
            Memo: $(this).find(".txt_Memo").val(),
            ClothesSizeId: $(this).attr("ClothesSizeId"),
            ClothesSizeName: $(this).attr("ClothesSizeName")

        });

    });

    var OrderDetailId = $(obj).attr("OrderDetailId");;




    AjaxPost("/ao/", "SaveOrderDetail",
                                   {
                                       ClothesSizeArray: JArrayToXmlStr(ClothesSizeArray),
                                       OrderId: OrderId,
                                       Color: $("#txt_Color").val(),
                                       OrderDetailId: OrderDetailId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           window.parent.BodyTip({

                                               text: "数量明细保存成功!",
                                               s: 5000

                                           });
                                           ClearPopWindow();
                                           GetOrderInfo({ NoInfo: true });

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);


}

// #endregion


// #region 工单

//新增一条工单
function ToAddOrderToWork(obj) {

    var MemberId = ConvertToInt($(obj).attr("MemberId"));

    AjaxPost("/ao/", "ToAddOrderToWork",
                                   {
                                       MemberId: MemberId,
                                       OrderId: OrderData.Info.OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           window.parent.BodyTip({
                                               text: "已为该用户成功增加空白工单",
                                               s: 5000

                                           });
                                           GetOrderToWorkList();
                                           ToSaveOrderToWork(data.OrderToWorkId);
                                       }
                                       else {
                                           alert(data.re)
                                       }
                                   }, false);

}

//弹出工单主体弹窗
function ToSaveOrderToWork(OrderToWorkId) {

    var str = "";
    var w = [];
    var otw = {};
    for (var i = 0; i < OrderToWorkData.OrderToWork.length; i++) {

        var j = OrderToWorkData.OrderToWork[i];
        if (j.OrderToWorkId == ConvertToInt(OrderToWorkId)) {
            otw = j;
            break;
        }

    }
    if (otw == {}) {

        alert("BUG:新工单没有插入或者没有获取到数据!");

        return;
    }


    w.push("<div class='div_SaveOrderToWork' id='div_SaveOrderToWork' ></div>");
    PopWindow({

        title: "编辑工单",
        html: w.join(""),
        close: true,
        shadow: true


    }, 800, 600);

    GetOrderToWorkInfo(OrderToWorkId);
    BindDateInput("txt_ReceivedTime", DateFormat(otw.ReceivedTime, "yyyy-MM-dd hh:mm:ss"), true);
    BindDateInput("txt_LimitTime", DateFormat(otw.LimitTime, "yyyy-MM-dd hh:mm:ss"), true);
    CountOrderToWork();

}

//获取工单主体
function GetOrderToWorkInfo(OrderToWorkId) {

    GetOrderToWorkList();

    AjaxPost("/ao/", "GetOrderToWorkInfo",
                                   {
                                       OrderToWorkId: OrderToWorkId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {


                                           if (OrderData.Detail.length > 0) {

                                               w = [];


                                               w.push("<ul>");
                                               w.push("<li>");
                                               w.push("<span>工单编号:</span>  " + OrderToWorkId + "");
                                               w.push("</li>");
                                               w.push("<li><span> </span></li>");
                                               w.push("<li>");

                                               w.push("<input type='hidden' id='hd_OrderToWorkId' Key='hd_OrderToWorkId' value='" + data.OrderToWork.OrderToWorkId + "'  />");
                                               w.push("<span>工单标题:</span>");



                                               w.push("<input id='txt_OrderToWorkTitle'  type='text' Key='OrderToWorkTitle' onblur='ChangeOrderToWork(this)' value='" + data.OrderToWork.OrderToWorkTitle + "' />");
                                               w.push("</li>");
                                               w.push("<li>");



                                               w.push("<span>交货时间:</span>");
                                               w.push("<input id='txt_LimitTime'  type='text' Key='LimitTime' onblur='ChangeOrderToWork(this)' value='" + data.OrderToWork.LimitTime + "' />");
                                               w.push("</li>");
                                               w.push("<li>");
                                               w.push("<span>单件工价(元)</span>");


                                               w.push("<input id='txt_Wages'  type='text' Key='Wages' onblur='ChangeOrderToWork(this)' value='" + data.OrderToWork.Wages + "'  ");

                                               if (data.OrderToWork.OrderToWorkStatusId >= 40) {
                                                   w.push(" disabled='disabled'  ");
                                               }

                                               w.push(" />");

                                               w.push("</li>");
                                               w.push("<li>");
                                               w.push("<span>领取裁片时间</span>");

                                               w.push("<input id='txt_ReceivedTime' Key='ReceivedTime' onblur='ChangeOrderToWork(this)' type='text' value='" + data.OrderToWork.ReceivedTime + "' />");
                                               w.push("</li>");
                                               w.push("</ul>")
                                               w.push("</div>");


                                               w.push("<div class='clr_19px'  ></div>");

                                               w.push("<table class='t3 tb_OrderToWorkDetail'id='tb_OrderToWorkDetail' >");   //开始书写表格

                                               w.push("<thead>");
                                               w.push("<tr>");
                                               w.push("<th style='width:170px;'>")
                                               w.push("颜色");
                                               w.push("</th>");
                                               for (var i = 0; i < OrderData.GroupClothesSize.length; i++) {

                                                   var j = OrderData.GroupClothesSize[i];
                                                   w.push("<th  >");
                                                   w.push(j.ClothesSizeName);
                                                   w.push("</th>");
                                               }
                                               w.push("</tr>");
                                               w.push("</thead>");


                                               w.push("<tbody>");
                                               //操作表格
                                               for (var i = 0; i < OrderData.Detail.length; i++) {

                                                   var j = OrderData.Detail[i];

                                                   w.push("<tr OrderDetailId='" + j.OrderDetailId + "' Color='" + j.Color + "'  >");
                                                   w.push("<td OrderDetailId='" + j.OrderDetailId + "' class='c'   >");
                                                   w.push(j.Color);
                                                   w.push("</td>");

                                                   for (var x = 0; x < OrderData.GroupClothesSize.length; x++) {

                                                       var y = OrderData.GroupClothesSize[x];



                                                       w.push("<td  OrderDetailId='" + j.OrderDetailId + "' class='c td_1' >");
                                                       var has = false;
                                                       for (var m = 0; m < OrderData.DetailVsClothesSize.length; m++) {

                                                           var n = OrderData.DetailVsClothesSize[m];
                                                           if (n.ClothesSizeId == y.ClothesSizeId && n.OrderDetailId == j.OrderDetailId) {


                                                               var p = 0; //已分派
                                                               var myp = 0;
                                                               var myCkp = 0;
                                                               var mydp = 0;
                                                               var OrderToWorkDetailId = 0;
                                                               if (OrderToWorkData.OrderToWorkDetailVsClothesSize.length > 0) {

                                                                   for (var v = 0; v < OrderToWorkData.OrderToWorkDetailVsClothesSize.length; v++) {

                                                                       var u = OrderToWorkData.OrderToWorkDetailVsClothesSize[v];       //OrderToWorkDetailVsClothesSize对象    u
                                                                       if (u.Color == j.Color && u.ClothesSizeId == n.ClothesSizeId) {

                                                                           if (u.OrderToWorkId == data.OrderToWork.OrderToWorkId) {

                                                                               //是这个颜色, 这个尺码, 而且也是本条工单

                                                                               myp = myp + u.Num;               //这条工单,这个尺码, 这个颜色, 我已经分派了多少
                                                                               myCkp = myCkp + u.CheckNum;      //这条工单, 这个尺码, 这个颜色, 我已经验收通过了多少
                                                                               OrderToWorkDetailId = u.OrderToWorkDetailId;
                                                                               mydp = mydp + u.DoneNum;
                                                                           }
                                                                           else {
                                                                               //是这个颜色, 这个尺码, 但是不是这个工单的

                                                                               p = p + u.Num;
                                                                           }






                                                                       }


                                                                   }
                                                               }


                                                               //w.push("<span>共<b class='b_gong'>" + n.Num + "</b>,已分派<b class='b_pai' >" + p + "</b>,可分派<b class='b_yu' >" + accSubtr(n.Num, p) + "</b></span>");


                                                               if (data.OrderToWork.OrderToWorkStatusId == 10) {
                                                                   // #region 分派数量


                                                                   w.push("<span>可分派<b class='b_yu' >" + accSubtr(n.Num, p) + "</b></span>");
                                                                   w.push("<div class='clr' ></div>");





                                                                   w.push("分派:<input type='text' value='" + myp + "' class='txt_AllotOrderToWork c' Color='" + j.Color + "' ClothesSizeName='" + y.ClothesSizeName + "'   OrderDetailId='" + j.OrderDetailId + "'  ClothesSizeId='" + n.ClothesSizeId + "'  MemberId='" + data.OrderToWork.MemberId + "' ");

                                                                   if (data.OrderToWork.OrderToWorkStatusId >= 40) {

                                                                       w.push("  disabled='disabled' ");
                                                                   }

                                                                   w.push(" max='" + accSubtr(n.Num, p) + "' onkeyup='onlyNum(this,{max:" + accSubtr(n.Num, p) + "})'  /> ");



                                                                   // #endregion
                                                               }
                                                               else if (data.OrderToWork.OrderToWorkStatusId == 30) {
                                                                   // #region 完成数量

                                                                   w.push("<div class='clr' ></div>");

                                                       
                                                                   if (myp > 0) {
                                                                       w.push("<span>");
                                                                       w.push("分派:<b>" + myp + "</b>");
                                                                       w.push("</span>");
                                                                       w.push(" 完成:<input type='text' class='txt_DoneNum' value='" + mydp + "' Color='" + j.Color + "'     ClothesSizeId='" + n.ClothesSizeId + "' Max='" + myp + "'  OrderToWorkDetailId='" + OrderToWorkDetailId + "'  ");   //完成数量
                                                                       w.push("  onchange='SaveDoneNum(this)'  /> ");
                                                                   }
                                                                   else {

                                                                       w.push("<span class=\"sp_w\">-</span>");
                                                                   }






                                                                   // #endregion
                                                               }
                                                               else if (data.OrderToWork.OrderToWorkStatusId == 40) {
                                                                   // #region 质检数量


                                                                   if (mydp == 0) {
                                                                       w.push("<span class=\"sp_w\">-</span>");
                                                                   }
                                                                   else {

                                                                       var NoCheckNum = accSubtr(mydp, myCkp);


                                                                       w.push("<div class='clr' ></div>");


                                                                       if (NoCheckNum == 0) {
                                                                           w.push("<span>完成:" + mydp + "</span>  ");

                                                                           w.push("<span>合格:" + myCkp + "</span>");
                                                                       }
                                                                       else {
                                                                           w.push("<span>还剩:" + NoCheckNum + "</span>  ");
                                                                           w.push(" 合格:<input type='text' class='txt_CheckNum' value='" + NoCheckNum + "' ClothesSizeId='" + n.ClothesSizeId + "'  OrderToWorkDetailId='" + OrderToWorkDetailId + "'   ");

                                                                           w.push(" max='" + NoCheckNum + "' onkeyup='onlyNum(this,{max:" + NoCheckNum + "})'  /> ");

                                                                       }


                                                                   }

                                                                   // #endregion
                                                               }
                                                               else {

                                                                   w.push("<span>分派:<b>" + myp + "</b></span>");
                                                                   w.push("<span>完成:<b>" + mydp + "</b></span>");
                                                                   w.push("<span>质检:<b>" + myCkp + "</b></span>");
                                                               }









                                                               has = true;

                                                           }



                                                       }
                                                       if (!has) {
                                                           w.push("<span class='sp_w' >无</span>");
                                                           //w.push("<div class='clr' ></div>");
                                                           //w.push(" <input type='text'  value='0' class='txt_DetailVsClothesSize c'  OrderDetailId='" + j.OrderDetailId + "'  ClothesSizeId='" + y.ClothesSizeId + "'  ")

                                                       }

                                                       w.push("</td>");
                                                   }

                                                   w.push("</tr>");


                                               }


                                               w.push("</tbody>");


                                               w.push("</table>");
                                               w.push("<div class='clr_20px' ></div>");

                                               w.push("<div>");
                                               w.push("<span class='sp_OrderToWorkCount  Quantity' >");
                                               w.push("总数:<b id='b_Quantity' >0</b>");
                                               w.push("</span>");

                                               w.push("<span class='sp_OrderToWorkCount CheckQuantity' >");
                                               w.push("通过质检:<b id='b_CheckQuantity' >0</b>;");
                                               w.push("</span>");
                                               w.push("<span class='sp_OrderToWorkCount OrderToWorkStatus' >当前状态:<b>" + data.OrderToWork.OrderToWorkStatusName + "</b></span>");








                                               w.push("<div class='div_b' >");


                                               if (data.OrderToWork.OrderToWorkStatusId == 10) {
                                                   //派单状态

                                                   w.push("<input type='button'  value='分派工单' onclick='AllotOrderToWork()' id='btn_closeOrderToWork'  />");
                                                   w.push("  <input type='button'  value='取消分派' onclick='ClearMemberOrderToWork(this)' id='btn_closeOrderToWork'  />");

                                               }
                                               else if (data.OrderToWork.OrderToWorkStatusId == 30) {
                                                   w.push("<input type='button'  value='全部完成' onclick='ALLDoneOrder()' id='btn_closeOrderToWork'  />   ");
                                                   w.push("   <input type='button'  value='确认提交' onclick='DoneOrderToWork()' id='btn_closeOrderToWork'  />");
                                               }
                                               else if (data.OrderToWork.OrderToWorkStatusId == 40) {
                                                   w.push("<input type='button'  value='提交质检结果' onclick='SaveCheckNumArray()' id='btn_closeOrderToWork'  />");


                                                   w.push("  <input type='button'  value='完成质检' onclick='CheckOrderToWork()' id='btn_closeOrderToWork'  />");
                                               }
                                               else if (data.OrderToWork.OrderToWorkStatusId == 50) {
                                                   w.push("<input type='button'  value='已交付成衣' onclick='PayOrder()' id='btn_closeOrderToWork'  />");

                                               }
                                               else if (data.OrderToWork.OrderToWorkStatusId == 60) {
                                                   w.push("<input type='button'  value='结算工单' onclick='EndOrderToWork()' id='btn_closeOrderToWork'  />");

                                               }


                                               w.push("</div>");

                                           }

                                           $("#div_SaveOrderToWork").html(w.join(""));

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);



}

function ALLDoneOrder()
{

    $(".txt_DoneNum").each(function () {

        var max = $(this).attr("Max");

        $(this).val(max);
        $(this).change();

    })



}


function ClearMemberOrderToWork(obj)
{




    AjaxPost("/ao/", "ClearMemberOrderToWork",
                                         {
                                             OrderToWorkId: $("#hd_OrderToWorkId").val()
                                     

                                         }, function (data) {
                                             
                                             ClearPopWindow();
                                             BindPageSetting();
                                         }, false);



}

//结算订单
function EndOrderToWork(obj) {


    var OrderToWorkId = $("#hd_OrderToWorkId").val();
    AjaxPost("/ao/", "EndOrderToWork",
                                   {
                                       OrderToWorkId: OrderToWorkId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           ClearPopWindow();
                                           BindPageSetting();

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);

}

//已(对上游订单客户)交付成衣
function PayOrder(obj) {


    var OrderToWorkId = $("#hd_OrderToWorkId").val();
    AjaxPost("/ao/", "PayOrder",
                                   {
                                       OrderToWorkId: OrderToWorkId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           ClearPopWindow();
                                           BindPageSetting();

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);

}

//已(对上游订单客户)交付成衣
function PayOrder(obj) {


    var OrderToWorkId = $("#hd_OrderToWorkId").val();
    AjaxPost("/ao/", "PayOrder",
                                   {
                                       OrderToWorkId: OrderToWorkId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           ClearPopWindow();
                                           BindPageSetting();

                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);

}

//分派工单
function AllotOrderToWork() {
    var OrderToWorkId = $("#hd_OrderToWorkId").val();

    var ColorArray = [];
    var VsArray = [];
    $(".txt_AllotOrderToWork").each(function () {
        var Color = $(this).attr("Color");
        var Num = ConvertToInt($(this).val());
        var ClothesSizeId = ConvertToInt($(this).attr("ClothesSizeId"));

        ColorArray.push({

            Color: Color
        });


        if (Num > 0) {
            VsArray.push({
                Color: Color,
                Num: Num,
                ClothesSizeId: ClothesSizeId

            });

        }



    });

    ColorArray = ColorArray.unique4();


    AjaxPost("/ao/", "AllotOrderToWork",
                                   {
                                       ColorArray: JArrayToXmlStr(ColorArray),
                                       VsArray: JArrayToXmlStr(VsArray),
                                       OrderToWorkId: OrderToWorkId

                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           alert("分派完成!");
                                           ClearPopWindow();
                                           BindPageSetting();
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);

}


//完成质检(质检过程是分段提交, 单独完成)
function CheckOrderToWork() {
    var OrderToWorkId = $("#hd_OrderToWorkId").val();
    AjaxPost("/ao/", "CheckOrderToWork",
                                   {
                                       OrderToWorkId: OrderToWorkId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           GetOrderToWorkList();

                                           GetOrderToWorkInfo(OrderToWorkId);


                                       }
                                       else {
                                           alert(data.re)
                                       }
                                   }, false);
}

//提交质检结果
function SaveCheckNumArray() {
    var CheckNumArray = [];



    $(".txt_CheckNum").each(function () {

        CheckNumArray.push({

            CheckNum: ConvertToInt($(this).val()),
            OrderToWorkId: $("#hd_OrderToWorkId").val(),
            OrderToWorkDetailId: $(this).attr("OrderToWorkDetailId"),
            ClothesSizeId: $(this).attr("ClothesSizeId")

        });

    });
    if (CheckNumArray.length == 0) {
        alert("没有可以提交的数据!");

        return;
    }
    var OrderToWorkId = $("#hd_OrderToWorkId").val();
    AjaxPost("/ao/", "SaveCheckNumArray",
                                   {
                                       CheckNumArray: JArrayToXmlStr(CheckNumArray),
                                       OrderToWorkId: OrderToWorkId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {

                                           GetOrderToWorkInfo(OrderToWorkId);
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);


}

//确认完成
var loading = false;
function DoneOrderToWork() {

    //alert("请等待工人员提交完成!");
    //ClearPopWindow();
    //return;

    if (!confirm("确定完成吗? 完成之后工单将进入质检环节!")) {
        return;
    }
    if (loading) {
        alert("请不要重复点击结算");
        return;
    }

    loading = true;
    AjaxPost("/ao/", "DoneOrderToWork",
                                   {
                                       OrderToWorkId: $("#hd_OrderToWorkId").val()
                                   }, function (data) {
                                       var w = [];
                                       loading = false;
                                       if (data.re == "ok") {
                                           window.parent.BodyTip({

                                               text: "工单已经完成生产, 进入质检环节!",
                                               s: 2000

                                           });

                                           GetOrderToWorkInfo($("#hd_OrderToWorkId").val());
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);
}

function CountOrderToWork() {

    AjaxPost("/ao/", "CountOrderToWork",
                                   {
                                       OrderToWorkId: $("#hd_OrderToWorkId").val()
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           $("#b_CheckQuantity").html(data.Count.CheckQuantity);
                                           $("#b_Quantity").html(data.Count.Quantity);
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);
}


function ChangeOrderToWork(obj) {


    AjaxPost("/ao/", "ChangeOrderToWork",
                           {
                               Key: $(obj).attr("Key"),
                               Val: $(obj).val(),

                               OrderToWorkId: $("#hd_OrderToWorkId").val()
                           }, function (data) {
                               var w = [];
                               if (data.re == "ok") {
                                   window.parent.BodyTip({

                                       text: "成功修改!",
                                       s: 2000

                                   });
                               }
                               else {
                                   alert(data.re)
                               }


                           }, false);





}

function GetOrderToWorkList() {




    AjaxPost("/ao/", "GetOrderToWorkList",
                                   {
                                       OrderId: OrderId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           OrderToWorkData = data;
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                   }, false);

}

function SaveDoneNum(obj) {
    var OrderToWorkDetailId = $(obj).attr("OrderToWorkDetailId");
    var ClothesSizeId = $(obj).attr("ClothesSizeId");
    var DoneNum = $(obj).val();
    AjaxPost("/ao/", "SaveDoneNum",
                                   {
                                       OrderToWorkDetailId: OrderToWorkDetailId,
                                       ClothesSizeId: ClothesSizeId,
                                       DoneNum: DoneNum,
                                       DoneLogTypeId: 20
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           CountOrderToWork();
                                       }
                                       else {
                                           alert(data.re);
                                       }


                                   }, false);
}



function SaveOrderToWorkDetail(obj) {



    var td = $(obj).parent();
    var objCheckNum = td.find(".txt_CheckNum");
    var objDetailVsClothesSize = td.find(".txt_DetailVsClothesSize");
    var Color = objDetailVsClothesSize.attr("Color");
    var MemberId = ConvertToInt(objDetailVsClothesSize.attr("MemberId"));
    var Num = ConvertToInt(objDetailVsClothesSize.val());
    var CheckNum = ConvertToInt(objCheckNum.val());
    var OrderToWorkId = $("#hd_OrderToWorkId").val();
    var ClothesSizeId = objDetailVsClothesSize.attr("ClothesSizeId");
    var ClothesSizeName = objDetailVsClothesSize.attr("ClothesSizeName");

    var objYu = td.find(".b_yu");    //余数
    var objPai = td.find(".b_pai");   //已派单
    var objGong = td.find(".b_gong");

    if (Num > ConvertToInt(objYu.html())) {

        window.parent.BodyTip({

            text: "没有足够的剩余订单可分派",
            s: 2000

        });
        objDetailVsClothesSize.val(objYu.html());
        objDetailVsClothesSize.focus();
        objDetailVsClothesSize.select();
        objDetailVsClothesSize.change();
        return;
    }
    else {
        if (CheckNum > Num) {
            window.parent.BodyTip({

                text: "质检数量不能大于分派数量",
                s: 2000

            });
            objCheckNum.val(Num);
            objCheckNum.focus();
            objCheckNum.select();
            objDetailVsClothesSize.change();
            return;

        }
        else {

            AjaxPost("/ao/", "SaveOrderToWorkDetail",
                                           {
                                               Color: Color,
                                               MemberId: MemberId,
                                               Num: Num,
                                               OrderToWorkId: OrderToWorkId,
                                               ClothesSizeId: ClothesSizeId,
                                               CheckNum: CheckNum
                                           }, function (data) {
                                               var w = [];
                                               if (data.re == "ok") {
                                                   window.parent.BodyTip({

                                                       text: "颜色[" + Color + "],尺码[" + ClothesSizeName + "],数量[" + Num + "]已经保存",
                                                       s: 2000

                                                   });

                                                   var pai = 0;
                                                   var yu = 0;
                                                   var gong = ConvertToInt(objGong.html());
                                                   for (var i = 0; i < OrderToWorkData.OrderToWorkDetailVsClothesSize.length; i++) {

                                                       var j = OrderToWorkData.OrderToWorkDetailVsClothesSize[i];
                                                       if (Color == j.Color && ClothesSizeId == j.ClothesSizeId && OrderToWorkId == j.OrderToWorkId && MemberId != j.MemberId) {

                                                           pai = pai + j.Num;
                                                       }
                                                   }

                                                   pai = accAdd(pai, Num);
                                                   CountOrderToWork();
                                                   //   objPai.html(pai);
                                                   // objYu.html(accSubtr(gong, pai));
                                               }
                                               else {
                                                   alert(data.re)
                                               }

                                           }, false);
        }
    }






}



// #endregion







