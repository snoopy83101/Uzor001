
/*
 * GET home page.
 */



exports.index = function (req, res) {

    switch (req.params[0].toLowerCase()) {



        case "/m/customerlist": //客户维护列表
            res.render('./Manage/Customer/list', { title: '客户维护列表', line: "<tr><td>尼玛哦. hello word!</td></tr>", t1: "客户列表", t2: "可以对客户信息进行查询和维护" });
            break;
        case "/m/customerinfo": //客户详情
            res.render('./Manage/Customer/info', { title: '客户编辑', t1: "编辑客户", t2: "基本信息" });
            break;
        case "/m/login": //用户端登录
            res.render('./Manage/Login', { title: '用户登录', t1: "编辑客户", t2: "基本信息" });
            break;



    }

    res.end();

};



exports.ajax = function (req, res) {




    console.log("111");

    var ajaxClass = req.body.class.toLowerCase()
    var ModelPath = "";
    switch (ajaxClass) {
        case "customer":
            ModelPath = "customer/customer";

            break;
        case "":
            break;

        default:

            throw "没有找到class:" + ajaxClass + ""
    }
    var ajaxModel = require("../models/" + ModelPath);
    var hello = new ajaxModel(req,res);


 //   console.log("激活ajax!");



}