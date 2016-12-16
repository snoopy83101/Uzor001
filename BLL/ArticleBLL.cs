using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

namespace BLL
{

    /// <summary>
    /// 文章资讯类
    /// </summary>
    public class ArticleBLL
    {




        /// <summary>
        /// 是否有权限修改这条新闻
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public bool HasPower(decimal MerId)
        {
            BLL.UserBLL bll = new UserBLL();
            if (bll.IsAdministrator())
            {
                return true;
            }
            string cuserId = bll.CurrentUserId();

            int i = DAL.DalComm.ExInt(" select count(0) from dbo.UserMerRoleView where MerId='" + MerId + "' and UserId='" + cuserId + "' ");
           if (i > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
        }

        public void AddArticleVsImg(Model.ArticleVsImageModel model)
        {
            DAL.ArticleVsImageDAL dal = new DAL.ArticleVsImageDAL();
            dal.Add(model);
           
        }
        /// <summary>
        /// 获得文章列表
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        public DataSet GetArticlePageList(string StrWhere, int CurrentPage)
        {
            DAL.ArticleDAL dal = new DAL.ArticleDAL();
            return dal.GetPageList(StrWhere, CurrentPage, 20);
        }

        public DataSet GetArticleList(string strWhere)
        {
            DAL.ArticleDAL dal = new DAL.ArticleDAL();
            return dal.GetList(strWhere);
        }


        public void DeleteArticleImg(string strWhere)
        {
            DAL.ArticleVsImageDAL dal = new DAL.ArticleVsImageDAL();
            dal.DeleteList(strWhere);
        }

        /// <summary>
        /// 保存一篇文章.
        /// </summary>
        /// <param name="model"></param>
        public void SaveArticle(Model.ArticleModel model)
        {
    
            DAL.ArticleDAL dal = new DAL.ArticleDAL();
            if (model.ArticleId == "")
            {
                model.ArticleId = Common.TimeString.GetNow_ff();
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }
        }



        /// <summary>
        /// 获得一篇文章,以及类别等其他数据
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public DataSet GetArticleById(string ArticleId)
        {

            DAL.ArticleDAL dal = new DAL.ArticleDAL();
            return dal.GetArticleDataById(ArticleId);
        }

        public DataSet GetArticleById(string ArticleId,decimal MerId)
        {

            DAL.ArticleDAL dal = new DAL.ArticleDAL();
            return dal.GetArticleDataById(ArticleId,MerId);
        }


        

    }
}
