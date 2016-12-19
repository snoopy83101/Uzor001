using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Common;
using Model;
using System.Transactions;
using io.rong;
using BLL;

namespace BPage
{
    public class Member : Common.BPageSetting2
    {


        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string para = ReStr("para");

                switch (para)
                {


                    #region 用户档期

                    case "ReSetMaxOrderPlanningTime":
                        ReSetMaxOrderPlanningTime();
                        break;

                    #endregion

                    #region 工种
                    case "SaveSkill":
                        SaveSkill();
                        break;

                    case "InvalidSkill":
                        InvalidSkill();
                        break;
                    case "SaveOneSkill":
                        SaveOneSkill();
                        break;

                    case "GetSkillList":
                        GetSkillList();
                        break;

                    #endregion

                    #region 团队管理

                    case "RemoveMemberTeam":
                        RemoveMemberTeam(); //将人员从团队中剥离
                        break;


                    case "AddMyTeam":
                        AddMyTeam();
                        break;

                    case "AddTeamMember":
                        AddTeamMember();
                        break;
                    case "GetMyTeamMemberList":
                        GetMyTeamMemberList();
                        break;
                    #endregion



                    #region 用户余额
                    #region 银行卡操作

                    case "RefuseSubjectCash":
                        RefuseSubjectCash();   //拒绝提现申请
                        break;

                    case "AllowSubjectCash":
                        AllowSubjectCash();   //通过提现申请
                        break;


                    case "GetSubjectCashList":
                        GetSubjectCashList();
                        break;
                    case "SaveSubjectCash":
                        SaveSubjectCash();
                        break;
                    case "SaveMemberBankCard":
                        SaveMemberBankCard();
                        break;



                    case "GetMemberBankCardList":
                        GetMemberBankCardList();
                        break;

                    case "GetMemberBankCardInfo":
                        GetMemberBankCardInfo();
                        break;
                    #endregion



                    case "GetMemberDetailPageSetting":
                        GetMemberDetailPageSetting();
                        break;

                    case "GetMemberAmountDetail":
                        GetMemberAmountDetail();
                        break;
                    #endregion

                    #region 用户技能级别相关

                    case "SaveProcessLvSubject":
                        SaveProcessLvSubject();
                        break;


                    case "ProcessLvStatusChange":
                        ProcessLvStatusChange();
                        break;

                    #endregion

                    #region 推广员相关

                    case "SetExtMemberLv":

                        SetExtMemberLv();
                        break;
                    case "SetExtMemberId":
                        SetExtMemberId();
                        break;
                    case "GetExtMemberDict":
                        GetExtMemberDict();
                        break;



                    #endregion
                        
                    #region 消费统计

                    case "GetQianDaoCountByTimeSolt":
                        GetQianDaoCountByTimeSolt();
                        break;

                    case "GetJiFenSumByTimSolt":
                        GetJiFenSumByTimSolt();
                        break;

                    case "GetAmountSumByTimeSolt":
                        GetAmountSumByTimeSolt();
                        break;
                    case "GetDingDanCountByTimeSolt":
                        GetDingDanCountByTimeSolt();
                        break;

                    #endregion

                    #region 客服与IM

                    case "GetDevicePush":
                        GetDevicePush();
                        break;
                    case "DelDevicePush":
                        DelDevicePush();
                        break;
                    case "SaveDevicePush":
                        SaveDevicePush();
                        break;


                    case "SyncGroup":  //同步用户用
                        SyncGroup();
                        break;

                    case "GetWeiDuMsgNumAndRongUserId":
                        GetWeiDuMsgNumAndRongUserId();
                        break;
                    case "GetZiXunList":
                        GetZiXunList();
                        break;

                    case "SaveZiXunLogInfo":
                        SaveZiXunLogInfo();   //保存一条咨询日志
                        break;
                    case "EndJieDai":
                        EndJieDai();   //结束单个会话
                        break;

                    case "GetWeiDuMsgNum":
                        GetWeiDuMsgNum();  //获得未读消息数
                        break;

                    case "GetWeiDuMsgNum2":
                        GetWeiDuMsgNum2();  //获得未读消息数(简单推荐)
                        break;
                    case "ReadRongMsg":
                        ReadRongMsg();  //更改消息的已读未读状态
                        break;

                    case "GetMsgLogList":
                        GetMsgLogList();
                        break;

                    case "GetKfJieDaiList":
                        GetKfJieDaiList();  //获得客服接待列表
                        break;

                    case "UserOnLineStatusChange":
                        UserOnLineStatusChange();
                        break;

                    case "GetUserOnline":
                        GetUserOnline();
                        break;

                    case "KfPageReady":
                        KfPageReady();//准备客服页面
                        break;


                    case "GetKfJieDai":
                        GetKfJieDai();   //请求客服接待
                        break;

                    case "GetRongToken":
                        GetRongToken();
                        break;

                    case "SendRongMsg":
                        SendRongMsg();
                        break;

                    #endregion

                    #region App推送绑定
                    case "DeviceBind":
                        DeviceBind();
                        break;

                    case "SendPush":
                        SendPush();
                        break;

                    #endregion


                    case "RemoveMemberVsFabric":
                        RemoveMemberVsFabric();
                        break;
                    case "SaveMemberVsFabric":
                        SaveMemberVsFabric();
                        break;


                    case "RemoveMemberVsPlate":
                        RemoveMemberVsPlate();
                        break;

                    case "SaveMemberVsPlate":
                        SaveMemberVsPlate();
                        break;

                    case "MemberVsPlateVsFabric":
                        MemberVsPlateVsFabric();
                        break;

                    case "ChangeMemberStatus":
                        ChangeMemberStatus();
                        break;

                    case "InvalidMember":
                        InvalidMember();
                        break;

                    case "GetTownList":
                        GetTownList();
                        break;

                    case "TopMember":
                        TopMember();
                        break;
                    case "ChangeGwc":
                        ChangeGwc();
                        break;
                    case "DelGwc":
                        DelGwc();
                        break;
                    case "AddGwc":
                        AddGwc();
                        break;
                    case "GwcAmountNum":
                        GwcAmountNum();
                        break;

                    case "ClearGwc":
                        ClearGwc();
                        break;
                    case "GetGwc":
                        GetGwc();
                        break;

                    case "TodayQianDao":
                        TodayQianDao();
                        break;

                    case "SendQianDao":
                        SendQianDao();
                        break;

                    case "DuanXinYanZheng":
                        DuanXinYanZheng();
                        break;


                    case "PhoneChangePwd":  //根据手机修改密码!
                        PhoneChangePwd();
                        break;


                    case "ChangeSex":
                        ChangeSex();
                        break;
                    case "ChangeNickName":
                        ChangeNickName(); //更改昵称
                        break;
                    case "ChangePhone":
                        ChangePhone();
                        break;
                    case "ChangeBirthday":
                        ChangeBirthday();
                        break;

                    case "ChangeMemberPic":
                        ChangeMemberPic();
                        break;
                    case "InvalidAddress":
                        InvalidAddress();
                        break;
                    case "ToDefault":
                        ToDefault();  //更改默认地址
                        break;

                    case "GetAddressInfo":
                        GetAddressInfo();
                        break;
                    case "GetAddressList":
                        GetAddressList();
                        break;
                    case "SaveAddress":
                        SaveAddress();
                        break;

                    case "CkPwd":
                        CkPwd();   //检查密码合法性
                        break;

                    case "NotHasPhone":
                        NotHasPhone();
                        break;
                    case "HasPhoneLoginStr":
                        HasPhoneLoginStr(); //手机登录名是否存在 手机号, 昵称
                        break;

                    case "PhoneZhuCe":
                        PhoneZhuCe();
                        break;
                    case "YzmLogin":
                        YzmLogin();
                        break;
                    case "Login1":
                        Login1();  //普通登录, 手机号+昵称
                        break;


                    case "SaveChouJiangInfo":
                        SaveChouJiangInfo();   //保存一个抽奖
                        break;
                    case "SearchChouJiangList":
                        SearchChouJiangList();  //查询抽奖列表
                        break;
                    case "SaveMyMember":
                        SaveMyMember();  //用户保存自己的会员信息时调用
                        break;
                    case "AcceptProcessLv":
                        AcceptProcessLv();  //通过技能认证
                        break;
                    case "SaveMember":
                        SaveMember();    //管理员保存用户
                        break;
                    case "SearchMemberList":
                        SearchMemberList(); //商家查询会员列表时调用
                        break;
                    case "GetMyMemberInfo":
                        GetMyMemberInfo();
                        break;
                    case "GetMemberInfo":
                        GetMemberInfo();
                        break;

                    case "YiChang":
                        YiChang();  //标记异常用户
                        break;


                    case "GetMemberList":
                        GetMemberList();
                        break;
                }
            }
            catch (Exception ex)
            {
                BLL.StaticBLL.ReThrow(ex);

            }
            context.Response.End();
        }

        private void ReSetMaxOrderPlanningTime()
        {
            decimal MemberId = ReDecimal("MemberId", 0);

            OrderBLL bll = new OrderBLL();
            DateTime dt = bll.SetMaxOrderPlanningTime(MemberId);
            ReDict2.Add("MaxOrderPlanningTime", dt.ToString());
            ReTrue();


        }

        private void RemoveMemberTeam()
        {
            decimal MemberId = ReDecimal("MemberId", 0);

            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }
            MemberBLL mbll = new MemberBLL();
            mbll.RemoveMemberTeam(MemberId);
            ReTrue();

        }

        private void SaveOneSkill()
        {








            Model.MemberVsSkillModel model = new MemberVsSkillModel();
            model.MemberId = ReDecimal("MemberId", 0);
            model.SkillId = ReInt("SkillId", 0);
            model.OrderId = ReInt("OrderId", 1);


            BLL.MemberBLL bll = new MemberBLL();
            bll.SaveOneSkill(model);


            ReTrue();



        }
        private void InvalidSkill()
        {
            int SkillId = ReInt("SkillId", 0);
            bool Invalid = ReBool("Invalid", false);
            if (SkillId == 0)
            {

                throw new Exception("SkillId不能为空!");
            }

            DAL.DalComm.ExReInt("  UPDATE dbo.Skill SET Invalid='" + Invalid + "' WHERE SkillId=" + SkillId + " ");


            ReTrue();

        }
        void SaveSkill()
        {
            Model.SkillModel model = new SkillModel();
            model.OrderNo = ReInt("OrderNo", 0);
            model.SkillName = ReStr("SkillName", "");
            model.SkillId = ReInt("SkillId", 0);


            DAL.SkillDAL dal = new DAL.SkillDAL();
            if (model.SkillId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }

            ReTrue();

        }


        private void GetSkillList()
        {

            bool Invalid = ReBool("Invalid", false);

            StringBuilder s = new StringBuilder();



            s.Append(" SELECT * FROM dbo.Skill where Invalid='" + Invalid + "' ORDER BY OrderNo DESC	");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void AddMyTeam()
        {
            BLL.MemberBLL bll = new MemberBLL();
            decimal MemberId = ReDecimal("MemberId", 0);



            decimal TeamId = bll.AddMyTeam(MemberId);


            ReDict2.Add("TeamId", TeamId.ToString());

            ReTrue();
        }

        private void GetMyTeamMemberList()
        {
            StringBuilder s = new StringBuilder();

            decimal MemberId = ReDecimal("MemberId", 0);



            if (MemberId == 0)
            {
                throw new Exception("用户编号不能为空!");
            }

            s.Append("  DECLARE @TeamId AS DECIMAL =(SELECT TeamId FROM  dbo.Member WHERE MemberId=" + MemberId + ") ");
            s.Append("  SELECT MemberId, PicImgUrl,RealName,Phone,Sex,ProcessLvStatusId,ProcessLvStatusName,ProcessLvId,ProcessLvName,ProcessLvTitle,SfzNo,SfzImgUrl1,SfzImgUrl2,TeamId,TeamLvId FROM dbo.MemberView WHERE TeamId=@TeamId order by TeamLvId desc ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];

            decimal TeamId = decimal.Parse(dr["TeamId"].ToString());

            if (TeamId == 0)
            {
                // throw new Exception("该成员不在团队中!");
                dt.Rows.Clear();
            }



            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void AddTeamMember()
        {


            BLL.MemberBLL bll = new MemberBLL();
            string Phone = ReStr("Phone", "");
            string yzm = ReStr("yzm", "");
            decimal TeamMemberId = ReDecimal("TeamMemberId", 0);
            bll.AddTeamMember(Phone, yzm, TeamMemberId);

            ReTrue();


        }

        private void RemoveMemberVsFabric()
        {

            Model.MemberVsFabricModel model = new MemberVsFabricModel();
            model.MemberId = ReDecimal("MemberId", 0);
            model.FabricId = ReInt("FabricId", 0);
            DAL.MemberVsFabricDAL dal = new DAL.MemberVsFabricDAL();
            dal.DeleteList(" MemberId='" + model.MemberId + "' and FabricId=" + model.FabricId + "  ");


            ReTrue();
        }

        private void SaveMemberVsFabric()
        {


            Model.MemberVsFabricModel model = new MemberVsFabricModel();
            model.MemberId = ReDecimal("MemberId", 0);
            model.FabricId = ReInt("FabricId", 0);
            DAL.MemberVsFabricDAL dal = new DAL.MemberVsFabricDAL();

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion





                dal.DeleteList(" MemberId='" + model.MemberId + "' and FabricId=" + model.FabricId + "  ");


                StringBuilder s = new StringBuilder();

                s.Append(" SELECT COUNT (0) FROM dbo.MemberVsFabric where MemberId='" + model.MemberId + "'  ");
                int i = DAL.DalComm.ExInt(s.ToString());
                if (i > 2)
                {
                    throw new Exception("最多只允许选择三项擅长面料");
                }


                dal.Add(model);

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion




            ReTrue();

        }

        private void RemoveMemberVsPlate()
        {
            Model.MemberVsPlateModel model = new MemberVsPlateModel();
            model.MemberId = ReDecimal("MemberId", 0);

            model.PlateId = ReInt("PlateId", 0);
            DAL.MemberVsPlateDAL dal = new DAL.MemberVsPlateDAL();




            dal.DeleteList(" MemberId='" + model.MemberId + "' and PlateId=" + model.PlateId + "  ");


            ReTrue();
        }

        private void SaveMemberVsPlate()
        {
            Model.MemberVsPlateModel model = new MemberVsPlateModel();
            model.MemberId = ReDecimal("MemberId", 0);

            model.PlateId = ReInt("PlateId", 0);
            DAL.MemberVsPlateDAL dal = new DAL.MemberVsPlateDAL();
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                dal.DeleteList(" MemberId='" + model.MemberId + "' and PlateId=" + model.PlateId + "  ");

                StringBuilder s = new StringBuilder();

                s.Append(" SELECT COUNT (0) FROM dbo.MemberVsPlate where MemberId='" + model.MemberId + "'  ");
                int i = DAL.DalComm.ExInt(s.ToString());
                if (i > 2)
                {
                    throw new Exception("最多只允许选择三项擅长品类");
                }


                dal.Add(model);

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
            ReTrue();
        }

        private void MemberVsPlateVsFabric()
        {


            decimal MemberId = ReDecimal("MemberId", 0);
            StringBuilder s = new StringBuilder();


            s.Append(" SELECT * FROM   dbo.Fabric ");

            s.Append(" SELECT * FROM dbo.Plate ");
            s.Append(" SELECT * FROM dbo.MemberVsFabric WHERE MemberId=" + MemberId + " ");

            s.Append(" SELECT *  FROM dbo.MemberVsPlate WHERE MemberId=" + MemberId + " ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());




            ReDict.Add("Fabric", JsonHelper.ToJson(ds.Tables[0]));
            ReDict.Add("Plate", JsonHelper.ToJson(ds.Tables[1]));
            ReDict.Add("MemberVsFabric", JsonHelper.ToJson(ds.Tables[2]));
            ReDict.Add("MemberVsPlate", JsonHelper.ToJson(ds.Tables[3]));
            ReTrue();


        }

        private void GetMemberList()
        {

            string MemberStr = ReStr("MemberStr", "");
            int ProcessLvId = ReInt("ProcessLvId", 0);
            int ProcessLvStatusId = ReInt("ProcessLvStatusId", 0);
            int top = ReInt("top", 10);


            StringBuilder s = new StringBuilder();
            s.Append(" SELECT top " + top + " * FROM dbo.MemberView WHERE  1=1 ");
            if (Common.Validator.IsIDCard(MemberStr))
            {

                s.Append(" and SfzNo like '" + MemberStr + "%'  ");
            }
            if (Common.Validator.IsMobile(MemberStr))
            {

                s.Append(" and Phone like '" + MemberStr + "%'  ");
            }
            if (Common.Validator.IsChinese(MemberStr))
            {

                s.Append(" and RealName like '" + MemberStr + "%'  ");
            }

            if (ProcessLvId != 0)
            {
                s.Append(" and ProcessLvId <=" + ProcessLvId + " ");
            }

            if (ProcessLvStatusId != 0)
            {
                s.Append(" and ProcessLvStatusId >=" + ProcessLvStatusId + " ");
            }


            DataSet ds = DAL.DalComm.BackData(s.ToString());


            DataTable dt = ds.Tables[0];


            ReDict.Add("list", JsonHelper.ToJson(dt));


            ReTrue();





        }

        private void ProcessLvStatusChange()
        {
            decimal MemberId = ReDecimal("MemberId", 0);
            int ProcessLvStatusId = ReInt("ProcessLvStatusId");
            if (MemberId == 0)
            {

                throw new Exception("MemberId不能为0");
            }


            BLL.MemberBLL bll = new MemberBLL();

            bll.ProcessLvStatusChange(MemberId, ProcessLvStatusId);





            ReTrue();
        }

        private void AllowSubjectCash()
        {


            decimal SubjectCashId = ReDecimal("SubjectCashId", 0);
            BLL.MemberBLL mbll = new BLL.MemberBLL();

            mbll.AllowSubjectCash(SubjectCashId);
            ReTrue();

        }

        private void RefuseSubjectCash()
        {
            decimal SubjectCashId = ReDecimal("SubjectCashId", 0);
            BLL.MemberBLL mbll = new BLL.MemberBLL();

            mbll.RefuseSubjectCash(SubjectCashId);
            ReTrue();
        }

        private void GetSubjectCashList()
        {
            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc ");
            DAL.SubjectCashDAL dal = new DAL.SubjectCashDAL();

            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");

            StringBuilder s = new StringBuilder();


            s.Append(" CreateTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");

            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);
            RePage2(ds);
        }

        private void YzmLogin()
        {
            string PhoneNo = ReStr("PhoneNo", "");
            string yzm = ReStr("yzm", "");
            decimal MerId = ReDecimal("MerId", 0);
            int LoginDay = ReInt("LoginDay", 10);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }



            if (PhoneNo == "")
            {
                throw new Exception("手机号码不能为空!");
            }


            if (!Common.Validator.IsMobile(PhoneNo))
            {
                throw new Exception("不规范的手机号码!");
            }

            if (yzm == "")
            {
                throw new Exception("验证码不能为空!");
            }


            BLL.MemberBLL bll = new BLL.MemberBLL();




            DataSet dsMember = bll.YzmLogin(MerId, PhoneNo, yzm);

            ReDict.Add("CurrentMember", JsonHelper.ToJsonNo1(dsMember));

            ReTrue();
        }

        private void SaveSubjectCash()
        {

            DAL.SubjectCashDAL dal = new DAL.SubjectCashDAL();
            Model.SubjectCashModel model = new SubjectCashModel();
            model.SubjectCashId = ReDecimal("SubjectCashId", 0);







            if (model.SubjectCashId == 0)
            {

                model = dal.GetModel(model.SubjectCashId);
            }
            else
            {


            }


            model.MemberId = ReDecimal("MemberId", 0);
            model.CreateTime = ReTime("CreateTime", DateTime.Now);
            model.Amount = ReDecimal("Amount", 0);
            model.SubjectCashStatusId = ReInt("SubjectCashStatusId", 0);
            model.Memo = ReStr("Memo", "");
            model.DoneTime = ReTime("DoneTime", DateTime.Now);

            BLL.MemberBLL bll = new BLL.MemberBLL();

            if (model.Amount < 100)
            {
                throw new Exception("提现金额不能小于100元");
            }

            bll.SaveSubjectCash(model);

            ReDict2.Add("SubjectCashId", model.SubjectCashId.ToString());
            ReTrue();

        }

        private void GetMemberBankCardInfo()
        {

            decimal MemberBankCardId = ReDecimal("MemberBankCardId", 0);


            if (MemberBankCardId == 0)
            {
                throw new Exception("没有MemberBankCardId!");
            }

            StringBuilder s = new StringBuilder();

            s.Append("SELECT * FROM  dbo.MemberBankCard WHERE MemberBankCardId="+MemberBankCardId+"");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            ReDict.Add("info", JsonHelper.ToJsonNo1(dt));

            ReTrue();

        }

        private void GetMemberBankCardList()
        {
            decimal MemberId = ReDecimal("MemberId", 0);
            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }

            DataSet ds = DAL.DalComm.BackData(" SELECT * FROM CORE.dbo.MemberBankCard WHERE MemberId=" + MemberId + " ");

            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));


            ReTrue();
        }

        private void SaveMemberBankCard()
        {
            Model.MemberBankCardModel model = new MemberBankCardModel();

            DAL.MemberBankCardDAL dal = new DAL.MemberBankCardDAL();
            model.MemberBankCardId = ReDecimal("MemberBankCardId", 0);

            if (model.MemberBankCardId > 0)
            {
                model = dal.GetModel(model.MemberBankCardId);

            }

            model.BankCardCode = ReStr("BankCardCode", "");
            model.BankName = ReStr("BankName", "");
            model.BankCardName = ReStr("BankCardName", "");
            model.OrderNo = ReInt("OrderNo", 0);
            model.MemberId = ReDecimal("MemberId", 0);
            model.BankCardAccount = ReStr("BankCardAccount", "");
            bool NeedYzm = ReBool("NeedYzm", true);


            string yzm = ReStr("yzm", "");
            if (model.BankCardName == "")
            {
                throw new Exception("持卡人姓名不能为空!");
            }

            if (model.BankName == "")
            {
                throw new Exception("银行不能为空!");
            }
            if (model.BankCardCode == "")
            {
                throw new Exception("卡号不能为空!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" DECLARE @PhoneNo AS VARCHAR(50) =(SELECT TOP 1 Phone FROM CORE.dbo.Member WHERE MemberId=" + model.MemberId + ") ");
            s.Append("  SELECT count(0) FROM   DBLOG.dbo.StMsg WHERE PhoneNo=@PhoneNo AND CreateTime >'" + DateTime.Now.AddDays(-2) + "' AND  ReKey='" + yzm + "' ");



            int i = DAL.DalComm.ExInt(s.ToString());

            if (i == 0)
            {
                if (yzm != BLL.StaticBLL.MerOneConfig(1999, "MaxYzm", "光芒神剑"))
                {

                    if (NeedYzm)
                    {

                        throw new Exception("验证码不正确");
                    }
                }
            }

            BLL.MemberBLL bll = new BLL.MemberBLL();
            bll.SaveMemberBankCard(model);
            ReTrue();
        }


        private void GetMemberDetailPageSetting()
        {

            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc ");

            DateTime dtm1 = ReTime("CreateTime1");

            DateTime dtm2 = ReTime("CreateTime2");

            DAL.MemberAmountDetailDAL dal = new DAL.MemberAmountDetailDAL();

            StringBuilder s = new StringBuilder();

            decimal MemberId = ReDecimal("MemberId", 0);
            s.Append(" 1=1 ");
            if (MemberId != 0)
            {

                s.Append(" and  MemberId=" + MemberId + " ");
            }

            s.Append(" and CreateTime between '" + dtm1 + "' and '" + dtm2 + "' ");

            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);
            RePage2(ds);

        }

        private void GetMemberAmountDetail()
        {

            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc ");
            DAL.MemberAmountDetailDAL dal = new DAL.MemberAmountDetailDAL();

            StringBuilder s = new StringBuilder();

            decimal MemberId = ReDecimal("MemberId", 0);

            s.Append(" MemberId=" + MemberId + " ");


            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);
            RePage2(ds);
        }

        private void AcceptProcessLv()  //通过认证
        {

            decimal MemberId = ReDecimal("MemberId", 0);


            int ProcessLvId = ReInt("ProcessLvId", 0);





            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                if (MemberId != 0 && ProcessLvId != 0)
                {

                    DAL.DalComm.ExReInt(" 	   UPDATE dbo.Member SET ProcessLvId=" + ProcessLvId + " WHERE MemberId=" + MemberId + " ");
                }

                string uid = ReStr("uid", "");




                BLL.MemberBLL bll = new MemberBLL();

                bll.AcceptProcessLv(MemberId);
             #region 事务关闭

                transactionScope.Complete();

            }
            #endregion










            ReTrue();
        }

        private void SaveMember()
        {

            Model.MemberModel model = new Model.MemberModel();
            DAL.MemberDAL dal = new DAL.MemberDAL();


            model.MemberId = ReDecimal("MemberId", 0);

            if (model.MemberId > 0)
            {
                model = dal.GetModel(model.MemberId);
            }
            else
            {

                model.CreateTime = DateTime.Now;
            }

            model.MemberCode = ReStr("MemberCode", "");
            model.NickName = ReStr("NickName", "");
            model.Sex = ReStr("Sex", "");

            model.MemberName = ReStr("MemberName", "");
            model.RealName = ReStr("RealName", "");

            model.Memo = ReStr("Memo", "");
            model.WxPtId = ReDecimal("WxPtId", 0);
            model.WxOpenId = ReStr("WxOpenId", "");
            model.Invalid = ReBool("Invalid", false);
            model.Email = ReStr("Email", "");
            model.Phone = ReStr("Phone", "");

            model.PicImgId = ReStr("PicImgId", "");
            model.Integral = ReDecimal("Integral", 0);

            model.MemoName = ReStr("MemoName", "");
            //  model.LastBuyTime = ReTime("LastBuyTime", DateTime.Now);
            //  model.IsPromoter = ReBool("IsPromoter", false);
            //  model.ExtMemberId = ReDecimal("ExtMemberId", 0);
            // model.ExtMemberLv = ReInt("ExtMemberLv", 0);
            model.LastSiteId = ReDecimal("LastSiteId", 0);
            model.ProcessLvId = ReInt("ProcessLvId", 0);
            // model.ProcessLvStatusId = ReInt("ProcessLvStatusId", 0);
            model.SfzNo = ReStr("SfzNo", "");
            //model.SfzImg1 = ReStr("SfzImg1", "");
            //model.SfzImg2 = ReStr("SfzImg2", "");
            model.AreaId = ReStr("AreaId", "");
            model.Address = ReStr("Address", "");

            BLL.MemberBLL bll = new BLL.MemberBLL();
            bll.SaveMember(model);
            ReTrue();

        }

        private void GetMemberInfo()//给后台用户使用
        {


            decimal MemberId = ReDecimal("MemberId", 0);
            StringBuilder s = new StringBuilder();



            s.Append(" DECLARE @DjsAmount as decimal =(   SELECT   ISNULL(SUM(o.CheckQuantity* o.Wages),0) AS  DjsAmount FROM dbo.OrderToWork o WHERE MemberId=" + MemberId + " AND OrderToWorkStatusId=50 )");


            s.Append(" SELECT *,@DjsAmount AS DjsAmount FROM dbo.MemberView WHERE MemberId=" + MemberId + " ");
            s.Append(" SELECT mvs.*, s.SkillName FROM dbo.MemberVsSkill mvs  ");
            s.Append("  INNER JOIN dbo.Skill s ON s.SkillId = mvs.SkillId WHERE mvs.MemberId=" + MemberId + " ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            ReDict.Add("info", JsonHelper.ToJsonNo1(dt));
            ReDict.Add("Skill", JsonHelper.ToJson(ds.Tables[1]));
            ReTrue();
        }

        private void SaveProcessLvSubject()
        {

            BLL.MemberBLL bll = new MemberBLL();
            string SfzImg1 = ReStr("SfzImg1", "");
            string SfzImg2 = ReStr("SfzImg2", "");


            string Sex = ReStr("Sex", "");
            string RealName = ReStr("RealName", "");
            string SfzNo = ReStr("SfzNo", "");





            string AreaId = ReStr("AreaId", "");
            string Address = ReStr("Address", "");

            decimal MemberId = ReDecimal("MemberId", 0);
            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为空!");
            }

            int ProcessLvId = ReInt("ProcessLvId", 0);

            int ProcessLvStatusId = 10;

            int SkillId = ReInt("SkillId", 0);
            if (SkillId == 0)
            {
                throw new Exception("技能必须填写!");
            }

            #region 验证
            if (RealName == "")
            {
                throw new Exception("姓名不能为空!");
            }



            if (RealName.Length > 4)
            {
                throw new Exception("姓名不能大于四个汉字!");
            }
            if (!Common.Validator.IsChinese(RealName))
            {
                throw new Exception("姓名必须为中文汉字!");
            }
            if (!Common.Validator.IsIDCard(SfzNo))
            {
                throw new Exception("必须输入正确的身份证号码");
            }
            if (SfzImg1 == "" || SfzImg2 == "")
            {
                throw new Exception("必须上传身份证原件双面");
            }


            if (AreaId == "")
            {
                throw new Exception("区县不能为空!");
            }

            if (Address == "")
            {
                throw new Exception("详细地址必须填写!");
            }

            #endregion

            StringBuilder s = new StringBuilder();




            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                bll.SaveOneSkill(new MemberVsSkillModel()
                {

                    SkillId = SkillId,
                    MemberId = MemberId

                });

                s.Append(" UPDATE dbo.Member SET ");
                s.Append(" AreaId='" + AreaId + "', ");
                s.Append(" RealName='" + RealName + "', ");
                s.Append(" Sex='" + Sex + "', ");
                s.Append(" SfzNo='" + SfzNo + "', ");
                s.Append(" SfzImg1='" + SfzImg1 + "', ");
                s.Append(" SfzImg2='" + SfzImg2 + "',  ");
                //s.Append(" SfzNo='" + SfzNo + "',  ");
                s.Append(" Address='" + Address + "', ");
                s.Append(" ProcessLvStatusId=" + ProcessLvStatusId + ", ");
                s.Append(" ProcessLvId=" + ProcessLvId + " ");
                s.Append(" where MemberId=" + MemberId + "  ");


                BLL.MsgBLL msgBll = new BLL.MsgBLL();

                msgBll.SendMsgToUser("1999001", new MsgTextModel()
                {
                    CreateTime = DateTime.Now,
                    MsgContent = "用户:" + RealName + "申请了技能认证",
                    MsgTitle = "用户:" + RealName + "申请了技能认证",
                    MsgType = "SaveProcessLvSubject",
                    EndTime = DateTime.Now.AddDays(1),
                    Extra = "{MemberId:" + MemberId + "}"


                });



                DAL.DalComm.ExReInt(s.ToString());

                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion






            ReTrue();

        }

        private void GetExtMemberDict()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.ExtMemberDict WITH(NOLOCK) ORDER BY ExtMemberLv ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];


            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();



        }

        private void RemoveExtMemberId()
        {
            throw new NotImplementedException();
        }

        private void SetExtMemberId()
        {
            decimal MemberId = ReDecimal("MemberId", 0);
            int ExtMemberId = ReInt("ExtMemberId", 0);

            DAL.DalComm.ExReInt(" UPDATE dbo.Member SET  ExtMemberId='" + ExtMemberId + "' WHERE MemberId='" + MemberId + "' ");

            ReTrue();
        }

        private void SetExtMemberLv()
        {
            decimal MemberId = ReDecimal("MemberId", 0);
            int ExtMemberLv = ReInt("ExtMemberLv", 0);

            DAL.DalComm.ExReInt(" UPDATE dbo.Member SET  ExtMemberLv='" + ExtMemberLv + "' WHERE MemberId='" + MemberId + "' ");

            ReTrue();

        }

        private void ChangeSex()
        {
            string Sex = ReStr("Sex", "");
            decimal MemberId = ReDecimal("MemberId", 0);

            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.Member SET Sex='" + Sex + "' WHERE MemberId='" + MemberId + "' ");
            DAL.DalComm.ExReInt(s.ToString());

            ReTrue();
        }

        private void ChangeBirthday()
        {
            decimal MemberId = ReDecimal("MemberId", 0);
            DateTime Birthday = ReTime("Birthday");


            DAL.DalComm.ExReInt(" UPDATE dbo.Member SET Birthday='" + Birthday + "' WHERE MemberId='" + MemberId + "' ");
            ReTrue();

        }

        private void ChangePhone()
        {
            string Pwd = ReStr("Pwd");
            decimal MemberId = ReDecimal("MemberId", 0);
            string Phone = ReStr("Phone", "").Trim();
            string Yzm = ReStr("Yzm", "");

            decimal MerId = ReDecimal("MerId", 0);

            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }

            int i = DAL.DalComm.ExInt("  SELECT COUNT(0) FROM dbo.Member WHERE MemberId='" + MemberId + "' AND Pwd='" + Pwd + "' ");




            if (i < 1)
            {

                throw new Exception("密码输入不正确");
            }


            i = DAL.DalComm.ExInt(" SELECT * FROM dbo.Member where MerId='" + MerId + "' AND Phone='" + Phone + "' ");
            if (i > 0)
            {

                throw new Exception("手机号[" + Phone + "]已被注册.如果您确定您是此号的机主可以尝试使用找回密码功能!");
            }


            if (!Common.Validator.IsMobile(Phone))
            {
                throw new Exception("请正确输入手机号码");
            }

            i = DAL.DalComm.ExInt("  SELECT COUNT(0) FROM DBLOG.dbo.StMsg WHERE PhoneNo='" + Phone + "' AND ReKey='" + Yzm + "' and CreateTime >=" + DateTime.Now.AddHours(-24) + " ");
            if (i < 0)
            {
                throw new Exception(" 验证码不正确 ");
            }


            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.Member SET Phone='" + Phone + "' WHERE MemberId='" + MemberId + "' ");


            DAL.DalComm.ExReInt(s.ToString());

            ReTrue();

        }

        private void ChangeNickName()
        {
            StringBuilder s = new StringBuilder();

            decimal MemberId = ReDecimal("MemberId", 0);

            string NickName = ReStr("NickName", "");

            if (NickName.Length < 3)
            {
                throw new Exception("昵称至少为3个字符以上!");
            }

            //if (Common.StringPlus.isLegalCharacters(NickName))
            //{
            //    throw new Exception("昵称只能为汉字或英文");
            //}

            s.Append(" UPDATE dbo.Member SET NickName='" + NickName + "' WHERE MemberId='" + MemberId + "' ");
            DAL.DalComm.ExReInt(s.ToString());

            ReTrue();

        }

        private void YiChang()
        {
            StringBuilder s = new StringBuilder();

            decimal MemberId = ReDecimal("MemberId", 0);
            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }

            int MemberLv = ReInt("MemberLv", 0);

            if (MemberLv == 0)
            {

            }

            s.Append(" UPDATE dbo.Member SET MemberLv=" + MemberLv + " WHERE MemberId=" + MemberId + " ");


            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();
        }

        private void SaveDevicePush()
        {
            StringBuilder s = new StringBuilder();
            Model.DevicePushModel model = new DevicePushModel();
            model.DeviceId = ReStr("DeviceId", "");
            model.PushType = ReStr("PushType", "");
            model.ReKey = ReStr("ReKey", "");
            model.Memo = ReStr("Memo", "");

            DAL.DevicePushDAL dal = new DAL.DevicePushDAL();

            dal.Add(model);
            ReTrue();

        }

        private void DelDevicePush()
        {
            StringBuilder s = new StringBuilder();
            Model.DevicePushModel model = new DevicePushModel();
            model.DeviceId = ReStr("DeviceId", "");
            model.PushType = ReStr("PushType", "");
            model.ReKey = ReStr("ReKey", "");



            s.Append(" Delete  FROM DBMSG.dbo.DevicePush  WHERE 1=1 ");
            if (model.DeviceId != "")
            {
                s.Append(" and DeviceId='" + model.DeviceId + "' ");
            }
            else
            {
                throw new Exception("DeviceId不能为空!");
            }
            if (model.PushType != "")
            {
                s.Append(" and PushType='" + model.PushType + "' ");
            }
            if (model.ReKey != "")
            {
                s.Append(" and ReKey='" + model.ReKey + "' ");
            }

            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void GetDevicePush()
        {
            StringBuilder s = new StringBuilder();

            Model.DevicePushModel model = new DevicePushModel();
            model.DeviceId = ReStr("DeviceId", "");
            model.PushType = ReStr("PushType", "");
            model.ReKey = ReStr("ReKey", "");



            s.Append(" SELECT * FROM DBMSG.dbo.DevicePush WITH(NOLOCK) WHERE 1=1 ");
            if (model.DeviceId != "")
            {
                s.Append(" and DeviceId='" + model.DeviceId + "' ");
            }
            if (model.PushType != "")
            {
                s.Append(" and PushType='" + model.PushType + "' ");
            }
            if (model.ReKey != "")
            {
                s.Append(" and ReKey='" + model.ReKey + "' ");
            }

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();


        }

        private void SyncGroup()   //具有排他性, 当登录一个店铺的时候, 强制登出其他所有店铺
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();

            string RongUserId = ReStr("RongUserId", "");
            decimal MerId = ReDecimal("MerId", 0);
            string BranchId = ReStr("BranchId", "");

            if (RongUserId == "")
            {
                throw new Exception("RongUserId不能为空!");
            }
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }

            Dictionary<string, string> GroupInfo = new Dictionary<string, string>();

            string Gid = "Branch" + BranchId;

            GroupInfo.Add(Gid, Gid);

            mbll.SyncGroup(RongUserId, GroupInfo, MerId);
            ReTrue();

        }

        private void GetWeiDuMsgNumAndRongUserId()
        {

            string TargetId = ReStr("TargetId", "");
            if (TargetId == "")
            {
                throw new Exception("TargetId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT RongUserId,COUNT(0) AS MsgNum FROM YYHD.dbo.RongMsgLogView  WHERE TargetId ='" + TargetId + "' AND MsgStatus<20  GROUP BY  RongUserId ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void GetZiXunList()
        {
            string RongUserId = ReStr("RongUserId", "");
            int top = ReInt("top", 5);
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT top " + top + " *  FROM YYHD.dbo.ZiXunLogView with(NOLOCK) WHERE RongUserId='" + RongUserId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void SaveZiXunLogInfo()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            Model.ZiXunLogInfoModel model = new ZiXunLogInfoModel();
            model.CreateTime = DateTime.Now;
            model.Memo = ReStr("Memo", "");
            model.RongUserId = ReStr("RongUserId", "");
            model.ZiXunLogJson = ReStr("ZIXunLogJson", "");
            model.ZiXunLogReKey = ReStr("ZiXunLogReKey", "");
            model.ZiXunLogTypeId = ReInt("ZiXunLogTypeId", 0);

            if (model.ZiXunLogTypeId == 0)
            {
                ReTrue();
            }
            else
            {

                mbll.SaveZiXunLogInfo(model);
                ReTrue();
            }



        }

        private void EndJieDai()
        {
            string DeviceId = ReStr("DeviceId", "");

            string UserId = ReStr("uid", "");



            StringBuilder s = new StringBuilder();
            s.Append(" delete from YYHD.dbo.KfJieDai where UserId='" + UserId + "' and DeviceId='" + DeviceId + " '  ");
            DAL.DalComm.ExReInt(s.ToString());

            ReTrue();

        }

        private void GetUserOnline()
        {
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" select * from  YYHD.dbo.UserOnlineView with(nolock) where MerId='" + MerId + "' and UserOnlineStatusId<50 ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();
        }

        private void GetWeiDuMsgNum()
        {
            Model.RongMsgLogInfoModel model = new RongMsgLogInfoModel();

            model.ReRole = ReStr("ReRole");
            model.SendRole = ReStr("SendRole");
            model.ReUserId = ReStr("ReUserId", "");
            model.RongUserId = ReStr("RongUserId", "");
            model.ReDeviceId = ReStr("ReDeviceId", "");
            model.ReMemberId = ReDecimal("ReMemberId", 0);
            //  model.ReUserId = ReStr("uid", "");
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            int i = mbll.GetWeiDuMsgNum(model);
            ReDict2.Add("i", i.ToString());
            ReTrue();

        }
        private void GetWeiDuMsgNum2()
        {
            string RongUserId = ReStr("RongUserId", "");
            string TargetId = ReStr("TargetId", "");

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT count(0) as MsgNum FROM dbo.RongMsgLogView with(nolock) where ");
            s.Append("  MsgStatus<20  ");
            if (RongUserId != "")
            {
                s.Append(" and RongUserId='" + RongUserId + "' ");
            }
            if (TargetId != "")
            {
                s.Append("  and TargetId='" + TargetId + "' ");
            }

            int i = DAL.DalComm.ExInt(s.ToString());
            ReDict2.Add("MsgNum", i.ToString());
            ReTrue();
        }



        private void ReadRongMsg()
        {

            string RongUserId = ReStr("RongUserId", "");

            //string SendRole = ReStr("SendRole");
            //string ReRole = ReStr("ReRole");
            string TargetId = ReStr("TargetId", "");
            decimal RongMsgLogId = ReDecimal("RongMsgLogId", 0);
            //string ReUserId = ReStr("ReUserId", "");
            //decimal ReMemberId = ReDecimal("ReMemberId", 0);
            int MsgStatus = ReInt("MsgStatus", 20);

            StringBuilder s = new StringBuilder();
            s.Append("  UPDATE YYHD.dbo.RongMsgLogView SET MsgStatus=" + MsgStatus + " WHERE 1=1 and MsgStatus<" + MsgStatus + " ");


            if (RongMsgLogId != 0)
            {
                s.Append(" and RongMsgLogId='" + RongMsgLogId + "' ");
            }



            if (TargetId != "")
            {
                s.Append(" and TargetId='" + TargetId + "' ");
            }
            else
            {

                throw new Exception("没有传递TargetId");
            }


            if (RongUserId != "")
            {
                s.Append(" and RongUserId='" + RongUserId + "' ");
            }
            else
            {
                throw new Exception("没有传递RongUserId");
            }








            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void GetMsgLogList()
        {
            decimal MemberId1 = ReDecimal("MemberId1", 0);
            decimal MemberId2 = ReDecimal("MemberId2", 0);
            string CurrentUserId1 = ReStr("uid1", "");
            string CurrentUserId2 = ReStr("uid2", "");

            string DeviceId1 = ReStr("DeviceId1", "");  //发送方的DeviceId
            string DeviceId2 = ReStr("DeviceId2", "");   //接收方的DeviceId
            int top = ReInt("top", 10);
            decimal BeforRongMsgLogId = ReDecimal("BeforRongMsgLogId", 0);
            StringBuilder s = new StringBuilder();
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();


            s1.Append(" SELECT  DeviceId   FROM    DBMSG.dbo.Device  with(nolock)  where 1=2 ");
            s2.Append("SELECT  DeviceId   FROM    DBMSG.dbo.Device  with(nolock)  where 1=2 ");
            if (MemberId1 != 0)
            {
                s1.Append(" or MemberId = " + MemberId1 + " ");

            }

            if (MemberId2 != 0)
            {
                s2.Append(" or MemberId = " + MemberId2 + " ");

            }

            if (CurrentUserId1 != "")
            {

                s1.Append(" or UserId = '" + CurrentUserId1 + "' ");
            }
            if (CurrentUserId2 != "")
            {

                s2.Append(" or UserId = '" + CurrentUserId2 + "' ");
            }
            if (DeviceId1 != "")
            {

                s1.Append(" or DeviceId = '" + DeviceId1 + "'  ");
            }

            if (DeviceId2 != "")
            {
                s2.Append(" or DeviceId = '" + DeviceId2 + "'  ");    //一般情况下, 1和2都只会出现一种可能性,要不就是MemberId,要不就是UserId,要不就是DeviceId. 不会同时都出现
            }


            s.Append(" SELECT TOP " + top + " ");
            s.Append(@"  a.RongMsgLogId,
                         a.SendRole,
                         a.ReRole,
                         a.CreateTime,
                         a.RongUserId,
                         a.ContentText,
                         a.MsgStatus,
                         a.Extra ");
            s.Append(" FROM    ( ");
            s.Append(" (  SELECT  *   FROM   YYHD.dbo.RongMsgLogView WITH(NOLOCK) WHERE   RongUserId IN(" + s1.ToString() + ")  and TargetId in (" + s2.ToString() + ") )  "); //我发给对方的
            s.Append("  UNION ALL ");
            s.Append(" (  SELECT  *   FROM   YYHD.dbo.RongMsgLogView WITH(NOLOCK) WHERE   RongUserId IN (" + s2.ToString() + ") and TargetId in (" + s1.ToString() + ") )  ");  //对方发给我的
            s.Append(" ) a ");
            s.Append(" where 1=1 ");

            if (BeforRongMsgLogId != 0)
            {
                s.Append(" and RongMsgLogId<" + BeforRongMsgLogId + " ");
            }

            s.Append("   GROUP BY   ");
            s.Append(@"  a.RongMsgLogId,
                         a.SendRole,
                         a.ReRole,
                         a.CreateTime,
                         a.RongUserId,
                         a.ContentText,
                         a.MsgStatus,
                         a.Extra ");
            s.Append(" ORDER BY RongMsgLogId DESC ");




            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void GetKfJieDaiList()
        {
            string CurrentUserId = ReStr("uid", "");
            decimal MerId = ReDecimal("MerId", 0);
            decimal MemberId = ReDecimal("MemberId", 0);
            string DeviceId = ReStr("DeviceId", "");
            StringBuilder s = new StringBuilder();
            s.Append("   SELECT  * FROM YYHD.dbo.KfJieDaiView WHERE MerId='" + MerId + "' AND UserId='" + CurrentUserId + "'  ");
            if (MemberId != 0)
            {
                s.Append(" and MemberId='" + MemberId + "' ");
            }
            if (DeviceId != "")
            {
                s.Append(" and DeviceId='" + DeviceId + "' ");
            }


            if (CurrentUserId == "")
            {
                throw new Exception("CurrentUserId不能为空!");
            }
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void UserOnLineStatusChange()
        {
            string CurrentUserId = ReStr("uid", "");
            decimal MerId = ReDecimal("MerId", 0);
            int UserOnlineStatusId = ReInt("UserOnlineStatusId", 10);

            DAL.DalComm.ExReInt("UPDATE YYHD.dbo.UserOnLine SET UserOnlineStatusId='" + UserOnlineStatusId + "',LastTime='" + DateTime.Now + "' WHERE UserId='" + CurrentUserId + "' AND MerId='" + MerId + "'");

            ReTrue();

        }

        private void KfPageReady()
        {
            StringBuilder s = new StringBuilder();

            string CurrentUserId = ReStr("uid", "");
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0");
            }
            if (CurrentUserId == "")
            {
                throw new Exception("CurrentUserId不能为空!");
            }
            DataSet ds_UserOnlineStatus = BLL.StaticBLL.CacheData("SELECT * FROM YYHD.dbo.UserOnlineStatus WITH(NOLOCK)", "UserOnlineStatus", 3000);
            DataTable dt_UserOnlineStatus = ds_UserOnlineStatus.Tables[0];


            s.Append(" SELECT * FROM YYHD.dbo.UserOnLine WITH(NOLOCK) WHERE UserId='" + CurrentUserId + "' and MerId='" + MerId + "'  ");

            s.Append(" SELECT * FROM YYHD.dbo.KfJieDaiView WITH(NOLOCK) WHERE UserId='" + CurrentUserId + "' and MerId='" + MerId + "'  ");//接待列表

            s.Append(" UPDATE YYHD.dbo.UserOnLine SET LastTime='" + DateTime.Now + "' where UserId='" + CurrentUserId + "' and MerId='" + MerId + "' ");//修改最后时间, 
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt_UserOnLine = ds.Tables[0];
            DataTable dt_JieDaiView = ds.Tables[1];
            #region 如果客服表中没有,则加入
            if (dt_UserOnLine.Rows.Count == 0)
            {
                Model.UserOnLineModel model = new UserOnLineModel();
                model.LastTime = DateTime.Now;
                model.MerId = MerId;
                model.UserOnlineStatusId = 10;
                model.PushTypeId = 1;
                model.UserId = CurrentUserId;
                DAL.UserOnLineDAL dal = new DAL.UserOnLineDAL();
                dal.Add(model);
                ds = DAL.DalComm.BackData(s.ToString());                             //如果客服列表中没有, 那么加入之后也要重新获取一遍数据
                dt_UserOnLine = ds.Tables[0];
                dt_JieDaiView = ds.Tables[1];
            }

            #endregion

            ReDict.Add("UserOnLine", JsonHelper.ToJsonNo1(dt_UserOnLine));
            ReDict.Add("KfJieDai", JsonHelper.ToJsonNo1(dt_JieDaiView));
            ReDict.Add("UserOnlineStatus", JsonHelper.ToJson(dt_UserOnlineStatus));
            ReTrue();
        }

        private void GetKfJieDai()   //获得一个客服接待
        {
            string KfUserId = ReStr("KfUserId", "");
            string RongUserId = ReStr("RongUserId", "");
            StringBuilder s = new StringBuilder();
            decimal MerId = ReDecimal("MerId", 0);
            string DeviceId = ReStr("DeviceId", "").Trim();
            if (DeviceId == "")
            {
                throw new Exception("DeviceId不能为空!");
            }
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }

            s.Append("  SELECT * FROM YYHD.dbo.KfJieDaiNumView WHERE UserOnlineStatusId<30   and MerId='" + MerId + "'   "); //如果有分部,还的加入分部查询条件

            if (KfUserId != "")
            {//没有指派客服人员

                s.Append(" and UserId='" + KfUserId + "' ");


            }
            s.Append("  ORDER BY UserOnlineStatusId, 接待数量 ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtKfUser = ds.Tables[0];

            if (dtKfUser.Rows.Count == 0)
            {
                throw new Exception("对不起, 当前没有在线的客服, 我们的营业时间在早8:00到晚10:00 !");

            }
            DataRow drKfUser = dtKfUser.Rows[0];

            Model.KfJieDaiModel kfJieDaiModel = new KfJieDaiModel();
            kfJieDaiModel.CreateTime = DateTime.Now;
            kfJieDaiModel.DeviceId = DeviceId;
            kfJieDaiModel.JieDaiStatus = 1;
            kfJieDaiModel.LastTime = DateTime.Now;
            kfJieDaiModel.UserId = drKfUser["UserId"].ToString();

            BLL.MemberBLL mbll = new BLL.MemberBLL();
            mbll.SaveKfJieDai(kfJieDaiModel);  //加入接待
            ReDict.Add("KfUserJson", JsonHelper.ToJsonNo1(dtKfUser));//返回客服信息
            ReTrue();



        }

        private void GetQianDaoCountByTimeSolt()
        {
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");
            int top = ReInt("top", 30);
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }

            StringBuilder s = new StringBuilder();


            s.Append("SELECT top " + top + " COUNT(0) AS 签到次数 , ");
            s.Append(" QianDaoMemberId, ");
            s.Append(" Phone ");
            s.Append(" FROM    dbo.QianDaoLogView q ");
            s.Append(" LEFT JOIN dbo.MemberView m ON q.QianDaoMemberId = m.MemberId ");
            s.Append(" WHERE  q.CreateTime BETWEEN '" + dtm1 + "' and '" + dtm2 + "' AND m.MerId=" + MerId + " ");
            s.Append(" GROUP BY QianDaoMemberId, Phone ");
            s.Append(" ORDER BY 签到次数 DESC ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            ReDict.Add("list", JsonHelper.ToJson(ds.Tables[0]));
            ReTrue();
        }

        private void GetDingDanCountByTimeSolt()
        {
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");
            int top = ReInt("top", 30);
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            string BranchId = ReStr("BranchId", "");
            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT top " + top + " COUNT(0) AS 购买次数 , ");
            s.Append("  m.Phone, ");
            s.Append("  CreateMember ");
            s.Append(" FROM    dbo.DingDanView d ");
            s.Append(" LEFT JOIN dbo.MemberView m ON m.MemberId = d.CreateMember ");
            s.Append(" WHERE   d.Status >= 110 ");
            s.Append(" and d.BranchId='" + BranchId + "' ");
            s.Append(" AND d.CreateTime BETWEEN '" + dtm1 + "' and '" + dtm2 + "' AND d.MerchantId=" + MerId + " ");
            s.Append(" GROUP BY CreateMember, ");
            s.Append(" m.Phone ");
            s.Append(" ORDER BY 购买次数 DESC ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            ReDict.Add("list", JsonHelper.ToJson(ds.Tables[0]));
            ReTrue();
        }

        private void GetAmountSumByTimeSolt()
        {
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");
            int top = ReInt("top", 30);
            int SourseTypeId = ReInt("SourseTypeId", 0);  //订单来源, 1是网站,2是app
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }

            string BranchId = ReStr("BranchId", "");
            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT top " + top + "  SUM(Amount) AS 购物金额, ");

            s.Append(" m.Phone, ");
            s.Append(" CreateMember ");
            s.Append(" FROM    dbo.DingDanView d ");
            s.Append(" LEFT JOIN dbo.MemberView m ON m.MemberId = d.CreateMember ");
            s.Append(" WHERE   d.Status >= 110 ");

            s.Append(" and d.BranchId='" + BranchId + "' ");
            if (SourseTypeId != 0)
            {
                s.Append("  AND d.SourseTypeId=" + SourseTypeId + " ");
            }
            s.Append(" AND d.CreateTime BETWEEN '" + dtm1 + "' and '" + dtm2 + "' ");
            s.Append("  AND d.MerchantId=" + MerId + "  ");
            s.Append(" GROUP BY CreateMember, ");
            s.Append(" m.Phone ");
            s.Append(" ORDER BY 购物金额 DESC ");



            DataSet ds = DAL.DalComm.BackData(s.ToString());
            ReDict.Add("list", JsonHelper.ToJson(ds.Tables[0]));
            ReTrue();

        }

        private void GetJiFenSumByTimSolt()
        {
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");
            int top = ReInt("top", 30);
            string sType = ReStr("sType", "");  //得到 dedao , 消耗 xiaohao
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT  SUM(JiFenChangeNum) AS 积分值 , ");
            s.Append(" m.MemberId, Phone ");
            s.Append(" FROM    dbo.JiFenChange j ");
            s.Append(" LEFT JOIN dbo.MemberView m ON   m.MemberId = j.MemberId ");
            s.Append(" WHERE   j.CreateTime  BETWEEN '" + dtm1 + "' and '" + dtm2 + "'  AND m.MerId=" + MerId + " ");
            switch (sType)
            {
                case "dedao":
                    s.Append(" AND JiFenChangeNum > 0 ");
                    break;
                case "xiaohao":
                    s.Append(" AND JiFenChangeNum < 0 ");
                    break;

                default:
                    break;
            }



            s.Append(" GROUP BY m.MemberId, m.Phone ");
            s.Append(" ORDER BY 积分值 desc ");





            DataSet ds = DAL.DalComm.BackData(s.ToString());
            ReDict.Add("list", JsonHelper.ToJson(ds.Tables[0]));
            ReTrue();

        }




        private void SendRongMsg()
        {
            Model.RongMsgLogInfoModel model = new RongMsgLogInfoModel();


            model.ReUserId = ReStr("ReUserId", ""); //接收的UserId
            model.ReMemberId = ReDecimal("ReMemberId", 0);  //接收的用户MemberId
            model.ReDeviceId = ReStr("ReDeviceId", "");  //接收的DeviceId

            model.SendRole = ReStr("SendRole");
            model.ReRole = ReStr("ReRole");

            model.RongUserId = ReStr("RongUserId", "");
            model.RongMsgLogTypeId = ReStr("RongMsgLogTypeId", "RC:TxtMsg");
            model.Title = ReStr("Title", "");
            model.ContentText = ReStr("ContentText", "");
            model.ImgUrl = ReStr("ImgUrl", "");
            model.Extra = ReStr("Extra", "");
            if (model.Extra == "")
            {
                model.Extra = "{}";
            }
            else
            {
                model.Extra = HttpUtility.UrlDecode(model.Extra);
            }


            decimal MerId = ReDecimal("MerId", 0);




            BLL.MemberBLL mbll = new BLL.MemberBLL();

            mbll.SendRongMsg(model, MerId);
            ReDict2.Add("RongMsgLogId", model.RongMsgLogId.ToString());
            ReTrue();
        }

        private void GetRongToken()
        {
            string RongUserId = ReStr("RongUserId", "");
            if (RongUserId == "")
            {
                throw new Exception("RongUserId不能为空!");
            }
            string DeviceId = ReStr("DeviceId", RongUserId);

            decimal MemberId = ReDecimal("MemberId", 0);   //当前用户, 如果当前是匿名的话, 通常为0 , 如果当前客服的话, 这个通常为-1;
            string DeviceHardwareId = ReStr("DeviceHardwareId", "");  //硬件串号
            string RongName = ReStr("RongName", "");  //预留字段,暂时无效
            string RongPortraitUri = ReStr("RongPortraitUri", "");
            decimal MerId = ReDecimal("MerId", 0);
            string CurrentUserId = ReStr("uid", "");
            bool shuaxin = ReBool("shuaxin", false);
            BLL.MemberBLL mbll = new BLL.MemberBLL();

            Model.DeviceModel model = new DeviceModel();

            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfig(MerId, new string[] { "RongAppKey", "RongAppSecret" });





            model = mbll.GetDeviceModel(DeviceId); //需要关联主键取回,如果不存在记录, 那么返回的model里面所有值都为null
            model.DeviceId = DeviceId;
            model.MemberId = MemberId;
            model.DeviceHardwareId = DeviceHardwareId;
            model.LastTime = DateTime.Now;
            model.MerId = MerId;
            model.UserId = CurrentUserId;
            if (model.RongToken == null)
            { //有可能为null,在下面的.trim().length中会报错.
                model.RongToken = "";
            }
            if (model.RongToken.Trim().Length < 5 || shuaxin == true)
            {
                //如果当前token小于5个字节, 或者强制刷新时, 都需要刷新
                model.RongToken = mbll.GetRongToken(MerId, RongUserId, RongName, RongPortraitUri);
            }
            else
            {
                //否则不刷新

            }

            mbll.DeviceBind(model);  //绑定设备信息


            ReDict2.Add("MsgNum", model.MsgNum.ToString());
            ReDict2.Add("token", model.RongToken);
            ReDict2.Add("RongAppKey", MerConfig["RongAppKey"]);
            ReDict2.Add("RongAppSecret", MerConfig["RongAppSecret"]);
            //ReDict2.Add("Rong_KfUserId", Rong_KfUserId); //融云的kfID

            ReTrue();

        }

        private void SendPush()
        {

            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            string MemberIds = ReStr("MemberIds", "");
            string eventStr = ReStr("eventStr", "");
            string alert = ReStr("alert", "");

            BLL.JPushBLL jbll = new BLL.JPushBLL();
            jbll.SendPush(alert, MemberIds, eventStr, MerId, null);
            ReTrue();

        }

        private void DeviceBind()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            Model.DeviceModel model = new DeviceModel();
            string DeviceId = ReStr("DeviceId", "");
            decimal MerId = ReDecimal("MerId", 0);
            if (DeviceId == "")
            {
                throw new Exception("DeviceId不能为空!");
            }
            if (MerId == 0)
            {
                throw new Exception("MerId不能为空");
            }


            model = mbll.GetDeviceModel(DeviceId);
            if (model.DeviceId == null)
            {//如果是新增的,无所谓
                model.RongToken = ReStr("RongToken", "");
            }
            model.AppVersion = ReStr("AppVersion", "");
            model.DeviceId = ReStr("DeviceId", "");
            model.LastTime = DateTime.Now;
            model.MemberId = ReDecimal("MemberId", 0);
            model.SystemType = ReStr("SystemType", "");
            model.SystemVersion = ReStr("SystemVersion", "");

            model.IpAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            model.MerId = ReDecimal("MerId", 1646);
            if (model.MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }


            mbll.DeviceBind(model);
            ReTrue();

        }

        private void InvalidMember()
        {
            bool Invalid = ReBool("Invalid", true);
            decimal MemberId = ReDecimal("MemberId", 0);
            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" update  dbo.Member set Invalid='" + Invalid + "' where MemberId='" + MemberId + "'  ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void ChangeMemberStatus()
        {
            int Status = ReInt("Status", 0);
            decimal MemberId = ReDecimal("MemberId", 0);
            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" update  dbo.Member set Status='" + Status + "' where MemberId='" + MemberId + "'  ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }


        void GetTownList()
        {

            BLL.TownBLL tbll = new BLL.TownBLL();
            DataTable dt = BLL.StaticBLL.GetTownList().Tables[0];
            ReDict.Add("TownList", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void DelGwc()
        {
            decimal GwcId = ReDecimal("GwcId", 0);


            //if (GwcId == 0)
            //{
            //    throw new Exception("购物车ID不能为0!");
            //}

            DAL.GwcDAL dal = new DAL.GwcDAL();
            StringBuilder s = new StringBuilder();
            if (GwcId == 0)
            {

                string BranchId = ReStr("BranchId", "");
                string ProId = ReStr("ProId", "");

                s.Append(" ProId='" + ProId + "' and BranchId='" + BranchId + "'  ");

            }
            else
            {

                s.Append("  GwcId='" + GwcId + "'  ");
            }
            dal.DeleteList(s.ToString());
            ReTrue();
        }


        private void ClearGwc()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            BLL.MerchantBLL merbll = new BLL.MerchantBLL();
            decimal MemberId = ReDecimal("MemberId", mbll.GetCurrentDecimalMemberId());
            string ProId = ReStr("ProId", "");
            merbll.ClearGwc(MemberId, ProId);
            ReTrue();

        }
        void ChangeGwc()
        {
            BLL.MerchantBLL merbll = new BLL.MerchantBLL();
            Model.GwcModel model = new GwcModel();
            model.GwcId = ReDecimal("GwcId", 0);
            if (model.GwcId == 0)
            {

                throw new Exception("购物车ID不能为0!");
            }

            model = merbll.GetGwcModel(model.GwcId); //重新赋值实体类Model

            model.hope = ReDecimal("hope", 0);
            model.Invalid = ReBool("Invalid", false);
            model.MemberId = ReDecimal("MemberId", 0);

            model.ProTeXing = ReInt("ProTeXing", 1);
            model.Quantity = ReDecimal("Quantity", 1);
            model.ZlBs = ReDecimal("ZlBs", 1);

            merbll.ChangeGwc(model);



            ReTrue();

        }

        void AddGwc()
        {


            Model.GwcModel model = new GwcModel();





            model.CreateTime = DateTime.Now;
            model.hope = ReDecimal("hope", 0);
            model.Invalid = ReBool("Invalid", false);
            model.MemberId = ReDecimal("MemberId", 0);
            model.ProId = ReStr("ProId", "");
            model.ProTeXing = ReInt("ProTeXing", 1);
            model.Quantity = ReDecimal("Quantity", 1);
            model.ZlBs = ReDecimal("ZlBs", 1);
            model.BranchId = ReStr("BranchId", "");
            string ZoneId = ReStr("ZoneId", "");
            if (model.BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            else
            {

                int i = DAL.DalComm.ExInt(" SELECT COUNT(0) FROM dbo.Gwc WHERE BranchId <>'" + model.BranchId + "' AND Invalid=0 AND MemberId='" + model.MemberId + "' and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "') ");
                if (i > 0)
                {
                    throw new Exception("对不起，您的购物车中有其他商家的商品，由于派送起点和预约派送时间的原因，添加本商品之前，请先结算或清空购物车内的商品！");
                }

            }
            bool change = ReBool("change", false);
            if (change)//先删除, 再添加
            {
                DAL.DalComm.ExReInt(" DELETE FROM dbo.Gwc WHERE ProId='" + model.ProId + "' AND MemberId='" + model.MemberId + "' and BranchId='" + model.BranchId + "' ");

            }




            BLL.MerchantBLL merbll = new BLL.MerchantBLL();
            merbll.AddGwc(model, ZoneId);



            ReTrue();
        }

        private void TopMember()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();

            StringBuilder s = new StringBuilder();
            decimal MemberId = mbll.GetCurrentDecimalMemberId();
            if (MemberId == 0)
            {

                ReDict.Add("Member", "{\"MemberId\":0}");
            }
            else
            {
                s.Append(" select MemberId,Status,Invalid, Phone,NickName from MemberView with(nolock) where MemberId='" + MemberId + "'  UPDATE dbo.Member SET LastTime='" + DateTime.Now + "' WHERE MemberId='" + MemberId + "' ");

                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int Status = int.Parse(dr["Status"].ToString());
                bool Invalid = bool.Parse(dr["Invalid"].ToString());
                if (Invalid)
                {
                    mbll.LoginOut();
                    throw new Exception("当前用户已经作废, 请重新登录!");
                }
                if (Status < 0)
                {
                    mbll.LoginOut();
                    throw new Exception("当前用户已经冻结, 请重新登录!");
                }


                ReDict.Add("Member", JsonHelper.ToJsonNo1(dt));
            }



            ReTrue();

        }

        private void GwcAmountNum()   //购物车合计
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            BLL.MerchantBLL merbll = new BLL.MerchantBLL();
            decimal MemberId = ReDecimal("MemberId", mbll.GetCurrentDecimalMemberId());
            string BranchId = ReStr("BranchId", "");
            Dictionary<string, string> d = merbll.GwcAmountNum(MemberId, BranchId);
            ReDict.Add("GwcAmount", d["金额"]);
            ReDict.Add("GwcNum", d["数量"]);
            ReTrue();

        }


        private void GetGwc()  //获取购物车
        {
            decimal MemberId = ReDecimal("MemberId", 0);
            string BranchId = ReStr("BranchId", "");
            bool Clear = ReBool("Clear", true);
            string ZoneId = ReStr("ZoneId", "");
            StringBuilder s = new StringBuilder();
            StringBuilder w = new StringBuilder();

            DAL.GwcDAL dal = new DAL.GwcDAL();


            s.Append("SELECT * FROM dbo.GwcView with(nolock) WHERE MemberId=" + MemberId + " and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "') ");
            s.Append("         SELECT BranchId FROM dbo.GwcView WHERE MemberId=" + MemberId + " and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "') GROUP BY BranchId  ");




            DataSet ds = DAL.DalComm.BackData(s.ToString());
            s.Clear();


            DataTable dtGwc = ds.Tables[0];
            DataTable dtBranch = ds.Tables[1];

            List<DataRow> listDel = new List<DataRow>();

            List<DataRow> listNum = new List<DataRow>();
            if (Clear)
            {
                #region 清理


                if (dtGwc.Rows.Count > 0)
                {


                    if (dtBranch.Rows.Count > 1)
                    {


                        dal.DeleteList(" MemberId='" + MemberId + "'  ");

                    }
                    else if (dtBranch.Rows.Count == 1)
                    {

                        if (dtBranch.Rows[0]["BranchId"].ToString() == "")
                        {
                            dal.DeleteList(" MemberId='" + MemberId + "'  ");

                        }
                    }


                    foreach (DataRow drGwc in dtGwc.Rows)
                    {
                        int ProTeXing = int.Parse(drGwc["ProTeXing"].ToString());
                        int ProTeXing2 = int.Parse(drGwc["ProTeXing2"].ToString());
                        decimal GwcId = decimal.Parse(drGwc["GwcId"].ToString());
                        string ProName = drGwc["ProName"].ToString();
                        #region 有无作废

                        bool ProInvalid = bool.Parse(drGwc["ProInvalid"].ToString());
                        if (ProInvalid)
                        {

                            listDel.Add(drGwc);
                            continue;
                        }


                        #endregion


                        #region 下架

                        int Status = int.Parse(drGwc["Status"].ToString());

                        if (Status < 0)
                        {
                            listDel.Add(drGwc);
                            continue;
                        }


                        #endregion

                        #region 改变了商品的特性
                        if (ProTeXing != ProTeXing2)
                        {
                            listDel.Add(drGwc);
                            continue;
                        }

                        #endregion

                        #region 确认暂估值


                        decimal Quantity = decimal.Parse(drGwc["Quantity"].ToString());
                        decimal ZgProNum = decimal.Parse(drGwc["ZgProNum"].ToString());


                        if (ZgProNum <= 0)
                        {
                            //直接删除,这是错误数据
                            listDel.Add(drGwc);
                            continue;
                        }
                        else
                        {

                            if (Quantity > ZgProNum)
                            {
                                //改成暂估值
                                listNum.Add(drGwc);
                            }

                        }

                        #endregion






                    }
                }
                else
                {

                }  //
                #endregion
            }




            if (listDel.Count > 0)
            {


                s.Append(" delete from dbo.Gwc where GwcId in (");



                foreach (DataRow dr in listDel)
                {

                    s.Append(dr["GwcId"]);
                    s.Append(",");
                    dr.Delete();

                }
                s.Append("0) ");
            }
            if (listNum.Count > 0)
            {




                foreach (DataRow dr in listNum)
                {

                    s.Append(" update  dbo.Gwc set Quantity=" + dr["ZgProNum"].ToString() + " where GwcId =" + dr["GwcId"] + " ");



                }

            }

            dtGwc.AcceptChanges();
            DAL.DalComm.ExReInt(s.ToString());
            ReDict.Add("list", JsonHelper.ToJson(dtGwc));
            try
            {
                ReDict.Add("listDel", JsonHelper.ToJson(listDel));
            }
            catch (Exception)
            {


            }

            ReDict.Add("listNum", JsonHelper.ToJson(listNum));
            ReTrue();
        }


        private void TodayQianDao()
        {

            BLL.MemberBLL mbll = new BLL.MemberBLL();
            decimal MemberId = mbll.GetCurrentDecimalMemberId();
            bool IsQianDao = mbll.TodayQianDao(MemberId);
            ReDict2.Add("b", IsQianDao.ToString());
            ReDict2.Add("date", DateTime.Now.ToString("yyyy年MM月dd日"));
            ReTrue();
        }
        private void SendQianDao()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();

            decimal MemberId = mbll.GetCurrentDecimalMemberId();
            if (MemberId == 0)
            {
                throw new Exception("签到必须登录!");
            }
            int Days = 0;
            decimal GetJiFen = 0;
            mbll.SendQianDao(MemberId, ref Days, ref GetJiFen);
            ReDict2.Add("Days", Days.ToString());
            ReDict2.Add("GetJiFen", GetJiFen.ToString());
            ReTrue();

        }


        void PhoneChangePwd()
        {
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("无法定位MerId");
            }

            string Phone = ReStr("Phone", "");
            if (Phone.Trim() == "")
            {
                throw new Exception("不是手机号");
            }
            string PhoneYzm = ReStr("PhoneYzm", "");
            if (PhoneYzm.Trim() == "")
            {
                throw new Exception("没输验证码");
            }
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            bool b = mbll.DuanXinYanZheng(MerId, Phone, PhoneYzm, 0);
            if (b)
            {

                string pwd1 = ReStr("pwd1", "").Trim();
                string pwd2 = ReStr("pwd2", "").Trim();
                if (pwd1 != pwd2)
                {
                    throw new Exception("两次输入密码不相等!");
                }
                mbll.ChangePwdByPhone(pwd1, Phone, MerId);


                DataSet dsMember = mbll.Login1(Phone, pwd1, MerId, 0);

                ReDict.Add("CurrentMember", JsonHelper.ToJsonNo1(dsMember));



                ReTrue();   //成功修改!
            }
            else
            {
                throw new Exception("验证码错误,请尝试重新获取!");
            }

        }

        private void DuanXinYanZheng()
        {
            decimal StMsgTypeId = ReDecimal("StMsgTypeId", 0);
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("无法定位MerId");
            }

            string Phone = ReStr("Phone", "");
            if (Phone.Trim() == "")
            {
                throw new Exception("不是手机号");
            }
            string PhoneYzm = ReStr("PhoneYzm", "");
            if (PhoneYzm.Trim() == "")
            {
                throw new Exception("没输验证码");
            }
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            bool b = mbll.DuanXinYanZheng(MerId, Phone, PhoneYzm, StMsgTypeId);
            if (b)
            {
                ReTrue();
            }
            else
            {

                throw new Exception("验证码错误,请尝试重新获取!");
            }
        }



        private void ChangeMemberPic()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();

            BLL.ImageBLL imgBll = new BLL.ImageBLL();
            string ImgId = ReStr("ImgId");
            decimal MemberId = ReDecimal("MemberId", mbll.GetCurrentDecimalMemberId());
            string yImgId = DAL.DalComm.ExStr(" select PicImgId from dbo.Member with(nolock) where MemberId='" + MemberId + "' ");

            if (yImgId.Length < 10)
            {
                //如果ID不是上传得来, 不是sys就是空, 那还是不要管他了.

            }
            else
            {
                imgBll.ClearImg(yImgId);
            }






            mbll.ChangeMemberPic(MemberId, ImgId);

            ReTrue();

        }

        private void GetMyMemberInfo()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            string s = ReStr("s", "");
            decimal MyMemberId = 0;
            if (s == "app")
            {
                MyMemberId = ReDecimal("MemberId", 0);
            }
            else
            {
                if (!mbll.MemberLogin())
                {
                    throw new Exception("用户没有登录!");
                }
                MyMemberId = decimal.Parse(mbll.GetCurrentMemberId());
            }


            if (MyMemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }
            DataSet ds = mbll.GetMemberInfo(MyMemberId);
            DataTable dtMemberInfo = ds.Tables[0];
            DataTable dtSkill = ds.Tables[1];
            ReDict.Add("MemberInfo", JsonHelper.ToJsonNo1(dtMemberInfo));
            ReDict.Add("Skill", JsonHelper.ToJson(dtSkill));
            ReTrue();
        }

        private void InvalidAddress()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            decimal AddressId = ReDecimal("AddressId", 0);
            bool Invaid = ReBool("Invalid", true);
            mbll.InvalidAddress(AddressId, Invaid);
            ReDict2.Add("AddressId", AddressId.ToString());
            ReTrue();
        }

        private void ToDefault()
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            decimal AddressId = ReDecimal("AddressId", 0);
            mbll.ToDefault(AddressId);
            ReTrue();
        }

        private void GetAddressInfo()
        {
            decimal AddressId = ReDecimal("AddressId", 0);

            BLL.MemberBLL mbll = new BLL.MemberBLL();
            DataSet ds = mbll.GetAddressInfo(AddressId);
            ReDict.Add("AddressInfo", JsonHelper.ToJsonNo1(ds));
            ReTrue();
        }

        private void SaveAddress()
        {
            Model.AddressInfoModel model = new AddressInfoModel();
            model.SiteId = ReDecimal("SiteId", 0);
            if (model.SiteId == 0)
            {
                throw new Exception("SiteId不能为0!");
            }
            model.LastTime = DateTime.Now;
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                model.AddressId = ReDecimal("AddressId", 0);
                model.MemberId = ReDecimal("MemberId", mbll.GetCurrentDecimalMemberId()); //这个必须放在GetModel之前
                if (model.AddressId == 0)
                {

                }
                else
                {
                    model = mbll.GetAddressModel(model.AddressId);
                }
                model.ContactName = ReStr("ContactName", "");
                if (model.ContactName == "")
                {
                    throw new Exception("收货人姓名不能为空!");
                }
                model.Invalid = ReBool("Invalid", false);

                model.Memo = ReStr("Memo", "");
                model.OrderNo = ReInt("OrderNo", 1);
                if (model.Memo.Trim() == "")
                {
                    throw new Exception("详细地址不能为空");
                }
                model.IsDefault = ReBool("IsDefault", false);
                if (model.IsDefault)
                {

                    mbll.ClearDefaultAddress(model.MemberId);
                }

                model.Tel = ReStr("Tel", "");
                if (model.Tel == "")
                {
                    throw new Exception("电话号码不能为空!");

                }

                if (model.Tel.Length < 7)
                {
                    throw new Exception("请务必将电话号码填写正确!");
                }

                model.TownId = ReDecimal("TownId", 0);
                if (model.TownId == 0)
                {

                    throw new Exception("城镇不明确!");
                }



                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            mbll.SaveAddress(model);

            ReTrue();


        }
        public void GetAddressList()
        {
            StringBuilder s = new StringBuilder();
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            decimal MemberId = ReDecimal("MemberId", mbll.GetCurrentDecimalMemberId());
            int CurrentPage = ReInt("CurrentPage", 1);
            string SiteId = ReStr("SiteId", "");
            bool Invalid = ReBool("Invalid", false);
            s.Append(" MemberId='" + MemberId + "' ");
            s.Append(" and Invalid='" + Invalid + "' ");
            if (SiteId != "")
            {
                s.Append(" and SiteId='" + SiteId + "' ");
            }


            s.Append(" order by IsDefault desc ");
            DataSet ds = mbll.GetAddressPageList(s.ToString(), CurrentPage, 20, "*");
            RePage(ds);

        }
        private void CkPwd()
        {

            string Pwd = ReStr("Pwd", "");

            if (Pwd == "")
            {
                throw new Exception("非空!");
            }

            Common.Validator.CkPwd(Pwd);
            ReTrue();


        }

        private void NotHasPhone()
        {
            string Phone = ReStr("Phone", "");
            decimal MerId = ReDecimal("MerId", 0);

            if (Phone == "")
            {
                throw new Exception("非空!");
            }

            if (MerId == 0)
            {
                throw new Exception("MerId异常!");
            }

            if (!Common.Validator.IsMobile(Phone))
            {

                throw new Exception("不是合法的手机号码!");
            }

            StringBuilder s = new StringBuilder();

            s.Append(" select MemberId from dbo.Member where  ");
            s.Append(" (Phone='" + Phone + "' ) ");
            s.Append(" and MerId='" + MerId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtMember = ds.Tables[0];
            int num = dtMember.Rows.Count;
            if (num > 1)
            {
                throw new Exception("此号已经被注册,忘记密码请点击找回!");
            }


            ReTrue();

        }

        private void HasPhoneLoginStr()
        {
            string LoginStr = ReStr("LoginStr", "");
            decimal MerId = ReDecimal("MerId", 0);

            if (LoginStr == "")
            {
                throw new Exception("非空!");
            }

            if (MerId == 0)
            {
                throw new Exception("MerId异常!");
            }

            StringBuilder s = new StringBuilder();

            s.Append(" select * from dbo.Member where  ");
            s.Append(" (Phone='" + LoginStr + "' or NickName='" + LoginStr + "') ");
            s.Append(" and MerId='" + MerId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtMember = ds.Tables[0];
            int num = dtMember.Rows.Count;
            if (num < 1)
            {
                throw new Exception("手机或昵称不存在!");
            }
            if (num != 1)
            {
                throw new Exception("数据异常,请联系技术支持!");
            }

            DataRow drMember = dtMember.Rows[0];
            int Status = int.Parse(drMember["Status"].ToString());
            if (Status == -1)
            {
                throw new Exception("您已经被禁止登陆!");
            }
            bool Invalid = bool.Parse(drMember["Invalid"].ToString());
            if (Invalid)
            {

                throw new Exception("用户已经被作废");
            }
            ReTrue();


        }


        private void PhoneZhuCe()
        {

            decimal MerId = ReDecimal("MerId", 0);
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            decimal ExtMemberId = ReDecimal("ExtMemberId", 0);
            DataSet dsMember;
            bool b;
            //开始验证
            if (MerId == 0)
            {

                throw new Exception("MerId不能为0!");
            }



            string Phone = ReStr("Phone", "");

            if (Phone == "")
            {

                throw new Exception("手机号码不能为空");
            }
            b = Common.Validator.IsMobile(Phone);
            if (!b)
            {
                throw new Exception("" + Phone + "不是合法的手机号码!");
            }
            int MemberInt = mbll.MemberExInt(" Phone= '" + Phone + "' and MerId='" + MerId + "' ");
            if (MemberInt > 0)
            {
                throw new Exception("非常抱歉,该手机号" + Phone + "已被注册, 一个手机号只能注册一次, 如果忘记密码请点击找回密码 .");
            }
            string Pwd = ReStr("Pwd");
            string Pwd2 = ReStr("Pwd2");
            if (Pwd != Pwd2)
            {
                throw new Exception("两次输入的密码不相同!");
            }

            string PhoneYzm = ReStr("PhoneYzm", "");
            if (PhoneYzm == "")
            {
                throw new Exception("手机验证码不能为空!");
            }


            StringBuilder s = new StringBuilder();
            int GetJiFen = 0;
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                DateTime date2 = DateTime.Now;
                DateTime date1 = date2.AddHours(-24);
                s.Append(" select count(0) from DBLOG.dbo.StMsg with(nolock) where ");

                s.Append(" MerId='" + MerId + "' and PhoneNo='" + Phone + "' ");
                s.Append(" and Invalid = 0 ");
                s.Append(" and StMsgTypeId = 1 ");
                s.Append(" and CreateTime  BETWEEN '" + date1 + "' AND '" + date2 + "' ");
                s.Append(" and ReKey = '" + PhoneYzm + "' ");

                int i = DAL.DalComm.ExInt(s.ToString());

                if (i <= 0)
                {
                    if (PhoneYzm == MerId.ToString())
                    {

                    }
                    else
                    {
                        throw new Exception("验证码不正确请重发!");
                    }


                }
                //开始注册用户
                Model.MemberModel model = new MemberModel();
                model.Invalid = false;
                model.LastTime = DateTime.Now;
                model.Phone = Phone;
                model.Pwd = Pwd;
                model.Status = 0;
                model.MerId = MerId;
                model.CreateTime = DateTime.Now;

                mbll.ZhuCe(model, ref GetJiFen);  //注册积分赋值

                //作废注册信息
                s.Clear();
                s.Append(" update DBLOG.dbo.StMsg  set Invalid =1 where   ");
                s.Append(" MerId='" + MerId + "' and PhoneNo='" + Phone + "' ");
                s.Append(" and Invalid = 0 ");
                s.Append(" and StMsgTypeId = 1 ");
                s.Append(" and CreateTime  BETWEEN '" + date1 + "' AND '" + date2 + "' ");
                s.Append(" and ReKey = '" + PhoneYzm + "' ");


                s.Append(" UPDATE  dbo.Member SET ExtMemberId='" + ExtMemberId + "' WHERE MemberId='" + model.MemberId + "' ");

                DAL.DalComm.ExReInt(s.ToString());
                mbll.Login1(model.Phone, model.Pwd, model.MerId, 1);

                dsMember = DAL.DalComm.BackData(" select * from dbo.Member  where MemberId='" + model.MemberId + "' ");
                #region 事务关闭

                transactionScope.Complete();


            }


            #endregion
            ReDict.Add("m", JsonHelper.ToJsonNo1(dsMember));
            ReDict2.Add("GetJiFen", GetJiFen.ToString());
            ReTrue();
        }


        private void Login1()
        {

            string LoginStr = ReStr("LoginStr", "");
            string Pwd = ReStr("Pwd", "");
            decimal MerId = ReDecimal("MerId", 0);
            int LoginDay = ReInt("LoginDay", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            if (LoginStr == "")
            {
                throw new Exception("登录名不能为空!");
            }

            if (Pwd == "")
            {
                throw new Exception("密码不能为空!");
            }


            BLL.MemberBLL bll = new BLL.MemberBLL();
            DataSet dsMember = bll.Login1(LoginStr, Pwd, MerId, LoginDay);

            ReDict.Add("CurrentMember", JsonHelper.ToJsonNo1(dsMember));

            ReTrue();
        }

        private void SearchChouJiangList()
        {
            string inputStr = ReStr("inputStr", "");
            decimal WxPtId = ReDecimal("WxPtId", 0);
            if (WxPtId == 0)
            {
                throw new Exception("当前没有指定要编辑的微信平台?");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            if (inputStr != "")
            {
                s.Append(" ChouJiangName like '%" + inputStr + "%'  ");

            }

            s.Append(" and WxPtId='" + WxPtId + "' ");

            s.Append(" order by createTime desc ");
            BLL.MemberBLL bll = new BLL.MemberBLL();
            DataSet ds = bll.SearchChouJiangList(s.ToString(), ReInt("CurrentPage", 1), 20, "*");
            RePage(ds);

        }

        private void SaveChouJiangInfo()
        {
            Model.ChouJiangModel model = new ChouJiangModel();
            model.ChouJiangId = ReDecimal("ChouJiangId", 0);
            model.ChouJiangName = ReStr("ChouJiangName", "");

            model.BgTime = ReTime("BgTime");
            model.EndTime = ReTime("EndTime");
            model.LimitNum = ReInt("LimitNum", 1);
            model.WxPtId = ReDecimal("WxPtId", 0);
            model.ChouJiangTypeId = ReInt("ChouJiangTypeId");
            model.ChouJiangClassId = ReInt("ChouJiangClassId", 1);
            model.Memo = ReStr("Memo");
            model.Invalid = ReBool("Invalid", false);
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            mbll.SaveChouJiang(model);
            ReTrue();
        }

        private void SearchMemberList()
        {
            Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();
            string MemberCode = ReStr("MemberCode", "");
            string MemberName = ReStr("MemberName", "");
            decimal MemberId = ReDecimal("MemberId", 0);
            string Phone = ReStr("Phone", "");

            int CurrentPage = ReInt("CurrentPage", 1);
            int PageSize = ReInt("PageSize", 30);
            string cols = ReStr("cols", " * ");
            bool HasPhone = ReBool("HasPhone", true);

            decimal MerId = ReDecimal("MerId", cm.CurrentMerId);
            string Order = ReStr("Order", "");

            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");

            if (MemberId != 0)
            {
                s.Append(" and MemberId=" + MemberId + " ");
            }
            if (MemberCode != "")
            {
                s.Append(" and MemberCode like '%" + MemberCode + "%' ");
            }

            if (MerId != 0)
            {
                s.Append(" and MerId='" + MerId + "' ");

            }
            if (MemberName != "")
            {
                s.Append(" and RealName like '%" + MemberName + "%' or NickName like '%" + MemberName + "%' ");
            }

            if (Phone != "")
            {
                s.Append(" and Phone like '%" + Phone + "%' ");
            }

            if (HasPhone)
            {
                s.Append(" and Phone <> '' and Phone is not null ");
            }



            if (Order == "")
            {
                Order = (" LastTime desc ");
            }


            BLL.MemberBLL mbll = new BLL.MemberBLL();
            DataSet ds = mbll.SearchMemberList(s.ToString(), Order, CurrentPage, PageSize, cols);
            RePage2(ds);
        }




        private void SaveMyMember()
        {

            Model.CurrentMember cmb = BLL.MemberBLL.CurrentMember();
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            Model.MemberModel model = new MemberModel();
            model.MemberId = ReDecimal("MemberId", mbll.GetCurrentDecimalMemberId());
            if (model.MemberId == 0)
            {
                throw new Exception("用户MemberId暂时不能为0");
            }
            else
            {
                model = mbll.GetMemberModel(model.MemberId);
            }
            model.NickName = ReStr("NickName", "");
            model.RealName = ReStr("RealName", "");
            model.Email = ReStr("Email", "");
            model.Invalid = ReBool("Invalid", false);
            model.MemberName = ReStr("MemberName", "");
            //model.SfzImg1 = ReStr("", "");
            //model.SfzImg2 = ReStr("", "");
            //model.SfzNo = ReStr("", "");
            model.Phone = ReStr("Phone", "");
            model.Birthday = ReTime("Birthday", DateTime.Parse("1990-01-01"));
            model.MerId = ReDecimal("MerId", model.MerId);
            model.Status = ReInt("Status", 0);
            model.Sex = ReStr("Sex", "未知");
            mbll.SaveMyMember(model);
            ReTrue();

        }
    }
}
