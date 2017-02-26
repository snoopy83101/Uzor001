/// <reference path="/js/jquery-1.8.2.js" />
/// <reference path="/js/ZYUiPub.js" />


var customer = {};


$(function() {



    BindPageSetting();

})


function BindPageSetting() {



    customer._id = GetQueryString("customerId");
    BindAddress(document.getElementById("div_address"), function() {

        GetCustomer();

    });


}


function SaveCustomer() {

    customer = {

        name: $("#txt_CustomerName").val(),
        classId: $("#sel_CustomerClass").val(),
        className: $("#sel_CustomerClass").find("option:selected").text(),
        address:

        {
            memo: $("#txt_Address").val(),
            provinceId: $(".sel_province").val(),
            provinceName: $(".sel_province").find("option:selected").text(),
            cityId: $(".sel_city").val(),
            cityName: $(".sel_city").find("option:selected").text(),
            areaId: $(".sel_area").val(),
            areaName: $(".sel_area").find("option:selected").text()

        },
        tell: $("#txt_Tell").val(),
        email: $("#txt_Email").val(),
        managerName: $("#txt_ManagerName").val(),
        website: $("#txt_WebSite").val(),
        shopsite: $("#txt_Shop").val()



    }


    AjaxSubmit({
        class: "customer",
        para: "saveCustomer",
        j: JSON.stringify(customer)

    }, function(data) {
        if (data.re == "ok") {

            alert("成功保存用户信息!");
            window.location.href = ("customerInfo?customerId=" + data._id + "");

        }
        else {
            alert(data.errMsg)
        }

    });




}

function GetCustomer() {

    if (customer._id) {

        AjaxSubmit({
            class: "customer",
            para: "getCustomer",
            j: JSON.stringify({ _id: customer._id })

        }, function(data) {

            customer = data;

            $("#sel_CustomerClass").val(customer.classId);

            $("#txt_CustomerName").val(customer.name);
            $(".sel_province").val(customer.address.provinceId);
            $(".sel_province").change()
            $(".sel_city").val(customer.address.cityId);
            $(".sel_city").change();
            $(".sel_area").val(customer.address.areaId);
            $(".sel_area").change();
            $("#txt_Tell").val(customer.tell);
            $("#txt_Address").val(customer.address.memo);
            $("#txt_Email").val(customer.email);
            $("#txt_ManagerName").val(customer.managerName);
            $("#txt_WebSite").val(customer.website);
            $("#txt_Shop").val(customer.shopsite);
            


        });
    }


}






