

/// <reference path="/script/jquery-1.8.2.js" />
/// <reference path="/script/common.js" />
var MenuIndex = 0;


$(function () {



})


function ShowMenu(o)
{


    var b = $("#div_menu").is(":hidden");

    if (b) {

        $("#div_menu").find("a").removeClass("select");
        $("#div_menu").find("a").eq(MenuIndex).addClass("select");




        $("#div_menu").show(100);
    }
    else {






        $("#div_menu").hide(100);
    }



 


}





function BindPageSetting() {


}




