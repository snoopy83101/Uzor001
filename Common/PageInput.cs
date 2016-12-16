using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Common;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Net;
namespace Common
{
    public class PageInput
    {
        private const string StrKeyWord = "select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|or|and";
        private const string StrRegex = "[-|;|,|/|(|)|[|]|}|{|%|@|*|!|']";



        /// <summary>
        /// 微信接口专用
        /// </summary>
        /// <param name="posturl">微信接口的url</param>
        /// <param name="postData">微信接口提交的data</param>
        /// <returns></returns>
        public static string WxGetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...  
            try
            {
                // 设置参数  
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据  
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求  
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码  
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }


        public static string TokenJson()
        {
            return GetHTML("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx7f141ebca00734c2&secret=67178d423e17a75e4c9444d587160027");

        }
        public static string TokenStr()
        {
            var jsonStr = GetHTML("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx7f141ebca00734c2&secret=67178d423e17a75e4c9444d587160027");

            return StringPlus.GetTwoMiddleFirstStr(jsonStr, "\"access_token\":\"", "\",\"expires_in");
        }

        public static string GetHTML(string url)
        {
            WebClient web = new WebClient();

            web.Headers.Add("Authorization:Basic");
            byte[] buffer = web.DownloadData(url);


            return Encoding.UTF8.GetString(buffer);



        }

        //检测字符串
        public static bool CheckKeyWord(string _sWord)
        {
            if (Regex.IsMatch(_sWord, StrKeyWord, RegexOptions.IgnoreCase) || Regex.IsMatch(_sWord, StrRegex))
                return true;
            return false;
        }

        public static bool SqlFilter2(string InText)
        {
            string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join"; if (InText == null)
                return false;

            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }

            return false;
        }


        public static int ReInt(string paraName)
        {
            return int.Parse(HttpContext.Current.Request.Params[paraName]);

        }
        public static int ReInt(string paraName, int CatchInt)
        {
            try
            {
                return ReInt(paraName);
            }
            catch
            {
                return CatchInt;
            }

        }

        public static TimeSpan ReTimeSpan(string str)
        {



            return TimeSpan.Parse(ReStr(str));

        }

        public static TimeSpan ReTimeSpan(string str, TimeSpan cat)
        {
            try
            {
                return ReTimeSpan(str);
            }
            catch
            {

                return cat;
            }

        }

        public static string ReStr(string paraName)
        {

            string str = HttpContext.Current.Request.Params[paraName];

            if (str == null)
            {

                throw new Exception("" + paraName + "参数获取为null");
            }

            if (SqlFilter2(str))
            {
                throw new Exception("危险字符串:" + str + "");
            }

            if (paraName == "para")  //ajax请求开始
            {
                string s = ReStr("s", "web");
                if (s == "app")
                {
                    string GetMemberId = ReStr("GetMemberId");
                    CookieSings.AddCookieStr("CurrentMemberId", JiaMi.encMe(GetMemberId));
                }
            }



            return System.Web.HttpUtility.UrlDecode(str);

        }

        public static string ReStr(string paraName, string CatchStr)
        {
            try
            {

                return ReStr(paraName);
                //string r = System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Params[paraName]);
                //if (r == null)
                //{
                //    throw new Exception("");
                //}
                //return r;
            }
            catch
            {
                return CatchStr;

            }



        }
        public static DateTime ReTime(string paraName)
        {
            return DateTime.Parse(ReStr(paraName));

        }
        public static DateTime ReTime(string paraName, DateTime CatchTime)
        {
            try
            {
                return DateTime.Parse(ReStr(paraName));
            }
            catch
            {
                return CatchTime;
            }

        }
        public static decimal ReDecimal(string paraName)
        {


            return decimal.Parse(HttpContext.Current.Request.Params[paraName]);

        }
        public static decimal ReDecimal(string paraName, decimal CatchNum)
        {
            try
            {
                return decimal.Parse(HttpContext.Current.Request.Params[paraName]);
            }
            catch (Exception)
            {
                return CatchNum;
            }



        }
        /// <summary>
        /// 去登录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tip"></param>
        public static void ToLogin(string url, string tip)
        {
            url = HttpUtility.UrlEncode(url);
            tip = HttpUtility.UrlDecode(tip);

            HttpContext.Current.Response.Redirect("/login/?url=" + url + "&tip=" + tip + "");
        }
        public static void ToLogin(string tip)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            ToLogin(url, tip);

        }

        public static string InputCode(string str)
        {
            return PinYin.GetFirstLetter(str);
        }
    }
}
