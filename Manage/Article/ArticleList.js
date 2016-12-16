/// <reference path="../ZYUiPub.js" />
/// <reference path="../jquery-1.8.2.js" />



$(function () {

    Search(1);

});


function BindChildArtClass(obj, cldLv) {

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
            w.push("<option value='0' >全部子类别</option>")
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

function Search(CurrentPage) {


    AjaxPost("/aar/",
        "GetArticlePageList",
        {

            MerId: GetQueryString("MerId"),
            ArticleClassId: GetChildSelectVal("sp_class"),
            ArticleTitle: $("#txt_ArticleTitle").val(),
            CurrentPage: CurrentPage,
            Invalid: ConvertToBool($("#cb_invalid").attr("checked"))
        },
        function (data) {
            if (data.re == "ok") {
                var w = new Array();
                for (var i = 0; i < data.list.length; i++) {
                    var j = data.list[i];

                    w.push("<tr ArticleId='" + j.ArticleId + "' onclick='ToInfo(this)' >");
                    w.push("<td>");
                    w.push(j.ArticleTitle);
                    w.push("</td>");
                    w.push("<td>");
                    w.push(j.ArticleClassName);
                    w.push("</td>");
                    w.push("<td>");
                    w.push(j.CreateTime);
                    w.push("</td>");
                    w.push("<td>");

                    if (j.RecommendLv > 0) {
                        w.push("<red>置顶</red>");
                    }

                    w.push("</td>");
                    w.push("<td>");
                    w.push("暂无");
                    w.push("</td>");
                    w.push("</tr>");

                }
                $("#tb_list>tbody").html(w.join(""));

                tableClass("t1");
                $("#tb_list>tbody>tr").each(function () {



                    RightMenu($(this)[0], [

                          { Title: "作废", evName: "Flag" },
                          { Title: "置顶", evName: "RecommendLv" },
                          { Title: "取消置顶", evName: "NotRecommendLv" }
                    ]);


                });


                ZyPagerSetting("Search", CurrentPage, data.t, "1");
            }
            else {

                alert(data.re);
            }



        });

}



function NotRecommendLv(obj) {
    RecommendLv(obj, 0);
}

function RecommendLv(obj, lv) {

    if (lv == null) {
        lv = 10;
    }

    AjaxPost("/aar/", "RecommendLv",
        {
            MerId: GetQueryString("MerId"),
            ArticleId: $(obj).attr("ArticleId"),
            RecommendLv: lv


        }, function (data) {

            if (data.re == "ok") {

                alert("操作成功!");
                Search(1);
            }
            else {
                alert(data.re);
            }
        });

}


function Flag(obj, Invalid) {
    //为0是不作废,为1是作废
    if (Invalid == null) {
        Invalid = 1;
    }

    AjaxPost("/aar/", "FlagArticle",
       {
           MerId: GetQueryString("MerId"),
           ArticleId: $(obj).attr("ArticleId"),
           Invalid: Invalid


       }, function (data) {

           if (data.re == "ok") {

               alert("操作成功!");
               Search(1);
           }
           else {
               alert(data.re);
           }
       });
}


function ToInfo(obj) {
    var ArticleId = $(obj).attr("ArticleId");

    tiaozhuan("/Article/ArticleInfo.aspx?ArticleId=" + ArticleId + "&MerId=" + GetQueryString("MerId") + "");

}


function ToAddArticle() {
    var url = "ArticleInfo.aspx?MerId=" + $.cookie("CurrentMerId") + "";
    tiaozhuan(url);
}
