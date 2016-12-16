using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
namespace Manage.Article
{
    public partial class ArticleList : BLL.BJ.ArticleSetting
    {
        public ArticleList()
          : base(Common.PageInput.ReDecimal("MerId"))
        {

        }
        protected string ArticleClassOpHtml = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable dtClass = ds_Article.Tables[1];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dtClass.Rows)
            {
                w.Append("<option value='" + dr["ArticleClassId"] + "' >");
                w.Append(dr["ArticleClassName"]);
                w.Append("</option>");

            }
            ArticleClassOpHtml = w.ToString();

        }
    }
}