using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Common;
using Model;
using System.Transactions;
using io.rong;
using System.Xml;
using BeeCloud.Model;
using BeeCloud;

namespace BPage
{
    public class Merchant : Common.BPageSetting2
    {


        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string para = ReStr("para");
                switch (para)
                {
                    #region 服装品质

                    case "GetProcessLv":
                        GetProcessLv();
                        break;

                    case "GetProcessLocationType":
                        GetProcessLocationType();
                        break;


                    #endregion

                    #region 推广相关
                    case "ChangeCommission":
                        ChangeCommission();
                        break;
                    case "GetExtMemberDict":
                        GetExtMemberDict();
                        break;
                    case "SaveExtMemberDict":
                        SaveExtMemberDict();
                        break;
                    #endregion

                    #region 接口对接
                    case "ChangeAllowProNumInterface": //批量修改库存对接商品
                        ChangeAllowProNumInterface();
                        break;
                    case "ChangeAllowPriceInterface":  //批量修改价格对接商品
                        ChangeAllowPriceInterface();
                        break;


                    case "GetJkProNum":
                        GetJkProNum();   //获取需要对接的库存
                        break;
                    case "GetJkRePrice":
                        GetJkRePrice();   //获取需要对接的价格
                        break;

                    case "ChangeProNumAll":
                        ChangeProNumAll();  //循环更改所有的库存, 触发所有更改
                        break;
                    case "ChangeRePriceAll":
                        ChangeRePriceAll();  //循环更改所有的价格 触发所有更改
                        break;


                    case "UpJkProNum":
                        UpJkProNum();    //提交库存对接
                        break;
                    case "UpJkRePrice":
                        UpJkRePrice();   //提交价格对接
                        break;

                    case "DelJkRePrice":
                        DelJkRePrice();
                        break;
                    case "DelJkProNum":
                        DelJkProNum();
                        break;
                    #endregion


                    #region 品牌
                    case "ClearPinPai":
                        ClearPinPai();
                        break;
                    case "GetPinPaiPageList":
                        GetPinPaiPageList();
                        break;

                    case "SavePinPaiInfo":
                        SavePinPaiInfo();
                        break;

                    case "GetPinPaiInfo":
                        GetPinPaiInfo();
                        break;

                    case "InvalidPinPai":
                        InvalidPinPai();
                        break;

                    #endregion

                    #region 用户和角色

                    case "GetPaiSongUser":
                        GetPaiSongUser();
                        break;

                    case "RemoveUserMerRole":
                        RemoveUserMerRole();//删除一组用户角色关系
                        break;
                    case "GetUserMerRole":  //获取一个用户的所有角色
                        GetUserMerRole();
                        break;
                    case "GetMerRoleList":
                        GetMerRoleList();
                        break;


                    case "ChangeMemoName":
                        ChangeMemoName();
                        break;


                    case "GetUsersMerRole":
                        GetUsersMerRole();

                        break;

                    case "GetMerRoleUsers":
                        GetMerRoleUsers();  //查找商家关联的角色
                        break;
                    case "GetAppMenuList":
                        GetAppMenuList();//取得移动管理终端的菜单
                        break;
                    case "SaveMerRoleVsUser":
                        SaveMerRoleVsUser();
                        break;

                    case "SaveMerRole":
                        SaveMerRole();  //保存一个商家角色
                        break;

                    case "GetMenuVsMerRole":
                        GetMenuVsMerRole();
                        break;
                    case "SaveMenuPower":
                        SaveMenuPower();
                        break;
                    case "RemoveMerRoleVsUser":
                        RemoveMerRoleVsUser();   //删除一个商家的角色用户关系
                        break;

                    case "GetMerRoleVsUserList":
                        GetMerRoleVsUserList();   //获得一个商家的所有角色用户
                        break;

                    case "SaveMerAdmin":
                        SaveMerAdmin();  //设置商家管理员!
                        break;


                    #endregion

                    #region 订单管理


                    #region 订单打印

                    case "GetPaiSongDan":
                        GetPaiSongDan();
                        break;



                    #endregion
                    #region 订单推送



                    case "NewDingDanPush":
                        NewDingDanPush();
                        break;

                    case "PaiSongTuiSong":
                        PaiSongTuiSong();
                        break;
                    #endregion

                    #region 退货管理

                    case "GetThList":
                        GetThList();
                        break;
                    case "GetThDetailList":
                        GetThDetailList();
                        break;
                    case "SaveTh":
                        SaveTh();
                        break;

                    #endregion

                    #region 订单(明细)评价

                    case "InvalidPingJia":
                        InvalidPingJia();  //作废一条评价
                        break;
                    case "GetPingJiaList":
                        GetPingJiaList();
                        break;

                    case "SavePingJia":
                        SavePingJia(); //保存评价, 不是回评!
                        break;

                    case "SaveHuiPing": //回复评价

                        SaveHuiPing();
                        break;
                    #endregion
                    case "GetDingDanStatus":
                        GetDingDanStatus();
                        break;

                    case "GetDingDanAddress":
                        GetDingDanAddress();  //获得订单地址
                        break;

                    case "ChangeDingDanLabel":   //更改订单备注
                        ChangeDingDanLabel();
                        break;


                    case "DingDanVsPeiHuoUser":
                        DingDanVsPeiHuoUser();   //订单指定配货员
                        break;
                    case "DingDanVsPaiSongUser":
                        DingDanVsPaiSongUser();   //订单指定派送员
                        break;


                    case "RemoveDetail":
                        RemoveDetail(); //删除订单明细!
                        break;

                    case "NewDingDan":
                        NewDingDan();
                        break;

                    case "GetDingDanLogByDingDanId":
                        GetDingDanLogByDingDanId();   //获取订单日志
                        break;

                    case "GetProByDingDan":
                        GetProByDingDan();
                        break;
                    case "QueRenPeiHuo":
                        QueRenPeiHuo();
                        break;

                    case "QueRenDingDan":
                        QueRenDingDan();   //快递员确认收货订单
                        break;

                    case "ShenHeDingDan": //审核订单,后台管理员使用
                        ShenHeDingDan();

                        break;

                    case "QuXiaoDingDan": //用户取消订单, 在选择支付方式之前
                        QuXiaoDingDan();

                        break;

                    case "DelAddress":
                        DelAddress();
                        break;

                    case "SaveAddress":
                        SaveAddress();
                        break;
                    case "QuantityChange":
                        QuantityChange();
                        break;
                    case "doDelDingDanDetail":
                        doDelDingDanDetail();
                        break;
                    case "ChangeDingDanAddress":
                        ChangeDingDanAddress();   //修改订单地址
                        break;
                    case "GetDingDanInfo":
                        GetDingDanInfo();
                        break;

                    case "GetDingDanInfoByDingDanQueRen":
                        GetDingDanInfoByDingDanQueRen();    //根据订单确认页面的要求取得当前订单的信息 1主体,2明细,3派送方式
                        break;

                    case "GetDingDanPageList":
                        GetDingDanPageList();
                        break;
                    case "GetDingDanDetailList":
                        GetDingDanDetailList();
                        break;

                    case "GetDingDanDetail":
                        GetDingDanDetail();
                        break;
                    case "ChangeDingDanStause":
                        ChangeDingDanStause();   //改变订单属性
                        break;
                    case "ChangeDingDanDetail":  //改动订单明细
                        ChangeDingDanDetail();

                        break;


                    case "CheckPayment":
                        CheckPayment();  //检验订单的支付状态
                        break;
                    case "AddDingDan":
                        AddDingDan();  //添加一条新订单
                        break;
                    case "OfferLanSelPayType":

                        OfferLanSelPayType(); //选择线下支付方式
                        break;
                    #endregion

                    #region 商家和产品
                    #region 分部管理

                    case "":
                        break;


                    case "CopyPro":
                        CopyPro();  //分部产品同步
                        break;


                    case "GetProStatus":
                        GetProStatus();
                        break;

                    case "BranchProStatus":
                        BranchProStatus();    //一个分部的多个商品改变状态(上下架)
                        break;
                    case "ProBranchStatus":   //一个商品的多个分部改变状态(上下架)
                        ProBranchStatus();
                        break;
                    case "UpAllProToBranch":  //分部上货
                        UpAllProToBranch();
                        break;

                    case "UpProToBranch":  //分部上货
                        UpProToBranch();
                        break;

                    case "GetBranchVsZoneList":
                        GetBranchVsZoneList();
                        break;
                    case "DelBranchVsZone":
                        DelBranchVsZone();
                        break;
                    case "SaveBranchVsZone":
                        SaveBranchVsZone();
                        break;

                    case "SaveProVsBranch":
                        SaveProVsBranch();
                        break;

                    case "GetProVsBranch":
                        GetProVsBranch();

                        break;



                    case "GetProVsBranchList":
                        GetProVsBranchList();
                        break;

                    case "BindBranch":
                        BindBranch();
                        break;

                    case "GetBranchList":
                        GetBranchList();
                        break;

                    case "SaveBranch":
                        SaveBranch();
                        break;

                    #endregion

                    #region 商家配置

                    case "GetMerConfigList":
                        GetMerConfigList();
                        break;

                    case "SaveMerConfig":
                        SaveMerConfig();
                        break;

                    case "DelMerConfig":
                        DelMerConfig();

                        break;


                    #endregion

                    #region 产品管理

                    case "ProductKeyWord":
                        ProductKeyWord();  //重新生成全部商品的关键词
                        break;
                    case "ChangeProTeXingForProClass":  //修改产品类别中的产品特性
                        ChangeProTeXingForProClass();
                        break;

                    case "QuickSerchPro":
                        QuickSerchPro();  //快速查找产品列表
                        break;

                    case "RemoveShouCang":
                        RemoveShouCang();
                        break;

                    case "GetMyShouCangPageList":
                        GetMyShouCangPageList();   //获得我的收藏列表
                        break;

                    case "SaveShouCangPro":
                        SaveShouCangPro();
                        break;

                    case "GetProCldClass":
                        GetProCldClass();
                        break;

                    case "ProRecommend":
                        ProRecommend();
                        break;

                    case "GetAuthorPageList":
                        GetAuthorPageList();
                        break;

                    case "SaveAuthorInfo":
                        SaveAuthorInfo();
                        break;

                    case "GetProInfoByProCode":
                        GetProInfoByProCode();
                        break;


                    case "InvalidPro":
                        InvalidPro();
                        break;
                    case "GetCldProTypeList":
                        GetCldProTypeList();
                        break;
                    case "InvalidMer":

                        InvalidMer();   //作废一个商家
                        break;
                    case "StatusApplyForBindMer":
                        StatusApplyForBindMer();   //设置绑定请求
                        break;
                    case "SearchMerList":
                        SearchMerList();
                        break;

                    case "SearchMerProList":
                        SearchMerProList();
                        break;

                    case "SaveProClassInfo":
                        SaveProClassInfo();
                        break;

                    case "SaveMyMerchant":
                        SaveMyMerchant();
                        break;

                    case "GetMerType":
                        GetMerType();
                        break;

                    case "GetArticleClassInfo":
                        GetArticleClassInfo();
                        break;

                    case "InvalidAtricleClass":
                        InvalidAtricleClass();  //作废一个文章类别(商家后台保存)
                        break;
                    case "GetArticleClass":

                        GetArticleClass();
                        break;


                    case "SaveArticleClass":
                        SaveArticleClass();   //保存一条新闻类别(商家后台保存)
                        break;
                    case "ApplyForBindMer":    //添加一条绑定申请!
                        ApplyForBindMer();
                        break;
                    case "GetApplyForMerPageList":
                        GetApplyForMerPageList();   //获得绑定
                        break;

                    case "GetMerVsComment":

                        GetMerVsComment();
                        break;

                    case "GetProTypePostSerData":
                        GetProTypePostSerData();   //取得产品搜索所需的条件
                        break;


                    case "GetProComment":
                        GetProComment();
                        break;
                    case "SaveProComment":
                        SaveProComment();   //保存用户对产品的点评
                        break;

                    case "ChangeProDefaultImg":
                        ChangeProDefaultImg();  //更改产品默认图片
                        break;
                    case "GetProList":
                        GetProList();  //获得产品列表
                        break;
                    case "GetMerProList":
                        GetMerProList();  //获得产品列表
                        break;
                    case "GetProVsImg":
                        GetProVsImg();  //获得一个产品下所有的图片
                        break;

                    case "GetProInfo":
                        GetProInfo();  //取得一个产品的数据
                        break;
                    case "GetProVsBranchInfo":
                        GetProVsBranchInfo();
                        break;

                    case "GetProVsBranchInfoForSys":
                        GetProVsBranchInfoForSys();
                        break;

                    case "SaveProVsImg":
                        SaveProVsImg();  //添加一条产品与图片的关联关系
                        break;
                    case "FindProByZoneId":
                        FindProByZoneId();  //在一个区域内寻找固定产品是否在售
                        break;
                    case "GetProType":
                        GetProType();   //获得网站内的产品类别
                        break;
                    case "GetProClassByJson":  //获得商家的产品类别
                        GetProClassByJson();
                        break;
                    case "GetProClass":  //获得商家的产品类别
                        GetProClass();
                        break;

                    case "GetProClassX":
                        GetProClassX();   //获取商家的顶级类别, 或者某一个父类别的所有子类别
                        break;
                    case "GetProClassAll":
                        GetProClassAll();  //取得所有商品类别
                        break;

                    case "GetProClassInfo":
                        GetProClassInfo();
                        break;


                    case "SavePro":
                        SavePro(); //保存一个产品
                        break;
                    case "SaveMerchant":
                        SaveMerchant();
                        break;
                    case "GetMerList":
                        GetMerList();    //取得产品列表
                        break;

                    case "GetMerInfo":
                        GetMerInfo();  //取得一个商家
                        break;

                    case "InvalidProClass":
                        InvalidProClass();
                        break;

                    #endregion

                    #region 促销管理
                    case "CuXiaoVsProVsKeyChange":
                        CuXiaoVsProVsKeyChange();
                        break;

                    case "SaveCuXiaoInfo":
                        SaveCuXiaoInfo();
                        break;  //保存一个促销

                    case "SaveCuXiaoVsPro":
                        SaveCuXiaoVsPro();  //保存一个促销和产品的关联
                        break;

                    case "GetCuXiaoVsProList":
                        GetCuXiaoVsProList();
                        break;
                    case "GetCuXiaoInfo":
                        GetCuXiaoInfo();
                        break;

                    case "GetCuXiaoList": //获得促销分页列表
                        GetCuXiaoList();
                        break;

                    case "DelCuXiaoVsPro":
                        DelCuXiaoVsPro();
                        break;

                    case "InvalidCuXiao":
                        InvalidCuXiao();
                        break;
                    #endregion

                    #endregion

                    #region 配送管理


                    case "InheritPeiSongTypeByProClass":
                        InheritPeiSongTypeByProClass();
                        break;

                    case "GetPeiSongTypeList":
                        GetPeiSongTypeList();
                        break;
                    case "GetPeiSongTypeInfo":
                        GetPeiSongTypeInfo();
                        break;

                    case "SavePeiSongTypeInfo":
                        SavePeiSongTypeInfo();
                        break;

                    case "GetPeiSongTime":
                        GetPeiSongTime();
                        break;

                    case "SavePeiSongTimeSolt":
                        SavePeiSongTimeSolt();
                        break;


                    case "DelPeiSongTimeSolt":
                        DelPeiSongTimeSolt();
                        break;

                    #endregion

                    #region 产品属性


                    case "SerProList": //产品列表页综合查询

                        SerProList();

                        break;
                    case "SelZoneProList":
                        SelZoneProList();
                        break;

                    case "GetProAttrList":
                        GetProAttrList();
                        break;

                    case "GetProAttr":
                        GetProAttr();
                        break;

                    case "SaveProAttr":
                        SaveProArrt();
                        break;

                    #endregion

                    #region 积分管理


                    case "ChangeJiFenNumForProClass":
                        ChangeJiFenNumForProClass();
                        break;

                    case "GetJiFenChangeList":
                        GetJiFenChangeList();
                        break;

                    case "AddJifen":
                        AddJifen();   //赠与或扣除积分
                        break;

                    case "SearchJiFenChange":
                        SearchJiFenChange();
                        break;
                        #endregion

                }
            }
            catch (Exception ex)
            {
                BLL.StaticBLL.ReThrow(ex);
                context.Response.End();
            }

        }

        private void GetProcessLocationType()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.ProcessLocationType ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetProcessLv()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.ProcessLv ORDER BY ProcessLvId ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();


        }

        private void ChangeCommission()
        {

            int ExtMemberLv = ReInt("ExtMemberLv", 0);
            decimal Commission = ReDecimal("Commission", 0);
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.ExtMemberDict SET Commission ='"+ Commission + "' WHERE ExtMemberLv='"+ExtMemberLv+"' ");

            DAL.DalComm.ExReInt(s.ToString());

            ReTrue();
        }

        private void SaveExtMemberDict()
        {
            throw new NotImplementedException();
        }

        private void GetExtMemberDict()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" SELECT * FROM dbo.ExtMemberDict ORDER BY ExtMemberLv");

            DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();


        }

        private void ChangeAllowPriceInterface()
        {
            string ProIds = ReStr("ProIds", "");
            bool AllowPriceInterface = ReBool("AllowPriceInterface");

            if (ProIds == "")
            {
                throw new Exception("ProIds不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.ProVsBranch SET AllowPriceInterface='" + AllowPriceInterface + "' WHERE ProId IN (" + ProIds + ") ");
            DAL.DalComm.BackData(s.ToString());
            ReTrue();

        }

        private void ChangeAllowProNumInterface()
        {
            string ProIds = ReStr("ProIds", "");
            bool AllowProNumInterface = ReBool("AllowProNumInterface");

            if (ProIds == "")
            {
                throw new Exception("ProIds不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.ProVsBranch SET AllowProNumInterface='" + AllowProNumInterface + "' WHERE ProId IN (" + ProIds + ") ");
            DAL.DalComm.BackData(s.ToString());
            ReTrue();
        }

        private void GetDingDanStatus()
        {
            DataSet ds = DAL.DalComm.BackData(" SELECT * FROM dbo.DingDanStatus WITH(NOLOCK) ORDER BY DingDanStatusId ");
            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();


        }

        private void QuXiaoDingDan()
        {
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                string DingDanId = ReStr("DingDanId", "");

                if (DingDanId == "")
                {
                    throw new Exception("订单ID不能为空!");
                }



                StringBuilder s = new StringBuilder();
                s.Append(" SELECT Status,DingDanStatusName FROM dbo.DingDanView WHERE DingDanId='" + DingDanId + "' ");

                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count != 1)
                {
                    throw new Exception("订单数量不能为" + dt.Rows.Count + "");
                }
                DataRow dr = dt.Rows[0];

                int Status = int.Parse(dr["Status"].ToString());
                string DingDanStatusName = dr["DingDanStatusName"].ToString();
                if (Status <= 0)
                {

                }
                else
                {
                    throw new Exception("订单状态已经为" + DingDanStatusName + "无法取消请联系管理员!");
                }


                DAL.DalComm.ExReInt(" UPDATE dbo.DingDanInfo SET Status=-10 WHERE DingDanId='" + DingDanId + "' ");



                Model.DingDanLogModel logModel = new DingDanLogModel();
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                logModel.DingDanId = DingDanId;
                logModel.Memo = "订单被用户自行取消(由[" + DingDanStatusName + "]变更为[订单取消])";
                logModel.DingDanLogTypeId = -10;
                logModel.CreateTime = DateTime.Now;
                bll.SaveDingDanLog(logModel);


                ReTrue();





                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
        }



        private void OfferLanSelPayType()
        {
            int PayTypeId = ReInt("PayTypeId", 0);
            string DingDanId = ReStr("DingDanId", "");

            BLL.MerchantBLL MerBll = new BLL.MerchantBLL();
            StringBuilder s = new StringBuilder();
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                s.Append("SELECT Status,MerchantId,BranchId,DingDanStatusName FROM dbo.DingDanView WHERE DingDanId='" + DingDanId + "'  ");
                s.Append(" SELECT * FROM dbo.PayType WHERE PayTypeId='" + PayTypeId + "' ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dtDingDan = ds.Tables[0];
                if (dtDingDan.Rows.Count != 1)
                {

                    throw new Exception("订单数量不能为" + dtDingDan.Rows.Count + "");
                }

                DataRow drDingDan = dtDingDan.Rows[0];


                DataTable dtPayType = ds.Tables[1];




                if (dtPayType.Rows.Count != 1)
                {

                    throw new Exception("没有找到支付方式为" + PayTypeId + "的记录");
                }


                DataRow drPayType = dtPayType.Rows[0];

                string PayTypeName = drPayType["PayTypeName"].ToString();


                if (PayTypeId >= 20)
                {
                    throw new Exception("线下支付的PayTypeId不能大于20!");
                }



                decimal MerId = decimal.Parse(drDingDan["MerchantId"].ToString());
                string BranchId = drDingDan["BranchId"].ToString();
                int Status = int.Parse(drDingDan["Status"].ToString());
                string DingDanStatusName = drDingDan["DingDanStatusName"].ToString();

                if (MerId == 0)
                {
                    throw new Exception("MerId不能为0!");
                }

                if (DingDanId == "")
                {
                    throw new Exception("DingDanId不能为空!");
                }
                if (PayTypeId == 0)
                {
                    throw new Exception("PayTypeId不能为0!");
                }

                if (Status == 0)
                {



                    s.Clear();

                    s.Append("         UPDATE dbo.DingDanInfo SET PayTypeId='" + PayTypeId + "',Status='10' WHERE DingDanId='" + DingDanId + "'  ");

                    Model.DingDanLogModel logModel = new DingDanLogModel();
                    logModel.DingDanId = DingDanId;
                    logModel.DingDanLogTypeId = 10;
                    logModel.CreateTime = DateTime.Now;
                    logModel.Memo = "选择货到付款支付方式为[" + PayTypeName + "]";
                    MerBll.SaveDingDanLog(logModel);
                    MerBll.NewDingDanTiXing(DingDanId);




                }
                else
                {

                    throw new Exception("状态为[" + DingDanStatusName + "]的订单不能支付!");

                }






                DAL.DalComm.ExReInt(s.ToString());

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            ReTrue();

        }




        private void FindProByZoneId()
        {
            string ProId = ReStr("ProId", "");
            string BranchId = ReStr("BranchId", "");
            string ZoneId = ReStr("ZoneId", "");

            if (ZoneId == "")
            {
                throw new Exception("ZoneId不能为空!");
            }

            //if (BranchId == "")
            //{
            //    throw new Exception("BranchId不能为空!");
            //}

            if (ProId == "")
            {
                throw new Exception("ProId不能为空!");
            }

            StringBuilder s = new StringBuilder();

            s.Append(" SELECT * FROM dbo.ProVsBranchView WHERE FlagInvalid=0  and ProId='" + ProId + "' AND BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "') order by Status desc ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                throw new Exception("此产品在当前区域暂无销售信息!");
            }

            DataRow dr = dt.Rows[0];

            ReDict2.Add("BranchId", dr["BranchId"].ToString());

            ReTrue();

        }

        private void GetProInfoByProCode()
        {
            string ProCode = ReStr("ProCode", "");
            string ZoneId = ReStr("ZoneId", "");
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT TOP 1 * ");
            s.Append(" FROM    dbo.ProVsBranchView WITH ( NOLOCK ) ");
            s.Append(" WHERE   BranchId IN ( SELECT    BranchId ");
            s.Append("  FROM      dbo.BranchVsZone ");
            s.Append(" WHERE     ZoneId = '" + ZoneId + "' ) ");
            s.Append("  AND FlagInvalid = 0 ");
            s.Append(" AND ProCode='" + ProCode + "' ");
            s.Append(" ORDER BY ProStatusId DESC ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {

                ReDict.Add("info", JsonHelper.ToJsonNo1(dt));
            }
            else
            {

            }


            ReDict2.Add("num", dt.Rows.Count.ToString());

            ReTrue();

        }

        private void GetPeiSongTypeList()
        {
            string BranchId = ReStr("BranchId", "");

            DataSet ds = DAL.DalComm.BackData(" SELECT * FROM dbo.PeiSongType with(nolock) where BranchId='" + BranchId + "' ORDER BY OrderNo DESC ");
            ReDict.Add("list", JsonHelper.ToJson(ds.Tables[0]));
            ReTrue();

        }

        private void DelBranchVsZone()
        {
            Model.BranchVsZoneModel model = new BranchVsZoneModel();
            model.BranchId = ReDecimal("BranchId", 0);
            model.ZoneId = ReStr("ZoneId", "");


            DAL.DalComm.ExReInt(" DELETE FROM dbo.BranchVsZone where  BranchId='" + model.BranchId + "' and ZoneId='" + model.ZoneId + "' ");
            ReTrue();
        }

        private void SaveBranchVsZone()
        {
            Model.BranchVsZoneModel model = new BranchVsZoneModel();
            model.BranchId = ReDecimal("BranchId", 0);
            model.ZoneId = ReStr("ZoneId", "");

            if (model.ZoneId == "0" || model.ZoneId == "")
            {
                throw new Exception("ZoneId不能为空!");
            }

            if (model.BranchId == 0)
            {
                throw new Exception("BranchId不能为0!");
            }

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.SaveBranchVsZone(model);
            ReTrue();

        }

        private void GetBranchVsZoneList()
        {
            StringBuilder s = new StringBuilder();
            decimal BranchId = ReDecimal("BranchId", 0);

            s.Append("  SELECT * FROM dbo.Zone WHERE ZoneId IN ( SELECT bvz.ZoneId FROM dbo.BranchVsZone bvz WHERE BranchId='" + BranchId + "' )  ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void InvalidCuXiao()
        {
            decimal CuXiaoId = ReDecimal("CuXiaoId", 0);

            bool Invalid = ReBool("Invalid", true);

            if (CuXiaoId == 0)
            {
                throw new Exception("促销ID不能为0!");
            }

            DAL.DalComm.ExReInt(" UPDATE dbo.CuXiao SET Invalid='" + Invalid + "' WHERE CuXiaoId='" + CuXiaoId + "' ");
            ReTrue();
        }

        private void ChangeDingDanAddress()
        {
            Model.DingDanInfoModel model = new DingDanInfoModel();
            model.DingDanId = ReStr("DingDanId", "");


            if (model.DingDanId == "")
            {
                throw new Exception("订单ID不能为空!");
            }
            DAL.DingDanInfoDAL dal = new DAL.DingDanInfoDAL();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            model = dal.GetModel(model.DingDanId);
            model.PeiSongTime1 = ReTime("PeiSongTime1");
            model.PeiSongTime2 = ReTime("PeiSongTime2");

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(model.DingDanAttr);


            XmlNode node = xml.SelectSingleNode("root/电话");
            node.InnerText = ReStr("Tel", "");

            node = xml.SelectSingleNode("root/详细地址");
            node.InnerText = ReStr("address", "");

            node = xml.SelectSingleNode("root/收货人");
            node.InnerText = ReStr("ContactName", "");

            model.DingDanAttr = Common.XmlHelper.GetString(xml);


            dal.Update(model);
            ReTrue();

        }

        private void GetDingDanAddress()
        {
            string DingDanId = ReStr("DingDanId", "");
            if (DingDanId == "")
            {
                throw new Exception("DingDanId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT  PeiSongTime1 ,PeiSongTime2 ,详细地址 ,Tel,ContactName FROM dbo.DingDanView WITH(NOLOCK) where DingDanId='" + DingDanId + "'  ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void GetPaiSongUser()
        {
            StringBuilder s = new StringBuilder();
            var BranchId = ReStr("BranchId", "");

            if (BranchId == "")
            {

                throw new Exception("BranchId不能为空!");
            }

            s.Append("  SELECT * FROM dbo.UserMerRoleView WITH(NOLOCK) WHERE BranchId='" + BranchId + "' AND MerRoleName='派送员' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            ReDict.Add("list", JsonHelper.ToJson(ds.Tables[0]));
            ReTrue();

        }

        private void GetDingDanDetail()
        {
            StringBuilder s = new StringBuilder();
            decimal DingDanDetailId = ReDecimal("DingDanDetailId", 0);
            if (DingDanDetailId == 0)
            {
                throw new Exception("DingDanDetailId不能为0!");
            }
            s.Append(" SELECT * FROM dbo.DingDanDetailView WITH(NOLOCK) WHERE DingDanDetailId=" + DingDanDetailId + " ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("info", JsonHelper.ToJsonNo1(dt));
            ReTrue();
        }

        private void ChangeDingDanLabel()
        {
            string Label = ReStr("Label", "");
            string DingDanId = ReStr("DingDanId", "");
            DAL.DalComm.ExInt("UPDATE dbo.DingDanInfo SET Label='" + Label + "' WHERE DingDanId='" + DingDanId + "'");
            ReDict2.Add("Label", Label);
            ReTrue();

        }

        private void CopyPro()
        {
            string BranchId = ReStr("BranchId", "");

            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            int i = mbll.CopyPro(BranchId);

            ReDict2.Add("i", i.ToString());

            ReTrue();
        }

        private void GetProStatus()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();

            DataSet ds = bll.GetProStatus();

            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetPaiSongDan()
        {
            string DingDanId = ReStr("DingDanId", "");
            if (DingDanId == "")
            {
                throw new Exception("没法确定订单ID!");
            }
            DataSet ds = DAL.DalComm.BackData(" SELECT  * FROM CORE.dbo.DingDanDetailView with(nolock) WHERE DingDanId='" + DingDanId + "' select * from CORE.dbo.DingDanView with(nolock) where DingDanId='" + DingDanId + "' ");
            DataTable dtDingDanDetail = ds.Tables[0];
            DataRow[] drsDingDanDetail = dtDingDanDetail.Select(" DingDanDetailTypeId=10 ");
            decimal 派送费 = 0;
            decimal 支付金额 = 0;
            decimal 使用积分 = 0;
            decimal 订单金额 = 0;
            foreach (DataRow drDingDanDetail in drsDingDanDetail)
            {
                decimal Price = decimal.Parse(drDingDanDetail["Price"].ToString());
                派送费 = Price + 派送费;
                drDingDanDetail.Delete();

            }
            dtDingDanDetail.AcceptChanges();




            DataTable dtDingDanInfo = ds.Tables[1];
            Dictionary<string, string> canshu = new Dictionary<string, string>();
            XmlDocument x = new XmlDocument();//实例化一个XML文档  
            DataRow drDingDanInfo = dtDingDanInfo.Rows[0];
            使用积分 = Math.Round(decimal.Parse(drDingDanInfo["UseJiFen"].ToString()), 2);
            支付金额 = Math.Round(decimal.Parse(drDingDanInfo["PayAmount"].ToString()), 2);
            订单金额 = Math.Round(decimal.Parse(drDingDanInfo["Amount"].ToString()), 2);
            string 详细地址 = drDingDanInfo["详细地址"].ToString();
            派送费 = Math.Round(派送费, 2);
            x.LoadXml(drDingDanInfo["DingDanAttr"].ToString());

            foreach (XmlNode xn in x.ChildNodes[0].ChildNodes)
            {

                canshu.Add(xn.Name, xn.InnerText);

            }


            StringBuilder w = new StringBuilder();
            w.Append(DateTime.Parse(drDingDanInfo["PeiSongTime1"].ToString()).ToString("MM月dd日 hh:mm") + " - " + DateTime.Parse(drDingDanInfo["PeiSongTime2"].ToString()).ToString("hh:mm") + "");


            //   canshu.Add("配送方式", drDingDanInfo["PeiSongTypeName"].ToString());
            canshu.Add("会员账号", drDingDanInfo["NickName"].ToString());
            canshu.Add("派送费", 派送费.ToString());
            canshu.Add("使用积分", 使用积分.ToString());
            canshu.Add("支付金额", 支付金额.ToString());
            canshu.Add("订单金额", 订单金额.ToString());
            canshu.Add("期望到货时间", w.ToString());
            canshu.Add("DingDanId", DingDanId);
            ds.Tables.Remove(dtDingDanInfo);
            Common.RptCommon.ReData(ds, canshu);
        }

        private void ChangeRePriceAll()                                         //价格
        {

            decimal MerId = ReDecimal("MerId", 0);
            string BranchId = ReStr("BranchId", "");
            string dbName = ReStr("dbName", "");

            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            if (BranchId.Trim() == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            if (dbName.Trim() == "")
            {
                throw new Exception("线下系统的dbName不能为空!");

            }


            StringBuilder s = new StringBuilder();
            s.Append(@"

    
     TRUNCATE table [JK].[dbo].[ChangePrice]
    INSERT INTO [JK].[dbo].[ChangePrice]
           ([BranchId]
           ,[MerId]
           ,[OldRePrice]
           ,[RePrice]
           ,[VipRePrice]
           ,[CreateTime]
           ,[ProCode])

   ");
            s.Append(" select '" + BranchId + "'," + MerId + ",0,ISNULL( sale_price,0),ISNULL(vip_price,0),GETDATE(),ISNULL( barcode,'')  from  " + dbName + ".dbo.bi_t_item_info ");

            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void ChangeProNumAll()
        {

            decimal MerId = ReDecimal("MerId", 0);
            string BranchId = ReStr("BranchId", "");
            string dbName = ReStr("dbName", "");

            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            if (BranchId.Trim() == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            if (dbName.Trim() == "")
            {
                throw new Exception("线下系统的dbName不能为空!");

            }

            StringBuilder s = new StringBuilder();                                //库存
            s.Append(@"


TRUNCATE table [JK].[dbo].[ChangeNum]                 

INSERT INTO [JK].[dbo].[ChangeNum]
           ([BranchId]
           ,[MerId]
           ,[OldProNum]
           ,[ProNum]
           ,[CreateTime]
           ,[ProCode])
  
");
            s.Append("   select '" + BranchId + "'," + MerId + ",0,ISNULL( stock_qty,0),GETDATE(),ISNULL( i.barcode,'')  from  bw9ksyv10_01.dbo.ic_t_branch_stock s left join  " + dbName + ".dbo.bi_t_item_info i on s.item_no=i.item_no  ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }


        private void DelJkProNum()
        {
            DataTable dt = ReTable("JkProNumList");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {


                    List<string> ChangeNumIdList = new List<string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal ChangeNumId = decimal.Parse(dr["ChangeNumId"].ToString());


                        ChangeNumIdList.Add(ChangeNumId.ToString());



                    }


                    DAL.DalComm.ExReInt(" DELETE FROM jk.dbo.ChangeNum WHERE ChangeNumId IN (" + String.Join(",", ChangeNumIdList) + ")  ");
                    ReTrue();
                }
                else
                {
                    throw new Exception("没有要提交的行");
                }
            }
            else
            {
                throw new Exception("传值解析成的table为null!");
            }


        }

        private void DelJkRePrice()
        {
            DataTable dt = ReTable("JkRePriceList");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {


                    List<string> ChangePriceIdList = new List<string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal ChangePriceId = decimal.Parse(dr["ChangePriceId"].ToString());


                        ChangePriceIdList.Add(ChangePriceId.ToString());



                    }


                    DAL.DalComm.ExReInt(" DELETE FROM jk.dbo.ChangePrice WHERE ChangePriceId IN (" + String.Join(",", ChangePriceIdList) + ")  ");
                    ReTrue();
                }
                else
                {
                    throw new Exception("没有要提交的行");
                }
            }
            else
            {
                throw new Exception("传值解析成的table为null!");
            }
        }

        private void UpJkProNum()
        {
            DataTable dt = ReTable("JkProNumList");


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    StringBuilder s = new StringBuilder();

                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal ProNum = decimal.Parse(dr["ProNum"].ToString());
                        string BranchId = dr["BranchId"].ToString();
                        string ProCode = dr["ProCode"].ToString();

                        if (ProCode.ToString() != "")
                        {

                            string 其他更改 = "";

                            if (ProNum <= 0)
                            {
                                其他更改 = ",pvb.Status=-10 ";

                            }
                            else
                            {
                                其他更改 = ",pvb.Status=0 ";
                            }

                            s.Append("    UPDATE pvb SET pvb.ProNum=" + ProNum + "/ISNULL(p.InterfaceBaoZhuangNum,1)  " + 其他更改 + " FROM CORE.dbo.Product p with(nolock) INNER JOIN CORE.dbo.ProVsBranch pvb with(nolock) ON pvb.ProId=p.ProId WHERE pvb.BranchId='" + BranchId + "' and p.ProNumCode='" + ProCode + "' AND pvb.AllowProNumInterface=1  ");

                        }

                    }
                    DAL.DalComm.ExReInt(s.ToString());
                    ReTrue();
                }
                else
                {
                    throw new Exception("没有要提交的行");
                }
            }
            else
            {
                throw new Exception("传值解析成的table为null!");
            }
        }

        private void UpJkRePrice()
        {
            DataTable dt = ReTable("JkRePriceList");


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    StringBuilder s = new StringBuilder();

                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal RePrice = decimal.Parse(dr["RePrice"].ToString());


                        decimal VipRePrice = 0;
                        try
                        {
                            VipRePrice = decimal.Parse(dr["VipRePrice"].ToString());
                        }
                        catch (Exception)
                        {

                            VipRePrice = 0;
                        }


                        decimal 在售价格, 对比价格;

                        if (VipRePrice == 0)
                        {
                            在售价格 = RePrice;
                            对比价格 = 0;
                        }
                        else
                        {

                            在售价格 = VipRePrice;
                            对比价格 = RePrice;
                        }



                        string BranchId = dr["BranchId"].ToString();
                        string ProCode = dr["ProCode"].ToString();

                        if (ProCode.ToString() != "")
                        {

                            string 其他更改 = "";

                            if (在售价格 <= 0)
                            {
                                其他更改 = ",pvb.Status=-10 ";

                            }
                            if (对比价格 != 0)
                            {
                                其他更改 = 其他更改 + ",pvb.RePrice2=" + 对比价格 + " ";
                            }

                            s.Append("    UPDATE pvb SET pvb.RePrice=" + 在售价格 + "  " + 其他更改 + "  FROM CORE.dbo.Product p with(nolock) INNER JOIN CORE.dbo.ProVsBranch pvb with(nolock) ON pvb.ProId=p.ProId WHERE pvb.BranchId='" + BranchId + "' and ProCode='" + ProCode + "' AND pvb.AllowPriceInterface=1   ");

                        }

                    }
                    DAL.DalComm.ExReInt(s.ToString());
                    ReTrue();
                }
                else
                {
                    throw new Exception("没有要提交的行");
                }
            }
            else
            {
                throw new Exception("传值解析成的table为null!");
            }




        }

        private void GetJkRePrice()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" SELECT TOP 50 * FROM JK.dbo.ChangePrice  ");
            s.Append(" select count(0) as num From Jk.dbo.ChangePrice ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            int num = int.Parse(dt2.Rows[0]["num"].ToString());
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReDict2.Add("num", num.ToString());
            ReTrue();
        }

        private void GetJkProNum()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" SELECT TOP 50 * FROM JK.dbo.ChangeNum  ");
            s.Append(" select count(0) as num From Jk.dbo.ChangeNum ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            int num = int.Parse(dt2.Rows[0]["num"].ToString());
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReDict2.Add("num", num.ToString());

            ReTrue();

        }

        private void SaveProVsBranch()
        {
            Model.ProVsBranchModel model = new ProVsBranchModel();
            model.BranchId = ReStr("BranchId", "");
            model.ProId = ReStr("ProId", "");
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            DAL.ProVsBranchDAL dal = new DAL.ProVsBranchDAL();

            model = dal.GetModel(model.BranchId, model.ProId);


            model.ProName = ReStr("ProName", "");
            if (model.ProName == "")
            {
                throw new Exception("ProName不能为空!");
            }

            model.AllowPriceInterface = ReBool("AllowPriceInterface", true);
            model.AllowProNumInterface = ReBool("AllowProNumInterface", true);

            model.Discount = ReDecimal("Discount", 0);
            model.GetJiFenNum = ReDecimal("GetJiFenNum", 0);
            model.InheritDiscount = ReBool("InheritDiscount", true);
            model.InheritJiFenNum = ReBool("InheritJiFenNum", true);
            model.InheritPeiSongType = ReBool("InheritPeiSongType", true);
            model.InheritProTeXing = ReBool("InheritProTeXing", true);
            if (ReInt("InterfaceBaoZhuangNum", 0) != 0)
            {

                model.InterfaceBaoZhuangNum = ReInt("InterfaceBaoZhuangNum", 0);
            }


            model.IsInfiniteNum = ReBool("IsInfiniteNum", true);
            model.MinQuantity = ReDecimal("MinQuantity", 0);
            model.MinZl = ReDecimal("MinZl", 0);
            model.OnLineLv = ReInt("OnLineLv", 0);
            if (!model.AllowProNumInterface)
            {
                model.ProNum = ReDecimal("ProNum", 0);//如果不对接库存, 那才允许更改库存
            }


            if (!model.AllowPriceInterface)   //如果不对接价格, 那才允许更改价格
            {
                model.RePrice = ReDecimal("RePrice", 0);
            }

            model.RePrice2 = ReDecimal("RePrice2", 0);
            model.RePrice3 = ReDecimal("RePrice3", 0);

            if (ReInt("Status", 0) != 0)
            {
                model.Status = ReInt("Status", 0);
            }


            model.Zl = ReDecimal("Zl", 0);

            dal.Update(model);
            bll.InterfaceBaoZhuangNumNotZero();
            bll.ProVsBranchKeyWord(model.ProId);
            ReTrue();

        }

        private void GetProVsBranch()
        {
            StringBuilder s = new StringBuilder();
            decimal BranchId = ReDecimal("BranchId", 0);
            string ProId = ReStr("ProId", "");
            if (ProId == "")
            {
                throw new Exception("ProId不能为空!");
            }

            s.Append(" select *  FROM dbo.ProVsBranch with(nolock) where  ProId='" + ProId + "' and BranchId=" + BranchId + "  ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {

                throw new Exception("没有该数据!");
            }
            ReDict.Add("Info", JsonHelper.ToJsonNo1(dt));


            ReTrue();
        }

        private void ProductKeyWord()
        {

            string ProId = ReStr("ProId", "");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            mbll.ProductKeyWord(ProId);
            ReTrue();
        }

        private void SerProList()
        {
            BLL.MerchantBLL mBll = new BLL.MerchantBLL();
            decimal MerchantId = ReDecimal("MerId", 0);
            int CurrentPage = ReInt("CurrentPage", 1);
            string Order = ReStr("Order", "CreateTime desc");
            bool hasProNum = ReBool("hasProNum", true);  //如果只查询有数量的
            int PageSize = ReInt("PageSize", 40);
            string BranchId = ReStr("BranchId", "");
            if (BranchId == "")
            {

                throw new Exception("BranchId不能为空!");
            }

            string ProCode = ReStr("ProCode", "");
            string inputStr = ReStr("inputStr", "");
            string ProClassIds = ReStr("ProClassIds", "");
            int Status = ReInt("Status", 0);
            bool Invalid = ReBool("Invalid", false);
            string AllowProNumInterface = ReStr("AllowProNumInterface", "all");
            string AllowPriceInterface = ReStr("AllowPriceInterface", "all");
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            if (Status > 100)
            {
                //一般是9999
            }
            else
            {

                s.Append(" and Status =" + Status + " ");     //这里只能查询出固定状态的来, 目前是只有两种, 0(上架销售)或者-10(下架)
            }

            if (AllowPriceInterface != "all")
            {

                s.Append(" and AllowPriceInterface='" + AllowPriceInterface + "' ");
            }

            if (AllowProNumInterface != "all")
            {

                s.Append(" and AllowProNumInterface='" + AllowProNumInterface + "' ");
            }

            s.Append(" and  MerchantId=" + MerchantId + " ");
            s.Append(" and FlagInvalid='" + Invalid + "' ");




            if (ProClassIds != "")
            {

                s.Append("  and ProClassId in (" + ProClassIds + ") ");
            }


            if (inputStr != "")
            {
                s.Append(" and  KeyWord like '%" + inputStr + "%' ");
            }

            if (ProCode != "")
            {
                s.Append(" and  ProCode ='" + ProCode + "' ");
            }

            if (hasProNum)
            {
                s.Append(" and (ProNum>0 or IsInfiniteNum=1) ");

            }

            if (BranchId != "")
            {
                s.Append(" and  BranchId ='" + BranchId + "' ");
            }

            #region 页面条件

            DataTable dtSItem = ReTable("sItemArray");
            if (dtSItem != null)
            {

                if (dtSItem.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSItem.Rows)
                    {
                        string sKey = dr["sKey"].ToString();

                        switch (sKey)
                        {
                            case "ProClassId": //传递品类
                                decimal ProClassId = decimal.Parse(dr["sValueId"].ToString());
                                List<decimal> ProClassIdList = BLL.StaticBLL.GetProAllChildren(ProClassId);

                                s.Append(" and ProClassId in (" + string.Join(",", ProClassIdList) + ") ");

                                break;

                            case "KeyWord":
                                string KeyWord = dr["sValueId"].ToString();
                                s.Append(" and KeyWord like '%" + KeyWord + "%' or ProCode='" + KeyWord.Trim() + "' ");
                                if (Order == "DefaultOrder")
                                {
                                    Order = " dbo.CxCount('" + KeyWord + "',KeyWord) desc, CreateTime desc";
                                }

                                break;


                            case "PinPaiId":
                                decimal PinPaiId = decimal.Parse(dr["sValueId"].ToString());
                                s.Append(" and PinPaiId=" + PinPaiId + " ");
                                break;
                        }



                    }


                }

            }

            #endregion



            #region 扩展属性条件
            try
            {
                DataTable dtSArray = ReTable("sAttrArray");
                List<string> attrNameArray = new List<string>();
                if (dtSArray != null)
                {
                    if (dtSArray.Rows.Count > 0)
                    {
                        foreach (DataRow drSArray in dtSArray.Rows)
                        {
                            string attrCol = drSArray["attrCol"].ToString();
                            string attrType = drSArray["attrType"].ToString();
                            string attrName = drSArray["attrName"].ToString();
                            string attrValue = drSArray["attrValue"].ToString();
                            attrNameArray.Add(attrName);
                            switch (attrCol.ToLower())
                            {
                                case "reprice": //绑定价格

                                    decimal bg = decimal.Parse(attrValue.Split('_')[0]);
                                    decimal end = decimal.Parse(attrValue.Split('_')[1]);
                                    s.Append(" and RePrice >=" + bg + " and RePrice <=" + end + " ");
                                    break;

                                case "spec":  //规格

                                    s.Append(" and spec ='" + attrValue + "' ");


                                    break;
                                default:  //其他,产品扩展属性

                                    switch (attrType)
                                    {
                                        case "decimal": //数值区间
                                            decimal bg2 = decimal.Parse(attrValue.Split('_')[0]);
                                            decimal end2 = decimal.Parse(attrValue.Split('_')[1]);
                                            s.Append(" and attr.value('(/root/" + attrType + ")[1]','decimal') BETWEEN " + bg2 + " and " + end2 + " ");
                                            break;

                                        default:  //字符串

                                            s.Append(" and attr.value('(/root/" + attrType + ")[1]','nvarchar(50)') ='" + attrValue + "' ");

                                            break;
                                    }


                                    s.Append("   ");

                                    break;
                            }


                        }

                    }

                }
                ReDict2.Add("attrNameArray", string.Join(",", attrNameArray));

            }
            catch (Exception)
            {

                //不一定传的进来
            }

            #endregion


            string sWhere = s.ToString();

            if (Order == "DefaultOrder")
            {

                Order = " RecommendLv desc, CreateTime desc ";

            }

            //获得分部产品的分页列表
            DataSet ds = mBll.GetProVsBranchPageList(Order, sWhere, CurrentPage, PageSize, "ProStatusName,BranchId, BuyLv, ZgProNum,CreateTime,CreateUser,ProClassId,ProductClassName,Units,RePrice,RePrice2,RePrice3,Spec,Status, ProName,ProTitle,ProductImgUrl,ProductImgId,ProId,MerchantName, RecommendLv,ProCode,GetJiFenNum,ProNum,PinPaiId,ProTeXing,MinQuantity,Zl,MinZl");

            RePage2(ds);

        }

        void SelZoneProList()
        {

            BLL.MerchantBLL mBll = new BLL.MerchantBLL();
            decimal MerchantId = ReDecimal("MerId", 0);
            int CurrentPage = ReInt("CurrentPage", 1);
            string Order = ReStr("Order", "CreateTime desc");
            bool hasProNum = ReBool("hasProNum", true);  //如果只查询有数量的
            int PageSize = ReInt("PageSize", 40);
            string ZoneId = ReStr("ZoneId", "");
            //if (BranchId == "")
            //{

            //    throw new Exception("BranchId不能为空!");
            //}

            string ProCode = ReStr("ProCode", "");
            string inputStr = ReStr("inputStr", "");
            string ProClassIds = ReStr("ProClassIds", "");
            int Status = ReInt("Status", 0);
            bool Invalid = ReBool("Invalid", false);
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            if (Status > 100)
            {
                //一般是9999
            }
            else
            {

                s.Append(" and Status =" + Status + " ");     //这里只能查询出固定状态的来, 目前是只有两种, 0(上架销售)或者-10(下架)
            }

            s.Append(" and  MerchantId=" + MerchantId + " ");
            s.Append(" and FlagInvalid='" + Invalid + "' ");




            if (ProClassIds != "")
            {

                s.Append("  and ProClassId in (" + ProClassIds + ") ");
            }


            if (inputStr != "")
            {
                s.Append(" and  KeyWord like '%" + inputStr + "%' ");
            }

            if (ProCode != "")
            {
                s.Append(" and  ProCode ='" + ProCode + "' ");
            }

            if (hasProNum)
            {
                s.Append(" and (ProNum>0 or IsInfiniteNum=1) ");

            }

            if (ZoneId != "")
            {
                s.Append(" and  BranchId In (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "')  ");
            }

            #region 页面条件

            DataTable dtSItem = ReTable("sItemArray");
            if (dtSItem != null)
            {

                if (dtSItem.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSItem.Rows)
                    {
                        string sKey = dr["sKey"].ToString();

                        switch (sKey)
                        {
                            case "ProClassId": //传递品类
                                decimal ProClassId = decimal.Parse(dr["sValueId"].ToString());
                                List<decimal> ProClassIdList = BLL.StaticBLL.GetProAllChildren(ProClassId);

                                s.Append(" and ProClassId in (" + string.Join(",", ProClassIdList) + ") ");

                                break;

                            case "KeyWord":
                                string KeyWord = dr["sValueId"].ToString();
                                s.Append(" and KeyWord like '%" + KeyWord + "%' or ProCode='" + KeyWord.Trim() + "' ");
                                if (Order == "DefaultOrder")
                                {
                                    Order = " dbo.CxCount('" + KeyWord + "',KeyWord) desc, CreateTime desc";
                                }

                                break;


                            case "PinPaiId":
                                decimal PinPaiId = decimal.Parse(dr["sValueId"].ToString());
                                s.Append(" and PinPaiId=" + PinPaiId + " ");
                                break;
                        }



                    }


                }

            }

            #endregion



            #region 扩展属性条件
            try
            {
                DataTable dtSArray = ReTable("sAttrArray");
                List<string> attrNameArray = new List<string>();
                if (dtSArray != null)
                {
                    if (dtSArray.Rows.Count > 0)
                    {
                        foreach (DataRow drSArray in dtSArray.Rows)
                        {
                            string attrCol = drSArray["attrCol"].ToString();
                            string attrType = drSArray["attrType"].ToString();
                            string attrName = drSArray["attrName"].ToString();
                            string attrValue = drSArray["attrValue"].ToString();
                            attrNameArray.Add(attrName);
                            switch (attrCol.ToLower())
                            {
                                case "reprice": //绑定价格

                                    decimal bg = decimal.Parse(attrValue.Split('_')[0]);
                                    decimal end = decimal.Parse(attrValue.Split('_')[1]);
                                    s.Append(" and RePrice >=" + bg + " and RePrice <=" + end + " ");
                                    break;

                                case "spec":  //规格

                                    s.Append(" and spec ='" + attrValue + "' ");


                                    break;
                                default:  //其他,产品扩展属性

                                    switch (attrType)
                                    {
                                        case "decimal": //数值区间
                                            decimal bg2 = decimal.Parse(attrValue.Split('_')[0]);
                                            decimal end2 = decimal.Parse(attrValue.Split('_')[1]);
                                            s.Append(" and attr.value('(/root/" + attrType + ")[1]','decimal') BETWEEN " + bg2 + " and " + end2 + " ");
                                            break;

                                        default:  //字符串

                                            s.Append(" and attr.value('(/root/" + attrType + ")[1]','nvarchar(50)') ='" + attrValue + "' ");

                                            break;
                                    }


                                    s.Append("   ");

                                    break;
                            }


                        }

                    }

                }
                ReDict2.Add("attrNameArray", string.Join(",", attrNameArray));

            }
            catch (Exception)
            {

                //不一定传的进来
            }

            #endregion


            string sWhere = s.ToString();

            if (Order == "DefaultOrder")
            {

                Order = " RecommendLv desc, CreateTime desc ";

            }

            //获得分部产品的分页列表
            DataSet ds = mBll.GetProVsBranchPageList(Order, sWhere, CurrentPage, PageSize, "ProStatusName,BranchId, BuyLv, ZgProNum,CreateTime,CreateUser,ProClassId,ProductClassName,Units,RePrice,RePrice2,RePrice3,Spec,Status, ProName,ProTitle,ProductImgUrl,ProductImgId,ProId,MerchantName, RecommendLv,ProCode,GetJiFenNum,ProNum,PinPaiId,ProTeXing,MinQuantity,Zl,MinZl,BranchName");

            RePage2(ds);
        }

        private void DingDanVsPeiHuoUser()  //配货
        {
            string DingDanId = ReStr("DingDanId", "");
            string uid = ReStr("uid", "");

            int Status = DAL.DalComm.ExInt("");

            StringBuilder s = new StringBuilder();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            mbll.ShenHeDingdan(DingDanId, 1, "通过审核并指派给配货员");
            s.Append(" UPDATE dbo.DingDanInfo SET PeiHuoUserId='" + uid + "'  WHERE DingDanId='" + DingDanId + "' ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();


        }
        /// <summary>
        /// 派送员关联订单
        /// </summary>
        private void DingDanVsPaiSongUser()  //派送
        {
            string DingDanId = ReStr("DingDanId", "");
            string uid = ReStr("uid", "");



            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.DingDanVsPaiSongUser(DingDanId, uid);


            ReTrue();


        }

        private void GetAppMenuList()
        {
            StringBuilder s = new StringBuilder();

            decimal MerRoleId = ReDecimal("MerRoleId", 0);
            if (MerRoleId == 0)
            {
                throw new Exception("MerRoleId不能为0!");
            }

            s.Append(" SELECT m.* FROM CORE.dbo.AppMenuVsMerRole   mvr WITH(NOLOCK)  ");
            s.Append(" INNER JOIN CORE.dbo.AppMenuInfo m  WITH(NOLOCK)  ON m.AppMenuId = mvr.AppMenuId ");
            s.Append(" WHERE mvr.MerRoleId=" + MerRoleId + " ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void RemoveUserMerRole()
        {
            string UserId = ReStr("uid", "");
            if (UserId == "")
            {
                throw new Exception("UserId不能为空!");
            }
            decimal MerRoleId = ReDecimal("MerRoleId", 0);
            if (MerRoleId == 0)
            {
                throw new Exception("MerRoleId不能为空!");
            }
            string BranchId = ReStr("BranchId", "");

            StringBuilder s = new StringBuilder();
            s.Append(" DELETE FROM dbo.MerRoleVsUser WHERE BranchId='" + BranchId + "' AND MerRoleId=" + MerRoleId + " AND UserId='" + UserId + "' ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void GetUserMerRole()
        {
            decimal MerId = ReDecimal("MerId", 0);
            string BrachId = ReStr("BranchId", "");
            string uid = ReStr("uid", "");
            if (uid.Trim() == "")
            {
                throw new Exception("UserId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.UserMerRoleView WHERE UserId='" + uid + "'  ");
            if (MerId != 0)
            {

                s.Append("  AND MerId='" + MerId + "' ");
            }
            if (BrachId != "")
            {
                s.Append(" and BrachId='" + BrachId + "' ");
            }
            s.Append(" order by BranchId ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetMerRoleList()
        {
            decimal MerId = ReDecimal("MerId", 0);

            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.MerRole WITH(NOLOCK)  where MerId=" + MerId + " ");
            s.Append(" SELECT * FROM dbo.Branch WITH(NOLOCK) WHERE MerchantId=" + MerId + " ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            DataTable dt_Branch = ds.Tables[1];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReDict.Add("BranchList", JsonHelper.ToJson(dt_Branch));
            ReTrue();
        }



        /// <summary>
        /// 一个分部的多个商品改变状态
        /// </summary>
        void BranchProStatus()
        {
            string ProIds = ReStr("ProIds", "");
            string BranchId = ReStr("BranchId", "");
            if (BranchId == "")
            {
                throw new Exception("没有选中的分部!");
            }

            if (ProIds == "")
            {
                throw new Exception("没有选中的商品");
            }

            int Status = ReInt("Status", 0);
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.ProVsBranch SET Status='" + Status + "' WHERE ProId in (" + ProIds + ") AND BranchId ='" + BranchId + "'  ");

            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();


        }


        //一个商品从多个分部改变状态
        private void ProBranchStatus()
        {

            string ProId = ReStr("ProId", "");
            string BranchIds = ReStr("BranchIds", "");
            if (BranchIds == "")
            {
                throw new Exception("没有选中的分部!");
            }

            int Status = ReInt("Status", 0);
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.ProVsBranch SET Status='" + Status + "' WHERE ProId='" + ProId + "' AND BranchId in (" + BranchIds + ")  ");

            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void UpAllProToBranch()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            decimal MerId = ReDecimal("MerId", 0);
            string BranchId = ReStr("BranchId", "");
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            if (BranchId.Trim() == "")
            {
                throw new Exception("BranchId不能为空!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" select * from dbo.Product with(nolock) where MerchantId='" + MerId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtPro = ds.Tables[0];

            foreach (DataRow drPro in dtPro.Rows)
            {

                mbll.UpProToBranch(drPro["ProId"].ToString(), BranchId);  //上货


            }
            ReTrue();


        }

        private void UpProToBranch()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            string BranchIds = ReStr("BranchIds", "");
            if (BranchIds.Trim() == "")
            {
                throw new Exception("ProIds不能为空!");
            }
            string ProId = ReStr("ProId", "");
            if (ProId.Trim() == "")
            {
                throw new Exception("ProId不能为空");
            }
            List<string> BranchIdsList = new List<string>(BranchIds.Split(','));
            foreach (string BranchId in BranchIdsList)
            {

                mbll.UpProToBranch(ProId, BranchId);  //上货


            }
            ReTrue();

        }


        /// <summary>
        /// 商品类别对应的 --- 分部列表!!
        /// </summary>
        private void GetProVsBranchList()
        {

            string ProId = ReStr("ProId", "");
            decimal MerId = ReDecimal("MerId", 0);
            if (ProId == "")
            {
                throw new Exception("ProId不能为空!");
            }


            StringBuilder s = new StringBuilder();
            s.Append(" SELECT * FROM dbo.Branch b ");
            s.Append(" LEFT JOIN dbo.ProVsBranch pvb ON pvb.ProId='" + ProId + "' AND  b.BranchId=pvb.BranchId  ");
            s.Append(" WHERE b.MerchantId=" + MerId + " ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void BindBranch()
        {
            decimal BranchId = ReDecimal("BranchId", 0);
            if (BranchId == 0)
            {
                throw new Exception("BranchId不能为0");
            }

            StringBuilder s = new StringBuilder();
            s.Append("  SELECT * FROM dbo.Branch with(nolock) WHERE BranchId='" + BranchId + "'  ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];

            ReDict.Add("info", JsonHelper.ToJsonNo1(dt));
            ReTrue();

        }

        private void GetBranchList()
        {

            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            StringBuilder s = new StringBuilder();
            s.Append("  SELECT * FROM dbo.Branch with(nolock) WHERE MerchantId='" + MerId + "' ORDER BY OrderNo DESC ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void SaveBranch()
        {

            Model.BranchModel model = new BranchModel();
            model.BranchId = ReStr("BranchId");
            model.BranchVsId = ReStr("BranchVsId", "");
            model.BranchMemo = ReStr("BranchMemo", "");
            model.BranchName = ReStr("BranchName", "");
            model.MerchantId = ReDecimal("MerId", 0);

            model.Lat = ReDecimal("Lat", 0);
            model.Lng = ReDecimal("Lng", 0);
            model.BranchTel = ReStr("BranchTel", "");
            model.BranchPhoneNo = ReStr("BranchPhoneNo", "");
            model.QQ = ReStr("QQ", "");
            model.QQUrl = ReStr("QQUrl", "");
            model.ImgId = ReDecimal("ImgId", 0);
            model.OrderNo = ReInt("OrderNo", 0);
            if (model.MerchantId == 0)
            {
                throw new Exception("MerchantId不能为0!");
            }
            //model.Lat = 0;
            //model.Lng = 0;
            model.OrderNo = ReInt("OrderNo", 1);
            model.ImgId = 0;
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.SaveBranch(model);
            ReDict2.Add("BranchId", model.BranchId);
            ReTrue();

        }

        private void NewDingDanPush()
        {
            //decimal MerId = ReDecimal("MerId");
            //string BranchId = ReStr("Branchid", "");
            //BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            //mbll.NewDingDanTiXing(MerId, BranchId);
            //ReTrue();
        }

        private void CuXiaoVsProVsKeyChange()
        {

            decimal CuXiaoId = ReDecimal("CuXiaoId", 0);
            string VsKey = ReStr("VsKey", "");
            string ProId = ReStr("ProId", "");
            StringBuilder s = new StringBuilder();
            s.Append("UPDATE dbo.CuXiaoVsPro SET VsKey='" + VsKey + "' WHERE CuXiaoId='" + CuXiaoId + "' and ProId='" + ProId + "'");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }

        private void DelPeiSongTimeSolt()
        {

            decimal PeiSongTimeSoltId = ReDecimal("PeiSongTimeSoltId", 0);
            StringBuilder s = new StringBuilder();


            s.Append("  DELETE FROM dbo.PeiSongTimeSolt WHERE PeiSongTimeSoltId='" + PeiSongTimeSoltId + "' ");
            DAL.DalComm.ExReInt(s.ToString());

            ReTrue();



        }

        private void PaiSongTuiSong()
        {

            string DingDanId = ReStr("DingDanId", "");
            string PaiSongUserId = ReStr("PaiSongUserId", "");
            if (DingDanId == "")
            {
                throw new Exception("订单ID不能为空");
            }
            string PaiSongUserName = "";
            string PaiSongUserPhone = "";
            decimal Amount = 0;
            decimal PayAmount = 0;
            string MerName = "";
            string PhoneNo = "";
            decimal MerId = 0;
            int Status = 0;
            StringBuilder s = new StringBuilder();
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion



                s.Append(" SELECT PayAmount,Amount,Phone,Status FROM dbo.DingDanView WITH(NOLOCK) WHERE  DingDanId='" + DingDanId + "' ");
                s.Append(" SELECT RealName, Phone FROM  dbo.UserView  WITH(NOLOCK) WHERE UserId='" + PaiSongUserId + "' ");
                s.Append(" UPDATE dbo.DingDanInfo SET Status=40 WHERE DingDanId='" + DingDanId + "' ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());

                DataTable dtDingDan = ds.Tables[0];
                if (dtDingDan.Rows.Count == 0)
                {
                    throw new Exception("没有找到订单号为" + DingDanId + "的订单!");
                }

                #region 订单赋值
                DataRow drDingDan = dtDingDan.Rows[0];
                Status = int.Parse(drDingDan["Status"].ToString());
                if (Status >= 100)
                    throw new Exception("当前订单状态编码为" + Status + ",(100为送到,110为确认)");
                {


                }
                PhoneNo = drDingDan["Phone"].ToString();
                Amount = Math.Round(decimal.Parse(drDingDan["Amount"].ToString()), 2);
                PayAmount = Math.Round(decimal.Parse(drDingDan["PayAmount"].ToString()), 2);
                #endregion

                #region 派送人员赋值

                DataTable dtPaiSongUser = ds.Tables[1];
                if (dtPaiSongUser.Rows.Count > 0)
                {

                    DataRow drPaiSongUser = dtPaiSongUser.Rows[0];
                    PaiSongUserName = drPaiSongUser["RealName"].ToString();
                    PaiSongUserPhone = drPaiSongUser["Phone"].ToString();
                }

                #endregion

                #region 商家赋值

                DataSet dsMer = BLL.StaticBLL.GetMer();
                DataTable dtMer = dsMer.Tables[0];
                if (dtMer.Rows.Count > 0)
                {

                    DataRow drMer = dtMer.Rows[0];
                    MerName = drMer["MerchantName"].ToString();
                    MerId = decimal.Parse(drMer["MerchantId"].ToString());
                }
                #endregion

                #region 发送短信
                Model.StMsgModel model = new Model.StMsgModel();
                model.CreateTime = DateTime.Now;
                model.MerId = MerId;
                model.PhoneNo = PhoneNo;
                if (!Common.Validator.IsMobile(model.PhoneNo))
                {
                    throw new Exception("不合法的手机号?");
                }



                model.StMsgClassId = ReDecimal("StMsgClassId", 1);
                model.StMsgCode = "";
                model.StMsgContent = "您好!我是" + MerName + "的派送员[" + PaiSongUserName + "](tel:" + PaiSongUserPhone + "),您的订单马上送达, 请做好订单收货准备, 订单总额:" + Amount + "元, 支付金额:" + PayAmount + "元  ";
                model.ReKey = "null";
                model.StMsgId = 0;
                model.StMsgTypeId = ReDecimal("StMsgTypeId", 10); //10是派送推送!
                BLL.StaticBLL.SendStMsg(model);
                #endregion

                #region 写入订单日志
                Model.DingDanLogModel ddlModel = new DingDanLogModel();
                ddlModel.CreateTime = DateTime.Now;
                ddlModel.DingDanClassId = 40;
                ddlModel.DingDanId = DingDanId;
                ddlModel.DingDanLogTypeId = 40;
                ddlModel.Memo = "派送人员[" + PaiSongUserName + "](tel:" + PaiSongUserPhone + ")正在派件,并发送收货提醒";
                BLL.MerchantBLL mbll = new BLL.MerchantBLL();
                mbll.SaveDingDanLog(ddlModel);
                #endregion
                ReTrue();
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion



        }



        void GetProClassAll()
        {
            StringBuilder s = new StringBuilder();
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            DataTable dtProClass = BLL.StaticBLL.GetProClassCache(MerId).Tables[0].Copy();

            dtProClass.Columns.Add("cld", typeof(System.String));

            dtProClass.Columns.Remove("AttrSelXml");
            dtProClass.Columns.Remove("ProClassKeyWord");
            dtProClass.Columns.Remove("InheritPeiSongType");
            dtProClass.Columns.Remove("InheritProTeXing");
            dtProClass.Columns.Remove("ProTeXing");
            dtProClass.Columns.Remove("GetJiFenNum");
            dtProClass.Columns.Remove("InheritJiFenNum");
            dtProClass.Columns.Remove("CldProClassIds");
            dtProClass.Columns.Remove("InheritDiscount");
            dtProClass.Columns.Remove("Discount");
            dtProClass.Columns.Remove("MerchantId");
            dtProClass.Columns.Remove("AppMemo");
            dtProClass.Columns.Remove("Invalid");
            dtProClass.Columns.Remove("ProClassColor");
            dtProClass.Columns.Remove("ProductClassAppName");
            dtProClass.Columns.Remove("Memo");
            dtProClass.Columns.Remove("OrderNo");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");


            ReDict.Add("ProClassAll", JsonHelper.ToJson(dtProClass));
            ReTrue();
        }
        private void GetProClassByJson()
        {

            StringBuilder s = new StringBuilder();
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("MerId不能为0!");
            }
            DataTable dtProClass = BLL.StaticBLL.GetProClassCache(MerId).Tables[0].Copy();

            dtProClass.Columns.Add("cld", typeof(System.String));

            dtProClass.Columns.Remove("AttrSelXml");
            dtProClass.Columns.Remove("ProClassKeyWord");
            dtProClass.Columns.Remove("InheritPeiSongType");
            dtProClass.Columns.Remove("InheritProTeXing");
            dtProClass.Columns.Remove("ProTeXing");
            dtProClass.Columns.Remove("GetJiFenNum");
            dtProClass.Columns.Remove("InheritJiFenNum");
            dtProClass.Columns.Remove("CldProClassIds");
            dtProClass.Columns.Remove("InheritDiscount");
            dtProClass.Columns.Remove("Discount");
            dtProClass.Columns.Remove("MerchantId");
            dtProClass.Columns.Remove("AppMemo");
            dtProClass.Columns.Remove("Invalid");
            dtProClass.Columns.Remove("ProClassColor");
            dtProClass.Columns.Remove("ProductClassAppName");
            dtProClass.Columns.Remove("Memo");
            dtProClass.Columns.Remove("OrderNo");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");
            //dtProClass.Columns.Remove("ProductClassImgId");
            DataTable dtProClass1 = Common.DataSetting.TableSelect(" ParentProductClassId=0 ", dtProClass);



            foreach (DataRow drProClass1 in dtProClass1.Rows)
            {

                var ProductClassId1 = drProClass1["ProductClassId"].ToString();
                DataTable dtProClass2 = Common.DataSetting.TableSelect(" ParentProductClassId=" + ProductClassId1 + " ", dtProClass);
                if (dtProClass2.Rows.Count > 0)
                {
                    foreach (DataRow drProClass2 in dtProClass2.Rows)
                    {

                        var ProductClassId2 = drProClass2["ProductClassId"].ToString();
                        DataTable dtProClass3 = Common.DataSetting.TableSelect(" ParentProductClassId=" + ProductClassId2 + " ", dtProClass);
                        if (dtProClass3.Rows.Count > 0)
                        {
                            drProClass2["cld"] = JsonHelper.ToJson(dtProClass3);
                        }
                        else
                        {

                            drProClass2["cld"] = "[]";
                        }


                    }
                    drProClass1["cld"] = JsonHelper.ToJson(dtProClass2);
                    //drProClass1["cld"] = "[]";
                }
                else
                {
                    drProClass1["cld"] = "[]";
                }






            }

            ReDict.Add("ProClassJson", JsonHelper.ToJson(dtProClass1));
            ReTrue();


        }

        private void ChangeMemoName()
        {


            StringBuilder s = new StringBuilder();
            decimal MemberId = ReDecimal("MemberId", 0);
            string MemoName = ReStr("MemoName", "");
            s.Append("UPDATE dbo.Member SET MemoName='" + MemoName + "' WHERE MemberId='" + MemberId + "'");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();
        }



        private void RemoveDetail()
        {
            StringBuilder s = new StringBuilder();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            decimal DingDanDetailId = ReDecimal("DingDanDetailId", 0);
            if (DingDanDetailId == 0)
            {
                throw new Exception("DingDanDetailId不能为0!");
            }
            s.Append("SELECT Status FROM dbo.DingDanDetailView WHERE DingDanDetailId='" + DingDanDetailId + "' ");
            int Status = DAL.DalComm.ExInt(s.ToString());
            if (Status >= 10 && Status < 40)
            {
                mbll.RemoveDetail(DingDanDetailId);
            }
            else
            {
                throw new Exception("只有订单状态为:下单-配货完成 之间的订单才可以删除明细!");
            }

            ReTrue();
        }

        private void NewDingDan()
        {
            decimal CMerId = ReDecimal("CMerId", 0);
            if (CMerId == 0)
            {
                throw new Exception("不能确定MerId!");

            }

            DataSet ds = DAL.DalComm.BackData(" select DingDanId FROM dbo.DingDanView WITH(NOLOCK) WHERE Status BETWEEN 10 AND 30 AND MerchantId=" + CMerId + " ");
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void InvalidPingJia()   //作废评价
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            decimal PingJiaId = ReDecimal("PingJiaId", 0);
            mbll.InvalidPingJia(PingJiaId);
            ReTrue();
        }
        void GetDingDanLogByDingDanId()
        {
            string DingDanId = ReStr("DingDanId", "");
            if (DingDanId == "")
            {
                throw new Exception("获取不到订单ID");
            }
            DAL.DingDanLogDAL dal = new DAL.DingDanLogDAL();
            DataSet ds = dal.GetList(" DingDanId='" + DingDanId + "' order by CreateTime desc ");
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }
        private void SearchJiFenChange()
        {

            DateTime dtm1 = ReTime("CreateTime1", DateTime.Parse("1900-01-01"));
            DateTime dtm2 = ReTime("CreateTime2", DateTime.Parse("3000-01-01"));
            int CurrentPage = ReInt("CurrentPage", 1);
            string MemberPhone = ReStr("MemberPhone", "");
            string MemberCode = ReStr("MemberCode", "");
            string MemberName = ReStr("MemberName", "");
            decimal MerId = ReDecimal("MerId", 0);
            int JiFenChangeTypeId = ReInt("JiFenChangeTypeId", 0);
            if (MerId == 0)
            {
                throw new Exception("没有MerId不行!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" CreateTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "'  ");
            if (MemberPhone != "")
            {
                s.Append(" and Phone='" + MemberPhone + "' ");
            }
            if (MemberCode != "")
            {
                s.Append(" and MemberCode='" + MemberCode + "' ");
            }
            if (MemberName != "")
            {
                // s.Append("  and (NickName like '%" + MemberName + "%' or  MemoName like '%" + MemberName + "%' ) ");
            }
            if (MerId != 0)
            {
                s.Append(" and MerId='" + MerId + "' ");
            }
            if (JiFenChangeTypeId != 0)
            {
                s.Append(" and JiFenChangeTypeId='" + JiFenChangeTypeId + "' ");
            }

            DAL.JiFenChangeDAL dal = new DAL.JiFenChangeDAL();

            string order = " CreateTime desc ";

            DataSet ds = dal.GetPageList(s.ToString(), order, CurrentPage, 20, "*");
            RePage2(ds);

        }

        void AddJifen()
        {
            Model.JiFenChangeModel model = new JiFenChangeModel();
            model.JiFenChangeNum = ReDecimal("JiFenChangeNum", 0);
            if (model.JiFenChangeNum == 0)
            {
                throw new Exception("变更数为0或不是数字!");
            }
            model.JiFenChangeMemo = ReStr("JiFenChangeMemo", "");
            model.JiFenChangeClassId = 1;
            model.CreateTime = DateTime.Now;
            if (model.JiFenChangeNum > 0)
            {
                model.JifenChangeTypeId = 5; //赠与
            }
            else
            {
                model.JifenChangeTypeId = 6;  //扣除
            }
            model.MemberId = ReDecimal("MemberId", 0);
            if (model.MemberId == 0)
            {
                throw new Exception("Member没有明确!");
            }
            model.ReKey = Common.CookieSings.GetCurrentUserId();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.ChangeMemberJiFen(Convert.ToInt32(model.JiFenChangeNum), model.MemberId, model);
            ReTrue();

        }

        void ChangeDingDanDetail()
        {

            BLL.UserBLL ubll = new BLL.UserBLL();
            string DingDanId = ReStr("DingDanId", "");
            if (DingDanId == "")
            {
                throw new Exception("订单ID不能为空!");
            }
            decimal DingDanDetailId = ReDecimal("DingDanDetailId", 0);
            if (DingDanDetailId == 0)
            {
                throw new Exception("DingDanDetailId不能为空!");
            }
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            Model.DingDanDetailModel model = mbll.GetDingDanDetailModel(DingDanDetailId);
            model.Price = ReDecimal("Price");
            model.Quantity = ReDecimal("Quantity");
            string UserId = ReStr("uid", ubll.CurrentUserRealName());
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                mbll.SaveDingDanDetail(model);

                #region 写入修改日志


                #endregion
                Model.DingDanLogModel logModel = new DingDanLogModel();
                logModel.CreateTime = DateTime.Now;
                logModel.DingDanClassId = 0;
                logModel.DingDanId = DingDanId;
                logModel.DingDanLogTypeId = 25;
                logModel.Memo = "订单明细修改, 明细号:" + DingDanDetailId + " 单价:" + model.Price + ",数量:" + model.Quantity + ",操作员:" + UserId + " ";
                mbll.SaveDingDanLog(logModel);
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            ReTrue();

        }

        private void GetDingDanInfo()
        {
            string DingDanId = ReStr("DingDanId", "");
            if (DingDanId == "")
            {
                throw new Exception("订单ID不能为空!");
            }
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DataSet ds = mbll.GetDingDanInfo(DingDanId);
            DataTable dtInfo = ds.Tables[0];
            DataTable dtDetail = ds.Tables[1];
            DataTable dtLog = ds.Tables[2];
            ReDict.Add("Info", JsonHelper.ToJsonNo1(dtInfo));
            ReDict.Add("Detail", JsonHelper.ToJson(dtDetail));
            ReDict.Add("Log", JsonHelper.ToJson(dtLog));
            ReTrue();


        }

        private void GetDingDanInfoByDingDanQueRen()
        {




            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            BLL.MemberBLL memberBll = new BLL.MemberBLL();

            string BranchId = ReStr("BranchId", "");
            string SiteId = ReStr("SiteId", "");


            if (BranchId == "")
            {

                throw new Exception("BranchId不能为空!");
            }

            if (SiteId == "")
            {
                throw new Exception("SiteId不能为空!");
            }

            DataSet ds = mbll.GetDingDanInfoByDingDanQueRen(memberBll.GetCurrentDecimalMemberId(), BranchId, SiteId);

            DataTable dtGwc = ds.Tables[0];
            DataTable dtDingDanPeiSongType = ds.Tables[1]; //这里取得的是订单里的产品支持的配送方式
            DataTable MyAddress = ds.Tables[2];
            decimal MerId = ReDecimal("MerId", 0);

            if (MerId == 0)
            {
                throw new Exception("MerId不能为0");
            }
            bool ProHasPsType = ReBool("ProHasPsType", false);
            ReDict.Add("gwc", JsonHelper.ToJson(dtGwc));
            ReDict.Add("MyDefaultAddress", JsonHelper.ToJsonNo1(MyAddress));
            ReDict.Add("MyAddressList", JsonHelper.ToJson(MyAddress));
            ReDict.Add("CurrentMember", JsonHelper.ToJsonNo1(ds.Tables[3]));
            DataSet dsPeiSongType = mbll.dsPeiSongType(MerId, BranchId);
            DataTable dtPeiSongType = dsPeiSongType.Tables[0].Copy();
            try
            {
                //送达时间
                dtPeiSongType.Columns.Add("PeiSongEndTime");
            }
            catch (Exception)
            {


            }


            foreach (DataRow drPeiSongType in dtPeiSongType.Rows)
            {

                TimeSpan PeiSongTime = TimeSpan.Parse(drPeiSongType["PeiSongTime"].ToString());
                TimeSpan PeiHuoTime = TimeSpan.Parse(drPeiSongType["PeiHuoTime"].ToString());
                DateTime PeiSongEndTime = DateTime.Now + PeiHuoTime + PeiSongTime;

                if (drPeiSongType["PeiSongTeXing"].ToString() == "30")
                {

                    if (PeiSongEndTime.Day != DateTime.Now.Day)//这里必须是等于30的才会删除
                    {
                        //如果是即时配送,而且配送时间超过了今天, 那么就删除了吧
                        drPeiSongType.Delete();
                        continue;
                    }

                    TimeSpan nowTime = DateTime.Now.TimeOfDay;

                    TimeSpan BgTimeForDay = TimeSpan.Parse(drPeiSongType["BgTimeForDay"].ToString());
                    TimeSpan EndTimeForDay = TimeSpan.Parse(drPeiSongType["EndTimeForDay"].ToString());

                    if (nowTime < BgTimeForDay || nowTime > EndTimeForDay)
                    {  //在即时配送的前提下, 如果配送时间小于这个配送开始的时间, 或者大于这个配送结束的时间, 都不得使用这个配送
                        drPeiSongType.Delete();
                        continue;
                    }

                }




                drPeiSongType["PeiSongEndTime"] = PeiSongEndTime.ToString("yyyy/MM/dd HH:mm:ss");


                //循环所有支持的配送方式
                int PeiSongTypeId = int.Parse(drPeiSongType["PeiSongTypeId"].ToString());

                if (ProHasPsType)
                {
                    //如果需要挨个产品检测这个产品具不具备此派送方式, 否则将不显示此派送方式
                    bool has = false;
                    foreach (DataRow drDingDanPeiSongType in dtDingDanPeiSongType.Rows)
                    {
                        //循环这些订单明细中支持的配送方式





                        int DingDanPeiSongTypeId = int.Parse(drDingDanPeiSongType["PeiSongTypeId"].ToString());
                        if (PeiSongTypeId == DingDanPeiSongTypeId)
                        {
                            has = true;
                        }

                    }

                    if (!has)
                    {
                        drPeiSongType.Delete();
                    }
                }



            }



            dtPeiSongType.AcceptChanges();
            try
            {  //尝试一下, 有可能已经写入
                dtPeiSongType.Columns.Add("TimeSoltJsonArray");

            }
            catch
            {


            }




            DataTable dtTimeSolt = dsPeiSongType.Tables[1];
            try
            {  //尝试一下,有可能缓存中已经写入

                dtTimeSolt.Columns.Add("Date");
            }
            catch
            {


            }



            dtTimeSolt.Columns.Add("今天明天");
            foreach (DataRow drPeiSongType in dtPeiSongType.Rows)
            {
                DataTable dtTimeSoltArray = dtTimeSolt.Clone();

                int PeiSongTeXing = int.Parse(drPeiSongType["PeiSongTeXing"].ToString());
                TimeSpan PeiHuoTime = TimeSpan.Parse(drPeiSongType["PeiHuoTime"].ToString());
                int PeiSongTypeId = int.Parse(drPeiSongType["PeiSongTypeId"].ToString());






                DataTable myDtTimeSolt = Common.DataSetting.TableSelect(" PeiSongTypeId='" + PeiSongTypeId + "' ", dtTimeSolt);
                StringBuilder w = new StringBuilder();
                switch (PeiSongTeXing)
                {
                    case 10: //定时达
                    case 30: //神速达
                        int SelDay = int.Parse(drPeiSongType["SelDay"].ToString());

                        for (int i = 0; i < SelDay; i++)
                        {
                            string 日期 = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");

                            foreach (DataRow mydrTimeSolt in myDtTimeSolt.Rows)
                            {

                                mydrTimeSolt["Date"] = 日期;

                                DateTime 派送开始时间 = DateTime.Parse(mydrTimeSolt["Date"].ToString() + " " + mydrTimeSolt["BgTime"].ToString());
                                if (SelDay > 0)
                                {
                                    //如果当前时间大于配货开始时间
                                    if ((DateTime.Now + PeiHuoTime) > 派送开始时间)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                    }

                                    var d = Common.TimeString.dateDiff(DateTime.Now, 派送开始时间);

                                    if (d == 0)
                                    {
                                        mydrTimeSolt["今天明天"] = "今天";
                                    }
                                    else if (d == 1)
                                    {
                                        mydrTimeSolt["今天明天"] = "明天";
                                    }
                                    else if (d == 2)
                                    {
                                        mydrTimeSolt["今天明天"] = "后天";
                                    }
                                    else if (d == 3)
                                    {
                                        mydrTimeSolt["今天明天"] = "大后天";
                                    }
                                    else
                                    {
                                        mydrTimeSolt["今天明天"] = "" + d + "天后";
                                    }


                                    dtTimeSoltArray.Rows.Add(mydrTimeSolt.ItemArray); //添加进去
                                }
                                else
                                {
                                    //如果等于0的话, 那就是个废的
                                }

                            }
                        }
                        drPeiSongType["TimeSoltJsonArray"] = JsonHelper.ToJson(dtTimeSoltArray);
                        break;
                }
            }
            ReDict.Add("PsType", JsonHelper.ToJson(dtPeiSongType));

            DataSet dsPayType = BLL.StaticBLL.PayType();
            ReDict.Add("PayType", JsonHelper.ToJson(dsPayType.Tables[0]));
            ReTrue();
        }

        private void ChangeJiFenNumForProClass()
        {
            decimal ProductClassId = ReDecimal("ProductClassId");
            bool InheritJiFenNum = ReBool("InheritJiFenNum", true);
            decimal GetJiFenNum = ReDecimal("GetJiFenNum", 0) * decimal.Parse("0.01");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.ChangeJiFenNumForProClass(ProductClassId, InheritJiFenNum, GetJiFenNum);
            ReTrue();
        }

        private void ChangeProTeXingForProClass()
        {
            decimal ProductClassId = ReDecimal("ProductClassId");
            bool InheritProTeXing = ReBool("InheritProTeXing", true);
            int ProTeXingId = ReInt("ProTeXingId", 0);


            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.ChangeProTeXingForProClass(ProductClassId, InheritProTeXing, ProTeXingId);
            ReTrue();




        }

        void DelCuXiaoVsPro()
        {

            string ProId = ReStr("ProId");
            decimal CuXiaoId = ReDecimal("CuXiaoId");

            DAL.CuXiaoVsProDAL dal = new DAL.CuXiaoVsProDAL();
            dal.DeleteList(" CuXiaoId='" + CuXiaoId + "' and ProId='" + ProId + "' ");
            ReTrue();

        }

        void QuickSerchPro()
        {
            string ProName = ReStr("ProName", "");
            string ProId = ReStr("ProId", "");
            string ProCode = ReStr("ProCode", "");
            decimal MerId = ReDecimal("MerId");
            string col = ReStr("col", "*");
            int CurrentPage = ReInt("CurrentPage", 1);
            string BranchId = ReStr("BranchId", "");
            decimal CuXiaoId = ReDecimal("CuXiaoId", 0);
            string ZoneId = ReStr("ZoneId", "");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DAL.ProVsBranchDAL dal = new DAL.ProVsBranchDAL();
            StringBuilder s = new StringBuilder();
            s.Append(" MerchantId=" + MerId + " and FlagInvalid=0 ");
            if (ProName.Trim() != "")
            {
                s.Append(" and ProName like '%" + ProName + "%' ");

            }
            if (BranchId != "")
            {
                s.Append(" and BranchId='" + BranchId + "' ");
            }

            if (ProId.Trim() != "")
            {

                s.Append(" and ProId='" + ProId + "' ");
            }

            if (ProCode != "")
            {
                s.Append(" and ProCode='" + ProCode + "' ");
            }

            if (CuXiaoId != 0)
            {
                s.Append(" and ProId NOT IN (select ProId FROM dbo.CuXiaoVsPro WHERE CuXiaoId='" + CuXiaoId + "')   ");
            }

            if (ZoneId != "")
            {

                s.Append(" and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "') ");
            }
            else
            {

                throw new Exception("ZoneId不能等于空!");
            }


            string Order = " CreateTime desc ";
            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, 10, col);
            RePage2(ds);



        }

        void GetCuXiaoList()
        {
            string CuXiaoName = ReStr("CuXiaoName", "");
            int Status = ReInt("Status", 0);
            int CurrentPage = ReInt("CurrentPage", 1);
            int PageSize = ReInt("PageSize", 1);
            decimal MerId = ReDecimal("MerId");
            string BranchId = ReStr("BranchId", "");
            string ZoneId = ReStr("ZoneId", "");
            bool Invalid = ReBool("Invalid", false);
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" and MerId=" + MerId + " ");
            s.Append(" and Invalid='" + Invalid + "' ");
            if (CuXiaoName != "")
            {
                s.Append(" and CuXiaoName like'%" + CuXiaoName + "%' ");
            }
            if (Status != 0)
            {
                s.Append(" and Status =" + Status + " ");
            }
            if (BranchId != "")
            {
                s.Append(" and BranchId='" + BranchId + "' ");
            }

            if (ZoneId != "")
            {
                s.Append(" and ZoneId='" + ZoneId + "' ");
            }

            string Order = " CreateTime desc ";

            DataSet ds = mbll.GetCuXiaoList(s.ToString(), Order, CurrentPage, PageSize, "*");
            RePage2(ds);

        }

        private void GetCuXiaoInfo()
        {
            decimal CuXiaoId = ReDecimal("CuXiaoId", 0);

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DataSet ds = mbll.GetCuXiaoInfo(CuXiaoId);
            DataTable dt = ds.Tables[0];
            ReDict.Add("info", JsonHelper.ToJsonNo1(dt));
            ReTrue();



        }

        private void GetCuXiaoVsProList()
        {
            decimal CuXiaoId = ReDecimal("CuXiaoId", 0);
            int CurrentPage = ReInt("CurrentPage", 1);
            int PageSize = ReInt("PageSize", 100);
            string col = ReStr("col", "*");
            string ZoneId = ReStr("ZoneId", "");
            string Status = ReStr("Status", "normal");


            StringBuilder s = new StringBuilder();
            s.Append(" CuXiaoId='" + CuXiaoId + "' ");
            if (Status == "normal")
            {
                s.Append(" and Status >=0 ");
            }
            else if (Status == "all")
            {

            }


            s.Append("  ");
            if (ZoneId != "")
            {
                s.Append(" and ZoneId='" + ZoneId + "' ");
            }



            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            string Order = " VsOrderNo desc ";
            DAL.ProductDAL dal = new DAL.ProductDAL();

            DataSet ds = mbll.GetCuXiaoVsProList(s.ToString(), Order, CurrentPage, PageSize, col);
            DataSet ds2 = mbll.GetCuXiaoInfo(CuXiaoId);
            RePage2(ds, ds2);
        }

        private void SaveCuXiaoVsPro()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            Model.CuXiaoVsProModel model = new CuXiaoVsProModel();
            model.CuXiaoId = ReDecimal("CuXiaoId");


            model.ProId = ReStr("ProId");
            model.VsProName = ReStr("VsProName", "");
            model.OrderNo = ReInt("OrderNo", 1);
            model.BranchId = ReStr("BranchId", "");
            if (model.BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }

            mbll.SaveCuXiaoVsPro(model);
            ReTrue();


        }

        private void SaveCuXiaoInfo()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            Model.CuXiaoModel model = new CuXiaoModel();
            model.CuXiaoId = ReDecimal("CuXiaoId", 0);
            if (model.CuXiaoId != 0)
            {
                model = mbll.GetCuXiaoModel(model.CuXiaoId);
            }
            model.CuXiaoName = ReStr("CuXiaoName", "");
            if (model.CuXiaoName == "")
            {
                throw new Exception("促销名称不能为空!");
            }

            model.CuXiaoLabel = Common.PinYin.GetFullCodstring(model.CuXiaoName);
            model.BranchId = ReStr("BranchId", "");
            model.CuXiaoContent = ReStr("CuXiaoContent", "");
            model.CreateUser = Common.CookieSings.GetCurrentUserId();
            model.BgTime = ReTime("BgTime");
            model.EndTime = ReTime("EndTime");
            model.Status = ReInt("Status", 10);
            model.MerId = ReDecimal("MerId", 0);
            model.ZoneId = ReStr("ZoneId", "");
            if (model.ZoneId == "")
            {
                throw new Exception("ZoneId不能为空!");
            }
            mbll.SaveCuXiao(model);
            ReDict2.Add("CuXiaoId", model.CuXiaoId.ToString());
            ReTrue();

        }


        /// <summary>
        /// 清空产品的品牌
        /// </summary>
        private void ClearPinPai()
        {

            string ProId = ReStr("ProId", "");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            if (ProId == "")
            {

            }
            else
            {
                mbll.ClearPinPai(ProId);
            }
            ReTrue();
        }

        private void InvalidPinPai()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            decimal PinPaiId = ReDecimal("PinPaiId", 0);
            bool Invalid = ReBool("Invalid", true);
            mbll.InvalidPinPai(PinPaiId, Invalid);
            ReTrue();





        }

        private void SavePeiSongTimeSolt()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            Model.PeiSongTimeSoltModel model = new PeiSongTimeSoltModel();
            model.PeiSongTimeSoltId = ReDecimal("PeiSongTimeSoltId", 0);
            model.PeiSongTypeId = ReDecimal("PeiSongTypeId");
            model.BgTime = ReTimeSpan("BgTime");
            model.EndTime = ReTimeSpan("EndTime");
            model.Memo = ReStr("Memo", "");
            model.OrderNo = ReInt("OrderNo", 1);

            mbll.SavePeiSongTimeSolt(model);
            ReTrue();
        }

        private void GetPeiSongTime()
        {
            decimal PeiSongTypeId = ReDecimal("PeiSongTypeId", 0);
            DAL.PeiSongTimeSoltDAL dal = new DAL.PeiSongTimeSoltDAL();

            DataSet ds = dal.GetList(" PeiSongTypeId=" + PeiSongTypeId + " order by orderNo desc ");
            DataTable dt = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();






        }

        private void SavePeiSongTypeInfo()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();


            Model.PeiSongTypeModel model = new PeiSongTypeModel();
            model.PeiSongTypeId = ReDecimal("PeiSongTypeId");
            model.PeiSongTypeName = ReStr("PeiSongTypeName");
            model.MerId = ReDecimal("MerId");
            model.DefaultPrice = ReDecimal("DefaultPrice");
            model.FullPrice = ReDecimal("FullPrice");
            model.PeiSongTypeMemo = ReStr("PeiSongTypeMemo");
            model.OrderNo = ReInt("OrderNo");
            model.PeiSongTeXing = ReInt("PeiSongTeXing");
            model.SelDay = ReInt("SelDay");
            model.PeiSongTime = ReTimeSpan("PeiSongTime");
            model.PeiHuoTime = ReTimeSpan("PeiHuoTime");
            model.BgTimeForDay = ReTimeSpan("BgTimeForDay");
            model.EndTimeForDay = ReTimeSpan("EndTimeForDay");
            model.BranchId = ReStr("BranchId", "");
            mbll.SavePeiSongType(model);
            ReTrue();


        }

        private void GetPeiSongTypeInfo()
        {
            decimal PeiSongTypeId = ReDecimal("PeiSongTypeId", 0);

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DataSet ds = mbll.GetPeiSongTypeInfo(PeiSongTypeId);
            DataTable dtPeiSongType = ds.Tables[0];
            DataTable dtPeiSongTime = ds.Tables[1];
            ReDict.Add("PeiSongType", JsonHelper.ToJsonNo1(dtPeiSongType));
            ReDict.Add("PeiSontTime", JsonHelper.ToJson(dtPeiSongTime));
            ReTrue();
        }

        private void DelMerConfig()
        {
            string MerConfigId = ReStr("MerConfigId", "");
            decimal MerId = ReDecimal("MerId", 0);

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.DelMerConfig(MerId, MerConfigId);
            ReTrue();

        }

        private void SaveMerConfig()
        {
            Model.MerConfigModel model = new MerConfigModel();
            model.MerConfigId = ReStr("MerConfigId");
            model.MerId = ReDecimal("MerId");
            model.MerConfigTitle = ReStr("MerConfigTitle");
            model.MerConfigVal = ReStr("MerConfigVal");
            model.MerConfigTypeId = ReInt("MerConfigTypeId", 1);
            model.Memo = ReStr("Memo", "");
            model.OrderNo = ReInt("OrderNo", 1);

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.SaveMerConfig(model);
            ReTrue();
        }

        private void GetMerConfigList()
        {
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("不能确定MerId!");
            }
            DataSet ds = BLL.StaticBLL.MerConfigData(MerId);
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetProByDingDan()
        {
            // string ProIds = ReStrDeCode("ProIds", "");
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            decimal MemberId = ReDecimal("MemberId", mbll.GetCurrentDecimalMemberId());
            StringBuilder s = new StringBuilder();


            s.Append(@"SELECT  *
      

         ");

            s.Append("  FROM dbo.GwcView with(nolock) WHERE MemberId =" + MemberId + "");

            s.Append(@"    
        SELECT  PeiSongTypeId ,
                COUNT(PeiSongTypeId) AS 满足配送条件,OrderNo
        FROM    dbo.PeiSongTypeVsProView with(nolock) ");
            s.Append("  WHERE   ProId IN (SELECT  ProId   FROM dbo.GwcView with(nolock) WHERE MemberId =" + MemberId + ") ");
            s.Append("  GROUP BY PeiSongTypeId,OrderNo ORDER BY  满足配送条件 DESC,OrderNo ");
            s.Append(" select PeiSongTypeId, PeiSongTypeName,ProId,OrderNo from dbo.PeiSongTypeVsProView  with(nolock)  WHERE   ProId IN (SELECT  ProId   FROM dbo.GwcView with(nolock) WHERE MemberId =" + MemberId + ") order by OrderNo desc ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable list = ds.Tables[0];

            ReDict.Add("list", JsonHelper.ToJson(list));
            DataTable PeiSongTypeList = ds.Tables[1];

            if (PeiSongTypeList.Rows.Count > 0)
            {
                DataRow drPeiSongType = PeiSongTypeList.Rows[0];
                string 满足匹配条件 = drPeiSongType["满足配送条件"].ToString();
                DataTable PsList = Common.DataSetting.TableSelect(" 满足配送条件='" + 满足匹配条件 + "'  ", PeiSongTypeList);
                DataTable pvpList = ds.Tables[2];
                ReDict.Add("PsList", JsonHelper.ToJson(PsList));
                ReDict.Add("pvpList", JsonHelper.ToJson(pvpList));
            }
            ReTrue();


        }

        private void InheritPeiSongTypeByProClass()
        {
            DataTable dtPeiSongTypeList = ReTable("dtPeiSongTypeList");  //只有PeiSongTypeId即可.
            bool InheritPeiSongType = ReBool("InheritPeiSongType", true);
            decimal ProductClassId = ReDecimal("ProductClassId", 0);

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            mbll.InheritPeiSongTypeByProClass(ProductClassId, InheritPeiSongType, dtPeiSongTypeList);



            ReTrue();
        }

        //确认配货
        void QueRenPeiHuo()
        {
            string DingDanId = ReStr("DingDanId", "");
            string WxOpenId = ReStr("WxOpenId", "");
            string PeiHuoUserId = ReStr("PeiHuoUserId", "");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.QueRenPeiHuo(DingDanId, PeiHuoUserId);
            ReDict2.Add("tipStr", "配货信息已经确认!");
            ReTrue();


        }

        private void QueRenDingDan()
        {

            string DingDanId = ReStr("DingDanId", "");
            string PaiSongUserId = ReStr("PaiSongUserId", "");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            string tipStr = mbll.QueRenDingDan(DingDanId, PaiSongUserId, "");
            ReDict2.Add("tipStr", tipStr);
            ReTrue();
        }

        private void GetThDetailList()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            string ThId = ReStr("ThId", "");
            DataSet ds = mbll.GetThDetailList(" ThId='" + ThId + "' ");
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void SaveTh()
        {
            Model.ThInfoModel model = new ThInfoModel();
            model.DingDanId = ReStr("DingDanId", "");
            model.Memo = ReStr("Memo", "");
            model.ThAmount = ReDecimal("ThAmount", 0);
            model.ThJiFen = ReDecimal("ThJiFen", 0);
            model.ThTitle = ReStr("ThTitle", "");
            model.ThTypeId = ReInt("ThTypeId", 1);
            model.ThId = ReStr("ThId", "");

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            DataTable dtDetail = ReTable("ThDetailArray");
            if (dtDetail == null)
            {
                throw new Exception("没有明细!");
            }
            if (dtDetail.Rows.Count == 0)
            {
                throw new Exception("没有明细!");
            }
            List<Model.ThDetailModel> ListDetailModel = new List<ThDetailModel>();
            foreach (DataRow drDetail in dtDetail.Rows)
            {
                Model.ThDetailModel detailModel = new ThDetailModel();
                detailModel.DingDanDetailId = decimal.Parse(drDetail["DingDanDetailId"].ToString());
                detailModel.Memo = drDetail["Memo"].ToString();
                detailModel.ThDetailTypeId = int.Parse(drDetail["ThDetailTypeId"].ToString());
                detailModel.ThId = ""; //在BLL中会赋值
                detailModel.ThQuantity = decimal.Parse(drDetail["ThQuantity"].ToString());

                ListDetailModel.Add(detailModel);
            }

            mbll.SaveTh(model, ListDetailModel);
            ReTrue();

        }

        private void GetThList()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            string DingDanId = ReStr("DingDanId", "");
            DataSet ds = mbll.GetThList(" DingDanId='" + DingDanId + "' ");
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void SaveHuiPing()
        {
            decimal PingJiaId = ReDecimal("PingJiaId", 0);
            string HuiPingContent = ReStr("HuiPingContent", "").Trim();
            if (HuiPingContent == "")
            {
                throw new Exception("回评内容不能为空!");
            }

            string HuiPingUser = ReStr("HuiPingUser", "").Trim();
            if (HuiPingUser == "")
            {
                throw new Exception("无法确定回评的用户!");
            }

            DateTime HuiPingTime = DateTime.Now;

            StringBuilder s = new StringBuilder();

            s.Append(" update dbo.PingJiaInfo set ");
            s.Append(" HuiPingTime='" + HuiPingTime.ToShortDateString() + "', ");
            s.Append(" HuiPingUser='" + HuiPingUser + "', ");
            s.Append(" HuiPingContent='" + HuiPingContent + "' ");
            s.Append(" where PingJiaId='" + PingJiaId + "' ");
            DAL.DalComm.ExReInt(s.ToString());
            ReDict2.Add("HuiPingTime", HuiPingTime.ToString("yyyy-MM-dd hh:mm:ss"));

            ReTrue();

        }

        private void GetJiFenChangeList()
        {
            BLL.MemberBLL MemBll = new BLL.MemberBLL();
            BLL.MerchantBLL MerBll = new BLL.MerchantBLL();
            decimal MemberId = ReDecimal("MemberId", MemBll.GetCurrentDecimalMemberId());
            int CurrentPage = ReInt("CurrentPage", 1);
            int PageSize = ReInt("PageSize", 15);
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" and MemberId='" + MemberId + "' ");
            s.Append(" order by CreateTime desc ");
            DataSet ds = MerBll.GetJiFenChangeList(s.ToString(), CurrentPage, PageSize, " * ");
            RePage(ds);
        }

        private void GetPingJiaList()
        {
            StringBuilder s = new StringBuilder();
            string ProId = ReStr("ProId", "");
            int CurrentPage = ReInt("CurrentPage", 1);
            decimal MemberId = ReDecimal("MemberId", 0);
            string ProCode = ReStr("ProCode", "");
            string Phone = ReStr("Phone", "");
            string DingDanId = ReStr("DingDanId", "");
            int PingJiaLv = ReInt("PingJiaLv", -1);
            DateTime CreateTime1 = ReTime("CreateTime1", DateTime.Parse("1900-01-01"));
            DateTime CreateTime2 = ReTime("CreateTime2", DateTime.Now);
            int PageSize = ReInt("PageSize", 20);
            bool Invalid = ReBool("Invalid", false);
            s.Append(" 1=1 ");


            s.Append(" and Invalid='" + Invalid + "' ");

            if (DingDanId != "")
            {
                s.Append(" and DingDanid='" + DingDanId + "' ");
            }
            if (PingJiaLv != -1)
            {
                s.Append(" and PingJiaLv=" + PingJiaLv + " ");
            }
            if (Phone != "")
            {
                s.Append(" and Phone like '%" + Phone + "%' ");
            }

            s.Append(" and CreateTime BETWEEN '" + CreateTime1 + "' AND '" + CreateTime2 + "' ");

            if (ProCode != "")
            {
                s.Append(" and ProCode='" + ProCode + "' ");

            }

            if (ProId != "")
            {
                s.Append(" and ProId='" + ProId + "'  ");
            }

            if (MemberId != 0)
            {
                s.Append(" and MemberId=" + MemberId + " ");
            }

            s.Append(" Order by CreateTime desc ");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DataSet ds = mbll.GetPingJiaPageList(s.ToString(), CurrentPage, PageSize, " * ");
            RePage(ds);
        }

        void SavePingJia()
        {

            Model.PingJiaInfoModel model = new PingJiaInfoModel();
            model.PingJiaId = 0;
            model.DingDanDetailId = ReDecimal("DingDanDetailId", 0);



            if (model.DingDanDetailId == 0)
            {
                throw new Exception("没有明确评价的订单明细!");
            }
            model.PingJiaContent = ReStr("PingJiaContent", "");
            if (model.PingJiaContent.Trim() == "")
            {
                throw new Exception("评价主体不能为空!");
            }
            model.PingJiaLv = ReInt("PingJiaLv", 5);
            model.CreateTime = DateTime.Now;




            BLL.MerchantBLL Mbll = new BLL.MerchantBLL();
            int GetJiFen = 0;
            Mbll.SavePingJia(model, ref GetJiFen);
            ReDict2.Add("GetJiFen", GetJiFen.ToString());
            ReTrue();

        }

        private void ShenHeDingDan()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            string DingDanId = ReStr("DingDanId", "");
            int ShenHeId = ReInt("ShenHeId", 0);
            string ShenHeMemo = ReStr("ShenHeMemo", "");
            ReDict2.Add("Status", mbll.ShenHeDingdan(DingDanId, ShenHeId, ShenHeMemo));
            ReTrue();

        }

        private void GetDingDanDetailList()
        {
            StringBuilder s = new StringBuilder();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            string DingDanId = ReStr("DingDanId", "");
            if (DingDanId == "")
            {
                throw new Exception("订单ID没有确定!");
            }

            DataSet ds = mbll.GetDingDanDetailList(" DingDanId='" + DingDanId + "' ");
            DataTable dt = ds.Tables[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();


        }

        private void RemoveShouCang()
        {
            decimal ShouCangId = ReDecimal("ShouCangId", 0);
            if (ShouCangId == 0)
            {
                throw new Exception("没有选择的收藏ID!");
            }
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            mbll.RemoveShouCang(ShouCangId);

            ReTrue();

        }



        private void GetMyShouCangPageList()
        {
            BLL.MemberBLL memberBll = new BLL.MemberBLL();

            decimal MemberId = ReDecimal("MemberId", memberBll.GetCurrentDecimalMemberId());

            if (MemberId == 0)
            {
                throw new Exception("MemberId不能为0!");
            }

            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " CreateTime desc ");
            DAL.ShouCangProDAL dal = new DAL.ShouCangProDAL();
            StringBuilder s = new StringBuilder();
            s.Append(" MemberId='" + MemberId + "' ");
            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);



            //dal.getp
            //DataSet ds = mbll.(" MemberId=" + MemberId + " order by  CreateTime desc ", CurrentPage, 20, "*");
            RePage2(ds);
        }

        private void SaveShouCangPro()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            BLL.MemberBLL mberBll = new BLL.MemberBLL();
            Model.ShouCangProModel model = new ShouCangProModel();
            model.ShouCangId = ReDecimal("ShouCangId", 0);
            model.ProId = ReStr("ProId", "");
            if (model.ProId == "")
            {
                throw new Exception("产品ID不能为空!");
            }
            model.OrderNo = ReInt("OrderNo", 0);
            model.Memo = ReStr("Memo", "");
            model.MemberId = ReDecimal("MemberId", mberBll.GetCurrentDecimalMemberId());


            int i = DAL.DalComm.ExInt(" SELECT COUNT(0) FROM dbo.ShouCangPro WITH(NOLOCK) WHERE MemberId='" + model.MemberId + "' AND ProId='" + model.ProId + "' ");

            if (i > 0)
            {

                DAL.DalComm.ExReInt(" DELETE FROM dbo.ShouCangPro WHERE MemberId='" + model.MemberId + "' AND ProId='" + model.ProId + "' ");
                ReDict2.Add("del", "true");
            }
            else
            {
                mbll.SaveShouCangPro(model);
                ReDict2.Add("save", "true");
            }


            ReTrue();

        }





        private void GetPinPaiInfo()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            decimal PinPaiId = ReDecimal("PinPaiId", 0);

            DataSet ds = mbll.GetPinPaiInfo(PinPaiId);
            DataTable dtPinPaiInfo = ds.Tables[0];

            string PinPaiInfo = JsonHelper.ToJsonNo1(dtPinPaiInfo);

            ReDict.Add("PinPaiInfo", PinPaiInfo);
            ReDict.Add("imgArray", JsonHelper.ToJson(ds.Tables[1]));
            ReTrue();

        }

        private void SavePinPaiInfo()
        {
            Model.PinPaiInfoModel model = new PinPaiInfoModel();

            model.PinPaiId = ReDecimal("PinPaiId", 0);
            model.InputCode = ReStr("InputCode");
            model.PinPaiName = ReStr("PinPaiName");
            model.PinPaiJianJie = ReStr("PinPaiJianJie", "");
            model.PinPaiMemo = ReStrDeCode("PinPaiMemo");
            model.PinPaiImgId = ReStr("PinPaiImgId", "");
            if (model.PinPaiImgId == "")
            {
                model.PinPaiImgId = "nopic4";
            }
            model.OrderNo = ReInt("OrderNo", 1);
            model.PinPaiClassId = ReDecimal("PinPaiClassId", 0);
            model.PinPaiTypeId = ReDecimal("PinPaiTypeId", 0);
            model.MerId = ReDecimal("MerId");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                mbll.SavePinPaiInfo(model);

                mbll.DeletePinPaiVsImg(" PinPaiId='" + model.PinPaiId + "' ");
                DataTable dtImg = ReTable("imgArray");

                if (dtImg != null)
                {
                    if (dtImg.Rows.Count > 0)
                    {



                        foreach (DataRow dr in dtImg.Rows)
                        {
                            Model.PinPaiVsImgModel ImgModel = new PinPaiVsImgModel();
                            ImgModel.ImgId = dr["ImgId"].ToString();
                            ImgModel.PinPaiId = model.PinPaiId;
                            mbll.SavePinPaiVsImg(ImgModel);
                        }
                    }



                }
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
            ReTrue();
        }

        private void GetPinPaiPageList()
        {


            int CurrentPage = ReInt("CurrentPage", 1);
            decimal MerId = ReDecimal("MerId");
            string InputStr = ReStr("InputStr", "").Trim();
            bool Invalid = ReBool("Invalid", false);
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            if (MerId != 0)
            {

                s.Append(" and MerId=" + MerId + " ");
            }

            if (InputStr != "")
            {
                s.Append(" and (inputCode like '%" + InputStr + "%' or PinPaiName like '%" + InputStr + "%') ");
            }


            s.Append(" and Invalid='" + Invalid + "' ");




            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DataSet ds = mbll.GetPinPaiPageList(s.ToString(), CurrentPage, 20, "*");
            RePage(ds);



        }

        private void InvalidProClass()
        {
            decimal ProductClassId = ReDecimal("ProductClassId", 0);
            StringBuilder s = new StringBuilder();

            s.Append(" select count(0) from dbo.Product with(nolock) where ProClassId='" + ProductClassId + "' and FlagInvalid=0 ");
            int i = DAL.DalComm.ExInt(s.ToString());
            if (i > 0)
            {
                throw new Exception("该类别下面仍有没有作废的产品,因此您不能作废这个类别!");

            }


            s.Clear();

            s.Append("select count(0) from dbo.ProductClass with(nolock) where ParentProductClassId='" + ProductClassId + "' and Invalid=0  ");
            i = DAL.DalComm.ExInt(s.ToString());
            if (i > 0)
            {

                throw new Exception("该类别下扔有没有作废的子类别,因此您不能作废这个类别!");
            }


            s.Clear();
            s.Append(" update dbo.ProductClass set Invalid=1 where ProductClassId='" + ProductClassId + "' ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();

        }
        private void GetProClassInfo()
        {

            decimal ProductClassId = ReDecimal("ProductClassId", 0);
            if (ProductClassId == 0)
            {
                throw new Exception("ProductClassId不能为0!");
            }

            BLL.MerchantBLL mbll = new BLL.MerchantBLL();

            DataSet ds = mbll.GetProClassInfo(ProductClassId);

            string ProClassInfo = JsonHelper.ToJsonNo1(ds.Tables[0]);
            string PeiSongTypeVsProductClass = JsonHelper.ToJson(ds.Tables[1]);
            string ProClassVsPinPai = JsonHelper.ToJson(ds.Tables[2]);
            //   string ParProClass = JsonHelper.ToJsonNo1(ds.Tables[3]);
            ReDict.Add("ProClassInfo", ProClassInfo);
            ReDict.Add("PeiSongTypeVsProductClass", PeiSongTypeVsProductClass); //类别关联的配送方式
            ReDict.Add("ProClassVsPinPai", ProClassVsPinPai);
            // ReDict.Add("ParProClass", ParProClass);
            ReTrue();
        }
        private void GetProAttrList()
        {
            throw new NotImplementedException();
        }

        private void SaveProArrt()
        {
            throw new NotImplementedException();
        }

        private void GetProAttr()
        {
            throw new NotImplementedException();
        }



        private void ProRecommend()
        {
            string ProId = ReStr("ProId", "");
            if (ProId.Trim() == "")
            {
                throw new Exception("没有确定ProId");
            }
            int RecommendLv = ReInt("RecommendLv", 0);
            StringBuilder s = new StringBuilder();
            s.Append(" update CORE.dbo.Product set RecommendLv='" + RecommendLv + "' where ProId='" + ProId + "' ");
            DAL.DalComm.ExReInt(s.ToString());
            ReTrue();
        }
        /// <summary>
        /// 获取一个用户的所有商家角色
        /// </summary>
        private void GetUsersMerRole()
        {
            StringBuilder s = new StringBuilder();
            string uid = ReStr("uid", "");
            s.Append(" 1=1 ");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            if (uid != "")
            {
                DataSet ds = mbll.GetUserMerRoleList(uid);
                DataTable dt = ds.Tables[0];
                string list = JsonHelper.ToJson(dt);
                ReDict.Add("list", list);
                ReTrue();
            }
            else
            {
                throw new Exception("没有确定用户!");
            }
        }


        /// <summary>
        /// 保存一个用户的商家角色关系
        /// </summary>
        private void SaveMerRoleVsUser()
        {
            Model.MerRoleVsUserModel model = new MerRoleVsUserModel();
            model.UserId = ReStr("uid", "");
            if (model.UserId == "")
            {
                throw new Exception("没有确定用户!");
            }

            model.MerRoleId = ReDecimal("MerRoleId", 0);
            if (model.MerRoleId == 0)
            {
                throw new Exception("没有确定商家!");
            }
            model.BranchId = ReStr("BranchId", "");
            if (model.BranchId.Trim() == "")
            {
                throw new Exception("没有确定分部编号?");
            }


            DAL.MerRoleVsUserDAL dal = new DAL.MerRoleVsUserDAL();
            dal.Add(model);

            ReTrue();

        }

        private void GetMerRoleUsers()
        {
            StringBuilder s = new StringBuilder();
            decimal MerId = ReDecimal("MerId", 0);

            string uid = ReStr("uid", "");

            int c = ReInt("CurrentPage", 1);
            s.Append(" 1=1 ");
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            if (MerId != 0)
            {

                s.Append(" and MerId='" + MerId + "' ");

            }





            DataSet ds = mbll.GetMerRoleUsersPageList(s.ToString(), c, 30);
            RePage(ds);
        }




        private void SaveMerRole()
        {
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();
            decimal MerRoleId = ReDecimal("MerRoleId", 0);
            Model.MerRoleModel model = new MerRoleModel();
            model.MerRoleId = ReDecimal("MerRoleId", 0);
            model.MerId = cm.CurrentMerId;
            model.MerRoleMemo = ReStr("MerRoleMemo", "");
            model.Power = ReInt("Power", 0);
            model.MerRoleName = ReStr("MerRoleName", "");

            mbll.SaveMerRole(model);
            ReTrue();
        }

        private void GetMenuVsMerRole()
        {

            decimal MerRoleId = ReDecimal("MerRoleId", 0);
            if (MerRoleId == 0)
            {


                throw new Exception("错误!没有角色定位!");

            }
            else
            {



                StringBuilder s = new StringBuilder();
                s.Append(" select * FROM CORE.dbo.MenuVsMerRole  WITH(NOLOCK) where  MerRoleId='" + MerRoleId + "' ");
                s.Append(" select * FROM CORE.dbo.AppMenuVsMerRole  WITH(NOLOCK) where  MerRoleId='" + MerRoleId + "' ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                DataTable dtapp = ds.Tables[1];

                string list = JsonHelper.ToJson(dt);
                string applist = JsonHelper.ToJson(dtapp);
                ReDict.Add("list", list);
                ReDict.Add("applist", applist);
            }



            ReTrue();
        }



        private void SaveMenuPower()
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                decimal MerRoleId = ReDecimal("MerRoleId", 0);
                if (MerRoleId == 0)
                {
                    throw new Exception("错误!没有角色定位!");
                }
                DAL.MenuVsMerRoleDAL dal = new DAL.MenuVsMerRoleDAL();

                DataTable dt = ReTable("MenuSelectedArray");

                dal.DeleteList(" MerRoleId = '" + MerRoleId + "' ");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            Model.MenuVsMerRoleModel model = new MenuVsMerRoleModel();
                            model.MenuId = dr["MenuId"].ToString();
                            model.MerRoleId = MerRoleId;
                            dal.Add(model);
                        }
                    }





                }


                //开始移动端
                DAL.AppMenuVsMerRoleDAL dalApp = new DAL.AppMenuVsMerRoleDAL();
                DataTable dtapp = ReTable("AppMenuSelectedArray");

                dalApp.DeleteList(" MerRoleId = '" + MerRoleId + "' ");

                if (dtapp != null)
                {
                    if (dtapp.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtapp.Rows)
                        {
                            Model.AppMenuVsMerRoleModel model = new AppMenuVsMerRoleModel();
                            model.AppMenuId = dr["AppMenuId"].ToString();
                            model.MerRoleId = MerRoleId;
                            dalApp.Add(model);
                        }
                    }





                }

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            ReTrue();
        }

        private void RemoveMerRoleVsUser()
        {
            decimal MerRoleId = ReDecimal("MerRoleId", 0);
            string UserId = ReStr("uid", "");

            if (MerRoleId == 0)
            {
                throw new Exception("没有要删除的角色!");
            }

            if (UserId == "")
            {

                throw new Exception("角色对应的用户不明确!");
            }

            DAL.MerRoleVsUserDAL dal = new DAL.MerRoleVsUserDAL();

            dal.DeleteList(" UserId='" + UserId + "' and MerRoleId='" + MerRoleId + "' ");

            ReTrue();

        }




        private void GetMerRoleVsUserList()
        {
            StringBuilder s = new StringBuilder();
            decimal MerId = ReDecimal("MerId", 0);
            if (MerId == 0)
            {
                throw new Exception("没有选择任何商家!");

            }
            s.Append(" select * from dbo.UserMerRoleView where MerId='" + MerId + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            string jr = JsonHelper.ToJson(ds.Tables[0]);

            ReDict.Add("list", jr);
            ReTrue();


        }

        private void SaveMerAdmin()
        {
            StringBuilder w = new StringBuilder();
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                w.Append("操作成功! 详情参见:");

                Model.MerRoleModel model = new MerRoleModel();
                model.MerId = ReDecimal("MerId");
                DAL.MerRoleDAL dal = new DAL.MerRoleDAL();
                int i = dal.ExInt(" MerRoleName='系统管理员' and MerId='" + model.MerId + "' ");
                if (i > 0)
                {

                    decimal MerRoleId = DAL.DalComm.ExInt(" select MerRoleId from  CORE.dbo.MerRole where  MerRoleName='系统管理员' and MerId='" + model.MerId + "' ");
                    //已经有了这个职位,给model赋值
                    model = dal.GetModel(MerRoleId);

                    w.Append(" 商家已有系统管理员角色! ");
                }
                else
                {

                    w.Append("已经为该商家添加'系统管理员'角色;");
                    //还没有这个角色, 添加
                    model.MerRoleName = "系统管理员";
                    model.MerRoleMemo = "该单位最高权限!";
                    model.Power = 100;
                    dal.Add(model);
                }

                string AdminUserId = ReStr("AdminUserId", "");

                if (AdminUserId == "")
                {
                    throw new Exception("管理员账号不能为空!");
                }

                DAL.UserInfoDAL udal = new DAL.UserInfoDAL();
                int j = udal.ExInt(" UserId='" + AdminUserId + "' ");
                if (j > 0)
                {
                    //已经存在了这个用户
                    w.Append(" '" + AdminUserId + "' 已经存在了,因此密码没有更改; ");
                }
                else
                {
                    //这个用户没有存在!

                    BLL.UserBLL ubll = new BLL.UserBLL();
                    Model.UserInfoModel uModel = new UserInfoModel();
                    uModel.Pwd = ReStr("Pwd", "");
                    if (uModel.Pwd == "")
                    {
                        throw new Exception("密码不能为空");
                    }


                    uModel.UserId = AdminUserId;
                    uModel.UserTitle = AdminUserId;
                    uModel.UserTypeId = 1;
                    uModel.UserLv = 0;
                    uModel.TownId = 0;
                    uModel.CreateTime = DateTime.Now;

                    ubll.Registration(uModel);

                    w.Append(" '" + AdminUserId + "' 没有存在, 新增该用户; ");
                }







                Model.MerRoleVsUserModel mRoleVsUserModel = new MerRoleVsUserModel();
                DAL.MerRoleVsUserDAL mRoleVsUserDal = new DAL.MerRoleVsUserDAL();
                mRoleVsUserModel.MerRoleId = model.MerRoleId;
                mRoleVsUserModel.UserId = AdminUserId;
                if (mRoleVsUserDal.ExInt(" MerRoleId='" + mRoleVsUserModel.MerRoleId + "' and UserId='" + mRoleVsUserModel.UserId + "' ") > 0)
                {
                    //已经存在了这条关系
                    w.Append(" '" + AdminUserId + "' 用户本来就是该商家的管理员了;` ");


                }
                else
                {

                    w.Append(" '" + AdminUserId + "' 已经是该商家的系统管理员; ");
                    //没有这条关系, 添加!
                    mRoleVsUserDal.Add(mRoleVsUserModel);
                }



                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            ReDict2.Add("ReStr", w.ToString());
            ReTrue();


        }

        private void DelAddress()
        {
            Model.AddressInfoModel model = new AddressInfoModel();
            model.AddressId = ReDecimal("AddressId", 0);
            if (model.AddressId == 0)
            {
                throw new Exception("地址ID不能为0!");
            }
            else
            {
                DAL.AddressInfoDAL dal = new DAL.AddressInfoDAL();
                dal.DeleteList(" AddressId ='" + model.AddressId + "' ");
            }
            ReTrue();
        }

        private void SaveAddress()
        {
            Model.AddressInfoModel model = new AddressInfoModel();
            model.ContactName = ReStr("ContactName");
            //  model.CreateUser = CookieSings.GetCurrentUserId();
            model.Invalid = false;
            model.Memo = ReStr("Memo");
            model.OrderNo = 0;
            model.Tel = ReStr("Tel");
            model.TownId = 1;
            DAL.AddressInfoDAL dal = new DAL.AddressInfoDAL();
            dal.Add(model);
            ReTrue();
        }

        private void QuantityChange()
        {
            StringBuilder s = new StringBuilder();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            decimal DingDanDetailId = ReDecimal("DingDanDetailId", 0);
            Model.DingDanDetailModel detailModel = dal.GetModel(DingDanDetailId);

            decimal Quantity = ReDecimal("Quantity", 0);
            if (Quantity == 0)
            {
                throw new Exception("不能改为0, 如果作废请直接删除!");
            }
            detailModel.Quantity = Quantity;

            dal.Update(detailModel);
            decimal AmountDetail = detailModel.Quantity * detailModel.Price;
            decimal Amount = mbll.ReAmount(detailModel.DingDanId);
            ReDict2.Add("AmountDetail", AmountDetail.ToString());
            ReDict2.Add("Amount", Amount.ToString());
            ReTrue();

        }

        private void doDelDingDanDetail()
        {
            StringBuilder s = new StringBuilder();
            BLL.MerchantBLL mbll = new BLL.MerchantBLL();
            DAL.DingDanDetailDAL dal = new DAL.DingDanDetailDAL();
            decimal DingDanDetailId = ReDecimal("DingDanDetailId", 0);
            s.Append(@"SELECT COUNT(0) FROM  dbo.DingDanDetail
WHERE   DingDanId = ( SELECT    DingDanId
                      FROM      dbo.DingDanDetail
                      WHERE     DingDanDetailId = '" + DingDanDetailId + "'      )");
            var i = DAL.DalComm.ExInt(s.ToString());
            string DingDanId = DAL.DalComm.ExStr(" SELECT DingDanId FROM dbo.DingDanDetail WHERE DingDanDetailId='" + DingDanDetailId + "' ");
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                #region 开始操作

                if (i <= 1)
                {
                    //把订单也作废了

                    mbll.doInvalidDingDan(DingDanId);    //作废订单
                }
                else
                {


                }

                dal.DeleteList("   DingDanDetailId='" + DingDanDetailId + "'  ");  //删除订单明细


                decimal Amount = mbll.ReAmount(DingDanId);    //刷新订单金额
                ReDict2.Add("Amount", Amount.ToString());
                #endregion


                #region 事务关闭
                transactionScope.Complete();


            }
            #endregion




            ReTrue();

        }




        private void GetDingDanPageList()
        {
            StringBuilder s = new StringBuilder();
            BLL.MemberBLL mbll = new BLL.MemberBLL();
            int CurrentPage = ReInt("CurrentPage");
            decimal CreateMember = ReDecimal("CreateMember", mbll.GetCurrentDecimalMemberId());
            string DingDanId = ReStr("DingDanId", "");
            decimal MerId = ReDecimal("MerId", 0);

            string col = ReStr("col", "*");
            string PaiSongUserId = ReStr("PaiSongUserId", "");
            string PeiHuoUserId = ReStr("PeiHuoUserId", "");
            string SerType = ReStr("SerType", "");
            string SearchType = ReStr("SearchType", "");
            string BranchId = ReStr("BranchId", "");
            DateTime CreateTime1 = ReTime("CreateTime1", DateTime.Parse("1900-01-01"));
            DateTime CreateTime2 = ReTime("CreateTime2", DateTime.Parse("3000-01-01"));
            int Status = ReInt("Status", 0);
            bool IsYiChang = ReBool("IsYiChang", false);
            s.Append(" 1=1 ");


            switch (SerType)
            {
                case "peihuo":
                    SearchType = "no_detail";   //配货和派送查询都强制不查询明细.不然太慢
                    s.Append(" and Status >=20 and Status <30 "); //配货人员可以查询出来的状态



                    break;
                case "paisong":
                    SearchType = "no_detail"; //配货和派送查询都强制不查询明细.不然太慢
                    s.Append(" and Status >=30 and Status <110 "); //派送人员可以查询出来的状态
                    break;
                default:
                    break;
            }

            if (IsYiChang)
            {
                s.Append(" and MemberLv <0 and Label <> '' ");
            }
            if (PeiHuoUserId != "")
            {
                s.Append(" and PeiHuoUserId='" + PeiHuoUserId + "' ");
            }
            if (PaiSongUserId != "")
            {
                s.Append(" and PaiSongUserId='" + PaiSongUserId + "' ");
            }
            if (BranchId != "")
            {

                s.Append(" and BranchId='" + BranchId + "' ");
            }


            if (Status != 0)
            {
                s.Append(" and Status='" + Status + "' ");

            }

            if (MerId != 0)
            {
                s.Append(" and MerchantId='" + MerId + "' ");
            }

            if (DingDanId != "")
            {

                if (Common.StringPlus.IsBaoHan(DingDanId, "."))
                {
                    DingDanId = DingDanId.Replace(".", "%");

                    s.Append(" and DingDanId like '" + DingDanId + "' ");
                }
                else
                {
                    s.Append(" and DingDanId = '" + DingDanId + "' ");
                }


            }

            s.Append(" and CreateTime BETWEEN '" + CreateTime1.ToString() + "' AND '" + CreateTime2.ToString() + "' ");

            if (CreateMember != 0)
            {
                s.Append(" and CreateMember='" + CreateMember + "' ");
            }






            int PageSize = ReInt("PageSize", 15);


            string Order = ("  CreateTime desc ");





            DAL.DingDanInfoDAL infoDal = new DAL.DingDanInfoDAL();

            DataSet ds = infoDal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);

            if (SearchType != "no_detail")  //如果需要明细的话在查询明细
            {

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("detailArray");
                if (dt.Rows.Count > 0)
                {
                    List<string> detailDingDanIds = new List<string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        detailDingDanIds.Add("'" + dr["DingDanId"].ToString() + "'");
                    }
                    string detailDingDanIdsStr = string.Join(",", detailDingDanIds);

                    DataSet dsDetail = DAL.DalComm.BackData(" select * from  dbo.DingDanDetailView where  DingDanId in (" + detailDingDanIdsStr + ") order by DingDanDetailTypeId  ");
                    DataTable dtDetail = dsDetail.Tables[0];


                    foreach (DataRow dr in dt.Rows)
                    {
                        DataTable dtMyDetail = Common.DataSetting.TableSelect(" DingDanId='" + dr["DingDanId"].ToString() + "' ", dtDetail);

                        dr["detailArray"] = JsonHelper.ToJson(dtMyDetail);
                    }

                }
            }





            RePage2(ds);
        }

        private void ChangeDingDanStause()
        {

        }

        private void AddDingDan()
        {


            Model.DingDanInfoModel model = new DingDanInfoModel();
            model.AddressId = ReDecimal("AddressId", 0);
            if (model.AddressId == 0)
            {

                throw new Exception("配送地址没有确定!");
            }


            model.CreateTime = DateTime.Now;
            model.DingDanTitle = ReStr("DingDanTitle", "");
            model.IsDone = false;
            model.UseJiFen = ReDecimal("UseJiFen", 0); //使用了多少积分
            decimal PayAmount = ReDecimal("PayAmount", 0);  //实际支付了多少金额   总金额-积分+运费
            PayAmount = Math.Round(PayAmount, 2);

            decimal 商品总价 = ReDecimal("Amount", 0);  //商品总价
            商品总价 = Math.Round(商品总价, 2);   //四舍五入
            model.Memo = ReStr("Memo", "");
            model.MerchantId = ReDecimal("MerId");
            model.Status = 0; //刚下订单, 等待商家确认,与订单日志ID相符合
            model.EnTime = DateTime.Parse("3000-01-01");
            model.PeiHuoTime = DateTime.Parse("3000-01-01");
            model.PeiSongTime1 = ReTime("PeiSongTime1");
            model.PeiSongTime2 = ReTime("PeiSongTime2");
            model.PeiSongTypeId = ReDecimal("PeiSongTypeId", 0);
            model.PayTypeId = ReInt("PayTypeId", 1);
            model.SourseTypeId = ReInt("SourseTypeId", 1);
            model.BranchId = ReStr("BranchId", "");
            int BaoZhuang = ReInt("BaoZhuang", 1); //是否需要包装(购物袋), 1为需要

            BLL.MerchantBLL MerBll = new BLL.MerchantBLL();
            //   DataSet dsPeiSongType = MerBll.dsPeiSongType(1646);




            BLL.MemberBLL mbll = new BLL.MemberBLL();
            model.CreateMember = decimal.Parse(mbll.GetCurrentMemberId());

            int dingdanNum = DAL.DalComm.ExInt("SELECT COUNT(0) FROM dbo.DingDanView WHERE CreateMember='" + model.CreateMember + "' AND CreateTime BETWEEN '" + model.CreateTime.AddSeconds(-30) + "' AND '" + model.CreateTime.AddSeconds(10) + "'");

            if (dingdanNum > 0)
            {
                throw new Exception("您的订单已经提交，请不要在2分钟内重复下单!");
            }
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                StringBuilder s = new StringBuilder();
                s.Append(" select * from dbo.AddressView with(nolock) where AddressId ='" + model.AddressId + "' ");
                s.Append(" select * from dbo.PeiSongType with(nolock) where PeiSongTypeId='" + model.PeiSongTypeId + "' ");
                s.Append(" SELECT ExtMemberId FROM dbo.MemberView WHERE MemberId='" + model.CreateMember + "' ");



                DataSet dsOther = DAL.DalComm.BackData(s.ToString());
                DataTable dtAddress = dsOther.Tables[0];
                DataTable dtPeiSongType = dsOther.Tables[1];

                DataTable dtMember = dsOther.Tables[2];

                if (dtMember.Rows.Count == 0)
                {
                    throw new Exception("没有找到编号为" + model.CreateMember + "的用户!");
                }
                DataRow drMember = dtMember.Rows[0];
                decimal ExtMemberId = decimal.Parse(drMember["ExtMemberId"].ToString());  //该用户绑定的推广人员, 为0则没有绑定
                if (dtAddress.Rows.Count == 0)
                {
                    throw new Exception("配送地址没有确定!");
                }
                if (dtPeiSongType.Rows.Count == 0)
                {
                    throw new Exception("配送方式没有确定!");
                }
                DataRow drAddress = dtAddress.Rows[0];
                DataRow drPeiSongType = dtPeiSongType.Rows[0];

                #region 检查运费

                decimal FullPrice = decimal.Parse(drPeiSongType["FullPrice"].ToString());
                decimal DefaultPrice = decimal.Parse(drPeiSongType["DefaultPrice"].ToString());

                if (商品总价 >= FullPrice)
                {
                    DefaultPrice = 0;
                }



                #endregion


                StringBuilder DingDanXml = new StringBuilder();
                DingDanXml.Append("<root>");
                DingDanXml.Append("<地址编号>" + drAddress["AddressId"] + "</地址编号>");
                DingDanXml.Append("<电话>" + drAddress["Tel"] + "</电话>");
                DingDanXml.Append("<详细地址>" + drAddress["Memo"] + "</详细地址>");
                DingDanXml.Append("<乡镇编号>" + drAddress["TownId"] + "</乡镇编号>");
                DingDanXml.Append("<乡镇>" + drAddress["TownName"] + "</乡镇>");
                DingDanXml.Append("<收货人>" + drAddress["ContactName"] + "</收货人>");
                DingDanXml.Append("<实付运费>" + DefaultPrice + "</实付运费>");
                DingDanXml.Append("<运费定额>" + drPeiSongType["DefaultPrice"] + "</运费定额>");
                DingDanXml.Append("<包邮定额>" + drPeiSongType["FullPrice"] + "</包邮定额>");
                DingDanXml.Append("<配送方式>" + drPeiSongType["PeiSongTypeName"] + "</配送方式>");

                DingDanXml.Append("</root>");
                model.DingDanAttr = DingDanXml.ToString();

                MerBll.AddDingDan(model);  //插入订单主体

                #region 开始插入订单日志

                Model.DingDanLogModel logMod = new DingDanLogModel();
                logMod.CreateTime = DateTime.Now;
                logMod.DingDanClassId = 1;
                logMod.DingDanId = model.DingDanId;
                logMod.DingDanLogId = 0;
                logMod.DingDanLogTypeId = 0; //订单尚未支付

                logMod.Memo = "订单成功下达,正等待审核!";
                MerBll.SaveDingDanLog(logMod);
                #endregion

                #region 开始插入订单明细

                decimal 验证商品总价 = 0;   //验证订单商品总金额,不包含运费
                DataTable dtPro = ReTable("dtPro");
                if (dtPro == null)
                {
                    throw new Exception("不能提交没有明细的订单");
                }
                if (dtPro.Rows.Count < 1)
                {
                    throw new Exception("不能提交没有明细的订单");
                }
                decimal GetDingDanJiFen = 0;

                #region 写入配送明细

                Model.DingDanDetailModel detailPeiSong = new DingDanDetailModel();
                detailPeiSong.Price = DefaultPrice;
                detailPeiSong.Quantity = 1;
                detailPeiSong.ProId = "";
                detailPeiSong.Memo = "配送费用";
                detailPeiSong.DingDanDetailTypeId = 10;//物流明细
                //  detailPeiSong.YiXiang = 0;
                detailPeiSong.DingDanId = model.DingDanId;

                MerBll.AddDingDanDetail(detailPeiSong);
                #endregion

                #region 写入包装明细


                if (BaoZhuang == 1)
                {

                    Model.DingDanDetailModel detailBaoZhuang = new DingDanDetailModel();
                    detailBaoZhuang.Price = 0;  //包装明细不要钱的
                    detailBaoZhuang.Quantity = 1;
                    detailBaoZhuang.ProId = "";
                    detailBaoZhuang.Memo = "包装购物袋";
                    detailBaoZhuang.DingDanDetailTypeId = 20;//包装明细
                    //  detailPeiSong.YiXiang = 0;
                    detailBaoZhuang.DingDanId = model.DingDanId;

                    MerBll.AddDingDanDetail(detailBaoZhuang);

                }


                #endregion
                #region 写入补差价商品
                //Model.DingDanDetailModel detailChaJia = new DingDanDetailModel();
                //detailChaJia.Price = 0;  //包装明细不要钱的
                //detailChaJia.Quantity = 1;
                //detailChaJia.ProId = BLL.StaticBLL.MerOneConfig(model.MerchantId, "ChaJiaPro", "");
                //detailChaJia.Memo = "补差价";

                //detailChaJia.DingDanDetailTypeId = 1;//包装明细
                //                                     //  detailPeiSong.YiXiang = 0;
                //detailChaJia.DingDanId = model.DingDanId;



                //StringBuilder xmlChaJiaPro = new StringBuilder();

                //xmlChaJiaPro.Append("<root>");
                //xmlChaJiaPro.Append("<产品名称>补差价商品</产品名称>");
                //xmlChaJiaPro.Append("<规格>无</规格>");
                //xmlChaJiaPro.Append("<产品编号></产品编号>");
                //xmlChaJiaPro.Append("<计量单位>个</计量单位>");
                //xmlChaJiaPro.Append("<获得积分>0</获得积分>");
                //xmlChaJiaPro.Append("<产品特性>1</产品特性>");
                //xmlChaJiaPro.Append("<产品单价>0</产品单价>");
                //xmlChaJiaPro.Append("<下单质量>1</下单质量>");
                //xmlChaJiaPro.Append("<下单递增质量>1</下单递增质量>");
                //xmlChaJiaPro.Append("<下单质量倍数>1</下单质量倍数>");
                //xmlChaJiaPro.Append("<下单意向>0</下单意向>");
                //xmlChaJiaPro.Append("</root>");
                //detailChaJia.DingDanDetailAttr = xmlChaJiaPro.ToString();
                //    MerBll.AddDingDanDetail(detailChaJia);
                #endregion

                for (int i = 0; i < dtPro.Rows.Count; i++)
                {
                    DataRow drPro = dtPro.Rows[i];
                    Model.DingDanDetailModel detailModel = new DingDanDetailModel();
                    detailModel.DingDanId = model.DingDanId;
                    detailModel.Memo = drPro["Memo"].ToString();
                    detailModel.ProTeXing = int.Parse(drPro["ProTeXing"].ToString());

                    string ProName = drPro["ProName"].ToString();
                    detailModel.Price = decimal.Parse(drPro["Price"].ToString());
                    detailModel.Zlbs = decimal.Parse(drPro["Zlbs"].ToString());
                    if (detailModel.ProTeXing == 2)
                    {  //如果是生鲜属性, 那么单价就是金额
                        detailModel.Price = decimal.Parse(drPro["Amount"].ToString());
                        if (detailModel.Zlbs <= 0)
                        {
                            throw new Exception("生鲜数量不能为0, 错误商品[" + ProName + "],请返回修改数量, 如果确定数量没错, 请将商品清单截图并联系管理员!");
                        }
                    }

                    detailModel.ProId = drPro["ProId"].ToString();
                    detailModel.Quantity = decimal.Parse(drPro["Quantity"].ToString());
                    detailModel.Hope = decimal.Parse(drPro["hope"].ToString());


                    detailModel.MinZl = decimal.Parse(drPro["MinZl"].ToString());

                    detailModel.DingDanDetailTypeId = 1;



                    DataTable dtDataPro = DAL.DalComm.BackData(" select  ProName,GetJiFenNum, Spec,Units, ProNum,ZgProNum,Status,FlagInvalid,ProCode,ProId,ProTeXing from dbo.ProVsBranchView where ProId='" + detailModel.ProId + "' and BranchId='" + model.BranchId + "' ").Tables[0];
                    if (dtDataPro.Rows.Count == 0)
                    {
                        throw new Exception("非常抱歉!产品" + ProName + "在数据库中并不存在!");
                    }
                    DataRow drDataPro = dtDataPro.Rows[0];


                    decimal 明细获得积分 = 0;
                    detailModel.GetJifenNum = decimal.Parse(drDataPro["GetJiFenNum"].ToString());  //存储下单时的积分比率

                    明细获得积分 = Math.Round(decimal.Parse(drDataPro["GetJiFenNum"].ToString()) * detailModel.Price * detailModel.Quantity * 100, 0); //积分比率乘以单价*数量




                    GetDingDanJiFen = GetDingDanJiFen + 明细获得积分;   //订单总积分加上此订单明细积分

                    ProName = drDataPro["ProName"].ToString();
                    decimal ZgProNum = decimal.Parse(drDataPro["ZgProNum"].ToString());  //暂估库存
                    if (ZgProNum < detailModel.Quantity)
                    {
                        throw new Exception("非常抱歉! 产品" + ProName + "库存不足!");
                    }
                    int Status = int.Parse(drDataPro["Status"].ToString());
                    if (Status < 0)
                    {
                        throw new Exception("非常抱歉! 产品" + ProName + "已经下架!");
                    }
                    bool Invalid = bool.Parse(drDataPro["FlagInvalid"].ToString());
                    if (Invalid)
                    {
                        throw new Exception("非常抱歉! 产品" + ProName + "已经作废!");
                    }

                    StringBuilder xml = new StringBuilder();

                    if (detailModel.ProTeXing == 1)
                    {
                        验证商品总价 = detailModel.Price * detailModel.Quantity + 验证商品总价;    //数量*单价 加入总金额, 以验证备用
                    }
                    else if (detailModel.ProTeXing == 2)
                    {
                        验证商品总价 = detailModel.Price + 验证商品总价;     // 生鲜的单价在订单中就是金额.  所以不乘以质量倍数, 直接相加即可
                    }




                    xml.Append("<root>");
                    xml.Append("<产品名称>" + ProName.ToString().Trim() + "</产品名称>");
                    xml.Append("<规格>" + drDataPro["Spec"].ToString().Trim() + "</规格>");
                    xml.Append("<产品编号>" + drDataPro["ProCode"].ToString().Trim() + "</产品编号>");
                    xml.Append("<计量单位>" + drDataPro["Units"].ToString().Trim() + "</计量单位>");
                    xml.Append("<获得积分>" + 明细获得积分 + "</获得积分>");
                    xml.Append("<产品特性>" + detailModel.ProTeXing + "</产品特性>");
                    xml.Append("<产品单价>" + detailModel.Price + "</产品单价>");
                    xml.Append("<下单质量>" + detailModel.MinZl * detailModel.Zlbs + "</下单质量>");
                    xml.Append("<下单递增质量>" + detailModel.MinZl + "</下单递增质量>");
                    xml.Append("<下单质量倍数>" + detailModel.Zlbs + "</下单质量倍数>");
                    xml.Append("<下单意向>" + detailModel.Hope + "</下单意向>");
                    xml.Append("</root>");

                    //重要! 产品单价如果是生鲜产品,数量绝对为一,单价改成金额, 因为生鲜产品的特性

                    if (detailModel.ProTeXing == 2)
                    {
                        detailModel.Quantity = 1;//修改数量
                        //  detailModel.Price = detailModel.Price * detailModel.Zlbs; //生鲜订单本来金额就是单价, 单价就是金额, 因此无需修改单价
                    }

                    detailModel.DingDanDetailAttr = xml.ToString();
                    MerBll.AddDingDanDetail(detailModel);


                }
                #endregion

                #region 用户积分操作


                #region 增加用户积分

                Model.JiFenChangeModel jfChangeMod = new JiFenChangeModel();


                #endregion

                #region 使用用户积分

                #endregion

                #endregion





                #region 检查订单是否合法

                if (验证商品总价 != 商品总价)
                {
                    throw new Exception("不相等的订单总金额!(明细加和" + 验证商品总价 + ",参数值" + 商品总价 + ")");
                }
                if (PayAmount != (商品总价 + DefaultPrice - (model.UseJiFen * 1 / 100)))
                {
                    throw new Exception("支付金额不符!(支付金额" + PayAmount + ",参数值" + (商品总价 + DefaultPrice - (model.UseJiFen * 1 / 100)) + ")");

                }

                #endregion

                ReDict2.Add("Amount", 商品总价.ToString());
                ReDict2.Add("DingDanId", model.DingDanId);

                #region 清空购物车的本次提交产品
                List<string> ProIds = new List<string>();
                foreach (DataRow drPro in dtPro.Rows)
                {
                    //清空购物车
                    ProIds.Add(drPro["ProId"].ToString());
                }
                MerBll.ClearGwc(model.CreateMember, ProIds);
                #endregion





                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            //  MerBll.NewDingDanTiXing(model.MerchantId, model.BranchId); //订单提醒发送


            ReTrue();
        }

        private void CheckPayment()
        {

            string DingDanId = ReStr("DingDanId", "");


            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            int Num = bll.CheckPayment(DingDanId);
            ReDict2.Add("Num", Num.ToString());
            ReTrue();
        }

        private void SendPayment()
        {



            int PayTypeId = ReInt("PayTypeId", 0);
            string ReKey = ReStr("ReKey", "");

            if (PayTypeId == 0)
            {
                throw new Exception("支付方式必须选择!");
            }
            if (ReKey == "")
            {
                throw new Exception("订单ID不能为空!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" SELECT Phone, PayAmount FROM dbo.DingDanView WITH(NOLOCK) WHERE DingDanId='" + ReKey + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("没有找到ID号为" + ReKey + "的订单!");
            }
            DataRow dr = dt.Rows[0];

            decimal PayAmount = decimal.Parse(dr["PayAmount"].ToString());
            PayAmount = PayAmount + PayAmount * 1 / 100;//线上支付百分之一的手续费


            BeeCloud.BeeCloud.registerApp("5fc3d0d0-f31d-4c0f-bb47-9de51c486115", "0158d251-837f-426c-9178-b444effb82f0", "c757804c-7914-434a-876d-8b4fdda84c6c", "56108d18-f2ec-4719-bbd5-43052d986035");
            BeeCloud.BeeCloud.setTestMode(false);
            int totalFee = Convert.ToInt32(Math.Round(PayAmount * 100, 0, MidpointRounding.AwayFromZero));
            switch (PayTypeId)
            {
                case 1: //预约付款
                    break;

                case 10: //支付宝网页端付款


                    BCBill bill = new BCBill(BCPay.PayChannel.ALI_WEB.ToString(), totalFee, ReKey, "商城订单[" + ReKey + "]");
                    bill.returnUrl = "http://www.ruodianjs.com/Pay/PayDone.aspx?DingDanId=" + ReKey + "";  //这里必须是http开头, 因为是支付宝页面中跳转, 他不认相对路径

                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    ReDict2.Add("url", resultBill.url);
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.url + "</span><br/>");


                    break;
                case 20://微信扫码付款

                    ReDict2.Add("url", "/Pay/WxPay.aspx?DingDanId=" + ReKey + "");

                    break;




                default:
                    break;
            }
            ReDict2.Add("PayTypeId", PayTypeId.ToString());
            ReTrue();
        }


        private void GetAuthorPageList()
        {
            StringBuilder s = new StringBuilder();
            string inputStr = ReStr("inputStr");
            decimal MerchantId = ReDecimal("MerchantId", 0);
            bool Invalid = ReBool("Invalid", false);
            int c = ReInt("CurrentPage", 1);
            s.Append(" 1=1 ");
            s.Append(" and MerchantId='" + MerchantId + "' ");

            if (inputStr.Trim() != "")
            {
                s.Append(" and AuthorName like '%" + inputStr + "%'   ");
            }
            s.Append(" and Invalid ='" + Invalid + "' ");
            s.Append(" order by createTime desc ");
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            DataSet ds = bll.GetAuthorPageList(s.ToString(), c);
            RePage(ds);
        }

        private void SaveAuthorInfo()
        {

            Model.AuthorInfoModel model = new AuthorInfoModel();

            model.AuthorId = ReStr("AuthorId");
            model.AuthorName = ReStr("AuthorName");
            model.InputCode = PinYin.GetFirstLetter(model.AuthorName);
            model.AuthorMemo = ReStr("AuthorMemo");
            model.MerchantId = ReDecimal("MerchantId");
            model.CreateTime = DateTime.Now;
            model.PicImgId = ReStr("PicImgId");

            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            bll.SaveAuthorInfo(model);
            ReTrue();
        }

        private void GetCldProTypeList()
        {
            decimal ProductTypeId = ReDecimal("ProductTypeId");
            StringBuilder s = new StringBuilder();
            s.Append(" select * from CORE.dbo.ProductType where ParentProductTypeId='" + ProductTypeId + "' order by OrderNo desc ");
            DataTable dt = DAL.DalComm.BackData(s.ToString()).Tables[0];
            ReDict.Add("TypeList", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void InvalidPro()
        {
            string ProIds = ReStr("ProIds", "");
            bool Invalid = ReBool("Invalid", true);


            if (ProIds.Trim() == "")
            {
                throw new Exception("您没有选择任何产品!");
            }
            else
            {
                string[] ProIdArray = ProIds.Split(',');
                StringBuilder s = new StringBuilder();
                s.Append("  update CORE.dbo.Product set FlagInvalid='" + Invalid + "' Where ProId in ('0' ");
                foreach (string ProId in ProIdArray)
                {
                    s.Append(", '" + ProId + "' ");
                }
                s.Append(" ) ");

                DAL.DalComm.ExReInt(s.ToString());
                ReTrue();
            }
        }

        private void InvalidMer()
        {

            decimal MerId = ReDecimal("MerId");
            DAL.DalComm.ExReInt(" update dbo.Merchant set FlagInvalid='True' where MerchantId='" + MerId + "' ");
            ReTrue();
        }

        private void StatusApplyForBindMer()
        {
            StringBuilder s = new StringBuilder();
            decimal AutoId = ReDecimal("AutoId");
            int Status = ReInt("Status");
            string yuanyin = ReStr("yuanyin");
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion

                DAL.DalComm.ExStr("update dbo.ApplyForBindMer set Status='" + Status + "'  where AutoId='" + AutoId + "'");
                Model.RemindModel reModel = new RemindModel();
                reModel.CreateTime = DateTime.Now;
                reModel.MerLook = false;
                reModel.UserLook = false;
                reModel.Url = "";

                reModel.RemindTypeId = "绑定商家驳回";
                reModel.ReUserId = DAL.DalComm.ExStr(" select top 1 BindUserId from dbo.ApplyForBindMer where AutoId='" + AutoId + "' ");
                reModel.ReKey = AutoId.ToString();
                if (Status < 0)
                {//驳回
                    reModel.RemindTitle = "对不起,我们无法为您绑定这个商家,原因是:" + yuanyin + "";
                }
                else
                {//通过



                    Model.MerchantVsUserModel MvuModel = new MerchantVsUserModel();
                    MvuModel.MerchantId = DAL.DalComm.ExInt(" select top 1 BindMerchantId from dbo.ApplyForBindMer where AutoId='" + AutoId + "' ");
                    MvuModel.UserId = reModel.ReUserId;
                    MvuModel.Power = 100;
                    MvuModel.Memo = "管理员:" + Common.CookieSings.GetCurrentUserId() + "在" + DateTime.Now.ToString("yy年MM月dd日") + "通过;";
                    reModel.RemindTitle = "恭喜您,已经完成商家绑定! 您现在可以在\"个人中心-绑定商家\"中看到您绑定的商家了!";
                    DAL.MerchantVsUserDAL mvudal = new DAL.MerchantVsUserDAL();
                    if (mvudal.ExInt(" MerchantId ='" + MvuModel.MerchantId + "' and UserId='" + MvuModel.UserId + "' ") > 0)
                    {
                        throw new Exception("已经存在这条绑定信息!");
                    }
                    mvudal.Add(MvuModel);

                }
                BLL.CommBLL.AddNewRemind(reModel);
                #region  事务关闭

                transactionScope.Complete();


            }
            #endregion



            ReTrue();
        }

        private void GetApplyForMerPageList()
        {
            StringBuilder s = new StringBuilder();
            string inputStr = ReStr("inputStr");

            int Status = ReInt("Status");
            s.Append(" 1=1 ");
            if (inputStr != "")
            {

                s.Append(" and MerchantName like '%" + inputStr + "%' or UserId like '%" + inputStr + "%' ");

            }
            if (Status != 0)
            {
                s.Append(" and Status='" + Status + "' ");
            }

            s.Append(" order by createTime desc ");
            DAL.ApplyForBindMerDAL dal = new DAL.ApplyForBindMerDAL();
            int CurrentPage = ReInt("CurrentPage");
            DataSet ds = dal.GetPageList(s.ToString(), CurrentPage, 20);
            RePage(ds);
        }

        private void SearchMerList()   //取得商家
        {
            string MerchantTypeIds = ReStr("MerchantTypeIds");
            int CurrentPage = ReInt("CurrentPage");
            string InputStr = ReStr("InputStr", "");
            DAL.MerchantDAL dal = new DAL.MerchantDAL();
            DataSet ds = dal.GetMapMerPageList(MerchantTypeIds, InputStr, CurrentPage);
            RePage(ds);
        }

        private void SearchMerProList()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            string InputStr = ReStr("InputStr", "");
            string ProClassId = ReStr("ProClassId", "");
            int CurrentPage = ReInt("CurrentPage", 1);
            decimal MerId = ReDecimal("MerId");
            bool FlagInvalid = ReBool("Invalid", false);
            string AuthorId = ReStr("AuthorId", "");
            int PageSize = ReInt("PageSize", 20);
            string AuthorName = ReStr("AuthorName", "");
            int MoreRecommendLv = ReInt("MoreRecommendLv", 0);
            decimal PinPaiId = ReDecimal("PinPaiId", 0);
            string ProCode = ReStr("ProCode", "");
            string ProIds = ReStrDeCode("ProIds", "");

            if (MerId < 1)
            {
                throw new Exception("商家编号有异常!");
            }


            StringBuilder s = new StringBuilder();
            s.Append("1=1");

            if (MoreRecommendLv != 0)
            {
                s.Append(" and RecommendLv >= '" + MoreRecommendLv + "'");
            }
            if (AuthorName != "")
            {
                s.Append(" and AuthorName='" + AuthorName + "' ");
            }
            if (AuthorId != "")
            {
                s.Append(" and AuthorId='" + AuthorId + "' ");
            }

            s.Append(" and FlagInvalid='" + FlagInvalid + "' ");


            if (InputStr != "")
            {
                s.Append("  and (ProName like '%" + InputStr + "%' or AuthorName='" + InputStr + "' )  ");

            }
            if (ProClassId == "" || ProClassId == "0")
            {
                //用户没有指定类别查询
            }
            else
            {
                s.Append(" and ProClassId in (" + ProClassId + ") ");
            }
            s.Append(" and MerchantId='" + MerId + "' ");

            if (PinPaiId != 0)
            {

                s.Append(" and PinPaiId=" + PinPaiId + " ");
            }
            if (ProIds != null)
            {
                if (ProIds.Trim() != "")
                {
                    s.Append(" and ProId in (" + ProIds + ") ");
                }

            }



            if (ProCode.Trim() != "")
            {
                s.Append(" and ProCode='" + ProCode + "' ");
            }
            string Order = " RecommendLv desc, CreateTime desc ";

            DataSet ds = bll.GetProPageList(Order, s.ToString(), CurrentPage, 20, " AuthorName, CreateTime,CreateUser,ProClassId,ProductClassName,Units,RePrice,RePrice2,RePrice3,Spec,Status, ProName,ProTitle,ProductImgUrl,ProductImgId,ProId,MerchantName, RecommendLv,ProCode,GetJiFenNum,ProNum,PinPaiId,ProTeXing,MinZl,Zl ");
            RePage2(ds);
        }

        private void SaveProClassInfo()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            Model.ProductClassModel model = new ProductClassModel();

            model.ProductClassId = ReDecimal("ProductClassId", 0);
            model.ParentProductClassId = ReDecimal("ParentProductClassId", 0);


            model.ProductClassName = ReStr("ProductClassName");
            model.InheritPeiSongType = ReBool("InheritPeiSongType", true); //默认继承
            if (model.ProductClassName.Trim() == "")
            {
                throw new Exception("类别名称不能为空!");
            }

            model.OrderNo = ReInt("OrderNo", 1);
            model.Memo = ReStr("Memo", "");
            model.MerchantId = ReDecimal("MerId");
            model.ProductClassImgId = ReStr("ProductClassImgId", "");
            model.Invalid = ReBool("Invalid", false);
            model.AttrSelXml = ReStr("AttrSelXml", "<root></root>");
            model.ProClassColor = ReStr("ProClassColor", "");
            model.ProClassKeyWord = ReStr("ProClassKeyWord", "");
            model.InheritProTeXing = ReBool("InheritProTeXing", true);
            model.ProTeXing = ReInt("ProTeXing", 1);
            model.ImgId = ReStr("ImgId", "");
            if (model.ProTeXing <= 0)
            {
                throw new Exception("ProTeXing不能为" + model.ProTeXing + ", 如有BUG请联系管理员.");
            }

            model.InheritJiFenNum = ReBool("InheritJiFenNum", true);
            model.GetJiFenNum = ReDecimal("GetJiFenNum", 0);

            model.InheritPeiSongType = ReBool("InheritPeiSongType", true);



            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                DataTable dtVsPinPai = ReTable("PinPaiJsonArray");

                bll.SaveProClassInfo(model);
                if (dtVsPinPai != null)
                {
                    if (dtVsPinPai.Rows.Count > 0)
                    {
                        DAL.ProClassVsPinPaiDAL vsPinPaiDal = new DAL.ProClassVsPinPaiDAL();
                        vsPinPaiDal.DeleteList(" ProClassId='" + model.ProductClassId + "' ");
                        foreach (DataRow dr in dtVsPinPai.Rows)
                        {

                            Model.ProClassVsPinPaiModel vsPinPaiModel = new ProClassVsPinPaiModel();
                            vsPinPaiModel.ProClassId = model.ProductClassId;
                            vsPinPaiModel.PinPaiId = decimal.Parse(dr["PinPaiId"].ToString());
                            vsPinPaiModel.VsType = "";
                            vsPinPaiModel.OrderNo = int.Parse(dr["OrderNo"].ToString());
                            vsPinPaiDal.Add(vsPinPaiModel);
                        }
                    }

                }


                ReDict.Add("ProductClassId", model.ProductClassId);
                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion

            BLL.StaticBLL.ClearCache("ProClass1");
            ReTrue();

        }

        private void SaveMyMerchant()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            BLL.UserBLL ubll = new BLL.UserBLL();
            Model.MerchantModel model = new Model.MerchantModel();

            model.MerchantId = ReDecimal("MerchantId", 0);
            model.MerchantName = ReStr("MerchantName");
            #region 如果是新增判断是否存在名称
            if (model.MerchantId == 0)
            {
                bool b = DAL.DalComm.ExBool(" select count(0) from dbo.Merchant WITH(NOLOCK) where MerchantName='" + model.MerchantName + "' ");
                if (b)
                {

                    throw new Exception("对不起,名称为:'" + model.MerchantName + "'的商家已经存在! 不能重复添加, 如果需要绑定此商家, 请在'麻城地图'中查找并绑定,如有问题请联系站长qq:16248777");
                }
            }
            #endregion

            model.InputCode = PinYin.GetFirstLetter(model.MerchantName);

            model.MerchantMemo = ReStr("MerchantMemo","");
            model.MerchantContent = ReStrDeCode("MerchantContent","");
            model.MerchantClassId = ReDecimal("MerchantClassId",0);
            model.MerchantTypeId = ReDecimal("MerchantTypeId",0);
            model.Recommendlv = ReInt("Recommendlv", 0);
            model.HotLv = ReInt("HotLv", 0);
            model.Lng = ReDecimal("Lng");
            model.Lat = ReDecimal("Lat");
            model.WebSite = ReStr("WebSite");
            model.Logo = ReStr("Logo", "");
            model.TownId = ReDecimal("TownId",0);
            model.FlagInvalid = ReBool("FlagInvalid", false);
            model.Tell = ReStr("Tell");
            model.qq = ReStr("qq");
            model.Email = ReStr("Email");
            model.Address = ReStr("Address");
            model.Phone = ReStr("Phone");
            model.Name = ReStr("Name","");
            model.MerchantTypeTarget = ReStr("MerchantTypeTarget","");
            model.CreateUser = ubll.CurrentUserId();

            bool Add = false;
            if (model.MerchantId == 0)
            {
                Add = true;
            }
            DataTable dt = ReTable("VsMerTypeList");
            DataTable dtImg = ReTable("imgArray");
            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                #endregion
                bll.SaveMer(model);  //保存商家主体内容
                if (!Add)
                {
                    //修改
                    bll.DeleteMerchantVsMerchantType(" MerchantId='" + model.MerchantId + "' "); //删除之前的商家和行业的关联关系
                    bll.DeletemerchantVsImg(" MerchantId='" + model.MerchantId + "'  ");  //删除之前的商家和图片的关联关系
                    if (dtImg != null)
                    {

                        foreach (DataRow dr in dtImg.Rows)
                        {
                            MerchantVsImgModel mviModel = new MerchantVsImgModel();
                            mviModel.MerchantId = model.MerchantId;
                            mviModel.ImgId = dr["ImgId"].ToString();
                            mviModel.vsType = "MerContent";
                            bll.SaveMerchantVsImg(mviModel);
                        }
                    }
                }
                else
                { //新增

                    #region 添加默认新闻类别
                    Model.ArticleClassModel ArtClassModel = new ArticleClassModel();
                    ArtClassModel.ArticleClassImgId = "";
                    ArtClassModel.ArticleClassMemo = "";
                    ArtClassModel.ArticleClassName = "最新动态";
                    ArtClassModel.MerId = model.MerchantId;
                    ArtClassModel.Invalid = false;
                    ArtClassModel.ParentArticleClassId = 0;
                    DAL.ArticleClassDAL ArtClassdal = new DAL.ArticleClassDAL();
                    ArtClassdal.Add(ArtClassModel);  //添加默认新闻类别


                    #endregion


                    #region 添加默认产品类别

                    Model.ProductClassModel ProClassModel = new Model.ProductClassModel();
                    DAL.ProductClassDAL ProClassDal = new DAL.ProductClassDAL();
                    ProClassModel.Invalid = false;
                    ProClassModel.Memo = "";
                    ProClassModel.MerchantId = model.MerchantId;
                    ProClassModel.OrderNo = 0;
                    ProClassModel.ParentProductClassId = 0;
                    ProClassModel.ProductClassImgId = "";
                    ProClassModel.ProductClassName = "产品展示";
                    ProClassDal.Add(ProClassModel);
                    #endregion



                    if (ubll.IsAdministrator())
                    {
                        //如果是超级管理员,添加无需绑定
                    }
                    else
                    {


                        model.FlagInvalid = true;
                        Model.MerchantVsUserModel MvuModel = new MerchantVsUserModel();  //添加关联关系
                        MvuModel.MerchantId = model.MerchantId;
                        MvuModel.UserId = model.CreateUser;
                        DAL.MerchantVsUserDAL dal = new DAL.MerchantVsUserDAL();
                        //  dal.DeleteList(" MerchantId='"+model.MerchantId+"' ");
                        dal.Add(MvuModel);
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    Model.MerchantVsMerchantTypeModel modelVsType = new Model.MerchantVsMerchantTypeModel();
                    modelVsType.MerchantId = model.MerchantId;
                    modelVsType.MerchantTypeId = decimal.Parse(dr["MerchantTypeId"].ToString());
                    modelVsType.vsType = "my";
                    bll.SaveMerchantVsMerchantType(modelVsType);  //保存商家和行业的关联关系
                }

                #region 事务关闭

                transactionScope.Complete();


            }
            #endregion
            //  Common.ImgHelper.CreateMerLuPai1(model.MerchantId, model.MerchantName);
            ReDict2.Add("MerId", model.MerchantId.ToString());
            ReTrue();


        }

        private void GetMerType()
        {

            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            DataSet ds = bll.GetMerType(ReDecimal("PMerTypeId"));
            ReDict.Add("MerTypeList", JsonHelper.ToJson(ds));
            ReTrue();

        }

        private void GetArticleClassInfo()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            DataSet ds = bll.GetArticleClass(" ArticleClassId='" + ReInt("ArticleClassId") + "'  ");
            string j = JsonHelper.ToJsonNo1(ds.Tables[0]);
            ReDict.Add("ArticleClassInfo", j);
            ReTrue();
        }

        private void InvalidAtricleClass()   //作废一个商家新闻类别
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            bll.InvalidAtricleClass(ReInt("ArticleClassId"));
            ReTrue();
        }



        private void GetArticleClass()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            DataTable dt = bll.GetArticleClass(" MerId='" + ReStr("MerId") + "' and Invalid='" + ReBool("Invalid", false).ToString() + "' ").Tables[0];

            DataTable Pdt = Common.DataSetting.TableSelect(" ParentArticleClassId=0 ", dt);
            Pdt.Columns.Add("ChlidArt");
            foreach (DataRow pdr in Pdt.Rows)
            {
                DataTable Cdt = Common.DataSetting.TableSelect("  ParentArticleClassId = " + pdr["ArticleClassId"] + "  ", dt);
                pdr["ChlidArt"] = JsonHelper.ToJson(Cdt);
            }

            string j = JsonHelper.ToJson(Pdt);

            ReDict.Add("MyMerAtrClass", j);
            ReTrue();

        }

        private void SaveArticleClass()
        {

            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            Model.ArticleClassModel model = new Model.ArticleClassModel();
            model.ArticleClassId = ReInt("ArticleClassId", 0);
            model.ArticleClassName = ReStr("ArticleClassName");
            model.MerId = ReDecimal("MerId");
            model.OrderNo = ReInt("OrderNo", 1);
            model.ParentArticleClassId = ReInt("ParentArticleClassId", 0);
            model.ArticleClassMemo = ReStr("ArticleClassMemo", "");
            model.ArticleClassImgId = ReStr("ArticleClassImgId", "");
            model.Invalid = ReBool("Invalid", false);
            bll.SaveArticleClass(model);
            ReTrue();

        }

        private void ApplyForBindMer()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            Model.ApplyForBindMerModel model = new Model.ApplyForBindMerModel();




            model.AutoId = ReInt("AutoId");
            model.BindUserId = ReStr("BindUserId");
            model.BindMerchantId = ReInt("BindMerchantId");
            model.Status = ReInt("Status");
            model.BusinessNo = ReStr("BusinessNo");
            model.LegalName = ReStr("LegalName");
            model.OrganizationNo = ReStr("OrganizationNo");
            model.Tell = ReStr("Tell");
            model.qq = ReStr("qq");
            model.BusinessImgId = ReStr("BusinessImgId");
            model.email = ReStr("email");
            model.OrganizationName = ReStr("OrganizationName");
            model.Memo = ReStr("Memo");


            bool b = DAL.DalComm.ExBool(" select count(0) from dbo.ApplyForBindMer where BindUserId='" + Common.CookieSings.GetCurrentUserId() + "' and BindMerchantId='" + model.BindMerchantId + "'  ");
            if (b)
            {//已经存在
                throw new Exception("您已经提交了和此店铺的绑定请求, 我们正在处理, 在这期间请不要重复提交, 如有疑问请联系站长:qq:16248777 ");
            }
            else
            { //不存在

                bll.SaveApplyForBindMer(model);
            }

            ReTrue();


        }

        private void GetMerVsComment()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            string MerchantId = ReStr("MerchantId");
            int CurrentPage = ReInt("CurrentPage");

            DataSet ds = bll.GetMerVsComment(" MerchantId='" + MerchantId + "' order by CreateTime desc ", CurrentPage);
            RePage(ds);
        }




        private void GetMerList()
        {
            try
            {
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                string MerName = ReStr("MerName");
                int CurrentPage = ReInt("CurrentPage");
                DataSet ds = bll.GetMerPageList(" MerchantName Like '%" + MerName + "%' order by MerchantId desc ", CurrentPage);
                RePage(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetProTypePostSerData()
        {
            try
            {
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                BLL.TownBLL bllTown = new BLL.TownBLL();
                decimal ProTypeId = ReDecimal("ProTypeId");

                DataTable dt = bll.GetProType(" ParentProductTypeId='" + ProTypeId + "' ");

                string ProType = JsonHelper.ToJson(dt);


                dt = bllTown.GetTownList();
                string Town = JsonHelper.ToJson(dt);

                ReDict.Add("Town", Town);
                ReDict.Add("Type", ProType);
                ReTrue();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetProComment()
        {
            try
            {
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                string ProId = ReStr("ProId");
                DataTable dt = bll.GetProComment(ProId, 3);
                string json = JsonHelper.ToJson(dt);
                ReDict.Add("ProComm", json);
                ReTrue();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void SaveProComment()
        {
            try
            {


                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                Model.CommentModel CommentModel = new Model.CommentModel();
                Model.ProVsCommentModel VsCommentModel = new Model.ProVsCommentModel();
                CommentModel.CommentContent = ReStr("CommentContent");
                CommentModel.CommentTitle = ReStr("CommentTitle");
                CommentModel.CommentType = "产品点评";
                CommentModel.CreateTime = DateTime.Now;
                CommentModel.CreateUser = ReStr("CreateUser");
                CommentModel.FlagInvalid = false;
                CommentModel.ReceiveUser = "";


                VsCommentModel.CommentId = CommentModel.CommentId;
                VsCommentModel.ProId = ReStr("ProId");
                VsCommentModel.VsType = "用户点评";

                bll.AddProComment(VsCommentModel, CommentModel);  //添加
                ReTrue();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetMerProList()
        {

            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            int CurrentPage = ReInt("CurrentPage");
            decimal MerchantId = ReDecimal("MerchantId");

            DataSet ds = bll.GetProPageList(" MerchantId='" + MerchantId + "'  and FlagInvalid=0 order by createTime desc ", CurrentPage, 30);



            RePage(ds);
        }

        private void ChangeProDefaultImg()
        {
            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            string ImgId = ReStr("ImgId");
            string ProId = ReStr("ProId");

            try
            {
                bll.ChangeProDefaultImg(ProId, ImgId);
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }


        }



        private void GetProList()
        {


            BLL.MerchantBLL bll = new BLL.MerchantBLL();
            int CurrentPage = ReInt("CurrentPage");

            string ProductTypeIds = ReStr("ProductTypeIds", "");
            decimal bPrice = ReDecimal("bPrice");
            decimal ePrice = ReDecimal("ePrice");
            string TownIds = ReStr("TownIds", "all");
            if (TownIds.Trim() == "")
            {
                TownIds = "all";
            }

            decimal MerchantId = ReDecimal("MerchantId", 0);

            string MerchantIds = ReStr("MerchantIds", "");
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");
            s.Append(" And pro.FlagInvalid=0 ");
            s.Append(" And pro.RePrice >= '" + bPrice + "' and pro.RePrice <= '" + ePrice + "' ");

            if (ProductTypeIds == "all" || ProductTypeIds == "0" || ProductTypeIds == "")
            {

            }
            else
            {
                s.Append(" AND  pro.ProTypeId in (" + ProductTypeIds + ") ");
            }
            if (MerchantId != 0)
            {
                //如果传递了商家id, 根据商家id添加查询条件
                s.Append(" and pro.MerchantId ='" + MerchantId + "'  ");

            }
            else
            {
                //如果没有传递商家ID,则判断商家ID集合是否有传递
                if (MerchantIds == "")
                {
                    //如果集合也没有传递, 什么都不做
                }
                else
                {
                    //如果传递了集合,添加查询条件
                    s.Append(" and pro.MerchantId in (" + MerchantIds + ") ");
                }

            }

            if (TownIds != "all")
            {
                s.Append(" AND  town.TownId in (" + TownIds + ") ");


            }


            //     s.Append(" order by mer.CreateTime desc ");

            DataSet ds = bll.GetProPageList2(s.ToString(), CurrentPage, 27);
            DataTable dt = ds.Tables[2];
            DataColumn dc = new DataColumn("CommentJson");
            dt.Columns.Add(dc);
            DataTable CommentDt = bll.GetProCommentArray(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DataTable ThisProCommentDt = DataSetting.TableSelect(" ProId='" + dr["ProId"] + "' ", CommentDt);
                dr["CommentJson"] = JsonHelper.ToJson(ThisProCommentDt);

            }
            RePage(ds);
        }



        private void GetProVsImg()
        {
            try
            {
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                DataTable dt = bll.GetProVsImg(ReStr("ProId"));
                string ImgJson = Common.JsonHelper.ToJson(dt);
                ReDict.Add("ProVsImg", ImgJson);
                ReTrue();
            }
            catch (Exception ex)
            {
                ReThrow(ex);
            }
        }

        private void GetProInfo()
        {
            try
            {
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                BLL.MemberBLL mbll = new BLL.MemberBLL();
                string ProId = ReStr("ProId");
                string BranchId = ReStr("BranchId", "");

                StringBuilder s = new StringBuilder();
                s.Append(" SELECT top 1 * FROM dbo.ProVsBranchView with(nolock) WHERE ProId='" + ProId + "' ");
                s.Append(" SELECT top 5 * FROM dbo.PingJiaView with(nolock) WHERE ProId='" + ProId + "' AND Invalid=0 ORDER BY CreateTime DESC ");
                s.Append(" SELECT COUNT(0) AS 总评价数 FROM dbo.PingJiaView with(nolock) WHERE ProId='" + ProId + "' AND Invalid=0 ");
                s.Append(" SELECT * FROM dbo.ProVsImgView with(nolock) WHERE ProId='" + ProId + "' ORDER BY OrderNo DESC ");
                s.Append(" SELECT COUNT(0) as 购物车数量 FROM dbo.Gwc WITH(NOLOCK) WHERE MemberId='" + mbll.GetCurrentDecimalMemberId() + "' AND ProId='" + ProId + "' AND Invalid=0  ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                DataTable dtPj = ds.Tables[1];

                int PjCount = int.Parse(ds.Tables[2].Rows[0]["总评价数"].ToString());
                DataTable dtImg = ds.Tables[3];
                DataTable dtGwcNum = ds.Tables[4];
                DataRow drGwcNum = dtGwcNum.Rows[0];
                int 购物车数量 = int.Parse(drGwcNum["购物车数量"].ToString());

                if (dt.Rows.Count == 0)
                {
                    //木有找到这个产品
                    throw new Exception("没有找到这个产品!");

                }
                else
                {
                    string json = Common.JsonHelper.ToJsonNo1(dt);
                    string ImgJson = Common.JsonHelper.ToJson(dtImg);
                    ReDict.Add("ProInfo", json);
                    ReDict.Add("ProVsImg", ImgJson);
                    ReDict2.Add("GwcNum", 购物车数量.ToString());
                    ReDict2.Add("PjCount", PjCount.ToString());
                    ReDict.Add("PjList", JsonHelper.ToJson(dtPj));
                    ReTrue();
                }
            }
            catch (Exception ex)
            {


                ReThrow(ex);
            }
        }


        /// <summary>
        /// 后台用获取分部商品详情
        /// </summary>
        private void GetProVsBranchInfoForSys()
        {
            try
            {
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                BLL.MemberBLL mbll = new BLL.MemberBLL();
                string ProId = ReStr("ProId");
                string BranchId = ReStr("BranchId", "");
                StringBuilder s = new StringBuilder();
                s.Append(" SELECT top 1 * FROM dbo.ProVsBranchView with(nolock) WHERE ProId='" + ProId + "' and BranchId='" + BranchId + "'  ");
                s.Append(" SELECT top 5 * FROM dbo.PingJiaView with(nolock) WHERE ProId='" + ProId + "' AND Invalid=0 ORDER BY CreateTime DESC ");
                s.Append(" SELECT COUNT(0) AS 总评价数 FROM dbo.PingJiaView with(nolock) WHERE ProId='" + ProId + "' AND Invalid=0 ");
                s.Append(" SELECT * FROM dbo.ProVsImgView with(nolock) WHERE ProId='" + ProId + "' ORDER BY OrderNo DESC ");

                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                DataTable dtPj = ds.Tables[1];

                int PjCount = int.Parse(ds.Tables[2].Rows[0]["总评价数"].ToString());
                DataTable dtImg = ds.Tables[3];


                if (dt.Rows.Count == 0)
                {
                    //木有找到这个产品
                    throw new Exception("没有找到这个产品!");

                }
                else
                {
                    string json = Common.JsonHelper.ToJsonNo1(dt);
                    string ImgJson = Common.JsonHelper.ToJson(dtImg);
                    ReDict.Add("ProInfo", json);
                    ReDict.Add("ProVsImg", ImgJson);
                    ReDict2.Add("PjCount", PjCount.ToString());
                    ReDict.Add("PjList", JsonHelper.ToJson(dtPj));
                    ReTrue();
                }
            }
            catch (Exception ex)
            {


                ReThrow(ex);
            }
        }



        /// <summary>
        /// 前台用获取分部商品详情
        /// </summary>
        private void GetProVsBranchInfo()
        {
            try
            {
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                BLL.MemberBLL mbll = new BLL.MemberBLL();
                string ProId = ReStr("ProId");
                string BranchId = ReStr("BranchId", "");
                decimal MemberId = mbll.GetCurrentDecimalMemberId();

                string ZoneId = ReStr("ZoneId", "");
                StringBuilder s = new StringBuilder();
                s.Append(" SELECT top 1 * FROM dbo.ProVsBranchView with(nolock) WHERE ProId='" + ProId + "' and BranchId='" + BranchId + "'  ");
                s.Append(" SELECT top 5 * FROM dbo.PingJiaView with(nolock) WHERE ProId='" + ProId + "' AND Invalid=0 ORDER BY CreateTime DESC ");
                s.Append(" SELECT COUNT(0) AS 总评价数 FROM dbo.PingJiaView with(nolock) WHERE ProId='" + ProId + "' AND Invalid=0 ");
                s.Append(" SELECT * FROM dbo.ProVsImgView with(nolock) WHERE ProId='" + ProId + "' ORDER BY OrderNo DESC ");
                s.Append(" SELECT ISNULL( SUM(Quantity),0) as 购物车数量 FROM dbo.Gwc WITH(NOLOCK) WHERE MemberId='" + MemberId + "' AND ProId='" + ProId + "' AND Invalid=0  and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "') ");
                s.Append(" SELECT ISNULL( SUM(RePrice*Quantity),0) AS 商品金额 ,ISNULL( SUM(Quantity),0) as 商品数量 FROM dbo.GwcView WITH(NOLOCK)  WHERE MemberId='" + MemberId + "' AND Invalid=0 and BranchId IN (SELECT BranchId FROM dbo.BranchVsZone WHERE ZoneId='" + ZoneId + "')  ");
                s.Append(" SELECT * FROM dbo.PeiSongType WITH(NOLOCK)  WHERE BranchId='" + BranchId + "' ");
                s.Append(" SELECT * FROM dbo.ShouCangPro WITH(NOLOCK)  WHERE MemberId='" + MemberId + "' AND ProId='" + ProId + "' ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                DataTable dt = ds.Tables[0];
                DataTable dtPj = ds.Tables[1];

                int PjCount = int.Parse(ds.Tables[2].Rows[0]["总评价数"].ToString());
                DataTable dtImg = ds.Tables[3];
                DataTable dtGwcNum = ds.Tables[4];  //本产品在购物车中的数量
                DataRow drGwcNum = dtGwcNum.Rows[0];
                int 购物车数量 = int.Parse(drGwcNum["购物车数量"].ToString());

                DataTable dtCount = ds.Tables[5];  //所有产品在购物车中的数量和价格

                DataRow drCount = dtCount.Rows[0];

                decimal 商品金额 = decimal.Parse(drCount["商品金额"].ToString());

                int 商品数量 = int.Parse(drCount["商品数量"].ToString());

                #region 派送方式
                DataTable dtPeiSongTypeList = ds.Tables[6];
                if (dtPeiSongTypeList.Rows.Count == 0)
                {
                    throw new Exception("此商品所在的店铺缺少派送方式!请联系管理员");
                }

                #endregion

                var ShouCangNum = ds.Tables[7].Rows.Count;



                if (dt.Rows.Count == 0)
                {
                    //木有找到这个产品
                    throw new Exception("没有找到这个产品!");

                }
                else
                {
                    string json = Common.JsonHelper.ToJsonNo1(dt);
                    string ImgJson = Common.JsonHelper.ToJson(dtImg);
                    ReDict.Add("ProInfo", json);
                    ReDict.Add("ProVsImg", ImgJson);
                    ReDict2.Add("ShouCangNum", ShouCangNum.ToString());
                    ReDict2.Add("GwcNum", 购物车数量.ToString());
                    ReDict2.Add("AllAmount", Math.Round(商品金额, 2).ToString());
                    ReDict2.Add("AllProNum", 商品数量.ToString());
                    ReDict2.Add("PjCount", PjCount.ToString());
                    ReDict.Add("PjList", JsonHelper.ToJson(dtPj));
                    ReDict.Add("PeiSongTypeList", JsonHelper.ToJson(dtPeiSongTypeList));
                    ReTrue();
                }
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }


        private void SaveProVsImg()
        {
            try
            {

                Model.ProVsImgModel model = new Model.ProVsImgModel();
                model.ProId = ReStr("ProId");
                model.ImgId = ReStr("ImgId");
                model.OrderNo = 1;
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                bll.SaveProVsImg(model);
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }


        }


        private void GetProCldClass()
        {
            //此变量为当前选定类别的ProClassId,将查出此类别的直属子类别以及全部子类别
            decimal CurrentProClassId = ReDecimal("CurrentProClassId", 0);
            StringBuilder s = new StringBuilder();

            s.Append(" With temp As( ");
            s.Append(" Select * From dbo.ProductClass a  WITH(NOLOCK) WHERE ProductClassId=" + CurrentProClassId + " ");
            s.Append(" Union All ");
            s.Append(" Select b.* From dbo.ProductClass b  WITH(NOLOCK) ");
            s.Append(" inner Join temp t On b.ParentProductClassId=t.ProductClassId ");
            s.Append(" ) ");
            s.Append(" Select * From temp  WITH(NOLOCK)  WHERE Invalid=0  ; ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            DataTable dtCld = Common.DataSetting.TableSelect(" ParentProductClassId='" + CurrentProClassId + "' ", dt);

            ReDict.Add("ProCldJsonList", JsonHelper.ToJson(dtCld));
            ReDict.Add("ProAllCldJsonList", JsonHelper.ToJson(dt));
            ReTrue();

        }

        void GetProClassX()   //外层为0时取得最上层
        {
            decimal MerId = ReDecimal("MerId");
            decimal ParentProductClassId = ReDecimal("ParentProductClassId", 0);
            bool Invalid = ReBool("Invalid", false);
            string col = ReStr("col", "*");
            BLL.MerchantBLL bll = new BLL.MerchantBLL();

            DataSet ds = DAL.DalComm.BackData(" SELECT " + col + " FROM  dbo.ProClassView WITH(NOLOCK) WHERE MerchantId=" + MerId + " and ParentProductClassId='" + ParentProductClassId + "' and Invalid='" + Invalid + "'  ");

            DataTable dt = ds.Tables[0];
            string json = Common.JsonHelper.ToJson(dt);
            ReDict.Add("ProClass", json);

            ReTrue();
        }

        private void GetProClass()
        {

            int CacheTime = ReInt("CacheTime", 300);


            try
            {
                decimal MerId = ReDecimal("MerId");
                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                DataTable dt = bll.GetProClass(MerId, CacheTime);





                string json = Common.JsonHelper.ToJson(dt);
                ReDict.Add("ProClass", json);

                ReTrue();

            }
            catch (Exception ex)
            {
                ReThrow(ex);

            }

        }

        private void GetProType()
        {
            try
            {


                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                DataTable dt = bll.GetProType("");   //获得全部的类别
                string json = Common.JsonHelper.ToJson(dt);
                ReDict.Add("ProType", json);
                ReTrue();

            }
            catch (Exception ex)
            {
                ReThrow(ex);

            }
        }

        private void SavePro()
        {
            try
            {

                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                Model.ProductModel model = new Model.ProductModel();
                BLL.UserBLL ubll = new BLL.UserBLL();
                model.ProId = ReStr("ProId", "");
                if (model.ProId != "")
                {//修改 
                    model = bll.GetProModel(model.ProId);
                }

                //  model.BuyLv = ReInt("BuyLv");
                model.CreateTime = DateTime.Now;
                model.CreateUser = ReStr("CreateUser", ubll.CurrentUserId());
                model.FlagInvalid = ReBool("FlagInvalid");

                // model.MerchantId = ReDecimal("MerchantId");
                model.AuthorId = ReStr("AuthorId", "");
                model.ProClassId = ReInt("ProClassId");
                model.ProTypeId = ReInt("ProTypeId", 0);
                model.ProContent = HttpContext.Current.Server.UrlDecode(ReStr("ProContent"));
                model.ProductImgId = ReStr("ProductImgId");
                model.Spec = ReStr("Spec", "");
                model.Status = ReInt("Status", 1);
                model.SendPara = ReInt("SendPara", 0);//不可配送
                model.ProCode = ReStr("ProCode", "");
                model.ProName = ReStrDeCode("ProName");
                model.ProTitle = ReStrDeCode("ProTitle");
                //  model.RecommendLv = ReInt("RecommendLv");
                model.StreetId = 0;

                model.RePrice2 = ReDecimal("RePrice2");
                model.RePrice3 = ReDecimal("RePrice3");

                model.Units = ReStr("Units");
                model.MerchantId = ReDecimal("MerId");
                model.Attr = ReStr("Attr", "<root></root>");
                model.PinPaiId = ReDecimal("PinPaiId", 0);
                if (model.PinPaiId == 0)
                {
                    throw new Exception("品牌没有填写, 如果没有品牌, 需要在'品牌维护'中维护一个品牌,名称为'无品牌',然后将商品的品牌定为无品牌即可.");
                }

                model.GetJiFenNum = ReDecimal("GetJiFenNum", 0);
                model.InheritPeiSongType = ReBool("InheritPeiSongType", true);
                model.IsInfiniteNum = ReBool("IsInfiniteNum", false);
                model.AllowPriceInterface = ReBool("AllowPriceInterface", true);
                model.AllowProNumInterface = ReBool("AllowProNumInterface", true);
                model.MinQuantity = ReDecimal("MinQuantity", 1);
                model.InheritProTeXing = ReBool("InheritProTeXing", true); //是否继承特性
                model.InheritJiFenNum = ReBool("InheritJiFenNum", true); //是否继承积分
                model.ProTeXing = ReInt("ProTeXing", 1);
                model.GetJiFenNum = ReDecimal("GetJiFenNum", 0);
                model.MinZl = ReDecimal("MinZl", 500);
                decimal Zlbs = ReDecimal("Zlbs", 1);
                model.Zl = model.MinZl * Zlbs; //起售质量是乘出来的
                model.InterfaceBaoZhuangNum = ReInt("InterfaceBaoZhuangNum", 1);
                model.ProNumCode = ReStr("ProNumCode", "");

                if (model.InterfaceBaoZhuangNum < 1)
                {
                    throw new Exception("你能把数量小于1的东西包装进去吗?");
                }


                if (!model.AllowPriceInterface)
                {
                    model.RePrice = ReDecimal("RePrice");
                }
                else
                {

                }

                if (!model.AllowProNumInterface)
                {
                    model.ProNum = ReDecimal("ProNum", 0);
                }


                model.MinQuantity = ReDecimal("MinQuantity", 1);
                DataTable dtImg = ReTable("imgArray");





                if (model.ProTypeId <= 0)
                {
                    // throw new Exception("产品类别必须选择");
                    model.ProTypeId = 397;
                }

                #region 事务开启

                TransactionOptions transactionOption = new TransactionOptions();
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    #endregion

                    bll.SavePro(model);
                    bll.DeleteProVsImg(model.ProId);

                    if (dtImg != null)
                    {
                        foreach (DataRow dr in dtImg.Rows)
                        {
                            Model.ProVsImgModel ImgVsModel = new ProVsImgModel();
                            ImgVsModel.ImgId = dr["ImgId"].ToString();
                            ImgVsModel.ProId = model.ProId;
                            ImgVsModel.VsType = "ProInfo";
                            ImgVsModel.OrderNo = 1;
                            bll.SaveProVsImg(ImgVsModel);
                        }
                    }

                    #region 添加新鲜事

                    string MerName = ReStr("MerName", "");
                    string ProImgUrl = ReStr("ProImgUrl", "");
                    string MerLogoUrl = ReStr("MerLogoUrl", "");
                    //用户修改了状态
                    Model.DynamicModel dyModel = new Model.DynamicModel();
                    dyModel.DynamicLv = 80;
                    dyModel.DynamicMerId = 0;
                    dyModel.DynamicTitle = "'" + MerName + "'上架了新产品: '" + Common.StringPlus.GetLeftStr(model.ProName, 100, "...") + "'";
                    dyModel.DynamicType = "新产品";
                    dyModel.DynamicUserId = model.CreateUser;
                    dyModel.DynamicMerId = model.MerchantId;

                    Dictionary<string, string> ReXml = new Dictionary<string, string>();
                    ReXml.Add("url", "/Pro/?ProId=" + model.ProId + "");


                    if (Common.FileString.IsFileCunZai(ProImgUrl))
                    {
                        ReXml.Add("ProImg", ReStr("ProImgUrl"));
                    }

                    ReXml.Add("MerName", MerName);
                    ReXml.Add("MerLogoUrl", MerLogoUrl);
                    ReXml.Add("RePrice", model.RePrice.ToString());
                    ReXml.Add("Units", model.Units);
                    ReXml.Add("ProId", model.ProId);
                    ReXml.Add("Title", Common.StringPlus.GetLeftStr(model.ProTitle, 100, "..."));
                    dyModel.JsonMemo = Common.XmlHelper.BackXmlStr(ReXml);
                    BLL.CommBLL.AddDynamic(dyModel);
                    #endregion



                    #region 开始添加配送方式

                    DataTable dtPeiSongType = ReTable("PeiSongTypeList");
                    bll.InheritPeiSongTypeByPro(model.ProId, model.InheritPeiSongType, dtPeiSongType);

                    #endregion


                    #region 事务关闭

                    transactionScope.Complete();


                }
                #endregion

                bll.ProductKeyWord(model.ProId);
                ReDict2.Add("ProId", model.ProId);
                ReTrue();


            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }




        }

        private void GetMerInfo()
        {
            try
            {

                BLL.MerchantBLL bll = new BLL.MerchantBLL();
                decimal MerId = ReDecimal("MerId");

                DataSet ds = bll.GetMerInfoFaseById(MerId);
                DataTable dt = ds.Tables[0];

                string json = Common.JsonHelper.ToJsonNo1(dt);

                DAL.AttentionDAL dal = new DAL.AttentionDAL();
                int i = 0;
                bool Attention = false;
                try
                {

                    i = dal.ExInt(" UserId='" + Common.CookieSings.GetCurrentUserId() + "' and AttentionMerId='" + MerId + "' ");
                }
                catch
                {

                }

                if (i > 0)
                {
                    Attention = true;
                }
                ReDict.Add("MerJson", json);
                ReDict2.Add("Attention", Attention.ToString().ToLower());
                ReTrue();


            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }
        }

        private void SaveMerchant()
        {


            try
            {
                DAL.TownDAL dal = new DAL.TownDAL();
                Model.MerchantModel model = new Model.MerchantModel();
                model.FlagInvalid = ReBool("FlagInvalid");
                model.HotLv = ReInt("HotLv");

                model.Lat = ReDecimal("Lat");
                model.Lng = ReDecimal("Lng");
                model.Logo = ReStr("Logo");
                model.MerchantClassId = ReDecimal("MerchantClassId");
                model.MerchantId = ReDecimal("MerchantId", 0);
                model.MerchantMemo = ReStr("MerchantMemo","");
                model.MerchantName = ReStr("MerchantName");
                model.MerchantTypeId = ReDecimal("MerchantTypeId");
                model.qq = ReStr("qq");
                model.Recommendlv = ReInt("Recommendlv");
                model.Tell = ReStr("Tell");
                model.TownId = ReDecimal("TownId");
                model.WebSite = ReStr("WebSite");
                model.Email = ReStr("Email");
                model.MerchantTypeTarget = ReStr("MerchantTypeTarget");
                model.InputCode = PinYin.GetFirstLetter(model.MerchantName);
                if (model.Lat == 0 || model.Lng == 0)
                {

                    throw new Exception("获取坐标有误,请尝试拖动红色图标然后再试!这种错误一般由于地图API的不稳定造成!");
                }



                BLL.MerchantBLL bll = new BLL.MerchantBLL();

                bll.SaveMer(model);

                ReDict2.Add("MerchantId", model.MerchantId.ToString());
                ReTrue();
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }













        }
    }
}

