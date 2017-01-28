using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LitJson;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace Manage.test
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BsonDocument b = new BsonDocument();

          


            JArray j =  DAL.Mongo.Find(b, "MemberLog", "log");

        }
    }
}