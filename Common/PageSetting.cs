using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
namespace Common
{

    /// <summary>
    /// 所有ajax页面的边界抽象类
    /// </summary>
    public class BPageSetting : System.Web.UI.Page
    {



        public List<Model.ReListModel> ReList = new List<Model.ReListModel>();
        public Dictionary<string, object> ReDict = new Dictionary<string, object>();
        public Dictionary<string, string> ReDict2 = new Dictionary<string, string>();





        public DataTable ReTable(string paraName)
        {

            return DataSetting.CXmlToDatatTable(ReStr(paraName,null));
        }

        public int ReInt(string paraName, int CatchInd)
        {
            try
            {
                return Common.PageInput.ReInt(paraName);
            }
            catch
            {

                return CatchInd;
            }


        }

        public int ReInt(string paraName)
        {
            return Common.PageInput.ReInt(paraName);

        }

        public string ReStrDeCode(string paraName, string CatchStr)
        {
            try
            {
                return ReStrDeCode(paraName);
            }
            catch
            {
                return CatchStr;

            }

        }


        public string ReStrDeCode(string paraName)
        {

            return HttpUtility.UrlDecode(ReStr(paraName));
        }

        public string ReStr(string paraName)
        {
            return Common.PageInput.ReStr(paraName);

        }

        public static TimeSpan ReTimeSpan(string str)
        {
            return Common.PageInput.ReTimeSpan(str);
        }

        public static TimeSpan ReTimeSpan(string str, TimeSpan cat)
        {

            return Common.PageInput.ReTimeSpan(str,cat);
        }

        public string ReStr(string paraName, string CatchStr)
        {

            return Common.PageInput.ReStr(paraName, CatchStr);
        }
        public DateTime ReTime(string paraName)
        {
            return Common.PageInput.ReTime(paraName);

        }

        public DateTime ReTime(string paraName, DateTime CatchTime)
        {
            return Common.PageInput.ReTime(paraName, CatchTime);
        }
        public decimal ReDecimal(string paraName)
        {


            return Common.PageInput.ReDecimal(paraName);

        }
        public decimal ReDecimal(string paraName, decimal CatchNum)
        {
            return Common.PageInput.ReDecimal(paraName, CatchNum);



        }

        /// <summary>
        /// 返回分页配置
        /// </summary>
        /// <param name="ds"></param>
        public void RePage(DataSet ds)
        {
            try
            {
                int totalPage = Common.DataSetting.totalPage(ds);  //最多页数
                int RowCount = Common.DataSetting.RowCount(ds);
                DataTable dt = ds.Tables[2];
                string json = Common.JsonHelper.ToJson(dt);
                ReDict2.Add("t", totalPage.ToString());
                ReDict2.Add("r", RowCount.ToString());
                ReDict.Add("list", json);

                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }

        }

        public void RePage2(DataSet ds)
        {
            try
            {
                DataRow dr = ds.Tables[1].Rows[0];
                int totalPage = int.Parse(dr["总页数"].ToString());
                int RowCount = int.Parse(dr["总行数"].ToString());
                int c = int.Parse(dr["当前页"].ToString());
                DataTable dt = ds.Tables[0];
                string json = Common.JsonHelper.ToJson(dt);
                ReDict2.Add("t", totalPage.ToString());
                ReDict2.Add("r", RowCount.ToString());
                ReDict2.Add("c", c.ToString());
                ReDict.Add("list", json);

                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }
        }

        public void RePage2(DataSet ds, DataSet ds2)
        {
            try
            {
                DataRow dr = ds.Tables[1].Rows[0];
                int totalPage = int.Parse(dr["总页数"].ToString());
                int RowCount = int.Parse(dr["总行数"].ToString());
                int c = int.Parse(dr["当前页"].ToString());
                DataTable dt = ds.Tables[0];
                string json = Common.JsonHelper.ToJson(dt);
                string json2 = Common.JsonHelper.ToJson(ds2);
                ReDict2.Add("t", totalPage.ToString());
                ReDict2.Add("r", RowCount.ToString());
                ReDict2.Add("c", c.ToString());
                ReDict.Add("by", json2);
                ReDict.Add("list", json);

                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }
        }


        public void RePage(DataSet ds, DataSet ds2)
        {
            try
            {
                int totalPage = Common.DataSetting.totalPage(ds);  //最多页数
                int RowCount = Common.DataSetting.RowCount(ds);
                DataTable dt = ds.Tables[2];
                string json = Common.JsonHelper.ToJson(dt);
                string json2 = Common.JsonHelper.ToJson(ds2);
                ReDict2.Add("t", totalPage.ToString());
                ReDict2.Add("r", RowCount.ToString());
                ReDict.Add("list", json);
                ReDict.Add("by", json2);
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }

        }




        /// <summary>
        /// 返回分页配置
        /// </summary>
        /// <param name="ds"></param>
        public void ReProCommentPage(DataSet ds, DataTable CommDt)
        {
            try
            {
                int totalPage = Common.DataSetting.totalPage(ds);  //最多页数
                DataTable dt = ds.Tables[2];
                string json = Common.JsonHelper.ToJson(dt);
                ReDict2.Add("t", totalPage.ToString());
                ReDict.Add("list", json);
                ReDict.Add("CommList", CommDt);
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }

        }
        public bool ReBool(string paraName, bool CatchBool)
        {
            bool b;
            try
            {
                b = ReBool(paraName);
            }
            catch
            {
                b = CatchBool;
            }

            return b;
        }

        public bool ReBool(string paraName)
        {
            string str = ReStr(paraName, "null");


            switch (str.ToLower())
            {

                case "null":
                    throw new Exception("" + paraName + "不是有效的布尔值!是否漏传?");
                    break;
                case "on":
                    return true;
                    break;
                case "off":
                    return false;

                case "true":
                    return true;
                    break;
                case "false":
                    return false;
                    break;
                case "0":
                    return false;
                    break;
                case "1":
                    return true;
                    break;
                case "undefined":
                    return false;
                    break;
                default:
                    break;
            }


            return Convert.ToBoolean(HttpContext.Current.Request.Params[paraName]);
        }


        public void ReThrow(Exception ex)
        {
            StringBuilder w = new StringBuilder();
            w.Append(" { \"re\":\"" + HttpUtility.UrlEncode(ex.Message.ToString()) + "\",\"reMsg\":\"" + HttpUtility.UrlEncode(ex.ToString()) + "\" } ");
       


            HttpContext.Current.Response.Write(w.ToString());

        }

        public void ReTrue()
        {
            StringBuilder w = new StringBuilder();
            w.Append(" { ");
            #region 检查json对象序列
            if (ReDict.Count == 0)
            {


            }
            else
            {

                foreach (KeyValuePair<string, object> item in ReDict)
                {

                    w.Append("\"" + item.Key + "\"");
                    w.Append(":");


                    w.Append(item.Value);


                    w.Append(",");
                }
            }
            #endregion

            #region 检查json文本序列
            if (ReDict2.Count == 0)
            {


            }
            else
            {

                foreach (KeyValuePair<string, string> item in ReDict2)
                {

                    w.Append("\"" + item.Key + "\"");
                    w.Append(":");

                    w.Append("\"");
                    w.Append(item.Value);
                    w.Append("\"");

                    w.Append(",");
                }
            }
            #endregion



            w.Append("  \"re\":\"ok\"  ");
            //w.Remove(w.Length - 1, 1);
            w.Append("}");

            HttpContext.Current.Response.Write(w.ToString());



        }

        public void ReTrue(string JsonStr)
        {
            HttpContext.Current.Response.Write(JsonStr);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
