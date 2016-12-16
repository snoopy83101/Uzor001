using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace BPage
{
    public class BBS : Common.BPageSetting2
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string para = ReStr("para");
                switch (para)
                {
                    case "Ding":
                        Ding();
                        break;
                    case "InvalidTieZi":
                        InvalidTieZi();  //作废一篇帖子
                        break;
                    case "Recommend":
                        Recommend();   //置顶或者取消置顶一篇帖子
                        break;
                    case "ToIndex":
                        ToIndex();
                        break;
                    case "ToYueMingZhong":
                        ToYueMingZhong();
                        break;

                    case "GetHuiYing":
                        GetHuiYing();
                        break;

                    case "GetTieZiInfoPageList":
                        GetTieZiInfoPageList();    //获得帖子主体以及回帖列表
                        break;
                    case "SearchTieZiPageList":
                        SearchTieZiPageList();  //获得帖子列表
                        break;

                    case "SaveTieZiInfo":
                        SaveTieZiInfo();
                        break;

                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();

        }


        private void Ding()
        {
            BLL.BBSBLL bll = new BLL.BBSBLL();

            decimal TieZiId = ReDecimal("TieZiId");
            int i = bll.Ding(TieZiId);
            ReDict2.Add("DingNum", i.ToString());
            ReTrue();
        }

        private void ToIndex()
        {
            BLL.BBSBLL bll = new BLL.BBSBLL();

            decimal TieZiId = ReDecimal("TieZiId");
            bool IsIndex = ReBool("IsIndex", true);
            if (bll.HasTieZiPower(TieZiId))
            {
                string YueMingZhong = ReStr("YueMingZhong");
                StringBuilder s = new StringBuilder();
                s.Append(" update  BBS.DBO.TIEZI set IsIndex='" + IsIndex + "' where TieZiId='" + TieZiId + "' ");

                DAL.DalComm.ExReInt(s.ToString());

                ReTrue();
            }
            else
            {
                throw new Exception("您好像没有权限执行这个操作!");
            }
        }

        private void ToYueMingZhong()
        {
            BLL.BBSBLL bll = new BLL.BBSBLL();
            decimal TieZiId = ReDecimal("TieZiId");

            if (bll.HasTieZiPower(TieZiId))
            {
                string YueMingZhong = ReStr("YueMingZhong");
                StringBuilder s = new StringBuilder();
                s.Append(" update  BBS.DBO.TIEZI set YueMingZhong='" + YueMingZhong + "' where TieZiId='" + TieZiId + "' ");

                DAL.DalComm.ExReInt(s.ToString());

                ReTrue();
            }
            else
            {
                throw new Exception("您好像没有权限执行这个操作!");
            }

        }

        private void Recommend()
        {
            BLL.BBSBLL bll = new BLL.BBSBLL();
            decimal TieZiId = ReDecimal("TieZiId");
            if (bll.HasTieZiPower(TieZiId))
            {
                StringBuilder s = new StringBuilder();
                int RecommendLv = ReInt("RecommendLv");
                s.Append(" update BBS.dbo.TieZi  set RecommendLv='" + RecommendLv + "' where TieZiId='" + TieZiId + "'  ");



                DAL.DalComm.ExReInt(s.ToString());

                ReTrue();
            }
            else
            {
                throw new Exception("您好像没有权限!");
            }

        }

        private void InvalidTieZi()
        {

            BLL.BBSBLL bll = new BLL.BBSBLL();


            decimal TieZiId = ReDecimal("TieZiId");

            if (bll.HasTieZiPower(TieZiId))
            {
                bool Invalid = ReBool("Invalid", true);

                StringBuilder s = new StringBuilder();

                s.Append(" update BBS.dbo.TieZi  set Invalid ='" + Invalid + "' where TieZiId='" + TieZiId + "' ");

                DAL.DalComm.ExReInt(s.ToString());
            }
            else
            {
                throw new Exception("你好像木有权限这么做!");
            }



            ReTrue();
        }



        private void GetHuiYing()
        {
            decimal ParentTieZiId = ReDecimal("ParentTieZiId");

            int PageInt = ReInt("PageInt", 5);
            string cols = ReStr("cols", "*");
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" and ParentTieZiId ='" + ParentTieZiId + "' ");
            s.Append(" order by createTime ");
            int CurrentPage = ReInt("CurrentPage");
            BLL.BBSBLL bll = new BLL.BBSBLL();
            DataSet ds = bll.GetTieZiPageList(s.ToString(), CurrentPage, PageInt, cols);
            RePage(ds);
        }

        private void GetTieZiInfoPageList()
        {
            decimal TieZiId = ReDecimal("TieZiId");
            int CurrentPage = ReInt("CurrentPage");
            bool AddHot = ReBool("AddHot", true);
            bool OnlyLz = ReBool("OnlyLz", false);
            StringBuilder s = new StringBuilder();

            s.Append(" TieZiId='" + TieZiId + "' ");

            s.Append("  or ParentTieZiId='" + TieZiId + "' ");

            s.Append(" and Invalid=0 ");
            s.Append(" order by CHARINDEX(CONVERT(VARCHAR(20),TieZiId) , '" + TieZiId + "') desc, ");
            if (OnlyLz)
            {
                string lz = DAL.DalComm.ExStr(" select CreateUser from BBS.dbo.TieZi where TieZiId='" + TieZiId + "'  ");
                s.Append("CHARINDEX(CONVERT(VARCHAR(50),CreateUser) , '" + lz + "') desc,");
            }

            s.Append(" CreateTime   ");

            BLL.BBSBLL bll = new BLL.BBSBLL();
            if (AddHot)
            {
                bll.AddHot(TieZiId);
            }
            DataSet ds = bll.GetTieZiPageList(s.ToString(), CurrentPage, 10, "*");

            DataTable dt = ds.Tables[2];

            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("huiying");
                s.Clear();
                List<string> ls = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (i != 0)
                    {
                        ls.Add(" select top 5 * from BBS.dbo.TieZiView where  ParentTieZiId='" + dr["TieZiId"] + "' ");
                    }
                }

                string sqlstr = string.Join(" UNION ALL ", ls);


                if (sqlstr.Trim() != "")
                {
                    DataTable dtHuiYing = DAL.DalComm.BackData(sqlstr).Tables[0];


                    foreach (DataRow dr in dt.Rows)
                    {

                        StringBuilder w = new StringBuilder();
                        DataTable dthy = Common.DataSetting.TableSelect(" ParentTieZiId='" + dr["TieZiId"] + "' ", dtHuiYing);
                        dr["huiying"] = JsonHelper.ToJson(dthy);
                    }
                }



            }

            RePage(ds);

        }

        private void SaveTieZiInfo()
        {

            BLL.BBSBLL bbll = new BLL.BBSBLL();


            Model.TieZiModel model = new TieZiModel();
            model.TieZiId = ReDecimal("TieZiId", 0);

            if (bbll.HasTieZiPower(model.TieZiId))
            {
                //是否有操作权限
            }
            else
            {
                throw new Exception("您没有操作权限!");
            }

            if (model.TieZiId > 0)
            {//修改 

                model = bbll.GetTieZiModel(model.TieZiId);
                model.CreateTime = ReTime("CreateTime", DateTime.Now);

            }
            else
            {
                //新增
                model.Ip = ReStr("Ip", HttpContext.Current.Request.UserHostAddress);
                model.CreateUser = Common.CookieSings.GetCurrentUserId();
                model.Source = ReStr("Source", "");
                model.HideUser = ReBool("HideUser");
                model.WxOpenId = ReStr("WxOpenId", "");
                model.YueMingZhong = "";
                model.RecommendLv = ReInt("RecommendLv", 0);
            }


            model.TieZiTitle = ReStr("TieZiTitle", "");
            bool AddHot = ReBool("AddHot", false);
            model.TieZiContent = ReStrDeCode("TieZiContent");

            model.TieZiSummary = Common.StringPlus.GetLeftStr(Common.StringPlus.OutHtmlText(model.TieZiContent), 120, "");



            model.TieZiImgId = ReStr("TieZiImgId", "");
            model.MiniImgUrl = ReStr("MiniImgUrl", "");

            model.ForumId = ReDecimal("ForumId");
            model.TieZiType = ReStr("TieZiType", "");
            model.TieZiClass = ReStr("TieZiClass", "");


            model.ParentTieZiId = ReDecimal("ParentTieZiId", 0);


            model.RepLastUser = model.CreateUser;
            model.Invalid = ReBool("Invalid", false);
            DataTable dtImg = ReTable("imgArray");

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
            #endregion
                dtImg = BLL.BJ.ImgSetting.ImgArraySetting(dtImg, model.TieZiContent);
                if (dtImg != null)
                {
                    model.TieZiImgId = dtImg.Rows[0]["ImgId"].ToString();

                }


                bbll.SaveTieZi(model);
                bbll.DeleteTieZiVsImgByTieZiId(model.TieZiId);              //删除帖子下所有的图片,重新插入.
                if (dtImg != null)
                {
                    foreach (DataRow dr in dtImg.Rows)
                    {
                        Model.TieZiVsImgModel VsImgModel = new TieZiVsImgModel();
                        VsImgModel.TieZiId = model.TieZiId;
                        VsImgModel.ImgId = dr["ImgId"].ToString();
                        VsImgModel.vsType = "UserImg";
                        bbll.AddTieZiVsImg(VsImgModel);
                    }
                }
                ReDict2.Add("ForumId", model.ForumId.ToString());
                ReDict2.Add("TieZiId", model.TieZiId.ToString());

                if (model.ParentTieZiId > 0)
                {
                    int ParentRepCount = DAL.DalComm.ExInt(" select RepCount from BBS.DBO.TIEZI WHERE TieZiId='" + model.ParentTieZiId + "'  ");
                    ReDict2.Add("ParentRepCount", ParentRepCount.ToString());
                    #region 开始提醒
                    RemindModel remindmodel = new RemindModel();
                    remindmodel.CreateTime = DateTime.Now;
                    remindmodel.MerLook = false;
                    remindmodel.ReKey = model.ParentTieZiId.ToString();
                    remindmodel.ReMerchantId = 0;
                    remindmodel.RemindTitle = "您发布的帖子有了新的回应!";
                    remindmodel.RemindTypeId = model.TieZiType;
                    remindmodel.ReUserId = ReStr("ReUserId", "");
                    remindmodel.Url = "/t/?TieZiId=" + ReDecimal("ReTieZiId") + "";


                    remindmodel.UserLook = false;


                    BLL.CommBLL commBll = new BLL.CommBLL();

                    if (remindmodel.ReUserId.Trim() != "")
                    {
                        commBll.SaveReMind(remindmodel);
                    }






                    #endregion
                    #region 如果是回帖,更新主贴时间

                    if (model.TieZiType == "回帖")
                    {
                        //说明本帖是个回帖,更新回帖数目, 和最后发表人
                        int i = DAL.DalComm.ExReInt(" update bbs.dbo.TieZi set UpdateTime ='" + DateTime.Now.ToString() + "' , RepLastUser='" + model.CreateUser + "'  where TieZiId='" + model.ParentTieZiId + "'  ");
                        if (i <= 0)
                        {
                            throw new Exception("没有找到ID为" + model.ParentTieZiId + "的主贴!");
                        }
                    }
                    #endregion
                }
                #region 事务结束

                transactionScope.Complete();


            }
                #endregion
            ReTrue();



        }

        private void SearchTieZiPageList()
        {

            int CurrentPage = ReInt("CurrentPage", 1);
            decimal ForumId = ReDecimal("ForumId");
            int MaxPage = ReInt("MaxPage", 30);  //每一页显示的行数
            string cols = ReStr("cols", " * ");  //所有列名, 如果没有获取则为全部列
            bool Invalid = ReBool("Invalid", false);
            BLL.BBSBLL bll = new BLL.BBSBLL();
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            if (ForumId != 0)
            {
                s.Append(" and ForumId='" + ForumId + "' ");
            }
            else
            {

            }
            s.Append(" and TieZiType='主贴' ");
            s.Append(" and Invalid='" + Invalid + "' ");
            s.Append("  ORDER BY RecommendLv desc, UpdateTime desc ");


            DataSet ds = bll.GetTieZiPageList(s.ToString(), CurrentPage, MaxPage, cols);

            DataTable dt = ds.Tables[2];

            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("huitie");
                s.Clear();
                List<string> ls = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    ls.Add(" select * from ( select top 3 " + cols + " from BBS.dbo.TieZiView WITH(NOLOCK) where  ParentTieZiId='" + dr["TieZiId"] + "' and Invalid = 0 order by CreateTime desc )   a" + i + "   ");
                }
                string sqlstr = string.Join(" UNION ALL ", ls);
                if (sqlstr.Trim() != "")
                {
                    DataTable dtHuiYing = DAL.DalComm.BackData(sqlstr).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder w = new StringBuilder();
                        DataTable dthy = Common.DataSetting.TableSelect(" ParentTieZiId='" + dr["TieZiId"] + "' ", dtHuiYing);
                        dr["huitie"] = JsonHelper.ToJson(dthy);
                    }
                }



            }

            RePage(ds);
        }

    }
}
