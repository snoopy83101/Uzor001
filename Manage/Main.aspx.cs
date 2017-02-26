using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage
{
    public partial class Main : System.Web.UI.Page
    {
        protected string MenuTop = "";
        protected string MenuJson = "[]";
        protected string ScriptStr = "";
        protected decimal CurrentMerId = 0;
        protected string CDomain = "";
        public decimal QyWxPtId = 0;
        public string QyWxPtJson = "{}";
        protected void Page_Load(object sender, EventArgs e)
        {



            Model.CurrentMerModel cm = new Model.CurrentMerModel();

            StringBuilder s = new StringBuilder();
            StringBuilder sc = new StringBuilder();
            try
            {


                cm = BLL.MerchantBLL.CurrentModel();
                if (cm.CurrentUserId == "")
                {
                    BLL.UserBLL.MastLogin("");
                }
                CurrentMerId = cm.CurrentMerId;
                sc.Append(" var CMerId=" + cm.CurrentMerId + "; ");
                sc.Append("  localStorage['MerId'] = CMerId; ");
                sc.Append(" localStorage['UserId'] = '" + cm.CurrentUserId + "'; ");
                sc.Append(" var CurrentUserId='" + cm.CurrentUserId + "'; ");
                sc.Append(" var CMerName='" + cm.CurrentMerName + "'; ");
                sc.Append(" var domain= '" + Request.Url.Host + "'; ");
            }
            catch
            {
                Response.Redirect("~/Login.aspx");
            }
            BLL.UserBLL ubll = new BLL.UserBLL();

            s.Append(" select * from CORE.dbo.MenuInfo  with(nolock) ");
            s.Append(" where 1=1  and ( ");



            s.Append("  MenuId in (select MenuId from CORE.dbo.MenuVsMerRole where MerRoleId in (SELECT mvu.MerRoleId FROM dbo.MerRoleVsUser mvu WHERE UserId='" + cm.CurrentUserId + "') ) ");

            if (ubll.IsAdministrator())
            {
                //如果是超级管理员
                s.Append("  or (AdminPower = 100) ");

            }
            else
            {




            }

            if (cm.CurrentMerRoleName == "系统管理员")
            {
                //商家的系统管理员

                s.Append(" or (AdminPower = 90 ) ");

            }
            else
            {
                //不是商家的系统管理员


            }

            s.Append(" ) order by OrderNo  ");

            s.Append(" select * from dbo.UserView with(nolock) where UserId='" + cm.CurrentUserId + "' ");

            s.Append(" DECLARE @QyWxPtId as DECIMAL =(SELECT top 1 QyWxPtId FROM dbo.QyWxPt with(nolock) WHERE MerId='" + cm.CurrentMerId + "' order by OrderNo desc)");  //微信企业号
            s.Append(" SELECT * FROM dbo.QyWxPt WHERE QyWxPtId=@QyWxPtId  ");
            DataSet dsMenu = DAL.DalComm.BackData(s.ToString());

            DataTable dtMenu = Common.DataSetting.TableSelect(" ParentMenuId='' ", dsMenu.Tables[0]);
            DataTable dtUser = dsMenu.Tables[1];
            DataTable dtQyWxPt = dsMenu.Tables[2];
            if (dtQyWxPt.Rows.Count > 0)
            {

                QyWxPtJson = Common.JsonHelper.ToJsonNo1(dtQyWxPt);
                QyWxPtId = decimal.Parse(dtQyWxPt.Rows[0]["QyWxPtId"].ToString());
                sc.Append(" var QyWxPtId=" + QyWxPtId + "; ");
                sc.Append(" var QyWxPtJson=" + QyWxPtJson + "; ");
            }



            foreach (DataRow drUser in dtUser.Rows)
            {

                sc.Append(" localStorage.CurrentUserPicImgUrl='" + drUser["中头像"] + "'; ");
            }
            StringBuilder w = new StringBuilder();


            dtMenu.Columns.Add("Menus");
            foreach (DataRow drMenu in dtMenu.Rows)
            {

                w.Append("<li>");
                w.Append("<a>");
                w.Append("<span class='text'>");
                w.Append(drMenu["MenuName"]);
                w.Append("</span>");
                w.Append("</a>");
                w.Append("</li>");


                DataTable dtChildMenu = Common.DataSetting.TableSelect(" ParentMenuId='" + drMenu["MenuId"] + "' ", dsMenu.Tables[0]);

                drMenu["Menus"] = Common.JsonHelper.ToJson(dtChildMenu);

            }
            MenuJson = Common.JsonHelper.ToJson(dtMenu);

            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfig(cm.CurrentMerId);

            try
            {
                sc.Append(" var DingDanTiXing='" + MerConfig["DingDanTiXing"] + "'; ");
            }
            catch
            {
                sc.Append(" var DingDanTiXing='0'; ");

            }

            foreach (var item in MerConfig)
            {
                sc.Append(" localStorage." + item.Key + "='" + item.Value + "'; ");
            }


            ScriptStr = sc.ToString();





            //BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            //mbll.NewDingDanTiXing(CurrentMerId);

        }
    }
}