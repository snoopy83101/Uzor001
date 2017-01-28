
function DelImg(j) {
    if (j.ImgId) {

        if (j.ImgId != "") {
            AjaxPost("/ac/", "DelImageById",
                                 {
                                     ImgId: j.ImgId
                                 }, function (data) {
                                     var w = [];
                                     if (data.re == "ok") {
                                         return;
                                     }
                                     else {
                                         alert(data.re)
                                     }


                                 }, false);

        }
    }

    if (j.ImgUrl) {

        if (j.ImgUrl != "") {
            AjaxPost("/ac/", "ImgUrl",
                                         {
                                             ImgUrl: j.ImgUrl
                                         }, function (data) {
                                             var w = [];
                                             if (data.re == "ok") {

                                             }
                                             else {
                                                 alert(data.re)
                                             }


                                         }, false);

        }

    }





}