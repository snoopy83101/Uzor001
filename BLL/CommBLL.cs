using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Transactions;
using io.rong;
using LitJson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace BLL
{
    public class CommBLL
    {


        DAL.MerVsCommentDAL MerCDal = new DAL.MerVsCommentDAL();
        DAL.CommentDAL CDal = new DAL.CommentDAL();
        DAL.JobVsCommentDAL JvCDal = new DAL.JobVsCommentDAL();


        #region 识别银行卡

        public JObject ReadBankCard(string BankCardCode)
        {
            #region 银行字典
            JObject jBank = JObject.Parse(@"{
  'SRCB': '深圳农村商业银行', 
  'BGB': '广西北部湾银行',
  'SHRCB': '上海农村商业银行',
  'BJBANK': '北京银行',
  'WHCCB': '威海市商业银行',
  'BOZK': '周口银行',
  'KORLABANK': '库尔勒市商业银行',
  'SPABANK': '平安银行',
  'SDEB': '顺德农商银行',
  'HURCB': '湖北省农村信用社',
  'WRCB': '无锡农村商业银行',
  'BOCY': '朝阳银行',
  'CZBANK': '浙商银行',
  'HDBANK': '邯郸银行',
  'BOC': '中国银行',
  'BOD': '东莞银行',
  'CCB': '中国建设银行',
  'ZYCBANK': '遵义市商业银行',
  'SXCB': '绍兴银行',
  'GZRCU': '贵州省农村信用社',
  'ZJKCCB': '张家口市商业银行',
  'BOJZ': '锦州银行',
  'BOP': '平顶山银行',
  'HKB': '汉口银行',
  'SPDB': '上海浦东发展银行',
  'NXRCU': '宁夏黄河农村商业银行',
  'NYNB': '广东南粤银行',
  'GRCB': '广州农商银行',
  'BOSZ': '苏州银行',
  'HZCB': '杭州银行',
  'HSBK': '衡水银行',
  'HBC': '湖北银行',
  'JXBANK': '嘉兴银行',
  'HRXJB': '华融湘江银行',
  'BODD': '丹东银行',
  'AYCB': '安阳银行',
  'EGBANK': '恒丰银行',
  'CDB': '国家开发银行',
  'TCRCB': '江苏太仓农村商业银行',
  'NJCB': '南京银行',
  'ZZBANK': '郑州银行',
  'DYCB': '德阳商业银行',
  'YBCCB': '宜宾市商业银行',
  'SCRCU': '四川省农村信用',
  'KLB': '昆仑银行',
  'LSBANK': '莱商银行',
  'YDRCB': '尧都农商行',
  'CCQTGB': '重庆三峡银行',
  'FDB': '富滇银行',
  'JSRCU': '江苏省农村信用联合社',
  'JNBANK': '济宁银行',
  'CMB': '招商银行',
  'JINCHB': '晋城银行JCBANK',
  'FXCB': '阜新银行',
  'WHRCB': '武汉农村商业银行',
  'HBYCBANK': '湖北银行宜昌分行',
  'TZCB': '台州银行',
  'TACCB': '泰安市商业银行',
  'XCYH': '许昌银行',
  'CEB': '中国光大银行',
  'NXBANK': '宁夏银行',
  'HSBANK': '徽商银行',
  'JJBANK': '九江银行',
  'NHQS': '农信银清算中心',
  'MTBANK': '浙江民泰商业银行',
  'LANGFB': '廊坊银行',
  'ASCB': '鞍山银行',
  'KSRB': '昆山农村商业银行',
  'YXCCB': '玉溪市商业银行',
  'DLB': '大连银行',
  'DRCBCL': '东莞农村商业银行',
  'GCB': '广州银行',
  'NBBANK': '宁波银行',
  'BOYK': '营口银行',
  'SXRCCU': '陕西信合',
  'GLBANK': '桂林银行',
  'BOQH': '青海银行',
  'CDRCB': '成都农商银行',
  'QDCCB': '青岛银行',
  'HKBEA': '东亚银行',
  'HBHSBANK': '湖北银行黄石分行',
  'WZCB': '温州银行',
  'TRCB': '天津农商银行',
  'QLBANK': '齐鲁银行',
  'GDRCC': '广东省农村信用社联合社',
  'ZJTLCB': '浙江泰隆商业银行',
  'GZB': '赣州银行',
  'GYCB': '贵阳市商业银行',
  'CQBANK': '重庆银行',
  'DAQINGB': '龙江银行',
  'CGNB': '南充市商业银行',
  'SCCB': '三门峡银行',
  'CSRCB': '常熟农村商业银行',
  'SHBANK': '上海银行',
  'JLBANK': '吉林银行',
  'CZRCB': '常州农村信用联社',
  'BANKWF': '潍坊银行',
  'ZRCBANK': '张家港农村商业银行',
  'FJHXBC': '福建海峡银行',
  'ZJNX': '浙江省农村信用社联合社',
  'LZYH': '兰州银行',
  'JSB': '晋商银行',
  'BOHAIB': '渤海银行',
  'CZCB': '浙江稠州商业银行',
  'YQCCB': '阳泉银行',
  'SJBANK': '盛京银行',
  'XABANK': '西安银行',
  'BSB': '包商银行',
  'JSBANK': '江苏银行',
  'FSCB': '抚顺银行',
  'HNRCU': '河南省农村信用',
  'COMM': '交通银行',
  'XTB': '邢台银行',
  'CITIC': '中信银行',
  'HXBANK': '华夏银行',
  'HNRCC': '湖南省农村信用社',
  'DYCCB': '东营市商业银行',
  'ORBANK': '鄂尔多斯银行',
  'BJRCB': '北京农村商业银行',
  'XYBANK': '信阳银行',
  'ZGCCB': '自贡市商业银行',
  'CDCB': '成都银行',
  'HANABANK': '韩亚银行',
  'CMBC': '中国民生银行',
  'LYBANK': '洛阳银行',
  'GDB': '广东发展银行',
  'ZBCB': '齐商银行',
  'CBKF': '开封市商业银行',
  'H3CB': '内蒙古银行',
  'CIB': '兴业银行',
  'CRCBANK': '重庆农村商业银行',
  'SZSBK': '石嘴山银行',
  'DZBANK': '德州银行',
  'SRBANK': '上饶银行',
  'LSCCB': '乐山市商业银行',
  'JXRCU': '江西省农村信用',
  'ICBC': '中国工商银行',
  'JZBANK': '晋中市商业银行',
  'HZCCB': '湖州市商业银行',
  'NHB': '南海农村信用联社',
  'XXBANK': '新乡银行',
  'JRCB': '江苏江阴农村商业银行',
  'YNRCC': '云南省农村信用社',
  'ABC': '中国农业银行',
  'GXRCU': '广西省农村信用',
  'PSBC': '中国邮政储蓄银行',
  'BZMD': '驻马店银行',
  'ARCU': '安徽省农村信用社',
  'GSRCU': '甘肃省农村信用',
  'LYCB': '辽阳市商业银行',
  'JLRCU': '吉林农信',
  'URMQCCB': '乌鲁木齐市商业银行',
  'XLBANK': '中山小榄村镇银行',
  'CSCB': '长沙银行',
  'JHBANK': '金华银行',
  'BHB': '河北银行',
  'NBYZ': '鄞州银行',
  'LSBC': '临商银行',
  'BOCD': '承德银行',
  'SDRCU': '山东农信',
  'NCB': '南昌银行',
  'TCCB': '天津银行',
  'WJRCB': '吴江农商银行',
  'CBBQS': '城市商业银行资金清算中心',
  'HBRCU': '河北省农村信用社'
}");
            #endregion
            JObject jReturn = new JObject();
          


    

            try
            {
                string url = "https://ccdcapi.alipay.com/validateAndCacheCardInfo.json?_input_charset=utf-8&cardNo=" + BankCardCode + "&cardBinCheck=true";

                WebClient wc = new WebClient();


                Byte[] pageData = wc.DownloadData(url);
                string pageHtml = Encoding.UTF8.GetString(pageData);

                JObject jPost = JObject.Parse(pageHtml);
                jReturn["re"] = "ok";
                jReturn["BankName"] = jBank[jPost["bank"].ToString()].ToString();

                jReturn["BankId"] = jPost["bank"];
                jReturn["BankLogo"] = "https://apimg.alipay.com/combo.png?d=cashier&t=" + jPost["bank"] + "";
            }
            catch (Exception ex)
            {

                jReturn["re"] = "notok";
                jReturn["BankName"] = "";
                jReturn["eror"] = ex.Message;
               
            }

          

            return jReturn;

        }


    


        #endregion


        #region 语音合成

        public string GetSoundToken()
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id=0A7LXbqLhlRKucTMcvRKOfGreapAsg2Y&client_secret=347mVoDyqe5TksoGhfKSCa4o3hfGZtxp");



            JsonData Extra = new JsonData();
            Extra = JsonMapper.ToObject(reply);


            return Extra["access_token"].ToString();

        }



        public string GetSoundReadUrl(string Str)
        {

            string s = "http://tsn.baidu.com/text2audio?tex=" + Str + "&lan=zh&cuid=wangli&ctp=1&tok=" + GetSoundToken() + "";

            return s;
        }

        #endregion

        #region 地理位置


        /// <summary>
        /// 返回地理位置分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="pagesize"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataSet GetSitePageList(string strWhere, string Order, int CurrentPage, int pagesize, string col)
        {

            DAL.SiteDAL dal = new DAL.SiteDAL();
            return dal.GetPageList(strWhere, Order, CurrentPage, pagesize, col);

        }


        /// <summary>
        /// 保存一个地址
        /// </summary>
        /// <param name="model"></param>
        public void SaveSite(Model.SiteModel model)
        {

            DAL.SiteDAL dal = new DAL.SiteDAL();

            if (model.SiteId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }


        /// <summary>
        /// 保存一个site明细
        /// </summary>
        /// <param name="model"></param>
        public void SaveSiteDetail(Model.SiteDetailModel model)
        {
            if (model.SiteId == 0)
            {
                throw new Exception("SiteId为空,那还保存什么SiteDetail?");

            }

            DAL.SiteDetailDAL dal = new DAL.SiteDetailDAL();
            if (model.SiteDetailId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }


        #endregion

        #region 通知

        public void SendNotice(Model.NoticeModel model, List<Model.NoticeTargetModel> TargetModelList, decimal MerId)
        {


            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfigCache(MerId, 2000);
            DAL.NoticeTargetDAL tarDal = new DAL.NoticeTargetDAL();
            DAL.NoticeDAL dal = new DAL.NoticeDAL();

            dal.Add(model);


            foreach (Model.NoticeTargetModel TargetModel in TargetModelList)
            {
                TargetModel.NoticeId = model.NoticeId;

                string reJson = RongCloudServer.PublishMessage(MerConfig["RongAppKey"],
                         MerConfig["RongAppSecret"],
                         "messager",
                          TargetModel.TargetId,
                         "RC:TxtMsg", //消息类型
                         " {\"content\":\"" + model.NoticeContent + "\",\"extra\":" + model.Extra + "}" //消息内容
                         , model.NoticeTitle);
                TargetModel.NoticeStatus = 10; //已发送
                tarDal.Add(TargetModel);
            }





        }


        #endregion

        #region 广告相关


        public void SaveLocation(Model.LocationModel model)
        {

            DAL.LocationDAL dal = new DAL.LocationDAL();

            if (model.LocationId.Trim() == "")
            {
                model.LocationId = Common.TimeString.GetNow_ff();
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }


        public void SavePageInfo(Model.PageInfoModel model)
        {

            DAL.PageInfoDAL dal = new DAL.PageInfoDAL();
            if (model.PageId.Trim() == "")
            {
                model.PageId = Common.TimeString.GetNow_ff();
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }


        /// <summary>
        /// 保存一组广告
        /// </summary>
        /// <param name="model"></param>
        public void SaveAd(Model.AdInfoModel model)
        {
            DAL.AdInfoDAL dal = new DAL.AdInfoDAL();
            if (model.AdId == 0)
            {

                dal.Add(model);
            }
            else
            {//修改 

                dal.Update(model);
            }
        }


        public DataSet GetLocationList(string WhereStr)
        {
            DAL.LocationDAL dal = new DAL.LocationDAL();

            return dal.GetList(WhereStr);

        }

        #endregion

        #region 页面元素


        public DataSet GetIndexDataPageList(string strWhere, int CurrentPage)
        {

            DAL.IndexDataDAL dal = new DAL.IndexDataDAL();
            return dal.GetPageList(strWhere, CurrentPage, 20);

        }


        public Model.IndexDataModel GetIndexDataModel(decimal AutoId)
        {
            DAL.IndexDataDAL dal = new DAL.IndexDataDAL();
            return dal.GetModel(AutoId);
        }
        /// <summary>
        /// 添加一个新的页面元素
        /// </summary>
        /// <param name="model"></param>
        public void SaveIndexDataInfo(Model.IndexDataModel model)
        {
            DAL.IndexDataDAL dal = new DAL.IndexDataDAL();
            if (model.AutoId == 0)
            {
                dal.Add(model);

            }
            else
            {
                dal.Update(model);

            }



        }

        #endregion

        #region 图片相关
        /// <summary>
        /// 根据图片ID删除图片
        /// </summary>
        /// <param name="ImgId"></param>
        public static void DelImageById(string ImgId)
        {

            StringBuilder s = new StringBuilder();

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                string ImgUrl = DAL.DalComm.ExStr(" select ImgUrl from CORE.dbo.ImageInfo WITH(NOLOCK)  where   ImgId='" + ImgId + "' ");

                s.Append(" delete from dbo.ImageInfo where ImgId='" + ImgId + "' and ImgType <> 'system' ");
                int i = DAL.DalComm.ExReInt(s.ToString());
                if (i > 0)
                {
                    Common.FileString.FileDel(ImgUrl);
                }


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion





        }


        /// <summary>
        /// 根据路径删除图片
        /// </summary>
        /// <param name="ImgUrl"></param>
        public static void DelImageByUrl(string ImgUrl)
        {

            StringBuilder s = new StringBuilder();

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                s.Append(" delete from dbo.ImageInfo where ImgUrl='" + ImgUrl + "' and ImgType <> 'system' ");

                int i = DAL.DalComm.ExReInt(s.ToString());
                if (i > 0)
                {
                    Common.FileString.FileDel(ImgUrl);
                }
                else
                {
                    i = DAL.DalComm.ExInt(" select count(0) from dbo.ImageInfo WITH(NOLOCK)   ImgUrl='" + ImgUrl + "' ");
                    if (i == 0)
                    {
                        //就不在数据库中,直接删除
                        Common.FileString.FileDel(ImgUrl);

                    }
                }


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion


        }




        /// <summary>
        /// 添加新提醒
        /// </summary>
        /// <param name="model"></param>
        public static void AddNewRemind(Model.RemindModel model)
        {

            model.CreateTime = DateTime.Now;
            model.RemindId = Common.TimeString.GetNow_ff();
            DAL.RemindDAL dal = new DAL.RemindDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 添加新动态
        /// </summary>
        /// <param name="model"></param>
        public static void AddDynamic(Model.DynamicModel model)
        {
            model.CreateTime = DateTime.Now;
            model.FlagInvalid = false;
            DAL.DynamicDAL dal = new DAL.DynamicDAL();

            dal.Add(model);

        }




        /// <summary>
        /// 快捷输出点评配置
        /// </summary>
        /// <param name="CommentType"></param>
        /// <returns></returns>
        public static string CommentConfigJson(string CommentType)
        {
            StringBuilder w = new StringBuilder();
            w.Append("var cj = {");
            switch (CommentType.ToLower())
            {

                case "mer":

                    w.Append(" CommentType: 'mer',");
                    w.Append(" MerchantId: '" + Common.PageInput.ReStr("MerchantId") + "'  ");



                    break;

                case "pro":

                    w.Append(" CommentType: 'pro',");
                    w.Append(" ProId: '" + Common.PageInput.ReStr("ProId") + "'");

                    break;
            }
            w.Append("};");

            return w.ToString();

        }



        /// <summary>
        /// 保存一个新的提醒
        /// </summary>
        /// <param name="model"></param>
        public void SaveReMind(Model.RemindModel model)
        {
            model.RemindId = Common.TimeString.GetNow_ff();
            if (model.ReUserId.Trim() == Common.CookieSings.GetCurrentUserId().Trim())
            {
                //如果被提醒的用户就是当前我登录的用户, 那就无需提醒了吧

                return;
            }
            DAL.RemindDAL dal = new DAL.RemindDAL();
            dal.Add(model);

        }


        /// <summary>
        /// 获得点评信息
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public DataSet GetCommentPageList(string s, int c, int t)
        {
            DAL.CommentDAL dal = new DAL.CommentDAL();
            DataSet ds = dal.GetPageList(s.ToString(), c, t);
            return ds;

        }

        public DataSet GetCommentRepList(string s, int c)
        {
            DAL.CommentDAL dal = new DAL.CommentDAL();

            DataSet ds = dal.GetProCommentPageList(s, c, 5);
            return ds;
        }

        #endregion

        #region 动态和关注


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public void AddNewDynamic(Model.DynamicModel model)
        {
            DAL.DynamicDAL dal = new DAL.DynamicDAL();
            dal.Add(model);
        }



        /// <summary>
        /// 检查是否关注了这个商家
        /// </summary>
        /// <param name="AttentionMerId"></param>
        /// <returns></returns>
        public bool IsAttentionMer(decimal AttentionMerId)
        {

            DAL.AttentionDAL dal = new DAL.AttentionDAL();

            int i = dal.ExInt(" UserId='" + Common.CookieSings.GetCurrentUserId() + "' and AttentionMerId= '" + AttentionMerId + "' ");

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否关注了这个用户
        /// </summary>
        /// <param name="AttentionUserId"></param>
        /// <returns></returns>
        public bool IsAttentionUser(string AttentionUserId)
        {
            DAL.AttentionDAL dal = new DAL.AttentionDAL();

            int i = dal.ExInt(" UserId='" + Common.CookieSings.GetCurrentUserId() + "' and AttentionUserId= '" + AttentionUserId + "' ");

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void DeleteAttention(string strWhere)
        {
            DAL.AttentionDAL dal = new DAL.AttentionDAL();
            dal.DeleteList(strWhere);
        }
        /// <summary>
        /// 添加用户关注
        /// </summary>
        public void AddAttentionUser(string AttentionUserId)
        {

            DAL.AttentionDAL dal = new DAL.AttentionDAL();
            DataTable dt = dal.GetList("  ").Tables[0];
            if (!IsAttentionUser(AttentionUserId))
            {
                Model.AttentionModel model = new AttentionModel();
                model.AttentionMerId = 0;
                model.AttentionType = 1;
                model.AttentionUserId = AttentionUserId;
                model.DynamicLv = 1;
                model.FlagInvalid = false;
                model.UserId = Common.CookieSings.GetCurrentUserId();
                dal.Add(model);
            }
            else
            {
                throw new Exception("您已经关注了这个用户!");
            }

        }

        /// <summary>
        /// 添加商家关注
        /// </summary>
        public void AddAttentionMer(decimal MerId)
        {


            DAL.AttentionDAL dal = new DAL.AttentionDAL();
            DataTable dt = dal.GetList("  UserId='" + Common.CookieSings.GetCurrentUserId() + "' and AttentionMerId= '" + MerId + "'").Tables[0];
            if (!IsAttentionMer(MerId))
            {
                Model.AttentionModel model = new AttentionModel();
                model.AttentionMerId = MerId;
                model.AttentionType = 2;
                model.AttentionUserId = "";
                model.DynamicLv = 1;
                model.FlagInvalid = false;
                model.UserId = Common.CookieSings.GetCurrentUserId();
                dal.Add(model);
            }
            else
            {
                throw new Exception("您已经关注了这个商家!");
            }


        }



        #endregion

        #region 点评留言

        public void SaveInformationVsComment(Model.InformationVsCommentModel model)
        {
            DAL.InformationVsCommentDAL dal = new DAL.InformationVsCommentDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 添加一个商家点评
        /// </summary>
        /// <param name="model"></param>
        public void SaveNewMerVsDal(MerVsCommentModel model)
        {

            MerCDal.Add(model);


        }




        /// <summary>
        /// 添加一条房产点评数据
        /// </summary>
        /// <param name="model"></param>
        public void SaveHouseVsComment(Model.HouseVsCommentModel model)
        {
            DAL.HouseVsCommentDAL dal = new DAL.HouseVsCommentDAL();

            dal.Add(model);


        }

        /// <summary>
        /// 添加一个职位点评
        /// </summary>
        /// <param name="model"></param>
        public void SaveNewJobVsDal(JobVsCommentModel model)
        {

            JvCDal.Add(model);


        }


        public void SaveNewComment(CommentModel model)
        {

            if (model.CommentId > 0)
            {//添加 
                CDal.Add(model);
            }
            else
            {
                //修改
                CDal.Add(model);
            }

        }

        #endregion

        #region 点评列表
        /// <summary>
        /// 获得点评列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        public DataSet GetRemindPageList(string strWhere, int CurrentPage)
        {
            DAL.RemindDAL dal = new DAL.RemindDAL();
            return dal.GetPageList(strWhere, CurrentPage, 20);
        }
        #endregion

    }
}
