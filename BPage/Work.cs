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


namespace BPage
{
    public class Work : Common.BPageSetting2
    {
        public void ProcessRequest(HttpContext context)
        {

            try
            {
                string para = ReStr("para");
                switch (para)
                {

                   

                    case "ChangeAdGroupNo":
                        ChangeAdGroupNo();
                        break;

                    case "ChangeAdOrderNo":
                        ChangeAdOrderNo();
                        break;
                    case "SaveLocation":
                        SaveLocation();
                        break;

                    case "SavePageInfo":
                        SavePageInfo();
                        break;

                    case "AddLocation":
                        AddLocation();
                        break;

                    case "doRecommend":
                        doRecommend();
                        break;

                    case "SaveSlideShowInfo":
                        SaveSlideShowInfo();               //保存一个幻灯片
                        break;


                    case "GetSlideShowList":
                        GetSlideShowList();
                        break;

                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();



        }


        public void test1()
        {
            #region 事务开启
     
             TransactionOptions transactionOption = new TransactionOptions();

            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;
            

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                 

                test2();


                #region 事务关闭

                transactionScope.Complete();
            

            }
            #endregion
        }

        public void test2()
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(0, transactionOption))
            {
                #endregion





                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        private void ChangeAdOrderNo()
        {
            int OrderNo = ReInt("OrderNo", 0);
            decimal AdId = ReDecimal("AdId", 0);

            if (AdId == 0)
            {
                throw new Exception("AdId不能为0!");
            }

            DAL.DalComm.ExInt(" UPDATE CORE.dbo.AdInfo SET OrderNo='" + OrderNo + "' WHERE AdId='" + AdId + "' ");
            ReTrue();
        }

        private void ChangeAdGroupNo()
        {
            int AdGroupNo = ReInt("AdGroupNo", 0);
            decimal AdId = ReDecimal("AdId", 0);

            if (AdId == 0)
            {
                throw new Exception("AdId不能为0!");
            }

            DAL.DalComm.ExInt(" UPDATE CORE.dbo.AdInfo SET AdGroupNo='"+AdGroupNo+"' WHERE AdId='"+AdId+"' ");
            ReTrue();
        }

        private void SaveLocation()
        {
            Model.LocationModel model = new Model.LocationModel();
            model.LocationId = ReStr("LocationId", "");
            model.LocationName = ReStr("LocationName", "");
            if (model.LocationName == "")
            {
                throw new Exception("位置名称不能为空!");
            }
            model.Memo = ReStr("Memo", "");
            model.OrderNo = ReInt("OrderNo", 0);
            model.PageId = ReStr("PageId");
            model.LocationLabel = Common.PinYin.GetFullCodstring(model.LocationName);

            BLL.CommBLL bll = new BLL.CommBLL();
            bll.SaveLocation(model);
            ReTrue();
        }

        private void SavePageInfo()
        {

            Model.PageInfoModel model = new Model.PageInfoModel();
            model.PageId = ReStr("PageId", "");
            model.OrderNo = ReInt("OrderNo", 0);
            model.PageMemo = ReStr("PageMemo", "");
            model.PageName = ReStr("PageName", "");
            model.BranchId = ReStr("BranchId", "");
            model.ZoneId = ReStr("ZoneId", "0");

            if (model.ZoneId == "0")
            {
                throw new Exception("ZoneId不能为空!");
            }

            if (model.PageName == "")
            {
                throw new Exception("PageName不能为null");
            }

            model.PageLabel = Common.PinYin.GetFullCodstring(model.PageName);
            if (model.PageName == "")
            {
                throw new Exception("页面名称不能为空!");
            }
            model.MerId = ReDecimal("MerId", 0);
            BLL.CommBLL bll = new BLL.CommBLL();
            bll.SavePageInfo(model);
            ReTrue();

        }

        private void AddLocation()
        {

        }

        private void doRecommend()
        {

            DAL.RecommendDAL dal = new DAL.RecommendDAL();
            BLL.RecommendBLL bll = new BLL.RecommendBLL();


            Model.RecommendModel model = new Model.RecommendModel();
            model.RecommendId = ReStr("RecommendId", "");

            if (model.RecommendId != "")
            {
                model = dal.GetModel(model.RecommendId);
            }
            model.Invalid = false;
            model.RecommendMemo = ReStr("RecommendMemo", "");
            model.RecommendType = ReStr("RecommendType", "");
            model.ReKey = ReStr("ReKey");
            model.BgTime = ReTime("BgTime");
            model.EndTime = ReTime("EndTime");

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                bll.doRecommend(model); //添加一条推广信息
                int RecommendLv = ReInt("RecommendLv");

                switch (model.RecommendType)
                {
                    case "职位":
                        DAL.DalComm.ExReInt(" update dbo.Job set RecommendLv='" + RecommendLv + "' where JobId='" + model.ReKey + "' ");
                        break;


                    case "房源":
                        DAL.DalComm.ExReInt(" update dbo.House set RecommendLv='" + RecommendLv + "' where HouseId='" + model.ReKey + "' ");
                        break;



                    case "供求":

                        DAL.DalComm.ExReInt(" update dbo.Information set RecommendLv='" + RecommendLv + "' where InformationId='" + model.ReKey + "' ");
                        break;

                }


                #region 事务结束

                transactionScope.Complete();


            }
            #endregion


            ReTrue();
        }


        /// <summary>
        /// 获得分页查询
        /// </summary>
        private void GetSlideShowList()
        {
            BLL.WorkSys.SlideShowBLL bll = new BLL.WorkSys.SlideShowBLL();
            StringBuilder s = new StringBuilder();
            s.Append("");
            int CurrentPage = ReInt("CurrentPage");
            DataSet ds = bll.GetSlideShowList(s.ToString(), CurrentPage);
            RePage(ds);
        }



        /// <summary>
        /// 保存一组幻灯片
        /// </summary>
        private void SaveSlideShowInfo()
        {

            Model.SlideShowModel model = new Model.SlideShowModel();
            model.SlideshowId = ReDecimal("SlideshowId");
            model.SlideshowTitle = ReStr("SlideshowTitle");
            model.SlideshowMemo = ReStr("SlideshowMemo");
            model.CreateTime = ReTime("CreateTime");
            model.CreateUser = ReStr("CreateUser");
            model.OrderNo = ReInt("OrderNo");
            model.lv = ReInt("lv");
            model.SlideshowType = ReStr("SlideshowType");
            model.SlideshowImgId = ReStr("SlideshowImgId");
            model.Url = ReStr("Url");
            model.Event = ReStr("Event");
            BLL.WorkSys.SlideShowBLL bll = new BLL.WorkSys.SlideShowBLL();

            bll.SaveSlideShowInfo(model);
            ReTrue();

        }
    }
}
