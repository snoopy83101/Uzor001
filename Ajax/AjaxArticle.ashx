<%@ WebHandler Language="C#" Class="AjaxArticle" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Common;
using Model;
using System.Transactions;
public class AjaxArticle : Common.BPageSetting2, IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        try
        {
            string para = ReStr("para");
            switch (para)
            {

                case "NextArticleId":
                    NextArticleId();
                    break;
                case "PrevArticleId":
                    PrevArticleId();
                    break;

                case "GetArticleClassList":
                    GetArticleClassList();
                    break;

                case "FlagArticle":
                    FlagArticle();
                    break;

                case "RecommendLv":
                    RecommendLv();
                    break;

                case "GetArticlePageList":
                    GetArticlePageList();  //获得文章列表
                    break;

                case "SaveArticle":
                    SaveArticle();      //保存一个文章
                    break;
                case "GetArticleInfo":
                    GetArticleInfo();      //获得一条文章数据
                    break;

            }
        }
        catch (Exception ex)
        {

            ReThrow(ex);
        }
        context.  Response.End();

    }

    private void PrevArticleId()
    {
        string ArticleId = ReStr("Article", "");
        DAL.ArticleDAL dal = new DAL.ArticleDAL();
        Model.ArticleModel model = dal.GetModel(ArticleId);

        StringBuilder s = new StringBuilder();

        //    s.Append(" select top 1 * from where CreateTime <='"++"' ");

    }

    private void NextArticleId()
    {
        throw new NotImplementedException();
    }

    private void GetArticleClassList()
    {
        DAL.ArticleClassDAL dal = new DAL.ArticleClassDAL();
        int ParentArticleClassId = ReInt("ParentArticleClassId", 0);
        decimal MerId = ReDecimal("MerId", 0);
        StringBuilder s = new StringBuilder();
        if (ParentArticleClassId != 0)
        {
            //取得子类
            s.Append(" ParentArticleClassId='" + ParentArticleClassId + "'  and Invalid=0 ");
        }
        else
        {
            //取得父类
            s.Append("  MerId='" + MerId + "' and Invalid=0 ");

        }
        DataTable dt = dal.GetList(s.ToString()).Tables[0];
        string ArtClassArray = JsonHelper.ToJson(dt);
        ReDict.Add("ArtClassArray", ArtClassArray);
        ReTrue();
    }

    private void RecommendLv()   //推荐这条新闻
    {
        BLL.ArticleBLL bll = new BLL.ArticleBLL();

        if (bll.HasPower(ReDecimal("MerId")))
        {
            //如果有权限
        }
        else
        {
            //如果木有权限

            throw new Exception("你貌似木有这个权限!");
        }

        DAL.DalComm.ExReInt(" update dbo.Article set RecommendLv='" + ReInt("RecommendLv") + "' where ArticleId='" + ReStr("ArticleId") + "'  ");

        ReTrue();
    }




    private void FlagArticle()   //作废这条新闻
    {
        BLL.ArticleBLL bll = new BLL.ArticleBLL();

        if (bll.HasPower(ReDecimal("MerId")))
        {
            //如果有权限
        }
        else
        {
            //如果木有权限

            throw new Exception("你貌似木有这个权限!");
        }

        DAL.DalComm.ExReInt(" update dbo.Article set Invalid=" + ReInt("Invalid") + " where ArticleId='" + ReStr("ArticleId") + "'  ");

        ReTrue();

    }




    /// <summary>
    /// 文章列表
    /// </summary>
    private void GetArticlePageList()
    {

        int ParentArticleClassId = ReInt("ParentArticleClassId", 0);
        int ArticleClassId = ReInt("ArticleClassId", 0);
        int CurrentPage = ReInt("CurrentPage");
        string ArticleTitle = ReStr("ArticleTitle", "");
        decimal MerId = ReDecimal("MerId");
        bool Invalid = ReBool("Invalid", false);
        StringBuilder s = new StringBuilder();
        s.Append(" 1=1 and MerId='" + MerId + "' ");



        s.Append(" and Invalid='" + Invalid + "' ");

        if (ArticleTitle.Trim() != "")
        {

            s.Append(" and ArticleTitle like '%" + ArticleTitle + "%' ");
        }



        if (ArticleClassId != 0)
        { //有类别查询
            s.Append(" and ArticleClassId='" + ArticleClassId + "' ");

        }
        else
        {
            if (ParentArticleClassId == 0)
            {
            }
            else
            {
                s.Append(" and ParentArticleClassId=" + ParentArticleClassId + " ");
            }

        }
        s.Append(" order by RecommendLv desc, createTime desc ");
        BLL.ArticleBLL bll = new BLL.ArticleBLL();
        DataSet ds = bll.GetArticlePageList(s.ToString(), CurrentPage);

        RePage(ds);


    }

    private void GetArticleInfo()
    {
        string ArticleId = ReStr("ArticleId");

        BLL.ArticleBLL bll = new BLL.ArticleBLL();
        DataSet ds = bll.GetArticleById(ArticleId);
        string j = JsonHelper.ToJsonNo1(ds);
        ReDict.Add("ArticleJson", j);
        ReTrue();

    }

    private void SaveArticle()
    {
        Model.ArticleModel model = new Model.ArticleModel();

        model.ArticleId = ReStr("ArticleId");
        model.ArticleTitle = ReStr("ArticleTitle","");
        if (model.ArticleTitle == "")
        {
            throw new Exception("标题不能为空!");
        }
        model.ArticleSummary = ReStr("ArticleSummary","");
        model.ArticleContent = ReStrDeCode("ArticleContent");
        model.ArticleSource = ReStr("ArticleSource");
        model.CreateTime = ReTime("CreateTime");
        model.CreateUser = ReStr("CreateUser","");
        model.ArticleTypeId = ReInt("ArticleTypeId");
        model.ArticleClassId = ReInt("ArticleClassId");
        model.Author = ReStr("Author");
        model.ArticleImgId = ReStr("ArticleImgId","");
        model.Memo = ReStr("Memo","");
        model.Invalid = ReBool("Invalid");
        BLL.ArticleBLL bll = new BLL.ArticleBLL();
        DataTable dtImg = ReTable("imgArray");

        BLL.ImageBLL ibll = new BLL.ImageBLL();


        #region 事务开启
        TransactionOptions transactionOption = new TransactionOptions();
        transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
        {
            #endregion


            if (model.ArticleImgId == "")
            {
                if (dtImg != null && dtImg.Rows.Count > 0)
                {

                    model.ArticleImgId = dtImg.Rows[0]["ImgId"].ToString();
                }
            }


            bll.SaveArticle(model);  //添加文章主题
            bll.DeleteArticleImg("  AricleId='" + model.ArticleId + "' ");  //删除图片关联
            string ArticleImgUrl = ReStr("ArticleImgUrl", "");
            if (dtImg != null)
            {   //添加绑定图片

                //   ArticleImgUrl = dtImg.Rows[0]["url"].ToString();

                foreach (DataRow dr in dtImg.Rows)
                {
                    ArticleVsImageModel AvI = new ArticleVsImageModel();

                    AvI.AricleId = model.ArticleId;
                    AvI.ImgId = dr["ImgId"].ToString();
                    AvI.VsType = "UE";
                    bll.AddArticleVsImg(AvI);
                }
            }
            else
            {

            }




            #region 开始插入新鲜事
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {//新闻不是商家发的


            }
            else
            {
                try
                {

                    string MerName = ReStr("MerName", "");
                    string MerLogoUrl = ReStr("MerLogoUrl", "");

                    Model.DynamicModel dyModel = new DynamicModel();
                    dyModel.DynamicLv = 50;
                    dyModel.DynamicMerId = MerId;
                    dyModel.DynamicTitle = "发布了新文章:'" + Common.StringPlus.GetLeftStr(model.ArticleTitle, 100, "...") + "'";
                    dyModel.DynamicType = "商家文章";
                    dyModel.DynamicUserId = "";

                    Dictionary<string, string> ReXml = new Dictionary<string, string>();
                    ReXml.Add("url", "/MerArticle/?ArticleId=" + model.ArticleId + "");
                    if (Common.FileString.IsFileCunZai(ArticleImgUrl))
                    {
                        ReXml.Add("ArtImgUrl", ArticleImgUrl);
                    }
                    ReXml.Add("MerName", MerName);
                    ReXml.Add("MerLogoUrl", MerLogoUrl);
                    ReXml.Add("ArticleTitle", Common.StringPlus.GetLeftStr(model.ArticleTitle, 100, "..."));

                    dyModel.JsonMemo = XmlHelper.BackXmlStr(ReXml);

                    BLL.CommBLL.AddDynamic(dyModel);
                }
                catch
                {
                    //失败了就算了.   不就是一条动态么


                }



            }

            #endregion

            #region 事务关闭
            transactionScope.Complete();


        }
        #endregion

        ReTrue();

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}