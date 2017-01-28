using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;
using System.Text;
using System.Xml;
using Common;
using System.IO;
using LitJson;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BPage
{
    public class Ads : Common.BPageSetting2
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string para = ReStr("para");
                switch (para)
                {


                    case "SaveAd":
                        SaveAd();
                        break;

                    case "GetAdList":
                        GetAdList();
                        break;

                    case "GetAdInfo":
                        GetAdInfo();
                        break;
                    case "DeleteAd":
                        DeleteAd();
                        break;

                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();





        }

        private void DeleteAd()
        {
            var _id = ReObjId("_id", "");




            BsonDocument b = new BsonDocument();

            b["_id"] = _id;

            DAL.Mongo.DeleteOne(b, "Ad");
            ReTrue();


        }

        private void GetAdInfo()
        {
            string _id = ReStr("_id", "");
            string Client = ReStr("Client", "");
            string WinName = ReStr("WinName", "");
            string FrameName = ReStr("FrameName", "");
            string Loction = ReStr("Loction", "");
            BsonDocument b = new BsonDocument();


            if (_id != "")
            {

                b["_id"] = MongoDB.Bson.ObjectId.Parse(_id);
            }
            if (Client != "")
            {

                b["Client"] = Client;
            }

            if (WinName != "")
            {

                b["WinName"] = WinName;
            }
            if (FrameName != "")
            {

                b["FrameName"] = FrameName;
            }
            if (Loction != "")
            {

                b["Loction"] = Loction;
            }


            JObject j = new JObject();





            JArray ja = DAL.Mongo.Find(b, "Ad");


            if (ja.Count > 0)
            {
                j = (JObject)ja[0];
            }

            ReDict.Add("info", j);


            ReTrue();


        }

        private void GetAdList()
        {


            BsonDocument b = new BsonDocument();


            b = ReBson("b", new BsonDocument());

            JArray ja = DAL.Mongo.Find(b, "Ad", "uzor");


            ReDict.Add("list", ja);

            ReTrue();


        }

        private void SaveAd()
        {



            BsonDocument j = new BsonDocument();
            j = ReBson("data", null);

            j["ChangeTime"] = DateTime.Now;

            BLL.AdBLL bll = new BLL.AdBLL();

            bll.SaveAd(j);



            ReTrue();



        }
    }


}
