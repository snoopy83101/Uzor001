/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../ZYUiPub.js" />
var iMarkerPoint;

$(function () {


    BindTab();
    BindMap();
    BindPageSetting();

});



//#region 绑定主体内容


function BindPageSetting() {



    var PathJson = {};
    PathJson["首页"] = "/default.aspx";
    PathJson["我的首页"] = "/myInfo/";
    PathJson["我的商家 > 设置"] = "#";
    Breadcrumbs(PathJson);

    var MerId = GetQueryString("MerId");
    $("#sel_MerType").change();

    if (MerId == 0 || MerId == "") {//添加

        $("#div_left").hide();


    }
    else { //修改


        $("#div_left").show();

        $("#txt_MerName").val(MerJson.MerchantName);
        $("#txt_MerMemo").html(MerJson.MerchantMemo);
        // $("#sel_MerType").val(MerJson.MerchantTypeId);
        $("#img_1").attr("src", MerJson.LogoUrl);
        $("#img_1").attr("ImgId", MerJson.Logo);
        $("#sel_Town").val(MerJson.TownId);
        $("#txt_MerchantId").val(localStorage.MerId);

        //  UE.getEditor("txt_MerchantContent").setContent("123123123",false);
        // $("#txt_MerchantContent").text(MerJson.MerchantContent);
        $("#txt_Name").val(MerJson.Name);
        $("#txt_phone").val(MerJson.Phone);
        $("#txt_tell").val(MerJson.Tell);
        $("#txt_qq").val(MerJson.qq);
        $("#txt_Email").val(MerJson.Email);
        $("#txt_WebSite").val(MerJson.WebSite);
        $("#txt_Address").val(MerJson.Address);
        SetMerType();
    }

    BindMap(MerJson.Lng, MerJson.Lat);

    LookLogo();
    BindBranchList();
}

function SetMerType() {   //设置所选择的行业
    var w = new Array();
    for (var i = 0; i < VsMerTypeList.length; i++) {
        var j = VsMerTypeList[i];
        w.push("<a class='a_selMerType' MerchantTypeId='" + j.MerchantTypeId + "'  >" + j.MerchantTypeName + "");
        w.push("<b class='b_close' onclick='CloseMerType(this)' >x</b>")
        w.push("</a>");
    }
    $("#sp_selMerType").append(w.join(""));
}

//#endregion


function SetMerType() {   //设置所选择的行业
    var w = new Array();
    for (var i = 0; i < VsMerTypeList.length; i++) {
        var j = VsMerTypeList[i];
        w.push("<a class='a_selMerType' MerchantTypeId='" + j.MerchantTypeId + "'  >" + j.MerchantTypeName + "");
        w.push("<b class='b_close' onclick='CloseMerType(this)' >x</b>")
        w.push("</a>");
    }
    $("#sp_selMerType").append(w.join(""));
}

function GetMerType() {   //获得所选择的行业
    if ($(".a_selMerType").length == 0) {

        throw ("您至少选择一门行业");
    }

    VsMerTypeList = new Array();
    $(".a_selMerType").each(function () {

        var MerchantTypeId = $(this).attr("MerchantTypeId");

        var MerchantTypeName = $(this).text().replace("x", "");
        VsMerTypeList.push({
            MerchantTypeId: MerchantTypeId,
            MerchantTypeName: MerchantTypeName
        });
    });

    return JArrayToXmlStr(VsMerTypeList);
}



function SaveMerchant() {

    var MerchantTypeTarget = "";
    var a = new Array();
    $(".a_selMerType").each(function () {
        var txt = $(this).not("b").text();
        txt = txt.replace("x", "");
        a.push(txt);
    });



    MerchantTypeTarget = a.join(",");


    try {
        AjaxPost("/am/", "SaveMyMerchant",
       {
           MerchantId: $("#txt_MerchantId").val(),
           InputCode: MerJson.InputCode,
           MerchantName: $("#txt_MerName").val(),
           MerchantMemo: $("#txt_MerMemo").val(),



           MerchantContent: encodeURIComponent(ed.getContent()),
           MerchantClassId: 0,
           MerchantTypeId: 0,   //预留了,木有用了.
           Lng: iMarkerPoint.lng,
           Lat: iMarkerPoint.lat,
           WebSite: $("#txt_WebSite").val(),
           Logo: $("#img_1").attr("ImgId"),
           TownId: $("#sel_Town").val(),
           VsMerTypeList: GetMerType(),
           Tell: $("#txt_tell").val(),
           qq: $("#txt_qq").val(),
           Email: $("#txt_Email").val(),
           Address: $("#txt_Address").val(),
           Phone: $("#txt_phone").val(),
           Name: $("#txt_Name").val(),
           MerchantTypeTarget: MerchantTypeTarget,
           imgArray: JArrayToXmlStr(imgArray)

       }, function (data) {
           if (data.re == "ok") {

               alert("保存成功,点击确定刷新页面!");

               tiaozhuan("/Merchant/SaveMer.aspx?MerId=" + data.MerId + "");

           }
           else {

               alert(data.re);
           }

       });
    } catch (e) {
        alert(e);
        return;
    }

}

function ToBack() {
    fanhui();
}


//当行业选项改变时
function changeMerType(obj) {

    var CurrentMerTypeId = $(obj).val();

    AjaxPost("/am/", "GetMerType",
        {
            PMerTypeId: CurrentMerTypeId

        }, function (data) {

            if (data.re == "ok") {

                var l = data.MerTypeList.Table;
                var w = new Array();
                for (var i = 0; i < l.length; i++) {
                    var j = l[i];
                    w.push("<li MerchantTypeId='" + j.MerchantTypeId + "' onclick='SelMerType(this)' >");
                    w.push(j.MerchantTypeName);
                    w.push("</li>");
                }
                $("#ul_merType").html(w.join(""));
            }
            else {
                alert(data.re);
            }

        });

}


//选择商家行业
function SelMerType(obj) {
    $(obj).siblings().removeClass("select");
    $(obj).addClass("select");
    var MerchantTypeId = $(obj).attr("MerchantTypeId");
    var MerchantTypeName = $(obj).html();
    var w = new Array();
    w.push("<a class='a_selMerType' MerchantTypeId='" + MerchantTypeId + "'  >" + MerchantTypeName + "");
    w.push("<b class='b_close' onclick='CloseMerType(this)' >x</b>")
    w.push("</a>");
    if ($(".a_selMerType").length > 2) {
        alert("最多只能选择3条行业");
        return;
    }

    if ($(".a_selMerType[MerchantTypeId='" + MerchantTypeId + "']").length > 0) {
        //如果已经有了
        return;
    }
    else {



        $("#sp_selMerType").append(w.join(""));
    }
}

function CloseMerType(obj) {

    $(obj).parent().remove();
}



//#region  地图相关



function BindMap(lng, lat) {

    if (lng == null || lat == null) {
        lng = 118.163651;
        lat = 36.197684;

    }

    var map = new BMap.Map("div_Map", { enableMapClick: false });
    var point = new BMap.Point(lng, lat);
    map.centerAndZoom(point, 17);   //设置中心点,以及缩放比例


    iMarker = new BMap.Marker(point);  // 创建标注
    map.addOverlay(iMarker);


    //iMarker = new BMap.Marker(point);  // 创建标注


    //iMarker.addEventListener("dragend", function () {
    //    iMarkerPoint = this.getPosition();  //获取一个点



    //});
    iMarker.addEventListener("dragend", function () {
        iMarkerPoint = this.getPosition();  //获取一个点



    });


    iMarker.enableDragging();    //可拖拽
    //map.addOverlay(iMarker);
    iMarkerPoint = iMarker.getPosition();  //获取当前点坐标
    map.enableScrollWheelZoom();    //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用
    map.addEventListener("click", function (e) {

        if (e.overlay != null) {
            return;
        }
        else {
            if (iMarker != null) {
                map.removeOverlay(iMarker);

            }

        }
        iMarkerPoint.lng = e.point.lng;
        iMarkerPoint.lat = e.point.lat;
        iMarker = new BMap.Marker(e.point);  // 创建标注
        map.addOverlay(iMarker);
        iMarker.enableDragging();    //可拖拽
        //map.addOverlay(iMarker);
        iMarkerPoint = iMarker.getPosition();  //获取当前点坐标
        iMarker.addEventListener("dragend", function () {
            iMarkerPoint = this.getPosition();  //获取一个点



        });
    });

}


//#endregion




function LookLogo() {

    //var ImgUrl = $("#a_Logo").attr("ImgUrl");
    //$("#a_Logo").hover(function () {

    //    $("#img_logo").attr("src", $("#a_Logo").attr("ImgUrl"));
    //    $("#img_logo").show();

    //}, function () {

    //    $("#img_logo").hide();
    //});

    $("#btn_logo").click(function () {

        OpImgCutting("4|3");

    });

}


function SaveImg(j) {

    $("#img_1").attr("src", j.ImgUrl);
    $("#img_1").attr("ImgId", j.ImgId);

    $("#img_1").hover(function () {
        $(this).addClass("");

    }, function () {
        $(this).addClass("");
    });
}

// #region 分部相关

function BindBranchList() {



    AjaxPost("/am/", "GetBranchList",
                                   {
                                       MerId: localStorage.MerId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];
                                               w.push("<tr ondblclick='ToAddBranch(this)' " + JsonToParaStr(j) + " >");
                                               w.push("<td>" + j.BranchId + "</td>");
                                               w.push("<td>" + j.BranchName + "</td>");
                                               w.push("<td>" + j.BranchMemo + "</td>");
                                               w.push("</tr>");
                                           }
                                       }
                                       else {
                                           alert(data.re)
                                       }

                                       $("#tbody_Branch").html(w.join(""));
                                       // ZyPagerSetting("", CurrentPage, data.t, "1");
                                   }, false);
}

function ToAddBranch(obj) {

    var url = "SaveBranch.aspx?MerId=" + localStorage.MerId + "";
    if (obj) {
        var BranchId = $(obj).attr("BranchId");
        url = url + "&BranchId=" + BranchId;
    }
    else {

    }

    window.open(url);

}

// #endregion
