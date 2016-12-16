using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
namespace Manage.Article.PopWindow
{
    public partial class ArtClassInfo : System.Web.UI.Page
    {
        protected string ParentArticleClassJson = "{}";
        protected string ArticleClassJson = "{}";
        protected void Page_Load(object sender, EventArgs e)
        {
            int ParentArticleClassId = Common.PageInput.ReInt("ParentArticleClassId", 0);
            int ArticleClassId = Common.PageInput.ReInt("ArticleClassId", 0);
            DAL.ArticleClassDAL dal = new DAL.ArticleClassDAL();
            DataTable dt;
            if (ArticleClassId != 0)
            {
                dt = dal.GetList("  ArticleClassId='" + ArticleClassId + "' ").Tables[0];
                ArticleClassJson = JsonHelper.ToJsonNo1(dt);
            }
            if (ParentArticleClassId != 0)
            {
                dt = dal.GetList("  ArticleClassId='" + ParentArticleClassId + "' ").Tables[0];
                ParentArticleClassJson = JsonHelper.ToJsonNo1(dt);
            }








        }
    }
}