
/**
 * Module dependencies.
 */

var express = require('express');
var routes = require('./routes/index.js');
var http = require('http');
var path = require('path');
var xml2js = require('xml2js');
var fs = require('fs');
var data = fs.readFileSync("web.config", "utf-8");




var parser = new xml2js.Parser();   //xml -> json


var parseString = parser.parseString;

parseString(data, function (err, result) {
 //   console.dir(JSON.stringify(result));
       global.mgcon = result.configuration.appSettings[0]["add"][2]["$"]["value"];


});





var app = express();





// all environments
app.set('port', process.env.PORT || 1337);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');
//app.use(express.favicon());
//app.use(express.logger('dev'));
//app.use(express.json());
//app.use(express.urlencoded());
//app.use(express.methodOverride());
//app.use(app.router);dd
//app.use(require('stylus').middleware(path.join(__dirname, 'public')));
app.use(express.static(path.join(__dirname, '/public')));

// development only
if ('development' == app.get('env')) {
    console.log("这是开发环境!");
 
    app.locals.pretty = true;
}


app.get('*', routes.index);
app.post('*', routes.ajax);

http.createServer(app).listen(app.get('port'), function () {
    console.log('服务已启动,正在监听端口:' + app.get('port'));
});
    