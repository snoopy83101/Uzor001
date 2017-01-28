

/// <reference path="/Script/jquery-1.8.2.js" />
/// <reference path="/Script/ZYUiPub.js" />
/// <reference path="/script/common.js" />
if (MobileUA.SMART_PHONE) {

    tiaozhuan("/Wap/UzorService");
}
$(function () {

    BindPageSetting();

})

function BindPageSetting() {
    $("#a_dh_UzorService").addClass("select");

}




