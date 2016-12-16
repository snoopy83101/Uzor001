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
using Model;
using LitJson;

namespace BPage
{
    public class Order : Common.BPageSetting2
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string para = ReStr("para");
                switch (para)
                {

        

                    case "ClearMemberOrderToWork":
                        ClearMemberOrderToWork();
                        break;

                    case "CountMinQuantity":
                        CountMinQuantity();
                        break;


                    case "CountPlanningTime2":
                        CountPlanningTime2();
                        break;

                    case "CountPlanningTime":
                        CountPlanningTime();
                        break;

                    case "ChangeOrderDetail":
                        ChangeOrderDetail();  //更改订单明细 :比如更改颜色
                        break;

                    case "RemoveOrderDetail":
                        RemoveOrderDetail();
                        break;

                    case "GetOrderLogList":
                        GetOrderLogList();
                        break;

                    case "PendingOrderToWork":
                        PendingOrderToWork();
                        break;

                    case "PayOrder":
                        PayOrder();
                        break;

                    case "SaveCheckNumArray":
                        SaveCheckNumArray();
                        break;
                    case "CheckOrderToWork":
                        CheckOrderToWork();
                        break;

                    case "AllotOrderToWork":
                        AllotOrderToWork();
                        break;

                    case "SaveDoneNumArray":
                        SaveDoneNumArray();   //批量提交完成数量
                        break;

                    case "SaveDoneNum":
                        SaveDoneNum();
                        break;
                    case "CountOrder":
                        CountOrder();
                        break;
                    case "SaveOrderDetailVsClothesSize":
                        SaveOrderDetailVsClothesSize();
                        break;

                    case "OrderExpect":
                        OrderExpect();
                        break;

                    case "DoneOrderToWork":
                        DoneOrderToWork();
                        break;

                    case "RemoveOrderVsMember":
                        RemoveOrderVsMember();
                        break;


                    case "SaveOrderToWorkDetail":
                        SaveOrderToWorkDetail();
                        break;
                    case "CountOrderToWork":
                        CountOrderToWork();
                        break;
                    case "ChangeOrderToWork":
                        ChangeOrderToWork();
                        break;
                    case "ToAddOrderToWork":
                        ToAddOrderToWork();//插入一条参数默认的工单
                        break;

                    case "SaveOrderVsMember":
                        SaveOrderVsMember();
                        break;


                    case "ChangeOrderStatus":
                        ChangeOrderStatus();
                        break;

                    case "GetClothesSize":
                        GetClothesSize();
                        break;
                    case "SaveOrderDetail":
                        SaveOrderDetail();
                        break;
                    case "GetOrderList":
                        GetOrderList();
                        break;

                    case "AddOrderInfo":
                        AddOrderInfo();
                        break;
                    case "SaveOrderInfo":
                        SaveOrderInfo();
                        break;


                    case "GetOrderInfo":
                        GetOrderInfo();
                        break;

                    case "GetOrderToWorkInfo":
                        GetOrderToWorkInfo();
                        break;

                    case "GetOrderToWorkList":
                        GetOrderToWorkList();
                        break;
                    case "GetOrderToWorkPageList":
                        GetOrderToWorkPageList();
                        break;
                    case "GetOrderDetail":
                        GetOrderDetail();
                        break;

                    case "Get":
                        break;
                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();


        }

        private void ClearMemberOrderToWork()
        {
            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);


            BLL.OrderBLL bll = new BLL.OrderBLL();
            if (OrderToWorkId != 0)
            {

                bll.ClearMemberOrderToWork(OrderToWorkId);
            }
            else
            {

                decimal MemberId = ReDecimal("MemberId", 0);
                string OrderId = ReStr("OrderId", "");

                bll.ClearMemberOrderToWork(OrderId, MemberId);
            }

            ReTrue();
        }

        private void CountMinQuantity()
        {

            decimal OrderQuantity = ReDecimal("OrderQuantity", 0);
            int Places = ReInt("Places", 0);
            decimal d = BLL.FormulaBLL.MinQuantity(OrderQuantity, Places);


            ReDict2.Add("MinQuantity", d.ToString());
            ReTrue();

        }

        private void CountPlanningTime2()
        {
            int PlanningDay = ReInt("PlanningDay", 0);
            DateTime ReceivedTime = ReTime("ReceivedTime", DateTime.Now);

            DateTime PlanningTime = BLL.FormulaBLL.PlanningTime(PlanningDay, ReceivedTime);

            ReDict2.Add("PlanningTime", PlanningTime.ToString("yyyy-MM-dd HH:dd:ss"));


            ReTrue();




        }

        private void CountPlanningTime()
        {




            decimal OrderQuantity = ReDecimal("OrderQuantity", 0);
            decimal OrderExpectDay1 = ReDecimal("OrderExpectDay1", 0);
            decimal OrderExpectDay2 = ReDecimal("OrderExpectDay2", 0);
            decimal OrderExpectDay3 = ReDecimal("OrderExpectDay3", 0);

            int Places = ReInt("Places", 0);
            DateTime ReceivedTime = ReTime("ReceivedTime", DateTime.Now);
            var j = BLL.FormulaBLL.PlanningTime(OrderQuantity, OrderExpectDay1, OrderExpectDay2, OrderExpectDay3, Places, ReceivedTime);

            ReTrue(j);


        }

        private void ChangeOrderDetail()
        {
            string Color = ReStr("Color", "");
            decimal OrderDetailId = ReDecimal("OrderDetailId", 0);
            if (Color.Trim() == "")
            {
                throw new Exception("颜色不能为空!");
            }

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion




                BLL.OrderBLL bll = new BLL.OrderBLL();

                StringBuilder s = new StringBuilder();
                s.Append(" SELECT * FROM dbo.OrderDetail WHERE OrderDetailId='" + OrderDetailId + "' ");

                s.Append(" UPDATE dbo.OrderDetail SET Color='" + Color + "' WHERE OrderDetailId='" + OrderDetailId + "'  ");

                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有找到订单明细!");
                }
                DataRow dr = dt.Rows[0];
                string OldColor = dr["Color"].ToString();


                #region 记录日志
                BLL.UserBLL ubll = new BLL.UserBLL();
                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = dr["OrderId"].ToString();
                LogModel.OrderLogClassId = 10010;
                LogModel.OrderLogTitle = "操作员更改了颜色, 将[" + OldColor + "]更改为[" + Color + "]";
                LogModel.OrderToWorkId = 0;
                LogModel.MemberId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                try
                {
                    LogModel.UserId = ubll.CurrentUserId();

                }
                catch (Exception)
                {


                }

                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            ReTrue();
        }

        private void RemoveOrderDetail()
        {
            decimal OrderDetailId = ReDecimal("OrderDetailId", 0);


            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.RemoveOrderDetail(OrderDetailId);

            ReTrue();
        }

        private void GetOrderLogList()
        {
            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " OrderLogId desc ");
            DAL.OrderLogDAL dal = new DAL.OrderLogDAL();
            StringBuilder s = new StringBuilder();
            string OrderId = ReStr("OrderId", "");
            if (OrderId == "")
            {
                throw new Exception("OrderId不能为空!");
            }
            s.Append(" 1=1 ");

            s.Append(" and OrderId='" + OrderId + "' ");

            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);
            RePage2(ds);
        }

        private void PendingOrderToWork()  //结算
        {
            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);
            if (OrderToWorkId == 0)
            {
                throw new Exception("OrderToWorkId不能为空!");
            }
            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.PendingOrderToWork(OrderToWorkId);
            ReTrue();
        }

        private void PayOrder()
        {
            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);
            if (OrderToWorkId == 0)
            {
                throw new Exception("OrderToWorkId不能为空!");
            }
            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.PayOrder(OrderToWorkId);
            ReTrue();

        }

        private void SaveCheckNumArray()
        {
            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);
            if (OrderToWorkId == 0)
            {
                throw new Exception("OrderToWorkId不能为空!");
            }

            DataTable dt = ReTable("CheckNumArray");


            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.SaveCheckNumArray(OrderToWorkId, dt);
            ReTrue();
        }

        private void CheckOrderToWork()
        {
            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);
            if (OrderToWorkId == 0)
            {
                throw new Exception("OrderToWorkId不能为空!");
            }

            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.CheckOrderToWork(OrderToWorkId);
            ReTrue();
        }

        private void SaveDoneNumArray()
        {
            DataTable dt = ReTable("DoneNumArray");

            BLL.OrderBLL bll = new BLL.OrderBLL();


            bll.SaveDoneNumArray(dt);
            ReTrue();
        }

        private void AllotOrderToWork()
        {

            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);
            if (OrderToWorkId == 0)
            {
                throw new Exception("OrderToWorkId不能为空!");
            }
            DataTable dtColor = ReTable("ColorArray");

            dtColor = Common.DataSetting.Distinct(dtColor, new string[] { "Color" });

            DataTable dtVs = ReTable("VsArray");

            List<Model.OrderToWorkDetailModel> detailList = new List<Model.OrderToWorkDetailModel>();
            List<Model.OrderToWorkDetailVsClothesSizeModel> vsList = new List<Model.OrderToWorkDetailVsClothesSizeModel>();

            if (dtColor != null)
            {

                if (dtColor.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtColor.Rows)
                    {
                        Model.OrderToWorkDetailModel model = new Model.OrderToWorkDetailModel();
                        model.OrderToWorkId = OrderToWorkId;
                        model.Color = dr["Color"].ToString();
                        detailList.Add(model);
                    }


                }
                else
                {
                    throw new Exception("颜色不能为空!");
                }


            }
            else
            {
                throw new Exception("颜色不能为null");
            }


            if (dtVs != null)
            {
                if (dtVs.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtVs.Rows)
                    {
                        Model.OrderToWorkDetailVsClothesSizeModel model = new Model.OrderToWorkDetailVsClothesSizeModel();
                        model.ChangeTime = DateTime.Now;
                        model.Num = decimal.Parse(dr["Num"].ToString());
                        model.Memo = dr["Color"].ToString(); //颜色备注
                        model.ClothesSizeId = int.Parse(dr["ClothesSizeId"].ToString());
                        vsList.Add(model);

                    }

                }
                else
                {
                    throw new Exception("分派数据不能为0");

                }
            }
            else
            {

                throw new Exception("分派数据不能为null");
            }
            BLL.OrderBLL bll = new BLL.OrderBLL();

            bll.AllotOrderToWork(detailList, vsList);
            ReTrue();

        }

        private void SaveDoneNum()
        {
            BLL.OrderBLL bll = new BLL.OrderBLL();

            decimal DoneNum = ReDecimal("DoneNum", 0);

            decimal OrderToWorkDetailId = ReDecimal("OrderToWorkDetailId", 0);  //颜色

            int ClothesSizeId = ReInt("ClothesSizeId", 0);   //尺码

            int DoneLogTypeId = ReInt("DoneLogTypeId", 0);//10为自己上传, 20为后台用户更改, 不能为0
            if (DoneLogTypeId == 0)
            {
                throw new Exception("DoneLogTypeId不能为0!");
            }

            bll.SaveDoneNum(DoneNum, OrderToWorkDetailId, ClothesSizeId, DoneLogTypeId);
        }

        private void CountOrder()
        {
            string OrderId = ReStr("OrderId", "");
            BLL.OrderBLL bll = new BLL.OrderBLL();

            DataSet ds = bll.CountOrder(OrderId);
            DataTable dt = ds.Tables[0];

            ReDict.Add("Count", JsonHelper.ToJsonNo1(dt));
            ReTrue();
        }

        private void SaveOrderDetailVsClothesSize()
        {

            string OrderId = ReStr("OrderId", "");

            if (OrderId == "")
            {
                throw new Exception("OrderId不能为空!");
            }
            Model.OrderDetailVsClothesSizeModel model = new Model.OrderDetailVsClothesSizeModel();
            model.OrderDetailId = ReDecimal("OrderDetailId", 0);
            model.ClothesSizeId = ReInt("ClothesSizeId", 0);
            model.Num = ReDecimal("Num", 0);

            DAL.OrderDetailVsClothesSizeDAL dal = new DAL.OrderDetailVsClothesSizeDAL();

            StringBuilder s = new StringBuilder();

            s.Append(" SELECT OrderId ,Color ,ClothesSizeName FROM dbo.OrderDetailVsClothesSizeView WHERE ClothesSizeId=" + model.ClothesSizeId + " AND OrderDetailId=" + model.OrderDetailId + " ");

            s.Append(" DECLARE @ClothesSizeName AS VARCHAR(50) =(SELECT ClothesSizeName FROM  dbo.ClothesSize WHERE ClothesSizeId=" + model.ClothesSizeId + ")  ");
            s.Append(" DECLARE @Color AS VARCHAR(50) =(SELECT Color FROM  dbo.OrderDetail WHERE OrderDetailId=" + model.OrderDetailId + ")  ");
            s.Append(" SELECT @ClothesSizeName AS ClothesSizeName,@Color AS Color  ");
            s.Append("  UPDATE  dbo.OrderDetailVsClothesSize  SET Num=" + model.Num + " WHERE ClothesSizeId=" + model.ClothesSizeId + " AND OrderDetailId=" + model.OrderDetailId + "  ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            DataTable dtStr = ds.Tables[1];
            DataRow drStr = dtStr.Rows[0];
            if (dt.Rows.Count > 0)
            {
                #region 建立日志

                Model.OrderLogModel LogModel = new Model.OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 10010;
                LogModel.OrderLogTitle = "保存了数量明细: 颜色" + drStr["Color"].ToString() + ",尺码" + drStr["ClothesSizeName"].ToString() + ",数量" + model.Num + ".";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                LogModel.UserId = ubll.CurrentUserId();
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion
            }
            else
            {
                dal.Add(model);


                #region 建立日志

                Model.OrderLogModel LogModel = new Model.OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 10010;
                LogModel.OrderLogTitle = "新增了数量明细: 颜色" + drStr["Color"].ToString() + ",尺码" + drStr["ClothesSizeName"].ToString() + ",数量" + model.Num + ".";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                LogModel.UserId = ubll.CurrentUserId();
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion

            }







            ReTrue();





        }

        private void DoneOrderToWork()
        {
            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);

            BLL.OrderBLL bll = new BLL.OrderBLL();

            bll.DoneOrderToWork(OrderToWorkId);

            ReTrue();
        }

        private void CountOrderToWork()
        {

            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);


            BLL.OrderBLL bll = new BLL.OrderBLL();



            DataSet ds = bll.CountOrderToWork(OrderToWorkId);


            DataTable dt = ds.Tables[0];
            ReDict.Add("Count", JsonHelper.ToJsonNo1(dt));
            ReTrue();

        }

        private void OrderExpect()
        {
            Model.OrderExpectModel model = new Model.OrderExpectModel();
            model.Num = ReDecimal("Num", 0);
            model.OrderExpectDay = ReInt("OrderExpectDay", 0);
            model.OrderId = ReStr("OrderId", "");

            DAL.OrderExpectDAL dal = new DAL.OrderExpectDAL();



            StringBuilder s = new StringBuilder();
            dal.Update(model);
            //  s.Append("  DELETE FROM dbo.OrderExpect WHERE OrderExpectDay=" + model.OrderExpectDay + " and OrderId='" + model.OrderId + "' ");

            s.Append(" DECLARE @MaxExpectNum AS INT =( SELECT MAX(Num) FROM dbo.OrderExpect WHERE OrderId='" + model.OrderId + "' ) ");
            s.Append(" UPDATE dbo.OrderInfo SET MaxExpectNum=@MaxExpectNum WHERE OrderId='" + model.OrderId + "'  ");

            DAL.DalComm.ExReInt(s.ToString());


            ReTrue();

        }

        private void ReleaseOrder()
        {
            string OrderId = ReStr("OrderId", "");

            if (OrderId == "")
            {
                throw new Exception("OrderId不能为空!");
            }


            StringBuilder s = new StringBuilder();
            s.Append("  ");

        }


        private void AddOrderInfo()
        {

            BLL.UserBLL ubll = new BLL.UserBLL();
            string UserId = ubll.CurrentUserId();
            Model.OrderInfoModel model = new Model.OrderInfoModel();




            StringBuilder s = new StringBuilder();

            s.Append(" SELECT OrderId FROM dbo.OrderInfo WHERE CreateUserId='" + UserId + "' AND OrderStatusId=5 ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];

            if (1 == 2)
            {
                DataRow dr = dt.Rows[0];

                model.OrderId = dr["OrderId"].ToString();

                //我以前发布过草稿了. 先转到以前的草稿
            }
            else
            {

                BLL.OrderBLL bll = new BLL.OrderBLL();
                model.OrderTitle = "外发订单[" + DateTime.Now.ToString("MM月dd日 HH:mm") + "]草稿 - " + UserId + "";
                model.CreateTime = DateTime.Now;
                model.OrderId = "";
                model.ProcessLvId = 20;
                model.Places = 1;
                model.ProcessLocationTypeId = 20;
                model.OrderClassId = 100; //缝制
                model.OrderStatusId = 5; //草稿
                model.Unit = "件";
                model.CreateUserId = UserId;
                model.PendingDay = 20;
                model.PlanningDay = 3;
                model.ReceivedTime = DateTime.Now.AddDays(1);

                bll.SaveOrderInfo(model);

            }






            ReDict2.Add("OrderId", model.OrderId);
            ReTrue();
        }

        private void RemoveOrderVsMember()
        {
            Model.OrderVsMemberModel model = new OrderVsMemberModel();

            model.MemberId = ReDecimal("MemberId", 0);
            model.OrderId = ReStr("OrderId", "");
            model.Memo = ReStr("Memo", "");
            string RemoveType = ReStr("RemoveType", "");


            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.RemoveOrderVsMember(model, RemoveType);



            ReTrue();



        }

        private void gettest()
        {


            decimal d = ReDecimal("1123", 1);

            DateTime date = new DateTime();

            date = DateTime.Now;

            ReTrue();

        }

        private void GetOrderToWorkInfo()
        {
            string OrderToWorkId = ReStr("OrderToWorkId", "");

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.OrderToWorkView WHERE OrderToWorkId='" + OrderToWorkId + "' ");

            s.Append(" SELECT otwd.* FROM dbo.OrderToWorkDetail otwd ");   //明细
            s.Append(" INNER JOIN dbo.OrderToWork otw ON otw.OrderToWorkId = otwd.OrderToWorkId ");
            s.Append(" WHERE otw.OrderToWorkId='" + OrderToWorkId + "' ");
            s.Append("  ");

            s.Append(" SELECT otwd.OrderToWorkDetailId,  otwd.Color,otwdvc.Num,otwdvc.CheckNum,otwdvc.DoneNum, otwdvc.ClothesSizeId,otw.MemberId,otw.OrderToWorkId FROM dbo.OrderToWorkDetailVsClothesSize otwdvc  "); //最全的明细(颜色)对应尺码
            s.Append(" INNER JOIN dbo.OrderToWorkDetail otwd ON otwd.OrderToWorkDetailId = otwdvc.OrderToWorkDetailId ");
            s.Append(" INNER JOIN dbo.OrderToWork otw ON otw.OrderToWorkId = otwd.OrderToWorkId ");
            s.Append(" WHERE otw.OrderToWorkId='" + OrderToWorkId + "' ");


            s.Append(" SELECT c.ClothesSizeName,c.ClothesSizeId FROM dbo.OrderToWorkDetailVsClothesSize v ");
            s.Append(" INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkDetailId = v.OrderToWorkDetailId ");
            s.Append(" INNER JOIN dbo.ClothesSize c ON c.ClothesSizeId = v.ClothesSizeId ");
            s.Append(" WHERE OrderToWorkId='" + OrderToWorkId + "' GROUP BY c.ClothesSizeName,c.ClothesSizeId ");


            s.Append(" SELECT * FROM dbo.OrderExpect WHERE OrderId=(SELECT TOP 1 OrderId FROM dbo.OrderToWork WHERE OrderToWorkId=" + OrderToWorkId + ") ");



            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtOrderToWork = ds.Tables[0];
            DataTable dtOrderToWorkDetail = ds.Tables[1];
            DataTable dtOrderToWorkDetailVsClothesSize = ds.Tables[2];
            DataTable dtGroupClothesSize = ds.Tables[3];
            DataTable dtOrderExpect = ds.Tables[4];
            ReDict.Add("OrderToWork", JsonHelper.ToJsonNo1(dtOrderToWork));
            ReDict.Add("OrderToWorkDetail", JsonHelper.ToJson(dtOrderToWorkDetail));
            ReDict.Add("OrderToWorkDetailVsClothesSize", JsonHelper.ToJson(dtOrderToWorkDetailVsClothesSize));
            ReDict.Add("GroupClothesSize", JsonHelper.ToJson(dtGroupClothesSize));
            ReDict.Add("OrderExpect", JsonHelper.ToJson(dtOrderExpect));

            ReTrue();
        }

        private void GetOrderToWorkPageList()
        {


            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " OrderToWorkStatusId,  CreateTime desc ");
            DAL.OrderToWorkDAL dal = new DAL.OrderToWorkDAL();

            decimal MemberId = ReDecimal("MemberId", 0);

            int OrderToWorkStatusId = ReInt("OrderToWorkStatusId", 0);
            DataSet ds;
            StringBuilder s = new StringBuilder();
            if (OrderToWorkStatusId != -100)
            {



                s.Append(" 1=1 ");


                if (MemberId != 0)
                {
                    s.Append(" and MemberId=" + MemberId + "");
                }

                if (OrderToWorkStatusId != 0)
                {
                    s.Append(" and OrderToWorkStatusId=" + OrderToWorkStatusId + "  ");
                }

                ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);
            }
            else
            {
                //如果是待派单
                DAL.OrderVsMemberDAL VsDal = new DAL.OrderVsMemberDAL();
                s.Append(" 1=1 ");
                s.Append(" and VsStatus BETWEEN 10 and 15 ");
                if (MemberId != 0)
                {
                    s.Append(" and MemberId=" + MemberId + "");
                }

                ds = VsDal.GetPageList(s.ToString(), "CreateTime desc ", CurrentPage, PageSize, " * ");

            }

            RePage2(ds);

        }

        private void SaveOrderToWorkDetail()
        {
            string Color = ReStr("Color", "");
            decimal MemberId = ReDecimal("MemberId", 0);
            decimal Num = ReDecimal("Num", 0);
            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);
            decimal DoneNum = ReDecimal("DoneNum", 0);
            if (OrderToWorkId == 0)
            {
                throw new Exception("OrderToWorkId不能为0!");
            }


            decimal CheckNum = ReDecimal("CheckNum", 0);

            if (CheckNum > Num)
            {

                throw new Exception("质检数量不能大于分派数量");
            }

            int ClothesSizeId = ReInt("ClothesSizeId", 0);

            BLL.OrderBLL bll = new BLL.OrderBLL();
            StringBuilder s = new StringBuilder();
            Model.OrderToWorkDetailModel dModel = new Model.OrderToWorkDetailModel();
            dModel.Color = Color;
            dModel.OrderToWorkId = OrderToWorkId;
            bll.SaveOrderToWorkDetail(dModel);

            Model.OrderToWorkDetailVsClothesSizeModel vModel = new Model.OrderToWorkDetailVsClothesSizeModel();
            vModel.Num = Num;
            vModel.OrderToWorkDetailId = dModel.OrderToWorkDetailId;
            vModel.CheckNum = CheckNum;
            vModel.ClothesSizeId = ClothesSizeId;
            vModel.DoneNum = DoneNum;
            vModel.ChangeTime = DateTime.Now;
            bll.SaveOrderToWorkDetailVsClothesSize(vModel);



            ReTrue();
        }

        private void ChangeOrderToWork()
        {
            string Val = ReStr("Val", "");

            string Key = ReStr("Key", "");

            decimal OrderToWorkId = ReDecimal("OrderToWorkId", 0);

            if (OrderToWorkId == 0)
            {
                throw new Exception("OrderToWorkId不能为空!");
            }

            DAL.DalComm.BackData(" UPDATE dbo.OrderToWork SET " + Key + "='" + Val + "' WHERE OrderToWorkId='" + OrderToWorkId + "' ");

            ReTrue();
        }

        private void ToAddOrderToWork()
        {
            string OrderId = ReStr("OrderId", "");
            decimal MemberId = ReDecimal("MemberId", 0);
            if (MemberId == 0)
            {
                throw new Exception("用户编号不能为空!");
            }

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();

                s.Append(" select * FROM dbo.OrderView WHERE OrderId='" + OrderId + "' ");

                s.Append(" SELECT Phone,RealName FROM dbo.Member WHERE MemberId=" + MemberId + " ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                DataTable dtMember = ds.Tables[1];
                DataRow drMember = dtMember.Rows[0];
                DataRow dr = dt.Rows[0];

                DAL.OrderToWorkDAL dal = new DAL.OrderToWorkDAL();
                Model.OrderToWorkModel model = new Model.OrderToWorkModel();
                model.CreateTime = DateTime.Now;
                BLL.UserBLL ubll = new BLL.UserBLL();

                string UserId = ubll.CurrentUserId();
                model.CreateUserId = UserId;
                model.LimitTime = DateTime.Parse(dr["PlanningTime"].ToString());
                model.Wages = decimal.Parse(dr["OrderWages"].ToString());
                model.OrderToWorkTitle = "工单[" + model.CreateTime.ToString("MM-dd HH:mm") + "]";
                model.OrderToWorkStatusId = 10;
                model.ReceivedImgId = "";
                model.MemberId = MemberId;
                model.OrderId = ReStr("OrderId", "");
                model.ReceivedTime = DateTime.Parse(dr["ReceivedTime"].ToString());
                model.DoneTime = DateTime.Parse("3000-01-01");
                if (model.OrderId == "")
                {
                    throw new Exception("OrderId不能为空!");
                }

                if (model.MemberId == 0)
                {
                    throw new Exception("MemberId不能为空!");
                }

                dal.Add(model);

                s.Clear();
                s.Append(" UPDATE  dbo.OrderVsMember SET VsStatus=15 WHERE OrderId='" + model.OrderId + "' AND MemberId=" + model.MemberId + "  "); 

                DAL.DalComm.BackData(s.ToString());



                ReDict2.Add("OrderToWorkId", model.OrderToWorkId.ToString());


                #region 建立日志

                Model.OrderLogModel LogModel = new Model.OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 20010;
                LogModel.OrderLogTitle = "分派工单给用户[" + drMember["Phone"] + "(姓名:" + drMember["RealName"] + ")]";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                LogModel.MemberId = MemberId;
                LogModel.UserId = UserId;
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
            ReTrue();



        }

        private void GetOrderToWorkList()
        {

            string OrderId = ReStr("OrderId", "");

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.OrderToWorkView WHERE OrderId='" + OrderId + "' ");

            s.Append(" SELECT otwd.* FROM dbo.OrderToWorkDetail otwd ");   //明细
            s.Append(" INNER JOIN dbo.OrderToWork otw ON otw.OrderToWorkId = otwd.OrderToWorkId ");
            s.Append(" WHERE otw.OrderId='" + OrderId + "' ");
            s.Append("  ");

            s.Append(" SELECT  otwd.Color,otwdvc.Num,otwdvc.CheckNum,otwdvc.DoneNum, otwdvc.ClothesSizeId, otwdvc.OrderToWorkDetailId ,otw.MemberId,otw.OrderToWorkId FROM dbo.OrderToWorkDetailVsClothesSize otwdvc  "); //最全的明细(颜色)对应尺码
            s.Append(" INNER JOIN dbo.OrderToWorkDetail otwd ON otwd.OrderToWorkDetailId = otwdvc.OrderToWorkDetailId ");
            s.Append(" INNER JOIN dbo.OrderToWork otw ON otw.OrderToWorkId = otwd.OrderToWorkId ");
            s.Append(" WHERE otw.OrderId='" + OrderId + "' ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtOrderToWork = ds.Tables[0];
            DataTable dtOrderToWorkDetail = ds.Tables[1];
            DataTable dtOrderToWorkDetailVsClothesSize = ds.Tables[2];


            ReDict.Add("OrderToWork", JsonHelper.ToJson(dtOrderToWork));
            ReDict.Add("OrderToWorkDetail", JsonHelper.ToJson(dtOrderToWorkDetail));
            ReDict.Add("OrderToWorkDetailVsClothesSize", JsonHelper.ToJson(dtOrderToWorkDetailVsClothesSize));
            ReTrue();

        }

        private void SaveOrderVsMember()
        {
            Model.OrderVsMemberModel model = new Model.OrderVsMemberModel();
            model.CreateTime = DateTime.Now;
            model.Invalid = false;
            model.MemberId = ReDecimal("MemberId", 0);
            model.OrderId = ReStr("OrderId", "");
            model.VsStatus = 10;

            model.VsType = ReInt("VsType", 0);

            if (model.VsType == 0)
            {
                throw new Exception("VsType不能为空!");
            }

            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.SaveOrderVsMember(model);

            ReTrue();
        }

        private void ChangeOrderStatus()
        {
            BLL.OrderBLL bll = new BLL.OrderBLL();

            string OrderId = ReStr("OrderId", "");
            int OrderStatusId = ReInt("OrderStatusId", 0);
            if (OrderId == "")
            {
                throw new Exception("OrderId不能为空!");
            }


            if (OrderStatusId == 0)
            {
                throw new Exception("OrderStatusId不能为0!");
            }

            switch (OrderStatusId)
            {
                case 20:  //转到抢单

                    bll.OrderToQiangDan(OrderId);

                    break;

                case 10:  //一般来说, 从抢单撤回到发布状态, 可以主动指派给用户做
                    bll.OrderToFaBu(OrderId);
                    break;


                case 30:  //订单状态开始生产, 释放所有用户, 不能再抢单

                    bll.OrderToProduction(OrderId);
                    break;

                case -10://作废

                    bll.OrderToZuoFei(OrderId);
                    break;

                case 100: //完结
                    bll.OrderToDone(OrderId);
                    break;
            }

            ReTrue();

        }

        private void GetOrderDetail()
        {
            StringBuilder s = new StringBuilder();
            decimal OrderDetailId = ReDecimal("OrderDetailId", 0);

            s.Append(" SELECT   Color ,OrderDetailId FROM  dbo.OrderDetail   WHERE OrderDetailId='" + OrderDetailId + "' ");
            s.Append(" SELECT   ovc.*, c.ClothesSizeName ,");
            s.Append(" c.ClothesSizeId,");
            s.Append(" od.Color,");
            s.Append(" od.OrderDetailId");
            s.Append(" FROM     dbo.OrderDetailVsClothesSize ovc");
            s.Append(" INNER JOIN dbo.ClothesSize c ON c.ClothesSizeId = ovc.ClothesSizeId");
            s.Append(" INNER JOIN dbo.OrderDetail od ON od.OrderDetailId = ovc.OrderDetailId");
            s.Append(" WHERE od.OrderDetailId='" + OrderDetailId + "'");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            DataTable dtOrderDetailVsClothesSize = ds.Tables[1];

            ReDict.Add("info", JsonHelper.ToJsonNo1(dt));
            ReDict.Add("ovcList", JsonHelper.ToJson(dtOrderDetailVsClothesSize));
            ReTrue();

        }

        private void SaveOrderDetail()
        {
            Model.OrderDetailModel model = new Model.OrderDetailModel();
            model.OrderDetailId = ReDecimal("OrderDetailId", 0);
            DAL.OrderDetailDAL dal = new DAL.OrderDetailDAL();
            if (model.OrderDetailId != 0)
            {
                model = dal.GetModel(model.OrderDetailId);
            }

            model.OrderId = ReStr("OrderId", "");

            if (model.OrderId == "")
            {
                throw new Exception("OrderId不能为空!");
            }
            model.Color = ReStr("Color").Trim();
            if (model.Color == "")
            {
                throw new Exception("Color不能为空!");
            }
            else
            {


            }
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                if (model.OrderDetailId == 0)
                {
                    int i = DAL.DalComm.ExInt(" SELECT count(0) FROM  dbo.OrderDetail WHERE Color ='" + model.Color + "' and OrderId='" + model.OrderId + "' ");
                    if (i > 0)
                    {
                        throw new Exception("您已经添加了名称为[" + model.Color + "]的颜色!不可以重复添加");
                    }
                    dal.Add(model);
                }
                else
                {

                    dal.Update(model);
                }


                DataTable dtClothesSize = ReTable("ClothesSizeArray");
                DAL.OrderDetailVsClothesSizeDAL VsDal = new DAL.OrderDetailVsClothesSizeDAL();
                VsDal.DeleteList(" OrderDetailId='" + model.OrderDetailId + "' ");
                if (dtClothesSize != null)
                {

                    if (dtClothesSize.Rows.Count != 0)
                    {

                        StringBuilder ClothesSizeNameStr = new StringBuilder();
                        foreach (DataRow drClothesSize in dtClothesSize.Rows)
                        {




                            Model.OrderDetailVsClothesSizeModel VsModel = new Model.OrderDetailVsClothesSizeModel();
                            VsModel.OrderDetailId = model.OrderDetailId;
                            VsModel.Num = decimal.Parse(drClothesSize["Num"].ToString());
                            VsModel.Memo = drClothesSize["Memo"].ToString();
                            VsModel.ClothesSizeId = int.Parse(drClothesSize["ClothesSizeId"].ToString());

                            if (VsModel.Num > 0)
                            {

                                VsDal.Add(VsModel);


                                ClothesSizeNameStr.Append("" + drClothesSize["ClothesSizeName"] + drClothesSize["Num"].ToString() + "；");
                            }

                        }


                        #region 建立日志

                        Model.OrderLogModel LogModel = new Model.OrderLogModel();
                        LogModel.CreateTime = DateTime.Now;
                        LogModel.OrderId = model.OrderId;
                        LogModel.OrderLogClassId = 10010;
                        LogModel.OrderLogTitle = "新增颜色:" + model.Color + "," + ClothesSizeNameStr.ToString() + "";
                        LogModel.OrderToWorkId = 0;
                        LogModel.ReKey = "";
                        LogModel.ReKey2 = "";

                        BLL.UserBLL ubll = new BLL.UserBLL();
                        LogModel.UserId = ubll.CurrentUserId();
                        DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                        LogDal.Add(LogModel);
                        #endregion


                    }
                    else
                    {

                        throw new Exception("ClothesSizeArray不能为0");
                    }
                }
                else
                {
                    throw new Exception("ClothesSizeArray必须传递");
                }







                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.CountOrder(model.OrderId);
            ReTrue();
        }

        private void GetClothesSize()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.ClothesSize ");
            DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0]; ;

            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void GetOrderInfo()
        {
            StringBuilder s = new StringBuilder();

            string OrderId = ReStr("OrderId", "");
            if (OrderId == "")
            {
                throw new Exception("OrderId不能为空!");
            }

            s.Append(" SELECT * FROM dbo.OrderView WITH(NOLOCK) WHERE OrderId='" + OrderId + "' ");


            s.Append(" SELECT   Color ,OrderDetailId FROM  dbo.OrderDetail   WHERE OrderId='" + OrderId + "' ");



            s.Append(" SELECT   ovc.*, c.ClothesSizeName ,");
            s.Append(" c.ClothesSizeId,");
            s.Append(" od.Color,");
            s.Append(" od.OrderDetailId");
            s.Append(" FROM     dbo.OrderDetailVsClothesSize ovc");
            s.Append(" INNER JOIN dbo.ClothesSize c ON c.ClothesSizeId = ovc.ClothesSizeId");
            s.Append(" INNER JOIN dbo.OrderDetail od ON od.OrderDetailId = ovc.OrderDetailId");
            s.Append(" WHERE od.OrderId='" + OrderId + "'");

            s.Append("  SELECT c.ClothesSizeId, c.ClothesSizeName FROM dbo.OrderDetailVsClothesSize ovc ");
            s.Append(" INNER JOIN dbo.OrderDetail od ON od.OrderDetailId = ovc.OrderDetailId ");
            s.Append("  INNER JOIN dbo.ClothesSize c ON c.ClothesSizeId = ovc.ClothesSizeId  ");
            s.Append(" WHERE od.OrderId='" + OrderId + "' ");        //Group的尺码
            s.Append(" GROUP BY c.ClothesSizeId,c.ClothesSizeName ");
            s.Append("  ");

            s.Append(" SELECT ovi.*,ImgUrl FROM dbo.OrderVsImg AS ovi WITH(NOLOCK) INNER JOIN dbo.ImageInfo i ON i.ImgId = ovi.ImgId WHERE OrderId='" + OrderId + "' ");

            s.Append(" SELECT ovm.OrderId,ovm.MemberId,ovm.VsStatus,ovm.VsStatusName ,m.Phone,m.PicImgId,m.PicImgUrl,m.NickName,m.RealName,m.Sex ,p.ProcessLvTitle,p.ProcessLvName,ovm.VsPlaces  FROM  dbo.OrderVsMemberView ovm ");
            s.Append(" INNER JOIN dbo.MemberView m ON m.MemberId = ovm.MemberId  ");
            s.Append(" LEFT JOIN dbo.ProcessLv p ON p.ProcessLvId = m.ProcessLvId ");
            s.Append(" WHERE OrderId='" + OrderId + "'  ");
            s.Append(" Order By  ovm.VsPlaces, ovm.CreateTime desc ");



            s.Append("  DECLARE @DoneQuantity AS INT =(    ");
            s.Append("  SELECT  ISNULL( SUM(v.DoneNum),0) FROM dbo.OrderToWorkDetailVsClothesSize v  ");
            s.Append("  INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkDetailId = v.OrderToWorkDetailId  ");
            s.Append("  INNER JOIN dbo.OrderToWork k ON k.OrderToWorkId = d.OrderToWorkId  ");
            s.Append("   WHERE OrderId='" + OrderId + "' AND Invalid=0  ) ");

            s.Append("  DECLARE @WorkQuantity AS INT =(    ");
            s.Append("  SELECT  ISNULL( SUM(v.Num),0) FROM dbo.OrderToWorkDetailVsClothesSize v  ");
            s.Append("  INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkDetailId = v.OrderToWorkDetailId  ");
            s.Append("  INNER JOIN dbo.OrderToWork k ON k.OrderToWorkId = d.OrderToWorkId  ");
            s.Append("   WHERE OrderId='" + OrderId + "' AND Invalid=0  ) ");


            s.Append("   DECLARE @CheckQuantity AS INT =(    ");
            s.Append("   SELECT ISNULL(  SUM(v.CheckNum),0) FROM dbo.OrderToWorkDetailVsClothesSize v ");
            s.Append("   INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkDetailId = v.OrderToWorkDetailId ");
            s.Append("  INNER JOIN dbo.OrderToWork k ON k.OrderToWorkId = d.OrderToWorkId  ");
            s.Append("  WHERE OrderId='" + OrderId + "' AND Invalid=0 )  ");
            s.Append("  DECLARE @OrderToWorkNum AS INT=(  ");
            s.Append("  SELECT COUNT(0) FROM  dbo.OrderToWork WHERE OrderId='" + OrderId + "' AND Invalid=0  ");
            s.Append("  )  ");
            s.Append("  DECLARE @OrderVsMemberNum AS INT=(  ");
            s.Append("  SELECT COUNT(0) FROM dbo.OrderVsMember WHERE OrderId='" + OrderId + "'  ");
            s.Append("  )  ");
            s.Append("  DECLARE @OrderQuantity AS INT =(   ");
            s.Append("  SELECT ISNULL(SUM(v.Num),0) FROM dbo.OrderDetailVsClothesSize v   ");
            s.Append("  INNER JOIN dbo.OrderDetail d ON d.OrderDetailId = v.OrderDetailId  ");
            s.Append("  WHERE OrderId='" + OrderId + "')  ");
            s.Append("    ");
            s.Append("    ");
            s.Append("    ");
            s.Append("    ");
            s.Append("    ");
            s.Append("  SELECT @DoneQuantity AS  DoneQuantity,@CheckQuantity AS CheckQuantity,@OrderToWorkNum AS OrderToWorkNum,@OrderVsMemberNum AS OrderVsMemberNum  ");
            s.Append("  ,@OrderQuantity AS OrderQuantity  ");
            s.Append("  ,@WorkQuantity AS WorkQuantity  ");

            s.Append(" SELECT * FROM dbo.OrderExpect where OrderId='" + OrderId + "' ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtInfo = ds.Tables[0];
            DataTable dtDetail = ds.Tables[1];
            DataTable dtDetailVsClothesSize = ds.Tables[2];
            DataTable dtGroupClothesSize = ds.Tables[3];
            DataTable dtImg = ds.Tables[4];
            DataTable dtOrderVsMember = ds.Tables[5];
            DataTable dtCount = ds.Tables[6];
            DataTable dtOrderExpect = ds.Tables[7];

            ReDict.Add("Info", JsonHelper.ToJsonNo1(dtInfo));
            ReDict.Add("Detail", JsonHelper.ToJson(dtDetail));
            ReDict.Add("DetailVsClothesSize", JsonHelper.ToJson(dtDetailVsClothesSize));
            ReDict.Add("GroupClothesSize", JsonHelper.ToJson(dtGroupClothesSize));
            ReDict.Add("imgArray", JsonHelper.ToJson(dtImg));
            ReDict.Add("OrderVsMember", JsonHelper.ToJson(dtOrderVsMember));
            ReDict.Add("Count", JsonHelper.ToJsonNo1(dtCount));
            ReDict.Add("OrderExpect", JsonHelper.ToJson(dtOrderExpect));
            ReTrue();



        }

        private void SaveOrderInfo()
        {
            Model.OrderInfoModel model = new Model.OrderInfoModel();
            DAL.OrderInfoDAL dal = new DAL.OrderInfoDAL();
            model.OrderId = ReStr("OrderId", "");
            if (model.OrderId != "")
            {

                model = dal.GetModel(model.OrderId);
            }

            model.OrderTitle = ReStr("OrderTitle", "");
            model.OrderContent = HttpContext.Current.Server.UrlDecode(ReStr("OrderContent"));
            model.ProcessLvId = ReInt("ProcessLvId", 0);
            model.OrderCode = ReStr("OrderCode", "");
            model.ClientsCode = ReStr("ClientsCode", "");
            //  model.OrderQuantity = ReDecimal("OrderQuantity", 0);
            model.OrderWages = ReDecimal("OrderWages", 0);
            model.OrderContacts = ReStr("OrderContacts", "");
            model.OrderTel = ReStr("OrderTel", "");
            model.OrderAddress = ReStr("OrderAddress", "");
            model.Unit = ReStr("Unit", "");
            model.ProcessLocationTypeId = ReInt("ProcessLocationTypeId", 0);
            model.OrderClassId = ReInt("OrderClassId", 0);
            model.OrderImgId = ReStr("OrderImgId", "");
            model.Invalid = ReBool("Invalid", false);

            model.ReleaseTypeId = ReInt("ReleaseTypeId", 10);  //默认拆单发布

            model.LimitTime = ReTime("LimitTime", DateTime.Parse("3000-01-01"));
            model.PlanningTime = ReTime("PlanningTime", DateTime.Parse("3000-01-01"));
            model.MinQuantity = ReDecimal("MinQuantity", 0);
            model.Places = ReInt("Places", 0);
            model.ReceivedTime = ReTime("ReceivedTime", DateTime.Now);
            model.PlanningDay = ReInt("PlanningDay", 0);
            BLL.OrderBLL bll = new BLL.OrderBLL();
            bll.SaveOrderInfo(model);

            DataTable dtImg = ReTable("OrderVsImg");

            DAL.OrderVsImgDAL oviDal = new DAL.OrderVsImgDAL();
            oviDal.DeleteList(" OrderId='" + model.OrderId + "' ");

            if (dtImg != null)
            {
                if (dtImg.Rows.Count > 0)
                {



                    foreach (DataRow drImg in dtImg.Rows)
                    {

                        Model.OrderVsImgModel oviModel = new Model.OrderVsImgModel();
                        oviModel.ImgId = drImg["ImgId"].ToString();
                        oviModel.OrderId = model.OrderId;
                        oviDal.Add(oviModel);
                    }

                }
            }

            ReDict2.Add("OrderStatusId", model.OrderStatusId.ToString());
            ReDict2.Add("OrderId", model.OrderId);
            ReTrue();
        }

        private void GetOrderList()
        {


            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc");
            string GetType = ReStr("GetType", "");
            string OrderId = ReStr("OrderId", " ReceivedTime  desc ");
            decimal MemberId = ReDecimal("MemberId", 0);
            bool CountByStatus = ReBool("CountByStatus", false);
            string OrderStatusIdArray = ReStr("OrderStatusIdArray", "");

            DAL.OrderInfoDAL dal = new DAL.OrderInfoDAL();

            StringBuilder s = new StringBuilder();

            s.Append(" 1=1 ");

            if (OrderStatusIdArray != "" && OrderStatusIdArray != "All")
            {

                s.Append(" and OrderStatusId in ("+ OrderStatusIdArray + ") ");
            }

            if (GetType == "QiangDan")
            {
                //if (MemberId == 0)
                //{
                //    throw new Exception("用户编号不能为空");
                //}


                s.Append(" and OrderStatusId>=20 ");
               // s.Append(" and ReceivedTime >GETDATE() ");   //领取裁片时间必须大于当前时间
                //   s.Append(" and PlanningTime >GETDATE() AND ReceivedTime >GETDATE() ");
            }
            else
            {


                if (OrderId.Trim() != "")
                {
                    s.Append(" and OrderId='" + OrderId + "' ");

                }
                else
                {
                    DateTime CreateTime1 = ReTime("CreateTime1");
                    DateTime CreateTime2 = ReTime("CreateTime2");
                    s.Append(" and CreateTime BETWEEN '" + CreateTime1 + "' AND '" + CreateTime2 + "' ");
                }



            }
            DataSet ds2 = new DataSet();
            if (CountByStatus)
            {

                StringBuilder sg = new StringBuilder();
                sg.Append("  SELECT  COUNT(0) as OrderStatusNum  ,OrderStatusId FROM dbo.OrderView where " + s.ToString() +"  GROUP BY OrderStatusId ");

                 ds2 = DAL.DalComm.BackData(sg.ToString());


            }


            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);

            if (GetType == "QiangDan")
            {

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("Memo");
                dt.Columns.Add("VsStatus");
                dt.Columns.Add("VsStatusName");



                if (dt.Rows.Count > 0 && MemberId != 0)
                {
                    List<string> OrderIds = new List<string>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        OrderIds.Add("'" + dr["OrderId"].ToString() + "'");
                    }

                    s.Clear();
                    s.Append("   SELECT OrderId,MemberId,VsStatus,VsStatusName,Memo FROM dbo.OrderVsMemberView WHERE OrderId IN (" + string.Join(",", OrderIds) + ") and MemberId='" + MemberId + "' ");
                    DataSet dsvs = DAL.DalComm.BackData(s.ToString());
                    DataTable dtvs = dsvs.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {

                        string OrderId2 = dr["OrderId"].ToString();

                        foreach (DataRow drvs in dtvs.Rows)
                        {
                            string vsOrderId = drvs["OrderId"].ToString();

                            if (OrderId2 == vsOrderId)
                            {

                                dr["Memo"] = drvs["Memo"].ToString();
                                dr["VsStatus"] = drvs["VsStatus"];
                                dr["VsStatusName"] = drvs["VsStatusName"];
                            }
                        }

                    }

                }


            }

            if (CountByStatus)
            {

                RePage2(ds, ds2);
            }
            else
            {

                RePage2(ds);

            }



        }
    }
}
