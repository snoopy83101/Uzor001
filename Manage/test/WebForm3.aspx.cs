using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LitJson;
namespace Manage.test
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            JsonData j = new JsonData();
            j["test"] = "中文";

            Response.Write(j.ToJson());
            Response.End();

        }
    }
}