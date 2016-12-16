using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common.Cookies
{
    public class CookieSings
    {
    
        /// <summary>
        /// 把COOKIE命名为空字符串""
        /// </summary>
        /// <param name="cookiename">传入Cookie的名称</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie ck;
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                ck = HttpContext.Current.Request.Cookies[cookiename];

            }
            else
            {
                ck = new HttpCookie(cookiename);

            } 
            ck[cookiename] = "";
            ck[cookiename] = "";
            HttpContext.Current.Response.AppendCookie(ck);
        }


        public void LoginIn(string UserCode)
        {
          

        }


        /// <summary>
        /// 给指定的COOKIE增加指定的值
        /// </summary>
        /// <param name="cookiename">传入COOKIE名称</param>
        /// <param name="addString">传入要增加的字符串</param>
        public static void AddCookieStr(string cookiename,string addString)
        {
            //用户登录

            HttpCookie ck = new HttpCookie(cookiename);
            //ck.Expires = DateTime.Now.AddMinutes(30); //DateTime.Now.AddDays(1);
            ck.HttpOnly = false;
            ck.Value = addString;
            HttpContext.Current.Response.AppendCookie(ck);
            //HttpContext.Current.Session["userlogin"] = BLL.Cookie.JiaMi.DeCode(adminName);
        }
    }
}
