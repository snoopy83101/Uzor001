using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;
using System.Text;
using System.Xml;
using Common;
using System.IO;
using LitJson;
using System.Web;
namespace BPage
{
    public class Comm : Common.BPageSetting2
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string para = ReStr("para");
                switch (para)
                {

                    case "GetAddressByAreaId":
                        GetAddressByAreaId();
                        break;


                    case "DownLoadApp":
                        DownLoadApp();
                        break;


                    case "GetProcessLvContacts":
                        GetProcessLvContacts();
                        break;

                    #region 语音

                    case "GetSoundReadUrl":
                        GetSoundReadUrl();
                        break;

                    case "GetSoundToken":
                        GetSoundToken();
                        break;


                    #endregion

                    #region 地理位置

                    case "GetProvince":
                        GetProvince();
                        break;

                    case "GetCity":
                        GetCity();
                        break;
                    case "GetArea":
                        GetArea();
                        break;
                    #endregion

                    #region 消息

                    case "PushMsg":
                        PushMsg();
                        break;

                    case "GetMsgText":
                        GetMsgText();
                        break;
                    case "GetMsgList":
                        GetMsgList();
                        break;

                    case "SaveMsgReadLog":
                        SaveMsgReadLog();
                        break;

                    case "SaveMsg":
                        SaveMsg();
                        break;
                    case "SaveMsgText":
                        SaveMsgText();
                        break;
                    case "GetNewMsgNum":
                        GetNewMsgNum();
                        break;

                    case "ReadMsg":
                        ReadMsg();
                        break;
                    case "GetMyMsgList":
                        GetMyMsgList();
                        break;
                    #endregion

                    #region 建议

                    case "GetJianYiPageList":
                        GetJianYiPageList();
                        break;
                    case "SaveJianYi":
                        SaveJianYi();
                        break;
                    #endregion


                    #region 返回登录域
                    case "LoginDomain":
                        LoginDomain();
                        break;

                    #endregion

                    #region 项目特定

                    #region 便民电话
                    case "DelBmTel":
                        DelBmTel();
                        break;
                    case "SaveBmTel":
                        SaveBmTel();
                        break;

                    case "GetBmTelInfo":
                        GetBmTelInfo();
                        break;

                    case "GetBmTelTypeList":
                        GetBmTelTypeList();     //便民电话类别
                        break;

                    case "GetBmTelPageList":
                        GetBmTelPageList();
                        break;
                    case "GetBmTelList":           //便民电话列表
                        GetBmTelList();
                        break;
                    #endregion

                    #region 首页

                    case "GetAppIndex":
                        GetAppIndex();
                        break;

                    #endregion

                    #endregion

                    #region 地理位置

                    case "GetZoneInfo":
                        GetZoneInfo();
                        break;

                    case "GetZoneList":
                        GetZoneList();
                        break;


                    case "DelBranchVsSiteOrder":
                        DelBranchVsSiteOrder();
                        break;

                    case "ChangeBranchVsSiteOrder":
                        ChangeBranchVsSiteOrder();
                        break;

                    case "GetBranchVsSiteList":
                        GetBranchVsSiteList();
                        break;


                    case "BranchVsSiteBind":
                        BranchVsSiteBind();
                        break;


                    case "GetNearbySite":
                        GetNearbySite();
                        break;

                    case "GetSiteInfo":
                        GetSiteInfo();
                        break;
                    case "GetSitePageList":
                        GetSitePageList();
                        break;


                    case "GetSiteList":
                        GetSiteList();
                        break;
                    case "SaveSite":
                        SaveSite();
                        break;
                    case "SaveSiteDetail":
                        SaveSiteDetail();
                        break;

                    case "DelSiteDetail":
                        DelSiteDetail();
                        break;

                    #endregion

                    #region 定位

                    case "DeviceLocation":
                        DeviceLocation();
                        break;


                    #endregion

                    #region 上传文件
                    case "FilePost":


                        FilePost();
                        break;
                    #endregion


                    #region 静态化

                    case "ToDefaultHtml":
                        ToDefaultHtml();  //首页静态化
                        break;


                    #endregion


                    #region 短信

                    case "SendStMsg":
                        SendStMsg();
                        break;
                    case "GetStMsgList":
                        GetStMsgList();
                        break;


                    #endregion
                    case "GetPageList":
                        GetPageList();
                        break;
                    case "AppPoolRecycleByMerId":
                        AppPoolRecycleByMerId();
                        break;

                    case "SelInput":  //筛选文本框
                        SelInput();
                        break;

                    case "InvalidAd":
                        InvalidAd();
                        break;

                    case "SaveAd":
                        SaveAd();  // 添加一条广告信息
                        break;


                    case "ChangeLocationOrderNo":
                        ChangeLocationOrderNo();
                        break;

                    case "GetLocationList":
                        GetLocationList();
                        break;

                    case "ShuaXin":

                        ShuaXin();
                        break;

                    case "SerachIndexDataList":
                        SerachIndexDataList();
                        break;

                    case "SaveIndexDataInfo":
                        SaveIndexDataInfo();
                        break;


                    case "GetRemindNum":
                        GetRemindNum();
                        break;

                    case "LookMyRemind":
                        LookMyRemind();
                        break;

                    case "GetRemindPageList":
                        GetRemindPageList();
                        break;

                    case "MyRepComment":
                        MyRepComment();        //获得我的
                        break;
                    case "GetCommentUrl":
                        GetCommentUrl();
                        break;

                    case "MyComment":
                        MyComment();          //我的点评信息
                        break;


                    case "GetCommentRepList":
                        GetCommentRepList();
                        break;

                    case "GetNewRemind":
                        GetNewRemind();
                        break;

                    case "ExPwd":
                        ExPwd();
                        break;

                    case "ExHasUserIdOrEmail":
                        ExHasUserIdOrEmail();   //检查用户名或邮箱是否存在
                        break;

                    case "ExUserId":   //检查用户名是否可以注册
                        ExUserId();
                        break;
                    case "ExMail":
                        ExMail();   //检查邮箱是否可以注册
                        break;

                    case "GetCommentRepPageList":
                        GetCommentRepPageList();
                        break;

                    case "GetCommentList":
                        GetCommentList();
                        break;
                    case "AddMerAttention":
                        AddMerAttention();   //加商家关注
                        break;


                    case "DelMerAttention":
                        DelMerAttention();   //取消商家关注
                        break;


                    case "AddUserAttention":
                        AddUserAttention();   //加用户关注
                        break;


                    case "DelUserAttention":
                        DelUserAttention();   //取消用户关注
                        break;


                    case "SaveComment":

                        SaveComment();   //保存点评
                        break;

                    case "DownPicture":
                        DownPicture();
                        break;

                    case "CropPicture":
                        CropPicture();  //剪裁图片
                        break;

                    case "DelImageById":
                        DelImageById();
                        break;

                    case "DelImageByUrl":
                        DelImageByUrl();
                        break;
                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();





        }

        private void GetAddressByAreaId()
        {

            string AreaId = ReStr("AreaId", "");

            if (AreaId == "")
            {
                throw new Exception("AreaId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" DECLARE @AreaId AS VARCHAR(50)='" + AreaId + "' ");
            s.Append(" DECLARE @CityId AS VARCHAR(50) =(SELECT TOP 1 CityId FROM dbo.Area WHERE AreaId=@AreaId); ");
            s.Append(" DECLARE @ProvinceId AS VARCHAR(50)=(SELECT TOP 1  ProvinceId FROM dbo.City WHERE CityId=@CityId); ");
            s.Append(" SELECT * FROM  dbo.Province  ");
            s.Append(" SELECT * FROM  dbo.City WHERE ProvinceId= @ProvinceId ");
            s.Append(" SELECT * FROM  dbo.Area WHERE CityId=@CityId ");
            s.Append(" SELECT @AreaId AS AreaId ,@CityId AS CityId,@ProvinceId AS ProvinceId ");



            DataSet ds = DAL.DalComm.BackData(s.ToString());

            ReDict.Add("Province", JsonHelper.ToJson(ds.Tables[0]));
            ReDict.Add("City", JsonHelper.ToJson(ds.Tables[1]));
            ReDict.Add("Area", JsonHelper.ToJson(ds.Tables[2]));
            ReDict.Add("Current", JsonHelper.ToJsonNo1(ds.Tables[3]));
            ReTrue();
        }

        private void DownLoadApp()
        {
            System.Net.WebClient wc = new System.Net.WebClient();

            string AppUrl = ReStr("AppUrl", "");



            var src = "/upload/uzjob" + DateTime.Now.ToString("MMddHHmmss") + ".apk";

            var path = System.Web.HttpContext.Current.Server.MapPath(src);
            wc.DownloadFile(AppUrl, path);


            JsonData j = new JsonData();
            j["AppUrl"] = "http://" + HttpContext.Current.Request.Url.Authority + src;
            ReTrue(j);
        }

        private void GetProcessLvContacts()
        {

            var MerConfig = BLL.StaticBLL.MerConfig(1999);

            ReDict2.Add("ProcessLvContacts", MerConfig["ProcessLvContacts"]);
            ReDict2.Add("ProcessLvAddress", MerConfig["ProcessLvAddress"]);
            ReDict2.Add("ProcessLvTel", MerConfig["ProcessLvTel"]);

            ReTrue();

        }

        private void GetSoundReadUrl()
        {
            string ReadStr = ReStr("ReadStr", "");
            if (ReadStr == "")
            {
                throw new Exception("不能为空!");
            }
            BLL.CommBLL bll = new BLL.CommBLL();
            string ReadUrl = bll.GetSoundReadUrl(ReadStr);

            ReDict2.Add("ReadUrl", ReadUrl);

            ReTrue();




        }

        private void GetSoundToken()
        {





        }

        private void GetArea()
        {
            StringBuilder s = new StringBuilder();

            string CityId = ReStr("CityId", "");
            if (CityId == "")
            {
                throw new Exception("CityId不能为空!");
            }
            s.Append(" SELECT *  FROM dbo.area WITH(nolock) where CityId='" + CityId + "' ");
            DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetCity()
        {
            StringBuilder s = new StringBuilder();
            string ProvinceId = ReStr("ProvinceId", "");
            if (ProvinceId == "")
            {
                throw new Exception("ProvinceId不能为空!");
            }
            s.Append(" SELECT *  FROM dbo.city WITH(nolock) where ProvinceId='" + ProvinceId + "' ");
            DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetProvince()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT *  FROM dbo.province WITH(nolock) ");
            DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void GetZoneInfo()
        {
            StringBuilder s = new StringBuilder();

            decimal ZoneId = ReDecimal("ZoneId", 0);

            if (ZoneId == 0)
            {
                throw new Exception("ZoneId不能为0!");
            }

            s.Append(" SELECT * FROM Zone WHERE ZoneId =" + ZoneId + " ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            ReDict.Add("info", JsonHelper.ToJsonNo1(ds));

            ReTrue();

        }

        private void GetSiteList()
        {
            string SiteName = ReStr("SiteName", "");
            if (SiteName == "")
            {

                throw new Exception("小区名称关键词不能为空!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT SiteId,SiteName FROM dbo.SiteView WITH(NOLOCK) WHERE SiteName LIKE '%" + SiteName + "%'");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];


            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();


        }

        private void PushMsg()
        {

            decimal MsgId = ReDecimal("MsgId", 0);
            decimal MerId = ReDecimal("MerId", 0);
            string ZoneId = ReStr("ZoneId", "");
            if (MerId == 0)
            {
                throw new Exception("MerId不能为空!");
            }

            if (MsgId == 0)
            {
                throw new Exception("Msg不能为0!");
            }

            StringBuilder s = new StringBuilder();
            s.Append("  SELECT * FROM YYHD.dbo.MyMsgView WHERE MsgId='" + MsgId + "' ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());


            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];

                BLL.JPushBLL jbll = new BLL.JPushBLL();


                string[] tag = new string[] { "ZoneId_" + ZoneId + "" };

                if (ZoneId == "")
                {
                    tag = null;
                }

                jbll.SendPush(dr["MsgTitle"].ToString(), dr["TargetDeviceId"].ToString(), "", MerId, tag);



            }
            else
            {
                throw new Exception("没有找到MsgId为" + MsgId + "的推送");
            }



            ReTrue();

        }

        private void GetMsgText()
        {
            StringBuilder s = new StringBuilder();

            decimal MsgTextId = ReDecimal("MsgTextId", 0);

            if (MsgTextId == 0)
            {
                throw new Exception("MsgTextId不能为0!");
            }
            s.Append(" SELECT * FROMDBMSG.dbo.MsgTextView WHERE MsgTextId='" + MsgTextId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            ReDict.Add("info", JsonHelper.ToJsonNo1(ds));
            ReTrue();
        }

        private void GetMsgList()
        {
            decimal MemberId = ReDecimal("MemberId", 0);
            string MsgType = ReStr("MsgType", "");


            StringBuilder s = new StringBuilder();

            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc ");
            DAL.MsgDAL dal = new DAL.MsgDAL();

            s.Append(" 1=1 ");


            if (MsgType != "")
            {
                s.Append(" and MsgType='" + MsgType + "' ");
            }



            if (MemberId != 0)
            {
                s.Append(" and TargetDeviceId='" + MemberId + "' or TargetDeviceId='' ");
            }
            s.Append(" GROUP BY " + col + " ");
            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);

            RePage2(ds);
        }

        private void SaveMsgReadLog()
        {
            Model.MsgReadLogModel model = new Model.MsgReadLogModel();
            model.DeviceId = ReStr("DeviceId", "");
            model.MsgTextId = ReDecimal("MsgTextId", 0);
            model.CreateTime = DateTime.Now;
            DAL.MsgReadLogDAL dal = new DAL.MsgReadLogDAL();
            dal.DeleteList(" DeviceId='" + model.DeviceId + "' and MsgTextId='" + model.MsgTextId + "'  ");
            dal.Add(model);
            ReTrue();
        }

        private void GetMyMsgList()
        {
            decimal MemberId = ReDecimal("MemberId", 0);

            if (MemberId == 0)
            {

                throw new Exception("MemberId不能为0");
            }

            StringBuilder s = new StringBuilder();

            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc ");
            DAL.MsgDAL dal = new DAL.MsgDAL();






            s.Append(" TargetDeviceId='" + MemberId + "' or TargetDeviceId='' ");
            s.Append(" GROUP BY " + col + " ");
            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);

            RePage2(ds);

        }

        private void ReadMsg()
        {
            Model.MsgReadLogModel model = new Model.MsgReadLogModel();
            model.DeviceId = ReStr("DeviceId", "");
            model.MsgTextId = ReDecimal("MsgTextId", 0);
            model.CreateTime = ReTime("CreateTime", DateTime.Now);
            DAL.MsgReadLogDAL dal = new DAL.MsgReadLogDAL();
            dal.DeleteList(" DeviceId='" + model.DeviceId + "' and  MsgTextId='" + model.MsgTextId + "' ");
            dal.Add(model);

            ReTrue();
        }

        private void GetNewMsgNum()
        {
            var DeviceId = ReStr("DeviceId", "");
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT  ISNULL(SUM(条数), 0) AS 未读条数 ");
            s.Append(" FROM    ( SELECT    1 AS 条数 ");
            s.Append("  FROM      YYHD.dbo.MyMsgView ");
            s.Append(" WHERE     ( TargetDeviceId = '" + DeviceId + "' ");
            s.Append(" OR TargetDeviceId = '' ");
            s.Append(" ) ");
            s.Append(" AND EndTime > GETDATE() ");
            s.Append(" AND 是否已读 = '未读' ");
            s.Append(" GROUP BY  MsgTextId ");
            s.Append(" ) a ");
            s.Append("  ");

            int Num = DAL.DalComm.ExInt(s.ToString());

            ReDict2.Add("Num", Num.ToString());
            ReTrue();

        }

        private void SaveMsgText()
        {
            Model.MsgTextModel model = new Model.MsgTextModel();

            model.MsgTextId = ReDecimal("MsgTextId", 0);
            model.MsgTitle = ReStr("MsgTitle", "");
            model.MsgContent = HttpContext.Current.Server.UrlDecode(ReStr("MsgContent", ""));
            model.MsgType = ReStr("MsgType", "");
            model.CreateTime = ReTime("CreateTime", DateTime.Now);
            model.EndTime = ReTime("EndTime", DateTime.Now.AddDays(3));
            model.Extra = ReStr("Extra", "{}");

            DAL.MsgTextDAL dal = new DAL.MsgTextDAL();
            if (model.MsgTextId == 0)
            {

                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
            ReDict2.Add("MsgTextId", model.MsgTextId.ToString());
            ReTrue();

        }

        private void SaveMsg()
        {

            Model.MsgModel model = new Model.MsgModel();
            model.MsgId = ReDecimal("MsgId", 0);
            model.SendDeviceId = ReStr("SendDeviceId", "");
            model.TargetDeviceId = ReStr("TargetDeviceId", "");
            model.MsgTextId = ReDecimal("MsgTextId", 0);
            model.ZoneId = ReStr("ZoneId", "").Trim();

            if (model.ZoneId == "0")
            {
                model.ZoneId = "";
            }

            DAL.MsgDAL dal = new DAL.MsgDAL();

            dal.DeleteList(" SendDeviceId='" + model.SendDeviceId + "' and TargetDeviceId='" + model.TargetDeviceId + "' and MsgTextId='" + model.MsgTextId + "' and ZoneId='" + model.ZoneId + "' ");

            dal.Add(model);

            ReDict2.Add("MsgId", model.MsgId.ToString());

            ReTrue();
        }

        private void GetJianYiPageList()
        {
            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc");
            DAL.JianYiDAL dal = new DAL.JianYiDAL();
            StringBuilder s = new StringBuilder();

            DateTime CreateTime1 = ReTime("CreateTime1");
            DateTime CreateTime2 = ReTime("CreateTime2");
            CreateTime2.AddDays(1);
            s.Append(" 1=1 ");

            s.Append(" and CreateTime  BETWEEN '" + CreateTime1 + "' AND  '" + CreateTime2 + "' ");
            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);

            RePage2(ds);
        }

        private void SaveJianYi()
        {
            Model.JianYiModel model = new Model.JianYiModel();
            model.JianYiId = ReDecimal("JianYiId", 0);
            model.CreateTime = ReTime("CreateTime", DateTime.Now);
            model.JianYiTitle = ReStr("JianYiTitle", "");
            model.JianYiContent = ReStr("JianYiContent", "");
            model.MemberId = ReDecimal("MemberId", 0);


            if (model.MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }

            DAL.JianYiDAL dal = new DAL.JianYiDAL();
            if (model.JianYiId == 0)
            {
                dal.Add(model);
            }
            else
            {

                dal.Update(model);
            }


            ReTrue();


        }

        private void GetNearbySite()
        {
            decimal Lng = ReDecimal("Lng", 0);
            decimal Lat = ReDecimal("Lat", 0);
            string SiteName = ReStr("SiteName", "").Trim();
            StringBuilder s = new StringBuilder();




            s.Append(" SELECT TOP 10 * FROM  dbo.SiteView with(nolock) where 1=1");


            if (SiteName != "")
            {
                s.Append(" and SiteName like '%" + SiteName + "%' ");
            }
            else
            {

                s.Append(" and SiteLng BETWEEN " + Lng + "-0.01 and " + Lng + "+0.01 and SiteLat BETWEEN " + Lat + "-0.01 and " + Lat + "+0.01  ");
            }
            string Lng加减 = "SiteLng-" + Lng + "";
            if (Lng < 0)
            {
                Lng = -Lng;

                Lng加减 = "SiteLng+" + Lng + "";
            }
            string Lat加减 = "SiteLat-" + Lat + "";
            if (Lat < 0)
            {

                Lat = -Lat;
                Lat加减 = "SiteLat+" + Lat + "";
            }


            s.Append(" ORDER BY SQUARE(" + Lng加减 + ")+SQUARE(" + Lat加减 + ")");



            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }



        private void GetZoneList()
        {
            DataSet ds = DAL.DalComm.BackData("SELECT * FROM dbo.Zone where Invalid=0 ");

            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();


        }

        private void DelBmTel()
        {
            decimal BmTelId = ReDecimal("BmTelId", 0);
            if (BmTelId == 0)
            {
                throw new Exception("BmTelId不能为0!");
            }
            DataSet ds = DAL.DalComm.BackData("delete  FROM dbo.BmTel WHERE BmTelId=" + BmTelId + "");

            ReTrue();
        }

        private void GetBmTelInfo()
        {
            decimal BmTelId = ReDecimal("BmTelId", 0);
            if (BmTelId == 0)
            {
                throw new Exception("BmTelId不能为0!");
            }

            DataSet ds = DAL.DalComm.BackData("SELECT * FROM dbo.BmTel WITH(NOLOCK) WHERE BmTelId=" + BmTelId + "");
            ReDict.Add("info", JsonHelper.ToJsonNo1(ds.Tables[0]));
            ReTrue();

        }

        private void SaveBmTel()
        {
            Model.BmTelModel model = new Model.BmTelModel();

            model.BmTelId = ReDecimal("BmTelId", 0);

            DAL.BmTelDAL dal = new DAL.BmTelDAL();
            if (model.BmTelId != 0)
            {
                model = dal.GetModel(model.BmTelId);
            }

            model.BmTelNo = ReStr("BmTelNo", "");
            model.BmTelTitle = ReStr("BmTelTitle", "");
            model.BmTelTypeId = ReInt("BmTelTypeId", 0);
            model.BmTelMemo = ReStr("BmTelMemo", "");
            model.OrderNo = ReInt("OrderNo", 0);

            if (model.BmTelId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
            ReTrue();
        }

        private void LoginDomain()
        {
            decimal MerId = ReDecimal("MerId", 0);
            switch (MerId.ToString())
            {
                case "1646":  //世纪东方超市
                    ReDict2.Add("LoginDomain", "http://system.sjdfcs.com");

                    break;

                case "1666":  //优易送
                    ReDict2.Add("LoginDomain", "http://sysyys.lituo001.com");
                    break;
                default:
                    throw new Exception("MerId不能为" + MerId + "");



            }

            ReTrue();
        }

        private void GetBmTelList()
        {

            int BmTelTypeId = ReInt("BmTelTypeId", 0);

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.BmTel WITH(NOLOCK) where ");
            s.Append("  1=1 ");
            if (BmTelTypeId != 0)
            {
                s.Append(" and BmTelTypeId='" + BmTelTypeId + "' ");
            }
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetBmTelPageList()
        {
            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", "BmTelId desc");
            DAL.BmTelDAL dal = new DAL.BmTelDAL();
            StringBuilder s = new StringBuilder();
            int BmTelTypeId = ReInt("BmTelTypeId", 0);
            s.Append(" 1=1 ");
            if (BmTelTypeId != 0)
            {
                s.Append(" and BmTelTypeId=" + BmTelTypeId + " ");
            }
            s.Append(" Group by " + col + " ");

            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);

            RePage2(ds);


        }

        private void GetBmTelTypeList()
        {
            DataSet ds = DAL.DalComm.BackData(" SELECT * FROM dbo.BmTelType with(nolock) ");

            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();

        }

        //获取广告页面列表, 可以根据分部
        private void GetPageList()
        {
            Model.PageInfoModel model = new Model.PageInfoModel();
            model.BranchId = ReStr("BranchId", "");
            model.ZoneId = ReStr("ZoneId", "");
            model.MerId = ReDecimal("MerId", 0);
            model.Invalid = ReBool("Invalid", false);
            StringBuilder s = new StringBuilder();

            s.Append("  select * from CORE.dbo.PageInfo with(nolock) where ");
            if (model.MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            s.Append(" 1=1 ");

            s.Append(" and MerId=" + model.MerId + " ");
            s.Append(" and Invalid='" + model.Invalid + "' ");
            //if (model.BranchId != "")
            //{
            //    s.Append(" and BranchId='" + model.BranchId + "'  ");
            //}

            if (model.ZoneId != "")
            {
                s.Append(" and ZoneId='" + model.ZoneId + "' ");
            }
            else
            {
                throw new Exception("ZoneId不能为空!");
            }
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetAppIndex()
        {

            decimal MerId = ReDecimal("MerId", 0);
            string BranchId = ReStr("BranchId", "");
            string ZoneId = ReStr("ZoneId", "");
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }



            StringBuilder s = new StringBuilder();
            s.Append(" SELECT  * From CORE.dbo.AdView WITH ( NOLOCK ) where  PageLabel = 'appShouYe' and MerId=" + MerId + " and Invalid=0 ");
            if (BranchId != "")
            {
                s.Append("      AND BranchId = '" + BranchId + "' ");
            }

            if (ZoneId != "")
            {

                s.Append(" and ZoneId='" + ZoneId + "' ");
            }

            s.Append("  ORDER BY OrderNo DESC ");

            s.Append(" SELECT  Location ,LocationLabel ,AdGroupNo , COUNT(0) AS AdNum FROM   CORE.dbo.AdView WITH ( NOLOCK ) WHERE   PageLabel = 'appShouYe' and MerId=" + MerId + " and Invalid=0   ");
            if (BranchId != "")
            {
                s.Append("      AND BranchId = '" + BranchId + "' ");
            }

            if (ZoneId != "")
            {
                s.Append(" and ZoneId='" + ZoneId + "'");
            }

            s.Append(" GROUP BY AdGroupNo ,  Location ,   LocationLabel ");
            s.Append(" ORDER BY AdGroupNo DESC ");

            s.Append("         SELECT * FROM dbo.CuXiaoProView WITH(NOLOCK) WHERE MerId='" + MerId + "' AND CuXiaoLabel='appShouYeLieBiao'  and FlagInvalid=0 and Status>=0   ");
            if (BranchId != "")
            {
                s.Append("      AND BranchId = '" + BranchId + "' ");
            }

            if (ZoneId != "")
            {
                s.Append(" and ZoneId='" + ZoneId + "'");
            }

            s.Append("  SELECT * FROM CORE.dbo.CuXiao WITH(NOLOCK) WHERE  ZoneId='" + ZoneId + "'  AND CuXiaoLabel='YouPinShangXian'  ");  //取得优品上线

            if (ZoneId != "")
            {
                s.Append(" SELECT * FROM dbo.Zone WITH(NOLOCK) WHERE ZoneId='" + ZoneId + "' ");


            }

            if (ZoneId != "")
            {
                s.Append(" and ZoneId='" + ZoneId + "'");
            }


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable list = ds.Tables[0];
            DataTable groupList = ds.Tables[1];
            DataTable proList = ds.Tables[2];
            DataTable ypsx = ds.Tables[3];

            if (ZoneId != "")
            {
                DataTable dt_Zone = ds.Tables[4];
                ReDict.Add("Zone", JsonHelper.ToJsonNo1(dt_Zone));
            }


            ReDict.Add("list", JsonHelper.ToJson(list));
            ReDict.Add("groupList", JsonHelper.ToJson(groupList));
            ReDict.Add("proList", JsonHelper.ToJson(proList));

            if (ypsx.Rows.Count > 0)
            {
                ReDict2.Add("YpsxCuXiaoId", ypsx.Rows[0]["CuXiaoId"].ToString());   //优品上线促销ID
            }

            ReTrue();
        }

        private void DelBranchVsSiteOrder()
        {
            Model.BranchVsSiteModel model = new Model.BranchVsSiteModel();
            model.BranchId = ReStr("BranchId", "");
            model.SiteId = ReDecimal("SiteId", 0);

            DAL.BranchVsSiteDAL dal = new DAL.BranchVsSiteDAL();

            dal.DeleteList(" BranchId='" + model.BranchId + "' and  SiteId='" + model.SiteId + "'");
            ReTrue();

        }

        private void ChangeBranchVsSiteOrder()
        {
            Model.BranchVsSiteModel model = new Model.BranchVsSiteModel();
            model.BranchId = ReStr("BranchId", "");
            model.SiteId = ReDecimal("SiteId", 0);
            model.VsOrder = ReInt("VsOrder", 10);
            StringBuilder s = new StringBuilder();

            s.Append("  UPDATE dbo.BranchVsSite set VsOrder=" + model.VsOrder + "  WHERE BranchId='" + model.BranchId + "' and SiteId='" + model.SiteId + "'  ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();
        }

        private void GetBranchVsSiteList()
        {
            Model.BranchVsSiteModel model = new Model.BranchVsSiteModel();
            model.BranchId = ReStr("BranchId", "");
            model.SiteId = ReDecimal("SiteId", 0);

            StringBuilder s = new StringBuilder();

            s.Append("  SELECT * FROM dbo.BranchVsSiteView WITH(NOLOCK) WHERE BranchId='" + model.BranchId + "'  ");

            if (model.SiteId != 0)
            {
                s.Append(" AND SiteId=" + model.SiteId + " ");
            }
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void BranchVsSiteBind()
        {
            Model.BranchVsSiteModel model = new Model.BranchVsSiteModel();
            model.BranchId = ReStr("BranchId", "");
            model.SiteId = ReDecimal("SiteId", 0);
            if (model.BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }

            if (model.SiteId == 0)
            {
                throw new Exception("分部关联的SiteId不能为0!");
            }
            model.VsOrder = ReInt("VsOrder", 10);
            DAL.BranchVsSiteDAL dal = new DAL.BranchVsSiteDAL();
            dal.Add(model);
            ReTrue();
        }

        private void DelSiteDetail()
        {
            decimal SiteDetailId = ReDecimal("SiteDetailId", 0);
            StringBuilder s = new StringBuilder();
            DAL.SiteDetailDAL dal = new DAL.SiteDetailDAL();
            dal.DeleteList(" SiteDetailId=" + SiteDetailId + " ");
            ReTrue();
        }

        private void GetSiteInfo()
        {

            decimal SiteId = ReDecimal("SiteId", 0);
            if (SiteId == 0)
            {
                throw new Exception("SiteId为0还取什么siteInfo?");
            }
            else
            {

                StringBuilder s = new StringBuilder();


                s.Append(" SELECT * FROM dbo.Site WITH(NOLOCK) WHERE  SiteId=" + SiteId + " ");
                s.Append(" SELECT * FROM dbo.SiteDetail WITH(NOLOCK)  WHERE SiteId=" + SiteId + " ");

                DataSet ds = DAL.DalComm.BackData(s.ToString());


                DataTable dtInfo = ds.Tables[0];
                DataTable dtDetail = ds.Tables[1];

                ReDict.Add("info", JsonHelper.ToJsonNo1(dtInfo));
                ReDict.Add("detail", JsonHelper.ToJson(dtDetail));

                ReTrue();
            }


        }

        private void GetSitePageList()
        {
            int CurrentPage = ReInt("CurrentPage", 1);
            string inputStr = ReStr("inputStr", "");
            string ZoneId = ReStr("ZoneId", "");
            BLL.CommBLL bll = new BLL.CommBLL();
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            if (ZoneId != "")
            {
                s.Append(" and ZoneId='" + ZoneId + "' ");
            }

            if (inputStr != "")
            {
                s.Append(" and SiteName like '%" + inputStr + "%' ");
            }
            DataSet ds = bll.GetSitePageList(s.ToString(), " SiteId desc ", CurrentPage, 40, " * ");
            RePage2(ds);
        }

        private void SaveSite()
        {
            Model.SiteModel model = new Model.SiteModel();
            model.ParentSiteId = ReDecimal("ParentSiteId", 0);
            model.SiteId = ReDecimal("SiteId", 0);
            model.SiteLat = ReDecimal("SiteLat", 0);
            model.SiteLng = ReDecimal("SiteLng", 0);
            model.SiteMemo = ReStr("SiteMemo", "");
            model.SiteName = ReStr("SiteName", "");
            model.ZoneId = ReDecimal("ZoneId", 0);
            model.SiteLat = ReDecimal("SiteLat", 0);
            model.SiteLng = ReDecimal("SiteLng", 0);
            model.Unit = ReStr("Unit", "");

            BLL.CommBLL bll = new BLL.CommBLL();
            bll.SaveSite(model);
            ReDict2.Add("SiteId", model.SiteId.ToString());
            ReTrue();


        }

        private void SaveSiteDetail()
        {

            Model.SiteDetailModel model = new Model.SiteDetailModel();
            model.Extra = ReStr("Extra", "{}");
            model.OrderNo = ReInt("OrderNo", 10);
            model.SiteDetailId = ReDecimal("SiteDetailId", 0);
            model.SiteDetailName = ReStr("SiteDetailName", "");
            model.SiteDetailType = ReStr("SiteDetailType", "text");
            model.SiteDetaiMemo = ReStr("SiteDetaiMemo", "");
            model.SiteId = ReDecimal("SiteId", 0);
            BLL.CommBLL bll = new BLL.CommBLL();
            bll.SaveSiteDetail(model);
            ReTrue();
        }

        private void DeviceLocation()
        {
            string DeviceId = ReStr("DeviceId", "");
            decimal Lng = ReDecimal("Lng", 0);
            decimal Lat = ReDecimal("Lat", 0);

            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.Device SET Lat=" + Lat + " ,Lng=" + Lng + " WHERE DeviceId='" + DeviceId + "' ");

            ReTrue();
        }

        private void FilePost()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files["file"];
            string FilePath = HttpContext.Current.Server.MapPath("/upload/IMGS");

            string strNewPath = Common.FileString.GetSaveFilePath(FilePath) + "" + Common.FileString.GetExtension(file.FileName, ".jpg");
            file.SaveAs(FilePath + strNewPath);
            if (file.ContentLength > 1 * 1024 * 1024)
            {
                throw new Exception("上载文件不得大于1mb");
            }

            Model.ImageInfoModel model = new Model.ImageInfoModel();
            DAL.ImageInfoDAL dal = new DAL.ImageInfoDAL();



            string url = "/upload/IMGS" + strNewPath.Replace("\\", "/");
            model.ImgUrl = url;
            model.IsBind = false;
            model.ImgId = Common.TimeString.GetNow_ff();
            model.ImgType = "ad";
            model.CreateTime = DateTime.Now;
            dal.Add(model);
            ReDict2.Add("ImgId", model.ImgId);
            ReDict2.Add("ImgUrl", url);
            ReTrue();
        }

        private void GetStMsgList()
        {
            DAL.StMsgDAL dal = new DAL.StMsgDAL();
            decimal MerId = ReDecimal("MerId", 0);
            string PhoneNo = ReStr("PhoneNo", "").Trim();
            int CurrentPage = ReInt("CurrentPage", 1);
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 and MerId='" + MerId + "' ");
            if (PhoneNo != "")
            {
                s.Append(" and PhoneNo='" + PhoneNo + "' ");
            }

            string OrderNo = " CreateTime desc ";

            DataSet ds = dal.GetPageList(s.ToString(), OrderNo, CurrentPage, 20, "*");
            RePage2(ds);


        }

        private void ToDefaultHtml()
        {

            decimal MerId = ReDecimal("MerId", 0);

            if (MerId == 0)
            {
                throw new Exception("MerId必须确定!");
            }
            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfig(MerId);
            var url = "http://www." + MerConfig["Domain"];
            url = url + "/aj/ToHtml.aspx?type=default";
            string ConvertRe = Common.PageInput.GetHTML(url);
            ReDict2.Add("ConvertRe", ConvertRe);
            ReTrue();
        }

        private void AppPoolRecycleByMerId()
        {
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {

                throw new Exception("不能定位merId");
            }
            BLL.StaticBLL.AppPoolRecycleByMerId(MerId);
            ReTrue();
        }

        private void SendStMsg()
        {

            Model.StMsgModel model = new Model.StMsgModel();
            model.CreateTime = DateTime.Now;
            model.MerId = ReDecimal("MerId", 0);
            model.PhoneNo = ReStr("PhoneNo", "");
            if (!Common.Validator.IsMobile(model.PhoneNo))
            {
                throw new Exception("请输入正确的手机号码");
            }
            Random r = new Random();


            model.ReKey = r.Next(1000, 9999).ToString();
            model.StMsgClassId = ReDecimal("StMsgClassId", 1);
            model.StMsgCode = "";
            model.StMsgContent = ReStr("StMsgContent");
            model.StMsgContent = model.StMsgContent.Replace("{ReKey}", model.ReKey);
            model.StMsgId = 0;
            model.StMsgTypeId = ReDecimal("StMsgTypeId", 1); //1是用户注册!

            BLL.StaticBLL.SendStMsg(model);

            ReTrue();

        }

        private void SelInput()
        {
            StringBuilder s = new StringBuilder();
            string InputCode = ReStr("InputCode");
            string SelType = ReStr("SelType");

            switch (SelType)
            {
                case "Community":
                    #region 小区
                    s.Append(" select top 20 MerchantId,MerchantName,LogoUrl,Logo from  dbo.MerchantView  WITH(NOLOCK)   where InputCode like '" + InputCode + "%' or MerchantName like '" + InputCode + "%'  and FlagInvalid=0 ");


                    #endregion
                    break;
                case "Author":

                    decimal MerId = ReDecimal("MerId", 0);
                    s.Append(" select top 8 AuthorName,AuthorId from dbo.AuthorInfo  WITH(NOLOCK)   where (InputCode like '" + InputCode + "%' or AuthorName like '" + InputCode + "%')  and Invalid=0 and MerchantId='" + MerId + "' ");
                    break;


                default:
                    break;
            }

            DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
            string ja = JsonHelper.ToJson(dt);
            ReDict.Add("ja", ja);
            ReTrue();
        }

        private void InvalidAd()
        {
            decimal AdId = ReDecimal("AdId");
            bool Invalid = ReBool("Invalid", true);
            StringBuilder s = new StringBuilder();
            s.Append(" update  CORE.dbo.AdInfo set Invalid='" + Invalid + "'  where  AdId ='" + AdId + "'");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();
        }

        private void SaveAd()
        {
            Model.AdInfoModel model = new Model.AdInfoModel();
            model.AdId = ReDecimal("AdId", 0);
            model.AdTitle = ReStr("AdTitle");
            model.AdContent = ReStr("AdContent");
            model.AdMemo = ReStr("AdMemo");
            model.Url = ReStr("AdUrl");
            model.BindEvent = ReStr("BindEvent");
            model.AdType = ReStr("AdType");
            model.AdClass = ReStr("AdClass");
            model.CreateTime = ReTime("CreateTime", DateTime.Now);
            model.CreateUser = Common.CookieSings.GetCurrentUserId();
            model.OrderNo = ReInt("OrderNo");
            model.EndTime = ReTime("EndTime", DateTime.Now.AddMonths(1));
            model.Invalid = ReBool("Invalid", false);
            model.Location = ReStr("Location");
            model.W = ReInt("W");
            model.H = ReInt("H");
            model.ImgId = ReStr("ImgId");
            BLL.CommBLL bll = new BLL.CommBLL();
            bll.SaveAd(model);
            ReTrue();
        }

        private void ChangeLocationOrderNo()
        {
            string LocationId = ReStr("LocationId", "");
            int OrderNo = ReInt("OrderNo", 1);
            if (LocationId != "")
            {
                StringBuilder s = new StringBuilder();
                s.Append(" update CORE.dbo.Location set OrderNo='" + OrderNo + "' where LocationId='" + LocationId + "' ");
                DAL.DalComm.ExReInt(s.ToString());
                ReTrue();
            }
            else
            {
                throw new Exception("LocationId不能为空");
            }
        }

        private void GetLocationList()
        {
            string PageId = ReStr("PageId", "");

            if (PageId == "")
            {
                throw new Exception("PageId不能为空,您没有选择任何页面!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" and PageId='" + PageId + "' ");
            s.Append(" order by orderNo  ");
            BLL.CommBLL bll = new BLL.CommBLL();
            DataSet ds = bll.GetLocationList(s.ToString());
            DataTable dt = ds.Tables[0];
            string j = Common.JsonHelper.ToJson(dt);
            ReDict.Add("LocationList", j);
            ReTrue();
        }

        private void Invalid()
        {

            StringBuilder s = new StringBuilder();
            string InvalidType = ReStr("InvalidType");
            switch (InvalidType.ToLower())
            {
                case "job":
                    //职位信息
                    string JobId = ReStr("JobId");
                    s.Append(" update CORE.dbo.Job  set Invalid=0 where JobId='" + JobId + "' ");

                    break;
                case "house":
                    //房产信息
                    string HouseId = ReStr("HouseId");
                    s.Append(" update CORE.dbo.House set CreateTime='" + DateTime.Now + "' where JobId='" + HouseId + "' ");
                    break;
                case "information":
                    //供求信息
                    string Information = ReStr("Information");
                    s.Append(" update CORE.dbo.Information  set CreateTime='" + DateTime.Now + "' where JobId='" + Information + "' ");
                    break;
                default:
                    break;
            }

            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();
        }

        private void ShuaXin()
        {
            StringBuilder s = new StringBuilder();
            string ShuaXinType = ReStr("ShuaXinType");
            switch (ShuaXinType.ToLower())
            {
                case "job":
                    //职位信息
                    string JobId = ReStr("JobId");
                    s.Append(" update CORE.dbo.Job  set CreateTime='" + DateTime.Now + "' where JobId='" + JobId + "' ");

                    break;
                case "house":
                    //房产信息
                    string HouseId = ReStr("HouseId");
                    s.Append(" update CORE.dbo.House set CreateTime='" + DateTime.Now + "' where JobId='" + HouseId + "' ");
                    break;
                case "information":
                    //供求信息
                    string Information = ReStr("Information");
                    s.Append(" update CORE.dbo.Information  set CreateTime='" + DateTime.Now + "' where JobId='" + Information + "' ");
                    break;
                default:
                    break;
            }

            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void SerachIndexDataList()
        {
            int CurrentPage = ReInt("CurrentPage");
            BLL.CommBLL bll = new BLL.CommBLL();
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" order by orderNo desc ");
            DataSet ds = bll.GetIndexDataPageList(s.ToString(), CurrentPage);
            RePage(ds);
        }




        private void SaveIndexDataInfo()
        {
            Model.IndexDataModel model = new Model.IndexDataModel();

            BLL.CommBLL bll = new BLL.CommBLL();

            model.AutoId = ReDecimal("AutoId", 0);

            if (model.AutoId != 0)
            {
                model = bll.GetIndexDataModel(model.AutoId);
            }
            model.ItemTitle = ReStr("ItemTitle");
            model.ItemContent = ReStr("ItemContent");
            model.ItemType = ReStr("ItemType");
            model.ItemClass = ReStr("ItemClass");
            model.OrderNo = ReInt("OrderNo", 0);
            model.CreateTime = DateTime.Now;

            model.ImgId = ReStr("ImgId", "");
            model.ReKey = ReStr("ReKey");
            model.Url = ReStr("ToUrl", "");

            switch (model.ItemType)
            {

                case "首页_美食":
                    #region 首页餐饮分支

                    switch (model.ItemClass.ToLower())
                    {
                        case "pro":
                            Dictionary<string, string> ProDict = new Dictionary<string, string>();
                            BLL.MerchantBLL Merbll = new BLL.MerchantBLL();
                            DataSet ds = Merbll.GetProInfoById(model.ReKey);   //产品ID
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                throw new Exception("没有编号为" + model.ReKey + "的产品!");
                            }

                            DataRow dr = dt.Rows[0];
                            ProDict.Add("MerchantName", dr["MerchantName"].ToString());
                            ProDict.Add("MerchantId", dr["MerchantId"].ToString());
                            model.JsonMemo = XmlHelper.BackXmlStr(ProDict);
                            break;
                        case "mer":
                            model.JsonMemo = "";
                            break;
                    }
                    break;
                default:
                    break;

                    #endregion





            }

            model.JsonMemo = "<root></root>";
            model.EventName = ReStr("EventName", "");



            bll.SaveIndexDataInfo(model);
            ReDict2.Add("AutoId", model.AutoId.ToString());
            ReTrue();


        }

        private void GetRemindNum()
        {

            int i = DAL.DalComm.ExInt(" select count(0) from dbo.Remind where ReUserId='" + Common.CookieSings.GetCurrentUserId() + "'  and UserLook=0 ");

            ReDict2.Add("i", i.ToString());

            ReTrue();

        }

        private void LookMyRemind()
        {

            decimal RemindId = ReDecimal("RemindId");

            DAL.DalComm.ExReInt(" UPDATE dbo.Remind SET UserLook='true' where RemindId='" + RemindId + "' ");
            ReTrue();
        }

        private void GetRemindPageList()
        {

            int CurrentPage = ReInt("CurrentPage");
            BLL.CommBLL bll = new BLL.CommBLL();
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" and  ReUserId ='" + CookieSings.GetCurrentUserId() + "' ");

            s.Append(" order by createTime desc  ");
            DataSet ds = bll.GetRemindPageList(s.ToString(), CurrentPage);
            RePage(ds);


        }



        private void GetCommentUrl()
        {
            decimal CommentId = ReDecimal("CommentId");

            decimal ParentCommenId = DAL.DalComm.ExInt("select ParentCommenId from dbo.Comment where CommentId='" + CommentId + "'");
            string url = "";
            if (ParentCommenId == 0)
            {
                //是主留言
                url = BackCommentUrl(CommentId);
            }
            else
            { //是点评
                url = BackCommentUrl(ParentCommenId);
            }

            ReDict2.Add("url", url);
            ReTrue();
        }



        private string BackCommentUrl(decimal CommentId)
        {

            BLL.CommBLL bll = new BLL.CommBLL();

            DataSet ds = DAL.DalComm.BackData(" select *  from dbo.Comment WITH(NOLOCK)  where CommentId='" + CommentId + "' ");
            StringBuilder s = new StringBuilder();
            string url = "";


            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                decimal ParentCommenId = decimal.Parse(dr["ParentCommenId"].ToString());
                if (ParentCommenId == 0)
                {
                    //这是一条主留言
                    string CommentType = dr["CommentType"].ToString();

                    switch (CommentType.ToLower())
                    {

                        case "pro":  //如果是产品
                            s = new StringBuilder();
                            s.Append(" select top 1 ProId from dbo.ProVsComment WITH(NOLOCK)  where CommentId='" + dr["CommentId"] + "'  ");
                            string ProId = DAL.DalComm.ExStr(s.ToString());
                            url = "/Pro/?ProId=" + ProId;
                            break;

                        case "house":  //如果是房产
                            s = new StringBuilder();
                            s.Append(" select top 1 HouseId from dbo.HouseVsComment WITH(NOLOCK)  where CommentId='" + dr["CommentId"].ToString() + "' ");
                            string HouseId = DAL.DalComm.ExStr(s.ToString());
                            url = "/House/fangyuan/?HouseId=" + HouseId;
                            break;
                        case "pmer":  //产品点评了商家
                        case "mer":   //如果点评了产品
                            s = new StringBuilder();
                            s.Append(" select top 1 MerchantId  from dbo.MerVsComment WITH(NOLOCK)  where CommentId='" + dr["CommentId"].ToString() + "' ");
                            string MerchantId = DAL.DalComm.ExStr(s.ToString());
                            url = "/Mer/?MerchantId=" + MerchantId;
                            break;
                        case "job":
                            s = new StringBuilder();
                            s.Append(" select top 1 JobId from dbo.JobVsComment WITH(NOLOCK) where CommentId='" + dr["CommentId"] + "' ");
                            string JobId = DAL.DalComm.ExStr(s.ToString());
                            url = "/job/JobInfo/?JobId=" + JobId;
                            break;
                    }

                }
                else
                {
                    //如果这是一条回复留言
                    throw new Exception("这是一条回复,而不是点评!");

                }
                url = url + "&CommentId=" + CommentId + "";
            }
            return url;
        }


        private void MyComment()
        {
            BLL.CommBLL bll = new BLL.CommBLL();
            int CurrentPage = ReInt("CurrentPage");
            DataSet ds = bll.GetCommentPageList("  Createuser='" + Common.CookieSings.GetCurrentUserId() + "' and ParentCommenId=0  order by createTime desc ", CurrentPage, 20);
            RePage(ds);
        }

        private void MyRepComment()
        {
            BLL.CommBLL bll = new BLL.CommBLL();
            int CurrentPage = ReInt("CurrentPage");
            DataSet ds = bll.GetCommentPageList("  Createuser='" + Common.CookieSings.GetCurrentUserId() + "' and ParentCommenId<>0  order by createTime desc ", CurrentPage, 20);
            RePage(ds);
        }


        private void DelImageById()
        {
            string ImgId = ReStr("ImgId");

            BLL.CommBLL.DelImageById(ImgId);
            ReTrue();

        }

        public void DelImageByUrl()
        {
            string ImgUrl = ReStr("ImgUrl");

            BLL.CommBLL.DelImageByUrl(ImgUrl);
            ReTrue();

        }




        /// <summary>
        /// 获得回复的点评
        /// </summary>
        private void GetCommentRepList()
        {
            BLL.CommBLL bll = new BLL.CommBLL();
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" and  ParentCommenId='" + ReDecimal("ParentCommenId") + "' ");
            s.Append(" order by CreateTime ");
            DataSet ds = bll.GetCommentRepList(s.ToString(), ReInt("CurrentPage"));
            RePage(ds);
        }

        private void GetNewRemind()
        {
            DAL.RemindDAL dal = new DAL.RemindDAL();


        }

        private void ExPwd()
        {
            DAL.UserInfoDAL dal = new DAL.UserInfoDAL();

            string inputStr = ReStr("inputStr");
            string pwd = ReStr("Pwd");

            if (dal.ExInt(" (UserId='" + inputStr + "' or Email='" + inputStr + "') and Pwd='" + pwd + "' ") == 0)
            {
                throw new Exception("密码输入不正确!");
            }
            ReTrue();
        }

        private void ExHasUserIdOrEmail()
        {
            DAL.UserInfoDAL dal = new DAL.UserInfoDAL();

            string inputStr = ReStr("inputStr");


            if (dal.ExInt(" UserId='" + inputStr + "' or Email='" + inputStr + "' ") == 0)
            {
                throw new Exception("用户名或邮箱不存在!");
            }
            ReTrue();
        }


        private void ExUserId()
        {
            DAL.UserInfoDAL dal = new DAL.UserInfoDAL();

            string uid = ReStr("uid");

            if (!Common.Validator.IsNormalChar(uid))
            {
                throw new Exception("非正常字符 字母，数字，下划线的组合!");
            }

            if (dal.ExInt(" UserId='" + uid + "' ") > 0)
            {
                throw new Exception("用户名(昵称)已经存在!");
            }
            ReTrue();
        }

        private void ExMail()
        {
            DAL.UserInfoDAL dal = new DAL.UserInfoDAL();

            string Email = ReStr("Email");

            if (!Common.Validator.IsEmail(Email))
            {
                throw new Exception("不是合法的邮件地址!");
            }

            if (dal.ExInt(" Email='" + Email + "' ") > 0)
            {
                throw new Exception("该邮箱已经存在!");
            }
            ReTrue();
        }

        private void GetCommentRepPageList()
        {

            DAL.CommentDAL dal = new DAL.CommentDAL();
            RePage(dal.GetPageList(" ParentCommenId='" + ReDecimal("ParentCommenId") + "' order by createtime  ", ReInt("CurrentPage"), 5));


        }


        private void GetCommentList()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            string CommentType = ReStr("CommentType");
            StringBuilder s = new StringBuilder();
            int CurrentPage = ReInt("CurrentPage");
            string order = ReStr("order", " order by createTime desc ");
            order = " " + order + " ";
            DataSet ds = null;
            switch (CommentType.ToLower())
            {
                case "mer":
                    string MerchantId = ReStr("MerchantId");
                    s.Append(" MerchantId='" + MerchantId + "' ");
                    s.Append(order);
                    ds = bll.GetMerVsComment(s.ToString(), CurrentPage);
                    break;

                case "pro":
                    string ProId = ReStr("ProId");
                    s.Append(" ProId='" + ProId + "' ");
                    s.Append(order);
                    ds = bll.GetProCommentPageList(s.ToString(), CurrentPage);
                    break;

                case "job":
                    BLL.JobBLL jobBll = new BLL.JobBLL();
                    string JobId = ReStr("JobId");
                    s.Append(" JobId='" + JobId + "' ");
                    s.Append(order);
                    ds = jobBll.GetJobCommentPageList(s.ToString(), CurrentPage);
                    break;

                case "house":
                    BLL.HouseBLL hbll = new BLL.HouseBLL();
                    string HouseId = ReStr("HouseId");
                    s.Append(" HouseId='" + HouseId + "' ");
                    s.Append(order);
                    ds = hbll.GetHouseVsCommentPageList(s.ToString(), CurrentPage);
                    break;
                case "information":

                    BLL.InformationBLL ibll = new BLL.InformationBLL();
                    decimal InformationId = ReDecimal("InformationId");
                    s.Append(" InformationId='" + InformationId + "' ");
                    s.Append(" order by CreateTime desc ");
                    ds = ibll.GetInformationVsCommentPageList(s.ToString(), CurrentPage, 10);
                    break;

                default:
                    break;
            }
            DAL.CommentDAL dal = new DAL.CommentDAL();
            DataTable dt = ds.Tables[2];
            if (dt.Rows.Count > 0)
            {
                DataTable dt_rep = dal.GetCommentRep(dt).Tables[0];
                dt.Columns.Add("RepJson");
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dt_myrep = Common.DataSetting.TableSelect(" ParentCommenId='" + dr["CommentId"] + "' ", dt_rep);
                    dr["RepJson"] = Common.JsonHelper.ToJson(dt_myrep);
                }
            }
            RePage(ds);
        }


        private void DelMerAttention()
        {
            string AttentionMerId = ReStr("AttentionMerId");
            BLL.CommBLL bll = new BLL.CommBLL();
            bll.DeleteAttention(" UserId='" + Common.CookieSings.GetCurrentUserId() + "' and AttentionMerId='" + AttentionMerId + "' ");
            ReTrue();
        }



        private void AddMerAttention()
        {
            Model.AttentionModel model = new Model.AttentionModel();
            BLL.CommBLL bll = new BLL.CommBLL();
            bll.AddAttentionMer(ReDecimal("AttentionMerId"));
            ReTrue();
        }

        //////////////////////////////////////////以上是关注商家  以下是关注用户/////////////////////////////////////////////

        /// <summary>
        /// 关注用户
        /// </summary>
        private void AddUserAttention()
        {
            Model.AttentionModel model = new Model.AttentionModel();
            BLL.CommBLL bll = new BLL.CommBLL();
            bll.AddAttentionUser(ReStr("AttentionUserId"));
            ReTrue();
        }

        private void DelUserAttention()
        {
            string AttentionUserId = ReStr("AttentionUserId");
            BLL.CommBLL bll = new BLL.CommBLL();
            bll.DeleteAttention(" UserId='" + Common.CookieSings.GetCurrentUserId() + "' and AttentionUserId='" + AttentionUserId + "' ");
            ReTrue();
        }






        private void SaveComment()
        {
            BLL.UserBLL ubll = new BLL.UserBLL();

            Model.CommentModel CommentModel = new Model.CommentModel();
            Model.ProVsCommentModel VsCommentModel = new Model.ProVsCommentModel();
            CommentModel.CommentContent = ReStr("CommentContent");
            CommentModel.CommentTitle = ReStr("CommentTitle");
            CommentModel.CommentType = ReStr("CommentType");
            CommentModel.CreateTime = DateTime.Now;
            CommentModel.CreateUser = ubll.CurrentUserId();
            CommentModel.ParentCommenId = ReDecimal("ParentCommenId", 0);
            CommentModel.FlagInvalid = false;
            CommentModel.ReceiveUser = "";
            Model.RemindModel RemindModel = new Model.RemindModel();
            RemindModel.CreateTime = DateTime.Now;
            RemindModel.MerLook = false;

            Model.DynamicModel dyModel = new Model.DynamicModel();
            dyModel.DynamicLv = 80;




            #region 事务开启


            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                BLL.CommBLL bll = new BLL.CommBLL();
                bll.SaveNewComment(CommentModel);  //保存点评内容

                if (CommentModel.CommentType.ToLower() == "pro")
                {

                    BLL.MerchantBLL mbll = new BLL.MerchantBLL();
                    #region 如果是点评了产品
                    VsCommentModel.CommentId = CommentModel.CommentId;
                    VsCommentModel.ProId = ReStr("ProId");



                    VsCommentModel.VsType = "用户点评";
                    mbll.AddProComment(VsCommentModel);  //添加


                    Model.MerVsCommentModel MvCmodel = new Model.MerVsCommentModel();
                    MvCmodel.CommentId = CommentModel.CommentId;
                    MvCmodel.MerchantId = ReDecimal("MerId");

                    MvCmodel.VsType = "产品-商家";

                    bll.SaveNewMerVsDal(MvCmodel);

                    #region 提醒开始

                    RemindModel.ReKey = VsCommentModel.ProId;   //保存提醒编号
                    string ProName = ReStr("ProName");
                    RemindModel.Url = "/Pro/?ProId=" + VsCommentModel.ProId + "&CommentId=" + CommentModel.CommentId + "";
                    RemindModel.RemindTitle = "用户\"" + CommentModel.CreateUser + "\"点评了您的产品(" + ProName + ")!";
                    RemindModel.RemindTypeId = "点评产品";
                    RemindModel.ReMerchantId = MvCmodel.MerchantId;
                    DataTable dt = ReTable("MerUserJsonArray");   //看看这个商家有多少用户
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)   //为每一个用户加入一个新的提醒
                        {

                            #region 循环插入提醒
                            RemindModel.ReUserId = dr["UserId"].ToString();

                            bll.SaveReMind(RemindModel);
                            #endregion

                            #region 循环插入新鲜事


                            #endregion

                        }
                    }


                    #endregion
                    #region 新鲜事开始

                    string MerName = ReStr("MerName");
                    string MerLogoUrl = ReStr("MerLogoUrl");
                    string ProImgUrl = ReStr("ProImgUrl");
                    decimal RePrice = ReDecimal("RePrice");
                    dyModel.DynamicUserId = CommentModel.CreateUser;
                    dyModel.DynamicTitle = "点评了产品 '" + ProName + "'";
                    dyModel.DynamicType = "点评产品";

                    Dictionary<string, string> ReXml = new Dictionary<string, string>();
                    ReXml.Add("url", "/Pro/?ProId=" + VsCommentModel.ProId + "&CommentId=" + CommentModel.CommentId + "");
                    //    ReXml.Add("ProImgUrl", "");
                    ReXml.Add("MerName", MerName);
                    if (Common.FileString.IsFileCunZai(MerLogoUrl))
                    {

                        ReXml.Add("MerLogoUrl", MerLogoUrl);
                    }
                    if (Common.FileString.IsFileCunZai(ProImgUrl))
                    {
                        ReXml.Add("ProImgUrl", ProImgUrl);
                    }
                    ReXml.Add("RePrice", RePrice.ToString());
                    ReXml.Add("ProName", ProName);
                    ReXml.Add("ProId", VsCommentModel.ProId);
                    dyModel.JsonMemo = XmlHelper.BackXmlStr(ReXml);
                    BLL.CommBLL.AddDynamic(dyModel);
                    #endregion







                    //  DataTable AboutPro = ReTable("AboutPro");


                    #endregion
                }
                else if (CommentModel.CommentType.ToLower() == "mer")
                {
                    #region 如果是点评了商家


                    Model.MerVsCommentModel MvCmodel = new Model.MerVsCommentModel();
                    MvCmodel.CommentId = CommentModel.CommentId;
                    MvCmodel.MerchantId = decimal.Parse(ReStr("MerchantId"));

                    MvCmodel.VsType = "点评商家";
                    bll.SaveNewMerVsDal(MvCmodel);


                    #region 提醒开始
                    RemindModel.ReKey = VsCommentModel.ProId;   //保存提醒编号
                    string MerName = ReStr("MerName");
                    RemindModel.UserLook = false;
                    RemindModel.MerLook = false;
                    RemindModel.RemindTitle = "您收到了一条来自(" + CommentModel.CreateUser + ")的商家点评!";
                    RemindModel.Url = "/Mer/?MerchantId=" + MvCmodel.MerchantId + "&CommentId=" + CommentModel.CommentId + "";
                    RemindModel.ReMerchantId = MvCmodel.MerchantId;
                    RemindModel.RemindTypeId = "点评商家";
                    DataTable dtReUser = ReTable("ReUserIdArray");
                    if (dtReUser != null)
                    {
                        foreach (DataRow dr in dtReUser.Rows)
                        {
                            RemindModel.ReUserId = dr["UserId"].ToString();
                            RemindModel.ReKey = MvCmodel.MerchantId.ToString();
                            bll.SaveReMind(RemindModel);
                        }
                    }
                    #endregion


                    #region 新鲜事开始



                    dyModel.DynamicUserId = CommentModel.CreateUser;
                    dyModel.DynamicTitle = "点评了商家 '" + MerName + "'";
                    dyModel.DynamicType = "点评产品";

                    Dictionary<string, string> ReXml = new Dictionary<string, string>();
                    ReXml.Add("url", "/Mer/?MerId=" + MvCmodel.MerchantId.ToString() + "&CommentId=" + CommentModel.CommentId + "");
                    ReXml.Add("MerName", MerName);
                    ReXml.Add("MerId", MvCmodel.MerchantId.ToString());
                    dyModel.JsonMemo = XmlHelper.BackXmlStr(ReXml);
                    BLL.CommBLL.AddDynamic(dyModel);

                    #endregion

                    #endregion    //保存点评商家
                }
                else if (CommentModel.CommentType.ToLower() == "job")
                {
                    #region 如果是点评了职位


                    Model.JobVsCommentModel JvCmodel = new Model.JobVsCommentModel();
                    JvCmodel.CommentId = CommentModel.CommentId;
                    JvCmodel.JobId = ReStr("JobId");

                    JvCmodel.VsType = "点评职位";
                    bll.SaveNewJobVsDal(JvCmodel);

                    #region 提醒开始
                    string JobtTitle = ReStr("JobtTitle");
                    RemindModel.MerLook = false;
                    RemindModel.UserLook = false;
                    RemindModel.Url = "/Job/JobInfo/?JobId=" + JvCmodel.JobId + "&CommentId=" + CommentModel.CommentId + "";
                    RemindModel.ReKey = JvCmodel.JobId;
                    RemindModel.RemindTitle = "用户(" + CommentModel.CreateUser + ")咨询了您发布的职位\"" + JobtTitle + "\"m,点击查看";
                    RemindModel.RemindTypeId = "点评职位";
                    RemindModel.ReUserId = ReStr("JobCreateUser");
                    bll.SaveReMind(RemindModel);
                    #endregion

                    #region 新鲜事开始

                    dyModel.DynamicUserId = CommentModel.CreateUser;
                    dyModel.DynamicTitle = "点评了招聘 '" + JobtTitle + "'";
                    dyModel.DynamicType = "点评招聘";

                    Dictionary<string, string> ReXml = new Dictionary<string, string>();
                    ReXml.Add("url", "/job/JobInfo/?JobId=" + JvCmodel.JobId + "&CommentId=" + CommentModel.CommentId + "");
                    ReXml.Add("JobtTitle", JobtTitle);
                    ReXml.Add("JobId", JvCmodel.JobId);
                    dyModel.JsonMemo = XmlHelper.BackXmlStr(ReXml);
                    BLL.CommBLL.AddDynamic(dyModel);


                    #endregion
                    #endregion    //保存点评商家
                }

                else if (CommentModel.CommentType.ToLower() == "house")
                {

                    #region 点评了房产
                    Model.HouseVsCommentModel HvcModel = new Model.HouseVsCommentModel();
                    HvcModel.CommentId = CommentModel.CommentId;
                    HvcModel.HouseId = ReStr("HouseId");
                    HvcModel.VsType = "点评房产";
                    bll.SaveHouseVsComment(HvcModel);

                    #region 提醒开始

                    string HouseTitle = ReStr("HouseTitle");

                    RemindModel.MerLook = false;
                    RemindModel.UserLook = false;
                    RemindModel.Url = "/House/fangyuan/?HouseId=" + HvcModel.HouseId + "&CommentId=" + CommentModel.CommentId;
                    RemindModel.ReKey = HvcModel.HouseId;
                    RemindModel.ReUserId = ReStr("CreateUser");   //被提醒的用户
                    RemindModel.RemindTypeId = "点评房产";
                    RemindModel.RemindTitle = "您发布的房源'" + HouseTitle + "',有新的咨询,来自:" + CommentModel.CreateUser + "";
                    bll.SaveReMind(RemindModel);

                    #endregion



                    #region 新鲜事开始

                    string HouseImgUrl = ReStr("HouseImgUrl");

                    dyModel.DynamicUserId = CommentModel.CreateUser;
                    dyModel.DynamicTitle = "点评了房产 '" + HouseTitle + "'";
                    dyModel.DynamicType = "点评房产";

                    Dictionary<string, string> ReXml = new Dictionary<string, string>();
                    ReXml.Add("url", "/House/fangyuan/?HouseId=" + HvcModel.HouseId + "&CommentId=" + CommentModel.CommentId + "");
                    ReXml.Add("HouseTitle", HouseTitle);
                    ReXml.Add("HouseId", HvcModel.HouseId);
                    if (Common.FileString.IsFileCunZai(HouseImgUrl))
                    {
                        ReXml.Add("HouseImgUrl", HouseImgUrl);
                    }
                    dyModel.JsonMemo = XmlHelper.BackXmlStr(ReXml);
                    BLL.CommBLL.AddDynamic(dyModel);


                    #endregion

                    #endregion

                }
                else if (CommentModel.CommentType.ToLower() == "information")
                {


                    #region 点评了供求信息

                    Model.InformationVsCommentModel IvcModel = new Model.InformationVsCommentModel();
                    IvcModel.CommentId = CommentModel.CommentId;
                    IvcModel.InformationId = ReDecimal("InformationId");
                    IvcModel.vsType = "点评供求";
                    bll.SaveInformationVsComment(IvcModel);
                    #region 提醒开始


                    RemindModel.MerLook = false;
                    RemindModel.UserLook = false;
                    RemindModel.Url = "/InformationInfo/?InformationId=" + IvcModel.InformationId + "&CommentId=" + CommentModel.CommentId;
                    RemindModel.ReKey = IvcModel.InformationId.ToString(); ;
                    RemindModel.ReUserId = ReStr("CreateUser");   //被提醒的用户
                    RemindModel.RemindTypeId = "点评供求";
                    RemindModel.RemindTitle = "您发布的供求信息,有新的咨询,来自:" + CommentModel.CreateUser + "";
                    bll.SaveReMind(RemindModel);


                    #endregion



                    #endregion

                }

                else if (CommentModel.CommentType.ToLower() == "comm")
                {
                    #region 如果是回复
                    RemindModel.ReKey = CommentModel.CommentId.ToString();
                    RemindModel.MerLook = false;
                    RemindModel.UserLook = false;
                    RemindModel.RemindTypeId = "回复点评";

                    RemindModel.Url = ReStr("ReUrl") + "&CommentId=" + CommentModel.CommentId;
                    RemindModel.RemindTitle = "有人回复了您的点评!";


                    DataTable dtUser = ReTable("RemindUserArray");
                    if (dtUser != null)
                    {

                        foreach (DataRow dr in dtUser.Rows)
                        {
                            RemindModel.ReUserId = dr["UserId"].ToString();
                            bll.SaveReMind(RemindModel);
                        }
                    }
                    int ReCoumt = DAL.DalComm.ExInt(" select count(0) from  dbo.Comment where ParentCommenId='" + CommentModel.ParentCommenId + "' ");
                    ReDict2.Add("ReCoumt", ReCoumt.ToString());

                    #endregion    //保存点评商家
                }

                DataTable dtImg = ReTable("imgArray");

                DAL.CommentVsImgDAL dal = new DAL.CommentVsImgDAL();


                if (dtImg != null)
                { //有图片

                    foreach (DataRow dr in dtImg.Rows)
                    {
                        try
                        {
                            Model.CommentVsImgModel CvIModel = new Model.CommentVsImgModel();
                            CvIModel.CommentId = CommentModel.CommentId;
                            CvIModel.ImgId = dr["ImgId"].ToString();
                            dal.Add(CvIModel);
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }

                #region 事务关闭
                transactionScope.Complete();


            }
            #endregion
            ReTrue();

        }

        private void DownPicture()
        {
            string HttpImgUrl = ReStr("HttpImgUrl");
            try
            {
                string ImgUrl = Common.ImgHelper.downOneImg(HttpImgUrl);

                Model.ImageInfoModel model = new Model.ImageInfoModel();
                model.ImgUrl = ImgUrl;
                model.ImgType = "临时文件";
                model.CreateTime = DateTime.Now;
                model.CreateUser = Common.CookieSings.GetCurrentUserId();
                model.IsBind = false;
                BLL.ImageBLL bll = new BLL.ImageBLL();
                bll.AddNewImages(model);
                ReDict2.Add("DownImgUrl", model.ImgUrl);
                ReDict2.Add("ImgId", model.ImgId);
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }


        }

        private void CropPicture()
        {

            Model.ImageInfoModel model = new Model.ImageInfoModel();
            BLL.ImageBLL bll = new BLL.ImageBLL();
            string YuanShiImgId = ReStr("ImgId");
            model.ImgId = ReStr("ImgId");
            DataTable dt = bll.GetList(" ImgId='" + model.ImgId + "' ").Tables[0];
            if (dt.Rows.Count == 0)
            {
                ReThrow(new Exception("没有编号为" + model.ImgId + "的图片!"));

            }
            DataRow dr = dt.Rows[0];
            model.ImgUrl = dr["ImgUrl"].ToString();

            int x = ReInt("x");
            int y = ReInt("y");
            int width = ReInt("width");
            int height = ReInt("height");

            try
            {
                string ImgUrl2 = Common.FileString.GetFileUrl2(model.ImgUrl, "_B");

                DrawImage(HttpContext.Current.Server.MapPath(model.ImgUrl), HttpContext.Current.Server.MapPath(ImgUrl2), x, y, width, height);  //剪裁图片

                model.ImgUrl = ImgUrl2;
                model.ImgType = "UserPic";
                model.CreateTime = DateTime.Now;
                model.CreateUser = Common.CookieSings.GetCurrentUserId();
                bll.AddNewImages(model);  //插入剪裁后的图片\
                string json = Common.JsonHelper.ToJson(model);
                ReDict.Add("ImgJson", json);

                BLL.CommBLL.DelImageById(YuanShiImgId);
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }

        }

        protected void DrawImage(string srcImage, string destImage, int x, int y, int width, int height)
        {
            using (System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(srcImage))
            {
                using (System.Drawing.Image templateImage = new System.Drawing.Bitmap(width, height))
                {
                    using (System.Drawing.Graphics templateGraphics = System.Drawing.Graphics.FromImage(templateImage))
                    {
                        templateGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        templateGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        templateGraphics.DrawImage(sourceImage, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(x, y, width, height), System.Drawing.GraphicsUnit.Pixel);
                        templateImage.Save(destImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
        }
    }
}
