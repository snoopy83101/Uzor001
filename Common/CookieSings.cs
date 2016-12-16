using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Common
{
    public class CookieSings
    {


        /// <summary>
        /// 获得当前用户ID,没有任何安全验证,慎用!
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserId()
        {

            string uid = HttpUtility.UrlDecode(Common.CookieSings.ReCooke("CurrentUserId"));
            uid = uid.Replace("&UserId=", "");
            uid = uid.Replace("&CurrentUserId=", "");
            if (uid == null)
            {
                uid = "";
            }
            return uid;
        }

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
                ck[cookiename] = "";
                ck.Expires = DateTime.Now.AddHours(-24);
                HttpContext.Current.Response.AppendCookie(ck);
            }
            //else
            //{
            //    ck = new HttpCookie(cookiename);

            //}

        }


        /// <summary>
        /// 登陆用户
        /// </summary>
        /// <param name="loginname"></param>
        public static void LoginIn(string UserCode)
        {
            //用户登录
            //UserCode = Common.JiaMi.EnCode(UserCode);
            HttpCookie ck = new HttpCookie("UserCode");
            //ck.Expires = DateTime.Now.AddMinutes(30); //DateTime.Now.AddDays(1);
            ck.HttpOnly = false;
            ck.Value = UserCode;
            HttpContext.Current.Response.AppendCookie(ck);
            //HttpContext.Current.Session["userlogin"] = BLL.Cookie.JiaMi.DeCode(adminName);

        }

        /// <summary>
        /// 给指定的COOKIE增加指定的值
        /// </summary>
        /// <param name="cookiename">传入COOKIE名称</param>
        /// <param name="addString">传入要增加的字符串</param>
        public static void AddCookieStr(string cookiename, string addString)
        {

            ClearCookie(cookiename);
            HttpCookie ck = new HttpCookie(cookiename);
            //ck.Expires = DateTime.Now.AddMinutes(30); //DateTime.Now.AddDays(1);

            ck.HttpOnly = false;
            ck.Value = addString;
            HttpContext.Current.Response.AppendCookie(ck);
        }

        /// <summary>
        /// 获得Cookie值
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static string ReCooke(string cookiename)
        {
            try
            {
                string str = HttpContext.Current.Request.Cookies[cookiename].Value;
                str = str.Replace("&" + cookiename + "=", "");
                return str;
            }
            catch (Exception)
            {

                return "";
            }
   
        }

        public static string GetCookie(string Key)
        {
            try
            {

                var ck = HttpContext.Current.Request.Cookies[Key].Value;

                ck = ck.Replace("&" + Key + "=", "");
                ck = ck.Replace("" + Key + "=", "");
                return ck;
            }
            catch
            {

                return "";
            }
        }

        /// <summary>
        /// 给指定的COOKIE增加指定的值
        /// </summary>
        /// <param name="cookiename">传入COOKIE名称</param>
        /// <param name="addString">传入要增加的字符串</param>
        /// <param name="AddMonths">需要记住的月份</param>
        public static void AddCookieStr(string cookiename, string addString, int AddMonths)
        {

            ClearCookie(cookiename);

            HttpCookie ck = new HttpCookie(cookiename);

            if (AddMonths == 0)
            {

            }
            else
            {
                ck.Expires = DateTime.Now.AddMonths(AddMonths); //DateTime.Now.AddDays(1);
            }


            ck.HttpOnly = false;
            ck.Value = addString;
            HttpContext.Current.Response.AppendCookie(ck);
        }
    }
}
