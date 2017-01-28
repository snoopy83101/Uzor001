﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
using LitJson;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;

namespace Common
{
    public class BPageSetting2
    {



        public List<Model.ReListModel> ReList = new List<Model.ReListModel>();
        public Dictionary<string, object> ReDict = new Dictionary<string, object>();
        public Dictionary<string, string> ReDict2 = new Dictionary<string, string>();




        public DataTable ReTable(string paraName)
        {
            try
            {
                return DataSetting.CXmlToDatatTable(ReStr(paraName));
            }
            catch (Exception)
            {
                return new DataTable();
            }

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

            return Common.PageInput.ReTimeSpan(str, cat);
        }

        public string ReStr(string paraName, string CatchStr)
        {

            return Common.PageInput.ReStr(paraName, CatchStr);
        }

        public ObjectId ReObjId(string paraName, string CatchStr)
        {
            return Common.PageInput.ReObjId(paraName, CatchStr);


        }

        public JObject ReJson(string paraName, JObject j = null)
        {
            if (j == null)
            {
                return Common.PageInput.ReJson(paraName);
            }
            else
            {
                return Common.PageInput.ReJson(paraName, j);
            }

        }

        public BsonDocument ReBson(string paraName, BsonDocument cb = null)
        {
            return Common.PageInput.ReBson(paraName, cb);

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

        public void RePage(JObject j)
        {
            try
            {
                int totalPage = (int)j["TotalPage"];  //最多页数
                int RowCount = (int)j["RowCount"];


                ReDict2.Add("t", totalPage.ToString());
                ReDict2.Add("r", RowCount.ToString());
                ReDict.Add("list", j["list"].ToString());

                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }

        }

        /// <summary>
        /// 后面不要再跟ReTrue! 不然完蛋!
        /// </summary>
        /// <param name="ds"></param>
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

            string re = HttpUtility.UrlEncode(ex.Message.ToString());

            w.Append(" { \"re\":\"" + re + "\",\"reMsg\":\"" + HttpUtility.UrlEncode(ex.ToString()) + "\" } ");





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


                    w.Append(item.Value.ToString());


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

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://jk.lituo001.com");
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Write(w.ToString());

            //  HttpContext.Current.Response.End();

        }


        public void ReTrue(JsonData j)
        {

            j["re"] = "ok";

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://jk.lituo001.com");
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Write(j.ToJson());

        }

        public void ReTrue(JObject j)
        {

            j["re"] = "ok";

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://jk.lituo001.com");
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Write(j.ToString());

        }

        public void ReTrue(string JsonStr)
        {
            HttpContext.Current.Response.Write(JsonStr);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
