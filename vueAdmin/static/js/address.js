
var address = [];
function BindAddress(obj,fun) {


    if (!fun)
    {
        fun = function () {

        }
    }

    $.get('/js/address.json').done(function(data) {

        // console.log(JSON.stringify(address));
        address = data;

        var w = [];


        w.push("<select class='sel_province' onchange='ProvinceChange(this)' >");

        for (var i = 0; i < address.length; i++) {

            var j = address[i];
            w.push("<option value='" + j.ProvinceId + "'  pi='" + i + "' >");
            w.push(j.ProvinceName);
            w.push("</option>");

        }

        w.push("</select>");

        w.push("<select class='sel_city' onchange='CityChange(this)' >");

        for (var i = 0; i < address[0].sub.length; i++) {

            var j = address[0].sub[i];
            w.push("<option value='" + j.CityId + "' pi='0' ci='" + i + "' >");
            w.push(j.CityName);
            w.push("</option>");
        }
        w.push("</select>");

        w.push("<select class='sel_area' onchange='AreaChange(this)' >");

        for (var i = 0; i < address[0].sub[0].sub.length; i++) {

            var j = address[0].sub[0].sub[i];
            w.push("<option value='" + j.AreaId + "' pi='0' ci='0' ai='" + i + "' >");
            w.push(j.AreaName);
            w.push("</option>");
        }
        w.push("</select>");

        $(obj).html(w.join(""));
        $(obj).find(".sel_province").change();
        fun(obj);
    });



}

function ProvinceChange(obj) {



    var pi = parseInt($(obj).find("option:selected").attr("pi"));


    var cityObj = $(obj).parent().find(".sel_city");

    var w = [];
    for (var i = 0; i < address[pi].sub.length; i++) {
        var j = address[pi].sub[i];
        w.push("<option value='" + j.CityId + "' pi='" + pi + "' ci='" + i + "' >");
        w.push(j.CityName);
        w.push("</option>");
    }

    cityObj.html(w.join(""));
    cityObj.change();

}


function CityChange(obj) {
    var pi = parseInt($(obj).find("option:selected").attr("pi"));
    var ci = parseInt($(obj).find("option:selected").attr("ci"));

    var areaObj = $(obj).parent().find(".sel_area");
    var w = [];
    for (var i = 0; i < address[pi].sub[ci].sub.length; i++) {
        var j = address[pi].sub[ci].sub[i];
        w.push("<option value='" + j.AreaId + "' pi='" + pi + "' ci='" + i + "' ai='" + i + "' >");
        w.push(j.AreaName);
        w.push("</option>");
    }

    areaObj.html(w.join(""));
}

function AreaChange(obj)
{

}