using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using Common;
using System.Text;
using System.Transactions;
using Model;

public partial class AjaxWx : BPageSetting
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string para = ReStr("para");
            switch (para)
            {

                #region 普通微信

                case "InvalidWxSendInfo":
                    InvalidWxSendInfo();
                    break;

                case "InvalidWxPt":
                    InvalidWxPt();
                    break;

                case "InvalidSuCai":
                    InvalidSuCai();
                    break;


                case "GetWxKeyTypeDetailSelectData":
                    GetWxKeyTypeDetailSelectData();
                    break;

                case "BindSuCaiInfo":
                    BindSuCaiInfo();  //绑定素材Info
                    break;

                case "GetSuCaiPageList":
                    GetSuCaiPageList();
                    break;

                case "SaveSuCaiInfo":
                    SaveSuCaiInfo();
                    break;

                case "SearchSuCaiList":
                    SearchSuCaiList();
                    break;

                case "GetWxPtMenu":
                    GetWxPtMenu();  //取得微信公众平台菜单
                    break;

                case "PostWxPtMenu":
                    PostWxPtMenu();
                    break;


                case "GetSendInfo":
                    GetSendInfo();
                    break;

                case "SearchSendPageList":

                    SearchSendPageList();
                    break;

                case "SaveSendInfo":
                    SaveSendInfo();
                    break;

                case "SearchWxPrPageList":
                    SearchWxPrPageList();

                    break;

                case "SaveWxPtInfo":
                    SaveWxPtInfo();
                    break;


                case "GetWxPtInfo":
                    GetWxPtInfo();
                    break;


                #endregion


                #region 企业微信

                case "GetQyWxPtList":
                    GetQyWxPtList();
                    break;

                case "GetQyWxPtInfo":
                    GetQyWxPtInfo();
                    break;
                case "SaveQyWxPtInfo":
                    SaveQyWxPtInfo();
                    break;

                case "GetQyWxPtAppInfo":
                    GetQyWxPtAppInfo();
                    break;
                case "GetQyWxPtAppList":
                    GetQyWxPtAppList();
                    break;

                case "SaveQyWxPtAppInfo":
                    SaveQyWxPtAppInfo();
                    break;


                case "GetQyWxPtGroupList":
                    GetQyWxPtGroupList();
                    break;

                case "GetQyWxPtGroupInfo":
                    GetQyWxPtGroupInfo();
                    break;


                case "SaveQyWxPtGroupInfo":
                    SaveQyWxPtGroupInfo();
                    break;

                case "GetQyWxPtVsUserList":
                    GetQyWxPtVsUserList();
                    break;

                case "SaveWxPtGroupVsUser":

                    SaveWxPtGroupVsUser();
                    break;

                case "SaveQyWxPtVsUser":
                    SaveQyWxPtVsUser();           //企业微信与用户的关联表
                    break;


                case "SendPeiHuoRemind":    //通知配货人员进行订单配货
                    SendPeiHuoRemind();
                    break;
                    #endregion

            }
        }
        catch (Exception ex)
        {
            ReThrow(ex);
            Response.End();

        }
    }

    private void GetQyWxPtList()
    {

        decimal MerId = ReDecimal("MerId", 0);
        DataSet ds = DAL.DalComm.BackData(" SELECT * FROM dbo.QyWxPt WHERE MerId='" + MerId + "' ");

        DataTable dt = ds.Tables[0];

        ReDict.Add("list", JsonHelper.ToJson(dt));
        ReTrue();

    }

    private void SendPeiHuoRemind()  //配货人员提醒
    {
        BLL.WxBLL wbll = new BLL.WxBLL();
        string DingDanId = ReStr("DingDanId", "");
        DataTable dtUser = ReTable("UserList");
        string SendMemo = ReStr("SendMemo", "");
        decimal QyWxPtAppId = ReDecimal("QyWxPtAppId", 0);


        StringBuilder w = new StringBuilder();
        StringBuilder s = new StringBuilder();
        List<string> ToUserList = new List<string>();

        #region 事务开启

        TransactionOptions transactionOption = new TransactionOptions();
        transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
        {
            #endregion

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.ShenHeDingdan(DingDanId, 1, "通过审核");
            s.Append("SELECT * FROM dbo.DingDanDetailView WITH(NOLOCK) WHERE DingDanId='" + DingDanId + "' ORDER BY DingDanDetailTypeId ");
            s.Append(" SELECT *  FROM dbo.DingDanView  WITH(NOLOCK) WHERE DingDanId='" + DingDanId + "' ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtDetail = ds.Tables[0];
            DataTable dtInfo = ds.Tables[1];
            if (dtInfo.Rows.Count != 1)
            {
                throw new Exception("这是个什么订单?");
            }
            DataRow drInfo = dtInfo.Rows[0];
            decimal MerId = decimal.Parse(drInfo["MerchantId"].ToString());
            decimal amount = decimal.Parse(drInfo["Amount"].ToString());
            DateTime PeiSongTime1 = DateTime.Parse(drInfo["PeiSongTime1"].ToString());
            DateTime PeiSongTime2 = DateTime.Parse(drInfo["PeiSongTime2"].ToString());
            amount = Math.Round(amount, 2);
            w.Append("编号:\n " + DingDanId + " \n");

            if (SendMemo.Trim() != "")
            {
                w.Append("调度备注:" + SendMemo + " \n");
            }

            w.Append("总金额:" + amount + " \n");
            w.Append("物流:" + PeiSongTime1.ToString("yy年MM月dd日 hh时mm分") + "-" + PeiSongTime2.ToString("yy年MM月dd日 hh时mm分") + "");
            w.Append("(" + drInfo["PeiSongTypeName"] + ") \n");
            var url = "http://www.sjdfcs.com/App/AppDingDanList_PeiHuo.aspx?DingDanId=" + DingDanId + "";

            w.Append("<a href='" + wbll.QyWxUrl(url, MerId) + "' >查看详细</a> \n");
            if (dtDetail.Rows.Count == 0)
            {
                throw new Exception("这个订单[" + DingDanId + "]没有明细吗?");
            }
            s.Clear();
            foreach (DataRow drUser in dtUser.Rows)
            {
                ToUserList.Add(drUser["QyWxPtUserId"].ToString());
                s.Append(" UPDATE dbo.DingDanInfo SET PeiHuoUserId='" + drUser["UserId"].ToString() + "',Status=20 WHERE DingDanId='" + DingDanId + "' ");
            }
            DAL.DalComm.ExReInt(s.ToString());





            #region 暂时无用

            //foreach (DataRow drDetail in dtDetail.Rows)
            //{


            //    decimal Price = decimal.Parse(drDetail["Price"].ToString());
            //    Price = Math.Round(Price, 2);
            //    decimal Quantity = decimal.Parse(drDetail["Quantity"].ToString());
            //    Quantity = Math.Round(Quantity);

            //    string 规格 = drDetail["规格"].ToString().Trim();

            //    int DingDanDetailTypeId = int.Parse(drDetail["DingDanDetailTypeId"].ToString());
            //    if (DingDanDetailTypeId == 10)
            //    {

            //        w.Append("配送费:" + Price + "元 \n");

            //    }
            //    else
            //    {


            //        //如果是产品
            //        w.Append("[" + drDetail["产品编号"] + "]");
            //        w.Append("" + drDetail["产品名称"] + ",");
            //        if (规格 != "")
            //        {
            //            w.Append("规格:" + drDetail["规格"] + ",");

            //        }

            //        w.Append("单价:" + Price + "元,");
            //        w.Append("数量:" + Quantity + "" + drDetail["计量单位"] + "");
            //        w.Append(" \n ");
            //    }
            //}
            #endregion




            #region 事务关闭

            transactionScope.Complete();


        }
        #endregion
        string jsonStr = wbll.SendQyTextMsg(ToUserList, w.ToString(), QyWxPtAppId);

        ReDict.Add("ReJson", jsonStr);
        ReTrue();

    }

    private void SaveQyWxPtVsUser()
    {
        Model.QyWxPtVsUserModel model = new QyWxPtVsUserModel();
        model.UserId = ReStr("uid");
        model.QyWxPtId = ReDecimal("QyWxPtId");
        model.QyWxPtUserId = ReStr("QyWxPtUserId");
        model.Memo = ReStr("Memo");
        BLL.WxBLL wbll = new BLL.WxBLL();
        wbll.SaveWxPtGroupVsUser(model);
        ReTrue();
    }

    private void SaveWxPtGroupVsUser()
    {

        //给管理组添加成员, 暂时无用.
    }

    private void GetQyWxPtVsUserList()    //微信平台和用户的关系 必须带有MerId否则无意义
    {
        StringBuilder s = new StringBuilder();
        BLL.WxBLL wbll = new BLL.WxBLL();

        decimal QyWxPtId = ReDecimal("QyWxPtId", 0);
        string UserIdStr = ReStr("UserIdStr", "");
        int CurrentPage = ReInt("CurrentPage", 1);
        decimal MerId = ReDecimal("MerId");
        s.Append(" 1=1 ");
        if (UserIdStr != "")
        {
            s.Append(" and UserId like='%" + UserIdStr + "%'  ");
        }

        s.Append(" and MerId=" + MerId + " ");
        DataSet ds = wbll.GetQyWxPtVsUserList(s.ToString(), " UserId desc ", CurrentPage, 20, " * ");
        RePage2(ds);

    }

    private void SaveQyWxPtGroupInfo()
    {
        Model.QyWxPtGroupModel model = new QyWxPtGroupModel();
        BLL.WxBLL wbll = new BLL.WxBLL();
        model.QyWxPtGroupId = ReDecimal("QyWxPtGroupId", 0);
        if (model.QyWxPtGroupId != 0)
        {
            model = wbll.GetQyWxPtGroupModel(model.QyWxPtGroupId);

        }

        model.QyWxPtId = ReDecimal("QyWxPtId", 0);
        model.Secret = ReStr("Secret");
        model.QyWxPtGroupMemo = ReStr("QyWxPtGroupMemo");

        wbll.SaveQyWxPtGroupInfo(model);
        ReDict2.Add("QyWxPtGroupId", model.QyWxPtGroupId.ToString());

        ReTrue();
    }

    private void GetQyWxPtGroupInfo()
    {
        Model.QyWxPtGroupModel model = new QyWxPtGroupModel();
        BLL.WxBLL wbll = new BLL.WxBLL();
        model.QyWxPtGroupId = ReDecimal("QyWxPtGroupId", 0);

        DataSet ds = wbll.GetQyWxPtGorupInfo(model.QyWxPtGroupId);
        DataTable dt = ds.Tables[0];
        ReDict.Add("info", JsonHelper.ToJsonNo1(dt));
        ReTrue();
    }

    private void GetQyWxPtGroupList()
    {
        StringBuilder s = new StringBuilder();
        decimal QyWxPtId = ReDecimal("QyWxPtId", 0);

        if (QyWxPtId != 0)
        {
            s.Append(" QyWxPtId=" + QyWxPtId + "  ");

        }

        DAL.QyWxPtGroupDAL dal = new DAL.QyWxPtGroupDAL();
        DataSet ds = dal.GetList(s.ToString());
        DataTable dt = ds.Tables[0];
        ReDict.Add("list", JsonHelper.ToJson(dt));
        ReTrue();

    }

    private void GetQyWxPtAppList()   //取得应用列表
    {
        StringBuilder s = new StringBuilder();
        decimal QyWxPtId = ReDecimal("QyWxPtId", 0);

        if (QyWxPtId != 0)
        {
            s.Append(" QyWxPtId=" + QyWxPtId + "  ");

        }
        else
        {
            throw new Exception("必须明确一个企业微信平台!");
        }

        DAL.QyWxPtAppDAL dal = new DAL.QyWxPtAppDAL();
        DataSet ds = dal.GetList(" QyWxPtId='" + QyWxPtId + "' ");
        DataTable dt = ds.Tables[0];
        ReDict.Add("list", JsonHelper.ToJson(dt));
        ReTrue();


    }

    private void SaveQyWxPtAppInfo()
    {
        BLL.WxBLL wbll = new BLL.WxBLL();
        Model.QyWxPtAppModel model = new QyWxPtAppModel();
        model.QyWxPtAppId = ReDecimal("", 0);

        model.AgentID = ReStr("AgentID", "");
        model.DefaultGroupId = ReDecimal("DefaultGroupId", 0);
        model.QyWxPtAppMemo = ReStr("QyWxPtAppMemo", "");
        model.QyWxPtAppName = ReStr("QyWxPtAppName", "");
        model.QyWxPtId = ReDecimal("QyWxPtId", 0);
        if (model.QyWxPtId == 0)
        {
            throw new Exception("必须确定企业微信平台!");
        }
        wbll.SaveQyWxPtAppInfo(model);
        ReDict2.Add("QyWxPtAppId", model.QyWxPtAppId.ToString());
        ReTrue();
    }

    private void GetQyWxPtAppInfo()
    {
        throw new NotImplementedException();
    }

    private void SaveQyWxPtInfo()
    {


        BLL.WxBLL wbll = new BLL.WxBLL();
        Model.QyWxPtModel model = new QyWxPtModel();
        model.QyWxPtId = ReDecimal("QyWxPtId", 0);
        if (model.QyWxPtId != 0)
        {
            model = wbll.GetQyWxPtModel(model.QyWxPtId);
        }
        model.CorpId = ReStr("CorpId", "");
        model.MerId = ReDecimal("MerId", 0);
        model.OrderNo = ReInt("OrderNo", 1);

        model.QyWxPtMemo = ReStr("QyWxPtMemo", "");
        model.QyWxPtName = ReStr("QyWxPtName", "");


        wbll.SaveQyWxPtInfo(model);
        ReTrue();
    }

    private void GetQyWxPtInfo()
    {
        BLL.WxBLL wbll = new BLL.WxBLL();
        decimal QyWxPtId = ReDecimal("QyWxPtId", 0);
        DataSet ds = wbll.GetQyWxPtInfo(QyWxPtId);
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count == 0)
        {
            throw new Exception("企业微信号不存在!");
        }

        ReDict.Add("QyWxPt", JsonHelper.ToJsonNo1(dt));
        ReTrue();

    }


    private void InvalidWxPt()
    {

        bool Invalid = ReBool("Invalid", true);
        decimal WxPtId = ReDecimal("WxPtId");
        BLL.WxBLL wbll = new BLL.WxBLL();
        wbll.InvalidWxPt(Invalid, WxPtId);
        ReTrue();

    }


    private void InvalidWxSendInfo()
    {

        bool Invalid = ReBool("Invalid", true);
        decimal WxSendId = ReDecimal("WxSendId");
        BLL.WxBLL wbll = new BLL.WxBLL();
        wbll.InvalidWxSendInfo(Invalid, WxSendId);
        ReTrue();

    }

    private void InvalidSuCai()
    {
        bool Invalid = ReBool("Invalid", true);
        decimal WxSuCaiInfoId = ReDecimal("WxSuCaiInfoId");
        BLL.WxBLL wbll = new BLL.WxBLL();

        wbll.InvalidSuCai(Invalid, WxSuCaiInfoId);
        ReTrue();
    }

    private void GetWxKeyTypeDetailSelectData()
    {
        BLL.WxBLL bll = new BLL.WxBLL();
        string KeyTypeId = ReStr("KeyTypeId", "");
        DataTable dt = bll.GetWxKeyTypeDetailSelectData(KeyTypeId);

        ReDict.Add("jr", JsonHelper.ToJson(dt));
        ReTrue();
    }

    private void BindSuCaiInfo()
    {
        decimal WxSuCaiInfoId = ReDecimal("WxSuCaiInfoId", 0);

        if (WxSuCaiInfoId == 0)
        {
            throw new Exception("没有确定WxSuCaiInfoId的值!");
        }
        StringBuilder s = new StringBuilder();

        s.Append(" select * from dbo.WxSuCaiInfo where WxSuCaiInfoId='" + WxSuCaiInfoId + "' ");

        s.Append(" select * from dbo.WxSuCaiDetailView where WxSuCaiInfoId='" + WxSuCaiInfoId + "' ");

        DataSet ds = DAL.DalComm.BackData(s.ToString());
        ReDict.Add("Info", JsonHelper.ToJsonNo1(ds.Tables[0]));
        ReDict.Add("Detail", JsonHelper.ToJson(ds.Tables[1]));
        ReTrue();
    }

    private void GetSuCaiPageList()
    {

        Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();
        string inputStr = ReStr("inputStr", "");
        decimal MerId = ReDecimal("MerId", cm.CurrentMerId);
        bool Invalid = ReBool("Invalid", false);
        int c = ReInt("c", 1);

        BLL.WxBLL bll = new BLL.WxBLL();

        DataSet ds = bll.GetSuCaiPageList(" Invalid='" + Invalid + "'  and MerId='" + MerId + "' and WxSuCaiTitle like '%" + inputStr + "%' ", c, 20, "*");
        RePage(ds);
    }

    private void SaveSuCaiInfo()
    {

        Model.WxSuCaiInfoModel model = new WxSuCaiInfoModel();
        BLL.WxBLL wbll = new BLL.WxBLL();
        Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();




        #region 事务开启

        TransactionOptions transactionOption = new TransactionOptions();
        transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
        {
            #endregion


            model.WxSuCaiInfoId = ReDecimal("WxSuCaiInfoId", 0);

            if (model.WxSuCaiInfoId == 0)
            {

                //新增

            }
            else
            {
                model = wbll.GetSuCaiInfoModel(model.WxSuCaiInfoId);

            }

            model.WxSuCaiTitle = ReStr("WxSuCaiTitle");
            model.InputCode = ReStr("InputCode", Common.PinYin.GetCodstring(model.WxSuCaiTitle));

            model.WxSuCaiContent = ReStrDeCode("WxSuCaiContent");
            model.Memo = ReStr("Memo", "");
            model.WxSuCaiTypeId = ReInt("WxSuCaiTypeId");
            model.WxSuCaiClassId = ReInt("WxSuCaiClassId");
            model.CreateUser = cm.CurrentUserId;
            model.Invalid = ReBool("Invalid", false);
            model.FmImgId = ReStr("FmImgId", "");
            model.MerId = ReDecimal("MerId", cm.CurrentMerId);




            wbll.SaveSuCaiInfo(model);

            switch (model.WxSuCaiClassId)
            {

                case 2:
                    //如果是图文
                    wbll.RemoveSuCaiDetail(" WxSuCaiInfoId='" + model.WxSuCaiInfoId + "' ");
                    DataTable dtDetail = ReTable("detail");

                    if (dtDetail.Rows.Count == 0)
                    {
                        throw new Exception("图文回复需要至少一条明细!");
                    }

                    foreach (DataRow drDetail in dtDetail.Rows)
                    {

                        Model.WxSuCaiDetailModel DetailModel = new WxSuCaiDetailModel();

                        DetailModel.ImgId = drDetail["ImgId"].ToString();
                        DetailModel.OrderNo = int.Parse(drDetail["OrderNo"].ToString());
                        DetailModel.OtherPara = drDetail["OtherPara"].ToString();
                        DetailModel.ReKey = drDetail["ReKey"].ToString();
                        DetailModel.Url = HttpUtility.UrlDecode(drDetail["Url"].ToString());
                        DetailModel.WxSuCaiInfoId = model.WxSuCaiInfoId;
                        DetailModel.WxSuCaiDetailMemo = drDetail["WxSuCaiDetailMemo"].ToString();
                        try
                        {
                            DetailModel.WxSuCaiDetailClassId = int.Parse(drDetail["WxSuCaiDetailClassId"].ToString());
                        }
                        catch
                        {
                            continue;
                        }


                        try
                        {
                            DetailModel.WxSuCaiDetailContent = drDetail["WxSuCaiDetailContent"].ToString();
                        }
                        catch
                        {

                            DetailModel.WxSuCaiDetailContent = "";
                        }

                        DetailModel.WxSuCaiDetailId = 0;  //必须的,就靠它新增
                        DetailModel.WxSuCaiDetailTitle = drDetail["WxSuCaiDetailTitle"].ToString();
                        wbll.SaveSuCaiDetail(DetailModel);
                    }
                    break;

                default:
                    //其他


                    break;
            }










            #region 事务关闭

            transactionScope.Complete();


        }

        #endregion
        ReDict2.Add("WxSuCaiInfoId", model.WxSuCaiInfoId.ToString());
        ReTrue();
    }




    private void SearchSuCaiList()
    {
        BLL.WxBLL bll = new BLL.WxBLL();
    }

    private void PostWxPtMenu()
    {
        BLL.WxBLL bll = new BLL.WxBLL();
        decimal WxPtId = ReDecimal("WxPtId");
        string mj = ReStrDeCode("mj");
        string re = bll.PostWxPtMenuJson(WxPtId, mj);
        ReTrue(re);
    }

    private void GetWxPtMenu()
    {

        BLL.WxBLL bll = new BLL.WxBLL();
        decimal WxPtId = ReDecimal("WxPtId");

        string mj = bll.GetWxPtMenuJson(WxPtId);

        ReTrue(mj);

    }


    private void GetSendInfo()
    {
        decimal WxSendId = ReDecimal("WxSendId", 0);
        if (WxSendId == 0)
        {
            throw new Exception("WxSendId不能为空");

        }
        BLL.WxBLL bll = new BLL.WxBLL();

        DataSet ds = bll.GetWxSendInfo(WxSendId);

        DataTable dtSendInfo = ds.Tables[0];
        DataTable dtKeyList = ds.Tables[1];

        ReDict.Add("SendInfo", JsonHelper.ToJsonNo1(dtSendInfo));
        ReDict.Add("KeyList", JsonHelper.ToJson(dtKeyList));
        ReTrue();
    }

    private void SearchSendPageList()
    {
        decimal WxPtId = ReDecimal("WxPtId", 0);

        int c = ReInt("CurrentPage", 1);
        string inputStr = ReStr("inputStr", "");

        bool Invalid = ReBool("Invalid", false);

        BLL.WxBLL bll = new BLL.WxBLL();

        if (WxPtId == 0)
        {

            throw new Exception("没有确定当前的微信公众平台!");
        }

        StringBuilder s = new StringBuilder();

        s.Append(" 1=1 ");
        s.Append(" and Invalid='" + Invalid + "' ");
        s.Append(" and WxPtId='" + WxPtId + "' ");
        if (inputStr.Trim() != "")
        {
            s.Append(" and WxSendTitle like '%" + inputStr + "%' ");
        }
        DataSet ds = bll.SearchSendPageList(c, 20, s.ToString(), "*");
        RePage(ds);

    }

    private void SaveSendInfo()
    {

        #region 事务开启

        TransactionOptions transactionOption = new TransactionOptions();
        transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
        {
            #endregion

            Model.WxSendInfoModel model = new WxSendInfoModel();
            BLL.WxBLL bll = new BLL.WxBLL();
            model.WxSendId = ReDecimal("WxSendId", 0);

            if (model.WxSendId == 0)
            {
                model.WxPtId = ReDecimal("WxPtId");
            }
            else
            {
                //如果是修改, 选取出原来的数据
                model = bll.GetWxSendModel(model.WxSendId);
            }
            model.WxSendTitle = ReStr("WxSendTitle");
            model.WxSendType = ReStr("WxSendType");
            model.WxSendClassId = ReInt("WxSendClassId");
            model.Memo = ReStr("Memo");
            model.FmImgId = ReStr("FmImgId");
            model.WxSuCaiId = ReDecimal("WxSuCaiId", 0);
            model.SendContent = ReStrDeCode("SendContent");
            bll.SaveSendInfo(model);



            DataTable dtKeys = ReTable("KeysAreray");

            if (dtKeys.Rows.Count == 0)
            {
                throw new Exception("没有添加激发时间, 这个回应没有触发将没有意义!");
            }


            DAL.WxKeyDAL keyDal = new DAL.WxKeyDAL();
            keyDal.DeleteList(" WxSendId='" + model.WxSendId + "' ");  //首先删除所有的key关联关系

            foreach (DataRow drKeys in dtKeys.Rows)
            {


                if (model.WxSendId == 0)
                {
                    throw new Exception("在执行插入之后,WxSendId不能为0!");
                }

                Model.WxKeyModel KeyModel = new WxKeyModel();
                KeyModel.WxSendId = model.WxSendId;
                KeyModel.KeyTitle = drKeys["KeyTitle"].ToString();
                KeyModel.KeyTypeId = drKeys["KeyTypeId"].ToString();
                KeyModel.KeyTypeDetailId = drKeys["KeyTypeDetailId"].ToString();
                keyDal.Add(KeyModel);   //逐个添加新的关联关系
            }

            ReDict.Add("WxSendId", model.WxSendId);
            #region 事务关闭

            transactionScope.Complete();


        }
        #endregion


        ReTrue();
    }

    private void SearchWxPrPageList()
    {
        Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();

        BLL.WxBLL bll = new BLL.WxBLL();
        int CurrentPage = ReInt("CurrentPage", 1);
        bool Invalid = ReBool("Invalid", false);

        StringBuilder s = new StringBuilder();

        s.Append(" MerId='" + cm.CurrentMerId + "' ");
        s.Append(" and Invalid='" + Invalid + "' ");

        DataSet ds = bll.GetWxPtPageList(s.ToString(), CurrentPage, 20, " * ");

        RePage(ds);



    }

    private void GetWxPtInfo()
    {

        BLL.WxBLL bll = new BLL.WxBLL();
        decimal WxPtId = ReDecimal("WxPtId", 0);
        if (WxPtId == 0)
        {
            throw new Exception("没传ID啊!");
        }
        DataSet ds = bll.GetWxPtInfoData(WxPtId);

        string jr = JsonHelper.ToJson(ds);
        ReDict.Add("da", jr);

        ReTrue();
    }

    private void SaveWxPtInfo()
    {

        Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();
        Model.WxPtInfoModel model = new WxPtInfoModel();
        model.WxPtId = ReDecimal("WxPtId", 0);
        BLL.WxBLL bll = new BLL.WxBLL();
        if (model.WxPtId == 0)
        {

        }
        else
        {
            model = bll.GetWxPtInfoModel(model.WxPtId);
        }

        model.WxPtCode = ReStr("WxPtCode");

        model.WxPtName = ReStr("WxPtName");
        model.ReUrl = ReStr("ReUrl");
        model.ReToken = ReStr("ReToken");
        model.AppId = ReStr("AppId");
        model.AppSecret = ReStr("AppSecret");
        model.MerId = cm.CurrentMerId;
        model.WxPtTypeId = ReInt("WxPtTypeId");
        model.YuanShiId = ReStr("YuanShiId");
        model.ArticleUrl = ReStr("ArticleUrl", "");
        model.ProUrl = ReStr("ProUrl", "");
        model.TieZiUrl = ReStr("TieZiUrl", "");
        bll.SaveWxPtInfo(model);
        ReTrue();
    }
}
