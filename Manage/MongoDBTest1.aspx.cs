using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using LitJson;
using Newtonsoft.Json;

using System.IO;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using System.Text;

namespace Manage
{
    public partial class MongoDBTest1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationSettings.AppSettings["MongoConnectStr"].ToString();

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("mydb");
            var collection = db.GetCollection<BsonDocument>("bar");


            JObject j = new JObject();




            BsonDocument b = new BsonDocument();

            b["iiid"] = 1;
            b["CreateTime"] = DateTime.Now;
            b["buer"] = true;


            //  collection.InsertOne(new BsonDocument(b));

    

       //     var jc = BsonJavaScript.Create("");

           


            List<BsonDocument> list = collection.Find(BsonDocument.Parse("{iiid:2}")).ToList();




            foreach (BsonDocument item in list)
            {





                var iii = item.ToJson();

                var str1 = item["_id"].ToString();






            }


            MongoDB.Bson.IO.JsonWriterSettings st = new MongoDB.Bson.IO.JsonWriterSettings();



            st.GuidRepresentation = GuidRepresentation.Standard;
            st.Indent = true;

            st.OutputMode = JsonOutputMode.Strict;






            BsonSerializationArgs args = default(BsonSerializationArgs);
            args.SerializeAsNominalType = true;
            args.SerializeIdFirst = true;
            Action<BsonSerializationContext.Builder> configurator;


            list.ToJson(st, null, null, args);

            string str = list.ToJson();




            //   JsonData jj =   JsonMapper.ToObject("{OrderId:" + OrderId + "}");


            //  string str="[{ "_id" : ObjectId("5854a625d77f05e25ca56f33"), "Name" : "Jack" }, { "_id" : ObjectId("5854a629d77f05e25ca56f34"), "Name" : "Jack" }, { "_id" : ObjectId("5854a62ad77f05e25ca56f35"), "Name" : "Jack" }, { "_id" : ObjectId("5854a752d77f06e25cbc794c"), "test" : 1, "name" : "测试" }, { "_id" : ObjectId("5854a7a6d77f07e25cef3b19"), "test" : 1, "name" : "测试", "testJson" : { "二级测试" : "二级测试", "二级测试2" : "二级测试2" } }]"


            var js = new JsonSerializerSettings();

            JArray JArrayObj = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(list.ToJson());









            Response.Write(JArrayObj.ToString());
            Response.End();

        }
    }
}