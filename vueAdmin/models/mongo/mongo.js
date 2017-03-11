
var mongoose = require('mongoose');



exports.Insert = function(o) {



    mongoose.connect('' + global.mgcon + '/' + o.db + '');
    var db = mongoose.connection;
    db.on('error', console.error.bind(console, 'connection error:'));

    db.on('connected', function() {
        console.log('Mongoose connection open to');
    });


    db.once('open', function(call) {
        // yay!

        console.log("已经打开权限!");
        var c = db.collection(o.collection);

        c.insertOne(o.data, function(a, b) {

            o.fun(a, b);

        });

        db.close();
    });




}


exports.Save = function(o) {


    mongoose.connect('' + global.mgcon + '/' + o.db + '');
    var db = mongoose.connection;
    db.on('error', console.error.bind(console, 'connection error:'));

    db.on('connected', function() {
        console.log('Mongoose connection open to');
    });

    o.data._id=mongoose.Types.ObjectId(o.data._id);
    db.once('open', function(call) {
        // yay!

        console.log("已经打开权限!");
        var c = db.collection(o.collection);




        c.save(o.data, function(a, b) {

            o.fun(a, b);

        });

        db.close();
    });


}



exports.findOne = function(o) {


    mongoose.connect('' + global.mgcon + '/' + o.db + '');
    var db = mongoose.connection;
    db.on('error', console.error.bind(console, 'connection error:'));

    db.on('connected', function() {
        console.log('Mongoose connection open to');
    });


    db.once('open', function(call) {
        // yay!


        var c = db.collection(o.collection);

        o.data._id = mongoose.Types.ObjectId(o.data._id);


        c.findOne(o.data, function(err, data) {

            o.fun(err, {info:data});

        });

        db.close();
    });


}

//查询多条数据
exports.find = function(o) {


    if (!o.data) {

        o.data = {};
    }

    if (!o.sort) {
        o.sort = {};
    }
    if (!o.limit) {

        o.limit = 1000;
    }
    if (!o.page) {
        o.page = 1;
    }

    o.skip = (o.page - 1) * o.limit;





    mongoose.connect('' + global.mgcon + '/' + o.db + '');
    var db = mongoose.connection;
    db.on('error', console.error.bind(console, 'connection error:'));

    db.on('connected', function() {
        console.log('Mongoose connection open to');
    });


    db.once('open', function(call) {
        // yay!


        var c = db.collection(o.collection);



        c.find(o.data).count(function myfunction(err, count) {

            console.log(count);
            var totalPage = Math.ceil(count / o.limit);


            c.find(o.data).skip(o.skip).limit(o.limit).sort(o.sort).toArray(function(err, list) {

                o.fun(err, {
                    list: list,
                    totalPage: totalPage, //总页码
                    page: o.page, //当前页码
                    limit: o.limit  //一页多少条

                });
                db.close();
            });





        });






    });

}

//}
