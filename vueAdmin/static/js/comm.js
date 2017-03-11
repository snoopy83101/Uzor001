

function AjaxSubmit(data, fun, async) {
    if (!async) {
        async = false;
    }
    $.ajax({
        type: "post",
        url: '/a',
        dataType: "json",
        data: data,
        async: async,
        success: function(data) {

            fun(data);
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        },
        timeout: 80000
    });




}




function GetQueryString(name, val) {   //获取QueryString参数,如果无则返回""
    if (val == null) {
        val = null;
    }
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        val = unescape(r[2]);

    if (val == null) {
        val = null;
    }
    if (val == "undefined") {
        val = null;
    }

    return val;
}


function LoadUI(o) {
    switch (o) {
        case "page":

            break;

        default:
    }


}


function Page(o) {


    //totalPages: 9,//分页总数
    //liNums: 7,//分页的数字按钮数(建议取奇数)
    //activeClass: 'activP', //active 类样式定义
    //callBack : function(page) {
    //                //console.log(page)
    // }

    var i = $(o.obj).find(".pagingUl").length;
    if (i > 0) {
        //已经绑定

        return;
    }


    var config = {};
    var fun = function myfunction(page) {


        o.fun(page);





    }

    config.totalPages = o.totalPages;
    config.liNums = 7;
    config.callBack = fun;
    config.activeClass = "activP";
    loadExtentFile("/UI/JqueryPage/jquery.page.js", "js", function() {
        $(o.obj).Page(config);
    });
    loadExtentFile("/UI/JqueryPage/jquery.page.css", "css");



}


//加载js或者css文件
function loadExtentFile(filePath, fileType, fun) {

    if (fileType == "js") {

        //var oJs = document.createElement('script');

        //oJs.setAttribute("type", "text/javascript");
        //oJs.setAttribute("src", filePath);//文件的地址 ,可为绝对及相对路径

        //var i = $("head script[src='" + filePath + "']").length;
        //if (i > 0) {
        //    //  alert("已加载js文件");
        //}
        //else {
        //    document.getElementsByTagName("head")[0].appendChild(oJs);//绑定

        //}

        $.getScript("" + filePath + "", function() {
            try {
                fun();
            } catch (e) {

            }

        })


    } else if (fileType == "css") {

        var oCss = document.createElement("link");
        oCss.setAttribute("rel", "stylesheet");
        oCss.setAttribute("type", "text/css");
        oCss.setAttribute("href", filePath);
        var i = $("head link[href='" + filePath + "']").length;
        if (i > 0) {
            // alert("已加载css文件");
        }
        else {
            document.getElementsByTagName("head")[0].appendChild(oCss);//绑定

        }


    }

}