


var ajax = require("../ajax/ajax");
var mongo = require("../mongo/mongo");

var validator = require("validator");
var moment = require("moment");


function get(req, res) {
    try {
        var para = req.body.para;


        switch (para) {
            case "saveCustomer":  //获取用户信息

                saveCustomer(req, res);
                break;
            case "getCustomer":   //获取用户信息
                getCustomer(req, res);
                break;
            case "getCustomerList":   //获取用户信息列表
                getCustomerList(req, res);
                break;
            default:
        }
    } catch (e) {


        console.log(e);
        ajax.re(res, {

            re: "notok",
            err: e,
            errMsg: e
        })
    }


}

function saveCustomer(req, res) {

    var j = req.body.j;

    if (!validator.isLength(j.name, { min: 1, max: 100 })) {

        throw "名称长度必须在1到100之间"
    }

    if (!j._id || !j.createTime) {
        j.createTime = moment().zone(-8)._d;
    }
    try {
        j.createTime=moment(j.createTime)._d;
    } catch (e) {
        
    } finally {
        
    }
   

    mongo.Save({

        data: j,
        db: "uzor",
        collection: "customer",
        fun(err, re) {
            if (err) {

                ajax.re(res, { err: err });
            }
            else {

                ajax.re(res, { val: j });
            }



        }

    });


}


function getCustomer(req, res) {
    var j = req.body.j;

    if (!j._id) {
        throw '不能没有id'
    }

    mongo.findOne({
        data: {
            _id: j._id

        },
        db: "uzor",
        collection: "customer",
        fun: function(err, data) {
            ajax.re(res, {

                err: err,
                val: data
            })


        }

    })
}


//查询客户列表
function getCustomerList(req, res) {

    var j = req.body.j
   
    mongo.find({

        data: j.data,
        limit: 10,
        db: "uzor",
        collection: "customer",
        sort: { createTime: -1 },
        page: j.page,
        fun: function(err, data) {

            ajax.re(res, {

                err: err,
                val: data
            })


        }

    })


}
module.exports = get;