using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.Merchant
{
    public partial class SaveMer : BLL.BJ.MerSetting
    {
        public SaveMer()
             : base(Common.PageInput.ReDecimal("MerId", 1999))
        {


        }
        protected string MerJson = "{}";

        protected string VsMerTypeList = "[]";
        protected string imgArray = "[]";
        protected string MerContent = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            decimal MerId = PageInput.ReDecimal("MerId", 1999);
            if (MerId == 0)
            {

            }
            else
            {

                DataTable dt = ds_Mer.Tables[0];
                MerJson = JsonHelper.ToJsonNo1(dt);
                MerContent = dt.Rows[0]["MerchantContent"].ToString();
                VsMerTypeList = JsonHelper.ToJson(ds_Mer.Tables[1]);
                imgArray = JsonHelper.ToJson(ds_Mer.Tables[2]);
            }


        }
    }
}