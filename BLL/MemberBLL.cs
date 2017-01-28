using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Common;
using System.Web;
using System.Transactions;
using Newtonsoft.Json.Linq;
using io.rong;
using System.Web.UI;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace BLL
{
    public class MemberBLL
    {

        #region 用户日志



        public void SaveMemberLog(BsonDocument b)
        {



            b["CreateTime"] = DateTime.Now;
            b["MemberId"] = b["MemberId"].ToString();
         
            DAL.Mongo.Insert(b, "MemberLog", "log");


            BsonDocument mb = new BsonDocument();
            mb["_id"] = b["MemberId"];

            mb["MemberId"]= b["MemberId"];
            mb["LastTime"] = b["CreateTime"];

            switch (b["MemberLogTypeId"].ToInt32())
            {

                case (int)Common.Dict.MemberLogType.通过实名认证:
                    mb["AcceptAuthenticationTime"] = b["CreateTime"];
                    DAL.Mongo.Update(mb, "Member", "uzor", true);

                    break;

                case (int)Common.Dict.MemberLogType.通过技能认证:
                    mb["AcceptProcessTime"] = b["CreateTime"];
                    DAL.Mongo.Update(mb, "Member", "uzor", true);

                    break;

                case (int)Common.Dict.MemberLogType.用户注册:
                    mb["RegistrationTime"] = b["CreateTime"];
                    DAL.Mongo.Update(mb, "Member", "uzor", true);

                    break;
                default:
            
                    break;
            }
  
        //    DAL.Mongo.Update(mb, "Member", "uzor", true);


        }



        #endregion

        #region 工种/技能




        public void SaveOneSkill(Model.MemberVsSkillModel model)
        {

            if (model.MemberId == 0)
            {
                throw new Exception("用户编号不能为空!");
            }

            if (model.SkillId == 0)
            {
                throw new Exception("技能编号不能为空!");
            }

            DAL.MemberVsSkillDAL dal = new DAL.MemberVsSkillDAL();
            dal.DeleteList(" MemberId=" + model.MemberId + " ");

            dal.Add(model);

        }

        #endregion

        #region 团队管理


        public void AddTeamMember(string Phone, string yzm, decimal TeamMemberId)
        {


            DataSet ds;
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion



                if (TeamMemberId == 0)
                {

                    throw new Exception("团队创始人不能为空!");
                }




                DAL.MemberDAL dal = new DAL.MemberDAL();
                StringBuilder s = new StringBuilder();
                s.Append(" select top 1  * from MemberView where Phone='" + Phone + "'  ");

                s.Append(" SELECT * FROM DBLOG.dbo.StMsg WHERE PhoneNo='" + Phone + "' AND ReKey='" + yzm + "'  AND CreateTime >'" + DateTime.Now.AddMinutes(-30) + "' ");
                s.Append("  SELECT TeamId,TeamLvId FROM dbo.Member where MemberId= " + TeamMemberId + " ");
                s.Append(" SELECT * FROM dbo.Team WHERE TeamId =(SELECT top 1 TeamId FROM  dbo.Member WHERE MemberId=" + TeamMemberId + ")  ");
                ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];
                DataTable dtYzm = ds.Tables[1];
                DataTable dtTeamMember = ds.Tables[2];
                DataTable dtTeam = ds.Tables[3];
                if (dtTeamMember.Rows.Count != 1)
                {
                    throw new Exception("团队负责人没有找到!");
                }
                if (dtTeam.Rows.Count != 1)
                {
                    throw new Exception("团队没有找到!");
                }


                if (dtYzm.Rows.Count == 0)
                {
                    if (yzm == BLL.StaticBLL.MerOneConfig(1999, "MaxYzm", "光芒神剑"))
                    {

                    }
                    else
                    {
                        throw new Exception("验证码不正确!");
                    }


                }
                Model.MemberModel model = new MemberModel();
                if (dt.Rows.Count == 0)
                {




                    model.MerId = 1999;
                    model.Phone = Phone;
                    int i = 0;
                    ZhuCe(model, ref i);  //只取得MemberId






                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    model.MemberId = decimal.Parse(dr["MemberId"].ToString());  //只取得MemberId
                    //   return ds;
                }



                #region 准备添加

                DataRow drTeamMember = dtTeamMember.Rows[0];

                AddTeamMember(model.MemberId, decimal.Parse(drTeamMember["TeamId"].ToString()), 10);


                #endregion




                #region 事务关闭

                transactionScope.Complete();
                #endregion

            }




        }
        /// <summary>
        /// 将用户添加到团队中,该用户一定不是创始人!
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="TeamId"></param>
        /// <param name="TeamLvId">10为普通成员</param>
        public void AddTeamMember(decimal MemberId, decimal TeamId, int TeamLvId)
        {




            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion







                StringBuilder s = new StringBuilder();

                s.Append("  SELECT TeamLvId,TeamId FROM dbo.Member where MemberId=" + MemberId + "  "); //查询被添加人的情况

                s.Append(" SELECT MemberId,TeamLvId FROM dbo.Member WHERE TeamId =" + TeamId + " ");  //我的团队里目前的情况

                DataSet ds = DAL.DalComm.BackData(s.ToString());


                DataTable dtMember = ds.Tables[0];
                DataRow dr = dtMember.Rows[0];
                decimal MemberTeamId = decimal.Parse(dr["TeamId"].ToString());
                decimal MemberTeamLvId = decimal.Parse(dr["TeamLvId"].ToString());



                if (MemberTeamId > 0)
                {
                    if (MemberTeamId == TeamId)
                    {
                        throw new Exception("该用户已在团队!");
                    }
                    //如果用户已经有了团队
                    int i = DAL.DalComm.ExInt("  SELECT COUNT(0) FROM dbo.Member WHERE TeamId=" + MemberTeamId + " ");  //被添加人的团队目前有多少人, 如果被添加人没有团队, 那么应该为0

                    if (i > 1)
                    {
                        throw new Exception("该成员已经加入其他团队，无法邀请。");
                    }



                }


                DataTable dtMember2 = ds.Tables[1];

                int TeamMaxNum = int.Parse(BLL.StaticBLL.MerOneConfig(1999, "TeamMaxNum", "0"));

                if (dtMember2.Rows.Count >= TeamMaxNum)
                {
                    throw new Exception("团队成员不能大于" + TeamMaxNum + "人!");
                }



                if (dtMember2.Rows.Count > 0)
                {


                    int i = 0;
                    foreach (DataRow drMember2 in dtMember2.Rows)
                    {
                        int TeamLvId2 = int.Parse(drMember2["TeamLvId"].ToString());

                        if (TeamLvId2 > 0)
                        {
                            i = i + 1;
                        }
                    }
                    if (i == 0)
                    {
                        throw new Exception("该团队没有创始人无法加入!");
                    }

                }
                else
                {

                }


                DAL.DalComm.ExReInt(" UPDATE dbo.Member SET TeamLvId=10, TeamId=" + TeamId + " WHERE MemberId=" + MemberId + "  ");



                CountTeam(TeamId);
                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion

        }


        public void CountTeam(decimal TeamId)
        {


            StringBuilder s = new StringBuilder();

            s.Append(" DECLARE @TeamId AS DECIMAL =" + TeamId + " ");
            s.Append(" DECLARE @TeamPlaces AS INT =( SELECT COUNT(0) FROM dbo.Member WHERE TeamId=@TeamId AND ProcessLvStatusId>=20 ) ");
            s.Append(" UPDATE  dbo.Team  SET	TeamPlaces=@TeamPlaces WHERE TeamId=@TeamId ");



            DAL.DalComm.ExReInt(s.ToString());

        }
        public void CountTeamByMemberId(decimal MemberId)
        {
            decimal TeamId = DAL.DalComm.ExDecimal(" SELECT TeamId FROM dbo.Member WHERE MemberId=" + MemberId + " ");


            if (TeamId != 0)
            {
                CountTeam(TeamId);
            }
            else
            {
                //团队是0那就不用统计了
            }




        }


        public decimal AddMyTeam(decimal MemberId)
        {


            decimal TeamId = 0;

            if (MemberId == 0)
            {
                throw new Exception("用户编号不能为空!");
            }



            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadCommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                StringBuilder s = new StringBuilder();
                s.Append(" SELECT TeamId,TeamLvId,Phone FROM dbo.Member where MemberId=" + MemberId + " ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("没有找到用户!");
                }
                DataRow dr = dt.Rows[0];
                decimal MyTeamId = decimal.Parse(dr["TeamId"].ToString());
                int MyTeamLvId = int.Parse(dr["TeamLvId"].ToString());


                if (MyTeamId != 0 || MyTeamLvId != 0)
                {
                    throw new Exception("您已经在团队中，无法创建新团队");

                }

                Model.TeamModel model = new TeamModel();
                model.TeamName = "";
                model.TeamPlaces = 1;
                DAL.TeamDAL dal = new DAL.TeamDAL();

                dal.Add(model);

                if (model.TeamId <= 0)
                {
                    throw new Exception("团队创建不成功!");
                }


                s.Clear();
                s.Append(" UPDATE dbo.Member SET TeamLvId=100, TeamId=" + model.TeamId + " WHERE MemberId=" + MemberId + " ");

                DAL.DalComm.ExReInt(s.ToString());  //设置我为团队创始人



                TeamId = model.TeamId;


                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion

            return TeamId;


        }
        #endregion

        #region 余额明细账

        public void MemberAmountChange(Model.MemberAmountDetailModel model)
        {

            if (model.MemberId == 0)
            {
                throw new Exception("用户编号不能为0!");
            }

            StringBuilder s = new StringBuilder();
            s.Append("  DECLARE @MemberId AS DECIMAL =" + model.MemberId + "  ");
            s.Append("   DECLARE @ChangeAmount AS DECIMAL =" + model.ChangeAmount + " ");
            s.Append("   DECLARE @OldAmount AS DECIMAL =( SELECT Amount FROM dbo.Member WHERE MemberId=@MemberId)  ");
            s.Append("  DECLARE @NewAmount AS DECIMAL =@OldAmount+ @ChangeAmount ");
            s.Append("  INSERT INTO dbo.MemberAmountDetail  ");
            s.Append("  ( OldAmount , ");
            s.Append("  NewAmount , ");
            s.Append("  MemberId , ");
            s.Append("  UserId , ");
            s.Append("  ChangeAmount , ");
            s.Append("  CreateTime , ");
            s.Append("  ReKey , ");
            s.Append("  ReKey2 , ");
            s.Append("  Memo , ");
            s.Append("   MemberAmountChangeTypeId) ");
            s.Append("  VALUES  ( @OldAmount ,   ");
            s.Append("  @NewAmount , ");
            s.Append("  @MemberId ,  ");
            s.Append("  '' ,  ");
            s.Append("  @ChangeAmount , ");
            s.Append("  '" + DateTime.Now + "' , ");
            s.Append("  '" + model.ReKey + "' ,  ");
            s.Append("  '" + model.ReKey2 + "' ,  ");
            s.Append("  '" + model.Memo + "' ,  ");
            s.Append("  " + model.MemberAmountChangeTypeId + "   ");
            s.Append("  ) ");
            s.Append("  UPDATE dbo.Member SET Amount=@NewAmount,OldAmount=@OldAmount WHERE MemberId=@MemberId ");

            if (model.MemberAmountChangeTypeId == 10)
            {



                //如果是订单结算,那就加入历史记录


            }


            // s.Append("  SELECT @ChangeAmount,@OldAmount,@NewAmount ");
            s.Append("   ");
            s.Append("   ");
            DAL.DalComm.ExInt(s.ToString());


        }
        #endregion

        #region 客服与IM

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RongUserId"></param>
        /// <param name="GroupInfo">格式group[groupid10001]=groupname1001</param>
        /// <param name="MerId"></param>
        public void SyncGroup(string RongUserId, Dictionary<string, string> GroupInfo, decimal MerId)
        {

            string[] GInfo = { };
            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfigCache(MerId, 2000);
            if (GroupInfo.Count == 0)
            {

            }
            else
            {
                int i = 0;
                foreach (KeyValuePair<string, string> g in GroupInfo)
                {

                    GInfo[i] = "group[" + g.Key + "]=" + g.Value + "";

                    RongCloudServer.JoinGroup(MerConfig["RongAppKey"], MerConfig["RongAppKey"], "messager", g.Key, g.Value);   //把messager加入到群组中

                    i++;
                }


            }




            string reJson = RongCloudServer.syncGroup(MerConfig["RongAppKey"], MerConfig["RongAppKey"], RongUserId, GInfo);



            JObject rj = JObject.Parse(reJson);
            if (rj["code"].ToString() != "200")
            {
                throw new Exception("同步群组没有成功!" + rj.ToString() + "");
            }
            else
            {

            }



        }

        public void RemoveMemberTeam(decimal MemberId)
        {

            StringBuilder s = new StringBuilder();



            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                decimal TeamId = DAL.DalComm.ExDecimal(" SELECT TeamId FROM	dbo.Member WHERE MemberId=" + MemberId + " ");




                s.Append("UPDATE dbo.Member SET  TeamId=0,TeamLvId=0 WHERE MemberId=" + MemberId + "");

                DAL.DalComm.ExReInt(s.ToString());


                CountTeam(TeamId);

                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion






        }

        /// <summary>
        /// 获得未读消息
        /// </summary>
        /// <param name="model">查询model, 必须具备ReRole, SendRole,以及发送方和接收方的必备参数</param>
        /// <returns></returns>
        public int GetWeiDuMsgNum(Model.RongMsgLogInfoModel model)
        {

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT COUNT(0) AS 未读消息 FROM YYHD.dbo.RongMsgLogView with(nolock) WHERE 1=1 and  MsgStatus<20 "); //未读的小于20
            switch (model.ReRole)
            {
                case "匿名设备":
                    s.Append(" and TargetId='" + model.ReDeviceId + "' ");
                    break;
                case "用户":
                    s.Append(" and ReMemberId='" + model.ReMemberId + "' ");
                    break;
                case "客服":
                    s.Append(" and ReUserId='" + model.ReUserId + "' ");
                    break;
            }

            switch (model.SendRole)
            {

                case "匿名设备":
                    if (model.RongUserId != "")   //这里加IF是为了如果传空, 那么获得的数量就是当前用户的所有未读信息
                    {
                        s.Append(" and RongUserId='" + model.RongUserId + "' ");
                    }


                    break;
                case "用户":
                    if (model.RongUserId != "")
                    {
                        s.Append(" and RongUserId='" + model.RongUserId + "' ");
                    }
                    break;
                case "客服":
                    if (model.RongUserId != "")
                    {
                        s.Append(" and RongUserId='" + model.RongUserId + "' ");
                    }
                    break;

            }

            return DAL.DalComm.ExInt(s.ToString());

        }

        public void SaveKfJieDai(KfJieDaiModel model)
        {
            DAL.KfJieDaiDAL dal = new DAL.KfJieDaiDAL();
            //首先判断是否已经在接待
            int i = dal.ExInt(" DeviceId='" + model.DeviceId + "' and UserId='" + model.UserId + "'   ");

            if (i > 0)
            {


            }
            else
            {
                //如果不在接待的话, 加入接待
                dal.Add(model);
            }







        }

        /// <summary>
        /// 身份证号是否唯一, JObject["re"]=true|false
        /// </summary>
        /// <param name="SfzNo"></param>
        /// <param name="MemberId">需要排除的用户ID</param>
        /// <returns></returns>
        public JObject OnlySfzNo(string SfzNo, decimal MemberId = 0)
        {
            JObject j = new JObject();

            j["num"] = 0;
            j["re"] = true;

            if (SfzNo == "")
            {
                return j;
            }

            StringBuilder s = new StringBuilder();

            if (!Common.Validator.IsIDCard(SfzNo))
            {
                throw new Exception("<" + SfzNo + ">不是有效的身份证号码");
            }

            s.Append(" SELECT MemberId FROM  dbo.Member WHERE SfzNo='" + SfzNo + "' ");

            if (MemberId > 0)
            {
                s.Append(" and MemberId<>" + MemberId + " ");
            }



            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            j["num"] = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                j["re"] = false;


            }
            else
            {
                j["re"] = true;
            }

            return j;
        }


        /// <summary>
        /// 验证手机号唯一性
        /// </summary>
        /// <param name="SfzNo">手机号码</param>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public JObject OnlyPhone(string Phone, decimal MemberId = 0)
        {

            StringBuilder s = new StringBuilder();
            JObject j = new JObject();
            if (!Common.Validator.IsMobile(Phone))
            {
                throw new Exception("<" + Phone + ">不是有效的手机号码");
            }

            s.Append(" SELECT MemberId FROM  dbo.Member WHERE Phone='" + Phone + "' ");

            if (MemberId > 0)
            {
                s.Append(" and MemberId<>" + MemberId + " ");
            }



            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            j["num"] = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                j["re"] = true;


            }
            else
            {
                j["re"] = false;
            }

            return j;
        }

        /// <summary>
        /// 银联卡的唯一性
        /// </summary>
        /// <param name="BankCardCode"></param>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public JObject OnlyBankCard(string BankCardCode, decimal MemberId = 0)
        {

            StringBuilder s = new StringBuilder();
            JObject j = new JObject();
            if (BankCardCode.Length < 8)
            {
                throw new Exception("请输入正确的银行卡号");
            }

            s.Append(" SELECT MemberId FROM dbo.MemberBankCard WHERE BankCardCode='" + BankCardCode + "'");

            if (MemberId > 0)
            {
                s.Append(" and MemberId<>" + MemberId + " ");
            }



            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            j["num"] = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                j["re"] = true;


            }
            else
            {
                j["re"] = false;
            }

            return j;
        }

        /// <summary>
        /// 保存一个咨询日志
        /// </summary>
        /// <param name="model"></param>
        public void SaveZiXunLogInfo(Model.ZiXunLogInfoModel model)
        {

            DAL.ZiXunLogInfoDAL dal = new DAL.ZiXunLogInfoDAL();
            dal.DeleteList(" ZiXunLogReKey='" + model.ZiXunLogReKey + "' and ZiXunLogTypeId='" + model.ZiXunLogTypeId + "' ");

            model.CreateTime = DateTime.Now;
            dal.Add(model);

        }

        public void ShuaXinKfJieDaiLastTime()
        {

        }


        public string SendRongMsg(Model.RongMsgLogInfoModel model, decimal MerId)
        {

            string reJson = "";


            DAL.TargetIdsDAL TargetDal = new DAL.TargetIdsDAL();
            DAL.RongMsgLogInfoDAL dal = new DAL.RongMsgLogInfoDAL();

            model.ContentText = model.ContentText.Replace("\n", string.Empty).Replace("\r", string.Empty);

            StringBuilder s = new StringBuilder();
            List<string> Targets = new List<string>();
            #region 检测ReRole以及SendRole
            DataSet ds;
            s.Append(" SELECT MemberId,UserId,DeviceId FROM DBMSG.dbo.Device WITH(NOLOCK) WHERE DeviceId='" + model.RongUserId + "'  ");
            switch (model.ReRole)
            {
                case "匿名设备":

                    ds = DAL.DalComm.BackData(s.ToString());
                    Targets.Add(model.ReDeviceId);
                    break;
                case "用户":  //如果是发给用户, 取出用户的所有设备
                            //s.Append(" SELECT DeviceId FROM DBMSG.dbo.Device WITH(NOLOCK) WHERE MemberId=" + model.ReMemberId + " ");
                    ds = DAL.DalComm.BackData(s.ToString());
                    //DataTable dtMember = ds.Tables[1];
                    //if (dtMember.Rows.Count == 0)
                    //{

                    //    throw new Exception("没有MemberId为" + model.ReMemberId + "的用户!");
                    //}
                    Targets.Add(model.ReMemberId.ToString());
                    //foreach (DataRow dr in dtMember.Rows)
                    //{
                    //    Targets.Add(dr["DeviceId"].ToString());
                    //}

                    break;
                case "客服": //如果是发给客服, 取出客服的所有设备
                    s.Append(" SELECT KfJieDaiId from  YYHD.dbo.KfJieDai WITH(NOLOCK) WHERE DeviceId='" + model.RongUserId + "' AND UserId='" + model.ReUserId + "' ");
                    s.Append(" Update YYHD.dbo.KfJieDai set LastTime='" + DateTime.Now + "' WHERE DeviceId='" + model.RongUserId + "' AND UserId='" + model.ReUserId + "'   ");
                    Targets.Add(model.ReUserId);
                    ds = DAL.DalComm.BackData(s.ToString());
                    DataTable dtJieDai = ds.Tables[1];
                    if (dtJieDai.Rows.Count == 0)
                    {
                        //没有接待数
                        Model.KfJieDaiModel KfJieDaiModel = new KfJieDaiModel();
                        KfJieDaiModel.CreateTime = DateTime.Now;
                        KfJieDaiModel.DeviceId = model.RongUserId;
                        KfJieDaiModel.JieDaiStatus = 1;
                        KfJieDaiModel.LastTime = KfJieDaiModel.CreateTime;
                        KfJieDaiModel.UserId = model.ReUserId;
                        DAL.KfJieDaiDAL kfJieDaiDal = new DAL.KfJieDaiDAL();
                        kfJieDaiDal.Add(KfJieDaiModel);
                    }
                    else
                    {
                        //已经接待

                    }


                    break;
                default:
                    throw new Exception("ReRole 不是匿名设备, 不是用户, 不是客服, 那是什么?");

            }

            switch (model.SendRole)//发送方
            {
                case "匿名设备":
                    break;
                case "用户":
                    break;
                case "客服":

                    break;
                default:
                    throw new Exception("SendRole 不是匿名设备, 不是用户, 不是客服, 那是什么?");
                    break;
            }
            #endregion


            //string TargetsStr = "";
            //if (Targets.Count > 0)
            //{


            //    TargetsStr = "[\"" + string.Join("\",\"", Targets) + "\"]";
            //}
            //else
            //{
            //    throw new Exception("发送序列不能为空!");
            //}


            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }

            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfigCache(MerId, 2000);


            model.CreateTime = DateTime.Now;
            DataTable dtSendDevice = ds.Tables[0]; //如果有其他extra内容, 加到这里去即可

            if (dtSendDevice.Rows.Count == 0)
            {
                throw new Exception("该设备没有注册, 请重新登录一下!");
            }

            string extra = JsonHelper.ToJsonNo1(dtSendDevice);



            JObject obj = JObject.Parse(extra);

            JObject objExtra = JObject.Parse(model.Extra);//这是传过来的
            obj["SendPicImgUrl"] = objExtra["SendPicImgUrl"];  //先赋值发送者的头像
            if (objExtra == null)
            {
                objExtra = JObject.Parse("{}");
            }
            if (objExtra["type"] != null)
            {
                obj["type"] = objExtra["type"];
                switch (objExtra["type"].ToString())
                {
                    case "img":
                        obj["ImgUrl"] = objExtra["ImgUrl"];
                        break;
                    default:
                        break;
                }

            }
            else
            {
                obj["type"] = "text";
                obj["ContentText"] = HttpUtility.UrlEncode(model.ContentText);
            }


            if (Common.StringPlus.IsBaoHan(model.ContentText.ToLower(), "proid="))
            {



                string ProId = Common.StringPlus.GetLeftStr(model.ContentText.ToLower(), "proid=", 20);
                if (ProId == null)
                {

                }
                else
                {

                    if (ProId != "")
                    {
                        obj["type"] = "pro";
                        obj["ProJson"] = JObject.Parse(JsonHelper.ToJsonNo1(DAL.DalComm.BackData(" SELECT ProId,ProName,ProductImgUrl,RePrice FROM  CORE.dbo.ProView with(NOLOCK) WHERE ProId='" + ProId + "' "))).ToString();
                        //obj["ProJson"] = "";
                    }
                }

            }






            // model.ContentText = HttpUtility.UrlEncode(model.ContentText);


            model.Extra = obj.ToString().Replace("\n", string.Empty).Replace("\r", string.Empty);//给写入数据用的, 拉取聊天记录中不需要这个,因为已经有了
            dal.Add(model);  //保存聊天记录
            obj["RongMsgLogId"] = model.RongMsgLogId;

            model.Extra = obj.ToString().Replace("\n", string.Empty).Replace("\r", string.Empty);  //给监听用的,这里面包含刚刚写入的RongMsgLogId
            foreach (string TargetId in Targets)
            {

                reJson = RongCloudServer.PublishMessage(MerConfig["RongAppKey"],
                MerConfig["RongAppSecret"],
                model.RongUserId,
                TargetId,
                model.RongMsgLogTypeId, //消息类型
                " {\"content\":\"有新的消息\",\"extra\":" + model.Extra + "}" //消息内容
                , "有新的消息");
                JObject rj = JObject.Parse(reJson);
                if (rj["code"].ToString() != "200")
                {
                    throw new Exception("该信息没有送达!");
                }

                TargetIdsModel targetIdsModel = new TargetIdsModel();
                targetIdsModel.MsgStatus = 0;
                targetIdsModel.RongMsgLogId = model.RongMsgLogId;
                targetIdsModel.TargetId = TargetId;
                TargetDal.Add(targetIdsModel);
            }

            #region 融云消息文档
            //文本消息    RC:TxtMsg   { "content":"hello","extra":"helloExtra"}
            //            content表示文本内容，extra为附加信息(如果开发者自己需要，可以自己在 App 端进行解析)
            //图片消息 RC:ImgMsg   { "content":"ergaqreg", "imageUri":"http://www.demo.com/1.jpg","extra":"helloExtra"}
            //            content表示图片缩略图，以Base64进行Encode，imageUri为图片Url，extra为附加信息(如果开发者自己需要，可以自己在 App 端进行解析)
            //语音消息 RC:VcMsg    { "content":"ergaqreg","duration":3,"extra":"helloExtra"}
            //            content表示语音内容，以Base64进行Encode，Duration表示语音长度，extra为附加信息(如果开发者自己需要，可以自己在 App 端进行解析)
            //图文消息 RC:ImgTextMsg   { "title":"hellotitle","content":"hello","imageUri":"http://www.demo.com/1.jpg","extra":"helloExtra"}
            //            title表示消息的标题，content是消息文本内容，imageUri为图片地址，extra为附加信息(如果开发者自己需要，可以自己在 App 端进行解析)
            //位置消息 RC:LBSMsg   {
            //                "content":"abcxfeadfbdzdik","latitude":24.114,"longitude":334.221,"poi":"北京市朝阳区北苑路北辰泰岳大厦",extra":"helloExtra"}	content表示位置图片缩略图，以Base64进行Encode，latitude表示纬度，longitude表示经度，poi表示位置的poi信息，extra为附加信息(如果开发者自己需要，可以自己在 App 端进行解析)
            //添加联系人消息 RC:ContactNtf   {
            //                    "operation":"op1","sourceUserId":"24","targetUserId":"21","message":"haha",extra":"helloExtra"}	operation 表示操作名，sourceUserId 表示请求者或者响应者的 UserId ，targetUserId 表示被请求者或者被响应者的 UserId ，message 表示请求或者响应消息，如添加理由或拒绝理由，extra 附加信息
            #endregion



            return reJson.ToString();



        }

        public void AuthenticationStatusChange(decimal memberId, int authenticationStatusId)
        {



            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {

                #endregion
                UserBLL ubll = new UserBLL();
                BsonDocument b = new BsonDocument();
                DAL.DalComm.ExReInt(" UPDATE dbo.member SET authenticationStatusId=" + authenticationStatusId + " WHERE memberId=" + memberId + "  ");
                CountTeamByMemberId(memberId);
                BLL.MsgBLL mbll = new MsgBLL();

                switch (authenticationStatusId)
                {

                    case 20:

    
                        b["MemberId"] = memberId;
                        b["Title"] = "用户已通过实名认证!";
                        b["UserId"] = ubll.CurrentUserId();
                        b["MemberLogTypeId"] = (int)Common.Dict.MemberLogType.通过实名认证;
                        b["MemberLogTypeName"] = Common.Dict.MemberLogType.通过实名认证.ToString();
                        mbll.SendMsgToDevice(10, "您已通过实名认证!", "AcceptAuthentication", memberId.ToString(), "messager");
                        break;
                    case 10:
         
                        b["MemberId"] = memberId;
                        b["Title"] = "用户实名身份待认证!";
                        b["UserId"] = ubll.CurrentUserId();
                        b["MemberLogTypeId"] = (int)Common.Dict.MemberLogType.申请实名认证;
                        b["MemberLogTypeName"] = Common.Dict.MemberLogType.申请实名认证.ToString();
                        mbll.SendMsgToDevice(10, "用户实名身份待认证!", "AcceptAuthentication", memberId.ToString(), "messager");
                        break;


                    case 0:
                        b["MemberId"] = memberId;
                        b["Title"] = "用户实名认证状态调整为[未认证]!";
                        b["UserId"] = ubll.CurrentUserId();
                        b["MemberLogTypeId"] = (int)Common.Dict.MemberLogType.未实名认证;
                        b["MemberLogTypeName"] = Common.Dict.MemberLogType.未实名认证.ToString();
                        mbll.SendMsgToDevice(10, "用户实名身份待认证!", "AcceptAuthentication", memberId.ToString(), "messager");
                        break;
                    default:

                        throw new Exception("实名认证状态不能为"+authenticationStatusId+"");
                       
                }
                SaveMemberLog(b);


                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion







        }

        public void ProcessLvStatusChange(decimal MemberId, int ProcessLvStatusId)
        {

            switch (ProcessLvStatusId)


            {
                case 10:
                case 0:

                    DAL.DalComm.ExReInt(" UPDATE dbo.member SET ProcessLvStatusId=" + ProcessLvStatusId + " WHERE MemberId=" + MemberId + "  ");
                    CountTeamByMemberId(MemberId);
                    break;
                case 20:

                    AcceptProcessLv(MemberId);
                    break;
                default:
                    throw new Exception("ProcessLvStatusId不能为" + ProcessLvStatusId + "");
                    break;
            }


        }

        public void AllowSubjectCash(decimal SubjectCashId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();

                s.Append(" SELECT * FROM  dbo.SubjectCash WHERE  SubjectCashId=" + SubjectCashId + " ");

                s.Append(" UPDATE dbo.SubjectCash SET SubjectCashStatusId=20 where SubjectCashId=" + SubjectCashId + " ");

                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("找不到编号为" + SubjectCashId + "的提现请求");
                }
                DataRow dr = dt.Rows[0];

                int SubjectCashStatusId = int.Parse(dr["SubjectCashStatusId"].ToString());

                if (SubjectCashStatusId != 10)
                {
                    throw new Exception("该状态的申请无法进行此操作[状态码" + SubjectCashStatusId + "]");
                }


                BLL.MsgBLL msgBll = new MsgBLL();


                msgBll.SendMsgToDevice(10, "您的[" + dr["Amount"].ToString() + "元]提现请求已经通过, 我们已经完成打款,请注意查收", "AllowSubjectCash", dr["MemberId"].ToString(), "messager");




                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion


        }


        public void RefuseSubjectCash(decimal SubjectCashId)
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();

                s.Append(" SELECT * FROM  dbo.SubjectCash WHERE  SubjectCashId=" + SubjectCashId + " ");

                s.Append(" UPDATE dbo.SubjectCash SET SubjectCashStatusId=-10 where SubjectCashId=" + SubjectCashId + " ");

                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("找不到编号为" + SubjectCashId + "的提现请求");
                }
                DataRow dr = dt.Rows[0];
                int SubjectCashStatusId = int.Parse(dr["SubjectCashStatusId"].ToString());

                if (SubjectCashStatusId != 10)
                {
                    throw new Exception("该状态的订单无法进行此操作[状态码" + SubjectCashStatusId + "]");
                }
                decimal Amount = decimal.Parse(dr["Amount"].ToString());

                BLL.MsgBLL msgBll = new MsgBLL();


                msgBll.SendMsgToDevice(10, "您的[" + dr["Amount"].ToString() + "元]提现请求被拒绝, 相关余额已经回滚,如有疑问请联系我们", "RefuseSubjectCash", dr["MemberId"].ToString(), "messager");



                MemberAmountChange(new MemberAmountDetailModel()
                {
                    ChangeAmount = Amount,
                    MemberId = decimal.Parse(dr["MemberId"].ToString()),
                    MemberAmountChangeTypeId = 100,
                    Memo = "提现被拒绝"

                });

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        public void SaveSubjectCash(SubjectCashModel model)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                if (model.Amount <= 0)
                {
                    throw new Exception("提现金额必须大于0");
                }

                DAL.SubjectCashDAL dal = new DAL.SubjectCashDAL();

                if (model.MemberBankCardId == 0)
                {
                    throw new Exception("未选择提现银行卡?");
                }

                if (model.SubjectCashId == 0)
                {
                    decimal Amount = DAL.DalComm.ExDecimal("SELECT Amount FROM dbo.Member WHERE MemberId=" + model.MemberId + "");

                    if (Amount < model.Amount)
                    {
                        throw new Exception("用户余额[" + Amount + "],不能提现[" + model.Amount + "]");
                    }

                    model.CreateTime = DateTime.Now;
                    model.DoneTime = DateTime.Parse("3000-01-01");
                    model.SubjectCashStatusId = 10;
                    dal.Add(model);


                    MemberAmountChange(new Model.MemberAmountDetailModel()
                    {
                        ChangeAmount = -model.Amount,
                        MemberAmountChangeTypeId = 100,
                        MemberId = model.MemberId,
                        ReKey = model.SubjectCashId.ToString()
                    });




                    BLL.MsgBLL msgBll = new MsgBLL();
                    msgBll.SendMsgToUser("1999001", new MsgTextModel()
                    {
                        CreateTime = DateTime.Now,
                        MsgContent = "用户发起了提现申请,金额[" + model.Amount + "]元,请尽快处理",
                        MsgTitle = "用户发起了提现申请,金额[" + model.Amount + "]元,请尽快处理",
                        MsgType = "SaveSubjectCash",
                        EndTime = DateTime.Now.AddDays(1),
                        Extra = "{MemberId:" + model.MemberId + "}"

                    });



                    msgBll.SendMsgToDevice(10, "我们已经收到您的[" + model.Amount + "元]提现申请,正常情况下将在10个工作日内为您打款,(注意,申请提现余额已经您的余额中扣除),如对此有疑问请联系客服人员", "SaveSubjectCash", "" + model.MemberId + "", "messager");

                }
                else
                {
                    throw new Exception("目前不能修改!");
                    dal.Update(model);
                }
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion


        }



        /// <summary>
        /// 通过技能级别认证
        /// </summary>
        /// <param name="MemberId"></param>
        public void AcceptProcessLv(decimal MemberId)
        {




            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = Common.Tran.isolationLevel(System.Transactions.IsolationLevel.ReadUncommitted);
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion


                int ProcessLvId = DAL.DalComm.ExInt(" SELECT ProcessLvId FROM dbo.Member WHERE MemberId=" + MemberId + " ");


                if (ProcessLvId <= 0)
                {

                    throw new Exception("请首先给该用户设置一个技能级别，并保存后再通过。");
                }




                StringBuilder s = new StringBuilder();
                s.Append("  UPDATE dbo.Member SET ProcessLvStatusId=20 WHERE MemberId=" + MemberId + " ");

                DAL.DalComm.ExReInt(s.ToString());






                CountTeamByMemberId(MemberId);

                MsgBLL msgBll = new MsgBLL();

                msgBll.SendMsgToDevice(10, "恭喜您,您之前提交的技能认证已通过", "AcceptProcessLv", MemberId.ToString(), "");

               


                #region 事务关闭

                transactionScope.Complete();

            }
            #endregion


            BsonDocument b = new BsonDocument();
            b["MemberId"] = MemberId;
            b["Title"] = "用户通过了技能认证";
            b["MemberLogTypeId"] = (int)Common.Dict.MemberLogType.通过技能认证;
            b["MemberLogTypeName"] = Common.Dict.MemberLogType.通过技能认证.ToString();
            SaveMemberLog(b);
        }

        public void SaveTx(SubjectInfoModel model)
        {

            DAL.SubjectInfoDAL dal = new DAL.SubjectInfoDAL();
            if (model.SubjectId == 0)
            {
                model.CreateTime = DateTime.Now;
                model.DoneTime = DateTime.Parse("3000-01-01");
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        public void SaveMemberBankCard(MemberBankCardModel model)
        {
            DAL.MemberBankCardDAL dal = new DAL.MemberBankCardDAL();

            if (model.MemberBankCardId == 0)
            {
                model.CreateTime = DateTime.Now;
                dal.Add(model);

            }
            else
            {
                dal.Update(model);
            }
        }

        /// <summary>
        /// 返回融云token的json字符串
        /// </summary>
        /// <param name="MerId"></param>
        /// <param name="RongUserId"></param>
        /// <param name="RongName"></param>
        /// <param name="RongPortraitUri"></param>
        /// <returns></returns>
        public string GetRongTokenJson(decimal MerId, string RongUserId, string RongName, string RongPortraitUri)
        {
            string[] sa = { "RongAppKey", "RongAppSecret" };

            Dictionary<string, string> MerConfig = StaticBLL.MerConfig(MerId, sa);


            string reStr = io.rong.RongCloudServer.GetToken(MerConfig["RongAppKey"], MerConfig["RongAppSecret"], RongUserId, RongName, RongPortraitUri);

            return reStr;

        }

        /// <summary>
        /// 获取融云token
        /// </summary>
        /// <param name="MerId"></param>
        /// <param name="RongUserId"></param>
        /// <param name="RongName"></param>
        /// <param name="RongPortraitUri"></param>
        /// <returns></returns>
        public string GetRongToken(decimal MerId, string RongUserId, string RongName, string RongPortraitUri)
        {

            string reStr = GetRongTokenJson(MerId, RongUserId, RongName, RongPortraitUri);
            JObject obj = JObject.Parse(reStr);

            string code = obj["code"].ToString();
            if (code != "200")
            {
                throw new Exception("融云返回错误码:code:" + code + "");
            }

            return obj["token"].ToString();


        }




        #endregion

        #region app推送绑定

        public void DeviceBind(Model.DeviceModel model)
        {
            DAL.DeviceDAL dal = new DAL.DeviceDAL();

            int i = dal.ExInt("   DeviceId='" + model.DeviceId + "' ");
            if (i > 0)//如果存在了这个设备那么就修改这个设备的信息
            {
                dal.Update(model);

            }
            else//如果不存在就添加这个设备的信息
            {
                dal.Add(model);
            }





        }


        public Model.DeviceModel GetDeviceModel(string DeviceId)
        {

            DAL.DeviceDAL dal = new DAL.DeviceDAL();
            return dal.GetModel(DeviceId);
        }
        #endregion


        #region 抽奖逻辑


        /// <summary>
        /// 返回抽奖类别html
        /// </summary>
        /// <returns></returns>
        public string SelChouJiangTypeHtml()
        {

            DataSet ds = ChouJiangType();
            StringBuilder w = new StringBuilder();
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    w.Append("<option value='" + dr["ChouJiangTypeId"] + "' >");
                    w.Append(dr["ChouJiangTypeName"]);
                    w.Append("");
                    w.Append("</option>");

                }
            }

            return w.ToString();
        }

        public DataSet ChouJiangType()
        {

            DAL.ChouJiangTypeDAL dal = new DAL.ChouJiangTypeDAL();
            return dal.GetList("");
        }

        public DataSet SearchChouJiangList(string StrWhere, int CurrentPage, int PageSize, string cols)
        {
            DAL.ChouJiangDAL dal = new DAL.ChouJiangDAL();

            DataSet ds = dal.GetPageList(StrWhere, CurrentPage, PageSize, cols);
            return ds;

        }

        public DataSet SearchChouJiangLogList(string StrWhere, int CurrentPage, int PageSize, string cols)
        {

            DAL.ChouJiangLogDAL dal = new DAL.ChouJiangLogDAL();
            DataSet ds = dal.GetPageList(StrWhere, CurrentPage, PageSize, cols);
            return ds;

        }



        public void SaveChouJiang(Model.ChouJiangModel model)
        {
            DAL.ChouJiangDAL dal = new DAL.ChouJiangDAL();

            if (model.ChouJiangId == 0)
            {
                model.CreateTime = DateTime.Now;
                dal.Add(model);
            }
            else
            {
                dal.Update(model);

            }


        }




        #endregion

        #region 会员逻辑



        /// <summary>
        /// 在一个商家的有效用户中, 含有几个此手机号的用户!
        /// </summary>
        /// <param name="MerId"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int HasMemberPhoneNum(decimal MerId, string Phone)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" select count(0) from dbo.Member where Phone='" + Phone + "' and Invalid=0 and MerId=" + MerId + " ");
            var i = DAL.DalComm.ExInt(s.ToString());
            return i;

        }

        public void ChangePwdByPhone(string pwd, string Phone, decimal MerId)
        {
            var i = HasMemberPhoneNum(MerId, Phone);

            if (i > 1)
            {

                throw new Exception("拥有此手机号的用户数量大于1,请联系管理员吧! qq19278765,电话 18678158567");
            }


            if (i == 0)
            {
                throw new Exception("没有用户拥有此手机号" + Phone + "");
            }
            StringBuilder s = new StringBuilder();



            s.Clear();
            s.Append(" Update dbo.Member set Pwd='" + pwd + "',PwdMd5='" + Common.JiaMi.MD5(pwd) + "' where Phone='" + Phone + "' and MerId='" + MerId + "' and Invalid=0  ");
            DAL.DalComm.ExInt(s.ToString());
        }


        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="ImgId"></param>
        public void ChangeMemberPic(decimal MemberId, string ImgId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.Member SET  PicImgId ='" + ImgId + "' WHERE MemberId='" + MemberId + "' ");
            DAL.DalComm.ExReInt(s.ToString());

        }

        public void CheckMember()
        {

            BLL.MemberBLL mbll = new BLL.MemberBLL();
            string WxOpenId = PageInput.ReStr("WxOpenId", "");
            if (WxOpenId == "")
            {

            }
            else
            {
                BLL.MemberBLL.SetMember(WxOpenId, decimal.Parse(Config.GetAppValue("MerId")));
            }
        }


        /// <summary>
        /// 用新分页进行用户查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="Order"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public DataSet SearchMemberList(string strWhere, string Order, int currentpage, int pagesize, string cols)
        {
            DAL.MemberDAL dal = new DAL.MemberDAL();

            return dal.GetPageList(strWhere, Order, currentpage, pagesize, cols);

        }


        /// <summary>
        /// 用旧分页进行数据查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public DataSet SearchMemberList(string strWhere, int currentpage, int pagesize, string cols)
        {
            DAL.MemberDAL dal = new DAL.MemberDAL();

            return dal.GetPageList(strWhere, currentpage, pagesize, cols);

        }

        public void SaveMember(Model.MemberModel model)
        {

            DAL.MemberDAL dal = new DAL.MemberDAL();





            if (model.MemberId <= 0)
            {
                throw new Exception("后台不能新增用户!");
                dal.Add(model);

            }
            else
            {


                #region 判断身份证

                JObject j = OnlySfzNo(model.SfzNo, model.MemberId);
                if ((bool)j["re"])
                {

                }
                else
                {

                    throw new Exception("该身份证号已经有其他用户使用了");
                }
                #endregion





                StringBuilder s = new StringBuilder();
                s.Append(" SELECT COUNT(0) as Num FROM member WHERE Email='" + model.Email + "' AND MemberId<>" + model.MemberId + " ");
                s.Append(" SELECT COUNT(0) as Num FROM member WHERE Phone='" + model.Phone + "' AND MemberId<>" + model.MemberId + " ");
                s.Append(" SELECT COUNT(0) as Num FROM member WHERE NickName='" + model.NickName + "'  AND MemberId<>" + model.MemberId + " ");


                DataSet ds = DAL.DalComm.BackData(s.ToString());

                int i = 0;

                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                i = int.Parse(dr["Num"].ToString());
                if (i > 0)
                {
                    if (model.Email != "")
                    {
                        throw new Exception("邮箱" + model.Email + "已经被其他用户使用了!");
                    }
                }


                dt = ds.Tables[1];
                dr = dt.Rows[0];
                i = int.Parse(dr["Num"].ToString());
                if (i > 0)
                {
                    throw new Exception("手机号" + model.Phone + "已经被其他用户使用了!");
                }

                dt = ds.Tables[2];
                dr = dt.Rows[0];
                i = int.Parse(dr["Num"].ToString());
                if (i > 0)
                {

                    if (model.NickName.Trim() == "")
                    {
                        //如果昵称等于空, 不判断重复
                    }
                    else
                    {
                        throw new Exception("昵称" + model.NickName + "已经被其他用户使用了!");
                    }

                }




                dal.Update(model);
            }

        }

        public void SaveMyMember(Model.MemberModel model)
        {
            DAL.MemberDAL dal = new DAL.MemberDAL();



            if (model.Phone == "1823232123")
            {
                throw new Exception("遇到此错误请立即截屏,并联系管理员,18678158567,qq19278765");
            }



            if (model.MemberId == 0)
            {
                if (model.NickName.Trim() != "")
                {
                    int i = DAL.DalComm.ExInt(" select count(0) from dbo.Member where NickName='" + model.NickName + "' and MerId='" + model.MerId + "'  ");
                    if (i > 0)
                    {
                        throw new Exception("昵称[" + model.NickName + "]已经有人使用了!");
                    }
                }

                if (model.Email.Trim() != "")
                {
                    int i = DAL.DalComm.ExInt(" select count(0) from dbo.Member where Email='" + model.Email + "' and MerId='" + model.MerId + "'  ");
                    if (i > 0)
                    {
                        throw new Exception("邮件[" + model.Email + "]已经有人使用了!");
                    }

                }

                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }

        public Model.MemberModel GetMemberModel(decimal MemberId)
        {
            DAL.MemberDAL dal = new DAL.MemberDAL();
            return dal.GetModel(MemberId);
        }


        /// <summary>
        /// 根据MerId取得可靠的用户, 防止多个公众平台之间的串号行为
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataSet GetMemberInfo(decimal MemberId, decimal MerId)
        {

            StringBuilder s = new StringBuilder();

            s.Append(" SELECT * FROM dbo.MemberView where MemberId= '" + MemberId + "'  and MerId='" + MerId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;


        }


        /// <summary>
        /// 获得我的总积分
        /// </summary>
        /// <returns></returns>
        public int GetMyJiFen()
        {
            DataSet ds = GetMemberInfo();
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0]["MyJiFen"].ToString());
        }



        /// <summary>
        /// 获得我的冻结积分
        /// </summary>
        /// <returns></returns>
        public int GetMyDjJiFen()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" select ISNULL(sum(UseJiFen),0) AS 冻结积分 from  CORE.dbo.DingDanInfo where CreateMember='" + GetCurrentDecimalMemberId() + "' and Status < 110 and Status >= 10  ");
            return DAL.DalComm.ExInt(s.ToString());

        }

        public DataSet GetMemberInfo()
        {
            return GetMemberInfo(GetCurrentDecimalMemberId());
        }

        public DataSet GetMemberInfo(decimal MemberId)
        {

            StringBuilder s = new StringBuilder();

            s.Append(" DECLARE @DjsAmount AS DECIMAL =(   SELECT   ISNULL(SUM(o.CheckQuantity* o.Wages),0) AS  DjsAmount FROM dbo.OrderToWork o WHERE MemberId=" + MemberId + " AND OrderToWorkStatusId BETWEEN 50 AND 60 )");
            s.Append(" DECLARE @MyPlateNum AS INT =(SELECT COUNT(0) FROM dbo.MemberVsPlate WHERE MemberId=" + MemberId + ") ");

            s.Append(" DECLARE @MyFabricNum AS INT =(SELECT COUNT(0) FROM dbo.MemberVsFabric WHERE MemberId=" + MemberId + ") ");

            s.Append(" DECLARE @CardNum AS INT =(SELECT COUNT(0) FROM CORE.dbo.MemberBankCard WHERE MemberId=" + MemberId + ") ");
            s.Append(" DECLARE @MaxLimitTime AS DATETIME= ");
            s.Append(" (SELECT MAX(LimitTime) AS MaxLimitTime FROM dbo.OrderToWork WHERE MemberId = " + MemberId + " AND OrderToWorkStatusId BETWEEN 10 AND 40) ");
            s.Append(" SELECT  m.*,pl.ProcessLvName,pl.ProcessLvTitle,@DjsAmount AS DjsAmount,@CardNum as CardNum, ");

            s.Append(" @MyFabricNum as MyFabricNum ,@MyPlateNum as MyPlateNum ");  //擅长面料,擅长款式的数量

            s.Append(" FROM  dbo.MemberView m LEFT JOIN dbo.ProcessLv pl ON m.ProcessLvId=pl.ProcessLvId ");

            s.Append("  where m.MemberId= '" + MemberId + "'  ");

            s.Append(" SELECT mvs.*, s.SkillName FROM dbo.MemberVsSkill mvs  ");
            s.Append("  INNER JOIN dbo.Skill s ON s.SkillId = mvs.SkillId WHERE mvs.MemberId=" + MemberId + " ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }
        public DataSet GetMemberInfo(string WxOpenId, decimal MerId)
        {

            StringBuilder s = new StringBuilder();

            s.Append(" SELECT * FROM dbo.MemberView where WxOpenId= '" + WxOpenId + "' and MerId='" + MerId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }
        #endregion

        #region 登录注册


        public void CkLogin()
        {
            if (!MemberLogin())
            {
                string url = "";
                try
                {
                    url = HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
                }
                catch
                {


                }
                HttpContext.Current.Response.Redirect("~/Login/Login.aspx?url=" + url + "");

            }

        }

        public bool MemberLogin()
        {

            try
            {

                string MemberId = GetCurrentMemberId();

                if (MemberId.Trim() == "0")
                {
                    return false;
                }

                if (MemberId != null)
                {
                    if (MemberId.Trim() == "")
                    {
                        throw new Exception("");
                    }


                }
                else
                {

                    throw new Exception("");
                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }


        public string GetCurrentMemberId()
        {

            string MemberId = Common.CookieSings.GetCookie("CurrentMemberId");
            MemberId = Common.JiaMi.uncMe(MemberId);
            return MemberId;
        }

        public decimal GetCurrentDecimalMemberId()
        {
            try
            {
                string MemberId = Common.CookieSings.GetCookie("CurrentMemberId");
                MemberId = Common.JiaMi.uncMe(MemberId);
                return decimal.Parse(MemberId);
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public int MemberExInt(string strSql)
        {

            DAL.MemberDAL dal = new DAL.MemberDAL();
            return dal.ExInt(strSql);
        }

        public void ZhuCe(Model.MemberModel model, ref int GetJiFen)
        {




            DAL.MemberDAL dal = new DAL.MemberDAL();
            StringBuilder s = new StringBuilder();
            if (dal.ExInt(" Phone='" + model.Phone + "' ") > 0)
            {

                throw new Exception("此手机号码已被注册!");
            }



            model.Birthday = DateTime.Parse("1900-01-01");
            model.LastTime = DateTime.Now;
            model.PwdMd5 = Common.JiaMi.MD5(model.Pwd);
            model.Sex = "未知";
            model.LastBuyTime = DateTime.Now;
            model.Invalid = false;
            model.CreateTime = DateTime.Now;
            model.MaxOrderPlanningTime = DateTime.Now;
            dal.Add(model);
            try
            {
                Model.JiFenChangeModel ModChange = new JiFenChangeModel();
                int ZhuCeJiFen = int.Parse(BLL.StaticBLL.MerOneConfig(model.MerId, "ZhuCeJiFen", "0"));
                GetJiFen = ZhuCeJiFen;
                ModChange.CreateTime = DateTime.Now;
                ModChange.JiFenChangeMemo = "用户注册奖励:" + ZhuCeJiFen + "点积分";
                ModChange.JiFenChangeNum = ZhuCeJiFen;
                ModChange.JifenChangeTypeId = 11;
                ModChange.MemberId = model.MemberId;
                ModChange.ReKey = model.MemberId.ToString();
                if (ModChange.JiFenChangeNum != 0 && ModChange.MemberId != 0)
                {
                    MerchantBLL mbll = new MerchantBLL();
                    mbll.ChangeMemberJiFen(ModChange);
                }

            }
            catch
            {







            }


            BsonDocument b = new BsonDocument();
            b["MemberId"] = model.MemberId;
            b["Phone"] = model.Phone;
            b["MemberLogTypeId"] =(int) Common.Dict.MemberLogType.用户注册;

            b["MemberLogTypeName"] = Common.Dict.MemberLogType.用户注册;


            SaveMemberLog(b);

        }
        public DataSet YzmLogin(decimal MerId, string PhoneNo, string yzm)
        {
            DataSet ds;
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                DAL.MemberDAL dal = new DAL.MemberDAL();
                StringBuilder s = new StringBuilder();
                s.Append(" select top 1  * from MemberView where Phone='" + PhoneNo + "'  ");

                s.Append(" SELECT * FROM DBLOG.dbo.StMsg WHERE PhoneNo='" + PhoneNo + "' AND ReKey='" + yzm + "'  AND CreateTime >'" + DateTime.Now.AddMinutes(-30) + "' ");
                ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];
                DataTable dtYzm = ds.Tables[1];

                if (dtYzm.Rows.Count == 0)
                {
                    if (yzm == BLL.StaticBLL.MerOneConfig(MerId, "MaxYzm", "光芒神剑"))
                    {

                    }
                    else
                    {
                        throw new Exception("验证码不正确!");
                    }


                }

                if (dt.Rows.Count == 0)
                {



                    Model.MemberModel model = new MemberModel();
                    model.MerId = 1999;
                    model.Phone = PhoneNo;
                    int i = 0;
                    ZhuCe(model, ref i);
                    ds = YzmLogin(MerId, PhoneNo, yzm);





                }
                else
                {


                    //   return ds;
                }




                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            return ds;

        }

        public DataSet Login1(string LoginStr, string Pwd, decimal MerId, int LoginDay)
        {
            DAL.MemberDAL dal = new DAL.MemberDAL();
            StringBuilder s = new StringBuilder();

            s.Append(" select MemberId,Phone,NickName,MemberName,MerId,WxPtId,WxOpenId ,MemberCode,Invalid,Status from CORE.dbo.Member where ");

            s.Append(" ( Phone='" + LoginStr + "' or NickName='" + LoginStr + "' ) ");
            s.Append(" and MerId=" + MerId + " ");
            s.Append(" and  (Pwd='" + Pwd + "' or PwdMd5='" + Common.JiaMi.MD5(Pwd) + "' ) ");
            //s.Append(" and  MerId='" + MerId + "' ");
            s.Append(" and Invalid=0 ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dtMemberInfo = ds.Tables[0];
            if (dtMemberInfo.Rows.Count == 0)
            {
                throw new Exception("找不到用户名或者密码不正确!");
            }
            if (dtMemberInfo.Rows.Count != 1)
            {
                throw new Exception("数据出错! 匹配用户数为'" + dtMemberInfo.Rows.Count + "'!建议您立刻联系技术支持:qq:16248777!");
            }



            DataRow drMember = dtMemberInfo.Rows[0];
            if (int.Parse(drMember["Status"].ToString()) < 0)
            {
                throw new Exception("您的用户已经被禁止登陆!请联系管理员");
            }
            SetMemberByDr(drMember, LoginDay);
            return ds;
        }

        public void LoginOut()
        {
            Common.CookieSings.AddCookieStr("CurrentMemberId", "", -1);
            Common.CookieSings.AddCookieStr("CurrentNickName", "", -1);
            Common.CookieSings.AddCookieStr("CurrentPhone", "", -1);
        }

        #endregion

        #region 我的收货地址


        public void SaveAddress(Model.AddressInfoModel model)
        {

            DAL.AddressInfoDAL dal = new DAL.AddressInfoDAL();


            if (model.AddressId == 0)
            {
                int i = dal.ExInt(" MemberId='" + model.MemberId + "' ");
                if (i > 20)
                {
                    throw new Exception("每个用户最多保留20条地址! (可以更改已有地址)");
                }
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }

        }




        /// <summary>
        /// 获得地址的实体类
        /// </summary>
        /// <param name="AddressId"></param>
        /// <returns></returns>
        public Model.AddressInfoModel GetAddressModel(decimal AddressId)
        {
            DAL.AddressInfoDAL dal = new DAL.AddressInfoDAL();

            return dal.GetModel(AddressId);

        }
        //清理一个用户的默认地址
        public void ClearDefaultAddress(decimal MemberId)
        {
            DAL.AddressInfoDAL dal = new DAL.AddressInfoDAL();
            DAL.DalComm.ExReInt(" Update CORE.dbo.AddressInfo set IsDefault=0 where MemberId='" + MemberId + "' ");
        }


        public DataSet GetAddressPageList(string StrWhere, int CurrentPage, int PageSize, string col)
        {

            DAL.AddressInfoDAL dal = new DAL.AddressInfoDAL();
            return dal.GetPageList(StrWhere, CurrentPage, PageSize, col);

        }
        public DataSet GetAddressInfo(decimal AddressId)
        {
            DAL.AddressInfoDAL dal = new DAL.AddressInfoDAL();
            return dal.GetAddressInfo(AddressId);
        }
        //设为默认地址
        public void ToDefault(decimal AddressId)
        {
            ClearDefaultAddress(GetCurrentDecimalMemberId());
            DAL.DalComm.ExReInt("  Update CORE.dbo.AddressInfo set IsDefault=1 where AddressId='" + AddressId + "' ");
        }

        public void InvalidAddress(decimal AddressId, bool Invalid)
        {
            DAL.DalComm.ExReInt(" Update CORE.dbo.AddressInfo set Invalid='" + Invalid + "' where AddressId='" + AddressId + "' ");
        }
        #endregion

        #region 静态快捷方法


        public static void SetMember(string WxOpenId, decimal MerId)
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            DataSet ds = mbll.GetMemberInfo(WxOpenId, MerId);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 1)
            {
                throw new Exception("当前用户定位出错:用户数量不能为" + dt.Rows.Count + ",请联系管理员:qq19278765!");
            }
            DataRow dr = dt.Rows[0];

            SetMemberByDr(dr);
        }


        public static void SetMember(decimal MemberId)
        {
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            DataSet ds = mbll.GetMemberInfo(MemberId);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 1)
            {
                throw new Exception("当前用户定位出错:用户数量不能为" + dt.Rows.Count + ",请联系管理员:qq19278765!");
            }
            DataRow dr = dt.Rows[0];

            SetMemberByDr(dr);
        }

        public static void SetMemberByDr(DataRow dr, int M)
        {
            if (M == 0)
            {


                CookieSings.AddCookieStr("CurrentMemberId", JiaMi.encMe(dr["MemberId"].ToString()));
                CookieSings.AddCookieStr("CurrentMemberName", JiaMi.encMe(dr["MemberName"].ToString()));
                CookieSings.AddCookieStr("CurrentMemberCode", JiaMi.encMe(dr["MemberCode"].ToString()));
                CookieSings.AddCookieStr("CurrentWxPtId", JiaMi.encMe(dr["WxPtId"].ToString()));
                CookieSings.AddCookieStr("CurrentWxOpenId", JiaMi.encMe(dr["WxOpenId"].ToString()));
                Common.CookieSings.AddCookieStr("CurrentNickName", Common.JiaMi.encMe(dr["NickName"].ToString()));
                Common.CookieSings.AddCookieStr("CurrentPhone", Common.JiaMi.encMe(dr["Phone"].ToString()));
            }
            else
            {
                CookieSings.AddCookieStr("CurrentMemberId", JiaMi.encMe(dr["MemberId"].ToString()), M);
                CookieSings.AddCookieStr("CurrentMemberName", JiaMi.encMe(dr["MemberName"].ToString()), M);
                CookieSings.AddCookieStr("CurrentMemberCode", JiaMi.encMe(dr["MemberCode"].ToString()), M);
                CookieSings.AddCookieStr("CurrentWxPtId", JiaMi.encMe(dr["WxPtId"].ToString()), M);
                CookieSings.AddCookieStr("CurrentWxOpenId", JiaMi.encMe(dr["WxOpenId"].ToString()), M);
                Common.CookieSings.AddCookieStr("CurrentNickName", Common.JiaMi.encMe(dr["NickName"].ToString()), M);
                Common.CookieSings.AddCookieStr("CurrentPhone", Common.JiaMi.encMe(dr["Phone"].ToString()), M);
            }






        }


        public static void SetMemberByDr(DataRow dr)
        {

            SetMemberByDr(dr, 0);




        }

        public static Model.CurrentMember CurrentMember()
        {




            Model.CurrentMember model = new Model.CurrentMember();
            try
            {



                BLL.MemberBLL mbll = new BLL.MemberBLL();
                string s = Common.PageInput.ReStr("s", "");
                if (s == "app")
                {
                    //如果是移动端登录
                    model.CurrentMemberId = Common.PageInput.ReDecimal("GetMemberId", 0);

                }
                else
                {
                    model.CurrentMemberId = decimal.Parse(JiaMi.uncMe(Common.CookieSings.ReCooke("CurrentMemberId")));
                }

                if (model.CurrentMemberId == 0)
                {

                    throw new Exception("用户没有登录!");
                }
                DataSet ds = DAL.DalComm.BackData(" select MemberCode,MemberName,WxOpenId,WxPtId from dbo.MemberView where MemberId='" + model.CurrentMemberId + "' ");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                DataRow dr = dt.Rows[0];
                model.CurrentMemberCode = dr["MemberCode"].ToString();
                model.CurrentMemberName = dr["MemberName"].ToString();
                model.CurrentWxOpenId = dr["WxOpenId"].ToString();

                model.CurrentWxPtId = decimal.Parse(dr["WxPtId"].ToString());
            }
            catch
            {
                return null;
            }

            return model;
        }
        #endregion


        #region 短信验证码


        public bool DuanXinYanZheng(decimal MerId, string Phone, string PhoneYzm, decimal StMsgTypeId)
        {
            StringBuilder s = new StringBuilder();
            DateTime date2 = DateTime.Now;
            DateTime date1 = date2.AddHours(-24);
            s.Append(" select count(0) from DBLOG.dbo.StMsg with(nolock) where ");

            s.Append(" MerId='" + MerId + "' and PhoneNo='" + Phone + "' ");
            s.Append(" and Invalid = 0 ");
            if (StMsgTypeId != 0)
            {
                s.Append(" and StMsgTypeId = " + StMsgTypeId + " ");
            }


            s.Append(" and CreateTime  BETWEEN '" + date1 + "' AND '" + date2 + "' ");
            s.Append(" and ReKey = '" + PhoneYzm + "' ");

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

        #endregion


        #region 签到逻辑


        public bool TodayQianDao(decimal MemberId)
        {
            DateTime 今天开始 = DateTime.Now.Date;
            int 今天签到次数 = DAL.DalComm.ExInt("SELECT  COUNT(0) FROM dbo.QianDaoLog WITH(NOLOCK) WHERE QianDaoMemberId='" + MemberId + "' AND CreateTime >'" + 今天开始 + "'");
            if (今天签到次数 > 0)
            {

                return true;
            }
            else
            {
                return false;
            }

        }


        public void SendQianDao(decimal MemberId, ref int Days, ref decimal GetJiFen)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                DAL.QianDaoLogDAL dal = new DAL.QianDaoLogDAL();
                StringBuilder s = new StringBuilder();

                //首先检测今天是否签到
                DateTime 今天开始 = DateTime.Now.Date;







                DateTime 昨天开始 = DateTime.Now.AddDays(-1).Date;
                s.Append("  SELECT  * FROM dbo.QianDaoLogView  WHERE QianDaoMemberId='" + MemberId + "' AND  CreateTime BETWEEN '" + 昨天开始 + "' AND '" + 今天开始 + "' ");
                s.Append(" SELECT * FROM dbo.MemberView WHERE MemberId='" + MemberId + "' ");




                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                DataTable dtMember = ds.Tables[1];
                if (dtMember.Rows.Count != 1)
                {
                    throw new Exception("MemberId=" + MemberId + ",数量是" + dtMember.Rows + "?! 没有吗? 这不科学!");
                }
                DataRow drMember = dtMember.Rows[0];
                //读取商家配置

                Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfig(decimal.Parse(drMember["MerId"].ToString()));
                int AddJiFenForQianDao = Convert.ToInt32(MerConfig["AddJiFenForQianDao"]);  //递增赠送积分
                int MaxJiFenForQianDao = Convert.ToInt32(MerConfig["MaxJiFenForQianDao"]);  //最大赠送积分
                int MinJiFenForQianDao = Convert.ToInt32(MerConfig["MinJiFenForQianDao"]);  //起始赠送积分
                Model.QianDaoLogModel model = new QianDaoLogModel();
                model.CreateTime = DateTime.Now;
                model.QianDaoMemberId = MemberId;
                model.QianDaoMemo = "正常签到";

                Model.JiFenChangeModel ModChange = new JiFenChangeModel();
                ModChange.CreateTime = model.CreateTime;
                ModChange.JifenChangeTypeId = 7;
                ModChange.MemberId = MemberId;
                ModChange.ReKey = "";  //最后有一个UPDATE来更改

                if (dt.Rows.Count > 0)
                {
                    //昨天也已经签过, 今天接着签

                    DataRow dr = dt.Rows[0];
                    int DayNum = int.Parse(dr["DayNum"].ToString());

                    model.DayNum = DayNum + 1;
                    Days = model.DayNum;
                    int 昨天的积分 = int.Parse(dr["JiFenChangeNum"].ToString());
                    ModChange.JiFenChangeNum = 昨天的积分 + AddJiFenForQianDao;
                    if (ModChange.JiFenChangeNum > MaxJiFenForQianDao)
                    {//如果大于最大值了. 那不行, 就的跟最大值持平
                        ModChange.JiFenChangeNum = MaxJiFenForQianDao;
                    }

                    GetJiFen = ModChange.JiFenChangeNum;
                    model.QianDaoMemo = "连续" + model.DayNum + "天签到,得到积分" + ModChange.JiFenChangeNum + "!";
                    ModChange.JiFenChangeMemo = model.QianDaoMemo;
                }
                else
                {
                    //昨天没有签过, 今天也没有签过,今天从新签
                    model.DayNum = 1;
                    Days = model.DayNum;
                    ModChange.JiFenChangeNum = MinJiFenForQianDao; //起始赠送积分
                    if (MinJiFenForQianDao != 0)
                    {
                        model.QianDaoMemo = "签到获得积分" + MinJiFenForQianDao + ",连续签到将获得更多积分!";
                    }
                    else
                    {
                        model.QianDaoMemo = "已经签到,签到积分奖励尚未开启!";
                    }
                    GetJiFen = ModChange.JiFenChangeNum;
                    ModChange.JiFenChangeMemo = model.QianDaoMemo;
                }




                BLL.MerchantBLL MerBll = new MerchantBLL();

                MerBll.ChangeMemberJiFen(ModChange);                 //写入积分变更
                model.JiFenChangeId = ModChange.JifenChangeId;
                s.Clear();

                int 今天签到次数 = DAL.DalComm.ExInt("SELECT  COUNT(0) FROM dbo.QianDaoLog  WHERE QianDaoMemberId='" + MemberId + "' AND CreateTime >'" + 今天开始 + "'");
                if (今天签到次数 > 0)
                {

                    throw new Exception("您今天已经签过到了，明天再来吧！");
                }
                else
                {
                    dal.Add(model);                                     //写入签到日志
                }




                s.Append(" UPDATE dbo.JiFenChange SET ReKey='" + model.QianDaoLogId + "' WHERE JifenChangeId='" + ModChange.JifenChangeId + "' "); //关联积分变更和签到日志
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }

        #endregion


    }
}
