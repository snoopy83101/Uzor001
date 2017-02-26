
var customerList = [];

$(function myfunction() {
    BindPageSetting();
    LoadUI("page");
})


function BindPageSetting() {

    getCustomer(1)


}

function getCustomer(page) {





    AjaxSubmit({
        class: "customer",
        para: "getCustomerList",
        j: JSON.stringify({ data: {}, page: page })


    }, function(data) {

        var w = [];
        customerList = data.list;
        for (var i = 0; i < data.list.length; i++) {

            var j = data.list[i];

            w.push("<tr  i='" + i + "' >");
            w.push("<td>");
            w.push(j.name);
            w.push("</td>");
            w.push("<td>");
            w.push(j.tell);
            w.push("</td>");

            w.push("<td>");
            w.push(j.address.provinceName + j.address.cityName);
            w.push("</td>");
            w.push("<td>");
            w.push(0);
            w.push("</td>");
            w.push("<td>");
            w.push(0);
            w.push("</td>");
            w.push("<td>");
            w.push(j.managerName);
            w.push("</td>");
            w.push("<td>");
            w.push(j.createTime);
            w.push("</td>");

            w.push("</tr>");
        }


        $("#tb").html(w.join(""));
        $("#tb").find("tr").dblclick(function myfunction() {

            var i = parseInt($(this).attr("i"));

            var _id = customerList[i]._id;


            window.open("customerinfo?customerId=" + _id + "");

        });
        Page({

            obj: document.getElementById("div_page"),
            fun: function(page) {
                getCustomer(page);

            },
            totalPages: data.totalPages
        });


    });
}

function ToAdd() {

    tiaozhuan("customerInfo");
}