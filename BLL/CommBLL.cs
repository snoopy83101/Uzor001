using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Transactions;
using io.rong;
using LitJson;
using System.Net;

namespace BLL
{
    public class CommBLL
    {


        DAL.MerVsCommentDAL MerCDal = new DAL.MerVsCommentDAL();
        DAL.CommentDAL CDal = new DAL.CommentDAL();
        DAL.JobVsCommentDAL JvCDal = new DAL.JobVsCommentDAL();


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
