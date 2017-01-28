using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Transactions;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using MongoDB.Bson;

namespace BLL
{
    public class StaticBLL
    {


        #region 产品处理




        public static DataTable GetProAllFatherDataTable(DataTable dt)
        {

            List<decimal> l = GetProAllFather(dt);

            DataSet dsAll = GetProClassCache();
            DataTable dtAll = dsAll.Tables[0];

            string selStr = "";
            if (l.Count == 0)
            {

                selStr = " 1=2 ";
            }
            else
            {
                selStr = "  ProductClassId in (" + string.Join(",", l) + ") ";
            }

            return Common.DataSetting.TableSelect(selStr, dtAll);
        }

        public static List<decimal> GetProAllFather(DataTable dt)
        {
            List<decimal> l = new List<decimal>();

            if (dt.Rows.Count == 0)
            {

                return l;
            }


            foreach (DataRow dr in dt.Rows)
            {
                decimal ProductClassId = decimal.Parse(dr["ProClassId"].ToString());

                foreach (decimal ProductClassId2 in GetProAllFather(ProductClassId))
                {
                    //添加到查询的父类别
                    l.Add(ProductClassId2);
                }


            }
            return l;
        }


        public static List<decimal> GetProAllBrother(decimal ProductClassId)
        {
            List<decimal> l = new List<decimal>();

            DataTable dt = GetProClassCache().Tables[0];
            DataTable dtCurrent = Common.DataSetting.TableSelect(" ProductClassId =" + ProductClassId + " ", dt);
            if (dtCurrent.Rows.Count == 0)
            {
                return l;
            }
            else
            {
                //l.Add(ProductClassId);
            }

            DataRow drCurrent = dtCurrent.Rows[0];
            decimal ParentProductClassId = decimal.Parse(drCurrent["ParentProductClassId"].ToString());
            DataTable dtBrotherProClass = Common.DataSetting.TableSelect(" ParentProductClassId=" + ParentProductClassId + " ", dt);  //取得所有兄弟节点
            if (dtBrotherProClass.Rows.Count > 0)
            {

                foreach (DataRow drBrotherProClass in dtBrotherProClass.Rows)
                {

                    l.Add(decimal.Parse(drBrotherProClass["ProductClassId"].ToString()));

                }
            }
            return l;

        }

        /// <summary>
        ///递归查询该ProductClassId节点上面的所有类别节点
        /// </summary>
        /// <param name="ProductClassId"></param>
        /// <returns></returns>
        public static List<decimal> GetProAllFather(decimal ProductClassId)
        {
            List<decimal> l = new List<decimal>();

            DataTable dt = GetProClassCache().Tables[0];
            DataTable dtCurrent = Common.DataSetting.TableSelect(" ProductClassId =" + ProductClassId + " ", dt);
            if (dtCurrent.Rows.Count == 0)
            {
                return l;
            }
            else
            {
                l.Add(ProductClassId);
            }
            DataRow drCurrent = dtCurrent.Rows[0];

            decimal ParentProductClassId = decimal.Parse(drCurrent["ParentProductClassId"].ToString());
            if (ParentProductClassId == 0)
            {
                //没有父类别
                return l;
            }
            else
            {

                DataTable dtParentProClass = Common.DataSetting.TableSelect(" ProductClassId=" + ParentProductClassId + " ", dt);
                if (dtParentProClass.Rows.Count > 0)
                {
                    foreach (decimal ProductClassId2 in GetProAllFather(ParentProductClassId))
                    {
                        //添加到查询的父类别
                        l.Add(ProductClassId2);
                    }
                }
                else
                { //没有查询到父类别


                }
            }

            return l;

        }

        //递归查询该ProductClassId节点下的所有类别节点
        public static List<decimal> GetProAllChildren(decimal ProductClassId)
        {

            List<decimal> l = new List<decimal>();
            l.Add(ProductClassId);
            DataTable dt = GetProClassCache().Tables[0];

            DataTable dtChild = Common.DataSetting.TableSelect(" ParentProductClassId =" + ProductClassId + " ", dt);
            if (dtChild.Rows.Count == 0)
            {
                //没有的话就我自己了



            }
            else
            {
                //如果还有的话

                foreach (DataRow drChild in dtChild.Rows)
                {

                    decimal CldProductClassId = decimal.Parse(drChild["ProductClassId"].ToString());
                    foreach (decimal c in GetProAllChildren(CldProductClassId))
                    {

                        l.Add(c);
                    }
                }

            }

            return l;
        }

        /// <summary>
        /// 取得公共部分广告
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAdPubCache()
        {

            return GetProAdByPageId("14082411414120216850");

        }

        public static DataSet dsPeiSongType(decimal MerId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("  select * from CORE.dbo.PeiSongType with(nolock) where MerId=" + MerId + " and SelDay>0 order by OrderNo desc  ");

            s.Append(" SELECT * FROM CORE.dbo.PeiSongTimeSolt pts   with(nolock) ");
            s.Append(" LEFT JOIN CORE.dbo.PeiSongType pt  with(nolock) ON pt.PeiSongTypeId = pts.PeiSongTypeId and pt.SelDay>0  ");
            s.Append(" WHERE MerId='" + MerId + "'  ");

            return BLL.StaticBLL.CacheData(s.ToString(), "PeiSongType", 3000);
        }


        /// <summary>
        /// 取得首页广告缓存
        /// </summary>
        /// <returns></returns>
        public static DataSet GetIndexAdCache()
        {

            return GetProAdByPageId("14071104192222047770");
        }


        /// <summary>
        /// 根据页面ID取得广告缓存
        /// </summary>
        /// <param name="PageId"></param>
        /// <returns></returns>
        public static DataSet GetProAdByPageId(string PageId)
        {


            StringBuilder s = new StringBuilder();
            s.Append(" select * FROM CORE.dbo.AdView with(nolock) where MerId=" + GetMerId(1000) + " AND Invalid=0  and PageId='" + PageId + "' order by OrderNo desc "); //首级类别


            return BLL.StaticBLL.CacheData(s.ToString(), "Ad_" + PageId + "", 8000);
        }




        /// <summary>
        /// 获得产品类别的缓存数据
        /// </summary>
        /// <returns></returns>
        public static DataSet GetProClassCache()
        {

            StringBuilder s = new StringBuilder();
            s.Append(" select * FROM dbo.ProductClass with(nolock) where MerchantId=" + GetMerId(1000) + " AND Invalid=0  order by orderno desc "); //首级类别


            return BLL.StaticBLL.CacheData(s.ToString(), "ProClass1", 8000);

        }

        public static DataSet GetProInfo(string ProId)
        {


            StringBuilder s = new StringBuilder();
            s.Append(" select * from CORE.dbo.ProView with(NOLOCK)  where ProId='" + ProId + "' ");

            s.Append(" select * from dbo.ProVsImgView with(NOLOCK)  where ProId='" + ProId + "' ");

            s.Append(" select top 8 ProName,ProTitle,ProId,ProductImgUrl,RePrice,RePrice2,RePrice3 from CORE.dbo.ProView  with(NOLOCK)  where ProClassId=(select top 1 ProClassId from dbo.Product with(NOLOCK) where ProId='" + ProId + "' ) and FlagInvalid=0 order by RecommendLv desc ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }


        /// <summary>
        /// 获得产品详情页广告缓存
        /// </summary>
        /// <returns></returns>
        public static DataSet GetProAd()
        {


            DataSet dsPro = BLL.StaticBLL.CacheData("SELECT * FROM CORE.dbo.AdView WITH(NOLOCK) WHERE PageId='14082411190326581186' and Invalid=0 ", "ProAd", 100);
            return dsPro;
        }

        #endregion

        #region 类别缓存

        /// <summary>
        /// 取得当前商家的全部类别缓存
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public static DataSet GetProClassCache(decimal MerId)
        {

            StringBuilder s = new StringBuilder();
            s.Append(" select * FROM dbo.ProductClass with(nolock) where MerchantId=" + MerId + " AND Invalid=0  order by orderno desc "); //首级类别


            return BLL.StaticBLL.CacheData(s.ToString(), "ProClass1", 8000);

        }

        #endregion

        #region 城镇
        public static DataSet GetTownList()
        {
            DataSet ds = CacheData(" SELECT  * FROM   CORE.dbo.Town with(nolock) where 1=1 order by OrderNo  ", "townList", 3000); ;


            return ds;

        }
        #endregion

        #region 统计数据

        /// <summary>
        /// 同步用户的统计数据
        /// </summary>
        /// <param name="MemberId"></param>
        public static void MemberNum(decimal MemberId)
        {


            StringBuilder s = new StringBuilder();

            s.Append(" DECLARE @DingDanNum AS INT  ,");
            s.Append(" @WshDingDanNum AS INT ,   ");
            s.Append(" @ShDingDanNum AS INT ,   ");
            s.Append(" @ThDingDanNum AS INT ,    ");
            s.Append(" @PingJiaNum AS INT ,    ");
            s.Append(" @XhJiFenNum AS DECIMAL,   ");
            s.Append(" @MemberId AS DECIMAL= " + MemberId + ", ");
            s.Append(" @LastBuyTime AS DATETIME ='1900-01-01' ");
            s.Append(" SET @DingDanNum=(SELECT COUNT(0) FROM dbo.DingDanInfo WITH(NOLOCK)  WHERE CreateMember=@MemberId);");
            s.Append(" SET @WshDingDanNum=(SELECT COUNT(0) FROM dbo.DingDanInfo   WITH(NOLOCK)  WHERE CreateMember=@MemberId AND Status<20);");
            s.Append(" SET @ShDingDanNum=(SELECT COUNT(0) FROM dbo.DingDanInfo WITH(NOLOCK)  WHERE CreateMember=@MemberId AND Status >=110 );");
            s.Append(" SET @ThDingDanNum=(SELECT COUNT(0) FROM dbo.DingDanInfo  WITH(NOLOCK) WHERE CreateMember=@MemberId AND Status >=200 );");
            s.Append(" SET @PingJiaNum=(SELECT COUNT(0) FROM dbo.PingJiaView  WITH(NOLOCK) WHERE MemberId =@MemberId AND Invalid=0   );");
            s.Append(" SET @XhJiFenNum=ISNULL((SELECT SUM(JiFenChangeNum) FROM dbo.JiFenChange WITH(NOLOCK)  WHERE MemberId=@MemberId AND JifenChangeTypeId= 2),0)*-1;");  //本来都是负数变成正数
            s.Append(" SET @LastBuyTime= ISNULL((select TOP 1 CreateTime FROM dbo.DingDanInfo WHERE CreateMember=@MemberId AND Status>=110),'1900-01-01'); ");
            s.Append(@" UPDATE  dbo.Member
        SET     DingDanNum = @DingDanNum ,
                WshDingDanNum = @WshDingDanNum ,
                ShDingDanNum = @ShDingDanNum ,
                ThDingDanNum = @ThDingDanNum ,
                PingJiaNum = @PingJiaNum ,
                XhJiFenNum = @XhJiFenNum,
                LastBuyTime = @LastBuyTime
        WHERE   MemberId = @MemberId ");

            DAL.DalComm.ExReInt(s.ToString());

        }


        #endregion

        #region 商家配置


        public static DataSet MerConfigData(decimal MerId)
        {
            DataSet ds = DAL.DalComm.BackData(" select * from CORE.dbo.MerConfig where MerId=" + MerId + " order by orderNo desc ");
            return ds;


        }

        public static Dictionary<string, string> MerConfig(decimal MerId)
        {
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            DataSet ds = MerConfigData(MerId);

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cfg.Add(dr["MerConfigId"].ToString(), dr["MerConfigVal"].ToString());
                }
            }

            return cfg;

        }

        /// <summary>
        /// 按照条件序列查询配置信息
        /// </summary>
        /// <param name="MerId"></param>
        /// <param name="MerConfigIds"></param>
        /// <returns></returns>
        public static Dictionary<string, string> MerConfig(decimal MerId, string[] MerConfigIds)
        {

            string MerConfigIdsStr = "'" + string.Join("','", MerConfigIds) + "'";
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            DataSet ds = DAL.DalComm.BackData(" select * from CORE.dbo.MerConfig with(nolock) where MerId=" + MerId + " and MerConfigId in (" + MerConfigIdsStr + ") order by orderNo desc ");
            DataTable dt = ds.Tables[0];
            if (MerConfigIds.Length > 0)
            {
                foreach (string MerConfigId in MerConfigIds)
                {

                    string val = "";
                    DataRow[] drs = dt.Select(" MerConfigId='" + MerConfigId + "' ");

                    if (drs.Length > 0)
                    {
                        val = drs[0]["MerConfigVal"].ToString();
                    }

                    cfg.Add(MerConfigId, val);

                }
                return cfg;
            }
            else
            {
                return cfg;
            }

        }


        public static string MerConfigJsonArray(decimal MerId)
        {
            DataSet ds = MerConfigData(MerId);
            return Common.JsonHelper.ToJson(ds.Tables[0]);

        }




        public static void ReThrow(Exception ex)
        {
            StringBuilder w = new StringBuilder();

            string re = HttpUtility.UrlEncode(ex.Message.ToString());

            w.Append(" { \"re\":\"" + re + "\",\"reMsg\":\"" + HttpUtility.UrlEncode(ex.ToString()) + "\" } ");
            #region 保存错误日志
            try
            {

                BsonDocument b = new BsonDocument();

                b["Msg"] = ex.Message;
                b["Con"] = ex.ToString();
                b["CreateTime"] = DateTime.Now;
                //b["TypeId"] = "BianJie";
                //b["TypeName"] = "边界层报错";

                #region 尝试获得UserId
                try
                {
                    b["UserId"] = Common.CookieSings.GetCurrentUserId();
                }
                catch (Exception)
                {

               
                }
                #endregion

                #region 尝试获得MemberId

                try
                {
                    string MemberId = Common.CookieSings.GetCookie("CurrentMemberId");
                    MemberId = Common.JiaMi.uncMe(MemberId);
                    b["MemberId"] = Convert.ToDecimal(MemberId);
                }
                catch (Exception)
                {

                    throw;
                }
                #endregion


                DAL.Mongo.Insert(b, "Error", "sysLog");


                //Model.ErroLogInfoModel model = new ErroLogInfoModel();
                //DAL.ErroLogInfoDAL dal = new DAL.ErroLogInfoDAL();

                //model.CreateTime = DateTime.Now;
                //model.ErroLogContent = ex.ToString();
                //model.Message = ex.Message;
                //try
                //{
                //    model.UserId = Common.CookieSings.GetCurrentUserId();
                //}
                //catch
                //{
                //}

                //try
                //{
                //    string MemberId = Common.CookieSings.GetCookie("CurrentMemberId");
                //    MemberId = Common.JiaMi.uncMe(MemberId);
                //    model.MemberId = decimal.Parse(MemberId);
                //}
                //catch
                //{

                //}
                //try
                //{
                //    dal.Add(model);
                //}
                //catch
                //{


                //}
            }
            catch
            {
                //如果存不上, 那就算了

            }
            #endregion




            HttpContext.Current.Response.Write(w.ToString());
        }

        /// <summary>
        /// 取得单条配置
        /// </summary>
        /// <param name="MerId"></param>
        /// <param name="MerConfigId"></param>
        /// <returns></returns>
        public static string MerOneConfig(decimal MerId, string MerConfigId, string CatchObj)
        {
            try
            {
                string str = DAL.DalComm.ExStr(" select  MerConfigVal from CORE.dbo.MerConfig where MerId=" + MerId + " and MerConfigId='" + MerConfigId + "' ");
                if (str.Trim() == "" || str == null)
                {
                    return CatchObj;
                }
                else
                {
                    return str;
                }
            }
            catch
            {

                return CatchObj;
            }


        }


        #endregion

        #region 应用程序池
        public static void AppPoolRecycleByMerId(decimal MerId)
        {
            DataSet ds = DAL.DalComm.BackData(" select * from CORE.dbo.AppPoolInfo where MerId=" + MerId + " ");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Common.AppPool.Recycle(dr["AppPoolName"].ToString());

                }

            }

        }


        #endregion

        #region 缓存

        public static DataSet PayType()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.PayType WITH(NOLOCK) WHERE Invalid=0 ORDER BY OrderNO DESC ");

            DataSet dsPay = BLL.StaticBLL.CacheData(s.ToString(), "PayType", 3000);
            return dsPay;
        }


        /// <summary>
        /// 商家配置缓存, 在后台管理中也可以使用!
        /// </summary>
        /// <param name="MerId"></param>
        /// <param name="CacheMinutes"></param>
        /// <returns></returns>
        public static Dictionary<string, string> MerConfigCache(decimal MerId, int CacheMinutes)
        {
            Dictionary<string, string> MerConfig = new Dictionary<string, string>();

            if (HttpContext.Current.Cache[MerId + "MerConfig"] == null)
            {
                MerConfig = BLL.StaticBLL.MerConfig(MerId);
                HttpContext.Current.Cache.Insert(MerId + "MerConfig", MerConfig, null, DateTime.Now.AddMinutes(CacheMinutes), TimeSpan.Zero);
            }
            else
            {
                MerConfig = (Dictionary<string, string>)HttpContext.Current.Cache[MerId + "MerConfig"];
            }
            return MerConfig;
        }


        /// <summary>
        /// 商家ID缓存, 后台管理中严禁使用!
        /// </summary>
        /// <param name="CacheMinutes"></param>
        /// <returns></returns>
        public static decimal GetMerId(int CacheMinutes)
        {
            decimal MerId = 0;

            if (HttpContext.Current.Cache["MerId"] == null)
            {
                MerId = decimal.Parse(Common.Config.GetAppValue("MerId"));
                HttpContext.Current.Cache.Insert("MerId", MerId, null, DateTime.Now.AddMinutes(CacheMinutes), TimeSpan.Zero);
            }
            else
            {
                MerId = decimal.Parse(HttpContext.Current.Cache["MerId"].ToString());
            }
            return MerId;
        }


        public static DataSet GetMer()
        {
            decimal MerId = GetMerId(1000);
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.Merchantview WITH(NOLOCK) WHERE MerchantId = '" + MerId + "'  ");

            DataSet ds = CacheData(s.ToString(), "Mer", 3000);

            return ds;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static DataSet CacheData(string sqlStr, string CacheKey, int CacheMinutes)
        {

            DataSet dsIndex = (DataSet)HttpContext.Current.Cache[CacheKey];

            if (dsIndex == null)
            {
                dsIndex = DAL.DalComm.BackData(sqlStr);
                dsIndex.ExtendedProperties.Add("ds_" + CacheKey + "_time", DateTime.Now.ToLongTimeString());
                HttpContext.Current.Cache.Insert(CacheKey, dsIndex, null, DateTime.Now.AddMinutes(CacheMinutes), TimeSpan.Zero);
            }
            else
            {

            }
            return dsIndex;
        }

        public static string CacheStr(string CacheKey)
        {
            if (HttpContext.Current.Cache[CacheKey] == null)
            {
                return "";
            }

            return HttpContext.Current.Cache[CacheKey].ToString();

        }

        public static string ChangeCaCheStr(string str, string CacheKey, int CacheMinutes)
        {
            try
            {
                HttpContext.Current.Cache.Insert(CacheKey, str, null, DateTime.Now.AddMinutes(CacheMinutes), TimeSpan.Zero);
                return str;
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }

        public static void ClearCache(string CacheKey)
        {
            HttpContext.Current.Cache.Remove("ds_" + CacheKey + "_time");

        }

        #endregion

        #region 短信

        public static string SendStMsg(Model.StMsgModel model)
        {
            string str = string.Empty;

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                if (model.MerId == 0)
                {

                    model.MerId = GetMerId(1000);
                }
                model.CreateTime = DateTime.Now;

                #region 判断信息合法性
                int i;


                i = DAL.DalComm.ExInt(" SELECT COUNT(0) FROM DBLOG.dbo.StMsg WHERE PhoneNo='" + model.PhoneNo + "' and CreateTime BETWEEN '" + model.CreateTime.AddMilliseconds(-50) + "' and '" + model.CreateTime + "' ");
                if (i > 0)
                {
                    throw new Exception("一个号码一分钟内只能接收一条短信!");
                }

                i = DAL.DalComm.ExInt(" SELECT COUNT(0) FROM DBLOG.dbo.StMsg WHERE PhoneNo='" + model.PhoneNo + "' and CreateTime BETWEEN '" + model.CreateTime.AddMilliseconds(-600) + "' and '" + model.CreateTime + "' ");

                if (i >= 10)
                {
                    throw new Exception("一个小时内不能接收" + i + "条信息!");

                }
                switch (Convert.ToInt32(model.StMsgTypeId))
                {

                    case 1: //是注册验证码信息

                        i = DAL.DalComm.ExInt(" select count(0) from dbo.Member with(nolock) where MerId='" + model.MerId + "' and Phone='" + model.PhoneNo + "' ");
                        if (i > 0)
                        {

                            throw new Exception("手机号" + model.PhoneNo + "已被注册!");
                        }
                        break;

                    case 2://是找回密码验证信息

                        i = DAL.DalComm.ExInt(" select count(0) from dbo.Member with(nolock) where MerId='" + model.MerId + "' and Phone='" + model.PhoneNo + "' ");
                        if (i <= 0)
                        {

                            throw new Exception("手机号" + model.PhoneNo + "没有注册!");
                        }


                        break;


                    case 10: //绑定银行卡
                        break;
                    default:
                        break;
                }


                #endregion

                #region 首先写入数据

                DAL.StMsgDAL dal = new DAL.StMsgDAL();
                model.Ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                dal.Add(model);

                #endregion

                if (model.MerId == 0)
                {

                    throw new Exception("MerId不能为0!");
                }

                DataSet ds_Mer = DAL.DalComm.BackData(" select * from dbo.MerchantView with(nolock) where MerchantId='" + model.MerId + "' ");
                DataTable dt_Mer = ds_Mer.Tables[0];
                if (dt_Mer.Rows.Count == 0)
                {
                    throw new Exception("没有MerId为" + model.MerId + "的商家!");

                }
                DataRow dr_Mer = dt_Mer.Rows[0];
                #region 螺丝帽短信接口
                string mobile = model.PhoneNo,
                       message = model.StMsgContent + "【优做】",
                       username = "api",
                       password = "key-936408119688c706fe1b8b4391c59ea8",
                       url = "http://sms-api.luosimao.com/v1/send.json";

                byte[] byteArray = Encoding.UTF8.GetBytes("mobile=" + mobile + "&message=" + message);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(username + ":" + password));
                webRequest.Headers.Add("Authorization", auth);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string Message = php.ReadToEnd();

                System.Console.Write(Message);
                System.Console.Read();


                #endregion


                #region 中国短信网短信接口
                //string sendurl = "http://smsapi.c123.cn/OpenPlatform/OpenApi";
                //string ac = "1001@500975210003";			//用户名
                //string authkey = "BAA5CDEDA20461E4178C818C8C432A42";	//密钥
                //string cgid = "5246";  //通道组编号
                //string csid = "1";//签名编号 ,可以为空时，使用系统默认的编号
                //string m = model.PhoneNo;  //发送号码
                //string c = "" + model.StMsgContent;
                //string action = "sendOnce";
                //StringBuilder sbTemp = new StringBuilder();
                ////POST 传值
                //sbTemp.Append("action=" + action + "&ac=" + ac + "&authkey=" + authkey + "&m=" + m + "&cgid=" + cgid + "&csid=" + csid + "&c=" + c);
                //byte[] bTemp = System.Text.Encoding.GetEncoding("utf-8").GetBytes(sbTemp.ToString());
                //string postReturn = doPostRequest(sendurl, bTemp);
                //Regex linkReg = new Regex("result=(.+)/>");
                //MatchCollection linkCollection = linkReg.Matches(postReturn);
                //str = linkCollection[0].Groups[1].Value;
                //str = str.Replace(">", "");
                #endregion


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
            return ("Post response is:" + str.Replace("\"", "")); //测试返回结果

        }

        //POST方式发送得结果
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }
        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }


        #endregion


    }
}
