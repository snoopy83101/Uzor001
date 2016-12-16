using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.IO;
using Model;
using System.Text;
namespace BLL
{
    public class UserBLL
    {
        public DAL.UserInfoDAL dalUser = new DAL.UserInfoDAL();




        public string CkUserLv()
        {
            StringBuilder s = new StringBuilder();
            BLL.UserBLL bll = new UserBLL();
            string UserId = Common.CookieSings.GetCurrentUserId();
            s.Append(" select  top 1 UserLv  from dbo.UserInfo where UserId='" + UserId + "' ");
            int UserLv = DAL.DalComm.ExInt(s.ToString());
            if (UserLv < 0)
            {
                throw new Exception("您已经被禁言! 如果您怀疑这是误操作, 请联系站长16248777 ");
            }
            else
            {
                return UserId;
            }
        }

        public bool HasPower(string JobId)
        {
            StringBuilder s = new StringBuilder();
            BLL.UserBLL bll = new UserBLL();
            if (bll.IsAdministrator())
            {
                return true;
            }
            else
            {
                string UserId = CkUserLv();
                if (JobId.Trim() == "")
                {
                    return true;
                }
                s.Append(" select top 1  CreateUser from dbo.JobView where JobId='" + JobId + "' ");
                string CreateUser = DAL.DalComm.ExStr(s.ToString());
                if (CreateUser == UserId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static void MastLogin(string str)
        {

            if (Common.CookieSings.GetCurrentUserId() == null || Common.CookieSings.GetCurrentUserId().Trim() == "")
            {


                string url = HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
                str = HttpUtility.UrlEncode(str);
                HttpContext.Current.Response.Redirect("/Login/?url=" + url + "&str=" + str + "");
            }

        }


        public static string GetTownOption()
        {
            BLL.TownBLL bll = new TownBLL();
            DataTable dt = bll.GetTownList();
            StringBuilder w = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {


                string TownName = dr["TownName"].ToString();
                w.Append("<option value='" + dr["TownId"] + "' >");
                TownName = TownName.Remove(TownName.Length - 1, 1);
                w.Append(TownName + "银");
                w.Append("</option>");

            }

            return w.ToString();
        }
        public static string GetTownOption2()
        {
            BLL.TownBLL bll = new TownBLL();
            DataTable dt = bll.GetTownList();
            StringBuilder w = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {


                string TownName = dr["TownName"].ToString();
                w.Append("<option value='" + dr["TownId"] + "' >");

                w.Append(TownName);
                w.Append("</option>");

            }

            return w.ToString();
        }



        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Registration(Model.UserInfoModel model)
        {
            if (model.Sex == "" || model.Sex == null)
            {
                model.Sex = "男";
            }

            model.Birthday = DateTime.Parse("1990-01-01");
            if (model.Sex == "男")
            {

                if (model.PicBig == "")
                {
                    model.PicSmall = "boy3";
                    model.PicMid = "boy2";
                    model.PicBig = "boy1";
                }
            }
            else if (model.Sex == "女")
            {
                if (model.PicBig == "")
                {
                    model.PicSmall = "girl1";
                    model.PicMid = "girl1";
                    model.PicBig = "girl1";
                }



            }
            if (model.TownId <= 0)
            {
                model.TownId = 1;
            }
            model.UserCode = DAL.DalComm.ExInt(" select MAX(UserCode) + 1 FROM    dbo.UserInfo ");

            model.PwdMd5 = Common.JiaMi.MD5(model.Pwd);


            if (dalUser.GetList(" UserId='" + model.UserId + "' ").Tables[0].Rows.Count > 0)
            {
                throw new Exception("此用户昵称已经有人使用了.");
            }
            if (model.Email != null)
            {



                if (model.Email.Trim() != "")
                {
                    //如果填写了邮箱的话.

                    if (dalUser.GetList(" Email='" + model.Email + "' ").Tables[0].Rows.Count > 0)
                    {

                        throw new Exception("此用邮箱'" + model.Email + "'已经有人使用了.");
                    }


                }
            }


            try
            {
                dalUser.Add(model);
                return "ok";
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public DataSet GetuserInfo(string UserId)
        {
            return dalUser.GetUserInfo(UserId);
        }


        public DataSet GetUserData(string UserId)
        {
            return dalUser.GetList(" UserId='" + UserId + "' ");

        }
        public DataSet GetUserList(string str)
        {
            return dalUser.GetList(str);

        }

        public DataSet GetUserPageList(string strWhere, int currentpage, int pagesize)
        {
            return dalUser.GetPageList(strWhere, currentpage, pagesize);

        }
        public DataSet GetUserData(string UserId, string Pwd)
        {
            return dalUser.GetList(" UserId='" + UserId + "' and ( Pwd='" + Pwd + "' or PwdMd5='" + Common.JiaMi.MD5(Pwd) + "') ");

        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public bool ChangePwd(string UserId, string Pwd)
        {
            return dalUser.ChangePwd(UserId, Pwd);
        }

        public void ImproveUserData(UserInfoModel model)
        {
            dalUser.Update(model);
        }

        public UserInfoModel GetModel(string UserId)
        {
            return dalUser.GetModel(UserId);
        }

        public void SaveMyUserInfo(UserInfoModel model)
        {
            dalUser.Update(model);
        }


        /// <summary>
        /// 如果用户存在则不添加
        /// </summary>
        /// <param name="model"></param>
        public void AddUser(UserInfoModel model)
        {

            var i = dalUser.ExInt(" UserId='" + model.UserId + "' ");
            if (i > 0)
            {

            }
            else
            {
                dalUser.Add(model);
            }


        }





        /// <summary>
        /// 检查用户和密码的一致性
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public bool CheckUserIdAndPwd(string UserId, string Pwd)
        {
            DataTable dt = dalUser.GetList(" UserId='" + UserId + "'  and ( Pwd='" + Pwd + "' or PwdMd5='" + Common.JiaMi.MD5(Pwd) + "') ").Tables[0];

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataSet GetUserInfoByWxOpenId(string OpenId)
        {
            StringBuilder s = new System.Text.StringBuilder();
            DAL.UserInfoDAL dal = new DAL.UserInfoDAL();
            DataSet ds = dal.GetUserInfoByWxOpenId(OpenId);
            return ds;
        }


        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string UserLoginBackUserId(string inputStr, string pwd, int RememberMouth)
        {

            string pwdmd5 = Common.JiaMi.MD5(pwd);
            DataTable dt = dalUser.GetList(" (UserId='" + inputStr + "' or Email='" + inputStr + "') and (Pwd='" + pwd + "' or PwdMd5='" + pwdmd5 + "'   ) ").Tables[0];


            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                int i = DAL.DalComm.ExReInt(" update dbo.userInfo set pwd='" + pwd + "' where userId='" + dr["UserId"].ToString() + "'  ");
                if (i != 1)
                {
                    throw new Exception("登录信息错误! 请联系管理员 qq:16248777");
                }
                string UserId = dr["UserId"].ToString();
                //  string Pwd = dr["Pwd"].ToString();为什么?
                if (RememberMouth > 0)
                {
                    LoginIn(UserId, pwd, RememberMouth);
                }
                else
                {
                    LoginIn(UserId, pwd);
                }
                return UserId;


            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 判断是否超级管理员
        /// </summary>
        /// <param name="CurrentUserAndRole"></param>
        /// <returns></returns>
        /// 
        public bool IsAdministrator()
        {

            return IsAdministrator(CurrentUserAndRole());
        }



        public bool IsAdministrator(List<string> CurrentUserAndRole)
        {
            if (CurrentUserAndRole == null)
            {
                return false;
            }

            if (CurrentUserAndRole.Count < 2)
            {
                return false;
            }

            for (int i = 1; i < CurrentUserAndRole.Count; i++)
            {
                string roleName = CurrentUserAndRole[i];
                if (roleName == "超级管理员")
                {
                    return true;

                }

            }

            return false;
        }



        /// <summary>
        /// 当前用户, 第一条是用户名,随后是Role角色
        /// </summary>
        /// <returns></returns>
        public List<string> CurrentUserAndRole()
        {

            List<string> li = new List<string>();
            DataSet ds = CurrentUserSub();

            if (ds == null)
            {
                return null;
            }

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable dt = ds.Tables[i];
                if (i == 0)
                {

                    DataRow dr = dt.Rows[0];
                    li.Add(dr["UserId"].ToString());

                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        li.Add(dr["RoleName"].ToString());
                    }
                }
            }
            return li;
        }



        //取得用户属性
        public DataSet CurrentUserSub()
        {

            string u = CurrentUserId("-1");
            if (u == "-1")
            {
                return null;
            }

            DataSet ds = dalUser.GetUserInfo(u);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            return ds;

        }


        /// <summary>
        /// 检测合法性后返回当前登录用户
        /// </summary>
        /// <returns></returns>
        public string CurrentUserId()
        {


            if (CheckLogin())
            {

                return HttpUtility.UrlDecode(Common.CookieSings.ReCooke("CurrentUserId"));
            }
            else
            {

                throw new Exception("用户权限错误，建议重新登录！");
            }


        }


        /// <summary>
        /// 单独返回用户的真实姓名
        /// </summary>
        /// <returns></returns>
        public string CurrentUserRealName()
        {
            string UserId = HttpUtility.UrlDecode(Common.CookieSings.ReCooke("UserId"));
            return DAL.DalComm.ExStr("SELECT RealName FROM dbo.UserInfo WITH(NOLOCK) WHERE UserId='" + UserId + "'");


        }

        public string CurrentUserId(string BackStr)
        {
            try
            {
                return CurrentUserId();
            }
            catch
            {
                return BackStr;
            }
        }

        /// <summary>
        /// 检查当前用户的合法性
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin()
        {
            try
            {

                if (HttpContext.Current.Request.Cookies["CurrentUserId"] == null)
                {
                    return false;

                }


                string UserId = System.Web.HttpUtility.UrlDecode(Common.CookieSings.ReCooke("CurrentUserId"));
                string UserKey = Common.CookieSings.ReCooke("CurrentUserKey");
                return CheckLogin(UserId, UserKey);
            }
            catch (Exception ex)
            {
                LoginOut();
                throw ex;
            }

        }


        /// <summary>
        /// 检查用户登陆的合法性
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserKey"></param>
        /// <returns></returns>
        public bool CheckLogin(string UserId, string UserKey)
        {


            string key = Common.JiaMi.DeCode(UserKey);
            string[] k = key.Split('|');
            if (k[0] != UserId)
            {
                throw new Exception("您的登陆信息验证没有通过,请尝试重新登陆!");
            }
            string pwdmd5 = Common.JiaMi.MD5(k[1]);
            int i = dalUser.ExInt(" UserId='" + UserId + "' and (Pwd='" + k[1] + "' or PwdMd5='" + pwdmd5 + "'  ) ");
            if (i > 0)
            {

                return true;
            }
            else
            {

                //用户名或者密码不正确
                LoginOut();
                throw new Exception("用户名或者密码不正确,您是否在之前记住了密码,随后又改动了密码?");
            }
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="UserId"></param>
        public void LoginIn(string UserId, string Pwd)
        {
            Common.CookieSings.AddCookieStr("CurrentUserKey", BackUserKey(UserId, Pwd));
            Common.CookieSings.AddCookieStr("CurrentUserId", UserId);
        }

        public void LoginIn(string key)
        {

            key = Common.JiaMi.DeCode(key);
            string[] k = key.Split('|');

            string UserId = k[0];
            string pwd = k[1];

            string pwdmd5 = Common.JiaMi.MD5(k[1]);
            int i = dalUser.ExInt(" UserId='" + UserId + "' and ( Pwd='" + k[1] + "' PwdMd5 = '" + pwdmd5 + "') ");
            if (i > 0)
            {
                LoginIn(UserId, pwd);
            }
            else
            {

                throw new Exception("加密串不合法!");
            }

        }

        public string GetUserIdFormKey(string key)
        {
            key = Common.JiaMi.DeCode(key);
            string[] k = key.Split('|');

            return k[0];

        }

        public string GetUserPwdFormKey(string key)
        {
            key = Common.JiaMi.DeCode(key);
            string[] k = key.Split('|');

            return k[1];
        }
        /// <summary>
        /// 生成用户安全码
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string BackUserKey(string UserId, string pwd)
        {

            return Common.JiaMi.EnCode(UserId + "|" + pwd);
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="UserId"></param>
        public void LoginOut()
        {
            Common.CookieSings.ClearCookie("CurrentUserKey");
            Common.CookieSings.ClearCookie("CurrentUserId");
        }


        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="UserId">用户名</param>
        /// <param name="Pwd">密码</param>
        /// <param name="AddMonths">记住Cookie的月份</param>
        public void LoginIn(string UserId, string Pwd, int AddMonths)
        {

            Common.CookieSings.AddCookieStr("CurrentUserKey", Common.JiaMi.EnCode(UserId + "|" + Pwd), AddMonths);
            Common.CookieSings.AddCookieStr("CurrentUserId", System.Web.HttpUtility.UrlEncode(UserId), AddMonths);
        }

        // <summary>
        // 获取当前登陆用户
        // </summary>
        // <returns></returns>
        //public string CurrentUserId()
        //{
        //    try
        //    {
        //        string UserId = Common.JiaMi.DeCode(HttpContext.Current.Request.Cookies["UserId"].Value);
        //        return UserId;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        /// <summary>
        /// 获得当前登陆用户的加密字符串
        /// </summary>
        /// <returns></returns>
        public string CurrentEnCodeUserId()
        {
            string UserId = HttpContext.Current.Request.Cookies["UserId"].Value;
            return UserId;

        }



        public string BackPwdMail(string UserId)
        {
            StringBuilder w = new StringBuilder();
            UserBLL ubll = new UserBLL();
            Model.UserInfoModel model = ubll.GetModel(UserId);
            string key = BackUserKey(model.UserId, model.Pwd);
            w.Append("<p>尊敬的沂源信息港用户  <strong>" + UserId + "</strong>： </p> ");
            w.Append("<div style='margin-left: 30px;'>");
            w.Append("<p>您好！</p>");

            w.Append("<p>您也可以点击如下链接来完成帐号验证。</p>");
            w.Append("<p><a href='http://localhost:99/myPwd/?key=" + key + "' target='_blank'>http://localhost:99/myPwd/?key=" + key + "</a></p>");
            w.Append("<p>如果上面的链接无法点击，您也可以复制链接，粘贴到您浏览器的地址栏内，然后按“回车”键打开预设页面，完成相应功能。</p>");
            //    w.Append("<p>验证将会在30分钟后失效，请尽快完成身份验证，否则需要重新进行验证。</p>");
            w.Append("<p>如果有其他问题，请联系我们,16248777@qq.com  QQ:16248777 谢谢！</p>");
            w.Append("</div>");
            w.Append("<p>此为系统消息，请勿回复</p>");

            w.Append(@"<style type='text/css'> body
        {
            font-size: 14px;
            font-family: arial,verdana,sans-serif;
            line-height: 1.666;
            padding: 0;
            margin: 0;
            overflow: auto;
            white-space: normal;
            word-wrap: break-word;
            min-height: 100px;
        }

        td, input, button, select, body
        {
            font-family: Helvetica, 'Microsoft Yahei', verdana;
        }

        pre
        {
            white-space: pre-wrap;
            white-space: -moz-pre-wrap;
            white-space: -pre-wrap;
            white-space: -o-pre-wrap;
            word-wrap: break-word;
        }

        th, td
        {
            font-family: arial,verdana,sans-serif;
            line-height: 1.666;
        }

        img
        {
            border: 0;
        }

        header, footer, section, aside, article, nav, hgroup, figure, figcaption
        {
            display: block;
        }
    </style>

    <style id='ntes_link_color' type='text/css'>
        a, td a
        {
            color: #003399;
        }
    </style>");


            if (Common.Validator.IsEmail(model.Email))
            {

                Common.Mail.Send("取回密码验证邮件[南麻街 - 沂源信息港旗下生活服务平台]", w.ToString(), model.Email);
            }
            else
            {
                throw new Exception(model.Email + "好像不是一个合法邮件!");
            }
            return model.Email;
        }


    }
}
