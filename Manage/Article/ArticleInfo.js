/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../ZYUiPub.js" />




$(function () {


    InputCheck("txt_ArticleTitle", "NotNull", "必填", "文章标题,必须填写");
    InputCheck("txt_ArticleSummary", "NotNull", "必填", "文章副标题,必须填写");

    BindDateInput("txt_CreateTime");
    GetArtice();


    $("#txt_ArticleTitle").blur(function () {  //如果标题失去焦点, 副标题自动获得,并且检测副标题如果没有内容,自动填写主标题的内容.

        if ($("#txt_ArticleSummary").val() != "") {
            $("#txt_ArticleSummary").val($(this).val());
        }

    });

    ReadyPageSetting();


});


function ToList() {


}


//从编辑器传过来的方法
function ChangeIndexImg(j) {

    SaveImg(j);
}


function SaveImg(j) {

    $("#img_atricleImg").attr("src", j.ImgUrl);
    $("#img_atricleImg").attr("ImgId", j.ImgId);
}

function ReadyPageSetting() {
    if ($("#sel_ArticleClass").children().length == 0) {
        ZyPopWindow("您还没有一个新闻类别,请先维护新闻类别!", { 现在去添加新闻类别: "do" }, 200, 80, "CallBack", 200);
    }


    if (GetQueryString("ArticleId") == "") {
        //如果是新增则执行, 修改就不执行
        if (localStorage.ArticleClassId) {
            var b = false;
            $("#sel_ArticleClass>option").each(function () {
                //如果第一类别内包含当前缓存的classid的话, 就赋值吧. 不包含就算了.

                if ($(this).attr("value") == localStorage.ArticleClassId) {
                    b = true;
                }
            })
            if (b) {
                $("#sel_ArticleClass").val(localStorage.ArticleClassId).change();
                setTimeout(function () {
                    localStorage.ArticleClassId2 = $("#sel_ArticleClass_1").val();
                }, 1000);
            }
        }
    }







}

function CallBack(m, v) {
    tiaozhuan("/MyMerArticleClass/?MerId=" + MerId);
}



function BindChildArtClass(obj, cldLv) {

    if (cldLv == null) {
        cldLv = 1;
    }

    var MyId = $(obj).attr("id");
    var CldId = MyId + "_" + cldLv + "";

    $("#" + CldId + "").remove();
    var ParentArticleClassId = $(obj).val();
    if (ParentArticleClassId == "0") {
        return;
    }

    AjaxPost("/aar/", "GetArticleClassList", {
        ParentArticleClassId: ParentArticleClassId
    }, function (data) {

        if (data.re == "ok") {
            if (data.ArtClassArray.length == 0) {
                return;
            }
            var w = new Array();
            w.push("<select id='" + CldId + "'  cldLv='" + (cldLv + 1) + "'  onchange='BindChildArtClass(this," + (cldLv + 1) + ")' >");
            //    w.push("<option value='0' >全部子类别</option>")
            for (var i = 0; i < data.ArtClassArray.length; i++) {
                var j = data.ArtClassArray[i];
                w.push("<option value='" + j.ArticleClassId + "'>");
                w.push(j.ArticleClassName);
                w.push("</option>");
            }
            w.push("</select>");
            $(obj).after(w.join(""));
        }
        else {
            alert(data.re);
        }

    });
}


function GetArtice() {

    $("#sel_ArticleClass").change();
    if (GetQueryString("ArticleId") == "") {

        return;
    }




    var j = ArticleJson;
    $("#tr_ArticleId").show();
    $("#txt_ArticleId").val(j.ArticleId);
    $("#txt_ArticleTitle").val(j.ArticleTitle);
    $("#cb_invalid").attr("checked", j.Invalid);
    $("#txt_ArticleSummary").val(j.ArticleSummary);
    $("#txt_ArticleSource").val(j.ArticleSource);
    $("#txt_CreateTime").val(DateFormat(j.CreateTime));
    $("#txt_Author").val(j.Author);
    $("#txt_Memo").val(j.Memo);

    //    SetChildSelectVal("sel_ArticleClass", j.ParentArticleClassId).change();
    $("#sel_ArticleClass").val(j.ParentArticleClassId).change();
    setTimeout(function () {
        $("#sel_ArticleClass_1").val(j.ArticleClassId);

    }, 1000);


    // $("#sel_ArticleClass").val(j.ArticleClassId)
    // $("#txt_ArticleContent").val(j.ArticleContent);
    ed.ready(function () {

        ed.setContent(j.ArticleContent);
    });

    $("#img_atricleImg").attr("ImgId", j.ArticleImgId);
    $("#img_atricleImg").attr("src", j.ArticleImgUrl);




}


function Ck() {
    var ArticleTitle = $("#txt_ArticleTitle").val();

    if (ArticleTitle == "") {
        return false;
    }
    return true;
}

function SaveArtice() {

    if (Ck()) {
        //检测通过


    }
    else {
        //没有检测通过
        return;

    }


    AjaxPost("/aar/",
        "SaveArticle",
        {

            ArticleId: GetQueryString("ArticleId"),
            ArticleTitle: $("#txt_ArticleTitle").val(),
            ArticleSummary: $("#txt_ArticleSummary").val(),
            ArticleContent: encodeURIComponent(UE.getEditor("txt_ArticleContent").getContent()),
            ArticleSource: $("#txt_ArticleSource").val(),
            CreateTime: $("#txt_CreateTime").val(),
            CreateUser: $("#txt_Author").val(),
            ArticleTypeId: 0,
            ArticleClassId: GetChildSelectVal("sp_class"),
            Author: $("#txt_Author").val(),
            ArticleImgId: $("#img_atricleImg").attr("ImgId"),
            Memo: $("#txt_Memo").val(),
            Invalid: $("#cb_invalid")[0].checked,
            imgArray: JArrayToXmlStr(imgArray),
            MerId: MerJson.MerchantId,
            MerLogoUrl: MerJson.LogoUrl,
            MerName: MerJson.MerchantName

        },
        function (data) {
            if (data.re == "ok") {


                localStorage.ArticleClassId = $("#sel_ArticleClass").val();
                localStorage.ArticleClassId2 = $("#sel_ArticleClass_1").val();
                shuaxin();
            }
            else {

                alert(data.re);
            }



        });


}


function BindArticle() {
    AjaxPost("/aar/",
        "GetArticleInfo",
        {

            ArticleId: GetQueryString("ArticleId"),
            MerId: GetQueryString("MerId")


        },
        function (data) {
            if (data.re == "ok") {
                alert("保存成功!");



            }
            else {

                alert(data.re);
            }



        });


}
