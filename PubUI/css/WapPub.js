/// <reference path="../JS/jquery-1.7.1.min.js" />
/// <reference path="../JS/jquery.mobile-1.1.0.min.js" />
$(function () {

    //实现图片慢慢浮现出来的效果



});


//今天明天昨天
function GetDateStr(AddDayCount) {
    var dd = new Date();
    dd.setDate(dd.getDate() + AddDayCount);//获取AddDayCount天后的日期
    //var y = dd.getFullYear();
    //var m = dd.getMonth() + 1;//获取当前月份的日期
    //var d = dd.getDate();


    return dd.Format("yyyy-MM-dd");
}
var scrollLoad = (function (options) {
    var defaults = (arguments.length == 0) ? { src: 'xSrc', time: 300 } : { src: options.src || 'xSrc', time: options.time || 300 };
    var camelize = function (s) {
        return s.replace(/-(\w)/g, function (strMatch, p1) {
            return p1.toUpperCase();
        });
    };
    this.getStyle = function (element, property) {
        if (arguments.length != 2) return false;
        var value = element.style[camelize(property)];
        if (!value) {
            if (document.defaultView && document.defaultView.getComputedStyle) {
                var css = document.defaultView.getComputedStyle(element, null);
                value = css ? css.getPropertyValue(property) : null;
            } else if (element.currentStyle) {
                value = element.currentStyle[camelize(property)];
            }
        }
        return value == 'auto' ? '' : value;
    };
    var _init = function () {
        var offsetPage = window.pageYOffset ? window.pageYOffset : window.document.documentElement.scrollTop,
            offsetWindow = offsetPage + Number(window.innerHeight ? window.innerHeight : document.documentElement.clientHeight),
            docImg = document.images,
            _len = docImg.length;
        if (!_len) return false;
        for (var i = 0; i < _len; i++) {
            var attrSrc = docImg[i].getAttribute(defaults.src),
                o = docImg[i], tag = o.nodeName.toLowerCase();
            if (o) {
                postPage = o.getBoundingClientRect().top + window.document.documentElement.scrollTop + window.document.body.scrollTop; postWindow = postPage + Number(this.getStyle(o, 'height').replace('px', ''));
                if ((postPage > offsetPage && postPage < offsetWindow) || (postWindow > offsetPage && postWindow < offsetWindow)) {
                    if (tag === "img" && attrSrc !== null) {
                        o.setAttribute("src", attrSrc);
                    }
                    o = null;
                }
            }
        };
        var scrollObj = window;

        if (options != null) {
            if (options["srollObjId"] != null) {
                scrollObj = document.getElementById(options.srollObjId);

            }
        }
        scrollObj.onscroll = function () {
            setTimeout(function () {
                _init();
            }, defaults.time);
        }
    };
    return _init();
});

function Poplituo() {
    var w = [];
    w.push("<img src='/images/lituo.jpg' class='img_lituo1'  />");

    PopSwt(w.join(""), {});

}

function GetCurrentPageId() {

    return $.mobile.activePage[0].id;
}


//返回上一页



function fanhui(url) {

    if (history.length > 1) {
        history.back(-1);

    }
    else {

        tiaozhuan(url);

    }
}

var scrollJson = {};


function ReCache() {
    try {
        window.applicationCache.swapCache();
    } catch (e) {
        alert(e);
    }
}

function zhiding() {
    $("html,body").scrollTop(1);
}
function zhidi() {
    $("html,body").scrollTop($("body").height());
}

function SaveScroll() {
    var ev = $.mobile.activePage;

    var pageId = ev[0].id;
    scrollJson[pageId] = $(document).scrollTop();

}

function GetSrcoll() {
    try {
        var ev = $.mobile.activePage;
        var pageId = ev[0].id;
        var i = scrollJson[pageId];


        $("html,body").scrollTop(i);
    }
    catch (e) {

    }


}



function AjaxPost(url, para, data, callback, async) {
    /// <summary> ajax提交 </summary>
    /// <param name=" url " type="String">ajax提交页面</param>
    /// <param name=" para " type="String">参数值</param>
    /// <param name=" data " type="String">json参数</param>
    /// <param name=" callback " type="String">Json回调函数(data)</param>
    /// <param name=" async " type="String">默认为false(同步),ture为异步</param>
    for (var i in data) {

        if (isString(data[i])) {
            data[i] = encodeURI(data[i]); //全部转码吧,少年!

        }


    }

    data.para = encodeURI(para);
    if (async == null) {
        //默认为同步
        async = false;
    }
    $.ajax({
        type: "post",
        url: url,
        dataType: "json",
        data: data,
        async: async,
        success: function (data) {
            data.re = decodeURI(data.re);
            callback(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }


    });
}


function isString(_str) {
    if (_str == null) {
        return false;
    }
    return typeof _str == 'string' || _str.constructor == String;
    //return typeof _str == "string" || ( _str && (typeof _str.substr == 'function'));
}


function WapPageSetting(sObj, totalPage, PageId) {
    $(".a_page_btn[PageId='" + PageId + "']").unbind("click");
    $(".a_page_btn[PageId='" + PageId + "']").click(function () {

        var CurrPage = parseInt($(this).attr("page"));

        if (CurrPage == parseInt(totalPage)) {

            $(this).html("木了,最后一页了来");
            $(this).listview('refresh');
            return;
        }
        else {

            CurrPage = CurrPage + 1;
            setTimeout(function () {
                eval(sObj + "(" + CurrPage + ")");
            }, 100);


            $(this).attr("page", CurrPage.toString());
        }
    });
}


function GetQueryString(name, val) {   //获取QueryString参数,如果无则返回""
    if (val == null) {
        val = "";
    }
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return val;
}


function ConvertToBool(str) {

    if (str == null) {
        return false;
    }
    switch (str.toLowerCase()) {
        case "true": case "yes": case "1": case "checked": return true;
        case "false": case "no": case "0": case null: case "": return false;
        default: return Boolean(string);
    }
}


function ConvertToInt(obj) {
    if (obj == null || obj == undefined || obj == NaN || $.trim(obj) == "") {
        return 0;
    }

    var i = parseInt(obj);
    if (i == NaN) {
        return 0;
    }
    else {
        return i;
    }

}
function ConvertToString(obj) {


    var str = String(obj);

    if (str == "undefined" || str == "null") {
        return "";
    }

    return str;
}
//#region 时间

//转化时间字符串
function DateFormat(str, yyMMdd) {
    if (yyMMdd == null) {
        yyMMdd = "yyyy-MM-dd";
    }

    var d;
    if (str == null) {
        d = new Date();
    }
    else {
        d = new Date(str);

    }


    return d.Format(yyMMdd);


}

function tiaozhuan(url) {

    window.location = url;
}

function isValidDate(str) {//判断是否是标准时间字符串,str变量为要判断的时间字符串
    if (!/^\d{4}\-\d\d?\-\d\d?/.test(str)) {
        return false;
    }
    var array = str.replace(/\-0/g, "-").split("-");
    var year = parseInt(array[0]);
    var month = parseInt(array[1]) - 1;
    var day = parseInt(array[2]);
    var date = new Date(year, month, day);
    return (date.getFullYear() == year &&
            date.getMonth() == month &&
            date.getDate() == day);
}

function comptime(myTimeStr) {
    var mt = myTimeStr.split(" ")[0];
    var nowDate = ClearDate(new Date(NowDate));
    if (nowDate == null) {//如果没有继承过模板页的NowDate变量, 则取本地时间
        nowDate = ClearDate(new Date());
    }
    var myDate = ClearDate(mt);
    if (nowDate.toDateString() == myDate.toDateString()) {
        return "今天";
    }
    else {
        var d = datediff(nowDate, myDate);



        if (d > 0 && d <= 7) {

            if (d == 1) {
                return "昨天";
            }
            else if (d == 2) {
                return "前天";

            }
            else if (d == 3) {

                return "大前天";
            }




            return d + "天前";
        }

        else if (d > 7 && 7 < 30) {

            return ConvertToInt(d / 7) + "周前";

        }
        else if (d > 30 && d < 365) {

            return ConvertToInt(d / 30) + "个月前";
        }
        else if (d > 365) {

            return mt;
        }



    }



}


function datediff(date2, date1) {
    var d, s, t, t2;
    var MinMilli = 1000 * 60;
    var HrMilli = MinMilli * 60;
    var DyMilli = HrMilli * 24;
    d = new Date();
    t = Date.parse(date1);
    t2 = Date.parse(date2);
    s = Math.round((t2 - t) / DyMilli);
    return (s);
}

function ClearDate(myTime) {


    var mydate = new Date(myTime);
    mydate.setHours(0);
    mydate.setMinutes(0);
    mydate.setSeconds(0)
    mydate.setMilliseconds(0);


    return mydate;

}


//#endregion


function zmnImgCenter(obj) {
    obj.each(function () {
        var $this = $(this);
        var objHeight = $this.height();//图片高度
        var objWidth = $this.width();//图片宽度
        var parentHeight = $this.parent().height();//图片父容器高度
        var parentWidth = $this.parent().width();//图片父容器宽度


        if (parentHeight < objHeight) {//外围高度小于图片高度
            var ml = (objHeight - parentHeight) / 2
            $(this).css("margin-top", 0 - ml);
        }

        return;


    })
}



//截取字符串
function JieQu(str, endNum, str2) {
    /// <summary> 截取字符串 </summary>
    /// <param name=" str " type="String"> 需要截取的字符串  </param>
    /// <param name=" endNum " type="String">  截取数 </param>
    /// <param name=" str2 " type="String">  截取后的后缀 </param>

    if (str.length <= endNum) {
        return str;
    }
    if (str2 == null) {
        str2 = "...";
    }

    return str.substr(0, endNum) + str2;

}

function GetWxOpenName(str) {

    return str.substr(str.length - 4);
}

function GetWxNameByJson(j) {

    if (j.CreateUser == "") {
        return "微信用户" + GetWxOpenName(j.WxOpenId);
    }
    else {

        if (j.HideUser) {
            return "匿名";
        }
        else {

            return j.CreateUser;
        }
    }
}

function GetUserPicByJson(j) {
    if (j.CreateUser == "") {
        return "/wap/images/weixin30.jpg";
    }
    else {
        return j.小头像;
    }
}

function JArrayToXmlStr(JArray) {
    var w = new Array();
    w.push("<root>");
    for (var i = 0; i < JArray.length; i++) {

        var j = JArray[i];
        w.push("<ajaxItem>");
        for (var obj in j) {

            var jsonKey = obj.toString();
            var jsonValue = ConvertToString(j[jsonKey]);
            w.push("<" + jsonKey + ">");
            w.push(jsonValue);
            w.push("</" + jsonKey + ">");
        }
        w.push("</ajaxItem>");
    }
    w.push("</root>");
    return w.join("");
}

//#region 过场



//弹出手机弹窗
function PopGuoChang(html, option) {

    var w = new Array();

    if (option == null) {
        option = {};
    }
    bodyWidth = $(window).width();
    option.width = ConvertToInt((bodyWidth ));
    option.height = $(window).height();
    if (option.history == null)
    {

        option.history == 0;
    }

    if (option.hdText == null)
    {

        option.hdText = "";
    }

    option.top = 0;

    option.left = 0;


    w.push("<div class='div_PopGuoChangInfo'style='width:" + option.width + "px; height:" + option.height + "px; top:" + option.top + "px;left:" + option.left + "px; ' >");
    w.push("<div class='div_guochang_hd' >");
    w.push("<span class='hd_text'>"+option.hdText+"</span>");
    w.push("<a class='a_fanhui' onclick='CloseGuoChang(" + option.history + ")' />");
    w.push("</div>");
    w.push("<div class='div_PopGuoChangContent'>");
    w.push("<div class='clr' style='height:50px' ></div>");
    w.push(html);
    w.push("</div>");
    w.push("</div>");



    $("body").append(w.join(""));
    $(".div_PopGuoChangContent").css("overflow-y", "auto");
    $(".div_PopGuoChangContent").height(option.height);
}

function CloseGuoChang(h) {
    if (h == 0 || h == null) {

    }
    else {
        history.back(h);
    }
    $(".div_PopGuoChangInfo").remove();

}

//#endregion


//#region 弹窗

//弹出手机弹窗
function PopSwt(html, option) {

    var w = new Array();

    if (option == null) {
        option = {};
    }
    bodyWidth = $("body").width();
    option.width = ConvertToInt((bodyWidth * 0.8));
    option.height = 0;
    option.top = 60;



    option.left = ConvertToInt(((bodyWidth / 2) - (option.width / 2)));



    w.push("<div class=\"SwtCon\" id=\"swt\" style='width:" + option.width + "px; height:auto; top:" + option.top + "px;left:" + option.left + "px; ' > ");
    w.push("<div class=\"Swtcons\">");
    w.push("<div class=\"SwtClose\" id=\"no\" onclick=\"CloseSwt(" + option.history + ")\">");
    w.push("<img src=\"/images/swtclose.png\">");
    w.push("</div>");
    w.push("<div class='clr_10px' ></div>");
    w.push("<div class='div_popContent'>");
    w.push(html);
    w.push("</div>");
    w.push("<div class='clr_10px' ></div>");

    w.push("</div>");
    w.push("</div>");


    $("body").append(w.join(""));
}

function CloseSwt(h) {
    if (h == 0 || h == null) {

    }
    else {
        history.back(h);
    }
    $(".SwtCon").remove();

}

//#endregion

//#region 分页




function ZyPagerSetting(sObj, currentpage, totalPage, PageId) {

    if (PageId == null) {
        PageId = "p1";
    }

    $("#page_next_" + PageId + ",#page_prev_" + PageId + ",#a_toPage_" + PageId + "").unbind("click");

    $("#page_next_" + PageId + "").bind("click", function () { toNextPage(sObj, PageId) });  //下一页绑定
    $("#page_prev_" + PageId + "").bind("click", function () { toPrevPage(sObj, PageId) });  //上一页绑定
    $("#a_toPage_" + PageId + "").bind("click", function () { ToPage(sObj, PageId) }); //转到绑定



    var pageArray = new Array;

    for (var i = 1; i <= totalPage; i++) {

        pageArray.push("<a id='a_page_" + PageId + "_" + i + "' pageNum='" + i + "' onclick='" + sObj + "(" + i + "," + PageId + ")'>" + i + "</a>");
    }
    var currentId = "a_page_" + PageId + "_" + currentpage + "";
    $("#span_pageMenu_" + PageId + "").html(pageArray.join(""));
    $("#" + PageId + "").children(".cruuent").removeClass("current");
    $("#" + currentId + "").addClass("current");
    HidePageInt(PageId); //隐藏列
}

function ReIndex(sObj) {
    var pageIndex = parseInt($("#span_pageMenu_" + PageId + " .current").attr("pageNum"));
    eval(" " + sObj + "(pageIndex);");

}

function toNextPage(sObj, PageId) {
    /// <summary>下一页</summary>
    var pageIndex = parseInt($("#span_pageMenu_" + PageId + " .current").attr("pageNum"));
    pageIndex++;

    if ($("#span_pageMenu_" + PageId + " a[pageNum='" + pageIndex + "']").length == 0) {
        return false;
    }
    else {
        eval(" " + sObj + "(pageIndex);");
    }

}
function toPrevPage(sObj, PageId) {

    var pageIndex = ConvertToInt($("#span_pageMenu_" + PageId + " .current").attr("pageNum"));

    pageIndex = pageIndex - 1;

    if (pageIndex < 1 || pageIndex == null || pageIndex == NaN) {
        return false;
    }
    else {
        eval(" " + sObj + "(pageIndex);");
    }

}
function HidePageInt(PageId) {
    var pageMenu = $("#span_pageMenu_" + PageId + "").children("a");
    var totalPage = pageMenu.length;
    if (totalPage < 20) {

        //如果总页数小于20页,不进行任何操作

    }
    else {
        //如果大于20页
        var currentPage = parseInt($(".current").html());
        if (currentPage < 10) {

            $("#span_pageMenu_" + PageId + ">a:gt(20)").hide();

        }
        else {
            $("#span_pageMenu_" + PageId + ">a:gt(" + (currentPage + 10).toString() + ")").hide();
            $("#span_pageMenu_" + PageId + ">a:lt(" + (currentPage - 10).toString() + ")").hide();
        }
    }
    $("#span_totalPage_" + PageId + "").html(totalPage);

}
function ToPage(sObj, PageId) {
    var ToPageNum = ConvertToInt($("#txt_ToPage_" + PageId + "").val());
    if (ToPageNum < 1) {
        return false;
    }
    else {
        //  sObj(ToPageNum, PageId);

        eval(" " + sObj + "(ToPageNum);");
    }
}

function DateFormat(str, yyMMdd) {
    if (yyMMdd == null) {
        yyMMdd = "yyyy-MM-dd";
    }

    var d;
    if (str == null) {
        d = new Date();
    }
    else {
        d = new Date(str);

    }


    return d.Format(yyMMdd);


}

function PagerHtml(search, currentPage, totalPage, PageId) {

    if (PageId == null) {
        PageId = "";
    }
    var w = new Array();
    w.push(" <div id='" + PageId + "' ");
    if (totalPage <= 1) {
        w.push(" style='display:none;' ");
    }

    w.push("   class='manu'><a class='pager_1' id='page_prev_" + PageId + "'>上一页</a><span id='span_pageMenu_" + PageId + "'>");

    for (var i = 1; i <= totalPage; i++) {



        w.push("<a id='a_page_" + PageId + "_" + i + "'");


        if (i == currentPage) {
            w.push(" class='current' ");
        }
        w.push("  onclick='" + search + "(" + i + ",\"" + PageId + "\")' pageNum='" + i + "'>" + i + "</a>");

    }


    w.push("</span> <span class='pager_1' > <a  id='page_next_" + PageId + "'>下一页</a><a id='a_toPage_" + PageId + "'>转 到</a><input style='width: 30px;' id='txt_ToPage_" + PageId + "' onkeypress='onlyNumber()' type='text'>页,共 <span id='span_totalPage_" + PageId + "'>" + totalPage + "</span> 页 </span>  </div>");

    return w.join("");
}

//#endregion


function BindUserSetting() {
    if (localStorage.UserId == "") {
        //如果没有登录
        $(".nolog,.noLogin").show();
        $(".haslog,.isLogin").hide();
    }
    else {
        //如果已经登录
        $(".nolog,.noLogin").hide();
        $(".haslog,.isLogin").show();


    }

}

function ToZhuCe() {

    CloseSwt();

    var w = new Array();
    w.push("<div class='div_login'>");
    w.push("<h2>用户注册</h2>");
    w.push("<ol>");
    w.push("<li  style='display:none'>");
    w.push("微信(加密):" + localStorage.openid + "");
    w.push("</li>");
    w.push("<li>");
    w.push("<span class='sp_tip1' >昵称:</span><input id='txt_userId' type='text' placeholder='昵称' class='txt_user'  />");
    w.push("</li>");
    w.push("<li>");
    w.push("<span class='sp_tip1' >密码:</span><input id='txt_pwd1' type='password' placeholder='您的密码' class='txt_pwd'  />");
    w.push("</li>");
    w.push("<li>");
    w.push("<span class='sp_tip1' >确认:</span><input id='txt_pwd2' type='password' placeholder='重复输入' class='txt_pwd'  />");
    w.push("</li>");
    w.push("</ol>");
    w.push("</div>");
    w.push("<div class='clr_10px'></div>")
    w.push("<div>");
    w.push("<input type='button' class='regb gb' onclick='DoZhuCe()' value='确认注册' />");
    w.push("<input type='button' class='loginb bb' onclick='ToLogin()' value='登录已有账户' />");

    w.push("</div>");
    PopSwt(w.join(""));

    $("#txt_userId").focus();
}

function DoZhuCe() {

    var pwd1 = $("#txt_pwd1").val();
    var pwd2 = $("#txt_pwd2").val();
    if (pwd1 != pwd2) {
        alert("两次输入的密码不一致!");
        $("#txt_pwd1").val("");
        $("#txt_pwd2").val("");
        return;
    }
    AjaxPost("/au/",
   "Registration",
   {

       uid: $("#txt_userId").val(),
       Pwd: pwd2,
       WxOpenID: localStorage.openid


   },
   function (data) {


       if (data.re == "ok") {
           alert("用户注册成功, 页面刷新后(" + data.UserId + ")将自动登录!");
           localStorage.setItem("UserId", data.UserId);
           window.location = window.location;

       }
       else {

           alert(data.re);
       }


   });
}


function shuaxin() {
    if (GetQueryString("redirect_uri") == "") {
        //一般页面,直接刷新
        window.location = window.location;

    }
    else {
        //微信跳转页,取得参数后刷新
        var redirect_uri = GetQueryString("redirect_uri");
        tiaozhuan(decodeURI(redirect_uri));
    }
}

function ToLogin() {

    CloseSwt();
    var w = new Array();
    w.push("<div class='div_login'>");
    w.push("<h2>用户登录</h2>");
    w.push("<ol>");
    w.push("<li style='display:none'>");
    w.push("微信(加密):" + localStorage.openid + "");
    w.push("</li>");
    w.push("<li>");
    w.push("<span class='sp_tip1' >昵称:</span><input id='txt_EmailOrUserId' type='text' placeholder='昵称或者邮箱' class='txt_user'  />");
    w.push("</li>");
    w.push("<li>");
    w.push("<span class='sp_tip1' >密码:</span><input id='txt_LoginPwd' type='password' placeholder='您的密码' class='txt_pwd'  />");
    w.push("</li>");
    w.push("</ol>");
    w.push("</div>");
    w.push("<div class='clr_10px'></div>")
    w.push("<div>");
    w.push("<input type='button' class='loginb bb' onclick='DoLogin()' value='登录' />");
    w.push("<input type='button' class='regb gb' onclick='ToZhuCe()' value='注册用户' />");
    w.push("</div>");
    PopSwt(w.join(""));
    $("#txt_EmailOrUserId").focus();
}

function DoLogin() {
    AjaxPost("/AjaxServer/UserAjax.aspx",
 "UserLogin",  //用户登陆
 {

     inputStr: $("#txt_EmailOrUserId").val(),

     Pwd: $("#txt_LoginPwd").val(),
     WxOpenID: localStorage.openid


 },
 function (data) {

     if (data.re == "ok") {
         //  alert("用户(" + data.UserId + "," + data.WxOpenID + ")登录成功!");

         localStorage.setItem("UserId", data.UserId);
         CloseSwt();
         shuaxin();
     }
     else {
         alert(data.re);
     }
 });
}

function ToPopSend() {

    var w = new Array();

    w.push("<h2 class='h2_sf'>请选择板块</h2>");
    w.push("<div class='clr_15px' ></div>");
    w.push("<div class='div_f f1' onclick='PopSend()' > ");
    w.push("微生活");
    w.push("</div>");
    w.push("<div class='div_f f3' href='/wap/job/saveresume.aspx' onclick='ToPageAndSend(this);'>");
    w.push("我的简历");
    w.push("</div>");
    w.push("<div class='div_f f3' href='/wap/job/savejob.aspx' onclick='ToPageAndSend(this);'>");
    w.push("发布招聘");
    w.push("</div>");
    w.push("<div class='div_f f4' href='/wap/house/savebuyhouse.aspx' onclick='ToPageAndSend(this);'>");
    w.push("发布售房");
    w.push("</div>");
    w.push("<div class='div_f f4' href='/wap/house/saverenthousedemand.aspx' onclick='ToPageAndSend(this);'>");
    w.push("发布租房");
    w.push("</div>");
    w.push("<div class='div_f f4' href='/wap/house/savebuyhousedemand.aspx' onclick='ToPageAndSend(this);'>");
    w.push("发布求购");
    w.push("</div>");
    w.push("<div class='div_f f4' href='/wap/house/saverenthousedemand.aspx' onclick='ToPageAndSend(this);'>");
    w.push("发布求租");
    w.push("</div>");

    PopSwt(w.join(""));
    zhiding();
}
function ToFormAndSend(ForumId) {
    tiaozhuan("/wap/bbs/list.aspx?ForumId=" + ForumId + "&do=send");
    shuaxin();
}

function ToPageAndSend(j) {
    var href = $(j).attr('href');
    tiaozhuan(href);

}
function changeURL(url) {

    window.history.pushState({}, 0, 'http://' + window.location.host + '/' + url);
}

Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//剥离HTML标签
function removeHTMLTag(str) {
    str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
    str = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
    //str = str.replace(/\n[\s| | ]*\r/g,'\n'); //去除多余空行
    str = str.replace(/&nbsp;/ig, '');//去掉&nbsp;
    return str;
}

//绑定时间控件
//function BindDateInput(inputId, txt) {

//    if (txt == null) {

//        if ($("#" + inputId + "").val() == "") {
//            if (NowDate != null) {
//                $("#" + inputId + "").val(DateFormat(NowDate, "yyyy-MM-dd"));
//            }

//        }


//    } else {


//        $("#" + inputId + "").val(txt);
//    }



//    $("#" + inputId + "").addClass("Wdate");
//    $("#" + inputId + "").click(function () {

//        WdatePicker();
//    });

//}



/*#region 转为小数 */

/*#region 保留两位小数  */


function ConverttoDecimal2f(x) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
        alert('function:ConverttoDecimal2f->parameter error');
        return false;
    }
    f_x = Math.round(f_x * 100) / 100;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + 2) {
        s_x += '0';
    }
    return s_x;
}
/*#endregion */

//乘法函数，用来得到精确的乘法结果 
//说明：javascript的乘法结果会有误差，在两个浮点数相乘的时候会比较明显。这个函数返回较为精确的乘法结果。 
//调用：accMul(arg1,arg2) 
//返回值：arg1乘以arg2的精确结果 
function accMul(arg1, arg2) {
    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
    try { m += s1.split(".")[1].length } catch (e) { }
    try { m += s2.split(".")[1].length } catch (e) { }
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
}


function loadExtentFile(filePath, fileType) {

    if (fileType == "js") {

        var oJs = document.createElement('script');

        oJs.setAttribute("type", "text/javascript");
        oJs.setAttribute("src", filePath);//文件的地址 ,可为绝对及相对路径

        document.getElementsByTagName("head")[0].appendChild(oJs);//绑定

    } else if (fileType == "css") {

        var oCss = document.create_rElement("link");
        oCss.setAttribute("rel", "stylesheet");
        oCss.setAttribute("type", "text/css");
        oCss.setAttribute("href", filePath);

        document.getElementsByTagName("head")[0].appendChild(oCss);//绑定

    }

}



//绑定时间控件
function BindDateInput(inputId, txt) {

    loadExtentFile("/UI/My97DatePicker/WdatePicker.js", "js");

    if (txt == null) {

        if ($("#" + inputId + "").val() == "") {
            if (NowDate != null) {
                $("#" + inputId + "").val(DateFormat(NowDate, "yyyy-MM-dd"));
            }

        }


    } else {


        $("#" + inputId + "").val(txt);
    }



    $("#" + inputId + "").addClass("Wdate");
    $("#" + inputId + "").click(function () {

        WdatePicker();
    });

}
