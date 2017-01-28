

/// <reference path="/Script/jquery-1.8.2.js" />
/// <reference path="/Script/ZYUiPub.js" />
/// <reference path="/script/common.js" />



$(function () {

    BindPageSetting();

})

function BindPageSetting() {

    GetAdList(1);
}



function PopSaveAd(obj) {

    ClearPopWindow();

    var _id = "";

    if (obj) {

        _id = $(obj).attr("_id");
    }


    var w = [];

    w.push("<ul class='ul_SaveAd'>");

    w.push("<li>");
    w.push("<input id='hd_id'  type='hidden' value='" + _id + "' />");


    w.push("<span>广告位置:</span>");
    w.push("<div class='div_Loction' id='div_Loction' >");
    w.push("<a LoctionId='appjob_root_home_banner' Client='appjob' WinName='root' FrameName='home' Loction='banner' >");
    w.push("工人端首页banner轮播");
    w.push("</a>");

    w.push("<a LoctionId='appjob_root_afterRoot' Client='appjob' WinName='root' FrameName='None' Loction='afterRoot' >");
    w.push("工人端框架之前全屏");
    w.push("</a>");
    w.push("</div>");
    w.push("</li>");

    w.push("<li>");
    w.push("<span>广告标题:</span>");
    w.push("<input id='txt_Title' type='text' value=''  />");
    w.push("</li>");

    w.push("<li>");
    w.push("<span>广告图片:</span>");
    w.push("<input type=\"file\" id=\"img_file\" class='' />");
    w.push("<img id='img_ad' class='img_ad' src='' />");
    w.push("</li>");

    w.push("<li>");
    w.push("<span>窗体名称:</span>");
    w.push("<input id='txt_winName' type='text' value=''  />");
    w.push("</li>");

    w.push("<li>");
    w.push("<span>页面路径:</span>");
    w.push("<input id='txt_url' type='text' value='' />");
    w.push("</li>");

    w.push("<li>");
    w.push("<span>显示/作废:</span>");
    w.push("<select id='sel_Invalid' >");
    w.push("<option value='false' >显示</option>");
    w.push("<option value='true' >作废</option>");
    w.push("</select>");
    w.push("</li>");

    w.push("</ul>");
    w.push("<div class='clr_10px' ></div>");
    w.push("<div class='c' >");
    w.push("<input type='button' value=' 保 存 '  onclick='SaveAd()'  />");
    w.push("</div>");


    PopWindow({
        title: "保存广告",
        html: w.join("")

    }, 500, 300)



    if (_id != "") {



        AjaxPost("/ads/", "GetAdInfo",
                                             {
                                                 _id: _id

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     $("#txt_Title").val(data.info.Title);
                                                     $("#txt_winName").val(data.info.To.WinName);
                                                     $("#txt_url").val(data.info.To.WinUrl);
                                                     $("#img_ad").attr("src", data.info.ImgUrl);
                                                     $("#img_ad").attr("ImgId", data.info.ImgId);
                                                     $("#sel_Invalid").val(ConvertToString(data.info.Invalid));
                                                     $("#div_Loction>a[LoctionId='" + data.info.LoctionId + "']").addClass("select");

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }


                                             }, false);



    }

    $("#img_file").change(function () {
        Upfile();
    })

    $("#div_Loction>a").click(function () {

        $(this).siblings().removeClass("select");
        $(this).addClass("select");

    })


}

function Upfile() {
    var imageFile = document.getElementById("img_file").files[0];

    if (!imageFile) {
        alert("没有选择图片文件?");
        return;
    }

    var form = new FormData;
    form.append('file', imageFile);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/ac?para=FilePost", true);
    xhr.send(form);
    xhr.onload = function (e) {
        if (this.status == 200) {
            var data = eval('(' + this.responseText + ')');
            var j = ConvertToJson(this.responseText);
            // #region 发送图文消息


            //  alert(this.responseText);

            if (j.ImgId) {
                if (j.ImgId != "") {  //真的传上去了,然后再删除
                    DelImg({

                        ImgId: $("#img_ad").attr("ImgId"),

                        ImgUrl: $("#img_ad").attr("src")
                    })

                }
            }



            $("#img_ad").attr("ImgId", j.ImgId);
            $("#img_ad").attr("src", j.ImgUrl);
            // alert("上传图片成功!");
            // #endregion
        }
    }
}



function SaveAd() {


    var selLoction = $("#div_Loction>a.select");

    if (selLoction.length != 1) {

        alert("必须选择一个广告位置");

        return;
    }



    var jdata = {
        _id: $("#hd_id").val(),
        Client: selLoction.attr("Client"),   //app
        WinName: selLoction.attr("WinName"),
        FrameName: selLoction.attr("FrameName"),
        Loction: selLoction.attr("Loction"),
        LoctionId: selLoction.attr("LoctionId"),
        LoctionName: selLoction.text(),
        MerId: localStorage.MerId,  //优做杭州工厂的编号为1999
        Title: $("#txt_Title").val(),
        ImgId: $("#img_ad").attr("ImgId"),
        ImgUrl: $("#img_ad").attr("src"),
        Memo: "",
        TypeId: "openW",
        TypeName: "打开广告窗口",
        Invalid: ConvertToBool($("#sel_Invalid").val()),
        To: {
            WinName: $("#txt_winName").val(),
            WinUrl: $("#txt_url").val()

        }

    };


    AjaxPost("/ads/", "SaveAd",
                                         {
                                             data: JSON.stringify(jdata)

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 ClearPopWindow();

                                                 GetAdList(1);

                                             }
                                             else {
                                                 alert(data.re)
                                             }

                                         }, false);


}


function GetAdList(CurrentPage) {




    var b = {
       

    };

    AjaxPost("/ads/", "GetAdList",
                                         {
                                             b: JSON.stringify(b)

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 if (data.list.length > 0) {
                                                     for (var i = 0; i < data.list.length; i++) {
                                                         var j = data.list[i];
                                                         w.push("<tr _id='" + j._id + "' ondblclick='PopSaveAd(this)'  >");
                                                         w.push("<td>");
                                                         w.push(j.Title);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.Client);
                                                         w.push(">");
                                                         w.push(j.WinName);
                                                         w.push(">");
                                                         w.push(j.FrameName);
                                                         w.push(">");
                                                         w.push(j.Loction);
                                                         w.push("</td>");
                                                         w.push("<td>");
                                                         w.push(j.ChangeTime);
                                                         w.push("</td>");
                                                         w.push("<td  class='c' >");

                                                         if (j.Invalid) {

                                                             w.push("作废");
                                                         }
                                                         else {

                                                             w.push("显示");
                                                         }


                                                         w.push("</td>");
                                                         w.push("</tr>");


                                                     }
                                                 }

                                                 $("#tb_ad").html(w.join(""));


                                                 $("#tb_ad>tr").each(function () {




                                                     /// <reference path="/Script/jquery-1.8.2.js" />
                                                     /// <reference path="/Script/ZYUiPub.js" />
                                                     /// <reference path="/script/common.js" />

                                                     r = [];




                                                     r.push({
                                                         evName: "DeleteAd",
                                                         Title: "删除",
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

function DeleteAd(obj) {

    var _id = $(obj).attr("_id");


    AjaxPost("/ads/", "DeleteAd",
                                         {
                                             _id: _id

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {


                                                 $(obj).remove();


                                             }
                                             else {
                                                 alert(data.re)
                                             }
                                         }, false);


}