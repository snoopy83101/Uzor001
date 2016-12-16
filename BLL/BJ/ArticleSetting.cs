using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Common;
using System.Web;

namespace BLL.BJ
{
    public class ArticleSetting : System.Web.UI.Page
    {

        public string MyUserId = "";

        public DataSet ds_Article = null;

        public ArticleSetting(decimal MerId)
        {
            BLL.ArticleBLL bll = new ArticleBLL();
            CheckMerBind(MerId);


            ds_Article = bll.GetArticleById("0", MerId);

        }
        public ArticleSetting(decimal MerId, string ArticleId)
        {
            BLL.ArticleBLL bll = new ArticleBLL();
            CheckMerBind(MerId);
            ds_Article = bll.GetArticleById(ArticleId, MerId);
        }


        public ArticleSetting()
        {


        }



        /// <summary>
        /// 检查商家的绑定, 要求页面必须具备商家编号"MerId"
        /// </summary>
        public void CheckMerBind(decimal MerId)
        {
            BLL.UserBLL ubll = new UserBLL();
            MerchantBLL bll = new MerchantBLL();
            MyUserId = ubll.CurrentUserId();
            DataSet ds = DAL.DalComm.BackData(" select * from dbo.UserMerRoleView with(nolock) where  UserId='" + MyUserId + "' and  MerId= " + MerId + "");
            if (ds.Tables[0].Rows.Count == 0)
            { //这个用户没有这个商家的绑定权限, 这下你大了.
                if (ubll.IsAdministrator())
                {
                    //如果你是超级管理员, 仍没有问题.
                }
                else
                {
                    HttpContext.Current.Response.Write("您是否确定您已经绑定了这个商家?");
                    HttpContext.Current.Response.End();
                }
            }


        }

    }
}
