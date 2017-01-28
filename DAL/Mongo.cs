using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Model;
using DBTools;
using Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using MongoDB.Driver;
namespace DAL
{
    public partial class Mongo
    {
        static string connectionString = System.Configuration.ConfigurationSettings.AppSettings["MongoConnectStr"].ToString();

        static MongoClient client = new MongoClient(connectionString);





        public static JObject Insert(BsonDocument b, string collectionName, string dbName = null)
        {

            JObject rj = new JObject();
            try
            {
                if (b["_id"] != null)
                {

                    if (b["_id"].ToString() == "")
                    {

                        b.Remove("_id");
                    }
                }
            }
            catch (Exception)
            {


            }




            foreach (BsonValue v in b.Values)
            {

                if (v.BsonType == BsonType.Decimal128)
                {


                    throw new Exception("不建议将decimal插入到MongoDB数据库中, 请转换为double类型!");
                }


            }


            if (dbName == null)
            {

                dbName = "uzor";
            }
            if (collectionName == null)
            {

                throw new Exception("collection不能为空!");
            }

            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);
            collection.InsertOne(b);
            return rj;

        }

        public static JObject Update(BsonDocument b, string collectionName, string dbName = null, bool IsUpsert = false)
        {



            JObject rj = new JObject();

            if (b["_id"] == null)
            {

                throw new Exception("此方法必须通过_Id更改因此不能为空!");


            }


            if (b["_id"].ToString() == "")
            {

                throw new Exception("此方法必须通过_Id更改因此不能为空!");
            }

            try
            {
                b["_id"] = ObjectId.Parse(b["_id"].ToString());
            }
            catch (Exception)
            {

       
            }

            if (dbName == null)
            {

                dbName = "uzor";
            }
            if (collectionName == null)
            {

                throw new Exception("collection不能为空!");
            }

            BsonDocument fb = new BsonDocument();
            BsonDocument sb = new BsonDocument();
            fb["_id"] = b["_id"];
            b.Remove("_id"); //不更改ID


            sb["$set"] = b;



            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            UpdateOptions uo = new UpdateOptions();
            uo.IsUpsert = IsUpsert;


            collection.UpdateOne(fb, sb,uo);
            return rj;


        }
        public static JArray Find(BsonDocument b, string collectionName, string dbName = null)
        {



            JArray rj = new JArray();


            if (dbName == null)
            {

                dbName = "uzor";
            }
            if (collectionName == null)
            {

                throw new Exception("collection不能为空!");
            }

            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            List<BsonDocument> list = collection.Find(b).ToList();

            rj = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(list.ToJson());

            return rj;
        }
        public static JObject Find(BsonDocument b, int CurrentPage, int PageSize, string collectionName, BsonDocument Sort = null, string dbName = null)
        {



            JArray rj = new JArray();


            if (dbName == null)
            {

                dbName = "uzor";
            }
            if (collectionName == null)
            {

                throw new Exception("collection不能为空!");
            }

            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            int totalPage = 1;

            long RowCount = collection.Find(b).Count();

            decimal d = (decimal)RowCount / (decimal)PageSize;


            totalPage = (int)Math.Ceiling(d);

            if (Sort == null)
            {

                Sort = new BsonDocument();
            }

            int SkipNum = (CurrentPage - 1) * PageSize;


            List<BsonDocument> list = collection.Find(b).Skip(SkipNum).Limit(PageSize).Sort(Sort).ToList();




            rj = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(list.ToJson());

            JObject j = new JObject();
            j["list"] = rj;
            j["CurrentPage"] = CurrentPage;
            j["TotalPage"] = totalPage;
            j["PageSize"] = PageSize;
            j["RowCount"] = RowCount;
            return j;
        }

        public static JObject DeleteOne(BsonDocument b, string collectionName, string dbName = null)
        {
            JObject rj = new JObject();

            if (dbName == null)
            {

                dbName = "uzor";
            }
            if (collectionName == null)
            {

                throw new Exception("collection不能为空!");
            }
            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            collection.DeleteOne(b);





            return rj;

        }


        public static JObject DeleteMany(BsonDocument b, string collectionName, string dbName = null)
        {
            JObject rj = new JObject();

            if (dbName == null)
            {

                dbName = "uzor";
            }
            if (collectionName == null)
            {

                throw new Exception("collection不能为空!");
            }
            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            collection.DeleteMany(b);

            return rj;

        }


    }
}
