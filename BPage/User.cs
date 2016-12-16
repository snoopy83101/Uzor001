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

namespace BPage
{
    public class User : Common.BPageSetting2
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string para = ReStr("para");
                switch (para)
                {




                    #region 消息权限

                    case "SaveMsgTypeVsMerRole":
                        SaveMsgTypeVsMerRole();
                        break;

                    case "GetMerRoleVsMsgType":
                        GetMerRoleVsMsgType();
                        break;


                    #endregion

                    #region 通知
                    case "SendNotice":
                        SendNotice();
                        break;

                    #endregion

                    case "GetUserList":
                        GetUserList();
                        break;


                    case "AddUser":
                        AddUser();
                        break;

                    case "SaveUser":
                        SaveUser();
                        break;

                    case "ForgetPwdForEmail":
                        ForgetPwdForEmail();
                        break;

                    case "SaveMyUserInfo":

                        SaveMyUserInfo();   //保存我的资料.
                        break;

                    case "CheckEmail":
                        CheckEmail();   //检测邮箱
                        break;

                    case "CheckUserId":

                        CheckUserId();  //检测用户名
                        break;

                    case "ChangePwd":
                        ChangePwd();

                        break;

                    case "CheckCurrentPwd":
                        CheckCurrentPwd();  //检测当前用户密码
                        break;

                    case "GetMerList":
                        GetMerList();  //获得用户的商家列表
                        break;


                    case "GetTownOption":
                        GetTownOption();
                        break;


                    case "GetUserJson":
                        GetUserJson();   //获得用户安全资料,需要UserKey的
                        break;

                    case "GetUserData":
                        GetUserData();   //获得用户资料
                        break;

                    case "ImproveUserData":
                        ImproveUserData();  //完善资料
                        break;

                    case "CheckLogin":
                        CheckLogin();
                        break;

                    case "LoginOut":
                        LoginOut();
                        break;

                    case "UserLogin":
                        UserLogin();
                        break;
                    case "Registration":
                        Registration();  //用户注册
                        break;

                    case "GetUserMerRoleList":
                        GetUserMerRoleList();  //根据角色获得用户列表

                        break;
                }
            }
            catch (Exception ex)
            {
                BLL.StaticBLL.ReThrow(ex);

            }
            finally
            {
                context.Response.End();
            }

        }

        private void SaveMsgTypeVsMerRole()
        {
            decimal MerRoleId = ReDecimal("MerRoleId", 0);

            if (MerRoleId == 0)
            {
                throw new Exception("MerRole不能为0!");
            }

            DataTable dt = ReTable("MsgTypeArray");

            DAL.MsgTypeVsMerRoleDAL dal = new DAL.MsgTypeVsMerRoleDAL();

            Model.MsgTypeVsMerRoleModel model = new MsgTypeVsMerRoleModel();


            model.MerRoleId = MerRoleId;




            if (dt != null)


            {


                dal.DeleteList(" MerRoleId='" + MerRoleId + "' ");  //删除
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {


                        string MsgTypeId = dr["MsgTypeId"].ToString();

                        model.MsgTypeId = MsgTypeId;



                        dal.Add(model);

                    }
                }

            }

            ReTrue();

        }

        private void GetMerRoleVsMsgType()
        {

            decimal MerRoleId = ReDecimal("MerRoleId", 0);

            string UserId = ReStr("uid", "");


            StringBuilder s = new StringBuilder();


            DataSet ds;

            if (UserId != "")//如果传了UserId, 则默认获取该User的所有角色的被允许监听的消息类型的并集
            {
                ds = DAL.DalComm.BackData(" SELECT * FROM DBMSG.dbo.MsgTypeVsMerRoleView WHERE MerRoleId IN (SELECT mvu.MerRoleId FROM dbo.MerRoleVsUser mvu WHERE UserId='" + UserId + "') ");
            }
            else
            {
                ds = DAL.DalComm.BackData(" SELECT * FROM DBMSG.dbo.MsgTypeVsMerRoleView WHERE MerRoleId='" + MerRoleId + "' ");

            }





            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();

        }

        private void GetUserList()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.UserView WITH(NOLOCK) WHERE 1=1 ");
            decimal MerId = ReDecimal("MerId", 0);
            string BranchId = ReStr("BranchId", "");
            if (MerId != 0)
            {
                s.Append(" and  ");

            }

            if (BranchId != "")
            {

            }

        }

        private void SendNotice()
        {

            string RongUserId = ReStr("RongUserId", "");
            if (RongUserId == "")
            {
                throw new Exception("RongUserId(发送方)不能为空!");
            }

            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }

            Model.NoticeModel model = new NoticeModel();
            model.CreateTime = DateTime.Now;
            model.Extra = ReStr("Extra", "{}");
            model.NoticeContent = ReStr("NoticeContent", "");
            model.NoticeTitle = ReStr("NoticeTitle", "");

            model.NoticeType = ReStr("NoticeType", "system");
            model.RongUserId = RongUserId;
            BLL.CommBLL bll = new BLL.CommBLL();
            string TargetIds = ReStr("TargetIds", "");
            if (TargetIds.Trim() == "")
            {
                throw new Exception("TargetIds不能为空!  ");
            }
            List<Model.NoticeTargetModel> TargetModelList = new List<NoticeTargetModel>();
            string[] TargetArray = TargetIds.Split(',');
            foreach (string TargetId in TargetArray)
            {
                Model.NoticeTargetModel TargetModel = new NoticeTargetModel();
                TargetModel.NoticeId = 0;

                TargetModel.TargetId = TargetId;
                TargetModel.NoticeStatus = 0; //未发送
                TargetModelList.Add(TargetModel);
            }

            bll.SendNotice(model, TargetModelList, MerId);

            ReTrue();

        }

        private void AddUser()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            BLL.UserBLL ubll = new BLL.UserBLL();
            Model.UserInfoModel model = new Model.UserInfoModel();
            Model.MerRoleVsUserModel vsModel = new Model.MerRoleVsUserModel();
            model.UserId = ReStr("uid", "");

            if (model.UserId.Length < 5)
            {
                throw new Exception("用户名不能小于5个字符!");
            }


            model.CreateTime = DateTime.Now;
            model.Birthday = DateTime.Parse("1900-01-01");

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                ubll.AddUser(model);
                vsModel.UserId = model.UserId;
                vsModel.BranchId = ReStr("BranchId", "");
                vsModel.MerRoleId = ReDecimal("MerRoleId", 0);
                mbll.SaveMerRoleVsUser(vsModel);



                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
            ReTrue();
        }

        private void SaveUser()
        {
            Model.UserInfoModel model = new Model.UserInfoModel();
            model.Birthday = ReTime("Birthday", DateTime.Parse("1900-01-01"));
            model.CreateTime = DateTime.Now;
            model.Currency = 0;
            model.Email = ReStr("Email", "");
            model.FlagMerchant = true;
            model.IdNo = ReStr("IdNo", "");
            model.Memo = ReStr("Memo", "");
            model.Phone = ReStr("Phone", "");
            model.PicBig = ReStr("PicBig", "");
            model.PicMid = ReStr("PicMid", "");
            model.PicSmall = ReStr("PicSmall", "");
            model.Power = 0;
            model.Pwd = ReStr("Pwd", "");
            model.PwdMd5 = ReStr("PwdMd5", "");
            model.qq = ReStr("qq", "");
            model.RealName = ReStr("RealName", "");
            model.Sex = ReStr("sex", "未知");
            model.StreetId = 0;
            model.Tell = ReStr("Tell", "");
            model.TownId = 1;
            model.UserCode = ReInt("UserCode", 0);
            model.UserId = ReStr("uid", "");
            if (model.UserId.Trim() == "")
            {
                throw new Exception("不能为空!");
            }
            model.UserLv = ReInt("UserLv", 90);
            model.UserTitle = ReStr("UserTitle", "");
            model.UserTypeId = 3;
            model.Validated = ReBool("Validated", true);
            model.WxOpenID = ReStr("WxOpenID", "");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            BLL.UserBLL ubll = new BLL.UserBLL();
            if (model.UserId == "小王")
            {
                if (ubll.CurrentUserId() != "小王")
                {
                    throw new Exception("小王不是一般人能改的!-_-!");
                }
            }
            ubll.SaveMyUserInfo(model);
            ReTrue();


        }
        private void GetUserMerRoleList()
        {

            string MerRoleName = ReStr("MerRoleName", "");
            decimal MerRoleId = ReDecimal("MerRoleId", 0);
            int AboveWorkStatus = ReInt("AboveWorkStatus", 0);
            int BelowWorkStatus = ReInt("BelowWorkStatus", 100);
            decimal QyWxPtId = ReDecimal("QyWxPtId", 0);
            string BranchId = ReStr("BranchId", "");
            StringBuilder s = new StringBuilder();
            s.Append(" select top 20 * FROM dbo.UserMerRoleView WHERE ");
            s.Append(" 1=1  ");

            if (MerRoleName != "")
            {
                s.Append(" and MerRoleName='" + MerRoleName + "' ");
            }

            if (MerRoleId != 0)
            {
                s.Append(" and MerRoleId='" + MerRoleId + "' ");
            }

            if (BranchId != "")
            {

                s.Append(" and BranchId='" + BranchId + "'  ");

            }

            s.Append(" and WorkStatusId>=" + AboveWorkStatus + " and WorkStatusId<=" + BelowWorkStatus + "  ");
            DataSet ds;
            if (QyWxPtId != 0)
            {
                s.Append(" SELECT * FROM dbo.QyWxPtApp WHERE  QyWxPtId='" + QyWxPtId + "' ");
                ds = DAL.DalComm.BackData(s.ToString());
                DataTable dtQyWxPtApp = ds.Tables[1];
                ReDict.Add("QyWxPtApp", JsonHelper.ToJson(dtQyWxPtApp));
            }
            else
            {
                ds = DAL.DalComm.BackData(s.ToString());
            }




            DataTable dtUser = ds.Tables[0];


            ReDict.Add("UserList", Common.JsonHelper.ToJson(dtUser));


            ReTrue();
        }


        /// <summary>
        /// 忘记密码
        /// </summary>
        private void ForgetPwdForEmail()
        {

            string UserId = DAL.DalComm.ExStr(" select top 1 UserId from dbo.UserInfo WITH(NOLOCK) where Email ='" + ReStr("EmailOrUserId") + "' or  UserId ='" + ReStr("EmailOrUserId") + "' ");

            if (UserId == "")
            {
                throw new Exception("用户名或邮箱填写不正确!");
            }
            else
            {
                BLL.UserBLL bll = new BLL.UserBLL();
                string Email = bll.BackPwdMail(UserId);
                ReDict2.Add("Email", Email);
                ReTrue();
            }
        }

        /// <summary>
        /// 完善资料
        /// </summary>
        private void SaveMyUserInfo()
        {

            BLL.UserBLL ubll = new BLL.UserBLL();


            Model.UserInfoModel model = ubll.GetModel(ubll.CurrentUserId());
            if (model.UserId.Trim() != "")
            {
                model = ubll.GetModel(model.UserId);
            }

            model.Birthday = ReTime("Birthday");
            model.CreateTime = DateTime.Now;
            model.Memo = ReStr("Memo");
            model.RealName = ReStr("RealName");
            model.Sex = ReStr("Sex");
            model.TownId = ReDecimal("TownId");
            if (model.UserTitle != ReStr("UserTitle"))
            {
                try
                {
                    //用户修改了状态
                    Model.DynamicModel dyModel = new Model.DynamicModel();
                    dyModel.DynamicLv = 80;
                    dyModel.DynamicMerId = 0;
                    dyModel.DynamicTitle = "" + model.UserId + "修改了自己的状态: '" + Common.StringPlus.GetLeftStr(ReStr("UserTitle"), 100, "...") + "'";
                    dyModel.DynamicType = "用户状态";
                    dyModel.DynamicUserId = model.UserId;
                    BLL.CommBLL.AddDynamic(dyModel);
                }
                catch (Exception ex)
                {

                    //什么都不做,失败就算了,不重要. 
                }
            }

            model.UserTitle = ReStr("UserTitle");
            model.PicBig = ReStr("PicBig");
            model.PicMid = model.PicBig;
            model.PicSmall = model.PicBig;

            model.qq = ReStr("qq", "");
            ubll.SaveMyUserInfo(model);
            ReTrue();
        }

        private void CheckEmail()
        {
            BLL.UserBLL bll = new BLL.UserBLL();

            string Email = ReStr("Email");

            DataSet ds = bll.GetUserList(" Email='" + Email + "' ");

            if (ds.Tables[0].Rows.Count > 0)
            {
                throw new Exception("邮件已经存在!");
            }
            else
            {
                ReTrue();
            }
        }

        private void CheckUserId()
        {
            BLL.UserBLL bll = new BLL.UserBLL();

            string UserId = ReStr("UserId");

            DataSet ds = bll.GetUserData(UserId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                throw new Exception("用户名已经存在!");
            }
            else
            {
                ReTrue();
            }


        }

        private void ChangePwd()
        {
            BLL.UserBLL bll = new BLL.UserBLL();
            string OldPwd = ReStr("OldPwd");
            string NewPwd1 = ReStr("NewPwd1");
            string NewPwd2 = ReStr("NewPwd2");
            string UserId = bll.CurrentUserId();
            if (!bll.CheckUserIdAndPwd(UserId, OldPwd))
            {
                throw new Exception("原始密码与当前用户不符！");
            }

            if (NewPwd1.Trim() != NewPwd2.Trim())
            {
                throw new Exception("输入了两次不同的新密码！");

            }

            bll.ChangePwd(UserId, NewPwd1);
            bll.LoginIn(UserId, NewPwd1);
            ReTrue();




        }

        private void CheckCurrentPwd()
        {
            BLL.UserBLL bll = new BLL.UserBLL();

            string pwd = ReStr("OldPwd");
            string UserId = bll.CurrentUserId();

            if (bll.CheckUserIdAndPwd(UserId, pwd))
            {
                //用户密码正确！
                ReTrue();

            }
            else
            {  //用户密码错误！
                throw new Exception("密码输入错误！");
            }


        }

        private void LoginOut()
        {
            BLL.UserBLL bll = new BLL.UserBLL();
            bll.LoginOut();
            ReTrue();
        }

        private void GetMerList()
        {
            string CurrentUserId = ReStr("CurrentUserId");
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            try
            {
                DataTable dt = bll.GetMerList("  ");
            }
            catch (Exception)
            {

                throw;
            }

        }


        //安全获得用户资料
        private void GetUserJson()
        {
            BLL.UserBLL bll = new BLL.UserBLL();


            try
            {

                string Ukey = ReStr("Ukey");
                if (Ukey == "")
                {

                    throw new Exception("登陆信息不存在,请重新登陆!");
                }
                Ukey = Common.JiaMi.DeCode(Ukey);
                string[] i = Ukey.Split('|');
                string UserId = i[0];
                string pwd = i[1];
                DataTable dt = bll.GetUserData(UserId, pwd).Tables[0];

                if (dt.Rows.Count == 0)
                {

                    throw new Exception("登陆信息不存在,请重新登陆!");


                }
                string json = Common.JsonHelper.ToJson(dt);

                ReDict.Add("Uj", json);
                ReTrue();

            }
            catch (Exception ex)
            {
                bll.LoginOut();
                ReThrow(ex);
            }
        }

        private void GetTownOption()
        {

        }

        private void ImproveUserData()
        {

            BLL.UserBLL bll = new BLL.UserBLL();
            Model.UserInfoModel model = bll.GetModel(ReStr("CurrentUserId"));

            model.TownId = ReDecimal("TownId");
            model.Birthday = ReTime("Birthday");
            model.Email = ReStr("Email");
            model.qq = ReStr("qq");
            model.Phone = ReStr("Phone");
            try
            {
                bll.ImproveUserData(model);
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }



        }

        private void GetUserData()
        {

            BLL.UserBLL bll = new BLL.UserBLL();


            try
            {
                DataTable dt = bll.GetUserData(ReStr("uid")).Tables[0];
                string json = Common.JsonHelper.ToJsonNo1(dt);

                ReDict.Add("UserInfo", json);
                ReTrue();

            }
            catch (Exception ex)
            {
                ReThrow(ex);
            }
        }

        private void CheckLogin()
        {
            string UserId = ReStr("CurrentUserId");
            string UserKey = ReStr("CurrentUserKey");

            try
            {
                BLL.UserBLL bll = new BLL.UserBLL();
                if (!bll.CheckLogin(UserId, UserKey))
                {
                    throw new Exception("您的登陆验证没有通过,请尝试重新登陆.");
                }
                else
                {

                    ReTrue();
                }


            }
            catch (Exception ex)
            {
                ReThrow(ex);
            }





        }


        /// <summary>
        /// 后台用户登录
        /// </summary>
        private void UserLogin()
        {

            try
            {
                Model.UserInfoModel model = new Model.UserInfoModel();
                string inputStr = ReStr("inputStr", "");
                if (inputStr == "")
                {
                    throw new Exception("必须填写登录名!");
                }

                string Pwd = ReStr("Pwd");
                int RememberMouth = ReInt("RememberMouth", 120);
                string WxOpenID = ReStr("WxOpenID", "");


                BLL.UserBLL bll = new BLL.UserBLL();


                string UserId = bll.UserLoginBackUserId(inputStr, Pwd, RememberMouth);
                if (UserId == null || UserId == "")
                {
                    throw new Exception("用户名不存在或密码不正确!");
                }
                else
                {
                    if (WxOpenID.Trim() != "")
                    {
                        DAL.DalComm.ExStr("update CORE.dbo.UserInfo set WxOpenID='" + WxOpenID + "' where UserId='" + UserId + "' ");
                    }


                    ReDict2.Add("WxOpenID", WxOpenID);
                    ReDict2.Add("UserId", UserId);

                    string sourse = ReStr("s", "");
                    if (sourse == "app")
                    {

                    }
                    else
                    {

                    }
                    DataSet ds = DAL.DalComm.BackData(" select * from CORE.dbo.UserMerRoleView where UserId='" + UserId + "' ");
                    DataTable dt = ds.Tables[0];
                    ReDict.Add("MerRoleList", Common.JsonHelper.ToJson(dt));
                    ReTrue();
                }

                string s = ReStr("s", "");


            }
            catch (Exception ex)
            {
                ReThrow(ex);

            }


        }



        /// <summary>
        /// 用户注册
        /// </summary>
        private void Registration()
        {
            Model.UserInfoModel model = new Model.UserInfoModel();
            model.UserId = ReStr("uid");
            model.Email = ReStr("Email", "");
            model.Pwd = ReStr("Pwd");
            model.Birthday = DateTime.Parse("1900-01-01");
            model.Phone = ReStr("phone", "");
            model.PicBig = "boy1";
            model.PicMid = "boy2";
            model.PicSmall = "boy3";
            model.WxOpenID = ReStr("WxOpenID", "");
            model.UserCode = DAL.DalComm.ExInt(" select MAX(UserCode) + 1 FROM    dbo.UserInfo ");
            model.CreateTime = DateTime.Now;
            BLL.UserBLL bll = new BLL.UserBLL();
            try
            {
                bll.Registration(model);
                bll.LoginIn(model.UserId, model.Pwd);
                ReDict2.Add("UserId", model.UserId);
                ReTrue();
            }
            catch (Exception ex)
            {
                ReThrow(ex);

            }


        }
    }

}