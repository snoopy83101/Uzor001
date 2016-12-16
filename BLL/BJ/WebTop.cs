using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Common;
namespace BLL.BJ
{
    public class WebTop
    {

        public WebTop()
        {


        }

        /// <summary>
        /// 打开顶部菜单
        /// </summary>
        /// <returns></returns>
        public static string wTop()
        {
            BLL.UserBLL bll = new UserBLL();
            StringBuilder w = new StringBuilder();
            w.Append("<div class='toolbar area' id='toolbar'>");
            w.Append("<a name='top'></a>");
            w.Append("<div class='left'>");
            w.Append("<span><a target='_blank' href='/default.aspx'>南麻街</a>|</span><span><a  href='/About/'>业务服务</a>|</span><span><a target='_blank' href='/MyMerSet/?MerId=0'>商家加盟</a>|</span><span class='favorite'><a onclick='AddFavorite(\"南麻街-沂源信息港旗下生活服务平台\",\"加入收藏夹\")' href='javascript:void(0)'><s></s>收藏</a>|</span><span class='phone'><a target='_blank' href='/About/?aboutId=7'><s></s>手机版</a>|<a href='http://url.cn/TvIelQ' target='_blank' >官方QQ群:339204350,点击立即加入!</a></span>");
            w.Append("</div>");
            w.Append("<div class='right r' style='float: right;'>");
            w.Append("<div class='l login_infos' id='toolbarpp'></div>");
            string UserId = "";
            w.Append("<div class=' l'>");
            w.Append("<em id='em_tip' class='profile'>");
            try
            {
                UserId = bll.CurrentUserId();
                w.Append("<a id='a_user' href='/myInfo/' target='_self'>" + UserId + ",个人中心</a>");
                w.Append("<a style='cursor:pointer;' onclick='LoginOutAndShuaXin();'>安全退出</a>");
            }
            catch
            {
                w.Append("<a id='a_user' style='cursor:pointer;'  onclick='ToLogin()' target='_self'>未登录,请点击登录</a>");
            }
            w.Append("<input id='hid_userid'  type='hidden' value='" + UserId + "' />");
            w.Append("</em>");
            w.Append("</div>");
            w.Append("</div>");
            w.Append("</div>");
            bool IsLogin = true;
            if (UserId == "")
            {
                IsLogin = false;
            }
            else
            {
                IsLogin = true;
            }
            w.Append("<script> var IsLogin=" + IsLogin.ToString().ToLower() + "; ");
            if (IsLogin)
            {

                StringBuilder s = new StringBuilder();
                s.Append("  select * from dbo.UserVsRole where RoleUserId='" + UserId + "'  ");
                s.Append(" select MerchantId,MerchantName from dbo.MerVsUserView where UserId='"+ UserId +"'  ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dtRole = ds.Tables[0];
                DataTable MyMer = ds.Tables[1];

                
                bool isAdmin = false;
                if (dtRole.Select(" RoleName='超级管理员' ").Length > 0)
                {
                    isAdmin = true;
                }
                string RoleJson = JsonHelper.ToJson(dtRole);
                w.Append(" var myRoleArray= " + RoleJson + "; ");
                w.Append(" var myUserId='" + UserId + "';  ");
                w.Append(" var isAdmin=" + isAdmin.ToString().ToLower() + "; ");
                if (MyMer.Rows.Count > 0)
                {
                    w.Append(" var hasMer=true; ");
                }
                else
                {
                    w.Append("var hasMer=false;  ");
                }
            }
            else
            {
                w.Append(" var myUserId='';  ");
                w.Append(" var myRoleArray = null; ");
                w.Append(" var isAdmin= false; ");
                w.Append(" var myRole = null; ");
                w.Append(" var hasMer=false; ");
            }
            w.Append(" </script> ");
            return w.ToString();
        }


    }
}
