using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Transactions;

namespace BLL
{
    public class OrderBLL
    {

        public DataSet CountOrderToWork(decimal OrderToWorkId)
        {

            StringBuilder s = new StringBuilder();

            s.Append(" DECLARE @CheckQuantity  AS INT =(  ");
            s.Append("  SELECT  ISNULL( SUM(v.CheckNum),0) AS CheckQuantity  FROM dbo.OrderToWork  o ");
            s.Append("  INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkId = o.OrderToWorkId ");
            s.Append("  INNER JOIN dbo.OrderToWorkDetailVsClothesSize v ON v.OrderToWorkDetailId = d.OrderToWorkDetailId ");
            s.Append("  WHERE d.OrderToWorkId=" + OrderToWorkId + " ");
            s.Append("  ) ");
            s.Append("   ");
            s.Append("  DECLARE @Quantity  AS INT =( ");
            s.Append(" SELECT  ISNULL( SUM(v.Num),0) AS CheckQuantity  FROM dbo.OrderToWork  o  ");
            s.Append("  INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkId = o.OrderToWorkId ");
            s.Append(" INNER JOIN dbo.OrderToWorkDetailVsClothesSize v ON v.OrderToWorkDetailId = d.OrderToWorkDetailId  ");
            s.Append("  WHERE d.OrderToWorkId=" + OrderToWorkId + " ");
            s.Append("  ) ");
            s.Append("   ");

            s.Append("  DECLARE @DoneQuantity  AS INT =( ");
            s.Append(" SELECT  ISNULL( SUM(v.DoneNum),0) AS CheckQuantity  FROM dbo.OrderToWork  o  ");
            s.Append("  INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkId = o.OrderToWorkId ");
            s.Append(" INNER JOIN dbo.OrderToWorkDetailVsClothesSize v ON v.OrderToWorkDetailId = d.OrderToWorkDetailId  ");
            s.Append("  WHERE d.OrderToWorkId=" + OrderToWorkId + " ");
            s.Append("  ) ");


            s.Append(" SELECT  @CheckQuantity AS CheckQuantity,@Quantity AS Quantity,@DoneQuantity as DoneQuantity  ");
            s.Append("  UPDATE dbo.OrderToWork SET CheckQuantity=@CheckQuantity ,Quantity=@Quantity,DoneQuantity=@DoneQuantity WHERE OrderToWorkId=" + OrderToWorkId + "  ");
            s.Append(" SELECT * FROM dbo.OrderToWork WHERE OrderToWorkId=" + OrderToWorkId + " ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            return ds;
        }

        public void PendingOrderToWork(decimal OrderToWorkId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                CountOrderToWork(OrderToWorkId);

                StringBuilder s = new StringBuilder();

                s.Append(" SELECT * FROM dbo.OrderToWork WHERE OrderToWorkId=" + OrderToWorkId + " ");


                s.Append(" UPDATE dbo.OrderToWork SET OrderToWorkStatusId=70 ,PendingTime='" + DateTime.Now + "' WHERE OrderToWorkId=" + OrderToWorkId + " ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有找到编号为" + OrderToWorkId + "的工单!");
                }
                DataRow dr = dt.Rows[0];
                decimal ChangeAmount = decimal.Parse(dr["Wages"].ToString()) * decimal.Parse(dr["CheckQuantity"].ToString());

                int OrderToWorkStatusId = int.Parse(dr["OrderToWorkStatusId"].ToString());

                if (OrderToWorkStatusId >= 70)
                {
                    //70就已经是结算了

                    throw new Exception("该工单已经完成结算,不能重复结算");
                }


                UserBLL ubll = new UserBLL();
                #region 冲入用户余额
                Model.MemberAmountDetailModel model = new MemberAmountDetailModel();
                model.ReKey = OrderToWorkId.ToString();
                model.UserId = ubll.CurrentUserId();
                model.ChangeAmount = ChangeAmount;
                model.MemberAmountChangeTypeId = 10; //工单结算
                model.MemberId = decimal.Parse(dr["MemberId"].ToString());
                BLL.MemberBLL mbll = new MemberBLL();
                mbll.MemberAmountChange(model);

                #endregion


                MsgBLL msgBll = new MsgBLL();

                msgBll.SendMsgToDevice(10, "您的工单已经结算完成,本次佣金[" + model.ChangeAmount + "元]已冲入您的余额中,随时可以申请提现,请注意查收", "PendingOrderToWork", model.MemberId.ToString(), "");


                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = dr["OrderId"].ToString();
                LogModel.OrderLogClassId = 20070;
                LogModel.OrderLogTitle = "工单完成,余额[" + model.ChangeAmount + "]已经充入到用户账户";
                LogModel.OrderToWorkId = OrderToWorkId;
                LogModel.MemberId = model.MemberId;
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
        }

        public void RemoveOrderDetail(decimal OrderDetailId)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                if (OrderDetailId == 0)
                {
                    throw new Exception("OrderDetailId不能为0");
                }

                StringBuilder s = new StringBuilder();

                s.Append(" SELECT * FROM dbo.OrderDetail WHERE OrderDetailId=" + OrderDetailId + "  ");

                s.Append(" DELETE FROM dbo.OrderDetail WHERE OrderDetailId=" + OrderDetailId + " ");

                s.Append(" DELETE FROM	 dbo.OrderDetailVsClothesSize WHERE OrderDetailId=" + OrderDetailId + "  ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有找到订单明细!");
                }
                DataRow dr = dt.Rows[0];
                string Color = dr["Color"].ToString();


                #region 记录日志
                BLL.UserBLL ubll = new UserBLL();
                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = dr["OrderId"].ToString();
                LogModel.OrderLogClassId = 10010;
                LogModel.OrderLogTitle = "操作员删除了颜色[" + Color + "]的明细,以及下面的所有尺码数量";
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


        }



        /// <summary>
        /// 给订单客户交付成衣, 客户已经付款, 所有工单进入准备结算(结算中)状态
        /// </summary>
        /// <param name="OrderToWorkId"></param>
        public void PayOrder(decimal OrderToWorkId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();
                s.Append("  select OrderToWorkStatusId,OrderToWorkStatusName,OrderId from dbo.OrderToWorkView WHERE OrderToWorkId=" + OrderToWorkId + " ");


                s.Append("        UPDATE dbo.OrderToWork SET OrderToWorkStatusId=60 WHERE OrderToWorkId=" + OrderToWorkId + " ");

                DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有OrderToWorkId为" + OrderToWorkId + "的工单数据!");
                }

                DataRow drotw = dt.Rows[0];
                int OrderToWorkStatusId = int.Parse(drotw["OrderToWorkStatusId"].ToString());
                string OrderToWorkStatusName = drotw["OrderToWorkStatusName"].ToString();
                if (OrderToWorkStatusId >= 60)
                {
                    throw new Exception("工单状态已经为:[" + OrderToWorkStatusName + "]");
                }

                DAL.DalComm.ExInt(s.ToString());


                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = drotw["OrderId"].ToString();
                LogModel.OrderLogClassId = 20060;
                LogModel.OrderLogTitle = "工单状态转为结算中";
                LogModel.OrderToWorkId = OrderToWorkId;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
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
        }

        public void SaveCheckNumArray(decimal OrderToWorkId, DataTable dt)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                #region 验证
                if (OrderToWorkId == 0)
                {
                    throw new Exception("OrderToWokrId不能为0!");
                }

                StringBuilder s = new StringBuilder();
                if (dt == null)
                {
                    throw new Exception("CheckNumArray不能为null");
                }
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("CheckNumArray不能为0");
                }

                #endregion

                StringBuilder w = new StringBuilder();
                w.Append("更新质检数量:");
                string OrderId = "";

                decimal MemberId = 0;
                DAL.OrderToWorkDetailVsClothesSizeDAL dal = new DAL.OrderToWorkDetailVsClothesSizeDAL();
                foreach (DataRow dr in dt.Rows)
                {


                    decimal CheckNum = decimal.Parse(dr["CheckNum"].ToString());

                    Model.OrderToWorkDetailVsClothesSizeModel model = new OrderToWorkDetailVsClothesSizeModel();
                    model = dal.GetModel(decimal.Parse(dr["OrderToWorkDetailId"].ToString()), int.Parse(dr["ClothesSizeId"].ToString()));
                    //是否存在多次质检


                    if (model.OrderToWorkDetailId == 0)
                    {
                        throw new Exception("OrderToWorkDetailId不能为0");
                    }
                    if (model.ClothesSizeId == 0)
                    {

                        throw new Exception("ClothesSizeId不能为0");
                    }


                    if (CheckNum + model.CheckNum > model.DoneNum)
                    {
                        throw new Exception("原本质检通过数量[" + model.CheckNum + "]+本次通过数量[" + CheckNum + "]大于完成数量[" + model.DoneNum + "] ");
                    }
                    else
                    {


                        model.CheckNum = model.CheckNum + CheckNum;

                        model.ChangeTime = DateTime.Now;

                        dal.Update(model);

                    }


                    s.Clear();

                    s.Append(" SELECT OrderId,OrderToWorkId,ClothesSizeName,Color,MemberId FROM dbo.OrderToWorkDetailVsClothesSizeView WHERE OrderToWorkDetailId=" + model.OrderToWorkDetailId + " AND ClothesSizeId=" + model.ClothesSizeId + " ");
                    DataSet ds = DAL.DalComm.BackData(s.ToString());

                    DataTable dtV = ds.Tables[0];
                    DataRow drV = dtV.Rows[0];
                    OrderId = drV["OrderId"].ToString();
                    MemberId = decimal.Parse(drV["MemberId"].ToString());
                    w.Append("[" + drV["Color"] + "" + drV["ClothesSizeName"] + "]:" + model.CheckNum + "；");


                }



                #region 建立日志

                if (OrderId == "")
                {
                    throw new Exception("订单编号不能为空!");
                }

                if (MemberId == 0)
                {
                    throw new Exception("用户编号不能为空!");
                }

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 20042;
                LogModel.OrderLogTitle = w.ToString();
                LogModel.OrderToWorkId = OrderToWorkId;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                try
                {
                    LogModel.UserId = ubll.CurrentUserId();
                }
                catch (Exception)
                {


                }
                LogModel.MemberId = MemberId;
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion
                CountOrderToWork(OrderToWorkId);


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion


        }

        public void CheckOrderToWork(decimal OrderToWorkId)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();
                s.Append("  select OrderToWorkStatusId,OrderToWorkStatusName,OrderId,MemberId from dbo.OrderToWorkView WHERE OrderToWorkId=" + OrderToWorkId + " ");


                s.Append("        UPDATE dbo.OrderToWork SET OrderToWorkStatusId=60,CheckTime='" + DateTime.Now + "' WHERE OrderToWorkId=" + OrderToWorkId + " ");



                DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有OrderToWorkId为" + OrderToWorkId + "的工单数据!");
                }

                DataRow drotw = dt.Rows[0];
                int OrderToWorkStatusId = int.Parse(drotw["OrderToWorkStatusId"].ToString());
                string OrderToWorkStatusName = drotw["OrderToWorkStatusName"].ToString();
                if (OrderToWorkStatusId >= 60)
                {
                    throw new Exception("工单状态已经为:[" + OrderToWorkStatusName + "]");
                }

                DAL.DalComm.ExInt(s.ToString());


                decimal MemberId = decimal.Parse(drotw["MemberId"].ToString());



                DAL.DalComm.ExReInt(" UPDATE dbo.Member SET CheckOrderNum=CheckOrderNum+1 WHERE MemberId=" + MemberId + "  ");

                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = drotw["OrderId"].ToString();
                LogModel.OrderLogClassId = 20050;
                LogModel.OrderLogTitle = "工单质检通过,状态改为[结算中]";
                LogModel.OrderToWorkId = OrderToWorkId;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                LogModel.MemberId = MemberId;
                BLL.UserBLL ubll = new BLL.UserBLL();
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


                MsgBLL msgBll = new MsgBLL();

                msgBll.SendMsgToDevice(10, "您的工单已被质检通过,绝大多数情况下本次工单的佣金会在20个工作日冲入您的余额", "CheckOrderToWork", MemberId.ToString(), "");

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion



        }

        public void SaveDoneNumArray(DataTable dt)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion




                StringBuilder s = new StringBuilder();


                if (dt == null)
                {
                    throw new Exception("获取内容为null");
                }
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("获取内容为0行");
                }
                decimal OrderToWorkId = 0;
                string Orderid = "";
                StringBuilder w = new StringBuilder();
                w.Append("更新完成进度:");
                DAL.OrderToWorkDetailVsClothesSizeDAL dal = new DAL.OrderToWorkDetailVsClothesSizeDAL();
                foreach (DataRow dr in dt.Rows)
                {
                    OrderToWorkId = decimal.Parse(dr["OrderToWorkId"].ToString());
                    if (OrderToWorkId == 0)
                    {
                        throw new Exception("OrderToWorkId不能为0!");
                    }

                    Model.OrderToWorkDetailVsClothesSizeModel model = new OrderToWorkDetailVsClothesSizeModel();
                    decimal OrderToWorkDetailId = decimal.Parse(dr["OrderToWorkDetailId"].ToString());
                    int ClothesSizeId = int.Parse(dr["ClothesSizeId"].ToString());
                    if (OrderToWorkDetailId == 0)
                    {

                        throw new Exception("OrderToWorkDetailId不能为0!");
                    }
                    if (ClothesSizeId == 0)
                    {
                        throw new Exception("ClothesSizeId不能为0!");
                    }
                    model = dal.GetModel(OrderToWorkDetailId, ClothesSizeId);
                    decimal DoneNum = decimal.Parse(dr["DoneNum"].ToString());

                    if (DoneNum + model.DoneNum > model.Num)
                    {
                        throw new Exception("完成数量[" + DoneNum + model.DoneNum + "]不能大于分派数量[" + model.Num + "]");
                    }
                    else
                    {
                        model.DoneNum = DoneNum + model.DoneNum;

                    }


                    dal.Update(model);

                    s.Clear();

                    s.Append(" SELECT OrderId,OrderToWorkId,ClothesSizeName,Color FROM dbo.OrderToWorkDetailVsClothesSizeView WHERE OrderToWorkDetailId=" + OrderToWorkDetailId + " AND ClothesSizeId=" + ClothesSizeId + " ");
                    DataSet ds = DAL.DalComm.BackData(s.ToString());

                    DataTable dtV = ds.Tables[0];
                    DataRow drV = dtV.Rows[0];
                    Orderid = drV["OrderId"].ToString();
                    w.Append("[" + drV["Color"] + "" + drV["ClothesSizeName"] + "]:" + model.DoneNum + "；");

                }
                s.Clear();
                s.Append(" UPDATE  dbo.OrderToWork SET ChangeTime =GETDATE() WHERE OrderToWorkId=" + OrderToWorkId + " ");
                DAL.DalComm.ExReInt(s.ToString());



                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = Orderid;
                LogModel.OrderLogClassId = 20032;
                LogModel.OrderLogTitle = w.ToString();
                LogModel.OrderToWorkId = OrderToWorkId;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                try
                {
                    LogModel.UserId = ubll.CurrentUserId();
                }
                catch (Exception)
                {


                }
                LogModel.MemberId = BLL.MemberBLL.CurrentMember().CurrentMemberId;
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

        }
        //取消登记 取消订单登记
        public void RemoveOrderVsMember(OrderVsMemberModel model, string RemoveType)
        {

            if (model.OrderId == "")
            {
                throw new Exception("订单编号不能为空!");
            }

            if (model.MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }

            if (RemoveType == "自行取消")
            {
                model.VsStatus = -1;
            }
            else if (RemoveType == "淘汰")
            {

                model.VsStatus = -10;
            }
            else
            {

                throw new Exception("取消类别RemoveType不能为空[" + RemoveType + "]!");
            }



            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();



                s.Append(" UPDATE dbo.OrderVsMember SET VsStatus=" + model.VsStatus + ",Memo='" + model.Memo + "' where  MemberId=" + model.MemberId + " and OrderId='" + model.OrderId + "'  ");



                DAL.OrderVsMemberDAL dal = new DAL.OrderVsMemberDAL();
                DAL.DalComm.ExReInt(s.ToString());
                int VsStatus = DAL.DalComm.ExInt("  SELECT VsStatus FROM dbo.OrderVsMember WHERE MemberId=" + model.MemberId + " AND OrderId='" + model.OrderId + "' ");

                if (VsStatus >= 20)
                {
                    throw new Exception("已经派单,无法取消申请");
                }
                int i = DAL.DalComm.ExInt(" SELECT count(0) FROM dbo.OrderToWork WHERE MemberId=" + model.MemberId + " AND OrderId='" + model.OrderId + "' and OrderToWorkStatusId>=30 ");   //生产中的订单
                if (i > 0)
                {
                    throw new Exception("已经派单(" + i + "个),无法取消申请");
                }


                ClearMemberOrderToWork(model.OrderId, model.MemberId);   //清理死在里面的订单,下了订单, 也不分派生产






                #region 重置生产档期
                DateTime MaxOrderPlanningTime = SetMaxOrderPlanningTime(model.MemberId);
                #endregion


                #region 建立日志

                Model.OrderLogModel LogModel = new Model.OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = model.OrderId;
                LogModel.OrderLogClassId = 20002;
                LogModel.OrderLogTitle = "用户取消了订单的申请";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                LogModel.MemberId = model.MemberId;
                BLL.UserBLL ubll = new BLL.UserBLL();
                try
                {
                    LogModel.UserId = ubll.CurrentUserId();
                }
                catch (Exception)
                {
                    //一般来说, 是用户自动取消的

                }

                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion



                #region 推送消息
                var msgContent = "";
                var msgContent2 = "";
                if (RemoveType == "淘汰")
                {
                    msgContent = "非常抱歉,您的订单申请已经被淘汰,原因:" + model.Memo + "";
                    msgContent2 = "有用户抢单登记被淘汰了,原因:" + model.Memo + "";

                }
                else if (RemoveType == "自行取消")
                {
                    msgContent = "您已经自行取消了订单申请.";
                    msgContent2 = "一个用户取消了订单的申请";
                }
                else
                {
                    msgContent = "取消了订单申请[未知原因]";
                    msgContent2 = "取消了订单申请[未知原因]";
                }

                BLL.MsgBLL msgBll = new BLL.MsgBLL();
                msgBll.SendMsgToUser("1999001", new MsgTextModel()
                {
                    CreateTime = DateTime.Now,
                    MsgContent = msgContent2,
                    MsgTitle = "",
                    MsgType = "MemberVsOutOrderToWork",
                    EndTime = DateTime.Now.AddDays(1),
                    Extra = "{ \"OrderId\":\"" + LogModel.OrderId + "\",MemberId:" + model.MemberId + " }"

                });





                msgBll.SendMsgToDevice(10, msgContent, "MemberVsOrderToWork", model.MemberId.ToString(), "", "{ OrderId:'" + model.OrderId + "',MemberId:" + model.MemberId + " }");
                #endregion



                CountOrder(model.OrderId);
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }



        public void ClearMemberOrderToWork(decimal OrderToWorkId)
        {




            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion




                DataSet ds = DAL.DalComm.BackData(" SELECT OrderToWorkStatusId,MemberId,OrderId FROM dbo.OrderToWork WHERE OrderToWorkId=" + OrderToWorkId + " ");


                DataTable dt = ds.Tables[0];


                DataRow dr = dt.Rows[0];


                int OrderToWorkStatusId = int.Parse(dr["OrderToWorkStatusId"].ToString());

                decimal MemberId = decimal.Parse(dr["MemberId"].ToString());

                string OrderId = dr["OrderId"].ToString();




                if (OrderToWorkStatusId >= 20)
                {

                    throw new Exception("该工单恐怕已经进入了生产流程，不允许清理。");
                }


                StringBuilder s = new StringBuilder();



                s.Append("  DELETE FROM dbo.OrderToWork  WHERE OrderToWorkId=" + OrderToWorkId + "  ");

                s.Append(" 	DELETE FROM dbo.OrderToWorkDetail WHERE OrderToWorkId= " + OrderToWorkId + " ");

                s.Append(" DELETE v FROM dbo.OrderToWorkDetailVsClothesSize v INNER JOIN dbo.OrderToWorkDetail d ON d.OrderToWorkDetailId = v.OrderToWorkDetailId WHERE d.OrderToWorkId=" + OrderToWorkId + "");

                DAL.DalComm.ExReInt(s.ToString());




                SetMaxOrderPlanningTime(MemberId);  //重置用户档期
                CountOrder(OrderId); //重新统计订单





                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion
















        }

        //清理工单
        public void ClearMemberOrderToWork(string OrderId, decimal MemberId)
        {

            //在事务中调用上面的方法



            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                DataSet ds = DAL.DalComm.BackData(" select OrderToWorkId  FROM dbo.OrderToWork WHERE OrderId=" + OrderId + " AND MemberId=" + MemberId + " AND OrderToWorkStatusId<=15  ");

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {

                }
                else
                {

                    foreach (DataRow dr in dt.Rows)
                    {

                        decimal OrderToWorkId = decimal.Parse(dr["OrderToWorkId"].ToString());
                        ClearMemberOrderToWork(OrderToWorkId);

                    }
                }


                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion









        }


        public void DoneOrderToWork(decimal OrderToWorkId)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();

            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();
                s.Append("  select OrderToWorkStatusId,OrderToWorkStatusName,OrderId,MemberId,RealName,Phone from dbo.OrderToWorkView WHERE OrderToWorkId=" + OrderToWorkId + " ");


                s.Append("        UPDATE dbo.OrderToWork SET OrderToWorkStatusId=40 WHERE OrderToWorkId=" + OrderToWorkId + " ");

                DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有OrderToWorkId为" + OrderToWorkId + "的工单数据!");
                }

                DataRow drotw = dt.Rows[0];

                decimal MemberId = decimal.Parse(drotw["MemberId"].ToString());
                int OrderToWorkStatusId = int.Parse(drotw["OrderToWorkStatusId"].ToString());
                string OrderToWorkStatusName = drotw["OrderToWorkStatusName"].ToString();
                if (OrderToWorkStatusId >= 40)
                {
                    throw new Exception("工单状态已经为:[" + OrderToWorkStatusName + "]");
                }

                DAL.DalComm.ExInt(s.ToString());


                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = drotw["OrderId"].ToString();
                LogModel.OrderLogClassId = 20040;
                LogModel.OrderLogTitle = "将工单改为完成生产(待验收)状态";
                LogModel.OrderToWorkId = OrderToWorkId;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                LogModel.MemberId = MemberId;
                BLL.UserBLL ubll = new BLL.UserBLL();
                try
                {
                    LogModel.UserId = ubll.CurrentUserId();
                    LogModel.OrderLogTitle = "将工单改为完成生产(待验收)状态--操作员:" + LogModel.UserId + "";

                }
                catch (Exception)
                {


                }

                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion


                #region 推送消息



                MsgBLL msgBll = new MsgBLL();
                //msgBll.SendMsgToUser("1999001", new MsgTextModel()
                //{
                //    CreateTime = DateTime.Now,
                //    MsgContent = "[" + drotw["RealName"] + "](" + drotw["Phone"] + ")的工单改为生产完成状态",
                //    MsgTitle = "",
                //    MsgType = "DoneOrderToWork",
                //    EndTime = DateTime.Now.AddDays(1),
                //    Extra = "{ OrderToWorkId:" + OrderToWorkId + " }"



                //});


                msgBll.SendMsgToDevice(10, "您有工单等待质检", "DoneOrderToWork", MemberId.ToString(), "", "{ OrderToWorkId:" + OrderToWorkId + " }");

                #endregion

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion


        }

        public void SaveDoneNum(decimal DoneNum, decimal OrderToWorkDetailId, int ClothesSizeId, int DoneLogTypeId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                if (OrderToWorkDetailId == 0)
                {
                    throw new Exception("OrderToWorkDetailId不能为0!");
                }
                if (ClothesSizeId == 0)
                {
                    throw new Exception("ClothesSizeId不能为0!");
                }
                decimal OldDoneNum = 0;
                DAL.OrderToWorkDetailVsClothesSizeDAL dal = new DAL.OrderToWorkDetailVsClothesSizeDAL();
                Model.OrderToWorkDetailVsClothesSizeModel model = new OrderToWorkDetailVsClothesSizeModel();
                model = dal.GetModel(OrderToWorkDetailId, ClothesSizeId);

                model.ChangeTime = DateTime.Now;
                OldDoneNum = model.DoneNum;
                model.DoneNum = DoneNum;

                dal.Update(model);






                DAL.OrderToWorkDoneLogDAL logDal = new DAL.OrderToWorkDoneLogDAL();
                Model.OrderToWorkDoneLogModel logModel = new OrderToWorkDoneLogModel();
                logModel.OrderToWorkDetailId = OrderToWorkDetailId;
                logModel.ClothesSizeId = ClothesSizeId;
                logModel.DoneLogTypeId = DoneLogTypeId;
                logModel.OldDoneNum = OldDoneNum;
                logModel.ChangeDoneNum = DoneNum;
                logModel.CreatTime = model.ChangeTime;
                if (DoneLogTypeId == 20)
                {
                    UserBLL ubll = new UserBLL();
                    logModel.ReKey = ubll.CurrentUserId();
                }

                logDal.Add(logModel);



                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        public void AllotOrderToWork(List<OrderToWorkDetailModel> detailList, List<OrderToWorkDetailVsClothesSizeModel> vsList)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                string OrderId = "";
                StringBuilder s = new StringBuilder();
                DAL.OrderToWorkDetailDAL detailDal = new DAL.OrderToWorkDetailDAL();
                DAL.OrderToWorkDetailVsClothesSizeDAL vsDal = new DAL.OrderToWorkDetailVsClothesSizeDAL();
                StringBuilder w = new StringBuilder();
                w.Append("分派工单:");
                foreach (Model.OrderToWorkDetailModel model in detailList)
                {

                    detailDal.Add(model);
                    //判断是不是在OrderDetail中存在?




                    foreach (Model.OrderToWorkDetailVsClothesSizeModel vsModel in vsList)
                    {

                        StringBuilder ww = new StringBuilder();
                        if (vsModel.Memo == "")
                        {
                            throw new Exception("vsModel.Memo备注颜色不能为空!");
                        }
                        if (model.Color == "")
                        {
                            throw new Exception("Color不能为空!");
                        }
                        ww.Append(model.Color + ":");
                        if (vsModel.ClothesSizeId == 0)
                        {

                            throw new Exception("ClothesSizeId不能为0!");
                        }
                        if (model.OrderToWorkDetailId == 0)
                        {
                            throw new Exception("OrderToWorkDetailId不能为0!");
                        }
                        if (model.OrderToWorkId == 0)
                        {
                            throw new Exception("OrderToWorkId不能为0!");
                        }

                        s.Clear();


                        if (vsModel.Memo == model.Color)
                        {
                            s.Append(" DECLARE @OrderId AS VARCHAR(50) =(SELECT o.OrderId FROM dbo.OrderToWorkDetail d INNER JOIN dbo.OrderToWork o ON  o.OrderToWorkId = d.OrderToWorkId WHERE OrderToWorkDetailId=" + model.OrderToWorkDetailId + ") ");
                            s.Append(" DECLARE @Quantity AS DECIMAL =(SELECT  ISNULL( SUM(Num),0) FROM dbo.OrderDetailVsClothesSizeView WHERE OrderId=@OrderId AND Color='" + model.Color + "' AND ClothesSizeId=" + vsModel.ClothesSizeId + " ) ");
                            s.Append(" DECLARE @WorkQuantity AS DECIMAL =( SELECT  ISNULL( SUM(Num),0) FROM dbo.OrderToWorkDetailVsClothesSizeView WHERE OrderId=@OrderId AND Color='" + model.Color + "' AND ClothesSizeId=" + vsModel.ClothesSizeId + " ) ");
                            s.Append(" DECLARE @ClothesSizeName AS VARCHAR(50) =(SELECT ClothesSizeName FROM  dbo.ClothesSize WHERE ClothesSizeId=" + vsModel.ClothesSizeId + ") ");
                            s.Append(" SELECT @Quantity AS Quantity,@WorkQuantity AS WorkQuantity,@OrderId AS OrderId,@ClothesSizeName as ClothesSizeName ");
                            s.Append("  ");
                            DataSet ds = DAL.DalComm.BackData(s.ToString());
                            DataTable dt = ds.Tables[0];
                            DataRow dr = dt.Rows[0];
                            decimal Quantity = decimal.Parse(dr["Quantity"].ToString());
                            decimal WorkQuantity = decimal.Parse(dr["WorkQuantity"].ToString());

                            OrderId = dr["OrderId"].ToString();
                            if (WorkQuantity > Quantity)
                            {
                                throw new Exception("[颜色:" + model.Color + "][尺码编号:" + vsModel.ClothesSizeId + "]的总数量为" + Quantity + ",分派数量" + WorkQuantity + "大于了总数量.");
                            }

                            vsModel.OrderToWorkDetailId = model.OrderToWorkDetailId;

                            ww.Append("[" + dr["ClothesSizeName"] + "]:" + WorkQuantity + "； ");
                            vsDal.Add(vsModel);
                        }
                        else
                        {
                            ww.Clear();
                        }
                        w.Append(ww);
                    }




                }






                DataSet dsOvw = CountOrderToWork(detailList[0].OrderToWorkId);
                DataTable dtOvw = dsOvw.Tables[1];

                if (dtOvw.Rows.Count == 0)
                {
                    throw new Exception("没有找到工单数据!");
                }
                DataRow drOvw = dtOvw.Rows[0];

                s.Clear();

                s.Append(" UPDATE dbo.OrderToWork SET OrderToWorkStatusId='30' WHERE OrderToWorkId=" + detailList[0].OrderToWorkId + " ");

                s.Append(" UPDATE dbo.OrderVsMember SET VsStatus=20 WHERE OrderId='" + OrderId + "' AND MemberId=" + drOvw["MemberId"].ToString() + "  ");
                DAL.DalComm.ExReInt(s.ToString());


                #region 建立日志

                if (OrderId == "")
                {
                    throw new Exception("OrderId不能为空!");
                }

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 20030;  //分派工单, 则工单即为生产中
                LogModel.OrderLogTitle = w.ToString();
                LogModel.OrderToWorkId = detailList[0].OrderToWorkId;
                LogModel.MemberId = decimal.Parse(drOvw["MemberId"].ToString());
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
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


                //推送消息
                MsgBLL msgBll = new MsgBLL();

                msgBll.SendMsgToDevice(10, "有新的工单分派给您, 请前往[我的工单]查看", "AllotOrderToWork", drOvw["MemberId"].ToString(), "");

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

        }

        public void SaveOrderInfo(OrderInfoModel model)
        {
            DAL.OrderInfoDAL dal = new DAL.OrderInfoDAL();


            //if (model.PlanningTime < model.ReceivedTime)
            //{
            //    throw new Exception("领取裁片时间必须小于交货时间");
            //}

            #region 自动计算验证
            //自动计算交货时间, 裁片时间+货期
            model.PlanningTime = BLL.FormulaBLL.PlanningTime(model.PlanningDay, model.ReceivedTime);


            //结算最小接单数量, 每个人的平均接单数量
            model.MinQuantity = BLL.FormulaBLL.MinQuantity(model.OrderQuantity, model.Places);
            if (model.ReleaseTypeId == 20)
            {//整单发布
                model.Places = 1;
                model.MinQuantity = model.OrderQuantity;
            }


            //自动计算对外发布的登记人数
            model.OutPlaces = model.Places * 2;
            #endregion




            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                if (model.OrderId == "")
                {



                    model.OrderId = Common.TimeString.GetNow_ff();

                    model.CreateUserId = "wangli";
                    model.CreateTime = DateTime.Now;
                    model.DoneTime = DateTime.Parse("3000-01-01");
                    model.ReceivedTime = DateTime.Now.AddDays(3);
                    model.LimitTime = DateTime.Parse("3000-01-01");
                    model.PlanningTime = DateTime.Now.AddDays(5);

                    dal.Add(model);

                    DAL.OrderExpectDAL edal = new DAL.OrderExpectDAL();
                    Model.OrderExpectModel emodel = new OrderExpectModel();
                    emodel.Num = 0;
                    emodel.OrderId = model.OrderId;
                    emodel.OrderExpectDay = 1;
                    edal.Add(emodel);

                    emodel.OrderExpectDay = 2;
                    edal.Add(emodel);

                    emodel.OrderExpectDay = 3;
                    edal.Add(emodel);


                    #region 建立日志

                    Model.OrderLogModel LogModel = new OrderLogModel();
                    LogModel.CreateTime = DateTime.Now;
                    LogModel.OrderId = model.OrderId;
                    LogModel.OrderLogClassId = 5;
                    LogModel.OrderLogTitle = "建立草稿";
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
                    dal.Update(model);

                }


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        /// <summary>
        /// 将一个外发订单改为抢单状态
        /// </summary>
        /// <param name="OrderId"></param>
        public void OrderToQiangDan(string OrderId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                StringBuilder s = new StringBuilder();
                s.Append(" UPDATE dbo.OrderInfo SET OrderStatusId=20 WHERE OrderId='" + OrderId + "' ");
                DAL.DalComm.ExReInt(s.ToString());



                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 20;
                LogModel.OrderLogTitle = "将订单改为抢单状态";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                LogModel.UserId = ubll.CurrentUserId();
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        public void OrderToZuoFei(string OrderId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.OrderInfo SET OrderStatusId=-10 WHERE OrderId='" + OrderId + "' ");
            DAL.DalComm.ExReInt(s.ToString());
        }

        public void OrderToDone(string OrderId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                StringBuilder s = new StringBuilder();
                s.Append(" UPDATE dbo.OrderInfo SET OrderStatusId=100 , DoneTime='" + DateTime.Now + "' WHERE OrderId='" + OrderId + "' ");


                DAL.DalComm.ExReInt(s.ToString());


                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 100;
                LogModel.OrderLogTitle = "将订单改为完成状态";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                LogModel.UserId = ubll.CurrentUserId();
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        public void OrderToFaBu(string OrderId)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();


                s.Append(" SELECT * FROM dbo.OrderInfo WHERE OrderId='" + OrderId + "' ");



                s.Append(" UPDATE dbo.OrderInfo SET OrderStatusId=10  WHERE OrderId='" + OrderId + "' ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("找不到订单数据");
                }
                DataRow dr = dt.Rows[0];
                int Places = int.Parse(dr["Places"].ToString());
                if (Places == 0)
                {
                    throw new Exception("名额等于0的订单不能发布!");
                }


                int ReleaseTypeId = int.Parse(dr["ReleaseTypeId"].ToString());

                decimal MinQuantity = decimal.Parse(dr["MinQuantity"].ToString());

                if (ReleaseTypeId == 10 && MinQuantity <= 0)

                {
                    throw new Exception("拆单发布时起接数量不能为0!");

                }


                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 10;
                LogModel.OrderLogTitle = "将订单改为发布状态";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                LogModel.UserId = ubll.CurrentUserId();
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

        }


        /// <summary>
        /// 登记订单
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrderVsMember(OrderVsMemberModel model)
        {


            var VsType = model.VsType;

            if (model.OrderId == "")
            {
                throw new Exception("订单编号不能为空!");
            }

            if (model.MemberId == 0)
            {
                throw new Exception("用户编号不能为空!");
            }


            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadCommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion



                DAL.OrderVsMemberDAL dal = new DAL.OrderVsMemberDAL();

                var i = dal.ExInt(" OrderId='" + model.OrderId + "' and MemberId=" + model.MemberId + " ");

                if (i > 0)
                {

                    model = dal.GetModel(model.OrderId, model.MemberId);
                    model.VsType = VsType;
                    if (model.VsStatus >= 10)
                    {
                        throw new Exception("用户已经关联了此订单!");
                    }

                    if (model.VsStatus <= -10)
                    {

                        if (model.VsType == 20)
                        {
                            //后台主动指派

                        }
                        else
                        {
                            throw new Exception("非常抱歉，用户的申请已经被淘汰，将不能重复登记此订单！");
                        }


                    }


                }

                StringBuilder s = new StringBuilder();
                #region 检测档期


                s.Append(" SELECT PlanningTime,ReceivedTime,OrderStatusId,OrderStatusName,ProcessLvId FROM dbo.OrderView WHERE OrderId='" + model.OrderId + "' ");
                s.Append(" SELECT MaxOrderPlanningTime,Phone,ProcessLvId,TeamId,TeamLvId FROM dbo.MemberView WHERE MemberId='" + model.MemberId + "'  ");
                DataSet dsVs = DAL.DalComm.BackData(s.ToString());
                DataTable dtOrder = dsVs.Tables[0];
                DataTable dtMember = dsVs.Tables[1];
                if (dtOrder.Rows.Count == 0)
                {
                    throw new Exception("没有找到订单数据!");
                }
                if (dtMember.Rows.Count == 0)
                {
                    throw new Exception("没有找到用户数据!");
                }
                DataRow drMember = dtMember.Rows[0];
                DataRow drOrder = dtOrder.Rows[0];
                int OrderStatusId = int.Parse(drOrder["OrderStatusId"].ToString());
                string OrderStatusName = drOrder["OrderStatusName"].ToString();


                int OrderProcessLvId = int.Parse(drOrder["ProcessLvId"].ToString());
                int MemberProcessLvId = int.Parse(drMember["ProcessLvId"].ToString());



                #region 团队检测
                decimal TeamId = decimal.Parse(drMember["TeamId"].ToString());
                decimal TeamLvId = decimal.Parse(drMember["TeamLvId"].ToString());

                if (TeamId > 0)
                {

                    if (TeamLvId != 100)
                    {
                        throw new Exception("您已经在团队中，只有团队负责人才能够登记订单。");
                    }

                    DataTable dtTeam = DAL.DalComm.BackData("  SELECT * FROM dbo.Team WHERE TeamId=" + TeamId + "").Tables[0];

                    if (dtTeam.Rows.Count == 0)
                    {
                        throw new Exception("找不到TeamId为[" + TeamId + "]的团队!");
                    }
                    DataRow drTeam = dtTeam.Rows[0];

                    model.VsPlaces = int.Parse(drTeam["TeamPlaces"].ToString());

                }
                else
                {
                    model.VsPlaces = 1;
                }




                #endregion

                if (VsType == 10)
                {//抢单

                    if (OrderStatusId != 20)
                    {
                        throw new Exception("此订单状态为[" + OrderStatusName + "]，已不能抢单!");
                    }


                    if (OrderProcessLvId < MemberProcessLvId)
                    {

                        throw new Exception("非常抱歉，此订单要求的技能等级您尚未达到！");
                    }

                }
                else if (VsType == 20)
                {//系统派单

                    if (OrderStatusId < 10 || OrderStatusId > 30)
                    {

                    }


                }




                DateTime PlanningTime = DateTime.Parse(drOrder["PlanningTime"].ToString());
                DateTime ReceivedTime = DateTime.Parse(drOrder["ReceivedTime"].ToString());

                DateTime MaxOrderPlanningTime = DateTime.Parse(drMember["MaxOrderPlanningTime"].ToString());




                if (MaxOrderPlanningTime > ReceivedTime)
                {

                    if (model.VsType == 20)
                    {

                    }
                    else
                    {

                        throw new Exception("您目前同档期已申请订单，您可以申请其他档期订单。");
                    }

                }

                if (DateTime.Now > ReceivedTime)
                {

                    if (model.VsType == 20)
                    {

                        //主动派单时, 领取裁片日期已过,予以忽略
                    }
                    else
                    {
                        throw new Exception("领取裁片时间已过。");

                    }


                }
                if (DateTime.Now > PlanningTime)
                {
                    throw new Exception("交货时间已过。");
                }


                #endregion

                model.VsStatus = 10;  //如果是之前抢过单, 只要在SaveOrderVsMember都是新登记的
                if (i > 0)
                {
                    dal.Update(model);
                }
                else
                {
                    dal.Add(model);//写入数据
                }





                s.Clear();

                #region 新的档期开始
                MaxOrderPlanningTime = SetMaxOrderPlanningTime(model.MemberId);

                #endregion





                s.Append(" DECLARE @OrderPlaces  AS int =( SELECT ISNULL(SUM(VsPlaces),0) FROM dbo.OrderVsMember WHERE OrderId='" + model.OrderId + "' and VsStatus>=10 ) ");

                s.Append(" DECLARE @Places  AS int =( SELECT Places FROM dbo.OrderInfo WHERE OrderId='" + model.OrderId + "' ) ");

                s.Append("   UPDATE dbo.OrderInfo SET OnlyPlaces=(OutPlaces- @OrderPlaces)  ");   //抢单发布人数-登记人数
                s.Append("    ");
                s.Append("  WHERE OrderId='" + model.OrderId + "'  ");

                s.Append(" SELECT OnlyPlaces FROM dbo.OrderInfo WHERE OrderId='" + model.OrderId + "'  ");




                DataSet ds = DAL.DalComm.BackData(s.ToString());



                #region 名额计算

                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int OnlyPlaces = int.Parse(dr["OnlyPlaces"].ToString());
                if (OnlyPlaces < 0)
                {

                    throw new Exception("名额已满");
                }

                #endregion


                var VsTypeName = "成功抢单";
                if (model.VsType == 20)
                {
                    VsTypeName = "主动派单";
                }

                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = model.OrderId;
                LogModel.OrderLogClassId = 20000;
                LogModel.OrderLogTitle = "用户[" + drMember["Phone"] + "]已登记,档期重置为[" + MaxOrderPlanningTime + "] (" + VsTypeName + ")";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                LogModel.MemberId = model.MemberId;

                BLL.UserBLL ubll = new BLL.UserBLL();
                try
                {
                    LogModel.UserId = ubll.CurrentUserId();
                    //这里因为有可能是前台用户发起, 也有可能是后台用户发起
                }
                catch (Exception)
                {


                }

                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion

                #region 推送消息
                BLL.MsgBLL msgBll = new MsgBLL();
                msgBll.SendMsgToUser("1999001", new MsgTextModel()
                {
                    CreateTime = DateTime.Now,
                    MsgContent = LogModel.OrderLogTitle,
                    MsgTitle = "",
                    MsgType = "MemberVsOrderToWork",
                    EndTime = DateTime.Now.AddDays(1),
                    Extra = "{ \"OrderId\":\"" + LogModel.OrderId + "\" ,MemberId:" + model.MemberId + "}"

                });

                string msgContent = "您已经接单申请已经提交, 我们会及时处理, 同时您的生产档期已更改为[" + MaxOrderPlanningTime.ToString("yyyy年MM月dd日 HH时mm分") + "]在此期间您将不能承接其他订单.";
                if (model.VsType == 20)
                {
                    msgContent = "您好,我们已为您登记了新订单,与此同时您的生产档期已更改为[" + MaxOrderPlanningTime.ToString("yyyy年MM月dd日 HH时mm分") + "]在此期间您将不能承接其他订单.";
                }

                msgBll.SendMsgToDevice(10, msgContent, "MemberVsOrderToWork", model.MemberId.ToString(), "", "{ OrderId:'" + model.OrderId + "' }");
                #endregion

                CountOrder(model.OrderId);  //统计订单             
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }


        //计算用户档期
        public DateTime SetMaxOrderPlanningTime(decimal MemberId)
        {

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT  ISNULL( MAX(o.PlanningTime),GETDATE()) as MaxOrderPlanningTime ,Count(0) as OrderPlanningNum  FROM dbo.OrderVsMemberView ovm ");
            s.Append(" INNER JOIN  dbo.OrderView o ON ovm.OrderId=o.OrderId and   ovm.VsStatus BETWEEN 0 AND 20 ");   //0以下的都是作废的
            s.Append(" WHERE ovm.MemberId=" + MemberId + "  AND o.PlanningTime>GETDATE() ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            DateTime MaxOrderPlanningTime = DateTime.Parse(dr["MaxOrderPlanningTime"].ToString());
            s.Clear();

            s.Append(" UPDATE dbo.member SET MaxOrderPlanningTime='" + MaxOrderPlanningTime + "' WHERE MemberId=" + MemberId + " ");

            DAL.DalComm.ExReInt(s.ToString());

            return MaxOrderPlanningTime;








        }

        /// <summary>
        /// 改变订单为生产状态
        /// </summary>
        /// <param name="OrderId"></param>
        public void OrderToProduction(string OrderId)
        {


            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();

                #region 释放所有0派单工人的档期
                s.Append(" UPDATE dbo.Member SET MaxOrderPlanningTime=GETDATE() WHERE MemberId IN   ");

                s.Append(" (SELECT om.MemberId FROM dbo.OrderVsMember om WHERE om.OrderId='" + OrderId + "' AND  om.MemberId NOT IN (SELECT MemberId FROM  dbo.OrderToWork WHERE OrderId='" + OrderId + "') ) ");

                #endregion


                s.Append(" UPDATE dbo.OrderInfo SET OrderStatusId=30  WHERE OrderId='" + OrderId + "' ");



                DAL.DalComm.ExReInt(s.ToString());
                #region 建立日志

                Model.OrderLogModel LogModel = new OrderLogModel();
                LogModel.CreateTime = DateTime.Now;
                LogModel.OrderId = OrderId;
                LogModel.OrderLogClassId = 30;
                LogModel.OrderLogTitle = "将订单改为生产状态";
                LogModel.OrderToWorkId = 0;
                LogModel.ReKey = "";
                LogModel.ReKey2 = "";
                BLL.UserBLL ubll = new BLL.UserBLL();
                LogModel.UserId = ubll.CurrentUserId();
                DAL.OrderLogDAL LogDal = new DAL.OrderLogDAL();
                LogDal.Add(LogModel);
                #endregion


                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        public DataSet CountOrder(string OrderId)
        {
            DataSet ds;
            StringBuilder s = new StringBuilder();




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

            s.Append(" DECLARE @OrderPlaces  AS int =( SELECT ISNULL(SUM(VsPlaces),0) FROM dbo.OrderVsMember WHERE OrderId='" + OrderId + "' and VsStatus>=10 ) ");




            s.Append("   UPDATE dbo.OrderInfo SET OrderQuantity=@OrderQuantity,CheckQuantity=@CheckQuantity   ");
            s.Append("  ,OnlyPlaces=(OutPlaces- @OrderPlaces )  ");
            s.Append("  ,WorkQuantity=@WorkQuantity  ");
            s.Append("  WHERE OrderId='" + OrderId + "'  ");
            s.Append("  SELECT @DoneQuantity AS  DoneQuantity,@CheckQuantity AS CheckQuantity,@OrderToWorkNum AS OrderToWorkNum,@OrderVsMemberNum AS OrderVsMemberNum  ");
            s.Append("  ,@OrderQuantity AS OrderQuantity  ");
            s.Append("  ,@WorkQuantity AS WorkQuantity , Places,ReleaseTypeId,   MinQuantity from OrderInfo where OrderId='" + OrderId + "' ");  //[0]



            s.Append(" SELECT MemberId FROM dbo.OrderVsMember WHERE OrderId='" + OrderId + "'  "); //[1]





            ds = DAL.DalComm.BackData(s.ToString());




            DataRow dr = ds.Tables[0].Rows[0];
            decimal OrderQuantity = decimal.Parse(dr["OrderQuantity"].ToString());

            int Places = int.Parse(dr["Places"].ToString());
            int ReleaseTypeId = int.Parse(dr["ReleaseTypeId"].ToString());
            s.Clear();

            #region 计算MinQuantity
            decimal MinQuantity = BLL.FormulaBLL.MinQuantity(OrderQuantity, Places);


            if (ReleaseTypeId == 20)
            {
                Places = 1;
                MinQuantity = OrderQuantity;
                s.Append("         UPDATE dbo.OrderInfo SET MinQuantity=OrderQuantity,Places=" + Places + " WHERE OrderId='" + OrderId + "' ");
            }
            else
            {
                s.Append("         UPDATE dbo.OrderInfo SET MinQuantity='" + MinQuantity + "' WHERE OrderId='" + OrderId + "' ");
            }

            DAL.DalComm.ExReInt(s.ToString());


            #endregion
            ds.Tables[0].Rows[0]["MinQuantity"] = MinQuantity;
            ds.Tables[0].Rows[0]["Places"] = Places;


            #region 统计一个订单登记的用户数 OrderVsMember
            //DataTable dtOrderVsMember = ds.Tables[1];
            //string str = "";
            //if (dtOrderVsMember.Rows.Count > 0)
            //{
            //    List<string> VsMemberArray = new List<string>();
            //    foreach (DataRow drOrderVsMember in dtOrderVsMember.Rows)
            //    {

            //        VsMemberArray.Add(drOrderVsMember["MemberId"].ToString());

            //    }

            //    str = string.Join(",", VsMemberArray);

            //}
            //s.Append(" UPDATE  dbo.OrderInfo SET VsMemberArray='" + str + "' WHERE OrderId='" + OrderId + "' ");
            //DAL.DalComm.ExReInt(s.ToString());
            #endregion
            return ds;

        }

        public void SaveOrderToWorkDetailVsClothesSize(OrderToWorkDetailVsClothesSizeModel vModel)
        {
            DAL.OrderToWorkDetailVsClothesSizeDAL dal = new DAL.OrderToWorkDetailVsClothesSizeDAL();
            int i = dal.ExInt(" ClothesSizeId=" + vModel.ClothesSizeId + " and OrderToWorkDetailId=" + vModel.OrderToWorkDetailId + " ");

            if (i > 0)
            {
                dal.Update(vModel);

            }
            else
            {
                dal.Add(vModel);
            }
        }

        public void SaveOrderToWorkDetail(OrderToWorkDetailModel dModel)
        {
            DAL.OrderToWorkDetailDAL dal = new DAL.OrderToWorkDetailDAL();
            dModel.OrderToWorkDetailId = DAL.DalComm.ExDecimal(" SELECT OrderToWorkDetailId FROM  dbo.OrderToWorkDetail where  Color='" + dModel.Color + "' and OrderToWorkId='" + dModel.OrderToWorkId + "' ");
            if (dModel.OrderToWorkDetailId > 0)
            {
                dal.Update(dModel);
            }
            else
            {
                dal.Add(dModel);
            }
        }
    }
}
