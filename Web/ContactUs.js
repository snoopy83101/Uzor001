/// <reference path="C:\anli\快工场\Web\script/jquery-1.8.2.js" />
/// <reference path="C:\anli\快工场\Web\script/ZYUiPub.js" />


var iMarkerPoint;
if (MobileUA.SMART_PHONE) {

    tiaozhuan("/Wap/ContactUs");
}
$(function () {

    BindPageSetting();

});


function BindPageSetting()
{
    $("#a_dh_ContactUs").addClass("select");

    $("#sp_address").html("地址:" + mj.Address);
    $("#sp_Tell").html("联系电话:" + mj.Tell);

    BindMap(mj.Lng,mj.Lat);

}


//#region  地图相关



function BindMap(lng, lat) {

    if (lng == null || lat == null) {
        lng = 118.163651;
        lat = 36.197684;

    }

    var map = new BMap.Map("div_Map", { enableMapClick: false });
    var point = new BMap.Point(lng, lat);
    map.centerAndZoom(point, 17);   //设置中心点,以及缩放比例


    iMarker = new BMap.Marker(point);  // 创建标注
    map.addOverlay(iMarker);


    //iMarker = new BMap.Marker(point);  // 创建标注


    //iMarker.addEventListener("dragend", function () {
    //    iMarkerPoint = this.getPosition();  //获取一个点



    //});
    iMarker.addEventListener("dragend", function () {
        iMarkerPoint = this.getPosition();  //获取一个点



    });


    iMarker.enableDragging();    //可拖拽
    //map.addOverlay(iMarker);
    iMarkerPoint = iMarker.getPosition();  //获取当前点坐标
    map.enableScrollWheelZoom();    //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用
    map.addEventListener("click", function (e) {

        if (e.overlay != null) {
            return;
        }
        else {
            if (iMarker != null) {
                map.removeOverlay(iMarker);

            }

        }
        iMarkerPoint.lng = e.point.lng;
        iMarkerPoint.lat = e.point.lat;
        iMarker = new BMap.Marker(e.point);  // 创建标注
        map.addOverlay(iMarker);
        iMarker.enableDragging();    //可拖拽
        //map.addOverlay(iMarker);
        iMarkerPoint = iMarker.getPosition();  //获取当前点坐标
        iMarker.addEventListener("dragend", function () {
            iMarkerPoint = this.getPosition();  //获取一个点



        });
    });

}


//#endregion
