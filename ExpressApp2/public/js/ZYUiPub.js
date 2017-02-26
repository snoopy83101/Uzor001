/// <reference path="ZYUiPub.js" />
/// <reference path="jquery-1.8.2.js" />




/*#region 公共变量 */

var tanhaoImg = " <img src='/images/a2.png' />";
var duihaoImg = " <img src='/images/a1.png' />";
var cuohaoImg = " <img src='/images/a3.png' />";



///页面参数传递成功值
var RESULT_OK = 'OK';
///ajax处理结果参数名称
var RESULT_ARGU = "result";
/*#endregion */

//数组去重
Array.prototype.unique4 = function () {
    this.sort();
    var re = [this[0]];
    for (var i = 1; i < this.length; i++) {
        if (this[i] !== re[re.length - 1]) {
            re.push(this[i]);
        }
    }
    return re;
}
//A是否包含B
function HasStr(strA, strB) {
    if (strA.indexOf(strB) < 0) {

        return false;
    }
    else {

        return true;
    }


}



//剥离HTML标签
function removeHTMLTag(str) {
    str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
    str = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
    //str = str.replace(/\n[\s| | ]*\r/g,'\n'); //去除多余空行
    str = str.replace(/&nbsp;/ig, '');//去掉&nbsp;
    return str;
}


function HideIp(ip) {

    var a = ip.split("."); //首先，由split将ip以.分割数组 
    a[a.length - 2] = "*";
    a[a.length - 1] = "*";

    return a.join(".");
}

function ZyPagerHtml(PageId, totalPage) {
    if (totalPage == null) {
        totalPage = 0;
    }

    var w = new Array();
    w.push("<div id='" + PageId + "' class=\"manu\">");
    w.push("<a id=\"page_prev_" + PageId + "\" >上一页</a><span id=\"span_pageMenu_" + PageId + "\"><a class=\"current\">1</a></span><a");
    w.push(" id=\"page_next_" + PageId + "\" >下一页</a><a id=\"a_toPage_" + PageId + "\">转 到</a><input onkeypress=\"onlyNumber()\" style=\"width:30px\" id=\"txt_ToPage_" + PageId + "\"");
    w.push("   type=\"text\"  />页,共 <span id=\"span_totalPage_" + PageId + "\"></span> 页");
    w.push("  </div>");
    return w.join("");

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


//检查验证码
function CkYzm(inputId) {
    var txt = $("#" + inputId + "").val();

    var yzm = GetCookie("yzm");
    if (txt.toLowerCase() == yzm.toLowerCase()) {
        return true;

    }
    else {

        return false;
    }
}


//弹出最大窗口
function OpenWindowMax(url) {


    window.open(url, '', 'fullscreen=1,directories=1,location=1,menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=1'); //让新打开的窗口全屏              
}

//绑定验证码
function BindYzm(objId, inputId) {

    var ipt = $("#" + inputId + "");
    ipt.focus(function () {

        AfterPageWriting(inputId, "验证码");
    });
    ipt.blur(function () {

        if (CkYzm(inputId)) {
            //验证码正确!
            AfterPageRight(inputId, "");
        }
        else {
            //验证码错误!

            AfterPageWorng(inputId, "验证码不正确!");
        }

    });

    $("#" + objId + "").click(function () {
        $(this).attr("src", "/yzm/?r=" + Math.random() + "");
    });


}



function delCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = GetCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}


//获取COOKIE值
function GetCookie(Name) {
    var search = Name + "=";
    if (document.cookie.length > 0) {
        offset = document.cookie.indexOf(search)
        if (offset != -1) {
            offset += search.length
            end = document.cookie.indexOf(";", offset)
            if (end == -1) end = document.cookie.length
            return (document.cookie.substring(offset, end));
        }
        else return ""

    }

}
function isWeiXin() {
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == 'micromessenger') {
        return true;
    } else {
        return false;
    }
}

//获取用户名
function CurrentUserId() {

    var Name = "UserId";

    return decodeURIComponent(GetCookie(Name));

}

//获得用户当前的安全登陆字符串
function CurrentUserKey() {

    var Name = "UserKey";
    return GetCookie(Name);

}
function Breadcrumbs(j) {
    /// <summary> 面包屑导航 </summary>
    /// <param name=" j " type="String">  {  首页:"/default.aspx" } </param>


    var w = new Array();

    for (i in j) {  //循环输出按钮

        var bName = i.toString();
        var bUrl = j[i];

        w.push("<a href='" + bUrl + "'>" + bName + "</a>");

    }

    //  $("#div_Path").before("<div class='clr' style='height:3px;' ></div>");
    $("#div_Path").html("<span class='sp_path1' ></span>" + w.join(">"));
    $("#div_Path").show();
}

function TitleChange(j) {

    var w = new Array();

    for (i in j) {  //循环输出按钮

        var titleName = i.toString();
        var titleValue = j[titleName];


        w.push(titleValue);




    }

    w.push("");

    $(document).attr("title", w.join(" - "));//修改title值


}


/*#region 基于JQUERY的弹窗扩展使用(自行开发) */

function BindSelect(Sid, JsonArray, Key, Val) {

    var opArray = new Array();
    for (var i = 0; i < JsonArray.length; i++) {

        var json = JsonArray[i];




        opArray.push("<option value='" + json[Val] + "' > ");
        opArray.push(json[Key]);
        opArray.push("</option>");




    }


    $("#" + Sid).html(opArray.join(""));


}

function isString(_str) {
    if (_str == null) {
        return false;
    }
    return typeof _str == 'string' || _str.constructor == String;
    //return typeof _str == "string" || ( _str && (typeof _str.substr == 'function'));
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

function WxAjaxPost(url, Json, callback) {
    $.ajax({
        type: "post",
        url: "/ajaxServer/wx.aspx",
        dataType: "json",
        data: {
            para: encodeURI("ApiCommon"),
            wxUrl: encodeURI(url),
            wxJson: encodeURI(JsonToStr(Json))
        },
        async: false,
        success: function (data) {
            data.re = decodeURI(data.re);
            callback(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }


    });


}


function AjaxPost(url, para, data, callback, async, timeout) {
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
    if (timeout == null) {
        timeout = 20000;
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
        },
        timeout: timeout

    });
}
//锚点链接
function maodian(objId, mss) {
    if (mss == null) {
        mmss = 500;
    }

    var pos = $("#" + objId + "").offset().top;
    $("html,body").animate({ scrollTop: pos }, 500);
}

//浏览器视窗的偏移量检测
function getWindowOffset() {
    var offset = {
        x: 0,
        y: 0
    };

    if (typeof window.pageXOffset != 'undefined' || typeof window.pageYOffset != 'undefined') {
        offset.x = window.pageXOffset;
        offset.y = window.pageYOffset;
    } else if (typeof document.compatMode != 'undefined' && document.compatMode == 'CSS1Compat') {
        offset.x = document.documentElement.scrollLeft;
        offset.y = document.documentElement.scrollTop;
    } else if (typeof document.body != 'undefined' && (document.body.scrollLeft || document.body.scrollTop)) {
        offset.x = document.body.scrollLeft;
        offset.y = document.body.scrollTop;
    }

    return offset;
}

//回调函数
function postback(w, v) {

    ZyPopClose();
}
//关闭窗口方法
function ZyPopClose() {
    $("#jqibox").remove();
}
//打开窗口方法
function ZyPopWindow(PopHtml, btnJson, W, H, callBack, top, left) {
    var offset = {
        x: 0,
        y: 0
    };
    offset = getWindowOffset();


    if (top == null) {
        top = 50;
    }
    top = top + offset.y; //加上当前视窗的垂直偏移量
    var PageW = $(window).width();
    var PageH = $(window).height();
    var leftPx = 0;

    if (left) {
        leftPx = left;
    }
    else {
        try {
            leftPx = PageW / 2 - W / 2 + offset.x - 50;
        } catch (e) {
            leftPx = 0;
        }

    }
    var w = new Array;
    var btnHtml = "";
    if (btnJson != null) {
        for (btn in btnJson) {  //循环输出按钮

            var btnName = btn.toString();
            var btnValue = btnJson[btn];
            var btnId = "jqiBtn_" + btnName;
            var callBackValue = "";

            callBackValue += "(";
            callBackValue += "$('#jqi'),";
            callBackValue += "'" + btnValue + "')";


            btnHtml += '<button onclick="' + callBack.toString() + callBackValue.toString() + '" id="' + btnId + '"  type="button" ';
            btnHtml += '  value="' + btnValue + '">';
            btnHtml += '' + btnName + '</button>';




        }
    }
    w.push('<div style="height:' + PageH + 'px" id="jqibox" class="jqibox" style="text-align:center" >');
    w.push('<div  style="height:' + PageH + 'px" id="jqifade" class="jqifade">');
    w.push('</div>');
    w.push('<div style=" left:' + leftPx + 'px; margin:0 auto; top:' + top + 'px; " id="jqi" class="jqi" >');
    w.push('<div class="jqicontainer">');
    w.push('<div class="jqiclose" onclick="ZyPopClose()">');
    w.push('X</div>');
    w.push('<div id="jqistates">');
    w.push('<div style="" id="jqi_state_state0" class="jqi_state" >');
    w.push('<div class="jqimessage">');
    w.push('<div style="width: ' + W + 'px; height: ' + H + 'px">');
    w.push(PopHtml);
    w.push('</div>');
    w.push('</div>');
    w.push('<div id="jqibuttons" class="jqibuttons">');
    w.push(btnHtml);
    w.push('  </div>');
    w.push(' </div>');
    w.push('  </div>');
    w.push('  </div>');
    w.push('  </div>');
    var htmlCode = w.join("");
    $("body").append(htmlCode);
    $("#jqibuttons").children().eq(0).addClass("select");
    $("#jqibuttons").children().eq(0).focus();
    $("#jqibox").keyup(function () {

        if (event.keyCode == 39) {
            //右箭头
            var selBtn = $("#jqibuttons").children(".select");
            selBtn.removeClass("select");
            selBtn.next().addClass("select");
            selBtn.next().focus();
        }
        else if (event.keyCode == 37) {
            //左箭头
            var selBtn = $("#jqibuttons").children(".select");
            selBtn.removeClass("select");
            selBtn.prev().addClass("select");
            selBtn.prev().focus();
        }

    });

}



/*#endregion */


//json数组对象往后台提交字符串
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




//给样式为T1的表格进行隔行换色
function tableClass(tableClass) {

    $("." + tableClass + ">tbody>tr").mouseover(function () {

        $(this).addClass("td_mouseover");
    });
    $("." + tableClass + ">tbody>tr").mouseout(function () {
        $(this).removeClass("td_mouseover");
    });
    $("." + tableClass + ">tbody>tr:odd").addClass("tr1");
}

//让一个文本框只能输入数字
function OnlyNumberForInput(obj) {


    obj.value = obj.value.replace(/[^\d.]/g, "");  //清除“数字”和“.”以外的字符  
    obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是.  
    obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的  
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');//只能输入两个小数  



}

//必须是onkeyup事件调用
function onlyNumber(obj, x) {//只能输入数字
    obj.value = obj.value.replace(/[^\d]/g, "");  //清除“数字”和“.”以外的字符  
    if (x) {
        obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是.  
        obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的  
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
        obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');//只能输入两个小数  
    }

}

function onlyNum(obj, o) {
    obj.value = obj.value.replace(/[^\d]/g, "");  //清除“数字”和“.”以外的字符  
    if (o.x) {
        obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是.  
        obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的  
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
        obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');//只能输入两个小数  
    }

    if (o.max != null) {



        if (ConvertToInt($(obj).val()) > ConvertToInt(o.max)) {

            $(obj).val(o.max);

        }
    }

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




function InputCheck(inputid, CkType, CkText, TipText) {
    /// <summary> 检测文本框的合法性 </summary>
    /// <param name=" inputid " type="String"> 文本框的ID,有些类别需要两个ID,则用|分隔  </param>
    /// <param name=" CkType " type="String">  NotNull,2pwd,mail,num,phone,email(是否存在),exuserid(用户是否可以注册) ,exhasuseridoremail(用户名或邮件是否存在)</param>
    /// <param name=" CkText " type="String">  文本框条件不满足时的提示 </param>
    /// <param name=" TipText " type="String"> 文本框获得焦点时的提示  </param>
    if (CkType == null) {
        CkType = "";
    }

    var input1 = inputid;
    var input2 = inputid;

    if (CkType.toLowerCase() == "2pwd") {
        var ipt = inputid.split("|");
        input1 = ipt[0];
        input2 = ipt[1];
    }

    if (CkType == "" || CkType == null) {
        CkType = "NotNull";
    }
    if (CkText == "" || CkText == null) {
        CkText = "必填";
    }
    try {
        var obj = $("#" + input2 + "");

        obj.focus(function () {

            obj.addClass("writing");
            if (TipText == null) {
                TipText = "";
            }
            AfterPageWriting(input2, TipText);
        });

        if (CkType == "num") {
            obj.keypress(function () {

                onlyNumber();
            });
        }
    }
    catch (e) {

    }




    obj.blur(function () {


        try {

            obj.removeClass("writing");
            var v = $.trim($(this).val());

            switch (CkType.toLowerCase()) {

                case "2pwd":
                    inputid = input2;
                    var str1 = $("#" + input1 + "").val();

                    var str2 = $("#" + input2 + "").val();
                    if (str2.length >= 4 && str2.length <= 16) {

                    }
                    else {
                        throw "密码必须介于4-16位之间!";
                    }

                    if (str1 != str2) {
                        throw "两次输入密码不正确!";
                    }

                    break;

                case "null":
                    AfterClear(inputid);
                    return;
                    break;
                case "notnull":  //不能为空
                    if (v == "") {

                        throw CkText;
                    }

                    break;
                case "mail":
                    if (!isEmail(v)) {
                        throw "邮件格式不正确!";
                    }

                    break

                case "num":
                    if (!IsNum(v)) {
                        throw "必须是数字!";
                    }
                    break;


                case "phone":
                    if (!isTellNo(v)) {
                        throw "不正确的电话或手机号码!";
                    }
                    break;


                case "exmail":
                    AjaxPost("/ac/", "ExMail", {      //检测邮箱是否可以注册

                        Email: v

                    }, function (data) {
                        if (data.re != "ok") {



                            throw data.re;
                        }

                    });
                    break;


                case "exuserid":                           //检测用户名是否可以注册
                    AjaxPost("/ac/", "ExUserId", {

                        uid: v

                    }, function (data) {
                        if (data.re != "ok") {
                            throw data.re;
                        }

                    });
                    break;


                case "exhasuseridoremail":
                    AjaxPost("/ac/", "ExHasUserIdOrEmail", {   //检测用户名或密码是否存在

                        inputStr: v

                    }, function (data) {
                        if (data.re != "ok") {

                            if (CkText == "") {
                                throw data.re;
                            }
                            else {
                                throw CkText;
                            }
                        }

                    });
                    break;

                default:

            }




            obj.removeClass("NoGood");
            obj.addClass("Good");

            obj.next("b").remove();
            obj.after("<b class='pageright'><span>s</span></b>");


        } catch (e) {  //出错!
            AfterPageWorng(inputid, e.toString());
        }


    });
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

function AfterClear(objId) {
    var obj = $("#" + objId + "");
    obj.next("b").remove();

}

function AfterPageWriting(objId, txt) {

    var obj = $("#" + objId + "");
    obj.removeClass("NoGood");

    obj.addClass("writing");

    obj.next("b").remove();
    obj.after("<b class='pagewriting'>" + txt + "<span>s</span></b>");

}

function AfterPageRight(objId, txt) {
    var obj = $("#" + objId + "");
    obj.removeClass("NoGood");
    obj.addClass("Good");

    obj.next("b").remove();
    obj.after("<b class='pageright'>" + txt + "<span>s</span></b>");
}

function AfterPageWorng(objId, txt) {
    var obj = $("#" + objId + "");
    obj.next("b").remove();
    var Bred = "<b class='pageworng'>" + txt.toString() + "</b>";



    if (obj.next().hasClass("pageworng")) {

    }
    else {
        obj.after(Bred);
    }

    obj.removeClass("Good");
    obj.addClass("NoGood");
}


//检测是否规则的时间字符串
function InputDateTimeCheck(inputid) {
    var input_dt = $("#" + inputid + "");
    var Bred = "<b class='pageworng'>不规则的时间字符串!</b>";
    var str = input_dt.val();
    if (isValidDate(str)) {
        $("#" + inputid + "").next("b[class='pageworng']").remove();
    }
    else {
        if (input_dt.next("b[class='pageworng']").length == 0) {
            input_dt.after(Bred);
        }
    }

}


//检测是否为空
function InputNullCheckReBool(inputid) {
    var InputValue = $("#" + inputid + "").val();
    InputValue = $.trim(InputValue);

    if (InputValue == null || InputValue == "") {
        return true;
    }
    else {
        return false;
    }
}


//检测是否规则的时间字符串
function InputDateTimeCheckReBool(inputid) {
    var input_dt = $("#" + inputid + "");
    var str = input_dt.val();
    if (isValidDate(str)) {
        return false;
    }
    else {
        return true;
    }

}


//#region 时间

//转化时间字符串
function DateFormat(str, yyMMdd) {
    if (yyMMdd == null) {
        yyMMdd = "yyyy-MM-dd";
    }

    var d;
    if (str == null || str == "") {
        d = new Date();
    }
    else {
        d = new Date(str);

    }


    return d.Format(yyMMdd);


}

//比较时间,如果第一个大返回1, 第二个大返回2, 相等返回0
function BiJiaoTime(Time1, Time2) {

    Time1 = ConvertToString(Time1);
    Time2 = ConvertToString(Time2);

    var oDate1 = new Date(Time1);
    var oDate2 = new Date(Time2);
    if (oDate1.getTime() > oDate2.getTime()) {
        return 1;
    } else if (oDate1.getTime() < oDate2.getTime()) {
        return 2;
    }
    else if (oDate1.getTime() == oDate2.getTime()) {
        return 0;
    }
    else {

        alert("比较时间出错[" + oDate1 + "][" + oDate2 + "]");
    }
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



//#region 页面操作

function shuaxin() {
    window.location = window.location;
}
//返回上一页
function fanhui() {
    history.back(-1);
}

function qianjin() {
    history.back(1);
}

function tiaozhuan(url) {
    window.location.href = url;
}


//#endregion



//#region json相关

function GetCurrentPage() {

    var c = ConvertToInt(GetQueryString("CurrentPage"));
    if (c == 0) {

        c = 1;
    }

    return c;
}

function GetQueryString(name, val) {   //获取QueryString参数,如果无则返回""
    if (val == null) {
        val = "";
    }
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        val = unescape(r[2]);

    if (val == null) {
        val = "";
    }
    if (val == "undefined") {
        val = "";
    }

    return val;
}

function GetQueryString2(name) {   //获取QueryString参数,如果无则返回""
    var reg = new RegExp("(^|/)" + name + "=([^/]*)(/|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return "";
}


//将JSON等其他元素转化成字符串并返回
function JsonToStr(object) {
    var type = typeof object;
    if ('object' == type) {
        if (Array == object.constructor)
            type = 'array';
        else if (RegExp == object.constructor)
            type = 'regexp';
        else
            type = 'object';
    }
    switch (type) {
        case 'undefined':
            return "\"\"";
            break;
        case 'unknown':
            return;
            break;
        case 'function':
        case 'boolean':
        case 'regexp':
            return object.toString();
            break;
        case 'number':
            return isFinite(object) ? object.toString() : 'null';
            break;
        case 'string':
            return '"' + object.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g,
               function () {
                   var a = arguments[0];
                   return (a == '\n') ? '\\n' :
                               (a == '\r') ? '\\r' :
                               (a == '\t') ? '\\t' : ""
               }) + '"';
            break;
        case 'object':
            if (object === null) return 'null';
            var results = [];
            for (var property in object) {
                var value = JsonToStr(object[property]);
                if (value !== undefined)
                    results.push(JsonToStr(property) + ':' + value);
            }
            return '{' + results.join(',') + '}';
            break;
        case 'array':
            var results = [];
            for (var i = 0; i < object.length; i++) {
                var value = JsonToStr(object[i]);
                if (value !== undefined) results.push(value);
            }
            return '[' + results.join(',') + ']';
            break;
    }
}
function json2str(o) {
    var arr = [];
    var fmt = function (s) {
        if (typeof s == 'object' && s != null) return json2str(s);
        return /^(string|number)$/.test(typeof s) ? "\"" + s + "\"" : s;
    }
    for (var i in o) arr.push("" + i + ":" + fmt(o[i]));
    return '{' + arr.join(',') + '}';
}
//查询JSON数组
function JsonArraySearch(option) {


    var jr = option.jr;//需要查询的数组
    var k = option.k;//需要查询的key名称
    var v = option.v;//需要查询的key值
    var type = option.type;
    if (type == null) {
        type = "Acc";  //准确查询
    }
    var new_jr = new Array();
    if (jr.length > 0) {
        for (var i = 0; i < jr.length; i++) {


            var j = jr[i];

            if (j[k] == v) {

                new_jr.push(j);
            }
        }


    }

    return new_jr;
}


//输出扩展属性
function JsonToParaStr(j) {
    var w = [];

    for (var i in j) {
        w.push(" ");
        w.push(i);
        w.push("=")
        w.push("'" + j[i] + "'");
        w.push(" ");
    }
    return w.join("");
}

//取得某一行的JSON的KEY值
function GetObj(json, index) {
    var i = 0;
    for (var obj in json) {
        if (i == index) {
            return obj.toString();
        }
        i++;
    }
}


//#endregion


//强制转换对象为字符串，如果对象为undefined或者NULL
function ConvertToString(obj) {


    var str = String(obj);

    if (str == "undefined" || str == "null") {
        return "";
    }

    return $.trim(str);
}


function ConvertToJson(str) {

    if (!str) {
        return {};
    }

    var typeStr = Object.prototype.toString.apply(str);

    if (typeStr == '[object Object]') {
        return str;
    }
    else if (typeStr == '[object String]') {

        var strJson = JSON.parse(str);
        return strJson;

    }
    else {

        return {};
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
function ShowModelessDialogSetting(url, W, H, scroll) {

    if (scroll == null) {
        scroll = "auto";
    }

    window.showModelessDialog(url, window, "dialogWidth:" + W + "px;dialogHeight:" + H + "px;center:1;scroll:" + scroll + ";help:1;edge: Raised;status:1");

}


function GetSideFrame(FrameId, JquerySelectStr) {
    return $(window.parent.frames[FrameId].document).find(JquerySelectStr);
}

function GetSideFramePage(FrameId) {
    return $(window.parent.frames[FrameId].document)[0];
}

function GetPWindowsByDialog() {
    var pWin = dialogArguments;
    return pWin;
}

function GetWxOpenName(str) {

    return str.substr(str.length - 4);
}

function ConvertToBool(str) {

    str = ConvertToString(str);
    if (str == null || str == "") {
        return false;
    }
    try {
        switch (str.toLowerCase()) {
            case "true": case "yes": case "1": case "checked": return true;
            case "false": case "no": case "0": case null: case "": case "null": case "undefined": return false;
            default: return true;
        }
    } catch (e) {
        return false;
    }

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

//document.write("前天：" + GetDateStr(-2));
//document.write("<br />昨天：" + GetDateStr(-1));
//document.write("<br />今天：" + GetDateStr(0));
//document.write("<br />明天：" + GetDateStr(1));
//document.write("<br />后天：" + GetDateStr(2));
//document.write("<br />大后天：" + GetDateStr(3));
function GetDateStr(AddDayCount) {
    var dd = new Date();

    dd.setDate(dd.getDate() + AddDayCount);//获取AddDayCount天后的日期
    //var y = dd.getFullYear();
    //var m = dd.getMonth() + 1;//获取当前月份的日期
    //var d = dd.getDate();


    return dd.Format("yyyy-MM-dd");
}

//绑定时间控件

//绑定时间控件
function BindDateInput(inputId, txt, hasTime) {




    loadExtentFile("/PubUI/My97DatePicker/WdatePicker.js", "js");




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
        if (hasTime) {
            WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });
        }
        else {
            WdatePicker();
        }

    });

}

/*#region 页面配置 */



function LoadConfig(ConfigJsonArray) {
    //    var ConfigJson = {
    //        htmlId: "",
    //        hide: false,
    //        must: true,
    //        next: "",
    //        patNext: "",
    //        defaultId: ""
    //        type:""

    //    };

    $.each(ConfigJsonArray, function (i) {
        var myJson = ConfigJsonArray[i];

        switch (myJson.htmlId) {
            case null:
                break;
            case "":
                break;
            case undefined:
                break;
            default:
                BindHtmlConfig(myJson);
        }
    });


}


function GetJsonValue(json, valName) {
    try {
        eval("return " + json.valName + ";");
    } catch (e) {
        return null;
    }
}
/*#endregion */

/*#region 气球提示框 */

var WaterNum = 1;
function waterCk(obj) {
    var objId = $(obj).attr("objId");

    $("#" + objId + "").focus();

}

function BindWater(objId, txt, fontSize, top) {
    $(".div_water[objId='" + objId + "']").remove();

    var obj = $("#" + objId)[0];

    if (obj == null) {
        return;
    }
    WaterNum++;
    top = ConvertToInt(top);

    if (fontSize == null) {

        fontSize == 12;
    }


    var Y = $("#" + objId).offset().top + top;
    var X = $("#" + objId).offset().left + 4;
    var w = new Array();
    var waterId = "div_water" + WaterNum + "";
    w.push("<div  id='" + waterId + "' class='div_water' onclick='waterCk(this)' ");
    w.push(" objId='" + objId + "'  style='left:" + X + "px; top:" + Y + "px; font-size:" + fontSize + "px; ' >");
    w.push(txt);
    w.push("</div> ");
    $(obj).before(w.join(""));


    //$("#" + objId).focus();
    //$("#" + objId).blur();
    $("#" + waterId + "").offset({ top: Y, left: X });
    if ($(obj).val() != "") {
        HideBindWater(obj);

    }
    $(obj).focus(function () {
        HideBindWater(obj);

    });



    $(obj).blur(function () {
        var txt = $.trim($(obj).val());
        if (txt == "" || txt == null) {

            ShowWater(obj);
        }

    });
}

function HideBindWater(obj) {

    $(".div_water[objId='" + obj.id + "']").hide();
}
function ShowWater(obj) {
    $(".div_water[objId='" + obj.id + "']").show();

}



var TipNum = 1;
function BindTips(objId, txt, W, L, op) {
    /// <summary>气球悬浮对话框</summary> 
    /// <param name="obj" type="Dom">要添加气球的元素ID</param>
    /// <param name="txt" type="String">气球的内容，可以为HTML</param>
    /// <param name="W" type="Int">气球的宽度</param>

    var obj = document.getElementById(objId);
    TipNum++;
    if (L == null) {
        L = 0;
    }

    if (op == null) {
        op = {
            type: "top"
        }
    }



    var Y = $(obj).offset().top;
    var X = $(obj).offset().left;
    X = X + L;
    var w = new Array();
    var imgArrow = "";
    switch (op.type) {
        case "top":
            imgArrow = ("<img class='tip_Arrow' style='margin-left:" + L + "px;' src='/images/Icon/bg.png' />");
            Y = Y + 3;
            break;
        case "bottom":
            imgArrow = ("<img class='tip_Arrow2' style='margin-left:" + L + "px;' src='/images/Icon/bg2.png' />");
            Y = Y + 20;
            break;
        default:

    }


    w.push(" <div id='tip" + TipNum + "' class='div_tip' style='left:" + X + "px;top:" + Y + "px;'>");

    w.push("<div class='div_tip_content' style='width:" + W + "px;' >");
    w.push("" + txt + "");
    w.push("</div>");

    w.push(imgArrow);


    w.push("</div>");
    $("body").append(w.join(""));
    var tipH = 0;
    if (op.type == "top") {
        tipH = $("#tip" + TipNum + "").height();
    }
    else {

    }
    $("#tip" + TipNum + "").offset({ top: Y - tipH, left: X });
}
function ClearTips() {
    /// <summary>清空所有的气球</summary>
    $(".div_tip").remove();
}

function ClearTipByInput(obj) {
    /// <summary>清空一个元素上的气球</summary>
    /// <param name="obj" type="Dom">需要清空的元素： $(元素)[0]</param>
    $(obj).prev(".div_tip").remove();
}

/*#endregion */

/*#region 判断大写是否打开 */

function detectCapsLock(event) {
    var e = event || window.event;
    var o = e.target || e.srcElement;

    var keyCode = e.keyCode || e.which; // 按键的keyCode 
    var isShift = e.shiftKey || (keyCode == 16) || false; // shift键是否按住
    if (
     ((keyCode >= 65 && keyCode <= 90) && !isShift) // Caps Lock 打开，且没有按住shift键 
     || ((keyCode >= 97 && keyCode <= 122) && isShift)// Caps Lock 打开，且按住shift键
     ) {
        //开了
        return true;
    }
    else {
        //没有开
        return false;
    }
}
/*#endregion */

/*#region 非法字符检测 */
function CheckStr(strForText) {
    var str = "[@/'\"#$%&^*]+";
    var reg = new RegExp(str);
    if (reg.test(strForText)) {
        return true;
    }
    else {
        return false;
    }
}

/*#endregion */


/*#region 年龄转换方法 */
function getAge(brDateStr) {
    /// <summary>输入生日，得到年龄</summary>
    /// <param name="brDateStr" type="DateTime">生日</param>
    var birthday = new Date(brDateStr.replace(/-/g, "\/"));
    var d = new Date();
    var age = d.getFullYear() - birthday.getFullYear() - ((d.getMonth() < birthday.getMonth() || d.getMonth() == birthday.getMonth() && d.getDate() < birthday.getDate()) ? 1 : 0);
    return age;
}

/*#endregion */

/*#region 身份证验证 */
function isIdCardNo(num) {
    var factorArr = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1);
    var error;
    var varArray = new Array();
    var intValue;
    var lngProduct = 0;
    var intCheckDigit;
    var intStrLen = num.length;
    var idNumber = num;
    // initialize
    if ((intStrLen != 15) && (intStrLen != 18)) {
        //error = "输入身份证号码长度不对！";
        //alert(error);
        //frmAddUser.txtIDCard.focus();
        return false;
    }
    // check and set value
    for (i = 0; i < intStrLen; i++) {
        varArray[i] = idNumber.charAt(i);
        if ((varArray[i] < '0' || varArray[i] > '9') && (i != 17)) {
            //error = "错误的身份证号码！.";
            //alert(error);
            //frmAddUser.txtIDCard.focus();
            return false;
        } else if (i < 17) {
            varArray[i] = varArray[i] * factorArr[i];
        }
    }
    if (intStrLen == 18) {
        //check date
        var date8 = idNumber.substring(6, 14);
        if (checkDate(date8) == false) {
            //error = "身份证中日期信息不正确！.";
            //alert(error);
            return false;
        }
        // calculate the sum of the products
        for (i = 0; i < 17; i++) {
            lngProduct = lngProduct + varArray[i];
        }
        // calculate the check digit
        intCheckDigit = 12 - lngProduct % 11;
        switch (intCheckDigit) {
            case 10:
                intCheckDigit = 'X';
                break;
            case 11:
                intCheckDigit = 0;
                break;
            case 12:
                intCheckDigit = 1;
                break;
        }
        // check last digit
        if (varArray[17].toUpperCase() != intCheckDigit) {
            //error = "身份证效验位错误!...正确为： " + intCheckDigit + ".";
            //alert(error);
            return false;
        }
    }
    else {        //length is 15
        //check date
        var date6 = idNumber.substring(6, 12);
        if (checkDate(date6) == false) {
            //alert("身份证日期信息有误！.");
            return false;
        }
    }
    //alert ("Correct.");
    return true;
}

function checkDate(date) {
    return true;
}
/*#endregion */

/*#region 邮政编码验证 */
function isPostNo(str) {
    if (str.length != 6) {
        str = "";
        return false;
    }
    else {
        return IsNum(str);
    }
}
/*#endregion */

/*#region 是否为数字 */
function IsNum(s) {
    if (s != null && s != "") {
        return !isNaN(s);
    }
    return false;
}
/*#endregion */

/*#region 验证float */
function IsFloat(_value) {
    var str = _value.replace(/(^\s*)|(\s*$)/g, "");
    var reValue = false;
    if (str.length != 0) {
        reg = /^\d+(\.\d+)?$/;
        if (!reg.test(str)) {
            reValue = false;
        }
        else {
            reValue = true;
        }
    }
    return reValue;
}
/*#endregion */

/*#region 是否为整数 */
function IsInt(s) {
    var reg = /^[0-9]*[1-9][0-9]*$/;
    return reg.test(s);
}
/*#endregion*/

//#region 判断电子邮件

function isEmail(strEmail) {


    if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
        return true;
    else
        return false;
}
//#endregion



//#region 判断电话号码 */
function isTellNo(phone) {
    var flag = false;
    var reg0 = /^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/;   //判断 固话   
    var reg1 = /^((\(\d{2,3}\))|(\d{3}\-))?(13|15|18)\d{9}$/;                     //判断 手机   
    if (reg0.test(phone)) flag = true;
    if (reg1.test(phone)) flag = true;
    return flag;
}
//#endregion */

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
function ConvertToDecimal(val, num) {

    if (!val) {
        return 0;
    }

    var valFloat = 0;
    try {
        valFloat = parseFloat(val);
    } catch (e) {
        return 0;
    }
    var multiplicand = Math.pow(10, num);
    var result = Math.round(valFloat * multiplicand) / multiplicand;
    if (isDecimal(result)) {
        return result;
    }
    else {
        return result + ".00";
    }
}



function isDecimal(s) {


    var m = s.toString();
    if (m.indexOf(".") > 0) {
        return true;  //是小数
    }
    else {
        return false; //不是小数
    }
}
/*#endregion */

//#region 判断几位小数

function checkDecimals(decimal, num) {
    var decallowed = num;
    var revValue = decimal.toString();
    if (revValue.indexOf('.') == -1) {
        revValue += ".";
    }
    var dectext = revValue.substring(revValue.indexOf('.') + 1, revValue.length);
    if (dectext.length > decallowed) {
        //        alert("careful!,don't write more than " + decallowed + " decimal. please try again!");
        //        document.getElementById("rev").focus();
        return false;
    } else {
        return true;
    }
}

//#endregion

//#region 除法函数

//除法
function accDiv(arg1, arg2) {
    var t1 = 0, t2 = 0, r1, r2;
    try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
    try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""))
        r2 = Number(arg2.toString().replace(".", ""))
        return (r1 / r2) * pow(10, t2 - t1);
    }
}

//乘法
function accMul(arg1, arg2) {
    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
    try { m += s1.split(".")[1].length } catch (e) { }
    try { m += s2.split(".")[1].length } catch (e) { }
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
}



//加法
function accAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2))
    return (arg1 * m + arg2 * m) / m
}


//减法
function accSubtr(arg1, arg2) {
    var r1, r2, m, n;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    //动态控制精度长度
    n = (r1 >= r2) ? r1 : r2;
    return ((arg1 * m - arg2 * m) / m).toFixed(n);
}
//给Number类型增加一个subtr 方法，调用起来更加方便。 
Number.prototype.subtr = function (arg) {
    return accSubtr(arg, this);
}

//#endregion 

//#region 系统计算器

function exec(command) {
    if (command == null) {
        command = "calc.exe";
    }

    window.oldOnError = window.onerror;
    window._command = command;
    window.onerror = function (err) {
        if (err.indexOf('utomation') != -1) {
            alert('命令' + window._command + '   已经被用户禁止！');
            return true;
        }
        else return false;
    };
    var wsh = new ActiveXObject('WScript.Shell');
    if (wsh)
        wsh.Run(command);
    window.onerror = window.oldOnError;
}

//#endregion


//#region 快捷操作流程 
var process = 1;
function ProcessToNext() {

    $("[process='" + process + "']").hide();
    //    $("[process='" + process + "']").addClass("processSelect");
    var nextProcess = parseInt(process) + 1;
    $("[process='" + nextProcess + "']").show();




    $("[Sprocess='" + nextProcess + "']").addClass("SprocessSelect");

    process++;

}

function ProcessToPre() {

    if (process == 1) {
        return;
    }
    $("[process='" + process + "']").hide();
    $("[Sprocess='" + process + "']").removeClass("SprocessSelect");
    var preProcess = parseInt(process) - 1;
    $("[process='" + preProcess + "']").show();
    process = process - 1;
}

//#endregion 

//#region 判断是否隐藏




function IsHide(obj) {
    var temp = $(obj).is(":hidden"); //是否隐藏
    return temp;

}

//#endregion


//#region 分页

function BeforePageBtn(CurrentPage, obj) {
    //var PageType = $(obj).parent().parent().attr("PageType");
    //if (PageType.toLowerCase() == "url") {
    //    var cUrlParam = ConvertToInt(GetQueryString("CurrentPage"));

    //    if (cUrlParam == 0) {

    //        //页面中没有CurrentPage参数
    //        if (HasStr(window.location.href, "?")) {

    //            tiaozhuan(window.location.href + "&CurrentPage=" + CurrentPage + "");
    //        }
    //        else {
    //            tiaozhuan(window.location.href + "?CurrentPage=" + CurrentPage + "");

    //        }
    //        return;
    //    }
    //    else {
    //        //已经有了CurrentPage参数
    //        if (cUrlParam != CurrentPage) {
    //            var httpUrl = window.location.href;
    //            httpUrl = httpUrl.replace("CurrentPage=" + cUrlParam + "", "CurrentPage=" + CurrentPage + "");

    //            tiaozhuan(httpUrl);

    //        }
    //    }

    //}


}


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

        pageArray.push("<a id='a_page_" + PageId + "_" + i + "' pageNum='" + i + "' onclick='BeforePageBtn(" + i + ",this); " + sObj + "(" + i + "," + PageId + ")'>" + i + "</a>");
    }
    var currentId = "a_page_" + PageId + "_" + currentpage + "";
    $("#span_pageMenu_" + PageId + "").html(pageArray.join(""));
    $("#" + PageId + "").children(".cruuent").removeClass("current");
    $("#" + currentId + "").addClass("current");
    HidePageInt(PageId); //隐藏列
}

function ReIndex(sObj) {
    var pageIndex = parseInt($("#span_pageMenu_" + PageId + " .current").attr("pageNum"));
    eval("BeforePageBtn(pageIndex,this); " + sObj + "(pageIndex);");

}

function toNextPage(sObj, PageId) {
    /// <summary>下一页</summary>
    var pageIndex = parseInt($("#span_pageMenu_" + PageId + " .current").attr("pageNum"));
    pageIndex++;

    if ($("#span_pageMenu_" + PageId + " a[pageNum='" + pageIndex + "']").length == 0) {
        return false;
    }
    else {
        eval(" BeforePageBtn(pageIndex,this); " + sObj + "(pageIndex);");
    }

}
function toPrevPage(sObj, PageId) {

    var pageIndex = ConvertToInt($("#span_pageMenu_" + PageId + " .current").attr("pageNum"));

    pageIndex = pageIndex - 1;

    if (pageIndex < 1 || pageIndex == null || pageIndex == NaN) {
        return false;
    }
    else {
        eval("BeforePageBtn(pageIndex,this); " + sObj + "(pageIndex);");
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

        eval("BeforePageBtn(ToPageNum,this); " + sObj + "(ToPageNum);");
    }
}
//#endregion


///计算两个整数的百分比值 
function GetPercent(num, total) {
    num = parseFloat(num);
    total = parseFloat(total);
    if (isNaN(num) || isNaN(total)) {
        return "-";
    }
    return total <= 0 ? "0%" : (Math.round(num / total * 10000) / 100.00 + "%");
}


//是否安装了FrameWork版本 传入数字1,2,3,4
function hasDotNetFramework(baseVersion) {
    //  alert( navigator.userAgent.toLowerCase() );
    if (typeof baseVersion == "undefined") baseVersion = 1;
    var userAgent = navigator.userAgent.toLowerCase();
    for (var i = baseVersion; i < 10; i++) {
        if (userAgent.indexOf('.net clr ' + i + '.') > -1) {
            return true;
        }
    }
    return false;
}


//选项卡绑定
function BindTab() {



    $(".dl_tab>dt>b").click(function () {


        $(this).siblings().removeClass("select");
        $(this).addClass("select");

        $(this).parent().parent().children("dd").children("div[tb]").hide();
        var tabId = $(this).attr("tb");

        $(this).parent().parent().children("dd").children("div[tb='" + tabId + "']").show();

    });
    $(".dl_tab>dt").each(function () {


        $(this).children("b").eq(0).click();
    });
}

function tabTable1(objId) {


    var obj = $("#" + objId + "")[0];

    $(obj).find("td").each(function () {
        $(this).click(function () {
            //当选项卡点击时

            $(obj).find("td").removeClass("tabSelect");
            $(this).addClass("tabSelect");
            var tab = $(this).attr("tab");
            $(".tab_content").hide();
            $(".tab_content[tab='" + tab + "']").show();

        });


    });
    $(obj).find("td").eq(0).click();

}



//Iframe自适应高度
function setIframeHeight(obj) {
    var iframe = obj;
    if (document.getElementById) {
        if (iframe && !window.opera) {
            if (iframe.contentDocument && iframe.contentDocument.body.offsetHeight) {
                iframe.height = iframe.contentDocument.body.offsetHeight + 20;
            }
            else if (iframe.Document && iframe.Document.body.scrollHeight) {
                iframe.height = iframe.Document.body.scrollHeight + 20;
            }
        }
    }
}

function setIframeHeight2(iframeObj) {
    if (!iframeObj) return; iframeObj.height = (iframeObj.Document ? iframeObj.Document.body.scrollHeight : iframeObj.contentDocument.body.offsetHeight);

}




function login() {

    $("#div_noLogin").hide();

    $("#div_loginIn").show();
}

jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') {
        options = options || {};
        if (value === null) {
            value = '';
            options = $.extend({}, options);
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString();
        }
        var path = options.path ? '; path=' + (options.path) : '';
        var domain = options.domain ? '; domain=' + (options.domain) : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else {
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};



function FileImgExist(imgurl) {
    var ImgObj = new Image(); //判断图片是否存在  
    ImgObj.src = imgurl;

    //没有图片，则返回-1  
    if (ImgObj.fileSize > 0 || (ImgObj.width > 0 && ImgObj.height > 0)) {
        return true;
    } else {
        return false;
    }
}


//判断文件是否存在!
function FileExist(FileURL) {
    var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    xmlhttp.open("GET", FileURL, false);
    xmlhttp.send();
    if (xmlhttp.readyState == 4) {
        if (xmlhttp.status == 200) {
            return true;
        }
        else if (xmlhttp.status == 404) {
            return false;

        }

        else {
            return false;
        }
    }
}



function OpComment(CommentType, Para, fun) {



    var ifm = '<iframe id="ifm_SaveComment" src="/UI/SaveComment.aspx?' + Para + '" frameborder="0" style="height:300PX; width:500px; border:none;" border=0  />';

    ZyPopWindow(ifm, { 保存点评: "SaveComment", 关闭窗口: "Close" }, 500, 300, "" + fun + "", 100);


}

//弹出iframe弹窗
function OpIfmWindow(ifmId, url, btnJson, W, H, CallBackFun, top) {



    var ifm = '<iframe id="' + ifmId + '" src="' + url + '" frameborder="0" style="height:' + H + 'PX; width:' + W + 'px; border:none;" border=0  />';

    ZyPopWindow(ifm, btnJson, W, H, CallBackFun, top);


}



//参数为长宽比, 4|3
function OpImg() {



    var ifm = '<iframe id="ifm_SaveImg" src="/UI/CropPicture2.aspx frameborder="0" style="height:430PX; width:650px; border:none;" border=0  />';

    ZyPopWindow(ifm, {}, 650, 430, "", 20);


}



function OpImgCutting(wh) {
    /// <summary> 图像切割 </summary>
    /// <param name=" wh " type="String">  4|3 比例 </param>
    var ifm = '<iframe id="ifm_SaveImg" src="/UI/CropPicture.aspx?wh=' + wh + '" frameborder="0" style="height:530PX; width:830px; border:none;" border=0  />';

    ZyPopWindow(ifm, {}, 830, 530, "", 20);


}

function FormatImgWH(img, W, H) {

    var wid = img.width;
    if (wid == 0) {
        wid = img.naturalWidth;
    }

    var het = img.height;
    if (het == 0) {
        het = img.naturalHeight;
    }

    if (wid > het) {
        //这是一张宽图片
        return { width: W, height: "auto", style: " width:" + W + "px;height:auto; " }
    }
    else {
        //这是一张长图片
        return { width: "auto", height: H, style: " width:auto;height:" + H + "px; " }
    }

}



var ImageSet = {};
function SetMiddle(image, height) {/// <summary>重设图片大小后让图片相对于DIV居中</summary>
    if (typeof (image) == 'string') image = document.images[image] || document.getElementById(image);
    var div = image.parentNode;
    if (div.nodeName != "DIV") {
        div = div.parentNode;
    }
    if (image.height > 0 && image.height < height) {
        var marginTopVal = (height - image.height) / 2;
        image.style.marginTop = parseInt(marginTopVal) + "px";
        ///不加px,在火狐下不支持！}else{image.height = height;

    }
    else {

        image.style.marginTop = "0px";
    }
}
ImageSet.Resize1 = function (image, width, height) {
    if (width == null || height == null) return; image.removeAttribute('width');
    image.removeAttribute('height');
    var w = image.width, h = image.height;
    var scalingW = w / width, scalingH = h / height;
    var scaling = w / h;
    if (scalingW >= scalingH) {
        $(image).width(width);
        $(image).height(width / scaling);
    } else {

        $(image).width(height * scaling);
        $(image).height(height);
    }
}
function imgReSize(imgObj, w, h) {
    ImageSet.Resize1(imgObj, w, h);
    SetMiddle(imgObj, h);
}


function FormatImgTo200Px(ImgUrl) {
    var url = ConvertToString(ImgUrl).toLocaleLowerCase();


    url = url.replace("/upload/imgs/", "/upload200/imgs/");

    return url;
}

function zmnImgCenter(obj) {
    obj.each(function () {
        var $this = $(this);
        var objHeight = $this.height();//图片高度
        var objWidth = $this.width();//图片宽度
        var parentHeight = $this.parent().height();//图片父容器高度
        var parentWidth = $this.parent().width();//图片父容器宽度
        var ratio = objHeight / objWidth;
        if (objHeight > parentHeight && objWidth > parentWidth) {//当图片宽高都大于父容器宽高
            if (objHeight > objWidth) {//赋值宽高
                $this.width(parentWidth);
                $this.height(parentWidth * ratio);
            }
            else {
                $this.height(parentHeight);
                $this.width(parentHeight / ratio);
            }
            objHeight = $this.height();//重新获取宽高
            objWidth = $this.width();
            if (objHeight > objWidth) {
                $(this).css("top", (parentHeight - objHeight) / 2);
                //定义top属性
            }
            else {
                //定义left属性
                $(this).css("left", (parentWidth - objWidth) / 2);
            }
        }
        else {//当图片宽高小于父容器宽高
            if (objWidth > parentWidth) {//当图片宽大于容器宽，小于时利用css text-align属性居中
                $(this).css("left", (parentWidth - objWidth) / 2);
            }
            $(this).css("top", (parentHeight - objHeight) / 2);
        }
    })
}



function RndNum(n) {
    /// <summary> 随机数 </summary>
    /// <param name=" n " type="String"> 随机数位数  </param>

    var rnd = "";

    for (var i = 0; i < n; i++)

        rnd += Math.floor(Math.random() * 10);

    return rnd;

}


function RightMenu(obj, evArray) {
    /// <summary> 绑定右键菜单 </summary>
    /// <param name=" obj " type="String">   </param>
    /// <param name=" evArray " type="String">  [{  evName:"方法名(obj)" , Title:"删除", evIcon:""  }] </param>



    $(obj).mousedown(function () {

        $(".div_right_menu").remove();
    });

    var objId = $(obj).attr("id");

    if (objId == "" || objId == null) {
        objId = RndNum(4);
        $(obj).attr("id", objId);
    }

    $(obj).contextmenu(function (e) {






        $(".div_right_menu").remove();



        var x = e.pageX;
        var y = e.pageY;


        var w = new Array();
        w.push("<div  class='div_right_menu' style=' top: " + y + "px; left: " + x + "px; ' >");
        w.push("<ul>");
        for (var i = 0; i < evArray.length; i++) {

            var j = evArray[i];
            var EvName =
              w.push("<li onclick=\"" + j.evName + "(document.getElementById('" + objId + "'));clearRightMenu()\" >" + j.Title + "</li>");
        }


        w.push("</ul>");
        w.push("</div>");
        $("body").append(w.join(""));



        window.event.returnValue = false;
        //  alert(1);
        return;

        if (document.all) window.event.returnValue = false;// for IE  
        else event.preventDefault();
    });
    $("body").click(function () {

        $(".div_right_menu").remove();
    });

}

function clearRightMenu() {
    /// <summary> 清理右键菜单  </summary>

    $(".div_right_menu").remove();
}

function Selecter(jArray, objId) {

    var w = new Array();
    if (jArray.length == 0) {
        return "";
    }
    for (var i = 0; i < length; i++) {




    }
    w.push("<span id='" + objId + "' >");
    w.push("");
    w.push("</option>");
}

function GetChildSelectVal(objId) {
    var sel = $("#" + objId + "").find("select").last();
    return sel.val();
}

function SetChildSelectVal(objId, valId) {
    var op = $("#" + objId + "").find("option").last();
    op.parent().val(valId);

}

var Ctop = 0;
function SelInput(objId, option, CalFun) {
    var obj = $("#" + objId + "")
    var ja;
    $("#txt_Author").keyup(function () {


        if (event.keyCode != 38 && event.keyCode != 13 && event.keyCode != 40) {
            $("#div_selInput").remove();
            var inputCode = $(obj).val();
            option["InputCode"] = $(obj).val();

            AjaxPost("/ac/", "SelInput",
               option, function (data) {

                   var w = new Array();
                   if (data.re == "ok") {

                       w.push("<div id='div_selInput' class='div_selInput' >");
                       ja = data.ja;
                       if (data.ja.length > 0) {

                           w.push("<table id='tb_selInput' class='tb_selInput t1' > ")
                           for (var i = 0; i < data.ja.length; i++) {

                               var j = data.ja[i];
                               w.push("<tr  i='" + i + "'    >");

                               for (var x = 0; x < option.col.length; x++) {
                                   w.push("<td>");
                                   var colName = option.col[x];
                                   var colVal = j[colName];
                                   if (colName.split(":").length > 1) {
                                       colVal = j[colName.split(":")[0]];
                                       var colType = colName.split(":")[1];
                                       switch (colType) {
                                           case "img":
                                               w.push(" <img style='height:30px' src='" + colVal + "'  />")
                                               break;
                                           default:
                                               w.push(colVal);
                                               break;
                                       }
                                   }
                                   else {
                                       w.push(colVal);
                                   }
                                   w.push("</td>");

                               }
                               w.push("</tr>");


                           }
                           w.push("</table>");


                       }
                       else {
                           w.push("没有符合条件的记录!");
                       }
                       w.push("</div>");
                   }
                   else {
                       w.push(data.re);
                   }

                   $(obj).after(w.join(""));
                   $("#tb_selInput").find("tr").hover(function () {
                       $(this).addClass("select");

                   }, function () {
                       $(this).removeClass("select");
                   });



                   $("#tb_selInput").find("tr").mousedown(function () {
                       var CselectTr = $(this);
                       var i = ConvertToInt(CselectTr.attr("i"));
                       CalFun(ja[i]);
                       $("#div_selInput").remove();
                   });
               }, true);
        }
        else {
            //字母或者数字按键
            var CselectTr = $("#tb_selInput").find(".select");



            //    alert(myTop + "," + PTop);
            switch (event.keyCode) {
                case 38:  //上
                    var upTr = CselectTr.prev();
                    if (upTr.length == 0) {
                        upTr = $("#tb_selInput").children("tbody").children("tr").last();
                    }
                    $("#tb_selInput").find("tr").removeClass("select");
                    upTr = upTr.addClass("select");


                    break;
                case 40:  //下
                    var downTr = CselectTr.next();
                    if (downTr.length == 0) {
                        downTr = $("#tb_selInput").children("tbody").children("tr").first();
                    }
                    $("#tb_selInput").find("tr").removeClass("select");
                    downTr.addClass("select");


                    //    $("#div_selInput").animate({ scrollTop: (myTop - PTop) }, 100);//1000是ms,也可以用slow代替

                    break;

                case 13: //回车
                    var i = ConvertToInt(CselectTr.attr("i"));
                    CalFun(ja[i]);

                    $("#div_selInput").remove();
                    break;
                default:

                    break;
            }

        }



    });
    $("#txt_Author").blur(function () {
        $("#div_selInput").remove();
    });
}


(function ($) {
    // alert($.fn.scrollLoading);
    $.fn.scrollLoading = function (options) {
        var defaults = {
            attr: "data-url"
        };
        var params = $.extend({}, defaults, options || {});
        params.cache = [];
        $(this).each(function () {
            var node = this.nodeName.toLowerCase(), url = $(this).attr(params["attr"]);
            if (!url) { return; }
            var data = {
                obj: $(this),
                tag: node,
                url: url
            };
            params.cache.push(data);
        });


        var loading = function () {
            var st = $(window).scrollTop(), sth = st + $(window).height();
            $.each(params.cache, function (i, data) {
                var o = data.obj, tag = data.tag, url = data.url;
                if (o) {
                    post = o.position().top; posb = post + o.height();
                    if ((post > st && post < sth) || (posb > st && posb < sth)) {
                        if (tag === "img") {
                            o.attr("src", url);
                        } else {
                            o.load(url);
                        }
                        data.obj = null;
                    }
                }
            });
            return false;
        };


        loading();
        $(window).bind("scroll", loading);
    };
})(jQuery);



//#region  加密解密方法



/// 加解密主调方法
/// beinetkey         密钥
/// message     加密时为待加密的字符串，解密时为待解密的字符串
/// encrypt     1：加密；0：解密
/// mode        true：CBC mode；false：非CBC mode
/// iv          初始化向量
function des(beinetkey, message, encrypt, mode, iv) {
    //declaring this locally speeds things up a bit
    var spfunction1 = new Array(0x1010400, 0, 0x10000, 0x1010404, 0x1010004, 0x10404, 0x4, 0x10000, 0x400, 0x1010400, 0x1010404, 0x400, 0x1000404, 0x1010004, 0x1000000, 0x4, 0x404, 0x1000400, 0x1000400, 0x10400, 0x10400, 0x1010000, 0x1010000, 0x1000404, 0x10004, 0x1000004, 0x1000004, 0x10004, 0, 0x404, 0x10404, 0x1000000, 0x10000, 0x1010404, 0x4, 0x1010000, 0x1010400, 0x1000000, 0x1000000, 0x400, 0x1010004, 0x10000, 0x10400, 0x1000004, 0x400, 0x4, 0x1000404, 0x10404, 0x1010404, 0x10004, 0x1010000, 0x1000404, 0x1000004, 0x404, 0x10404, 0x1010400, 0x404, 0x1000400, 0x1000400, 0, 0x10004, 0x10400, 0, 0x1010004);
    var spfunction2 = new Array(-0x7fef7fe0, -0x7fff8000, 0x8000, 0x108020, 0x100000, 0x20, -0x7fefffe0, -0x7fff7fe0, -0x7fffffe0, -0x7fef7fe0, -0x7fef8000, -0x80000000, -0x7fff8000, 0x100000, 0x20, -0x7fefffe0, 0x108000, 0x100020, -0x7fff7fe0, 0, -0x80000000, 0x8000, 0x108020, -0x7ff00000, 0x100020, -0x7fffffe0, 0, 0x108000, 0x8020, -0x7fef8000, -0x7ff00000, 0x8020, 0, 0x108020, -0x7fefffe0, 0x100000, -0x7fff7fe0, -0x7ff00000, -0x7fef8000, 0x8000, -0x7ff00000, -0x7fff8000, 0x20, -0x7fef7fe0, 0x108020, 0x20, 0x8000, -0x80000000, 0x8020, -0x7fef8000, 0x100000, -0x7fffffe0, 0x100020, -0x7fff7fe0, -0x7fffffe0, 0x100020, 0x108000, 0, -0x7fff8000, 0x8020, -0x80000000, -0x7fefffe0, -0x7fef7fe0, 0x108000);
    var spfunction3 = new Array(0x208, 0x8020200, 0, 0x8020008, 0x8000200, 0, 0x20208, 0x8000200, 0x20008, 0x8000008, 0x8000008, 0x20000, 0x8020208, 0x20008, 0x8020000, 0x208, 0x8000000, 0x8, 0x8020200, 0x200, 0x20200, 0x8020000, 0x8020008, 0x20208, 0x8000208, 0x20200, 0x20000, 0x8000208, 0x8, 0x8020208, 0x200, 0x8000000, 0x8020200, 0x8000000, 0x20008, 0x208, 0x20000, 0x8020200, 0x8000200, 0, 0x200, 0x20008, 0x8020208, 0x8000200, 0x8000008, 0x200, 0, 0x8020008, 0x8000208, 0x20000, 0x8000000, 0x8020208, 0x8, 0x20208, 0x20200, 0x8000008, 0x8020000, 0x8000208, 0x208, 0x8020000, 0x20208, 0x8, 0x8020008, 0x20200);
    var spfunction4 = new Array(0x802001, 0x2081, 0x2081, 0x80, 0x802080, 0x800081, 0x800001, 0x2001, 0, 0x802000, 0x802000, 0x802081, 0x81, 0, 0x800080, 0x800001, 0x1, 0x2000, 0x800000, 0x802001, 0x80, 0x800000, 0x2001, 0x2080, 0x800081, 0x1, 0x2080, 0x800080, 0x2000, 0x802080, 0x802081, 0x81, 0x800080, 0x800001, 0x802000, 0x802081, 0x81, 0, 0, 0x802000, 0x2080, 0x800080, 0x800081, 0x1, 0x802001, 0x2081, 0x2081, 0x80, 0x802081, 0x81, 0x1, 0x2000, 0x800001, 0x2001, 0x802080, 0x800081, 0x2001, 0x2080, 0x800000, 0x802001, 0x80, 0x800000, 0x2000, 0x802080);
    var spfunction5 = new Array(0x100, 0x2080100, 0x2080000, 0x42000100, 0x80000, 0x100, 0x40000000, 0x2080000, 0x40080100, 0x80000, 0x2000100, 0x40080100, 0x42000100, 0x42080000, 0x80100, 0x40000000, 0x2000000, 0x40080000, 0x40080000, 0, 0x40000100, 0x42080100, 0x42080100, 0x2000100, 0x42080000, 0x40000100, 0, 0x42000000, 0x2080100, 0x2000000, 0x42000000, 0x80100, 0x80000, 0x42000100, 0x100, 0x2000000, 0x40000000, 0x2080000, 0x42000100, 0x40080100, 0x2000100, 0x40000000, 0x42080000, 0x2080100, 0x40080100, 0x100, 0x2000000, 0x42080000, 0x42080100, 0x80100, 0x42000000, 0x42080100, 0x2080000, 0, 0x40080000, 0x42000000, 0x80100, 0x2000100, 0x40000100, 0x80000, 0, 0x40080000, 0x2080100, 0x40000100);
    var spfunction6 = new Array(0x20000010, 0x20400000, 0x4000, 0x20404010, 0x20400000, 0x10, 0x20404010, 0x400000, 0x20004000, 0x404010, 0x400000, 0x20000010, 0x400010, 0x20004000, 0x20000000, 0x4010, 0, 0x400010, 0x20004010, 0x4000, 0x404000, 0x20004010, 0x10, 0x20400010, 0x20400010, 0, 0x404010, 0x20404000, 0x4010, 0x404000, 0x20404000, 0x20000000, 0x20004000, 0x10, 0x20400010, 0x404000, 0x20404010, 0x400000, 0x4010, 0x20000010, 0x400000, 0x20004000, 0x20000000, 0x4010, 0x20000010, 0x20404010, 0x404000, 0x20400000, 0x404010, 0x20404000, 0, 0x20400010, 0x10, 0x4000, 0x20400000, 0x404010, 0x4000, 0x400010, 0x20004010, 0, 0x20404000, 0x20000000, 0x400010, 0x20004010);
    var spfunction7 = new Array(0x200000, 0x4200002, 0x4000802, 0, 0x800, 0x4000802, 0x200802, 0x4200800, 0x4200802, 0x200000, 0, 0x4000002, 0x2, 0x4000000, 0x4200002, 0x802, 0x4000800, 0x200802, 0x200002, 0x4000800, 0x4000002, 0x4200000, 0x4200800, 0x200002, 0x4200000, 0x800, 0x802, 0x4200802, 0x200800, 0x2, 0x4000000, 0x200800, 0x4000000, 0x200800, 0x200000, 0x4000802, 0x4000802, 0x4200002, 0x4200002, 0x2, 0x200002, 0x4000000, 0x4000800, 0x200000, 0x4200800, 0x802, 0x200802, 0x4200800, 0x802, 0x4000002, 0x4200802, 0x4200000, 0x200800, 0, 0x2, 0x4200802, 0, 0x200802, 0x4200000, 0x800, 0x4000002, 0x4000800, 0x800, 0x200002);
    var spfunction8 = new Array(0x10001040, 0x1000, 0x40000, 0x10041040, 0x10000000, 0x10001040, 0x40, 0x10000000, 0x40040, 0x10040000, 0x10041040, 0x41000, 0x10041000, 0x41040, 0x1000, 0x40, 0x10040000, 0x10000040, 0x10001000, 0x1040, 0x41000, 0x40040, 0x10040040, 0x10041000, 0x1040, 0, 0, 0x10040040, 0x10000040, 0x10001000, 0x41040, 0x40000, 0x41040, 0x40000, 0x10041000, 0x1000, 0x40, 0x10040040, 0x1000, 0x41040, 0x10001000, 0x40, 0x10000040, 0x10040000, 0x10040040, 0x10000000, 0x40000, 0x10001040, 0, 0x10041040, 0x40040, 0x10000040, 0x10040000, 0x10001000, 0x10001040, 0, 0x10041040, 0x41000, 0x41000, 0x1040, 0x1040, 0x40040, 0x10000000, 0x10041000);

    //create the 16 or 48 subkeys we will need
    var keys = des_createKeys(beinetkey);
    var m = 0, i, j, temp, temp2, right1, right2, left, right, looping;
    var cbcleft, cbcleft2, cbcright, cbcright2
    var endloop, loopinc;
    var len = message.length;
    var chunk = 0;
    //set up the loops for single and triple des
    var iterations = keys.length == 32 ? 3 : 9; //single or triple des
    if (iterations == 3) { looping = encrypt ? new Array(0, 32, 2) : new Array(30, -2, -2); }
    else { looping = encrypt ? new Array(0, 32, 2, 62, 30, -2, 64, 96, 2) : new Array(94, 62, -2, 32, 64, 2, 30, -2, -2); }

    message += '\0\0\0\0\0\0\0\0'; //pad the message out with null bytes
    //store the result here
    result = '';
    tempresult = '';

    if (mode == 1) {//CBC mode
        cbcleft = (iv.charCodeAt(m++) << 24) | (iv.charCodeAt(m++) << 16) | (iv.charCodeAt(m++) << 8) | iv.charCodeAt(m++);
        cbcright = (iv.charCodeAt(m++) << 24) | (iv.charCodeAt(m++) << 16) | (iv.charCodeAt(m++) << 8) | iv.charCodeAt(m++);
        m = 0;
    }

    //loop through each 64 bit chunk of the message
    while (m < len) {
        if (encrypt) {//加密时按双字节操作
            left = (message.charCodeAt(m++) << 16) | message.charCodeAt(m++);
            right = (message.charCodeAt(m++) << 16) | message.charCodeAt(m++);
        } else {
            left = (message.charCodeAt(m++) << 24) | (message.charCodeAt(m++) << 16) | (message.charCodeAt(m++) << 8) | message.charCodeAt(m++);
            right = (message.charCodeAt(m++) << 24) | (message.charCodeAt(m++) << 16) | (message.charCodeAt(m++) << 8) | message.charCodeAt(m++);
        }
        //for Cipher Block Chaining mode,xor the message with the previous result
        if (mode == 1) { if (encrypt) { left ^= cbcleft; right ^= cbcright; } else { cbcleft2 = cbcleft; cbcright2 = cbcright; cbcleft = left; cbcright = right; } }

        //first each 64 but chunk of the message must be permuted according to IP
        temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
        temp = ((left >>> 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
        temp = ((right >>> 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
        temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

        left = ((left << 1) | (left >>> 31));
        right = ((right << 1) | (right >>> 31));

        //do this either 1 or 3 times for each chunk of the message
        for (j = 0; j < iterations; j += 3) {
            endloop = looping[j + 1];
            loopinc = looping[j + 2];
            //now go through and perform the encryption or decryption 
            for (i = looping[j]; i != endloop; i += loopinc) {//for efficiency
                right1 = right ^ keys[i];
                right2 = ((right >>> 4) | (right << 28)) ^ keys[i + 1];
                //the result is attained by passing these bytes through the S selection functions
                temp = left;
                left = right;
                right = temp ^ (spfunction2[(right1 >>> 24) & 0x3f] | spfunction4[(right1 >>> 16) & 0x3f] | spfunction6[(right1 >>> 8) & 0x3f] | spfunction8[right1 & 0x3f] | spfunction1[(right2 >>> 24) & 0x3f] | spfunction3[(right2 >>> 16) & 0x3f] | spfunction5[(right2 >>> 8) & 0x3f] | spfunction7[right2 & 0x3f]);
            }
            temp = left; left = right; right = temp; //unreverse left and right
        } //for either 1 or 3 iterations

        //move then each one bit to the right
        left = ((left >>> 1) | (left << 31));
        right = ((right >>> 1) | (right << 31));

        //now perform IP-1,which is IP in the opposite direction
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
        temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
        temp = ((right >>> 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
        temp = ((left >>> 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
        temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);

        //for Cipher Block Chaining mode,xor the message with the previous result
        if (mode == 1) { if (encrypt) { cbcleft = left; cbcright = right; } else { left ^= cbcleft2; right ^= cbcright2; } }
        if (encrypt) {
            tempresult += String.fromCharCode((left >>> 24), ((left >>> 16) & 0xff), ((left >>> 8) & 0xff), (left & 0xff), (right >>> 24), ((right >>> 16) & 0xff), ((right >>> 8) & 0xff), (right & 0xff));
        }
        else {
            tempresult += String.fromCharCode(((left >>> 16) & 0xffff), (left & 0xffff), ((right >>> 16) & 0xffff), (right & 0xffff));
        } //解密时输出双字节
        encrypt ? chunk += 16 : chunk += 8;
        if (chunk == 512) { result += tempresult; tempresult = ''; chunk = 0; }
    } //for every 8 characters,or 64 bits in the message

    //return the result as an array
    return result + tempresult;
} //end of des

//des_createKeys
//this takes as input a 64 bit beinetkey(even though only 56 bits are used)
//as an array of 2 integers,and returns 16 48 bit keys
function des_createKeys(beinetkey) {
    //declaring this locally speeds things up a bit
    pc2bytes0 = new Array(0, 0x4, 0x20000000, 0x20000004, 0x10000, 0x10004, 0x20010000, 0x20010004, 0x200, 0x204, 0x20000200, 0x20000204, 0x10200, 0x10204, 0x20010200, 0x20010204);
    pc2bytes1 = new Array(0, 0x1, 0x100000, 0x100001, 0x4000000, 0x4000001, 0x4100000, 0x4100001, 0x100, 0x101, 0x100100, 0x100101, 0x4000100, 0x4000101, 0x4100100, 0x4100101);
    pc2bytes2 = new Array(0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808, 0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808);
    pc2bytes3 = new Array(0, 0x200000, 0x8000000, 0x8200000, 0x2000, 0x202000, 0x8002000, 0x8202000, 0x20000, 0x220000, 0x8020000, 0x8220000, 0x22000, 0x222000, 0x8022000, 0x8222000);
    pc2bytes4 = new Array(0, 0x40000, 0x10, 0x40010, 0, 0x40000, 0x10, 0x40010, 0x1000, 0x41000, 0x1010, 0x41010, 0x1000, 0x41000, 0x1010, 0x41010);
    pc2bytes5 = new Array(0, 0x400, 0x20, 0x420, 0, 0x400, 0x20, 0x420, 0x2000000, 0x2000400, 0x2000020, 0x2000420, 0x2000000, 0x2000400, 0x2000020, 0x2000420);
    pc2bytes6 = new Array(0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002, 0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002);
    pc2bytes7 = new Array(0, 0x10000, 0x800, 0x10800, 0x20000000, 0x20010000, 0x20000800, 0x20010800, 0x20000, 0x30000, 0x20800, 0x30800, 0x20020000, 0x20030000, 0x20020800, 0x20030800);
    pc2bytes8 = new Array(0, 0x40000, 0, 0x40000, 0x2, 0x40002, 0x2, 0x40002, 0x2000000, 0x2040000, 0x2000000, 0x2040000, 0x2000002, 0x2040002, 0x2000002, 0x2040002);
    pc2bytes9 = new Array(0, 0x10000000, 0x8, 0x10000008, 0, 0x10000000, 0x8, 0x10000008, 0x400, 0x10000400, 0x408, 0x10000408, 0x400, 0x10000400, 0x408, 0x10000408);
    pc2bytes10 = new Array(0, 0x20, 0, 0x20, 0x100000, 0x100020, 0x100000, 0x100020, 0x2000, 0x2020, 0x2000, 0x2020, 0x102000, 0x102020, 0x102000, 0x102020);
    pc2bytes11 = new Array(0, 0x1000000, 0x200, 0x1000200, 0x200000, 0x1200000, 0x200200, 0x1200200, 0x4000000, 0x5000000, 0x4000200, 0x5000200, 0x4200000, 0x5200000, 0x4200200, 0x5200200);
    pc2bytes12 = new Array(0, 0x1000, 0x8000000, 0x8001000, 0x80000, 0x81000, 0x8080000, 0x8081000, 0x10, 0x1010, 0x8000010, 0x8001010, 0x80010, 0x81010, 0x8080010, 0x8081010);
    pc2bytes13 = new Array(0, 0x4, 0x100, 0x104, 0, 0x4, 0x100, 0x104, 0x1, 0x5, 0x101, 0x105, 0x1, 0x5, 0x101, 0x105);

    //how many iterations(1 for des,3 for triple des)
    var iterations = beinetkey.length >= 24 ? 3 : 1;
    //stores the return keys
    var keys = new Array(32 * iterations);
    //now define the left shifts which need to be done
    var shifts = new Array(0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0);
    //other variables
    var lefttemp, righttemp, m = 0, n = 0, temp;

    for (var j = 0; j < iterations; j++) {//either 1 or 3 iterations
        left = (beinetkey.charCodeAt(m++) << 24) | (beinetkey.charCodeAt(m++) << 16) | (beinetkey.charCodeAt(m++) << 8) | beinetkey.charCodeAt(m++);
        right = (beinetkey.charCodeAt(m++) << 24) | (beinetkey.charCodeAt(m++) << 16) | (beinetkey.charCodeAt(m++) << 8) | beinetkey.charCodeAt(m++);

        temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
        temp = ((right >>> -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
        temp = ((left >>> 2) ^ right) & 0x33333333; right ^= temp; left ^= (temp << 2);
        temp = ((right >>> -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
        temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

        //the right side needs to be shifted and to get the last four bits of the left side
        temp = (left << 8) | ((right >>> 20) & 0x000000f0);
        //left needs to be put upside down
        left = (right << 24) | ((right << 8) & 0xff0000) | ((right >>> 8) & 0xff00) | ((right >>> 24) & 0xf0);
        right = temp;

        //now go through and perform these shifts on the left and right keys
        for (i = 0; i < shifts.length; i++) {
            //shift the keys either one or two bits to the left
            if (shifts[i]) { left = (left << 2) | (left >>> 26); right = (right << 2) | (right >>> 26); }
            else { left = (left << 1) | (left >>> 27); right = (right << 1) | (right >>> 27); }
            left &= -0xf; right &= -0xf;

            //now apply PC-2,in such a way that E is easier when encrypting or decrypting
            //this conversion will look like PC-2 except only the last 6 bits of each byte are used
            //rather than 48 consecutive bits and the order of lines will be according to 
            //how the S selection functions will be applied:S2,S4,S6,S8,S1,S3,S5,S7
            lefttemp = pc2bytes0[left >>> 28] | pc2bytes1[(left >>> 24) & 0xf]
| pc2bytes2[(left >>> 20) & 0xf] | pc2bytes3[(left >>> 16) & 0xf]
| pc2bytes4[(left >>> 12) & 0xf] | pc2bytes5[(left >>> 8) & 0xf]
| pc2bytes6[(left >>> 4) & 0xf];
            righttemp = pc2bytes7[right >>> 28] | pc2bytes8[(right >>> 24) & 0xf]
| pc2bytes9[(right >>> 20) & 0xf] | pc2bytes10[(right >>> 16) & 0xf]
| pc2bytes11[(right >>> 12) & 0xf] | pc2bytes12[(right >>> 8) & 0xf]
| pc2bytes13[(right >>> 4) & 0xf];
            temp = ((righttemp >>> 16) ^ lefttemp) & 0x0000ffff;
            keys[n++] = lefttemp ^ temp; keys[n++] = righttemp ^ (temp << 16);
        }
    } //for each iterations
    //return the keys we've created
    return keys;
} //end of des_createKeys


///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////

// 把字符串转换为16进制字符串
// 如：a变成61（即10进制的97）；abc变成616263
function stringToHex(s) {
    var r = '';
    var hexes = new Array('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f');
    for (var i = 0; i < (s.length) ; i++) { r += hexes[s.charCodeAt(i) >> 4] + hexes[s.charCodeAt(i) & 0xf]; }
    return r;
}
// 16进制字符串转换为字符串
// 如：61（即10进制的97）变成a；616263变成abc
function HexTostring(s) {
    var r = '';
    for (var i = 0; i < s.length; i += 2) {
        var sxx = parseInt(s.substring(i, i + 2), 16);
        r += String.fromCharCode(sxx);
    }
    return r;
}

/// 加密测试函数
/// s     待加密的字符串
/// k     密钥
function encMe(s, k) {
    if (k == null) {
        k = "wangli83";
    }
    return stringToHex(des(k, s, 1, 0));
}

/// 解密测试函数
/// s     待解密的字符串
/// k     密钥
function uncMe(s, k) {

    if (k == null) {
        k = "wangli83";
    }
    return des(k, HexTostring(s), 0, 0);
}


var EncodeURI = function (unzipStr, isCusEncode) {
    if (isCusEncode) {
        var zipArray = new Array();
        var zipstr = "";
        var lens = new Array();
        for (var i = 0; i < unzipStr.length; i++) {
            var ac = unzipStr.charCodeAt(i);
            zipstr += ac;
            lens = lens.concat(ac.toString().length);
        }
        zipArray = zipArray.concat(zipstr);
        zipArray = zipArray.concat(lens.join("O"));
        return zipArray.join("N");
    } else {
        //return encodeURI(unzipStr);
        var zipstr = "";
        var strSpecial = "!\"#$%&'()*+,/:;<=>?[]^`{|}~%";
        var tt = "";

        for (var i = 0; i < unzipStr.length; i++) {
            var chr = unzipStr.charAt(i);
            var c = StringToAscii(chr);
            tt += chr + ":" + c + "n";
            if (parseInt("0x" + c) > 0x7f) {
                zipstr += encodeURI(unzipStr.substr(i, 1));
            } else {
                if (chr == " ")
                    zipstr += "+";
                else if (strSpecial.indexOf(chr) != -1)
                    zipstr += "%" + c.toString(16);
                else
                    zipstr += chr;
            }
        }
        return zipstr;
    }
}

var DecodeURI = function (zipStr, isCusEncode) {
    if (isCusEncode) {
        var zipArray = zipStr.split("N");
        var zipSrcStr = zipArray[0];
        var zipLens;
        if (zipArray[1]) {
            zipLens = zipArray[1].split("O");
        } else {
            zipLens.length = 0;
        }

        var uzipStr = "";

        for (var j = 0; j < zipLens.length; j++) {
            var charLen = parseInt(zipLens[j]);
            uzipStr += String.fromCharCode(zipSrcStr.substr(0, charLen));
            zipSrcStr = zipSrcStr.slice(charLen, zipSrcStr.length);
        }
        return uzipStr;
    } else {
        //return decodeURI(zipStr);
        var uzipStr = "";

        for (var i = 0; i < zipStr.length; i++) {
            var chr = zipStr.charAt(i);
            if (chr == "+") {
                uzipStr += " ";
            } else if (chr == "%") {
                var asc = zipStr.substring(i + 1, i + 3);
                if (parseInt("0x" + asc) > 0x7f) {
                    uzipStr += decodeURI("%" + asc.toString() + zipStr.substring(i + 3, i + 9).toString());;
                    i += 8;
                } else {
                    uzipStr += AsciiToString(parseInt("0x" + asc));
                    i += 2;
                }
            } else {
                uzipStr += chr;
            }
        }
        return uzipStr;
    }
}

var StringToAscii = function (str) {
    return str.charCodeAt(0).toString(16);
}

var AsciiToString = function (asccode) {
    return String.fromCharCode(asccode);
}

//#endregion 



var dragging = false;
var iX, iY;
//新弹窗
function PopWindow(op, width, height) {

    var styleArray = [];

    var i = parseInt(Math.random() * 1000);
    if (op.shadow) {

        $("body").append("<div id='div_shadow' i='" + i + "' class='shadow' ></div>");
        $("#div_shadow").width($(document).width());
        $("#div_shadow").height($(document).height());
    }
    if (op.close == null) {
        op.close = true;
    }


    if (op.top != null) {

    }

    var popId = "div_pop1" + i;
    var w = [];



    w.push("<div  id='" + popId + "' i='" + i + "' class='div_pop1' shadow='" + op.shadow + "'  style='width:" + width + "px;   margin:-" + ConvertToInt(height / 2) + "px 0 0  -" + ConvertToInt(width / 2) + "px;' >");
    w.push("<dl class='dl_pop1' >");
    w.push("<dt class='dt_pop1' style='width:" + width + "'>");
    w.push("<span class='sp_pop_hd'>");
    w.push(op.title);
    w.push("</span>");
    if (op.close == true) {
        w.push("<a class='a_guanbi' onclick='ClearPopWindow(this)' i='" + i + "' >关闭</a>");
    }


    w.push("</dt>");
    w.push("<dd style='width:" + width + "px; height:" + height + "px '>");
    w.push("<div class='clr'></div>");
    w.push("<div class='div_pop_content'>");
    w.push(op.html);
    w.push("</div>");
    w.push("</dd>");
    w.push("</dl>");
    w.push("</div>");
    $("body").append(w.join(""));


    var oDiv = document.getElementById(popId);

    var top = $(window).height() / 2 - ConvertToInt(height / 2);
    var left = $(oDiv).position().left - ConvertToInt(width / 2);

    $(oDiv).css({ "margin": "0 0 0 0", "top": top + "px", "left": left + "px" });


    $(oDiv).find(".dt_pop1")[0].onmousedown = function (ev) {
        var oEvent = ev || event;
        var x = 0;
        var y = 0;
        x = oEvent.clientX - oDiv.offsetLeft;
        y = oEvent.clientY - oDiv.offsetTop;
        document.onmousemove = function (ev) {
            var oEvent = ev || event;
            var out1 = oEvent.clientX - x;
            var out2 = oEvent.clientY - y;



            var oWidth = document.documentElement.clientWidth;
            var oHeight = document.documentElement.clientHeight;

            if (op.InWin) {

                oWidth = document.documentElement.clientWidth - oDiv.offsetWidth;
                oHeight = document.documentElement.clientHeight - oDiv.offsetHeight;
            }

            if (out1 < 0)
            { out1 = 0; }
            else if (out1 > oWidth) {
                out1 = oWidth;
            }

            if (out2 < 0)
            { out2 = 0; }
            else if (out2 > oHeight) {
                out2 = oHeight;
            }

            oDiv.style.left = out1 + 'px';
            oDiv.style.top = out2 + 'px';
        }
        document.onmouseup = function () {
            document.onmousemove = null;
            document.onmouseup = null;
        }
        return false;//解决firefox低版本的bug问题
    }



}
function UpIndex(o) {
    if (!o) {
        o = {};
    }
    $(".div_pop1").css('z-index', "1000");
    $(o).css('z-index', "1001");
    if (!o.fun) {
        o.fun = function () {

        }
    }
    o.fun();

}

function ClearPopWindow(o) {


    if (o) {
        $(o).parents(".div_pop1").remove();

    }
    else {
        $(".div_pop1,.shadow").remove();
    }
    try {
        var i = $(o).attr("i");
        $("#div_shadow[i='" + i + "']").remove();
    } catch (e) {


    }


    try {
        if (!o.fun) {
            o.fun = function () {

            }
        }
        o.fun();
    } catch (e) {

    }


}


function QuickPopWindow(o, w, h) {

    o.html = "<div class='div_QuickPopWindow  div_" + o.type + "' ><p>" + o.html + "</p></div>";



    if (!o.btns) {

        if (o.href) {

            o.html += "<div class='div_pop_btn' ><a class='a_btn1' href='" + o.href + "' > 确 定 </a></div>";
        }
        else {

            o.html += "<div class='div_pop_btn' ><a class='a_btn1' onclick='ClearPopWindow()' > 确 定 </a></div>";
        }


    }
    if (!w) {
        w = 400;
    }
    if (!h) {

        h = 120;
    }

    PopWindow({
        title: o.title,
        html: o.html

    }, w, h)


}


//从localStorage中赋值到cookie中
function LoginCookie() {

    $.cookie("CurrentMerRoleId", localStorage.MerRoleId);
    $.cookie("CurrentMerName", localStorage.MerName);
    $.cookie("CurrentMerId", localStorage.MerId);
    $.cookie("CurrentMerRoleName", localStorage.MerRoleName);
    $.cookie("CurrentUserId", localStorage.UserId);
    $.cookie("CurrentBranchId", localStorage.BranchId);
}

function GetAddressByAreaId(obj, AreaId)
{



    var all = false;
    var idStr = ConvertToString($(obj).attr("idStr"));


    AjaxPost("/ac/", "GetAddressByAreaId",
                                         {
                                             AreaId: AreaId

                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {
                                                 var w = [];
                                                 w.push("<select id=\"sel_province" + idStr + "\"  idStr='" + idStr + "'  onchange='GetCity(this)' >");
                                                 if (all) {
                                                     w.push("<option value=''  >");
                                                     w.push("全部");
                                                     w.push("</option>");
                                                 }
                                                 for (var i = 0; i < data.Province.length; i++) {
                                                     var j = data.Province[i];

                                                     w.push("<option value='" + j.ProvinceId + "'>");
                                                     w.push(j.ProvinceName);
                                                     w.push("</option>");

                                                 }
                                                 w.push("</select>");

                                                 w.push("<select id=\"sel_city" + idStr + "\"  idStr='" + idStr + "'  onchange='GetCity(this)' >");
                                                 if (all) {
                                                     w.push("<option value=''  >");
                                                     w.push("全部");
                                                     w.push("</option>");
                                                 }
                                                 for (var i = 0; i < data.City.length; i++) {
                                                     var j = data.City[i];

                                                     w.push("<option value='" + j.CityId + "'>");
                                                     w.push(j.CityName);
                                                     w.push("</option>");

                                                 }
                                                 w.push("</select>");

                                                 w.push("<select id=\"sel_area" + idStr + "\" idStr='" + idStr + "'  >");
                                                 if (all) {
                                                     w.push("<option value=''  >");
                                                     w.push("全部");
                                                     w.push("</option>");
                                                 }
                                                 for (var i = 0; i < data.Area.length; i++) {
                                                     var j = data.Area[i];

                                                     w.push("<option value='" + j.AreaId + "'>");
                                                     w.push(j.AreaName);
                                                     w.push("</option>");

                                                 }
                                                 w.push("</select>");



                                               



                                                 $(obj).html(w.join(""));

                                                 $("#sel_area" + idStr + "").val(data.Current.AreaId);
                                                 $("#sel_city" + idStr + "").val(data.Current.CityId);
                                                 $("#sel_province" + idStr + "").val(data.Current.ProvinceId);


                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);


}

//省市县公共方法
function GetProvince(obj, all) {


    var idStr = ConvertToString($(obj).attr("idStr"));

    AjaxPost("/ac/", "GetProvince",
                                   {
                                       CurrentPage: 1
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           var w = [];
                                           w.push("<select id=\"sel_province" + idStr + "\"  idStr='" + idStr + "'  onchange='GetCity(this)' >");
                                           if (all) {
                                               w.push("<option value=''  >");
                                               w.push("选择省份");
                                               w.push("</option>");
                                           }
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];

                                               w.push("<option value='" + j.ProvinceId + "'>");
                                               w.push(j.ProvinceName);
                                               w.push("</option>");

                                           }
                                           w.push("</select>");
                                           $(obj).html(w.join(""));
                                           $("#sel_province").change();
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);
}

function GetCity(obj, all) {
    var ProvinceId = $(obj).val();

    var idStr = $(obj).attr("idStr");
    $("#sel_city" + idStr + "").remove(); $("#sel_area" + idStr + "").remove();
    if (ProvinceId == "") {
        return;
    }
    AjaxPost("/ac/", "GetCity",
                                   {
                                       ProvinceId: ProvinceId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           var w = [];
                                           w.push("<select id=\"sel_city" + idStr + "\"  idStr='" + idStr + "' onchange=\"GetArea(this)\" >")
                                           if (all) {
                                               w.push("<option value=''  >");
                                               w.push("选择城市");
                                               w.push("</option>");
                                           }
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];

                                               w.push("<option value='" + j.CityId + "'>");
                                               w.push(j.CityName);
                                               w.push("</option>");

                                           }
                                           w.push("</select>");
                                           $(obj).after(w.join(""));
                                           $("#sel_city").change();
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);


}


function GetArea(obj, all) {

    var CityId = $(obj).val();
    var idStr = $(obj).attr("idStr");
    $("#sel_area" + idStr + "").remove();
    if (CityId == "") {
        return;
    }
    AjaxPost("/ac/", "GetArea",
                                   {
                                       CityId: CityId
                                   }, function (data) {
                                       var w = [];
                                       if (data.re == "ok") {
                                           var w = [];
                                           w.push("<select id=\"sel_area" + idStr + "\" idStr='" + idStr + "'  >");
                                           if (all) {
                                               w.push("<option value=''  >");
                                               w.push("选择区县");
                                               w.push("</option>");
                                           }
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];

                                               w.push("<option value='" + j.AreaId + "'>");
                                               w.push(j.AreaName);
                                               w.push("</option>");

                                           }
                                           w.push("</select>");
                                           $(obj).after(w.join(""));
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);

}
//语音
function ReadStr(str) {
    if (ConvertToString(str) == "") {
        return;
    }

    AjaxPost("/ac/", "GetSoundReadUrl",
                                             {
                                                 ReadStr: str

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {


                                                     var a = $('#sd_sound');

                                                     a.attr("src", data.ReadUrl);
                                                     a[0].play();

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }
                                             }, true);
}

//现在拨打电话
function PhoneNow(Phone) {
    window.location.href = 'tel:' + Phone;
}

var MobileUA = (function () {
    var ua = navigator.userAgent.toLowerCase();

    var mua = {
        IOS: /ipod|iphone|ipad/.test(ua), //iOS  
        IPHONE: /iphone/.test(ua), //iPhone  
        IPAD: /ipad/.test(ua), //iPad  
        ANDROID: /android/.test(ua), //Android Device  
        WINDOWS: /windows/.test(ua), //Windows Device  
        TOUCH_DEVICE: ('ontouchstart' in window) || /touch/.test(ua), //Touch Device  
        MOBILE: /mobile/.test(ua), //Mobile Device (iPad)  
        ANDROID_TABLET: false, //Android Tablet  
        WINDOWS_TABLET: false, //Windows Tablet  
        TABLET: false, //Tablet (iPad, Android, Windows)  
        SMART_PHONE: false //Smart Phone (iPhone, Android)  
    };

    mua.ANDROID_TABLET = mua.ANDROID && !mua.MOBILE;
    mua.WINDOWS_TABLET = mua.WINDOWS && /tablet/.test(ua);
    mua.TABLET = mua.IPAD || mua.ANDROID_TABLET || mua.WINDOWS_TABLET;
    mua.SMART_PHONE = mua.MOBILE && !mua.TABLET; //是手机

    return mua;
}());

