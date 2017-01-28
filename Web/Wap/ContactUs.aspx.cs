using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Wap
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected string scStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            StringBuilder sc = new StringBuilder();

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT Lng,Lat,Phone,Tell,Address FROM dbo.MerchantView WHERE MerchantId=1999 ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];

            sc.Append(" var mj=" + Common.JsonHelper.ToJsonNo1(dt) + "; ");
            scStr = sc.ToString();
        }
    }
}