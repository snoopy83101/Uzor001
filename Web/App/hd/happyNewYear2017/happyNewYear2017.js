

/// <reference path="/script/jquery-1.8.2.js" />
/// <reference path="/script/common.js" />

var inAc = false;

apiready = function () {

 

    inAc = true;
    BindPageSetting();

}





function BindPageSetting() {


}



function JoinNow() {


    console.log(inAc);
    if (inAc) {

        localStorage.OrderId = "16123005501636419097";
        openW({

            name: "OrderInfo",
            url:"../../Order/OrderInfo.html"

        })

    }
    else {
        window.location.href = ("http://a.app.qq.com/o/simple.jsp?pkgname=com.chuanfou.uzjob");

    }



}


