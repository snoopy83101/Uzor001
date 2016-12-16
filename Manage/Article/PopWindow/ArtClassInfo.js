/// <reference path="../jquery-1.8.2.js" />
/// <reference path="../ZYUiPub.js" />



$(function () {

    if (ParentArticleClassJson.ArticleClassId != null) {//有外层
        $("#h_t").html("(" + ParentArticleClassJson.ArticleClassName + ")下的子类别");
    }
    else {
        $("#h_t").html("添加新类别");

    }
    $("#txt_ArticleClassName").focus();
    BindData();
});

function test1() {

    alert(1);
}


function BindData() {
    if (ArticleClassJson.ArticleClassId != null) {
        $("#txt_ArticleClassName").val(ArticleClassJson.ArticleClassName);
        $("#txt_ArticleClassMemo").val(ArticleClassJson.ArticleClassMemo);
    }
}

function GetJson() {
    return {
        ArticleClassId: GetQueryString("ArticleClassId"),
        ArticleClassName: $("#txt_ArticleClassName").val(),
        ArticleClassMemo: $("#txt_ArticleClassMemo").val(),
        ParentArticleClassId: GetQueryString("ParentArticleClassId")
    };
}
