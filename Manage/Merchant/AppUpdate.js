/// <reference path="C:\anli\快工场\Manage\Script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Manage\Script/ZYUiPub.js" />


$(function () {



})

function AppUp(n)
{

    switch (n) {
        case 1:




            break;
        default:

    }
}


function DownLoadApp()
{
    AjaxPost("/ac/", "DownLoadApp",
                                             {
                                                 AppUrl: $("#txt_Url").val()

                                             }, function (data) {
                                                 var w = [];
                                                 if (data.re == "ok") {
                                                     $("#sp_Url").html(data.AppUrl);

                                                 }
                                                 else {
                                                     alert(data.re)
                                                 }

                                  
                                             }, false);
    

}