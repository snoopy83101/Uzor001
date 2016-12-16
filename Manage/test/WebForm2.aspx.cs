using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.test
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //   HttpWebRequest objWebRequest = (HttpWebRequest)WebRequest.Create("");



            //BPage.Work w = new BPage.Work();
            //w.test1();



            Response.Write(BLL.FormulaBLL.transmitNum(Convert.ToDecimal(25.00001)));
        }

    }
}