using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
namespace Manage.Merchant.PopWindow
{
    public partial class SaveMerRole : System.Web.UI.Page
    {
        protected string MerRoleJson = "{ }";
        protected void Page_Load(object sender, EventArgs e)
        {

            decimal MerRoleId = Common.PageInput.ReDecimal("MerRoleId", 0);

            if (MerRoleId != 0)
            {
                //是修改

                DAL.MerRoleDAL dal = new DAL.MerRoleDAL();
                DataTable dt = dal.GetList(" MerRoleId='" + MerRoleId + "' ").Tables[0];

                MerRoleJson = Common.JsonHelper.ToJsonNo1(dt);


            }
            else
            {

            }
        }
    }
}