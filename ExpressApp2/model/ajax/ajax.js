
//ajax返回信息
function re(res, o) {


    if (!o) {
        o = {};


    }

    if (!o.val) {
        o.val = { re: "ok" };
    }
    if (o.err) {




        o.val.err = o.err;
        o.val.re = "notok";
        if (!o.errMsg) {

            o.errMsg = o.err;
        }
        o.val.errMsg = o.errMsg;

    }
    else {
        o.val.re = "ok";
    }

    if (o.val instanceof Array) {

        var sub = o.val;

        o.val = { re: "ok" }
        o.val.list  = sub;
    }





    var val = JSON.stringify(o.val);

    res.writeHead(200, { "Content-Type": "text/plain", "Access-Control-Allow-Origin": "http://uzor001.com,http://uzor000.com" });
    res.write(val);
    res.end();

}


exports.re = re;