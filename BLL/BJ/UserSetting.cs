using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BJ
{
    public class UserSetting : System.Web.UI.Page
    {


        public static  string UserId = "";
        public string UserJson = "";
        public void MustLogion()
        {
            BLL.UserBLL bll = new UserBLL();

            try
            {

                UserId = bll.CurrentUserId();
                if (UserId == "")
                {

                }
                UserJson = Common.JsonHelper.ToJsonNo1(bll.GetUserData(UserId));
            }
            catch (Exception ex)
            {
                string url = Server.UrlEncode(Request.RawUrl);
                Response.Redirect("/Login/?url=" + url + "");
                Response.End();
            }

        }

    }
}
