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
using io.rong;
using LitJson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace BLL
{
    public class AdBLL
    {


        public void SaveAd(BsonDocument data)
        {


            if (data["_id"].ToString() == "")
            {
                DAL.Mongo.Insert(data, "Ad");

            }
            else
            {
                DAL.Mongo.Update(data, "Ad");

            }




        }
    }
}
