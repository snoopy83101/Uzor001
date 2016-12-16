using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BJ;
using System.Data;
using System.Text;
using Common;

namespace Manage.Article
{
    public partial class ArticleInfo : ArticleSetting
    {
        protected string ArticleJson = "{}";
        protected string ArticleClassOpHtml = "";
        protected string ArticleClassSels = "";
        protected string imgArray = "[]";
        public ArticleInfo()
            : base(Common.PageInput.ReDecimal("MerId"), Common.PageInput.ReStr("ArticleId", ""))
        {
            //构造
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            StringBuilder w = new StringBuilder();
            DataTable dt = ds_Article.Tables[0];
            DataTable dtClass = ds_Article.Tables[1];
            if (dt.Rows.Count > 0)
            {
                string j = JsonHelper.ToJsonNo1(dt);
                ArticleJson = j;   //给文章JSON赋值

                DataRow dr = dt.Rows[0];
                int ArticleClassId = int.Parse(dr["ArticleClassId"].ToString());
                int ParentArticleClassId = DAL.DalComm.ExInt(" select top 1 ParentArticleClassId from dbo.ArticleClass where  ArticleClassId ='" + ArticleClassId + "'   ");

                if (ParentArticleClassId == 0)
                {
                    //顶级类别




                    foreach (DataRow drClass in dtClass.Rows)   //给该平台的类别选项赋值
                    {

                        w.Append("<option value='" + drClass["ArticleClassId"] + "'>");
                        w.Append(drClass["ArticleClassName"]);
                        w.Append("</option>");

                    }
                    ArticleClassOpHtml = w.ToString();
                }
                else
                {
                    //二级类别
                    foreach (DataRow drClass in dtClass.Rows)   //给该平台的类别选项赋值
                    {
                        if (ParentArticleClassId == int.Parse(drClass["ArticleClassId"].ToString()))
                        {
                            w.Append("<option selected=\"selected\"  value='" + drClass["ArticleClassId"] + "'>");
                        }
                        else
                        {
                            w.Append("<option   value='" + drClass["ArticleClassId"] + "'>");
                        }

                        w.Append(drClass["ArticleClassName"]);
                        w.Append("</option>");

                    }
                    ArticleClassOpHtml = w.ToString();

                }

            }
            else
            {

                foreach (DataRow drClass in dtClass.Rows)   //给该平台的类别选项赋值
                {

                    w.Append("<option value='" + drClass["ArticleClassId"] + "'>");
                    w.Append(drClass["ArticleClassName"]);
                    w.Append("</option>");
                    ArticleClassOpHtml = w.ToString();
                }

            }


            DataTable dtType = ds_Article.Tables[2];

            #region 绑定的图片附件列表


            w.Clear();
            DataTable dtImg = ds_Article.Tables[3];
            if (dtImg.Rows.Count > 0)
            {
                imgArray = JsonHelper.ToJson(dtImg);
            }

            #endregion
        }
    }
}