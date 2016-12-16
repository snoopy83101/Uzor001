using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using System.Web;
using io.rong;
using Model;
using Newtonsoft.Json.Linq;
using BeeCloud;
using BeeCloud.Model;

namespace BLL
{
    public class MerchantBLL
    {
        #region 公共变量

        private DAL.MerchantDAL MerDal = new DAL.MerchantDAL();
        private DAL.MerchantTypeDAL merTypeDal = new DAL.MerchantTypeDAL();
        private DAL.UserVsMerDAL UserVsMerDal = new DAL.UserVsMerDAL();
        private DAL.ProductDAL ProDal = new DAL.ProductDAL();
        private DAL.ProductTypeDAL ProTypeDal = new DAL.ProductTypeDAL();
        private DAL.ProductClassDAL ProClassDal = new DAL.ProductClassDAL();
        private DAL.ProVsImgDAL ProVsImgDal = new DAL.ProVsImgDAL();
        private DAL.ProVsCommentDAL ProVsComDal = new DAL.ProVsCommentDAL();
        private DAL.CommentDAL CommentDal = new DAL.CommentDAL();
        private DAL.MerVsCommentDAL MvcDal = new DAL.MerVsCommentDAL();

        #endregion 公共变量

        #region 订单


        #region 新订单提醒


        public void SendMsgToUser(List<string> UserList, Model.MsgTextModel MsgTextModel, decimal MerId)
        {

            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfigCache(MerId, 2000);
            DAL.MsgTextDAL dal = new DAL.MsgTextDAL();
            dal.Add(MsgTextModel);


            string reJson = "";

            foreach (string UserId in UserList)
            {


                reJson = RongCloudServer.PublishMessage(MerConfig["RongAppKey"],
                   MerConfig["RongAppSecret"],
                   "messager",
                    UserId,
                   "RC:TxtMsg", //消息类型
                   " {\"content\":\"" + MsgTextModel.MsgTitle + "\",\"extra\":" + MsgTextModel.Extra + "}" //消息内容
                   , "");

            }

            BLL.WxBLL wxBll = new WxBLL();
            try
            {
                wxBll.SendQyTextMsg(UserList, MsgTextModel.MsgTitle, 7);  //这个地方, 以后必须改为配置,这个7是写死的数字
            }
            catch (Exception)
            {

                //如果微信推送不成功就算完吧.可能因为网络问题
            }




        }


        public void SendMsgToUser(string BranchId, Model.MsgTextModel MsgTextModel)
        {

            StringBuilder s = new StringBuilder();
            s.Append("  SELECT MerchantId FROM dbo.Branch WHERE BranchId='" + BranchId + "' ");

            s.Append(" SELECT DeviceId FROM DBMSG.dbo.DevicePush WHERE ReKey='" + BranchId + "' AND PushType='" + MsgTextModel.MsgType + "' GROUP BY DeviceId  ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtMer = ds.Tables[0];

            if (dtMer.Rows.Count != 1)
            {
                throw new Exception(" dtMer行数不能为" + dtMer.Rows.Count + " ");
            }

            DataRow drMer = dtMer.Rows[0];
            decimal MerId = decimal.Parse(drMer["MerchantId"].ToString());


            DataTable dtUser = ds.Tables[1];

            if (dtUser.Rows.Count == 0)
            {
                return;
            }

            List<string> UserList = new List<string>();
            foreach (DataRow drUser in dtUser.Rows)
            {

                UserList.Add(drUser["DeviceId"].ToString());

            }

            SendMsgToUser(UserList, MsgTextModel, MerId);

        }

        public void NewDingDanTiXing(string DingDanId)
        {
            StringBuilder s = new StringBuilder();

            s.Append(" SELECT 详细地址,Tel,ContactName,PayAmount,PayTypeName ,BranchId,BranchName,PeiSongTime1,PeiSongTime2 FROM dbo.DingDanView WITH(NOLOCK) WHERE DingDanId='" + DingDanId + "' ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());


            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("没有知道订单[" + DingDanId + "]");
            }

            DataRow dr = dt.Rows[0];

            DateTime PeiSongTime1 = DateTime.Parse(dr["PeiSongTime1"].ToString());

            DateTime PeiSongTime2 = DateTime.Parse(dr["PeiSongTime2"].ToString());

            string 派送时段 = PeiSongTime1.ToString("MM-dd HH:mm") + "至" + PeiSongTime2.ToString("HH:mm");

            Model.MsgTextModel MsgTextModel = new MsgTextModel()
            {

                EndTime = DateTime.Now.AddDays(2),
                CreateTime = DateTime.Now,
                Extra = "{ \"type\":\"NewDingDan\" }",
                MsgContent = "",

                MsgTitle = "来自[" + dr["BranchName"] + "]的订单,地址:[" + dr["详细地址"] + "];收货人:[" + dr["ContactName"] + "];支付方式:[" + dr["PayTypeName"] + "];支付金额:[" + decimal.Round(decimal.Parse(dr["PayAmount"].ToString()), 2) + "];派送时段:[" + 派送时段 + "];电话:[" + dr["Tel"] + "];",
                MsgType = "NewDingDan"


            };

            SendMsgToUser(dr["BranchId"].ToString(), MsgTextModel);







        }


        #endregion


        #region 审核订单

        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="DingDanId"></param>
        /// <param name="ShenHeId">1为通过,-1为不通过</param>
        /// <param name="ShenHeMemo"></param>
        /// <returns></returns>
        public string ShenHeDingdan(string DingDanId, int ShenHeId, string ShenHeMemo)
        {
            if (DingDanStatus(DingDanId) >= 40)
            {
                throw new Exception("操作失败! 该订单已经进入派送环节!");
            }

            if (ShenHeId == 0)
            {
                throw new Exception("审核ID不能为0!");
            }
            if (DingDanId == "")
            {
                throw new Exception("订单ID没有确定!");
            }
            DataSet ds = GetDingDanInfo(DingDanId);

            DataTable dtDingDanInfo = ds.Tables[0];
            if (dtDingDanInfo.Rows.Count > 0)
            {
                DataRow drDingDanInfo = dtDingDanInfo.Rows[0];

                #region 插入日志

                Model.DingDanLogModel logMod = new Model.DingDanLogModel();
                logMod.DingDanClassId = 1;
                logMod.DingDanLogId = 0;
                logMod.DingDanId = DingDanId;
                BLL.UserBLL ubll = new BLL.UserBLL();

                switch (ShenHeId)
                {
                    case 1: //审核通过

                        logMod.Memo = "订单审核通过,审核员[" + ubll.CurrentUserRealName() + "]";
                        logMod.DingDanLogTypeId = 20;

                        break;

                    case -1://审核不通过

                        if (ShenHeMemo.Trim() == "")
                        {
                            throw new Exception("如果审核不通过, 原因不能为空!");
                        }
                        logMod.Memo = "审核未通过:" + ShenHeMemo + "审核员[" + ubll.CurrentUserRealName() + "]";
                        logMod.DingDanLogTypeId = -10;
                        break;

                    default:
                        break;
                }
                int Status = logMod.DingDanLogTypeId;

                DAL.DalComm.ExReInt(" update CORE.dbo.DingDanInfo set Status=" + Status + " where DingDanId='" + logMod.DingDanId + "' ");
                SaveDingDanLog(logMod);
                return Status.ToString();

                #endregion 插入日志
            }
            else
            {
                throw new Exception("没有找到ID为" + DingDanId + "的订单!");
            }
        }

        #endregion 审核订单


        #region 订单关联派送员


        /// <summary>
        /// 关联派送员,并写入订单日志
        /// </summary>
        /// <param name="DingDanId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public void DingDanVsPaiSongUser(string DingDanId, string UserId)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                StringBuilder s = new StringBuilder();





                s.Append(" SELECT * FROM dbo.UserInfo WITH(NOLOCK) WHERE UserId='" + UserId + "'  ");
                s.Append(" SELECT  Status,PaiSongUserId FROM dbo.DingDanInfo WITH(NOLOCK) WHERE DingDanId='" + DingDanId + "'  ");
                s.Append(" UPDATE dbo.DingDanInfo SET PaiSongUserId='" + UserId + "', Status=40  WHERE DingDanId='" + DingDanId + "' ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dtUser = ds.Tables[0];
                DataTable dtDingDan = ds.Tables[1];
                if (dtUser.Rows.Count == 0)
                {
                    throw new Exception("没有编号为" + UserId + "的后台用户!");
                }

                if (dtDingDan.Rows.Count == 0)
                {
                    throw new Exception("没有订单号为" + DingDanId + "的订单!");
                }

                DataRow drDingDan = dtDingDan.Rows[0];
                if (int.Parse(drDingDan["Status"].ToString()) >= 100)
                {
                    throw new Exception("订单已经送达,无法揽件!");
                }

                if (drDingDan["PaiSongUserId"].ToString() == UserId)
                {
                    throw new Exception("该订单您已经揽件,无需重复揽件!");
                }



                DataRow drUser = dtUser.Rows[0];


                Model.DingDanLogModel logmodel = new Model.DingDanLogModel();
                logmodel.DingDanId = DingDanId;
                logmodel.CreateTime = DateTime.Now;
                logmodel.DingDanClassId = 40;
                logmodel.DingDanLogTypeId = 40;
                logmodel.Memo = "派送员[" + drUser["RealName"] + ",编号:" + drUser["UserId"] + "]已经揽件,联系方式[" + drUser["Phone"] + "].";
                DAL.DingDanLogDAL dal = new DAL.DingDanLogDAL();
                dal.Add(logmodel);

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

        }


        #endregion

        #region 退货

        public DataSet GetThList(string strWhere)
        {
            DAL.ThInfoDAL dal = new DAL.ThInfoDAL();

            return dal.GetList(strWhere);
        }

        public DataSet GetThDetailList(string strWhere)
        {
            StringBuilder s = new StringBuilder();

            s.Append(" SELECT * FROM dbo.ThDetailView with(nolock) where " + strWhere + "");

            return DAL.DalComm.BackData(s.ToString());
        }

        /// <summary>
        /// 保存退货单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ListDetailModel"></param>
        public void SaveTh(Model.ThInfoModel model, List<Model.ThDetailModel> ListDetailModel)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion 事务开启

                decimal MemberId = DAL.DalComm.ExDecimal(" SELECT TOP 1  CreateMember from dbo.DingDanView WHERE DingDanId='" + model.DingDanId + "' ");
                StringBuilder s = new StringBuilder();
                DAL.ThInfoDAL dal = new DAL.ThInfoDAL();
                DAL.ThDetailDAL dalDetail = new DAL.ThDetailDAL();
                if (model.ThId == "")
                {
                    model.ThCreateTime = DateTime.Now;
                    model.ThId = Common.TimeString.GetNow_ff();
                    dal.Add(model);

                    if (ListDetailModel.Count == 0)
                    {
                        throw new Exception("不能保存没有明细的退库单!");
                    }

                    decimal 产品获得积分 = 0;
                    foreach (Model.ThDetailModel DetailModel in ListDetailModel)
                    {
                        DetailModel.ThId = model.ThId;
                        dalDetail.Add(DetailModel);
                        产品获得积分 = 产品获得积分 + DAL.DalComm.ExDecimal(" SELECT TOP 1  单个积分 from dbo.DingDanDetailView where DingDanDetailId ='" + DetailModel.DingDanDetailId + "' ");
                    }

                    #region 写入订单日志

                    Model.DingDanLogModel ddLogMod = new Model.DingDanLogModel();
                    ddLogMod.CreateTime = model.ThCreateTime;
                    ddLogMod.DingDanClassId = 1;
                    ddLogMod.DingDanLogTypeId = 200;
                    ddLogMod.DingDanId = model.DingDanId;
                    ddLogMod.Memo = "退货单[" + model.ThId + "](取消订单获得积分" + Convert.ToInt32(产品获得积分) + ", 退回积分" + Convert.ToInt32(model.ThJiFen) + ", 退货金额" + Math.Round(model.ThAmount, 2) + ")";
                    SaveDingDanLog(ddLogMod);

                    #endregion 写入订单日志

                    #region 改变订单状态为已经退货

                    s.Append(" update CORE.dbo.DingDanInfo set Status='" + ddLogMod.DingDanLogTypeId + "' where DingDanId='" + model.DingDanId + "' ");
                    DAL.DalComm.ExReInt(s.ToString());

                    #endregion 改变订单状态为已经退货

                    if (MemberId == 0)
                    {
                        throw new Exception("MemberId不能为0!");
                    }

                    if (产品获得积分 != 0)
                    {
                        #region 将订单产品获得的积分减走

                        产品获得积分 = (产品获得积分 * -1);

                        Model.JiFenChangeModel jfChangeMod = new Model.JiFenChangeModel();
                        jfChangeMod.CreateTime = model.ThCreateTime;
                        jfChangeMod.JiFenChangeClassId = 1;
                        jfChangeMod.JifenChangeTypeId = 4;
                        jfChangeMod.JiFenChangeMemo = "订单[" + model.DingDanId + "]退货时作废";
                        jfChangeMod.JiFenChangeNum = 产品获得积分;
                        jfChangeMod.ReKey = model.ThId;
                        jfChangeMod.MemberId = MemberId;
                        ChangeMemberJiFen(((int)产品获得积分), MemberId, jfChangeMod);

                        #endregion 将订单产品获得的积分减走
                    }

                    if (model.ThJiFen != 0)
                    {
                        #region 将积分退回给用户

                        Model.JiFenChangeModel jfChangeMod = new Model.JiFenChangeModel();
                        jfChangeMod.CreateTime = model.ThCreateTime;
                        jfChangeMod.JiFenChangeClassId = 1;
                        jfChangeMod.JifenChangeTypeId = 3;
                        jfChangeMod.JiFenChangeMemo = "订单[" + model.DingDanId + "]退货时返还";
                        jfChangeMod.JiFenChangeNum = model.ThJiFen;
                        jfChangeMod.ReKey = model.ThId;
                        jfChangeMod.MemberId = MemberId;
                        ChangeMemberJiFen((int)model.ThJiFen, MemberId, jfChangeMod);

                        #endregion 将积分退回给用户
                    }
                }
                else
                {
                    throw new Exception("退库单不允许修改!");
                }

                BLL.StaticBLL.MemberNum(MemberId); //统计数据更新

                #region 事务关闭

                transactionScope.Complete();
            }

            #endregion 事务关闭
        }

        #endregion 退货

        #region 订单(明细)评价

        public DataSet GetPingJiaPageList(string StrWhere, int CurrentPage, int PageSize, string col)
        {
            DAL.PingJiaInfoDAL dal = new DAL.PingJiaInfoDAL();
            return dal.GetPageList(StrWhere, CurrentPage, PageSize, col);
        }

        /// <summary>
        /// 保存评价,添加评价只能从前台发起
        /// </summary>
        /// <param name="model"></param>
        public void SavePingJia(Model.PingJiaInfoModel model, ref int GetJiFen)
        {
            //decimal ThQuantity = DAL.DalComm.ExDecimal("  SELECT ThQuantity FROM dbo.DingDanDetailView WHERE DingDanDetailId='"+model.DingDanDetailId+"'");
            //if (ThQuantity > 0)
            //{
            //    throw new Exception("对不起, 这条订单明细已经发生退货, 无法评价!");
            //}

            DAL.PingJiaInfoDAL dal = new DAL.PingJiaInfoDAL();
            if (model.PingJiaId == 0)
            {
                //添加评价
                model.CreateTime = DateTime.Now;
                if (model.HuiPingTime == new DateTime())
                {
                    model.HuiPingTime = DateTime.Parse("3000-01-01");
                }

                #region 事务开启

                TransactionOptions transactionOption = new TransactionOptions();
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    #endregion 事务开启

                    #region 评价得积分

                    StringBuilder s = new StringBuilder();
                    s.Append("    SELECT * from dbo.DingDanDetailView WITH(NOLOCK) WHERE DingDanDetailId='" + model.DingDanDetailId + "' ");

                    DataSet dsDingDanDetail = DAL.DalComm.BackData(s.ToString());
                    DataTable dtDingDanDetail = dsDingDanDetail.Tables[0];
                    if (dtDingDanDetail.Rows.Count != 1)
                    {
                        throw new Exception("评价对应的订单数量不能为" + dtDingDanDetail.Rows + "!");
                    }

                    DataRow drDingDanDetail = dtDingDanDetail.Rows[0];
                    decimal MerId = decimal.Parse(drDingDanDetail["MerchantId"].ToString());

                    string PingJiaJiFen = BLL.StaticBLL.MerOneConfig(MerId, "PingJiaJiFen", "0");  //取得评价得积分的配置!
                    if (PingJiaJiFen == "1")
                    {
                        //配置允许评价得积分!
                        int 已评价数 = int.Parse(drDingDanDetail["PingJiaId"].ToString());
                        if (已评价数 > 0)
                        {
                            throw new Exception("不能多次评价哦!");
                        }

                        decimal 获得积分 = decimal.Parse(drDingDanDetail["获得积分"].ToString());
                        decimal MemberId = decimal.Parse(drDingDanDetail["CreateMember"].ToString());

                        dal.Add(model);  //添加评价

                        获得积分 = decimal.Parse(BLL.StaticBLL.MerOneConfig(MerId, "PingJiaJiFenNum", "0"));


                        Model.JiFenChangeModel ModChange = new Model.JiFenChangeModel();
                        ModChange.CreateTime = model.CreateTime;
                        ModChange.JifenChangeTypeId = 9;
                        ModChange.MemberId = MemberId;
                        ModChange.ReKey = model.PingJiaId.ToString();
                        ModChange.JiFenChangeNum = 获得积分;
                        GetJiFen = Convert.ToInt32(获得积分);
                        ModChange.JiFenChangeMemo = "购买" + drDingDanDetail["产品名称"] + "并如实评价。";
                        ChangeMemberJiFen(ModChange);

                        BLL.StaticBLL.MemberNum(MemberId); //统计数据更新
                    }

                    #endregion 评价得积分

                    #region 事务关闭

                    transactionScope.Complete();
                }

                #endregion 事务关闭
            }
            else
            {
                dal.Update(model);
            }
        }

        #endregion 订单(明细)评价

        #region 订单维护

        public void RemoveDetail(decimal DingDanDetailId)
        {
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            dal.DeleteList("DingDanDetailId='" + DingDanDetailId + "'");
        }

        #endregion 订单维护

        public int DingDanStatus(string DingDanId)
        {
            return DAL.DalComm.ExInt(" SELECT Status FROM dbo.DingDanView WHERE DingDanId='" + DingDanId + "' ");
        }

        /// <summary>
        /// 确认配货完成
        /// </summary>
        /// <param name="DingDanId"></param>
        /// <param name="PeiHuoUserId"></param>
        public void QueRenPeiHuo(string DingDanId, string PeiHuoUserId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion 事务开启

                StringBuilder s = new StringBuilder();
                UserBLL ubll = new UserBLL();
                StringBuilder w = new StringBuilder();

                if (DingDanStatus(DingDanId) >= 110)
                {
                    throw new Exception("操作失败! 该订单状态已经确认收货!");
                }

                s.Append(" Update dbo.DingDanInfo set Status=30 ,PeiHuoTime='" + DateTime.Now + "' where DingDanId='" + DingDanId + "' ");

                DataTable dt配货员 = DAL.DalComm.BackData(" SELECT RealName,Tell,Phone FROM dbo.UserInfo WITH(NOLOCK) WHERE UserId='" + PeiHuoUserId + "'  ").Tables[0];

                Model.DingDanLogModel logModel = new Model.DingDanLogModel();
                logModel.DingDanClassId = 1;
                logModel.DingDanLogTypeId = 30;
                logModel.DingDanId = DingDanId;
                logModel.CreateTime = DateTime.Now;
                string 配货员姓名 = "";
                if (dt配货员.Rows.Count > 0)
                {
                    配货员姓名 = dt配货员.Rows[0]["RealName"].ToString();
                }
                logModel.Memo = "配货员:" + 配货员姓名 + ",已经确认完成配货,订单状态转为配货完成!";
                SaveDingDanLog(logModel);
                DAL.DalComm.ExReInt(s.ToString());



                #region 事务关闭

                transactionScope.Complete();
            }

            #endregion 事务关闭
            decimal Merid = DAL.DalComm.ExDecimal(" select MerchantId from dingdaninfo with(nolock) where dingdanid = '" + DingDanId + "' ");
            //  NewDingDanTiXing(DingDanId);
        }

        public int CopyPro(string BranchId)
        {
            decimal MerId = DAL.DalComm.ExDecimal(" SELECT MerchantId FROM dbo.Branch WHERE BranchId='" + BranchId + "' ");
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0! 分部编号BranchId为" + BranchId + "时没有上级商家MerId?!");
            }
            StringBuilder s = new StringBuilder();

            s.Append(" DECLARE @BranchId  AS VARCHAR = '" + BranchId + "' ");
            s.Append(" DECLARE @MerId AS DECIMAL=" + MerId + "  ");

            s.Append(@"
            
INSERT INTO dbo.ProVsBranch
        ( BranchId ,
          ProId ,
          RePrice ,
          RePrice2 ,
          RePrice3 ,
          Status ,
          MinQuantity ,
          Zl ,
          MinZl ,
          OnLineLv ,
          ProNum ,
          GetJiFenNum ,
          InheritPeiSongType ,
          InheritProTeXing ,
          IsInfiniteNum ,
          AllowPriceInterface ,
          AllowProNumInterface ,
          InheritJiFenNum ,
          InterfaceBaoZhuangNum ,
          InheritDiscount ,
          Discount,
          ProName,
          KeyWord
        )   ");



            s.Append("    SELECT    '" + BranchId + "',");
            s.Append(@"    p.ProId , -- ProId - varchar(50)
          p.RePrice , -- RePrice - decimal
          p.RePrice2 , -- RePrice2 - decimal
          p.RePrice3 , -- RePrice3 - decimal
          p.Status , -- Status - int
          p.MinQuantity , -- MinQuantity - decimal
          p.Zl , -- Zl - decimal
          p.MinZl , -- MinZl - decimal
          0 , -- OnLineLv - int
          p.ProNum , -- ProNum - decimal
          p.GetJiFenNum , -- GetJiFenNum - decimal
          p.InheritPeiSongType , -- InheritPeiSongType - bit
          p.InheritProTeXing , -- InheritProTeXing - bit
          p.IsInfiniteNum , -- IsInfiniteNum - bit
          p.AllowPriceInterface , -- AllowPriceInterface - bit
          p.AllowProNumInterface , -- AllowProNumInterface - bit
          p.InheritJiFenNum , -- InheritJiFenNum - bit
          p.InterfaceBaoZhuangNum , -- InterfaceBaoZhuangNum - int
          p.InheritDiscount , -- InheritDiscount - bit
          Discount,  -- Discount - decimal
          p.ProName,
          p.KeyWord
          ");
            s.Append("  FROM dbo.Product p WHERE ProId NOT IN (SELECT pvb.ProId FROM dbo.ProVsBranch pvb WHERE BranchId='" + BranchId + "' ) AND MerchantId=" + MerId + "   ");

            s.Append(" UPDATE dbo.Product SET ProName='暂无名称' WHERE ProName=''  ");  //处理没有名称的商品
            s.Append(" UPDATE dbo.product SET KeyWord=ProName WHERE KeyWord='' ");  //处理没有关键字的商品

            //同步商品名称
            s.Append("  UPDATE  pvb SET   ProName= p.ProName   FROM dbo.ProVsBranch pvb INNER JOIN dbo.Product p ON pvb.ProId=p.ProId WHERE pvb.ProName='' ");

            //同步商品关键词
            s.Append(" UPDATE  pvb SET   KeyWord= p.KeyWord   FROM dbo.ProVsBranch pvb INNER JOIN dbo.Product p ON pvb.ProId=p.ProId WHERE pvb.KeyWord='' ");
            s.Append(" UPDATE dbo.ProVsBranch SET InterfaceBaoZhuangNum=1 WHERE InterfaceBaoZhuangNum=0  "); //修正包装
            s.Append(" UPDATE  dbo.Product SET     ProNumCode = ProCode WHERE   ProNumCode = '' AND ProCode<>'' ");  //修正库存条码
            return DAL.DalComm.ExReInt(s.ToString());


        }

        public void SaveBranchVsZone(BranchVsZoneModel model)
        {
            DAL.BranchVsZoneDAL dal = new DAL.BranchVsZoneDAL();

            dal.DeleteList(" BranchId='" + model.BranchId + "' and ZoneId='" + model.ZoneId + "'  ");

            dal.Add(model);


        }


        /// <summary>
        /// 商品包装不能为0!
        /// </summary>
        public void InterfaceBaoZhuangNumNotZero()
        {

            DAL.DalComm.ExReInt(" UPDATE dbo.ProVsBranch SET InterfaceBaoZhuangNum=1 WHERE InterfaceBaoZhuangNum=0 ");
        }

        /// <summary>
        /// 商品的属性, -10已经下架, 0为上架销售
        /// </summary>
        /// <returns></returns>
        public DataSet GetProStatus()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM CORE.dbo.ProStatus ORDER BY ProStatusId DESC ");

            DataSet ds = BLL.StaticBLL.CacheData(s.ToString(), "ProStatus", 1000);
            return ds;
        }


        /// <summary>
        /// 确认订单逻辑
        /// </summary>
        /// <param name="DingDanId"></param>
        /// <param name="PaiSongUserId"></param>
        /// <param name="CzyUserId"></param>
        /// <returns></returns>
        public string QueRenDingDan(string DingDanId, string PaiSongUserId, string CzyUserId)
        {
            StringBuilder w = new StringBuilder();
            decimal.Parse(DingDanId.Replace(".", "")).ToString(); //验证是否可能为订单

            bool b = DAL.DalComm.ExBool("SELECT count(0) FROM dbo.DingDanInfo WHERE DingDanId='" + DingDanId + "' AND Status>=110");

            if (b)
            {
                throw new Exception("该订单已经确认收货,无需再次确认!");
            }

            StringBuilder s = new StringBuilder();

            if (DingDanId.Substring(0, 1) == ".")
            {
                var DingDanWhereStr = DingDanId.Replace(".", "%");

                DateTime dtm1 = DateTime.Now.AddDays(-7);
                DateTime dtm2 = DateTime.Now;

                s.Append("select PayAmount, MerchantId,详细地址,获得总积分,花费总积分,CreateMember,DingDanId from CORE.dbo.DingDanView with(nolock) where DingDanId like '" + DingDanWhereStr + "' and CreateTime BETWEEN '" + dtm1 + "' and '" + dtm2 + "'  ");
            }
            else
            {
                s.Append("select PayAmount,  MerchantId,详细地址,获得总积分,花费总积分,CreateMember,DingDanId from CORE.dbo.DingDanView with(nolock) where DingDanId='" + DingDanId + "'");
            }



            DataSet dsDingDan = DAL.DalComm.BackData(s.ToString());
            DataTable dtDingDan = dsDingDan.Tables[0];

            if (dtDingDan.Rows.Count > 0)
            {
                //if (dtDingDan.Rows.Count > 1)
                //{
                //    string aHerf = "http://www.sjdfcs.com/App/AppDingDanList.aspx?WxOpenId=" + PaiSongUserId + "&DingDanId=" + DingDanId + "";

                //    w.Append("最近出现多个尾号为" + DingDanId + "的订单,");
                //    w.Append("<a href=\"" + aHerf + "\">请点击这里进一步操作</a>");
                //    return w.ToString();
                //}

                //接下来只有一个情况

                DataRow drDingDan = dtDingDan.Rows[0];
                DingDanId = drDingDan["DingDanId"].ToString();

                decimal MerId = decimal.Parse(drDingDan["MerchantId"].ToString());
                DataTable dtPaiSongYuan = DAL.DalComm.BackData(" SELECT RealName,Tell,Phone FROM dbo.UserInfo WITH(NOLOCK) WHERE UserId='" + PaiSongUserId + "'  ").Tables[0];
                if (dtPaiSongYuan.Rows.Count > -1)  //这里不在检测是否派送员
                {//是派送员
                    //改变订单状态为确认收货
                    string ReMsg = "";

                    #region 事务开启

                    TransactionOptions transactionOption = new TransactionOptions();
                    transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                    {
                        #endregion 事务开启

                        #region 订单积分逻辑

                        ChangeMemberJiFen(drDingDan);  //订单积分逻辑,插入用户积分并改变订单状态!

                        #endregion 订单积分逻辑

                        var PaiSongYuanName = "";
                        try
                        {
                            DataRow drPaiSongYuan = dtPaiSongYuan.Rows[0];
                            PaiSongYuanName = drPaiSongYuan["RealName"].ToString();
                        }
                        catch (Exception)
                        {
                            PaiSongYuanName = "未知姓名";
                        }

                        #region 插入派送日志

                        Model.DingDanLogModel logMod = new Model.DingDanLogModel();
                        logMod.CreateTime = DateTime.Now;
                        logMod.DingDanId = DingDanId;
                        logMod.DingDanClassId = 1;
                        logMod.DingDanLogTypeId = 110;
                        logMod.Memo = "订单确认收货,派送员:[" + PaiSongYuanName + "]";
                        MerchantBLL mbll = new MerchantBLL();
                        mbll.SaveDingDanLog(logMod);
                        ReMsg = "您好,派送员[" + PaiSongYuanName + "]("+PaiSongUserId+"),送往[" + drDingDan["详细地址"] + "]订单[" + DingDanId + "]已经确认收货!";

                        #endregion 插入派送日志

                        BLL.StaticBLL.MemberNum(decimal.Parse(drDingDan["CreateMember"].ToString())); //统计数据更新



                 
                        DAL.DalComm.ExInt("    UPDATE dbo.DingDanInfo SET PaiSongUserId='" + PaiSongUserId + "' WHERE DingDanId='" + DingDanId + "' ");


                        #region 推广人员提成



                        s.Clear();
                        s.Append(" SELECT ExtMemberId FROM dbo.MemberView WHERE MemberId='" + drDingDan["CreateMember"].ToString() + "' ");

                        DataSet dsMember = DAL.DalComm.BackData(s.ToString());
                        DataTable dtMember = dsMember.Tables[0];

                        if (dtMember.Rows.Count == 1)
                        {
                            DataRow drMember = dtMember.Rows[0];


                            int ExtMemberId = int.Parse(drMember["ExtMemberId"].ToString());

                            if (ExtMemberId == 0)
                            {

                            }
                            else
                            {
                                DataSet dsExtMember = DAL.DalComm.BackData(" SELECT ext.ExtMemberLv, ext.ExtMemberName, ext.Commission, RealName FROM dbo.MemberView m  LEFT JOIN dbo.ExtMemberDict ext  ON ext.ExtMemberLv = m.ExtMemberLv WHERE MemberId='" + ExtMemberId + "' ");


                                DataTable dtExtMember = dsExtMember.Tables[0];

                                if (dtExtMember.Rows.Count == 0)
                                {

                                    throw new Exception("没有找到编号为[" + ExtMemberId + "]的推广员");
                                }


                                DataRow drExtMember = dtExtMember.Rows[0];

                                int ExtMemberLv = int.Parse(drExtMember["ExtMemberLv"].ToString());


                                if (ExtMemberLv < 10)
                                {
                                    //已经取消了推广员资质,暂时不解除绑定,但是无法收到提成




                                }
                                else
                                {
                                    //检测到推广员
                                    decimal PayAmount = decimal.Parse(drDingDan["PayAmount"].ToString());

                                    decimal Commission = decimal.Parse(drExtMember["Commission"].ToString());

                                    JiFenChangeModel jfModel = new JiFenChangeModel();
                                    jfModel.JiFenChangeNum = Math.Round(PayAmount * Commission * 100);

                                    jfModel.JiFenChangeMemo = "订单[" + DingDanId + "]的推广奖励";
                                    jfModel.ReKey = DingDanId;
                                    jfModel.JifenChangeTypeId = 50;
                                    jfModel.MemberId = ExtMemberId;
                                    jfModel.CreateTime = DateTime.Now;
                                    ChangeMemberJiFen(jfModel);



                                }


                            }









                        }



                        #endregion




                        #region 事务关闭

                        transactionScope.Complete();
                    }

                    #endregion 事务关闭
                    try
                    {
                        //  NewDingDanTiXing(DingDanId);
                    }
                    catch (Exception ex)
                    {


                    }

                    return ReMsg;
                }
                else
                {
                    //如果的确有这个订单, 但是我不是派送员
                    throw new Exception("不是派送员!");
                }
            }
            else
            {
                //如果没有找到这个订单
                throw new Exception("最近没有这个订单!");
            }

            //如果不是数字就不else了
        }

        /// <summary>
        /// 订单积分逻辑, 必须在事务中
        /// </summary>
        /// <param name="drDingDan"></param>
        public void ChangeMemberJiFen(DataRow drDingDan)
        {
            decimal 获得总积分 = decimal.Parse(drDingDan["获得总积分"].ToString());
            decimal 花费总积分 = decimal.Parse(drDingDan["花费总积分"].ToString());
            decimal MemberId = decimal.Parse(drDingDan["CreateMember"].ToString());
            DAL.JiFenChangeDAL dal = new DAL.JiFenChangeDAL();
            Model.JiFenChangeModel model = new Model.JiFenChangeModel();
            model.CreateTime = DateTime.Now;
            model.JiFenChangeClassId = 1;
            model.MemberId = MemberId;
            model.ReKey = drDingDan["DingDanId"].ToString();

            if (获得总积分 != 0)
            {
                model.JifenChangeTypeId = 1;  //借方
                model.JiFenChangeNum = 获得总积分;
                model.JiFenChangeMemo = "订单[" + model.ReKey + "]得到积分" + 获得总积分 + "";
                dal.Add(model);
            }

            if (花费总积分 != 0)
            {
                model.JifenChangeTypeId = 2;  //贷方
                model.JiFenChangeNum = 花费总积分 * -1;
                model.JiFenChangeMemo = "订单[" + model.ReKey + "]花费积分" + 花费总积分 + "";
                dal.Add(model);
            }

            decimal 积分改变 = 获得总积分 - 花费总积分;

            StringBuilder s = new StringBuilder();
            s.Append(" Update dbo.Member set MyJiFen=MyJiFen+" + 积分改变 + " where MemberId=" + MemberId + " ");
            s.Append(" Update dbo.DingDanInfo set  Status=110,EnTime='" + DateTime.Now + "',IsDone=1 where DingDanId=" + drDingDan["DingDanId"] + " ");

            s.Append(@"UPDATE  dbo.Product
SET     BuyLv = ( SELECT    CONVERT(INT, ISNULL(SUM(Quantity), 0)) AS BuyLv
                  FROM      dbo.DingDanDetailView
                  WHERE     Status >= 110
                            AND ProId = dbo.Product.ProId
                )
WHERE   ProId IN (SELECT ProId FROM dbo.DingDanDetailView WHERE DingDanId='" + drDingDan["DingDanId"] + "')");   //修改产品的销量
            DAL.DalComm.ExReInt(s.ToString());
            BLL.StaticBLL.MemberNum(model.MemberId);
        }

        /// <summary>
        /// 积分类型的OP输出
        /// </summary>
        /// <returns></returns>
        public string JiFenTypeSelectOpHtml()
        {
            DataSet ds = DAL.DalComm.BackData("SELECT * FROM dbo.JiFenChangeType WITH(NOLOCK) ");
            DataTable dt = ds.Tables[0];
            StringBuilder w = new StringBuilder();
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["JiFenChangeTypeId"] + "'>" + dr["JiFenChangeTypeName"].ToString() + "</option>");
            }
            return w.ToString();
        }

        /// <summary>
        /// 改变用户积分,并写入积分更改日志
        /// </summary>
        /// <param name="JiFen"></param>
        /// <param name="MemberId"></param>
        /// <param name="model"></param>
        public void ChangeMemberJiFen(int JiFen, decimal MemberId, Model.JiFenChangeModel model)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion 事务开启

                StringBuilder s = new StringBuilder();
                s.Append(" Update dbo.Member set MyJiFen=MyJiFen+" + JiFen + " where MemberId=" + MemberId + " ");
                int i = DAL.DalComm.ExReInt(s.ToString());
                if (i != 1)
                {
                    throw new Exception("更改积分的用户数为" + i + ", MemberId为'" + MemberId + "'?!");
                }
                DAL.JiFenChangeDAL dal = new DAL.JiFenChangeDAL();
                dal.Add(model);

                BLL.StaticBLL.MemberNum(model.MemberId);

                #region 事务关闭

                transactionScope.Complete();
            }

            #endregion 事务关闭
        }

        /// <summary>
        /// 当前最优的方法, 必须在事务中!
        /// </summary>
        /// <param name="model"></param>
        public void ChangeMemberJiFen(Model.JiFenChangeModel model)
        {

            model.JiFenChangeNum = Math.Round(model.JiFenChangeNum);
            if (model.JiFenChangeNum == 0)
            { }
            else
            {


                StringBuilder s = new StringBuilder();
                s.Append(" Update dbo.Member set MyJiFen=MyJiFen+" + model.JiFenChangeNum + " where MemberId=" + model.MemberId + " ");
                int i = DAL.DalComm.ExReInt(s.ToString());
                if (i != 1)
                {
                    throw new Exception("更改积分的用户数为" + i + ", MemberId为'" + model.MemberId + "'?!");
                }
                DAL.JiFenChangeDAL dal = new DAL.JiFenChangeDAL();
                dal.Add(model);
                BLL.StaticBLL.MemberNum(model.MemberId);
            }
        }

        public void SaveDingDanLog(Model.DingDanLogModel model)
        {
            DAL.DingDanLogDAL dal = new DAL.DingDanLogDAL();
            model.CreateTime = DateTime.Now;

            dal.Add(model);
        }

        public Model.DingDanDetailModel GetDingDanDetailModel(decimal DingDanDetailId)
        {
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            return dal.GetModel(DingDanDetailId);
        }

        public DataSet GetDingDanInfo(string DingDanId)
        {
            DAL.DingDanInfoDAL dal = new DAL.DingDanInfoDAL();
            return dal.GetDingDanInfoByDingDanId(DingDanId);
        }



        /// <summary>
        /// 根据订单确认页面的要求, 取得订单的当前信息,table1位订单主体, table2为订单明细, table3为派送方式
        /// </summary>
        /// <param name="DingDanId"></param>
        /// <returns></returns>
        public DataSet GetDingDanInfoByDingDanQueRen(decimal MemberId, string BranchId, string SiteId)
        {

            StringBuilder s = new StringBuilder();

            s.Append(" DECLARE @MemberId AS DECIMAL =" + MemberId + " ");
            s.Append(" DECLARE @ZoneId AS VARCHAR(50) = ISNULL( (SELECT TOP 1 ZoneId FROM dbo.Site WHERE SiteId='" + SiteId + "'),0) ");

            s.Append("  SELECT * FROM dbo.GwcView WHERE MemberId= @MemberId and BranchId='" + BranchId + "' and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId=@ZoneId) ");
            //            s.Append(@"
            //SELECT   psType.PeiSongTypeId
            //FROM    dbo.PeiSongTypeVsProView psType  WITH(NOLOCK) 
            //WHERE   ProId IN ( SELECT   gwc.ProId
            //                   FROM     dbo.GwcView gwc  WITH(NOLOCK) 
            //                   WHERE    MemberId=@MemberId  ) GROUP BY  psType.PeiSongTypeId 
            //");
            s.Append(" SELECT * FROM dbo.PeiSongType WHERE BranchId='" + BranchId + "' order by OrderNo desc ");
            BLL.MemberBLL mbll = new MemberBLL();
            s.Append(" SELECT  TOP 10 *  FROM dbo.AddressView WITH(NOLOCK) WHERE MemberId=" + MemberId + " and Invalid=0 and SiteId='" + SiteId + "' ORDER BY IsDefault DESC ");
            s.Append(" SELECT MyKyJiFen,MyDjJiFen,MyJiFen  FROM dbo.MemberView  WITH(NOLOCK)   WHERE MemberId=" + MemberId + "   ");

            return DAL.DalComm.BackData(s.ToString());

        }



        /// <summary>
        /// 优先在缓存中获得当前的派送方式. 只能在MerId已经确定的项目的应用程序池中获取
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataSet dsPeiSongType(decimal MerId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("  select * from CORE.dbo.PeiSongType with(nolock) where MerId=" + MerId + " order by OrderNo desc  ");

            s.Append(" SELECT * FROM CORE.dbo.PeiSongTimeSolt pts   with(nolock) ");
            s.Append(" LEFT JOIN CORE.dbo.PeiSongType pt  with(nolock) ON pt.PeiSongTypeId = pts.PeiSongTypeId    ");
            s.Append(" WHERE MerId='" + MerId + "'  ");

            return BLL.StaticBLL.CacheData(s.ToString(), "PeiSongType", 3000);
        }


        public DataSet dsPeiSongType(decimal MerId, string BranchId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("  select * from CORE.dbo.PeiSongType with(nolock) where MerId=" + MerId + " and BranchId='" + BranchId + "' order by OrderNo desc  ");

            s.Append(" SELECT * FROM CORE.dbo.PeiSongTimeSolt pts   with(nolock) ");
            s.Append(" LEFT JOIN CORE.dbo.PeiSongType pt  with(nolock) ON pt.PeiSongTypeId = pts.PeiSongTypeId    ");
            s.Append(" WHERE MerId='" + MerId + "' and pt.BranchId='" + BranchId + "'  ");

            return DAL.DalComm.BackData(s.ToString());
        }


        public DataSet GetDingDanDetailList(string strwhere)
        {
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            return dal.GetList(strwhere);
        }

        public void AddDingDan(Model.DingDanInfoModel model)
        {
            DAL.DingDanInfoDAL dal = new DAL.DingDanInfoDAL();
            model.DingDanId = Common.TimeString.GetNow_ff();
            dal.Add(model);
            BLL.StaticBLL.MemberNum(model.CreateMember);
        }

        public void AddDingDanDetail(Model.DingDanDetailModel model)
        {
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 只能修改
        /// </summary>
        /// <param name="model"></param>
        public void SaveDingDanDetail(Model.DingDanDetailModel model)
        {
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            dal.Update(model);
        }

        #region 作废方法

        /// <summary>
        /// 作废一个订单
        /// </summary>
        /// <param name="DingDanId"></param>
        public void doInvalidDingDan(string DingDanId)
        {
            DAL.DalComm.ExReInt(" Update CORE.dbo.DingDanInfo set Status='-1' where DingDanId='" + DingDanId + "' ");
        }

        /// <summary>
        /// 刷新订单的金额,并返回
        /// </summary>
        /// <param name="DingDanId"></param>
        /// <returns></returns>
        public decimal ReAmount(string DingDanId)
        {
            StringBuilder s = new StringBuilder();
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            DataTable dt = dal.GetList(" DingDanId='" + DingDanId + "' ").Tables[0];
            decimal Amount = 0;
            List<string> ProNames = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                decimal Price = decimal.Parse(dr["Price"].ToString());
                decimal Quantity = decimal.Parse(dr["Quantity"].ToString());
                Amount = Amount + (Price * Quantity);

                string ProName = dr["ProName"].ToString() + "[" + dr["Spec"] + "]";
                ProNames.Add(ProName);
            }
            s.Append(" update CORE.dbo.DingDanInfo set  Amount='" + Amount + "'  , Memo='" + string.Join(",", ProNames) + "'  where DingDanId='" + DingDanId + "' ");

            DAL.DalComm.ExReInt(s.ToString());
            return Amount;
        }

        #endregion 作废方法

        #endregion 订单

        #region 产品


        /// <summary>
        /// 生成关键词
        /// </summary>
        /// <param name="ProId"></param>
        /// <returns></returns>
        public int ProductKeyWord(string ProId)
        {
            StringBuilder s = new StringBuilder();
            if (ProId == null)
            {
                ProId = "";
            }

            s.Append(" UPDATE  dbo.Product SET  KeyWord= ISNULL( (SELECT KeyWord FROM dbo.ProView WHERE dbo.ProView.ProId= dbo.Product.ProId) ,'')  ");
            if (ProId != "")
            {
                s.Append(" where ProId='" + ProId + "' ");
            }
            int i = DAL.DalComm.ExReInt(s.ToString());
            return i;
        }


        public int ProVsBranchKeyWord(string ProId)
        {
            StringBuilder s = new StringBuilder();
            if (ProId == null)
            {
                ProId = "";
            }

            s.Append(" UPDATE  dbo.ProVsBranchView SET KeyWord =  ISNULL((ProductClassName+' '+ProTitle+' '+ProName ), '')   ");
            if (ProId != "")
            {
                s.Append(" where ProId='" + ProId + "' ");
            }
            int i = DAL.DalComm.ExReInt(s.ToString());
            return i;

        }

        /// <summary>
        /// 更改类别的积分比率
        /// </summary>
        /// <param name="ProductClassId"></param>
        /// <param name="InheritJiFenNum"></param>
        /// <param name="GetJiFenNum"></param>
        public void ChangeJiFenNumForProClass(decimal ProductClassId, bool InheritJiFenNum, decimal GetJiFenNum)
        {
            StringBuilder s = new StringBuilder();
            s.Append("DECLARE @ProClassId AS DECIMAL = " + ProductClassId + "");
            s.Append(@"SELECT  *
FROM    dbo.ProClassView WITH ( NOLOCK )
WHERE   ProductClassId = @ProClassId                 --本类别(0)

SELECT  *
FROM    dbo.ProClassView WITH ( NOLOCK )
WHERE   ProductClassId = ( SELECT TOP 1           --父类别(1)
                                    ParentProductClassId
                           FROM     dbo.ProClassView WITH ( NOLOCK )
                           WHERE    ProductClassId = @ProClassId
                         )

SELECT  *
FROM    dbo.ProClassView WITH ( NOLOCK )    --子类别只取愿意继承的(2)
WHERE   ParentProductClassId = @ProClassId and InheritProTeXing=1

");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dtProClass = ds.Tables[0];
            DataTable dtParentProductClass = ds.Tables[1];
            DataTable dtCldProductClass = ds.Tables[2];
            // DataTable dtProList = ds.Tables[3];
            if (dtProClass.Rows.Count == 0)
            {
                throw new Exception("没有这个类别!");
            }
            if (InheritJiFenNum)
            {//决定继承上级类别
                if (dtParentProductClass.Rows.Count > 0)
                {
                    DataRow drParentProductClass = dtParentProductClass.Rows[0];
                    GetJiFenNum = decimal.Parse(drParentProductClass["GetJiFenNum"].ToString());   //上级类别的积分比率
                }
                else
                {
                    throw new Exception("这个类别没有上级类别!");
                }
            }

            //开始更新

            s.Clear();
            s.Append("  DECLARE @ProClassId AS DECIMAL = " + ProductClassId + "");
            s.Append("  UPDATE dbo.ProductClass SET GetJiFenNum='" + GetJiFenNum + "' WHERE InheritJiFenNum=1 AND (ParentProductClassId=@ProClassId) ");
            s.Append(" UPDATE dbo.ProductClass SET GetJiFenNum='" + GetJiFenNum + "',InheritJiFenNum='" + InheritJiFenNum + "'   WHERE   ProductClassId=@ProClassId ");
            s.Append(" UPDATE dbo.Product SET GetJiFenNum='" + GetJiFenNum + "' WHERE ProClassId=@ProClassId ");
            DAL.DalComm.ExReInt(s.ToString());
            s.Clear();
            if (dtCldProductClass.Rows.Count > 0)
            { //如果存在愿意继承的子类别的话, 递归方法
                foreach (DataRow drCldProductClass in dtCldProductClass.Rows)
                {
                    decimal CldProClassId = decimal.Parse(drCldProductClass["ProductClassId"].ToString());
                    bool CldInheritJiFenNum = bool.Parse(drCldProductClass["InheritJiFenNum"].ToString());
                    ChangeJiFenNumForProClass(CldProClassId, CldInheritJiFenNum, GetJiFenNum);
                }
            }
        }

        /// <summary>
        /// 更改类别的产品特性
        /// </summary>
        /// <param name="ProductClassId"></param>
        /// <param name="InheritProTeXing"></param>
        /// <param name="ProTeXingId"></param>
        public void ChangeProTeXingForProClass(decimal ProductClassId, bool InheritProTeXing, int ProTeXingId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("DECLARE @ProClassId AS DECIMAL = " + ProductClassId + "");
            s.Append(@"SELECT  *
FROM    dbo.ProClassView WITH ( NOLOCK )
WHERE   ProductClassId = @ProClassId                 --本类别(0)

SELECT  *
FROM    dbo.ProClassView WITH ( NOLOCK )
WHERE   ProductClassId = ( SELECT TOP 1           --父类别(1)
                                    ParentProductClassId
                           FROM     dbo.ProClassView WITH ( NOLOCK )
                           WHERE    ProductClassId = @ProClassId
                         )

SELECT  *
FROM    dbo.ProClassView WITH ( NOLOCK )    --子类别只取愿意继承的(2)
WHERE   ParentProductClassId = @ProClassId and InheritProTeXing=1

");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dtProClass = ds.Tables[0];
            DataTable dtParentProductClass = ds.Tables[1];
            DataTable dtCldProductClass = ds.Tables[2];
            // DataTable dtProList = ds.Tables[3];
            if (dtProClass.Rows.Count == 0)
            {
                throw new Exception("没有这个类别!");
            }
            if (InheritProTeXing)
            {//决定继承上级类别
                if (dtParentProductClass.Rows.Count > 0)
                {
                    DataRow drParentProductClass = dtParentProductClass.Rows[0];
                    ProTeXingId = int.Parse(drParentProductClass["ProTeXing"].ToString());
                }
                else
                {
                    throw new Exception("这个类别没有上级类别!");
                }
            }

            //开始更新

            s.Clear();
            s.Append("DECLARE @ProClassId AS DECIMAL = " + ProductClassId + " ");
            s.Append(" UPDATE dbo.ProductClass SET ProTeXing='" + ProTeXingId + "' WHERE InheritProTeXing=1 AND (ParentProductClassId=@ProClassId) ");
            s.Append(" UPDATE dbo.ProductClass SET ProTeXing='" + ProTeXingId + "',InheritProTeXing='" + InheritProTeXing + "' WHERE   ProductClassId=@ProClassId ");
            s.Append(" UPDATE dbo.Product SET ProTeXing='" + ProTeXingId + "' WHERE ProClassId=@ProClassId ");
            DAL.DalComm.ExReInt(s.ToString());
            s.Clear();
            if (dtCldProductClass.Rows.Count > 0)
            { //如果存在愿意继承的子类别的话, 递归方法
                foreach (DataRow drCldProductClass in dtCldProductClass.Rows)
                {
                    decimal CldProClassId = decimal.Parse(drCldProductClass["ProductClassId"].ToString());
                    bool CldInheritProTeXing = bool.Parse(drCldProductClass["InheritProTeXing"].ToString());  //应该传递子类别的是否继承属性!
                    ChangeProTeXingForProClass(CldProClassId, CldInheritProTeXing, ProTeXingId);
                }
            }
        }

        public void RemoveShouCang(decimal ShouCangId)
        {
            DAL.ShouCangProDAL dal = new DAL.ShouCangProDAL();
            dal.DeleteList(" ShouCangId='" + ShouCangId + "' ");
        }

        public DataSet GetShouCangPageList(string strWhere, int c, int p, string col)
        {
            DAL.ShouCangProDAL dal = new DAL.ShouCangProDAL();
            return dal.GetPageList(strWhere, c, p, col);
        }

        public void SaveShouCangPro(Model.ShouCangProModel model)
        {
            if (model.ShouCangId == 0)
            {
                AddShouCangPro(model);
            }
            else
            {
                ChangeShouCangPro(model);
            }
        }

        public void AddShouCangPro(Model.ShouCangProModel model)
        {
            DAL.ShouCangProDAL dal = new DAL.ShouCangProDAL();

            if (dal.ExInt(" ProId='" + model.ProId + "' and MemberId=" + model.MemberId + " and ShouCangId <> " + model.ShouCangId + " ") > 0)
            {
                throw new Exception("您已经收藏了这个产品!");
            }
            else
            {
                model.CreateTime = DateTime.Now;

                dal.Add(model);
            }
        }

        public void ChangeShouCangPro(Model.ShouCangProModel model)
        {
            DAL.ShouCangProDAL dal = new DAL.ShouCangProDAL();

            dal.Update(model);
        }

        public DataSet GetAuthorInfo(string AuthorId)
        {
            DAL.AuthorInfoDAL dal = new DAL.AuthorInfoDAL();
            DataSet ds = dal.GetAuthorInfo(AuthorId);
            return ds;
        }

        public DataSet GetAuthorPageList(string strWhere, int c)
        {
            DAL.AuthorInfoDAL dal = new DAL.AuthorInfoDAL();
            DataSet ds = dal.GetPageList(strWhere, c, 20, "*");
            return ds;
        }

        public Model.AuthorInfoModel GetAuthorInfoModel(string AuthorId)
        {
            DAL.AuthorInfoDAL dal = new DAL.AuthorInfoDAL();
            return dal.GetModel(AuthorId);
        }

        public void SaveAuthorInfo(Model.AuthorInfoModel model)
        {
            DAL.AuthorInfoDAL dal = new DAL.AuthorInfoDAL();
            if (model.AuthorId == "")
            {
                model.AuthorId = Common.TimeString.GetNow_ff();
                //添加
                dal.Add(model);
            }
            else
            {  // 修改
                dal.Update(model);
            }
        }

        public Model.ProductModel GetProModel(string Proid)
        {
            return ProDal.GetModel(Proid);
        }

        public void SaveProClassInfo(Model.ProductClassModel model)
        {
            DAL.ProductClassDAL dal = new DAL.ProductClassDAL();
            StringBuilder s = new StringBuilder();
            if (model.ParentProductClassId == 0)
            {
                //是首级类别
            }
            else
            { //不是首级类别,判断继承关系
                Model.ProductClassModel ParModel = dal.GetModel(model.ParentProductClassId); //取得外层
                if (model.InheritProTeXing)
                {//如果产品特性继承
                    model.ProTeXing = ParModel.ProTeXing;
                }

                if (model.InheritJiFenNum)
                {//如果积分比率继承
                    model.GetJiFenNum = ParModel.GetJiFenNum;
                }
                if (model.InheritPeiSongType)
                { //如果派送继承
                    s.Clear();
                    s.Append("  DELETE FROM dbo.PeiSongTypeVsProductClass WHERE ProductClassId='" + model.ProductClassId + "' ");

                    s.Append(@"INSERT  dbo.PeiSongTypeVsProductClass
        ( PeiSongTypeId ,
          ProductClassId ,
          VsType
        )");
                    s.Append("  SELECT  PeiSongTypeId , ");
                    s.Append("  " + model.ProductClassId + " , ");
                    s.Append(" VsType ");
                    s.Append(" FROM    dbo.PeiSongTypeVsProductClass ");
                    s.Append(" WHERE   ProductClassId = '" + model.ParentProductClassId + "' ");
                }
            }

            if (model.ProductClassId == 0)
            {
                //添加
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        /// <summary>
        /// 检查当前登录用户是否有权限修改此商家的数据
        /// </summary>
        /// <param name="MerId">传入商家ID</param>
        public void CheckChangeMerPower(decimal MerId)
        {
            UserBLL userBll = new UserBLL();
            string UserId = userBll.CurrentUserId();
            CheckChangeMerPower(UserId, MerId);
        }

        /// <summary>
        /// 检查用户有没有更改此商家数据的权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="MerId"></param>
        public void CheckChangeMerPower(string UserId, decimal MerId)
        {
            DAL.UserVsRoleDAL userRoleDal = new DAL.UserVsRoleDAL();
            DAL.UserVsMerDAL usermerDal = new DAL.UserVsMerDAL();
            DAL.MerRoleVsUserDAL MerRoleVsUserDal = new DAL.MerRoleVsUserDAL();
            DataTable dt = userRoleDal.GetList(" RoleUserId='" + UserId + "' and RoleName='超级管理员' ").Tables[0];
            int i = dt.Rows.Count;
            if (i > 0)
            {//如果是超级管理员，直接通过验证
                return;
            }
            DataTable dt2 = MerRoleVsUserDal.GetList(" UserId ='" + UserId + "' and MerId= '" + MerId + "' ").Tables[0];
            i = dt2.Rows.Count;
            if (i == 0)
            {
                throw new Exception("对不起，您没有权限操作这个商家的数据！");
            }
        }

        public DataTable GetProComment(string ProId, int top)
        {
            return ProVsComDal.GetList(top, "ProId='" + ProId + "' order by CreateTime desc ", "").Tables[0];
        }

        public void AddProComment(Model.ProVsCommentModel ProVsCommModel)
        {
            ProVsComDal.Add(ProVsCommModel);
        }

        /// <summary>
        /// 添加一条产品回复
        /// </summary>
        /// <param name="ProVsCommModel">回复与产品关联实体</param>
        /// <param name="CommModel">产品回复数据实体</param>
        /// <returns></returns>
        public bool AddProComment(Model.ProVsCommentModel ProVsCommModel, Model.CommentModel CommModel)
        {
            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                //DO YOUR METHODS here
                //写入方法
                //分布式事务的方法
                try
                {
                    CommentDal.Add(CommModel);

                    ProVsCommModel.CommentId = CommModel.CommentId;
                    ProVsComDal.Add(ProVsCommModel);

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 获得产品分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="current"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public DataSet GetProPageList2(string strWhere, int current, int pagesize)
        {
            DataSet ds = ProDal.GetPageList2(strWhere, current, pagesize);
            return ds;
        }

        /// <summary>
        /// 获得产品分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="current"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public DataSet GetProPageList(string strWhere, int current, int pagesize)
        {
            DataSet ds = ProDal.GetPageList(strWhere, current, pagesize);
            return ds;
        }

        public DataSet GetProPageList(string Order, string strWhere, int current, int pagesize, string col)
        {
            DataSet ds = ProDal.GetPageList(strWhere, Order, current, pagesize, col);
            return ds;
        }

        /// <summary>
        /// 获得分部产品的分页列表
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="strWhere"></param>
        /// <param name="current"></param>
        /// <param name="pagesize"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataSet GetProVsBranchPageList(string Order, string strWhere, int current, int pagesize, string col)
        {
            DAL.ProVsBranchDAL dal = new DAL.ProVsBranchDAL();
            DataSet ds = dal.GetPageList(strWhere, Order, current, pagesize, col);
            return ds;

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public DataSet GetProCommentPageList(string strWhere, int current)
        {
            DAL.CommentDAL dal = new DAL.CommentDAL();
            return dal.GetProCommentPageList(strWhere, current, 10);
        }

        public DataTable GetProCommentArray(DataTable ProDt)
        {
            return ProDal.GetNo1Comment(ProDt, 3);
        }

        /// <summary>
        /// 更改产品默认图片
        /// </summary>
        /// <param name="ProId"></param>
        /// <param name="ImgId"></param>
        /// <returns></returns>
        public bool ChangeProDefaultImg(string ProId, string ImgId)
        {
            return ProDal.UpdateProDefaultImg(ProId, ImgId);
        }

        /// <summary>
        /// 获得产品列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetProInfo(string strWhere)
        {
            return ProDal.GetList(strWhere).Tables[0];
        }

        public DataSet GetProInfoById(string ProId)
        {
            return ProDal.GetProInfoById(ProId);
        }

        #region 产品类别

        /// <summary>
        /// 获得网站内的产品类表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetProType(string strWhere)
        {
            return ProTypeDal.GetList(strWhere).Tables[0];
        }

        /// <summary>
        /// 获得该商家的自定义列表,CacheTime为0则不读取缓存
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataTable GetProClass(decimal MerId, int cacheTime)
        {
            DataSet ds;
            StringBuilder s = new StringBuilder();

            s.Append("  SELECT ProductClassId,ParentProductClassId,ProductClassName,OrderNo,ImgId,ImgUrl FROM dbo.ProClassView WHERE MerchantId='" + MerId + "' AND Invalid=0 order by OrderNo desc  ");
            if (cacheTime == 0)
            {
                ds = DAL.DalComm.BackData(s.ToString());

            }
            else
            {
                ds = StaticBLL.CacheData(s.ToString(), "ProClass", cacheTime);
            }
            return ds.Tables[0];
        }




        /// <summary>
        /// 获得一条产品了别
        /// </summary>
        /// <param name="ProductClassId"></param>
        /// <returns></returns>
        public DataSet GetProClassInfo(decimal ProductClassId)
        {
            return ProClassDal.GetProClassInfo(ProductClassId);
        }

        public string ProTypeSelectHtml(string strWhere)
        {
            DataTable dt0 = GetProType(strWhere);
            DataRow[] drs0 = dt0.Select(" ParentProductTypeId=0 ");
            StringBuilder w = new StringBuilder();
            if (drs0.Length == 0)
            {
                return "<option value='-1'>没有'" + strWhere + "'可以查到的类别, 请联系站长<option>";
            }

            foreach (DataRow dr0 in drs0)
            {
                DataRow[] drs1 = dt0.Select(" ParentProductTypeId=" + dr0["ProductTypeId"] + " ");
                if (drs1.Length > 0)
                { //有二级类别
                    w.Append("<optgroup label=" + dr0["ProductTypeName"] + " >");

                    foreach (DataRow dr1 in drs1)
                    { //绑定二级类别
                        w.Append("<option value='" + dr1["ProductTypeId"] + "'>");
                        w.Append(dr1["ProductTypeName"]);
                        w.Append("</option>");
                    }
                    w.Append("</optgroup>");
                }
                else
                {//没有二级类别 ,直接绑定一级类别
                    w.Append("<option value='" + dr0["ProductTypeId"] + "'>");
                    w.Append(dr0["ProductTypeName"]);
                    w.Append("</option>");
                }
            }
            return w.ToString();
        }

        public string ProClassSelectHtml(decimal MerId)
        {
            DataTable dt0 = GetProClass(MerId, 300);

            DataRow[] drs0 = dt0.Select(" ParentProductClassId=0 ");
            StringBuilder w = new StringBuilder();
            if (drs0.Length == 0)
            {
                return "<option value='-1'>请先维护类别<option>";
            }

            foreach (DataRow dr0 in drs0)
            {
                DataRow[] drs1 = dt0.Select(" ParentProductClassId=" + dr0["ProductClassId"] + " ");
                if (drs1.Length > 0)
                { //有二级类别
                    w.Append("<optgroup label=" + dr0["ProductClassName"] + " >");

                    foreach (DataRow dr1 in drs1)
                    { //绑定二级类别
                        w.Append("<option value='" + dr1["ProductClassId"] + "'>");
                        w.Append(dr1["ProductClassName"]);
                        w.Append("</option>");
                    }

                    w.Append("");
                    w.Append("</optgroup>");
                }
                else
                {//没有二级类别 ,直接绑定一级类别
                    w.Append("<option value='" + dr0["ProductClassId"] + "'>");
                    w.Append(dr0["ProductClassName"]);
                    w.Append("</option>");
                }
            }
            return w.ToString();
        }

        #endregion 产品类别

        /// <summary>
        /// 获得产品下的图片集合
        /// </summary>
        /// <param name="ProId">传入产品ID</param>
        /// <returns></returns>
        public DataTable GetProVsImg(string ProId)
        {
            return ProVsImgDal.GetList(" ProId='" + ProId + "' ").Tables[0];
        }

        public void DeleteProVsImg(string ProId)
        {
            ProVsImgDal.DeleteList(" ProId='" + ProId + "' ");
        }

        public void SaveProVsImg(Model.ProVsImgModel model)
        {
            ProVsImgDal.Add(model);
        }

        /// <summary>
        /// 保存产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SavePro(Model.ProductModel model)
        {



            if (model.InheritProTeXing)
            { //如果继承产品特性
                model.ProTeXing = DAL.DalComm.ExInt("SELECT  TOP 1  ProTeXing FROM dbo.ProductClass WHERE ProductClassId='" + model.ProClassId + "'");
            }

            if (model.InheritJiFenNum)
            { //如果继承积分比率
                model.GetJiFenNum = DAL.DalComm.ExDecimal("SELECT  TOP 1  GetJiFenNum FROM dbo.ProductClass WHERE ProductClassId='" + model.ProClassId + "'");
            }

            bool ProCodeRepeat = bool.Parse(BLL.StaticBLL.MerOneConfig(model.MerchantId, "RepeatProCode", "false"));
            int i = 0;
            if (!ProCodeRepeat)
            {//如果不允许条码重复
                if (model.ProCode.Trim() == "")
                {
                    //如果条码为空则不予判断
                }
                else
                {
                    i = ProDal.ExInt("ProCode='" + model.ProCode + "'");
                }
            }

            if (model.ProNumCode.Trim() == "")
            {
                model.ProNumCode = model.ProCode;
            }

            if (model.ProductImgId.Trim() == "" || model.ProductImgId.Trim() == "0")
            {
                model.ProductImgId = "noPic1";
            }

            if (model.ProId == "" || model.ProId == null)
            {
                //新增
                if (i > 0)
                {
                    throw new Exception("" + model.ProCode + "出现重复条码!");
                }
                model.ProId = Common.TimeString.GetNow_ff();  //时间戳字符串
                return ProDal.Add(model);
            }
            else
            {
                //修改产品

                if (i > 1)
                {
                    throw new Exception("" + model.ProCode + "不允许出现重复条码!");
                }
                return ProDal.Update(model);
            }
        }

        #endregion 产品

        #region 商家


        //分部上货
        public void UpProToBranch(string ProId, string BranchId)
        {

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT COUNT(0) FROM dbo.ProVsBranch WITH(NOLOCK) WHERE ProId='" + ProId + "' AND BranchId=" + BranchId + " ");
            bool b = DAL.DalComm.ExBool(s.ToString());
            if (b)//如果已经存在, 就不管了
            {

            }
            else
            {
                //如果不存在,那就加进去
                Model.ProVsBranchModel model = new Model.ProVsBranchModel();
                s.Clear();
                s.Append("    SELECT * FROM  dbo.Product WITH(NOLOCK) WHERE ProId='" + ProId + "' ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有找到ProId为" + ProId + "的商品!");
                }
                DataRow dr = dt.Rows[0];
                model.AllowPriceInterface = bool.Parse(dr["AllowPriceInterface"].ToString());
                model.AllowProNumInterface = bool.Parse(dr["AllowProNumInterface"].ToString());
                model.BranchId = BranchId;
                model.Discount = decimal.Parse(dr["Discount"].ToString());
                model.GetJiFenNum = decimal.Parse(dr["GetJiFenNum"].ToString());
                model.InheritDiscount = bool.Parse(dr["InheritDiscount"].ToString());
                model.InheritJiFenNum = bool.Parse(dr["InheritJiFenNum"].ToString());
                model.InheritPeiSongType = bool.Parse(dr["InheritPeiSongType"].ToString());
                model.IsInfiniteNum = bool.Parse(dr["IsInfiniteNum"].ToString());
                model.MinQuantity = decimal.Parse(dr["MinQuantity"].ToString());
                model.MinZl = decimal.Parse(dr["MinZl"].ToString());
                model.OnLineLv = int.Parse(dr["OnLineLv"].ToString());
                model.ProId = dr["ProId"].ToString();
                model.ProNum = decimal.Parse(dr["ProNum"].ToString());
                model.RePrice = decimal.Parse(dr["RePrice"].ToString());
                model.RePrice2 = decimal.Parse(dr["RePrice2"].ToString());
                model.RePrice3 = decimal.Parse(dr["RePrice3"].ToString());
                model.Status = int.Parse(dr["Status"].ToString());
                model.Zl = decimal.Parse(dr["Zl"].ToString());


                DAL.ProVsBranchDAL dal = new DAL.ProVsBranchDAL();
                dal.Add(model);


            }
        }

        #region 分部


        /// <summary>
        /// 保存一个分部
        /// </summary>
        /// <param name="model"></param>
        public void SaveBranch(Model.BranchModel model)
        {
            DAL.BranchDAL dal = new DAL.BranchDAL();

            bool b = DAL.DalComm.ExBool(" select count(0) from  CORE.dbo.Branch where BranchId='" + model.BranchId + "' ");
            if (!b)
            {
                dal.Add(model);

            }
            else
            {

                dal.Update(model);
            }

        }
        #endregion


        #region 配置

        public void DelMerConfig(decimal MerId, string MerConfigId)
        {
            DAL.MerConfigDAL dal = new DAL.MerConfigDAL();

            dal.DeleteList(" MerId='" + MerId + "' and MerConfigId='" + MerConfigId + "'  ");
        }

        public void SaveMerConfig(Model.MerConfigModel model)
        {
            DAL.MerConfigDAL dal = new DAL.MerConfigDAL();

            bool b = DAL.DalComm.ExBool(" SELECT count(0) FROM CORE.dbo.MerConfig WHERE MerConfigId='" + model.MerConfigId + "' AND MerId='" + model.MerId + "' ");

            if (!b)
            {
                //不存在则添加
                dal.Add(model);
            }
            else
            {
                //存在则修改

                dal.Update(model);
            }
        }

        #endregion 配置

        /// <summary>
        /// 是否有权限改动这个商家
        /// </summary>
        /// <returns></returns>
        public bool hasPower(decimal MerchantId)
        {
            BLL.UserBLL ubll = new UserBLL();
            if (ubll.IsAdministrator())
            {
                return true;
            }
            else
            {
                string CurrentUser = Common.CookieSings.GetCurrentUserId();

                StringBuilder s = new StringBuilder();
                s.Append(" select count(0) from  dbo.MerchantVsUser where UserId='" + CurrentUser + "'  and MerchantId='" + MerchantId + "' and Power >= 0 ");
                int i = DAL.DalComm.ExInt(s.ToString());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void DeletemerchantVsImg(string StrWhere)
        {
            DAL.MerchantVsImgDAL dal = new DAL.MerchantVsImgDAL();
            dal.DeleteList(StrWhere);
        }

        /// <summary>
        /// 删除商家和行业的关联关系
        /// </summary>
        /// <param name="StrWhere"></param>
        public void DeleteMerchantVsMerchantType(string StrWhere)
        {
            DAL.MerchantVsMerchantTypeDAL dal = new DAL.MerchantVsMerchantTypeDAL();
            dal.DeleteList(StrWhere);
        }

        /// <summary>
        /// 保存商家和行业的关联关系
        /// </summary>
        /// <param name="model"></param>
        public void SaveMerchantVsMerchantType(Model.MerchantVsMerchantTypeModel model)
        {
            DAL.MerchantVsMerchantTypeDAL dal = new DAL.MerchantVsMerchantTypeDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 保存商家和图片的关系.
        /// </summary>
        /// <param name="model"></param>
        public void SaveMerchantVsImg(Model.MerchantVsImgModel model)
        {
            DAL.MerchantVsImgDAL dal = new DAL.MerchantVsImgDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 获得商家行业列表,为0是取得顶端行业
        /// </summary>
        public DataSet GetMerType(decimal MerTypeId)
        {
            return merTypeDal.GetList(" ParentMerchantTypeId='" + MerTypeId + "' ");
        }

        //作废一个商家新闻类别
        public void InvalidAtricleClass(int ArticleClassId)
        {
            DAL.ArticleClassDAL dal = new DAL.ArticleClassDAL();
            Model.ArticleClassModel model = dal.GetModel(ArticleClassId);
            model.Invalid = true;
            dal.Update(model);
        }

        public DataSet GetArticleClass(string strWhere)
        {
            DAL.ArticleClassDAL dal = new DAL.ArticleClassDAL();
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 保存新闻类别
        /// </summary>
        /// <param name="model"></param>
        public void SaveArticleClass(Model.ArticleClassModel model)
        {
            DAL.ArticleClassDAL dal = new DAL.ArticleClassDAL();
            if (model.ArticleClassId == 0)
            {
                //新增
                dal.Add(model);
            }
            else
            {  //修改
                dal.Update(model);
            }
        }

        public void SaveApplyForBindMer(Model.ApplyForBindMerModel model)
        {
            model.CreateTime = DateTime.Now;
            DAL.ApplyForBindMerDAL dal = new DAL.ApplyForBindMerDAL();
            dal.Add(model);
        }

        public DataSet GetMerVsUser(string strWhere)
        {
            return MerDal.GetUserMerList(strWhere);
        }

        //protected string MyMerOpHtml()
        //{
        //    DataTable dt = GetMyMer();

        //}

        public DataTable GetMyMer()
        {
            BLL.UserBLL bll = new UserBLL();
            string UserId = bll.CurrentUserId();
            DataTable dt = UserVsMerDal.GetList(" UserId='" + UserId + "' ").Tables[0];
            return dt;
        }

        /// <summary>
        /// 获得商家点评的分页列表
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        public DataSet GetMerVsComment(string StrWhere, int currentpage)
        {
            return MvcDal.GetPageList(StrWhere, currentpage, 10);
        }

        /// <summary>
        /// 保存商家
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveMer(Model.MerchantModel model)
        {
            if (model.Logo.Trim() == "")
            {
                model.Logo = "noPic1";
            }

            bool b = DAL.DalComm.ExBool(" select count(0) from dbo.Merchant WITH(NOLOCK) where MerchantId='" + model.MerchantId + "' ");

            if (!b)
            {
                //不存在这个MerchantId编号
                model.CreateTime = DateTime.Now;
                return MerDal.Add(model);
            }
            else
            {
                //已经存在
                model.CreateTime = DateTime.Now;
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                // bll.CheckChangeMerPower(model.MerchantId);   //检测权限
                return MerDal.Update(model);
            }
        }

        /// <summary>
        /// 根据商家主键取得商家的数据
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataSet GetMerInfo(decimal MerId)
        {
            return MerDal.GetMerInfoById(MerId);
        }

        /// <summary>
        /// 获得商家的快速数据
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataSet GetMerInfoFaseById(decimal MerId)
        {
            return MerDal.GetMerInfoFaseById(MerId);
        }

        public DataTable GetMerList(string strWhere)
        {
            return MerDal.GetList(strWhere).Tables[0];
        }

        /// <summary>
        /// 获得商家的分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        public DataSet GetMerPageList(string strWhere, int CurrentPage)
        {
            return MerDal.GetPageList(strWhere, CurrentPage, 10);
        }

        /// <summary>
        /// 获得商家类别
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetMerTypeList(string strWhere)
        {
            return merTypeDal.GetList(strWhere).Tables[0];
        }

        public DataSet GetALLMerType(string strWhere)
        {
            return merTypeDal.GetList(strWhere);
        }

        public string MerTypeOptionHtml(string strWhere)
        {
            DataTable dt = GetMerTypeList(strWhere);

            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["MerchantTypeId"] + "'>");
                w.Append(dr["MerchantTypeName"]);
                w.Append("</option>");
            }
            return w.ToString();
        }

        #endregion 商家

        #region 品牌

        public void ClearPinPai(string ProId)
        {
            DAL.DalComm.ExStr(" UPDATE  dbo.Product SET PinPaiId=0 WHERE ProId='" + ProId + "' ");
        }

        public void InvalidPinPai(decimal PinPaiId, bool Invalid)
        {
            DAL.DalComm.ExStr(" UPDATE  dbo.PinPaiInfo SET   Invalid='" + Invalid + "' WHERE PinPaiId='" + PinPaiId + "' ");
        }

        /// <summary>
        /// 获得品牌列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataSet GetPinPaiPageList(string strWhere, int c, int p, string col)
        {
            DAL.PinPaiInfoDAL dal = new DAL.PinPaiInfoDAL();
            return dal.GetPageList(strWhere, c, p, col);
        }

        /// <summary>
        /// 删除品牌和图片的关联关系
        /// </summary>
        /// <param name="StrWhere"></param>
        public void DeletePinPaiVsImg(string StrWhere)
        {
            DAL.PinPaiVsImgDAL dal = new DAL.PinPaiVsImgDAL();
            dal.DeleteList(StrWhere);
        }

        /// <summary>
        /// 添加品牌和图片的关联关系
        /// </summary>
        /// <param name="model"></param>
        public void SavePinPaiVsImg(Model.PinPaiVsImgModel model)
        {
            DAL.PinPaiVsImgDAL dal = new DAL.PinPaiVsImgDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 保存一个品牌
        /// </summary>
        /// <param name="model"></param>
        public void SavePinPaiInfo(Model.PinPaiInfoModel model)
        {
            DAL.PinPaiInfoDAL dal = new DAL.PinPaiInfoDAL();
            if (model.PinPaiId == 0)
            {
                model.InputCode = Common.PinYin.GetFirstLetter(model.PinPaiName);
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        /// <summary>
        /// 获得一个品牌
        /// </summary>
        /// <param name="PinPaiId"></param>
        /// <returns></returns>
        public DataSet GetPinPaiInfo(decimal PinPaiId)
        {
            DAL.PinPaiInfoDAL dal = new DAL.PinPaiInfoDAL();
            DataSet ds = dal.GetPinPaiInfo(PinPaiId);
            return ds;
        }

        #endregion 品牌

        #region 积分

        /// <summary>
        /// 获得积分变更分页列表
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataSet GetJiFenChangeList(string StrWhere, int CurrentPage, int PageSize, string col)
        {
            DAL.JiFenChangeDAL dal = new DAL.JiFenChangeDAL();
            return dal.GetPageList(StrWhere, CurrentPage, PageSize, col);
        }

        #endregion 积分

        #region 权限

        /// <summary>
        /// 获得一个用户的所有商家角色
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataSet GetUserMerRoleList(string uid)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" select * from dbo.UserMerRoleView where UserId='" + uid + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }

        /// <summary>
        /// 获得一个商家的所有角色列表
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataSet GetMerRoleList(decimal MerId)
        {
            DAL.MerRoleDAL dal = new DAL.MerRoleDAL();
            return dal.GetList(" MerId='" + MerId + "' ");
        }

        public DataSet GetMerRoleUsersPageList(string strWhere, int currentpage, int pagesize)
        {
            DAL.MerRoleVsUserDAL dal = new DAL.MerRoleVsUserDAL();

            return dal.GetPageListGroupByUserId(strWhere, currentpage, pagesize);
        }

        public void SaveMerRole(Model.MerRoleModel model)
        {
            DAL.MerRoleDAL dal = new DAL.MerRoleDAL();
            if (model.MerRoleId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        public void SaveMerRoleVsUser(Model.MerRoleVsUserModel model)
        {
            DAL.MerRoleVsUserDAL dal = new DAL.MerRoleVsUserDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 返回当前登录权限
        /// </summary>
        /// <returns></returns>
        public static Model.CurrentMerModel CurrentModel()
        {
            Model.CurrentMerModel model = new Model.CurrentMerModel();
            try
            {
                model.CurrentUserId = HttpUtility.UrlDecode(Common.CookieSings.ReCooke("CurrentUserId"));
                if (model.CurrentUserId == "" || model.CurrentUserId == null)
                {
                    return model;
                }
                model.CurrentMerRoleName = HttpUtility.UrlDecode(Common.CookieSings.ReCooke("CurrentMerRoleName"));
                model.CurrentMerName = HttpUtility.UrlDecode(Common.CookieSings.ReCooke("CurrentMerName"));
                model.CurrentMerId = decimal.Parse(Common.CookieSings.ReCooke("CurrentMerId"));
                model.CurrentMerRoleId = decimal.Parse(Common.CookieSings.ReCooke("CurrentMerRoleId"));
                return model;
            }
            catch (Exception)
            {
                model.CurrentUserId = "";
                model.CurrentMerRoleName = "";
                model.CurrentMerName = "";
                model.CurrentMerId = 0;
                model.CurrentMerRoleId = 0;
                return model;
            }



        }

        #endregion 权限

        #region 配送

        /// <summary>
        /// 配送方式和产品类别的关联
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        public DataSet GetPeiSongTypeMer(decimal MerId)
        {
            DAL.PeiSongTypeDAL dal = new DAL.PeiSongTypeDAL();
            return dal.GetList(" MerId=" + MerId + " order by OrderNo desc ");
        }

        /// <summary>
        /// 获得一个配送方式的详细情况
        /// </summary>
        /// <param name="PeiSongTypeId"></param>
        /// <returns></returns>
        public DataSet GetPeiSongTypeInfo(decimal PeiSongTypeId)
        {
            StringBuilder s = new StringBuilder();

            s.Append(" SELECT * FROM  dbo.PeiSongType WHERE PeiSongTypeId ='" + PeiSongTypeId + "'  ");
            s.Append(" SELECT * FROM dbo.PeiSongTimeSolt WHERE PeiSongTypeId='" + PeiSongTypeId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }

        public void SavePeiSongType(Model.PeiSongTypeModel model)
        {
            DAL.PeiSongTypeDAL dal = new DAL.PeiSongTypeDAL();
            if (model.PeiSongTypeId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        public void SavePeiSongTimeSolt(Model.PeiSongTimeSoltModel model)
        {
            DAL.PeiSongTimeSoltDAL dal = new DAL.PeiSongTimeSoltDAL();
            if (model.PeiSongTimeSoltId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        public void InheritPeiSongTypeByPro(string ProId, bool InheritPeiSongType, DataTable dtPeiSongTypeList)
        {
            if (InheritPeiSongType)
            { //继承
                dtPeiSongTypeList = DAL.DalComm.BackData("SELECT * FROM dbo.PeiSongTypeVsProductClass with(nolock) WHERE ProductClassId=(SELECT TOP 1 ProClassId FROM dbo.Product WHERE ProId='" + ProId + "') ").Tables[0];
            }
            else
            { //不继承
            }

            DAL.PeiSongTypeVsProductDAL PvPDal = new DAL.PeiSongTypeVsProductDAL();
            PvPDal.DeleteList(" ProId='" + ProId + "' ");
            if (dtPeiSongTypeList != null)
            {
                if (dtPeiSongTypeList.Rows.Count > 0)
                {
                    foreach (DataRow drPeiSongType in dtPeiSongTypeList.Rows)
                    {
                        Model.PeiSongTypeVsProductModel PvpMod = new Model.PeiSongTypeVsProductModel();
                        PvpMod.PeiSongTypeId = decimal.Parse(drPeiSongType["PeiSongTypeId"].ToString());
                        PvpMod.ProId = ProId;
                        PvpMod.VsType = "";
                        PvPDal.Add(PvpMod);
                    }
                }
            }
        }

        /// <summary>
        /// 产品类别的配送继承
        /// </summary>
        /// <param name="ProductClassId"></param>
        /// <param name="InheritPeiSongType"></param>
        /// <param name="dtPeiSongTypeList"></param>
        public void InheritPeiSongTypeByProClass(decimal ProductClassId, bool InheritPeiSongType, DataTable dtPeiSongTypeList)
        {
            StringBuilder s = new StringBuilder();
            s.Append("   UPDATE dbo.ProductClass SET InheritPeiSongType='" + InheritPeiSongType + "' WHERE ProductClassId='" + ProductClassId + "'");
            DAL.DalComm.ExReInt(s.ToString());
            DAL.ProductClassDAL ProClassDal = new DAL.ProductClassDAL();
            Model.ProductClassModel ProClassModel = ProClassDal.GetModel(ProductClassId);

            DAL.PeiSongTypeVsProductClassDAL dal = new DAL.PeiSongTypeVsProductClassDAL();
            dal.DeleteList(" ProductClassId=" + ProductClassId + " ");
            if (InheritPeiSongType)
            {
                //继承
                if (ProClassModel.ParentProductClassId == 0)
                {
                    throw new Exception("这是顶级类别,不能继承配送方式!");
                }
                else
                {
                    //把配送方式复制为上级类别的配送方式!
                    dtPeiSongTypeList = DAL.DalComm.BackData(" select * from dbo.PeiSongTypeVsProductClass where  ProductClassId='" + ProClassModel.ParentProductClassId + "' ").Tables[0];
                }
            }
            else
            {
                //不继承
            }

            //不继承,那就将配送方式复制给当前类别, 然后递归传递给当前类别下属的所有类别和所有产品
            if (dtPeiSongTypeList != null)
            {
                if (dtPeiSongTypeList.Rows.Count > 0)
                {
                    foreach (DataRow drPeiSongType in dtPeiSongTypeList.Rows)
                    {
                        Model.PeiSongTypeVsProductClassModel PvPmodel = new Model.PeiSongTypeVsProductClassModel();
                        PvPmodel.PeiSongTypeId = decimal.Parse(drPeiSongType["PeiSongTypeId"].ToString());
                        PvPmodel.ProductClassId = ProductClassId;
                        PvPmodel.VsType = "";
                        dal.Add(PvPmodel);
                    }
                }
            }

            //看看下面还有没有子类别

            s.Clear();
            s.Append(" SELECT * FROM dbo.ProductClass with(nolock) WHERE ParentProductClassId='" + ProductClassId + "'  ");
            s.Append(" SELECT * FROM  dbo.Product with(nolock) WHERE ProClassId='" + ProductClassId + "' and InheritPeiSongType=1 ");  //所有继承的产品
            DataSet ds_ProClassAndPro = DAL.DalComm.BackData(s.ToString());
            DataTable dtCld = ds_ProClassAndPro.Tables[0]; ;

            s.Clear();
            if (dtCld.Rows.Count > 0)
            {
                foreach (DataRow drCld in dtCld.Rows)
                {
                    bool cld_InheritPeiSongType = bool.Parse(drCld["InheritPeiSongType"].ToString());
                    decimal cld_ProductClassId = decimal.Parse(drCld["ProductClassId"].ToString());
                    if (cld_InheritPeiSongType == false)
                    {
                        continue;//如果有子类别, 他设置为不继承, 则中断
                    }
                    else
                    {
                        InheritPeiSongTypeByProClass(cld_ProductClassId, cld_InheritPeiSongType, dtPeiSongTypeList);
                    }
                }
            }

            //看看类别下有没有产品

            DataTable dtCldPro = ds_ProClassAndPro.Tables[1];

            if (dtCldPro.Rows.Count > 0)
            {
                DAL.PeiSongTypeVsProductDAL PvProDal = new DAL.PeiSongTypeVsProductDAL();
                foreach (DataRow drCldPro in dtCldPro.Rows)
                {
                    s.Append(" DELETE FROM dbo.PeiSongTypeVsProduct WHERE ProId='" + drCldPro["ProId"] + "' ");

                    if (dtPeiSongTypeList != null)
                    {
                        if (dtPeiSongTypeList.Rows.Count > 0)
                        {
                            foreach (DataRow drPeiSongTypeList in dtPeiSongTypeList.Rows)
                            {
                                Model.PeiSongTypeVsProductModel PvProMod = new Model.PeiSongTypeVsProductModel();
                                PvProMod.PeiSongTypeId = decimal.Parse(drPeiSongTypeList["PeiSongTypeId"].ToString());
                                PvProMod.ProId = drCldPro["ProId"].ToString();

                                s.Append(" INSERT INTO  dbo.PeiSongTypeVsProduct ");
                                s.Append(" ( PeiSongTypeId, ProId, VsType ) ");
                                s.Append(" VALUES  ( " + PvProMod.PeiSongTypeId + ",  ");
                                s.Append(" '" + PvProMod.ProId + "',  ");
                                s.Append(" '' ");
                                s.Append(" ) ");
                            }
                        }
                    }
                }
                DAL.DalComm.ExReInt(s.ToString());
            }
        }

        #endregion 配送

        #region 促销

        /// <summary>
        /// 保存促销
        /// </summary>
        /// <param name="model"></param>
        public void SaveCuXiao(Model.CuXiaoModel model)
        {
            DAL.CuXiaoDAL dal = new DAL.CuXiaoDAL();
            if (model.CuXiaoId == 0)
            {
                model.CreateTime = DateTime.Now;
                model.PyCode = Common.PinYin.GetFirstLetter(model.CuXiaoName);
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        /// <summary>
        /// 获得一个促销
        /// </summary>
        /// <param name="CuXiaoId"></param>
        /// <returns></returns>
        public DataSet GetCuXiaoInfo(decimal CuXiaoId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("          SELECT * FROM dbo.CuXiao WITH(NOLOCK) WHERE CuXiaoId=" + CuXiaoId + " SELECT ImgUrl,ImgId FROM CORE.dbo.AdView WITH(NOLOCK) WHERE AdMemo='" + CuXiaoId + "' AND  PageName='促销'  ");
            return DAL.DalComm.BackData(s.ToString());
        }

        public Model.CuXiaoModel GetCuXiaoModel(decimal CuXiaoId)
        {
            DAL.CuXiaoDAL dal = new DAL.CuXiaoDAL();

            return dal.GetModel(CuXiaoId);
        }

        /// <summary>
        /// 促销分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="Order"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataSet GetCuXiaoList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DAL.CuXiaoDAL dal = new DAL.CuXiaoDAL();
            return dal.GetPageList(strWhere, Order, currentpage, pagesize, col);
        }

        public DataSet GetCuXiaoVsProList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DAL.CuXiaoVsProDAL dal = new DAL.CuXiaoVsProDAL();

            DataSet ds = dal.GetPageList(strWhere, Order, currentpage, pagesize, col);
            DataTable dt = ds.Tables[0];

            try
            {
                dt.Columns.Add("起售价格");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal 起售价格 = 0;
                        decimal RePrice = decimal.Parse(dr["RePrice"].ToString());
                        switch (dr["ProTeXing"].ToString())
                        {
                            case "1":
                                //普通商品
                                decimal MinQuantity = decimal.Parse(dr["MinQuantity"].ToString());
                                起售价格 = RePrice * MinQuantity;
                                break;

                            case "2":
                                //生鲜商品

                                decimal Zl = decimal.Parse(dr["Zl"].ToString());
                                decimal MinZl = decimal.Parse(dr["MinZl"].ToString());
                                try
                                {
                                    起售价格 = RePrice * (Zl / MinZl);
                                }
                                catch
                                {
                                    起售价格 = 0;
                                }

                                break;
                        }
                        dr["起售价格"] = 起售价格;
                    }
                }
            }
            catch
            {
            }
            return ds;
        }

        public void SaveCuXiaoVsPro(Model.CuXiaoVsProModel model)
        {
            DAL.CuXiaoVsProDAL dal = new DAL.CuXiaoVsProDAL();
            if (dal.ExInt(" CuXiaoId=" + model.CuXiaoId + " and ProId='" + model.ProId + "' ") > 0)
            {
                //存在
                dal.Update(model);
            }
            else
            {
                //不存在
                dal.Add(model);
            }
        }

        #endregion 促销

        public void InvalidPingJia(decimal PingJiaId)
        {
            if (PingJiaId == 0)
            {
                throw new Exception("PingJiaId为0!");
            }

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion 事务开启

                StringBuilder s = new StringBuilder();
                s.Append(" SELECT JiFenChangeNum,MemberId,Invalid FROM dbo.PingJiaView WHERE PingJiaId='" + PingJiaId + "' ");
                s.Append(" UPDATE dbo.PingJiaInfo SET Invalid=1 WHERE PingJiaId='" + PingJiaId + "' ");
                DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有找到评价ID为" + PingJiaId + "的评价!");
                }
                DataRow dr = dt.Rows[0];

                bool Invalid = bool.Parse(dr["Invalid"].ToString());
                if (Invalid == true)
                {
                    throw new Exception("已经作废无需再次作废!");
                }

                Model.JiFenChangeModel ModChange = new Model.JiFenChangeModel();
                ModChange.JiFenChangeNum = decimal.Parse(dr["JiFenChangeNum"].ToString()) * -1;  //用负数冲销
                if (ModChange.JiFenChangeNum != 0)
                {  //如果为0那就算了吧
                    ModChange.CreateTime = DateTime.Now;
                    ModChange.ReKey = PingJiaId.ToString();
                    ModChange.JifenChangeTypeId = 10;
                    ModChange.MemberId = decimal.Parse(dr["MemberId"].ToString());
                    ModChange.JiFenChangeMemo = "评价作废冲销积分。";
                    ChangeMemberJiFen(ModChange);
                }
                BLL.StaticBLL.MemberNum(decimal.Parse(dr["MemberId"].ToString()));

                #region 事务关闭

                transactionScope.Complete();
            }

            #endregion 事务关闭
        }



        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="MemberId">用户ID,必须填写</param>
        /// <param name="ProId">产品ID,清空购物车内哪个产品, 如果留空则全部清空!</param>
        public void ClearGwc(decimal MemberId, string ProId)
        {
            DAL.GwcDAL dal = new DAL.GwcDAL();
            StringBuilder s = new StringBuilder();
            s.Append(" MemberId=" + MemberId + " ");
            if (ProId.Trim() != "")
            {
                s.Append(" and ProId='" + ProId + "' ");
            }
            dal.DeleteList(s.ToString());
        }

        public void ClearGwc(decimal MemberId, List<string> ProIds)
        {
            DAL.GwcDAL dal = new DAL.GwcDAL();
            StringBuilder s = new StringBuilder();

            foreach (string ProId in ProIds)
            {
                s.Append(" delete from dbo.Gwc where memberId='" + MemberId + "' and ProId='" + ProId + "'  ");
            }

            DAL.DalComm.ExReInt(s.ToString());
        }

        /// <summary>
        /// 改变购物车明细
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ChangeGwc(Model.GwcModel model)
        {

            DAL.GwcDAL dal = new DAL.GwcDAL();
            dal.Update(model);
            return GwcAmountNum(model.MemberId, model.BranchId);
        }

        public Model.GwcModel GetGwcModel(decimal GwcId)
        {
            DAL.GwcDAL dal = new DAL.GwcDAL();
            return dal.GetModel(GwcId);
        }

        public Dictionary<string, string> AddGwc(Model.GwcModel model, string ZoneId)
        {
            DAL.GwcDAL dal = new DAL.GwcDAL();

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.GwcView with(nolock) WHERE MemberId='" + model.MemberId + "' AND ProId='" + model.ProId + "' and BranchId='" + model.BranchId + "' and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "')   ");

            s.Append("         SELECT BranchId FROM dbo.GwcView WHERE MemberId=" + model.MemberId + "  and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "')   GROUP BY BranchId  ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            DataTable dtBranch = ds.Tables[1];

            if (dtBranch.Rows.Count > 1)
            {
                dal.DeleteList(" MemberId='" + model.MemberId + "'  ");
            }
            if (dtBranch.Rows.Count == 1)
            {
                string BranchId = dtBranch.Rows[0]["BranchId"].ToString();

                if (BranchId == "")
                {
                    dal.DeleteList(" MemberId='" + model.MemberId + "'  ");
                }
                else
                {

                    if (BranchId == model.BranchId)
                    {

                    }
                    else
                    {
                        throw new Exception("非常抱歉，您的购物车里已经加入了其他不同商家的商品，请先结算购物车，再添加此商品!");
                    }
                }

            }




            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                //购物车里有这个产品
                model.GwcId = decimal.Parse(dr["GwcId"].ToString());
                switch (model.ProTeXing)
                {
                    case 1:
                        //标准化商品
                        model.Quantity = model.Quantity + decimal.Parse(dr["Quantity"].ToString());
                        decimal CurrentQuantity = decimal.Parse(dr["ZgProNum"].ToString());
                        if (model.Quantity > CurrentQuantity)
                        {
                            model.Quantity = CurrentQuantity;
                        }

                        break;

                    case 2:
                        model.ZlBs = model.ZlBs + decimal.Parse(dr["ZlBs"].ToString());
                        //生鲜商品
                        break;
                }
                dal.Update(model);
            }
            else
            {
                //购物车里没有这个产品
                dal.Add(model);
            }
            return GwcAmountNum(model.MemberId, model.BranchId);
        }

        /// <summary>
        /// 取得购物车内的金额和数量
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GwcAmountNum(decimal MemberId, string BranchId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT  ISNULL(SUM(CASE WHEN ProTeXing = 1 THEN RePrice * Quantity  ");
            s.Append(" WHEN ProTeXing = 2 THEN RePrice * ZlBs  ");
            s.Append("  END), 0) AS 金额 ,count(0) as 数量 ");
            s.Append(" FROM    dbo.GwcView with(nolock) ");
            s.Append(" WHERE MemberId=" + MemberId + "  and BranchId='" + BranchId + "' ");

            Dictionary<string, string> d = new Dictionary<string, string>();
            Dictionary<string, string> sdr = DAL.DalComm.ExecSqlReDr(s.ToString());
            d.Add("金额", sdr["金额"].ToString());
            d.Add("数量", sdr["数量"].ToString());

            return d;
        }

        public int CheckPayment(string DingDanId)
        {
            int Num = 0;
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                string PayTypeName = "";

                BeeCloud.BeeCloud.registerApp("e332c1f2-dd0b-4591-a223-1411464c511e", "6a1d7014-51b6-4c1b-8079-a41e7fd9104e", "d24a1fc5-4d84-4f66-86da-4f0fd2f18985", "d24a1fc5-4d84-4f66-86da-4f0fd2f18985");
                BeeCloud.BeeCloud.setTestMode(false);
                List<BCBill> bills = new List<BCBill>();
                BCQueryBillParameter para = new BCQueryBillParameter();



                if (DingDanId == "")
                {
                    throw new Exception("DingDanId不能为空!");
                }


                StringBuilder s = new StringBuilder();
                s.Append(" SELECT CreateMember,Status,PayAmount , MerchantId,BranchId FROM dbo.DingDanView  WHERE DingDanId='" + DingDanId + "' ");



                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dtDingDan = ds.Tables[0];




                if (dtDingDan.Rows.Count == 0)
                {
                    throw new Exception("没有编号为" + DingDanId + "的订单!");
                }
                DataRow drDingDan = dtDingDan.Rows[0];
                string BranchId = drDingDan["BranchId"].ToString();
                decimal MerId = decimal.Parse(drDingDan["MerchantId"].ToString());
                int Status = int.Parse(drDingDan["Status"].ToString());
                decimal PayAmount = decimal.Parse(drDingDan["PayAmount"].ToString());

                BLL.MerchantBLL MerBll = new BLL.MerchantBLL();
                para.billNo = DingDanId;
                para.result = true;
                bills = BCPay.BCPayQueryByCondition(para);
                if (Status <= 0)
                {//如果是新下达的订单支付




                    decimal MemberId = decimal.Parse(drDingDan["CreateMember"].ToString());



                    s.Clear();
                    s.Append(" UPDATE dbo.DingDanInfo SET Status=10,Pay=100 where DingDanId='" + DingDanId + "'   ");
                    DAL.DalComm.ExReInt(s.ToString()); //改变订单状态




                    decimal billsPayAmount = 0;
                    if (bills.Count > 0)
                    {  //说明这个订单已经有支付记录!



                        DAL.PaymentDAL dal = new DAL.PaymentDAL();

                        foreach (BCBill bill in bills)
                        {
                            Model.PaymentModel model = new PaymentModel();
                            model.CreateTime = DateTime.Now;
                            model.MemberId = MemberId;
                            model.PayAmount = decimal.Parse(bill.totalFee.ToString()) / 100;
                            billsPayAmount = model.PayAmount + billsPayAmount;


                            model.PaymentTitle = bill.title;
                            model.ReKey = bill.billNo;
                            model.PaymentType = "DingDan";
                            model.PaymentMemo = "";
                            switch (bill.channel)
                            {
                                case "WX_NATIVE": //微信扫码支付
                                case "WX_APP":

                                    model.PayTypeId = 20;
                                    PayTypeName = "微信支付";

                                    break;
                                case "ALI_WEB": //支付宝
                                case "ALI_APP": //支付宝
                                case "ALI_QRCODE": //支付宝扫码支付
                                    model.PayTypeId = 30;
                                    PayTypeName = "支付宝支付";
                                    break;
                                default:

                                    throw new Exception("暂时没有开放" + bill.channel + "支付方式!");

                            }

                            dal.Add(model);
                            DingDanLogModel logModel = new DingDanLogModel();
                            logModel.CreateTime = model.CreateTime;
                            logModel.DingDanId = DingDanId;
                            logModel.Memo = "已在线支付(" + PayTypeName + ")";
                            logModel.DingDanLogTypeId = 10;
                            logModel.DingDanClassId = 10;
                            MerBll.SaveDingDanLog(logModel);

                            DAL.DalComm.ExReInt(" UPDATE dbo.DingDanInfo SET PayTypeId=" + model.PayTypeId + " where DingDanId='" + DingDanId + "'  ");


                        }

                        if (billsPayAmount < PayAmount)
                        {

                            throw new Exception("实际支付金额小于订单支付金额.");
                        }
                        else
                        {
                            //   ReDict2.Add("billsPayAmount", billsPayAmount.ToString());
                        }

                    }
                    else
                    {

                        throw new Exception("没有查到相关支付记录!");
                    }
                    s.Clear();
                    //s.Append(" SELECT CreateMember,Status,PayAmount,PayTypeName,PayTypeId ContactName,Tel FROM dbo.DingDanView WITH(NOLOCK) WHERE DingDanId='" + DingDanId + "' ");



                    //ds = DAL.DalComm.BackData(s.ToString());

                    //dtDingDan = ds.Tables[0];
                    //DataRow drDingDan2 = dtDingDan.Rows[0];
                    //ReDict.Add("DingDanInfo", JsonHelper.ToJsonNo1(dtDingDan));

                    MerBll.NewDingDanTiXing(DingDanId); //订单提醒发送


                    Num = bills.Count;

                }
                else
                {
                    Num = 1;

                }

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion


            return Num;


        }


    }
}