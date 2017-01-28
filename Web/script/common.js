/// <reference path="jquery-1.8.2.js" />
var FlagTiaoShi = false;
var ver = "1.0.6";
var local = "../../";  //当前页面到根目录的位置


//var CDomain = "https://uzor001.com"; var ajaxCDomain = "https://uzor001.com"; FlagTiaoShi = false;
var CDomain = "http://192.168.1.132:99"; var ajaxCDomain = "http://192.168.1.132:99"; FlagTiaoShi = false;
//var CDomain = "http://snoopy83101.oicp.net:99"; var ajaxCDomain = "http://snoopy83101.oicp.net:99"; FlagTiaoShi = true;

localStorage.Domain = CDomain;
var CMerId = 1999;
localStorage.MerId = CMerId;
var CurrentMember = GetCurrentMember();

var NowDate = new Date();


function ConvertToJson(str) {

    if (!str) {
        return {};
    }

    var typeStr = Object.prototype.toString.apply(str); a

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


function Get100Num(Number) {

    var JiFen = ConvertToInt(Number);

    var jifenStr = JiFen.toString();
    var jifenStrArray = jifenStr.split('');
    jifenStrArray[jifenStrArray.length - 1] = '0';
    jifenStrArray[jifenStrArray.length - 2] = '0';
    JiFen = ConvertToInt(jifenStrArray.join(""));


    return JiFen;
}


function setCustomRefreshHeaderInfo(fun) {

    api.setCustomRefreshHeaderInfo({
        bgColor: '#f2f8ff',
        image: {
            pull: 'widget://image/icon/refreshing_image_frame_01.png',
            transform: [
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png',
                      'widget://image/icon/refreshing_image_frame_24.png'

            ],
            load: [
                'widget://image/icon/refreshing_image_frame_01.png',
                'widget://image/icon/refreshing_image_frame_02.png',
                'widget://image/icon/refreshing_image_frame_03.png',
                'widget://image/icon/refreshing_image_frame_04.png',
                'widget://image/icon/refreshing_image_frame_05.png',
                'widget://image/icon/refreshing_image_frame_06.png',
                'widget://image/icon/refreshing_image_frame_07.png',
                'widget://image/icon/refreshing_image_frame_08.png',
                'widget://image/icon/refreshing_image_frame_09.png',
                'widget://image/icon/refreshing_image_frame_10.png',
                'widget://image/icon/refreshing_image_frame_11.png',
                'widget://image/icon/refreshing_image_frame_12.png',
                'widget://image/icon/refreshing_image_frame_13.png',
                'widget://image/icon/refreshing_image_frame_14.png',
                'widget://image/icon/refreshing_image_frame_15.png',
                'widget://image/icon/refreshing_image_frame_16.png',
                'widget://image/icon/refreshing_image_frame_17.png',
                'widget://image/icon/refreshing_image_frame_18.png',
                'widget://image/icon/refreshing_image_frame_19.png',
                'widget://image/icon/refreshing_image_frame_20.png',
                'widget://image/icon/refreshing_image_frame_21.png',
                'widget://image/icon/refreshing_image_frame_22.png',
                'widget://image/icon/refreshing_image_frame_23.png',
                'widget://image/icon/refreshing_image_frame_24.png'

            ]
        }

    }, function () {
        //下拉刷新被触发，自动进入加载状态，使用 api.refreshHeaderLoadDone() 手动结束加载中状态
        //下拉刷新被触发，使用 api.refreshHeaderLoadDone() 结束加载中状态  
        //get alert('开始加载刷新数据，摇一摇停止加载状态');
        setTimeout(function () {
            try {
                fun();
            } catch (e) {

            }
        }, 1000)


        setTimeout(function () {

            api.refreshHeaderLoadDone();
        }, 4000);
    });

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
//如果是调试状态, 则弹窗
function TestAlert(o) {



}


//如果末尾存在斜杠,则删除,为了协调路径
function RemoveLastSlash(src) {


    var d = "";


    var last = src.substr(src.length - 1, 1);

    if (last == "/") {
        src = src.substring(0, src.length - 1);
        return src;
    }
    else {

        return src;
    }
}

function ToLogin() {

    // localStorage["LoginBackWinName"] = api.winName;

    openW({
        url: "",
        name: ""

    })

}

//#region 用ApiCloud的Prefs方式本地存储临时变量

function setJsonPrefs(key, JsonValue) {
    api.setPrefs({
        key: key,
        value: JSON.stringify(JsonValue)

    });
}

function getJsonPrefs(key, callFunction) {
    try {
        api.getPrefs({
            key: key
        }, function (ret, err) {

            callFunction(ret.value, err);


        });
    } catch (e) {
        alert("getJsonPrefs失败:" + e);
        return null;
    }
}
//#endregion



//用户必须登录, 返回ture或者false,当false时是没有登录
function MustLogin(op) {
    try {

        if (op.local == null) {
            op.local = "../../"
        }

        if (op == null) {
            op = {};
        }

        if (op.cfun == null) {

            op.cfun = function () {



            }
        }

        if (GetCurrentMember().MemberId == 0) {

            api.actionSheet({
                title: '此操作要求用户登录',
                cancelTitle: '取  消',
                //     destructiveTitle: '红色警告按钮',
                buttons: ['已有账户,现在登录', '没有账户,现在注册']
            }, function (ret, err) {


                switch (ret.buttonIndex) {
                    case 1:
                        openW({
                            name: "login",
                            url: "" + op.local + "html/Login/login.html"

                        });
                        break;
                    case 2:
                        openW({
                            name: "zhuce",
                            url: "" + op.local + "html/Login/zhuce.html"

                        });
                        break;
                    case 3:
                        op.cfun();
                        break;

                    default:

                }
            });
            return false;
        }
        else {
            return true;
        }
    } catch (e) {

    }


}



//#region 用LocalStorage方式本地存储临时变量

function setJsonLocalStorage(key, JsonValue) {

    if (JsonValue == null) {

        localStorage[key] = null;
    }
    else {

        localStorage[key] = JSON.stringify(JsonValue);
    }



}

//删除LocalStroage为null
function RemoveJsonLocalStroage(key) {
    localStorage[key] = null;

}


//可以return返回值
function getJsonLocalStroage(key) {

    try {

        return JSON.parse(localStorage[key]);
    } catch (e) {
        return null;
    }


}
//#endregion

//#region 用户相关

function GetCurrentMember(j) {

    try {
        if (j == null) {
            j = {};
        }

        var cmj = { MemberId: 0, PicImgUrl: "/upload/MemberPic.png" };

        var str = localStorage["CurrentMember"];
        if (!j.fun) {

            j.fun = function () { }
        }

        if (str == null || str == "") {
            j.fun(cmj);
            return cmj
        }


        var strJson;
        try {
            strJson = JSON.parse(str);

        } catch (e) {
            return cmj;
        }

        if (str == "" || str == null || strJson.MemberId == 0 || strJson.MemberId == "" || strJson.MemberId == null) {

            localStorage["CurrentMember"] = "";
            j.fun(cmj);
            console.log("执行了登录方法");
            return cmj;


        }
        else {



            cmj = JSON.parse(str);



            if (j.re) {//重新登录获取
                //  alert("这里要提交了" + strJson.MemberId);


                AjaxPost("/amb/", "GetMyMemberInfo",
                                               {
                                                   MemberId: strJson.MemberId

                                               }, function (data) {

                                                   var w = [];
                                                   if (data.re == "ok") {
                                                       // alert("获取到了用户");
                                                       cmj = data.MemberInfo;
                                                       localStorage["CurrentMember"] = JSON.stringify(cmj);
                                                       try {

                                                           j.fun(cmj);
                                                       } catch (e) {

                                                       }

                                                   }
                                                   else {

                                                   }
                                               }, false, strJson);
            }
            else {

                j.fun(cmj)
                return cmj;
            }

        }
        return cmj;

    } catch (e) {
        alert("读取当前用户时出错:" + e + "");
    }

}

function myfunction() {









    $(function () {

        BindPageSetting();

    })

    function BindPageSetting() {


    }



}

function ShuaXinCurrentMember(j) {
    try {
        CurrentMember = GetCurrentMember(j);
    } catch (e) {

    }

}



function CurrentLoginOut() {
    localStorage["CurrentMember"] = JSON.stringify({ MemberId: 0 });
    // alert(GetCurrentMember().MemberId);
    IndexRePageSetting();
}

//用户变动时,需要执行的方法,重新连接融云等
function IndexRePageSetting() {

    //try {
    //    var rong = api.require('rongCloud');
    //    rong.disconnect(false); // 断开，且不再接收 Push
    //} catch (e) {

    //}

    api.execScript({
        name: 'index',
        script: 'RePageSetting();'
    });


}



//#endregion

function AjaxPost(url, para, data, callback, async, cmJson) {

    try {
        if (cmJson == null) {
            cmJson = GetCurrentMember();
        }
        for (var i in data) {
            if (isString(data[i])) {
                data[i] = encodeURI(data[i]); //全部转码吧,少年!
            }
        }

        data.para = encodeURI(para);
        data.s = "app";

        if (cmJson.MemberId != null) {
            //alert(cmJson.MemberId);
            data.GetMemberId = cmJson.MemberId;
        }
        var ri = 0;
        var ajaxSetting = {

            url: '' + ajaxCDomain + '' + url + '',
            method: 'post',
            cache: false,
            // timeout: 7,
            dataType: 'json',
            returnAll: false,
            data: {
                values: data
                //files: { file: 'fs://a.gif' }
            }
        }

        DoAjax(ajaxSetting, callback);


    } catch (e) {
        alert(e);
    }


}

function AjaxPostCache(url, para, data, callback, async, cmJson) {

    try {
        if (cmJson == null) {
            cmJson = GetCurrentMember();
        }
        for (var i in data) {
            if (isString(data[i])) {
                data[i] = encodeURI(data[i]); //全部转码吧,少年!
            }
        }

        data.para = encodeURI(para);
        data.s = "app";

        if (cmJson.MemberId != null) {
            //alert(cmJson.MemberId);
            data.GetMemberId = cmJson.MemberId;
        }
        var ri = 0;
        var ajaxSetting = {

            url: '' + ajaxCDomain + '' + url + '',
            method: 'post',
            cache: true,
            // timeout: 7,
            dataType: 'json',
            returnAll: false,
            data: {
                values: data
                //files: { file: 'fs://a.gif' }
            }
            //certificate: {
            //    path: 'widget://script/213964289770200.pfx',                //p12证书路径，支持fs://、widget://、cache://等文件路径协议，字符串类型
            //    password: '213964289770200'            //证书密码，字符串类型
            //}
        }

        DoAjax(ajaxSetting, callback);


    } catch (e) {
        alert(e);
    }


}

function DoAjax(ajaxSetting, callback, ri) {



    if (ri == null) {
        ri = 0;
    }
    api.ajax(ajaxSetting, function (ret, err) {
        if (ret) {
            ret.re = decodeURI(ret.re);
            callback(ret);
        } else {
            //ajax失败
            ri = ri + 1;
            if (ri <= 2) {
                if (!FlagTiaoShi) {//如果不是调试
                    DoAjax(ajaxSetting, callback, ri);
                }


            }
            else {




                callback({

                    // re: 'ajax已经失败重试超过3次,错误码：' + err.code + '；错误信息：' + err.msg + '网络状态码：' + err.statusCode + "提交内容:" + ajaxSetting.data.values.para + ""
                    re: "网络无法链接，请检查你的网络设置"


                })


            }



        };
    });
}


//是否字符串
function isString(_str) {
    if (_str == null) {
        return false;
    }
    return typeof _str == 'string' || _str.constructor == String;
    //return typeof _str == "string" || ( _str && (typeof _str.substr == 'function'));
}




// 主页面切换frame
var changeFrameInRoot = function (index) {
    api.execScript({
        name: 'root',
        scritp: 'changeIndexFrame(' + index + ')'
    });
};
// 主页面切换frame end


// 用于链接到淘宝链接
var toDetail = function (obj) {
    var url = $api.attr(obj, 'data-url');
    var title = $api.attr(obj, 'data-title');

    api.openWin({
        name: 'detailwinweb',
        url: '../detailframes/detailwinweb.html',
        pageParam: {
            title: title,
            url: url
        },
        rect: {
            x: 0,
            y: 0,
            w: 'auto',
            h: 'auto'
        }
    });
};
// 
// 打开分类列表
var openClassify = function () {
    api.openWin({
        name: 'detailclassify',
        url: './html/detailframes/detailclassify.html',
        bounces: false,
        rect: {
            x: 0,
            y: 0,
            w: 'auto',
            h: 'auto'
        }
    });
};
// 
// 语音识别
var openSpeechRec = function () {
    api.openWin({
        name: 'record',
        url: './html/detailframes/record.html',
        pageParam: {
            name: '语音搜索',
            title: '语音搜索'
        },
        rect: {
            x: 0,
            y: 0,
            w: 'auto',
            h: 'auto'
        }
    });
};

//扫码
function SaoMa(o) {

    var FNScanner = api.require('FNScanner');
    FNScanner.openScanner({
        autorotation: true
    }, function (ret, err) {
        if (ret) {
            switch (ret.eventType) {
                case "success":

                    //  alert("1");
                    AjaxPost("/am/", "GetProInfoByProCode",
                                                   {
                                                       ProCode: ret.content,
                                                       ZoneId: localStorage.ZoneId
                                                   }, function (data) {

                                                       if (data.re == "ok") {
                                                           if (data.num > 0) {

                                                               openW({
                                                                   name: "ProInfo",
                                                                   url: "html/Pro/ProInfo.html?ProId=" + data.info.ProId + "&BranchId=" + data.info.BranchId + ""

                                                               })
                                                           }
                                                           else {

                                                               api.toast({
                                                                   msg: '抱歉,没有找到此商品',
                                                                   duration: 2000,
                                                                   location: 'middle'
                                                               });
                                                           }
                                                       }
                                                       else {
                                                           alert(data.re)
                                                       }

                                                   }, false);


                    break;
                default:

            }
        } else {

        }
    });
}



// 通用header的window


var openH = function (op) {






    if (op.title == null) {
        op.title = "";

    }
    if (op.name == null) {
        op.name = op.title;
    }


    if (op.x == null) {
        op.x = 0;
    }
    if (op.y == null) {
        op.y = localStorage["y"];
    }
    if (op.w == null) {
        op.w = api.winWidth;
    }
    if (op.h == null) {
        op.h = api.winHeight;
    }

    api.openFrame(
         {
             name: op.name,
             url: op.url,
             bounces: false,
             reload: true,
             bgColor: '#EBE8E8',
             rect: {
                 x: op.x,
                 y: op.y,
                 w: op.w,
                 h: op.h
             },
         }
     , function (ret, err) {
         var name = ret.name;
         var index = ret.index;
         //    setBar(index);
     });


}

var openF = function (op) {
    //if (FlagTiaoShi) {
    //    api.clearCache();
    //}

    if (op.title == null) {
        op.title = "";

    }
    if (op.delay == null) {
        op.delay = 0;
    }
    if (op.name == null) {
        op.name = op.title;
    }


    if (op.x == null) {
        op.x = 0;
    }
    if (op.y == null) {
        op.y = 0;
    }
    if (op.w == null) {
        op.w = api.winWidth;
    }
    if (op.h == null) {
        op.h = api.winHeight;
    }
    if (op.animation == null) {
        op.animation = "movein";
    }

    if (op.bgColor) {
        op.bgColor = '#EBE8E8';
    }

    api.openFrame(
     {
         name: op.name,
         url: op.url,
         bounces: false,
         reload: true,
         bgColor: op.bgColor,
         rect: {
             x: op.x,
             y: op.y,
             w: op.w,
             h: op.h
         },
     }
 , function (ret, err) {
     var name = ret.name;
     var index = ret.index;
     //    setBar(index);
 });


}


var openW = function (op) {
    //if (FlagTiaoShi) {
    //    api.clearCache();
    //}

    if (op.title == null) {
        op.title = "";

    }
    if (op.delay == null) {
        op.delay = 0;
    }
    if (op.name == null) {
        op.name = op.title;
    }


    if (op.x == null) {
        op.x = 0;
    }
    if (op.y == null) {
        op.y = 0;
    }
    if (op.w == null) {
        op.w = api.winWidth;
    }
    if (op.h == null) {
        op.h = api.winHeight;
    }
    if (op.animation == null) {
        op.animation = "movein";
    }

    api.openWin({
        name: op.name,
        url: op.url,
        pageParam: {
            name: op.name,
            title: op.name
        },
        reload: true,
        allowEdit: true,
        rect: {
            x: op.x,
            y: op.y,
            w: op.w,
            h: op.h
        }
        ,
        delay: op.delay,
        bounces: false,
        vScrollBarEnabled: true,
        animation: op.animation
    });

}


var openCommon = function (name, title) {

    api.openWin({
        name: name,
        url: '../detailframes/' + name + '.html',
        pageParam: {
            name: name,
            title: title
        },
        rect: {
            x: 0,
            y: 0,
            w: 'auto',
            h: 'auto'
        }
    });
}
var openCommon2 = function (name, title) {
    api.openWin({
        name: name,
        url: './' + name + '.html',
        pageParam: {
            name: name,
            title: title
        },
        rect: {
            x: 0,
            y: 0,
            w: 'auto',
            h: 'auto'
        }
    });
}
var openCommon3 = function (name, title) {
    api.openWin({
        name: name,
        url: './html/detailframes/' + name + '.html',
        pageParam: {
            name: name,
            title: title
        },
        rect: {
            x: 0,
            y: 0,
            w: 'auto',
            h: 'auto'
        }
    });
}


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
//#region 强制转换


//强制转换对象为字符串，如果对象为undefined或者NULL
function ConvertToString(obj) {


    var str = String(obj);

    if (str == "undefined" || str == "null") {
        return "";
    }

    return $.trim(str);
}

function ConvertToInt(obj) {
    if (obj == null || obj == undefined || obj == NaN || $.trim(obj) == "") {
        return 0;
    }

    var i = Math.round(obj);
    if (i == NaN) {
        return 0;
    }
    else {
        return i;
    }

}
//转化为时间
function ConvertToDate(o) {



    str = o.toString();
    str = str.replace(/-/g, '/');


    var d = new Date(str);
    return d;
}

function ConvertToBool(str) {

    if (str == null) {
        return false;
    }
    switch (str.toString().toLowerCase()) {
        case "true": case "yes": case "1": case "checked": return true;
        case "false": case "no": case "0": case null: case "": return false;
        default: return Boolean(string);
    }

}
//#region 判断几位小数



function ConverttoDecimal2f(x) {
    try {

        var n = parseFloat(x);
        return n.toFixed(2);

    } catch (e) {
        return 0;
    }
}

function ConvertToDecimal(val, num) {

    var multiplicand = Math.pow(10, num);
    var result = Math.round(val * multiplicand) / multiplicand;
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

//#endregion}


//#endregion

var lding = false;
function showProgress(op) {

    lding = true;
    var o = {
        style: 'default',
        animationType: 'fade',
        title: '努力加载中...',
        text: '先喝杯茶...',
        modal: false
    }

    if (op == null) {

    }
    else {
        if (op.title) {
            o.title = op.title;
        }
        if (op.text) {
            o.text = op.text;
        }

    }




    api.showProgress(o);
}

function hideProgress() {
    lding = false;
    api.hideProgress();
}



//#region 页面参数


function GetQueryString(name, val) {   //获取QueryString参数,如果无则返回""
    if (val == null) {
        val = "";
    }
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return val;

}

function GetQueryString2(name) {   //获取QueryString参数,如果无则返回""
    var reg = new RegExp("(^|/)" + name + "=([^/]*)(/|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return "";
}


//#endregion



//#region 减少触控时间



function NoClickDelay(el) {
    this.element = typeof el == 'object' ? el : document.getElementById(el);
    if (window.Touch) this.element.addEventListener('touchstart', this, false);
}
NoClickDelay.prototype = {
    handleEvent: function (e) {
        switch (e.type) {
            case 'touchstart':
                this.onTouchStart(e);
                break;
            case 'touchmove':
                this.onTouchMove(e);
                break;
            case 'touchend':
                this.onTouchEnd(e);
                break;
        }
    },
    onTouchStart: function (e) {
        e.preventDefault(); this.moved = false;
        this.theTarget = document.elementFromPoint(e.targetTouches[0].clientX, e.targetTouches[0].clientY);
        if (this.theTarget.nodeType == 3) this.theTarget = theTarget.parentNode;
        this.theTarget.className += ' pressed';
        this.element.addEventListener('touchmove', this, false);
        this.element.addEventListener('touchend', this, false);
    },
    onTouchMove: function (e) {
        this.moved = true;
        this.theTarget.className = this.theTarget.className.replace(/ ?pressed/gi, '');
    },
    onTouchEnd: function (e) {
        this.element.removeEventListener('touchmove', this, false);
        this.element.removeEventListener('touchend', this, false);
        if (!this.moved && this.theTarget) {
            this.theTarget.className = this.theTarget.className.replace(/ ?pressed/gi, '');
            var theEvent = document.createEvent('MouseEvents');
            theEvent.initEvent('click', true, true);
            this.theTarget.dispatchEvent(theEvent);
        }
        this.theTarget = undefined;
    }
};




//#endregion


//#region 绑定下拉


//绑定下拉
function BindXiala(objFuc) {

    api.addEventListener({

        name: 'scrolltobottom'

    }, function (ret, err) {


        objFuc();


    });

}
//必须是onkeyup事件调用
function onlyNumber(obj, o) {//只能输入数字
    obj.value = obj.value.replace(/[^\d]/g, "");  //清除“数字”和“.”以外的字符  
    if (o.x) {
        obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是.  
        obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的  
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
        obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');//只能输入两个小数  
    }

    if (o.max) {



        if (ConvertToInt($(obj).val()) > ConvertToInt(o.max)) {

            $(obj).val(o.max);

        }
    }

}

//#region 除法函数


//除法函数，用来得到精确的除法结果 
//说明：javascript的除法结果会有误差，在两个浮点数相除的时候会比较明显。这个函数返回较为精确的除法结果。 
//调用：accDiv(arg1,arg2) 
//返回值：arg1除以arg2的精确结果 
function accDiv(arg1, arg2) {
    if (arg2 == 0) {
        //0做除数无意义
        return 0;
    }

    var t1 = 0, t2 = 0, r1, r2;
    try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
    try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""))
        r2 = Number(arg2.toString().replace(".", ""))
        return (r1 / r2) * pow(10, t2 - t1);
    }
}

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


//加法函数，用来得到精确的加法结果 
//说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。 
//调用：accAdd(arg1,arg2) 
//返回值：arg1加上arg2的精确结果 
function accAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2))
    return (arg1 * m + arg2 * m) / m
}


//减法函数，用来得到精确的减法结果 
//说明：javascript的减法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的减法结果。 
//调用：accSubtr(arg1,arg2) 
//返回值：arg1减去arg2的精确结果 
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


//扩展属性自动输出
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

//弹窗
function PopWindow(j, fun) {

    if (j == null) {
        return;
    }
    var w = [];
    w.push("");

    ZheBi({
        speed: 500
    }, function () {
        w.push("<div class='div_PopWindow' >");
        w.push("<a class='pop_close' tapmode=\"\" onclick='closePop(this)' ><img src='" + local + "image/swtclose.png' /></a>");
        w.push(j.html);
        w.push("</div>");
        $("body").append(w.join(""));
        if (fun != null) {
            fun();
        }
        api.parseTapmode();
    });
}

function closePop(obj) {
    if (obj == null) {
        $(".div_PopWindow").remove();
    }
    else {
        $(obj).parent().remove();

    }
    ZheBiFadeOut();
}

//遮蔽
function ZheBi(j, fun) {
    if (j == null) {
        j = {

        }
    }
    if (j.speed == null) {
        j.speed = 500;
    }

    var w = [];
    w.push("<div class=\"div_zhebi\">");
    //w.Append("<div class=\"div_zhebifooter\">");
    //w.Append(ConvertToString(j.html));
    //w.Append("</div>");
    w.push("</div>");
    $("body").append(w.join(""));

    fun();


}
function ZheBiFadeOut(j, fun) {
    if (j == null) {
        j = {};
    }


    if (j.speed == null) {
        j.speed = 500;
    }


    $(".div_zhebi").fadeOut(j.speed, function () {
        $(".div_zhebi").remove();
        if (fun != null) {
            fun();
        }
    });

}

//转为200像素的图片路径, 自动在前方追加域名, 域名后面不加/, 因此ProductImgUrl前面需要加/
function To200pxImg(ProductImgUrl) {

    if (!ProductImgUrl) {
        return "";
    }

    var ProImgUrl200 = CDomain + ProductImgUrl.replace("/upload/", "/upload200/");
    return ProImgUrl200;
}


function FormatImgUrl(ProductImgUrl) {



    if (ProductImgUrl != null || ProductImgUrl != "") {
        var ProductImgUrl2 = ProductImgUrl.toLowerCase();
        if (ProductImgUrl2.indexOf("http://") >= 0 || ProductImgUrl2.indexOf("https://") >= 0) {
            //外链图片
            //   alert("外链:" + ProductImgUrl);
            return ProductImgUrl;

        }
        else {
            //本站图片
            return CDomain + ProductImgUrl;
        }
    }
    else {
        return "";
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
function ClearDate(myTime) {


    var mydate = new Date(myTime);
    mydate.setHours(0);
    mydate.setMinutes(0);
    mydate.setSeconds(0)
    mydate.setMilliseconds(0);


    return mydate;

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

//字符串转JSON
function Str2Json(str, CatchJson) {
    try {
        return JSON.parse(str);
    } catch (e) {
        if (CatchJson == null) {

            alert(e);
        }
        else {
            return CatchJson;
        }
    }

}
//Json转字符串
function Json2Str(json, CatchStr) {

    try {
        return JSON.stringify(json);
    } catch (e) {
        if (CatchJson == null) {

            alert(e);
        }
        else {
            return CatchStr;
        }
    }
}


//手机号码加密
function PhoneJiaMi(PhoneNo) {
    try {
        var mphone = PhoneNo.substr(3, 4);

        var lphone = PhoneNo.replace(mphone, "****");
        return lphone;
    } catch (e) {
        return "";
    }

}

function SfzNoJiaMi(PhoneNo) {
    try {
        var mphone = PhoneNo.substr(6, 8);

        var lphone = PhoneNo.replace(mphone, "********");
        return lphone;
    } catch (e) {
        return "";
    }

}

function BankCardJiaMi(BankCardCode) {



    try {
        var i = BankCardCode.length;
        console.log(i);
        var mphone = BankCardCode.substr(4, i - 8);

        var lphone = BankCardCode.replace(mphone, "********");
        return lphone;
    } catch (e) {
        return "";
    }

}


//广告to搜索
function AdToSearch(txt) {

    openW({
        name: "ProList",
        url: local + "html/Pro/ProList.html?KeyWord=" + txt + ""
    });
}

//广告to产品
function AdToProInfo(ProId) {
    openW({
        name: "ProInfo",
        url: local + "html/Pro/ProInfo.html?ProId=" + ProId + ""
    });
}
//广告to类别
function AdToProClass(ProClassId) {
    openW({
        name: "ProList",
        url: local + "html/Pro/ProList.html?ProductClassId=" + ProClassId + ""
    });
}

//广告-文章
function AdToArticleInfo(ArticleId) {

    localStorage["CurrentArticleId"] = ArticleId;

    openW({
        name: "ArticleInfo",
        url: local + "html/Article/ArticleInfo.html?ArticleId=" + ArticleId + ""
    });
}

//广告-促销详情
function AdToCuXiaoInfo(CuXiaoId) {

    localStorage["CurrentCuXiaoId"] = CuXiaoId;

    openW({
        name: "CuXiaoInfo",
        url: local + "html/CuXiao/CuXiaoInfo.html?CuXiaoId=" + CuXiaoId + ""
    });
}

//调整IOS下的滚动条, 显示当前文本控件
function BindIosTextTop(obj, op) {

    //调整IOS下的滚动条, 显示当前文本控件
    $(obj).focus(function () {

        setTimeout(function () {
            $("html,body").animate({ scrollTop: $(obj).offset().top - 50 }, 300);

        }, 10);



    });
}


//获取融云的未读消息数
function GetRongUnreadCount(j) {

    if (j == null) {
        j = {};
    }
    if (j.targetId == null) {
        j.targetId = localStorage.Rong_KfUserId;  //如果没有指定targetId, 则默认客服Id
    }

    if (j.fun == null) {
        j.fun = function () {


        }
    }
    var rong = api.require('rongCloud');
    rong.getUnreadCount({
        conversationType: 'CUSTOMER_SERVICE',
        targetId: j.targetId
    }, function (ret, err) {
        api.toast({ msg: ret.result });

        localStorage.Rong_KfWdMsgNum = ret.result;
        if (ret.result > 0) {
            notification();
        }
        api.execScript({     //执行main的未读消息赋值方法
            name: 'main',
            script: 'KfWd();'
        });
        j.fun();
    });
}




//状态栏,文字, 声音
function notification(j) {
    if (j == null) {
        j = {};
    }
    if (j.vibrate == null) {

        j.vibrate = [500, 500];
    }

    if (j.sound == null) {
        j.sound = "widget://image/sound/msg.mp3"
        j.sound = "default";
    }
    // j.light = true;

    if (j.title != null) {
        j.notify = {
            updateCurrent: true,
            content: j.title,
            title: "新消息"
        }

    }
    api.notification(j, function (ret, err) {
        if (ret && ret.id) {
            //   api.alert(ret.id);
        }
    });
}

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

//现在拨打电话
function PhoneNow(Phone) {
    window.location.href = 'tel:' + Phone;
}

function FilePost(filepath, fun) {

    if (!fun) {

        fun = function (ret) {

        }
    }

    api.ajax({
        url: CDomain + "/ac/",
        method: 'post',
        timeout: 30,
        dataType: 'json',
        returnAll: false,
        data: {
            values: { para: encodeURI("FilePost") },
            files: { file: filepath }
        }
    }, function (ret, err) {
        if (ret) {
            // var urlJson = JSON.stringify(ret);
            // api.alert({ msg: urlJson });

            fun(ret);
        } else {
            api.alert({
                msg: ('错误码：' + err.code + '；错误信息：' + err.msg + '网络状态码：' + err.statusCode)
            });
        };
    });

}








//日期时间格式
String.prototype.isTime = function () {

    var r = this.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/);

    if (r == null) return false; var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6], r[7]);

    return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6] && d.getSeconds() == r[7]);

}


//省市县公共方法
function GetProvince(obj, all) {


    var idStr = ConvertToString($(obj).attr("idStr"));
    $("#sel_city" + idStr + "").remove(); $("#sel_area" + idStr + "").remove();
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
                                               w.push("全部");
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
                                               w.push("全部");
                                               w.push("</option>");
                                           }
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];

                                               w.push("<option value='" + j.CityId + "'>");
                                               w.push(j.CityName);
                                               w.push("</option>");

                                           }
                                           w.push("</select>");


                                           if ($("#sel_city" + idStr + "").length > 0) {
                                               $("#sel_city" + idStr + "").remove();
                                           }

                                           $(obj).after(w.join(""));




                                           $("#sel_city" + idStr + "").change();



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
                                               w.push("全部");
                                               w.push("</option>");
                                           }
                                           for (var i = 0; i < data.list.length; i++) {
                                               var j = data.list[i];

                                               w.push("<option value='" + j.AreaId + "'>");
                                               w.push(j.AreaName);
                                               w.push("</option>");

                                           }
                                           w.push("</select>");

                                           if ($("#sel_area" + idStr + "").length > 0) {
                                               $("#sel_area" + idStr + "").remove();
                                           }
                                           $(obj).after(w.join(""));
                                       }
                                       else {
                                           alert(data.re)
                                       }


                                   }, false);

}

function SelImg(fun, o) {


    if (!fun) {
        fun = function () { }
    }
    if (!o) {
        o = {};
    }
    api.actionSheet({
        title: '图片来源',
        cancelTitle: '取消',

        buttons: ['拍照', '图库']
    }, function (ret, err) {


        if (ret.buttonIndex == 3) {
            return;
        }

        console.log(JSON.stringify(ret));
        var index = ConvertToInt(ret.buttonIndex);



        var sourceType = "library";

        if (index == 1) {
            sourceType = "camera";
        }


        api.getPicture({
            sourceType: sourceType,
            encodingType: 'jpg',
            mediaValue: 'pic',
            destinationType: 'url',
            allowEdit: true,
            quality: 50,
            targetWidth: 600,
            saveToPhotoAlbum: false
        }, function (ret, err) {
            if (ret) {



                localStorage.SelImg = ret.data;
                //    alert(JSON.stringify(ret));
                if (o.local) {


                    //如果是本地
                    fun(ret);
                }
                else {

                    if (ConvertToString(ret.data) != "") {



                        FilePost(ret.data, function (data) {


                            fun(data);
                        });

                    }

                }



            } else {
                //  alert(JSON.stringify(err));
            }
        });



    });



}

function CountMsg() {

    var j = GetCurrentMember();
    if (j.MemberId == 0) {
        throw new Exception("MemberId不能为0");
    }

    AjaxPost("/amsg/", "CountMsg",
                                          {
                                              DeviceId: j.MemberId

                                          }, function (data) {
                                              var w = [];
                                              if (data.re == "ok") {
                                                  localStorage.MsgNum = data.Count.MsgNum;
                                                  api.sendEvent({
                                                      name: 'ShowMsgNum',
                                                      extra: {
                                                          "MsgNum": data.Count.MsgNum,
                                                          "ClassCount": data.ClassCount
                                                      }
                                                  });

                                              }
                                              else {
                                                  alert(data.re)
                                              }

                                          }, false);
}