using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.Pub
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string CurrentUserId = "";
        public bool IsAdmin = false;
        public bool IsLogin = false;
        public string ScriptStr = "";
        public string MerJson = "{ MerchantId:0 }";
        protected string NowDate = "";


        protected void Page_Load(object sender, EventArgs e)
        {


            NowDate = DateTime.Now.ToString();
            BLL.UserBLL ubll = new BLL.UserBLL();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();



            CurrentUserId = cm.CurrentUserId;
            IsAdmin = ubll.IsAdministrator();

            if (CurrentUserId.Trim() == "" || CurrentUserId == null)
            {

                IsLogin = false;
                Response.Write("<script src=\"/Script/jquery-1.8.2.js\"></script><script src = \"/Script/ZYUiPub.js\" ></script> ");
                Response.Write("<script>window.parent.LoginCookie(); setTimeout(function(){ shuaxin(); },1000)  </script>");
                Response.End();
                // throw new Exception("您还没有登录!");
            }
            else
            {

                IsLogin = true;
            }

            StringBuilder s = new StringBuilder();
            s.Append(" var CurrentUserId='" + CurrentUserId + "'; ");
            s.Append(" var IsAdmin=" + IsAdmin.ToString().ToLower() + "; ");
            s.Append(" var IsLogin=" + IsLogin.ToString().ToLower() + "; ");

            try
            {
                decimal MerId = Convert.ToDecimal(Request.QueryString["MerId"]);
                if (MerId == 0)
                {
                    MerId = BLL.MerchantBLL.CurrentModel().CurrentMerId;
                }

                if (MerId == 0)
                {

                }
                else
                {
                    mbll.CheckChangeMerPower(MerId);

                }





                MerJson = Common.JsonHelper.ToJsonNo1(mbll.GetMerInfoFaseById(MerId));

                s.Append(" var MerJson=" + MerJson + "; ");
            }
            catch
            {

            }

            ScriptStr = s.ToString();




        }
    }
}