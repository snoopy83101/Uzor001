/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../ZYUiPub.js" />



$(function () {

    BindAClass();
    //  $("#sel_ArticleClass").change();
});

function BindAClass()               //绑定平台
{
    AjaxPost("/am/", "GetArticleClass",
        {
            MerId: GetQueryString("MerId")
        },
        function (data) {
            if (data.re == "ok") {
                var w = new Array();
                for (var i = 0; i < data.MyMerAtrClass.length; i++) {
                    var j = data.MyMerAtrClass[i];

                    w.push("<tr ArticleClassId='" + j.ArticleClassId + "'>")
                    w.push("<td>");
                    w.push(j.ArticleClassName);
                    w.push("[" + j.ArticleClassId + "]");
                    w.push("</td>");
                    w.push("<td>");
                    w.push(j.ArticleClassMemo);
                    w.push("</td>");

                    w.push("<td style='text-align:left'>");
                    var cArray = $.parseJSON(j.ChlidArt);
                    if (cArray.length > 0) {
                        w.push("<ul class='ul_cArtclass'>");
                        for (var x = 0; x < cArray.length; x++) {

                            var c = cArray[x];


                            w.push("<li>");
                            w.push("<a onclick='PopABindClass(this)'    AddCld='AddCld'  ArticleClassId='" + c.ArticleClassId + "'  ParentArticleClassId='" + c.ParentArticleClassId + "'   class='a_className'>");
                            w.push(c.ArticleClassName);
                            w.push("[" + c.ArticleClassId + "]");
                            w.push("</a>");
                            w.push("<a class='a_btn' ArticleClassId='" + c.ArticleClassId + "'    onclick='Invalid(this,true)' >删</a>");
                            w.push("</li>");
                        }
                        w.push("</ul>");
                    }
                    else {
                        w.push("无子类别");
                    }
                    w.push("</td>");

                    w.push("<td>");
                    w.push("<input onclick='PopABindClass(this)' type='button' class='' value='编辑' />");
                    w.push("<input onclick='PopABindClass(this)'  AddCld='AddCld'  ArticleClassId='0'  ParentArticleClassId='" + j.ArticleClassId + "'  type='button' class='' value='加子类' />");
                    if (j.Invalid) {
                        //已经作废, 显示启用
                        w.push("<input ArticleClassId='" + j.ArticleClassId + "' type='button' onclick='Invalid(this,false)' class='' value='启用' />");
                    }
                    else {
                        //还没有作废,可以作废
                        w.push("<input ArticleClassId='" + j.ArticleClassId + "' type='button' onclick='Invalid(this,true)' class='' value='作废' />");
                    }
                    w.push("</td>");




                    w.push("</tr>");
                }

                //              $(".li_ac").remove();

                $("#tb_artClass").children("tbody").html(w.join(""));
            }
            else {

                alert(data.re);
            }

        });
}



//弹窗添加或编辑类别
function PopABindClass(obj) {

    var AddCld = $(obj).attr("AddCld");
    if (AddCld == "AddCld") {
        var ArticleClassId = $(obj).attr("ArticleClassId");

        var ParentArticleClassId = $(obj).attr("ParentArticleClassId");;
        OpIfmWindow("ifm_art", "/Article/popwindow/ArtClassInfo.aspx?ParentArticleClassId=" + ParentArticleClassId + "&ArticleClassId=" + ArticleClassId + "", { 保存: "save", 关闭: "close" }, 300, 200, "cb", 150);
        return;
    }

    var ArticleClassId = $(obj).parent().parent().attr("ArticleClassId");
    if (ArticleClassId == "" || ArticleClassId == null) {

        ArticleClassId = "0";
    }


    var w = new Array();


    OpIfmWindow("ifm_art", "/Article/popwindow/ArtClassInfo.aspx?ArticleClassId=" + ArticleClassId + "", { 保存: "save", 关闭: "close" }, 300, 200, "cb", 150);



}

function cb(m, v) {


    switch (v) {
        case "save":
            var j = document.getElementById("ifm_art").contentWindow.GetJson();

            j.MerId = localStorage.MerId;
            j.OrderNo = 1;
            j.ArticleClassImgId = "";
            j.Invalid = "false";
            AjaxPost("/am/", "SaveArticleClass", j, function (data) {

                if (data.re == "ok") {
                    BindAClass();
                    ZyPopClose();

                }
                else {

                    alert(data.re);
                }

            });


            break
        case "close":
            ZyPopClose();
            break;
        default:

    }
}


function Invalid(obj, Invalid) {
    var ArticleClassId = $(obj).attr("ArticleClassId");


    AjaxPost("/am/", "InvalidAtricleClass",
        {
            ArticleClassId: ArticleClassId,
            Invalid: Invalid
        }, function (data) {

            if (data.re == "ok") {
                BindAClass();
            }
            else {
                alert(data.re);

            }
        });


}

function ToArtcleList(obj) {
    var ArticleClassId = $(obj).parent().parent().attr("ArticleClassId");
    window.open("/MyMerArticleList/?MerId=" + GetQueryString("MerId") + "&ArticleClassId=" + ArticleClassId);
}
