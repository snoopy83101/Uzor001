using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using LitJson;
namespace Manage.test
{
    public partial class MongoDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("mydb");
            var collection = db.GetCollection<BsonDocument>("bar");
            JsonData j = new JsonData();

            j["test"] = 1;
            j["name"] = "测试";


            JsonData j2 = new JsonData();
            j2["二级测试"] = "二级测试";
            j2["二级测试2"] = "二级测试2";
            j["testJson"] = j2;



            // collection.InsertOne(new BsonDocument(BsonDocument.Parse(j.ToJson())));


            var list = collection.Find(new BsonDocument()).ToList();



            Response.Write(list.ToJson());
            Response.End();
        }
    }
}