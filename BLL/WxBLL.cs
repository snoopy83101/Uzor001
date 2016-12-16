using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Data;
using Model;
using Newtonsoft.Json.Linq;
using System.Transactions;


namespace BLL
{
    public class WxBLL
    {


        #region 普通微信
        #region 回复逻辑


        /// <summary>
        /// 素材类别的option循环
        /// </summary>
        /// <returns></returns>
        public string WxSuCaiClassSelectHtml()
        {
            DataSet ds = DAL.DalComm.BackData(" SELECT * FROM dbo.WxSuCaiClass WITH(NOLOCK)  ORDER BY OrderNo ");

            DataTable dt = ds.Tables[0];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["WxSuCaiClassId"] + "' >");
                w.Append(dr["WxSuCaiClassName"]);
                w.Append("</option>");
            }
            return w.ToString();
        }

        /// <summary>
        /// 回复逻辑
        /// </summary>
        /// <param name="MsgXml"></param>
        public string SendEvent(XmlElement rootElement)
        {

            RequestXML MsgXml = new RequestXML();

            MsgXml.MsgType = rootElement.SelectSingleNode("MsgType").InnerText;
            MsgXml.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            MsgXml.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
            MsgXml.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
            MsgXml.KeyTypeId = MsgXml.MsgType;



            StringBuilder s = new StringBuilder();
            StringBuilder c = new StringBuilder();
            BLL.MerchantBLL mbll = new MerchantBLL();
            s.Append(" select * from dbo.MemberView with(nolock) where WxOpenId='" + MsgXml.FromUserName + "' and Invalid=0 and YuanShiId='" + MsgXml.ToUserName + "' ");
            s.Append(" SELECT * FROM dbo.WxPtView  with(nolock) WHERE YuanShiId='" + MsgXml.ToUserName + "' AND Invalid=0 ");



            DataSet dsMember = DAL.DalComm.BackData(s.ToString());
            DataTable dtWxPt = dsMember.Tables[1];
            if (dtWxPt.Rows.Count != 1)
            {
                throw new Exception(" 原始ID为'" + MsgXml.ToUserName + "'数量是'" + dtWxPt.Rows.Count + "'! 请联系管理员:qq19278765 ");
            }
            DataRow drWxPt = dtWxPt.Rows[0];
            DataRow drMember;
            if (dsMember.Tables[0].Rows.Count > 0)
            {
                drMember = dsMember.Tables[0].Rows[0];
                //更改最后操作时间
                DAL.DalComm.ExReInt(" Update dbo.Member set LastTime='" + DateTime.Now + "' where MemberId='" + drMember["MemberId"] + "' ");

            }
            else
            {
                //首次关注,添加
                Model.MemberModel MemberModel = new MemberModel();
                MemberModel.Birthday = DateTime.Parse("1900-01-01");
                MemberModel.CreateTime = DateTime.Now;
                MemberModel.LastTime = DateTime.Now;
                MemberModel.Email = "";
                MemberModel.WxPtId = decimal.Parse(drWxPt["WxPtId"].ToString());
                MemberModel.WxOpenId = MsgXml.FromUserName;
                DAL.MemberDAL mbDal = new DAL.MemberDAL();
                mbDal.Add(MemberModel);
                int codeSize = 8 - MemberModel.MemberId.ToString().Length;
                MemberModel.MemberCode = MemberModel.MemberId.ToString();
                for (int i = 0; i < codeSize; i++)
                {
                    MemberModel.MemberCode = "0" + MemberModel.MemberCode;    //补齐前位的0,填充会员卡号
                }



                DataSet ds_ls = DAL.DalComm.BackData(" Update dbo.Member set MemberCode='" + MemberModel.MemberCode + "' where MemberId='" + MemberModel.MemberId + "'  select * from dbo.MemberView with(nolock) where  MemberId='" + MemberModel.MemberId + "'       UPDATE dbo.Member SET MerId=(SELECT TOP 1 dbo.WxPtInfo.MerId FROM dbo.WxPtInfo WHERE WxPtId=member.WxPtId ) WHERE MerId=0  ");

                drMember = ds_ls.Tables[0].Rows[0];
            }


            s.Clear();
            switch (MsgXml.MsgType.ToLower())
            {

                case "event":

                    MsgXml.KeyTypeDetailId = rootElement.SelectSingleNode("Event").InnerText; //按钮 click 
                    MsgXml.KeyTitle = GetMsgNodeValue(rootElement, "EventKey", "");
                    break;

                case "voice":   //如果是语音消息

                    MsgXml.KeyTitle = rootElement.SelectSingleNode("Recognition").InnerText; //语音识别内容

                    break;
                case "text":   //如果是文本消息
                    MsgXml.KeyTitle = rootElement.SelectSingleNode("Content").InnerText;

                    #region 系统功能(例如派送订单确认)

                    #region 派送订单确认


                    //如果输入了全部订单号, 或者以小数点开头输入了部分订单
                    if (MsgXml.KeyTitle.Trim().Length == 20 || MsgXml.KeyTitle.Trim().Substring(0, 1) == ".")
                    {


                        return QueakSend(MsgXml, mbll.QueRenDingDan(MsgXml.KeyTitle, MsgXml.FromUserName, ""));


                    }


                    #endregion
                    #endregion

                    break;

                case "image":  //如果是图片消息

                    MsgXml.KeyTitle = "";
                    break;

            }

            #region 判断是否快捷会员公共方法


            if (MsgXml.KeyTitle.ToLower() == "quickcard")
            {
                return MyMemberQuickCard(MsgXml);
            }

            #endregion



            s.Append("  SELECT * FROM CORE.dbo.WxKeyView with(nolock) WHERE YuanShiId='" + MsgXml.ToUserName + "' AND KeyTypeId='" + MsgXml.MsgType.ToLower() + "'  ");
            s.Append(" and  WxSendInvalid=0 and WxPtInvalid=0  ");
            if (MsgXml.KeyTitle != "")
            {
                s.Append(" AND  KeyTitle='" + MsgXml.KeyTitle + "' ");
            }

            if (MsgXml.KeyTypeDetailId != "")
            {
                s.Append(" AND KeyTypeDetailId='" + MsgXml.KeyTypeDetailId + "' ");
            }

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dtKey = ds.Tables[0];

            if (dtKey.Rows.Count > 0)
            {
                DataRow drKey = dtKey.Rows[0];


                string SendContent = Common.StringPlus.WxSendStr(HttpUtility.UrlDecode(drKey["SendContent"].ToString()));

                switch (drKey["WxSendClassId"].ToString())
                {

                    case "1": //快速文本回复

                        return QueakSend(MsgXml, SendContent);

                        break;

                    case "5":  //素材回复

                        string WxSuCaiId = drKey["WxSuCaiId"].ToString();

                        s.Clear();
                        s.Append(" select * from dbo.WxSuCaiInfo Where WxSuCaiInfoId='" + WxSuCaiId + "' ");
                        s.Append(" select * from dbo.WxSuCaiDetailView where WxSuCaiInfoId='" + WxSuCaiId + "'  ");
                        DataSet dsSuCai = DAL.DalComm.BackData(s.ToString());

                        DataTable dtSuCaiInfo = dsSuCai.Tables[0];
                        if (dtSuCaiInfo.Rows.Count == 0)
                        {
                            throw new Exception("没有找到对应的素材!");
                        }
                        DataRow drSuCaiInfo = dtSuCaiInfo.Rows[0];
                        switch (drSuCaiInfo["WxSuCaiClassId"].ToString())
                        {
                            case "1": //文本素材
                                return QueakSend(MsgXml, drSuCaiInfo["WxSuCaiContent"].ToString());
                                break;

                            case "2": //图文素材
                                DataTable dtSuCaiDetail = dsSuCai.Tables[1];
                                return ImgTextSend(MsgXml, dtSuCaiDetail);
                                break;
                        }
                        break;


                }


            }
            else
            {

            }

            return "";
        }


        #region 常用方法


        public string GetMsgNodeValue(XmlElement rootElement, string Key)
        {
            return rootElement.SelectSingleNode(Key).InnerText;
        }
        public string GetMsgNodeValue(XmlElement rootElement, string Key, string CatchStr)
        {
            try
            {
                return GetMsgNodeValue(rootElement, Key);
            }
            catch (Exception)
            {

                return CatchStr;
            }

        }

        public string ImgTextSend(RequestXML MsgXml, DataTable dtSuCaiDetail)
        {
            if (dtSuCaiDetail.Rows.Count == 0)
            {
                throw new Exception("");
            }

            StringBuilder w = new StringBuilder();
            StringBuilder s = new StringBuilder();
            s.Append(" select * from dbo.WxPtInfo where YuanShiId='" + MsgXml.ToUserName + "' and Invalid=0 and MerId='" + dtSuCaiDetail.Rows[0]["MerId"] + "' ");
            DataTable dtPtInfo = DAL.DalComm.BackData(s.ToString()).Tables[0];
            var ptNum = dtPtInfo.Rows.Count;
            if (ptNum != 1)
            {
                throw new Exception(" 该商家下绑定了" + ptNum + "个原始ID为" + MsgXml.ToUserName + "的公众平台账号! 请将重复绑定的公众号作废或联系管理员! ");
            }

            DataRow drPtInfo = dtPtInfo.Rows[0];


            w.Append("<xml>");
            w.Append("<ToUserName><![CDATA[" + MsgXml.FromUserName + "]]></ToUserName>");
            w.Append("<FromUserName><![CDATA[" + MsgXml.ToUserName + "]]></FromUserName>");
            w.Append("<CreateTime>" + MsgXml.CreateTime + "</CreateTime>");
            w.Append("<MsgType><![CDATA[news]]></MsgType>");
            w.Append("<ArticleCount>" + dtSuCaiDetail.Rows.Count + "</ArticleCount>");
            w.Append("<Articles>");

            foreach (DataRow dr in dtSuCaiDetail.Rows)
            {




                w.Append("<item>");
                w.Append("<Title><![CDATA[" + dr["WxSuCaiDetailTitle"] + "]]></Title>");
                w.Append("<Description><![CDATA[" + dr["WxSuCaiDetailMemo"] + "]]></Description>");
                w.Append("<PicUrl><![CDATA[http://116.255.167.132" + dr["ImgUrl"].ToString() + "]]></PicUrl>");
                StringBuilder u = new StringBuilder();
                switch (dr["WxSuCaiDetailClassId"].ToString())
                {
                    case "10"://新闻
                        u.Append(drPtInfo["ArticleUrl"].ToString());
                        u.Append("?");
                        u.Append("ArticleId=" + dr["ReKey"] + "");

                        break;
                    case "20"://产品
                        u.Append(drPtInfo["ProUrl"].ToString());
                        u.Append("?");
                        u.Append("ProId=" + dr["ReKey"] + "");
                        break;
                    case "30": //帖子
                        u.Append(drPtInfo["TieZiUrl"].ToString());
                        u.Append("?");
                        u.Append("TieZiId=" + dr["ReKey"] + "");
                        break;
                    case "100": //直接指定


                        u.Append(dr["Url"].ToString().Trim());

                        break;
                    default:
                        break;
                }


                w.Append("<Url><![CDATA[" + FormatLink(u.ToString(), MsgXml) + "]]></Url>");
                //   throw new Exception(FormatLink(u.ToString(),MsgXml));
                w.Append("</item>");

            }
            w.Append("</Articles>");
            w.Append("</xml>");

            return w.ToString();
        }

        public string QueakSend(RequestXML MsgXml, string SendStr)
        {
            StringBuilder w = new StringBuilder();

            w.Append("<xml>");
            w.Append("<ToUserName><![CDATA[" + MsgXml.FromUserName + "]]></ToUserName>");
            w.Append("<FromUserName><![CDATA[" + MsgXml.ToUserName + "]]></FromUserName>");
            w.Append("<CreateTime>" + MsgXml.CreateTime + "</CreateTime>");
            w.Append("<MsgType><![CDATA[text]]></MsgType>");
            w.Append("<Content><![CDATA[" + SendStr + "]]></Content>");
            w.Append("</xml>");

            return w.ToString();

        }


        public string FormatLink(string linkUrl, RequestXML MsgXml)
        {
            StringBuilder l = new StringBuilder();
            l.Append(linkUrl);
            if (Common.StringPlus.IsBaoHan(linkUrl, "WxOpenId="))
            {

            }
            else
            { //如果没有包含, 那么添加openId参数
                if (Common.StringPlus.IsBaoHan(linkUrl, "?"))
                {
                    l.Append("&");
                }
                else
                {
                    l.Append("?");
                }
                l.Append("WxOpenId=" + MsgXml.FromUserName + "");
            }
            return l.ToString();

        }
        #endregion

        #endregion


        #region 会员逻辑

        public string MyMemberQuickCard(RequestXML MsgXml)
        {

            StringBuilder w = new StringBuilder();

            StringBuilder s = new StringBuilder();
            s.Append(" select MemberCode,MemberName from CORE.dbo.MemberView where YuanShiId='" + MsgXml.ToUserName + "' and WxOpenId='" + MsgXml.FromUserName + "' ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    w.Append("您的会员编号是:" + dr["MemberCode"] + " \n ");
                    w.Append("您的会员名是:" + dr["MemberName"] + "");



                }

            }
            else
            {
                w.Append("您还没有开通会员卡! 请点击微会员--我的会员卡进行开通!");

            }


            return QueakSend(MsgXml, w.ToString());




        }


        //public DataSet MemberInfo(RequestXML MsgXml)
        //{ 


        //}
        #endregion

        #region 菜单逻辑


        public string PostWxPtMenuJson(decimal WxPtId, string MenuJson)
        {

            //MenuJson = "{" +
            //        "\"button\":[" +
            //            "{\"name\":\"菜单名称1\"," +
            //            "\"type\":\"click\"," +
            //            "\"key\":\"V01_S01\"" +
            //            "}," +
            //            "{\"name\":\"菜单名称2\"," +
            //            "\"type\":\"click\"," +
            //            "\"key\":\"V02_S01\"" +
            //            "}," +
            //            "{\"name\":\"菜单名称1\"," +
            //            "\"type\":\"click\"," +
            //            "\"key\":\"V03_S01\"" +
            //            "}" +
            //        "]" +
            //    "}";


            string ak = GetTokenStr(WxPtId);
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + ak + "";
            return WxPostPage(url, MenuJson, ak);

        }


        /// <summary>
        /// 获取菜单Json
        /// </summary>
        /// <param name="WxPtId"></param>
        /// <returns></returns>
        public string GetWxPtMenuJson(decimal WxPtId)
        {
            string ak = GetTokenStr(WxPtId);



            string jsonStr = Common.PageInput.GetHTML("https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + ak + "");

            return jsonStr;
        }

        #endregion

        #region 绑定维护

        /// <summary>
        /// 作废回复
        /// </summary>
        /// <param name="Invalid"></param>
        /// <param name="WxPtId"></param>
        public void InvalidWxSendInfo(bool Invalid, decimal WxSendId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.WxSendInfo SET INVALID='" + Invalid + "' where WxSendId='" + WxSendId + "' ");
            DAL.DalComm.BackData(s.ToString());
        }


        /// <summary>
        /// 作废微信平台
        /// </summary>
        /// <param name="Invalid"></param>
        /// <param name="WxPtId"></param>
        public void InvalidWxPt(bool Invalid, decimal WxPtId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.WxPtInfo SET INVALID='" + Invalid + "' where WxPtId='" + WxPtId + "' ");
            DAL.DalComm.BackData(s.ToString());
        }

        /// <summary>
        /// 作废素材
        /// </summary>
        /// <param name="Invalid"></param>
        /// <param name="WxSuCaiInfoId"></param>
        public void InvalidSuCai(bool Invalid, decimal WxSuCaiInfoId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE dbo.WxSuCaiInfo SET INVALID='" + Invalid + "' where WxSuCaiInfoId='" + WxSuCaiInfoId + "' ");
            DAL.DalComm.BackData(s.ToString());
        }


        public Model.WxSuCaiInfoModel GetSuCaiInfoModel(decimal WxSuCaiInfoId)
        {
            DAL.WxSuCaiInfoDAL dal = new DAL.WxSuCaiInfoDAL();
            return dal.GetModel(WxSuCaiInfoId);

        }

        public DataSet GetSuCaiPageList(string strWhere, int c, int pagesize, string cols)
        {
            DAL.WxSuCaiInfoDAL dal = new DAL.WxSuCaiInfoDAL();
            DataSet ds = dal.GetPageList(strWhere, c, pagesize, cols);
            DataTable dt = ds.Tables[2];
            if (dt.Rows.Count == 0)
            {
                return ds;
            }

            dt.Columns.Add("DetailJson");
            List<string> li = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {

                li.Add(dr["WxSuCaiInfoId"].ToString());

            }
            string WxSuCaiInfoIdStr = string.Join(",", li);

            DAL.WxSuCaiDetailDAL detailDal = new DAL.WxSuCaiDetailDAL();
            DataSet dsDetail = detailDal.GetList(" WxSuCaiInfoId in (" + WxSuCaiInfoIdStr + ") ");
            DataTable dtDetail = dsDetail.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                DataTable dj = Common.DataSetting.TableSelect(" WxSuCaiInfoId='" + dr["WxSuCaiInfoId"] + "' ", dtDetail);

                dr["DetailJson"] = Common.JsonHelper.ToJson(dj);

            }
            return ds;
        }



        /// <summary>
        /// 保存一个素材主体
        /// </summary>
        /// <param name="model"></param>
        public void SaveSuCaiInfo(Model.WxSuCaiInfoModel model)
        {
            DAL.WxSuCaiInfoDAL dal = new DAL.WxSuCaiInfoDAL();



            if (model.WxSuCaiInfoId == 0)
            {
                //新增
                model.CreateTime = DateTime.Now;

                dal.Add(model);

            }
            else
            {
                //修改
                dal.Update(model);

            }


        }



        public void RemoveSuCaiDetail(string strWhere)
        {

            DAL.WxSuCaiDetailDAL dal = new DAL.WxSuCaiDetailDAL();
            dal.DeleteList(strWhere);
        }
        /// <summary>
        /// 保存一个素材明细
        /// </summary>
        /// <param name="model"></param>
        public void SaveSuCaiDetail(Model.WxSuCaiDetailModel model)
        {


            DAL.WxSuCaiDetailDAL dal = new DAL.WxSuCaiDetailDAL();
            if (model.WxSuCaiDetailId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }


        }


        /// <summary>
        /// 获得一个回复明细的类别html(option序列)
        /// </summary>
        /// <returns></returns>
        public string GetWxSuCaiDetailClassHtml()
        {
            StringBuilder s = new StringBuilder();
            s.Append(" select * from dbo.WxSuCaiDetailClass with(nolock)  order by OrderNo ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            StringBuilder w = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    w.Append("<option value='" + dr["WxSuCaiDetailClassId"] + "' >");
                    w.Append(dr["WxSuCaiDetailClassName"]);
                    w.Append("</option>");


                }
                return w.ToString();
            }
            else
            {
                return "";
            }
        }


        public DataSet GetWxSendInfo(decimal WxSendId)
        {

            StringBuilder s = new StringBuilder();
            s.Append("  select * from dbo.WxSendView with(nolock) where WxSendId='" + WxSendId + "'  ");
            s.Append(" select * from dbo.WxKeyView with(nolock) where WxSendId='" + WxSendId + "' ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }


        public DataSet SearchSendPageList(int CurrentPage, int p, string strWhere, string col)
        {

            DAL.WxSendInfoDAL dal = new DAL.WxSendInfoDAL();

            return dal.GetPageList(strWhere, CurrentPage, p, col);


        }


        public void SaveSendInfo(Model.WxSendInfoModel model)
        {

            Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();
            DAL.WxSendInfoDAL dal = new DAL.WxSendInfoDAL();
            if (model.WxSendId == 0)
            {
                model.CreateTime = DateTime.Now;
                model.CreateUser = cm.CurrentUserId;
                dal.Add(model);

            }
            else
            {
                dal.Update(model);
            }
        }


        public Model.WxSendInfoModel GetWxSendModel(decimal WxSendId)
        {

            DAL.WxSendInfoDAL dal = new DAL.WxSendInfoDAL();
            return dal.GetModel(WxSendId);
        }




        public string GetWxSendClassSelectHtml()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" select * from dbo.WxSendClass with(nolock)  ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            StringBuilder w = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    w.Append("<option value='" + dr["WxSendClassId"] + "' >");
                    w.Append(dr["WxSendClassTitle"]);
                    w.Append("</option>");


                }
                return w.ToString();
            }
            else
            {
                return "";
            }

        }


        public DataTable GetWxKeyTypeDetailSelectData(string KeyTypeId)
        {
            StringBuilder s = new StringBuilder();

            s.Append(" select * from dbo.WxKeyTypeDetail with(nolock)  where KeyTypeId='" + KeyTypeId + "' ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            return dt;

        }


        public string GetWxKeyTypeSelectHtml()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" select * from dbo.WxKeyType with(nolock)  ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            StringBuilder w = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    w.Append("<option value='" + dr["KeyTypeId"] + "' >");
                    w.Append(dr["KeyTypeName"]);
                    w.Append("</option>");


                }
                return w.ToString();
            }
            else
            {
                return "";
            }

        }


        public string GetWxPtListSelectHtml()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" select * from dbo.WxPtType with(nolock)  ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            StringBuilder w = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    w.Append("<option value='" + dr["WxPtTypeId"] + "' >");
                    w.Append(dr["WxPtTypeName"]);
                    w.Append("</option>");


                }
                return w.ToString();
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 保存微信公众平台
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void SaveWxPtInfo(Model.WxPtInfoModel model)
        {


            DAL.WxPtInfoDAL dal = new DAL.WxPtInfoDAL();
            if (model.WxPtId == 0)
            {
                //添加微信公众平台
                model.AccessTokenCreateTime = DateTime.Now;

                dal.Add(model);
            }
            else
            {
                //修改微信公众平台
                dal.Update(model);
            }

        }


        /// <summary>
        /// 获取公众平台资料
        /// </summary>
        /// <param name="WxPtId"></param>
        /// <returns></returns>
        public DataSet GetWxPtInfoData(decimal WxPtId)
        {

            DAL.WxPtInfoDAL dal = new DAL.WxPtInfoDAL();

            return dal.GetInfoData(WxPtId);

        }

        public DataSet GetWxPtPageList(string strWhere, int c, int p, string col)
        {
            DAL.WxPtInfoDAL dal = new DAL.WxPtInfoDAL();

            return dal.GetPageList(strWhere, c, p, col);
        }



        public Model.WxPtInfoModel GetWxPtInfoModel(decimal WxPtId)
        {

            DAL.WxPtInfoDAL dal = new DAL.WxPtInfoDAL();

            return dal.GetModel(WxPtId);
        }


        #endregion

        #region 接口操作



        public string SendWxMsg(string SendMsg)
        {


            try
            {
                #region 回复图文消息

                StringBuilder j2 = new StringBuilder();
                j2.Append("{");
                j2.Append(" \"touser\":\"oOU-6uPx-2TNI_DU5SCLFaBXUSVk\",");
                j2.Append("\"msgtype\":\"news\",");
                j2.Append(" \"news\":{");
                j2.Append("\"articles\": [");
                j2.Append(" {");
                j2.Append(" \"title\":\"新订单:\",");
                j2.Append("\"description\":\"" + SendMsg + " \",");
                j2.Append(" \"url\":\"#\",");
                j2.Append("\"picurl\":\"#\"");
                j2.Append(" },");
                j2.Append("{");
                j2.Append("\"title\":\"立即回复\",");
                j2.Append("\"description\":\"分享您的生活\",");
                j2.Append("\"url\":\"#\",");
                j2.Append("\"picurl\":\"http://www.yyinfo.net/wap/icon/yule.png\"");
                j2.Append("}");


                j2.Append("]");
                j2.Append("}");
                j2.Append("}");



                //  return WxPostPage("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=你的token", j2.ToString());
                return "";
                #endregion
            }
            catch (Exception ex)
            {

                return ex.Message;
            }



        }

        public string SendWxText(string openId, string text)
        {

            StringBuilder s = new StringBuilder();
            s.Append("{");
            s.Append(" \"touser\":\"" + openId + "\",");
            s.Append(" \"msgtype\":\"text\",");
            s.Append(" \"text\":");
            s.Append(" {");
            s.Append("\"content\":\"" + text + "\"");
            s.Append(" }");
            s.Append("}");
            //  return WxPostPage("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=你的token", s.ToString());

            return "";
        }


        /// <summary>
        /// 开通认真的服务号发送客服消息
        /// </summary>
        /// <returns></returns>
        public string HttpPost()
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            string[] AllKey = HttpContext.Current.Request.Params.AllKeys;
            if (AllKey.Length > 0)
            {
                foreach (string key in AllKey)
                {
                    PostVars.Add(key, Common.PageInput.ReStr(key, ""));
                }
            }
            else
            {
                return "";
            }

            PostVars.Add("para", "GetArticleInfo");
            PostVars.Add("ArticleId", "14031705174932481508");
            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues("http://www.yyinfo.net/aar/", "POST", PostVars);
                //下面都没用啦，就上面一句话就可以了
                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
                //这是获取返回信息
                return sRemoteInfo;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //  request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //  response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream,
 Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// <summary>
        /// 用"你的token"替换token字符串, 来提交微信的post请求
        /// </summary>
        /// <param name="wxUrl"></param>
        /// <param name="wxJson"></param>
        /// <returns></returns>
        public string WxPostPage(string wxUrl, string wxJson, string ak)
        {

            string h = Common.PageInput.WxGetPage(wxUrl, wxJson);

            if (Common.StringPlus.GetStrCount(h, "40001") > 0)
            {

                //基本上完蛋了,再试一次


                string No2TokenUrl = wxUrl.Replace("你的token", RefToken());
                return Common.PageInput.WxGetPage(No2TokenUrl, wxJson);

            }
            else
            {
                return h;
            }
        }





        /// <summary>
        /// 获得公众平台的AccessToken
        /// </summary>
        /// <param name="WxPtId"></param>
        /// <returns></returns>
        public string GetTokenStr(decimal WxPtId)
        {
            DataSet ds = DAL.DalComm.BackData(" select * from dbo.WxPtInfo with(nolock) where WxPtId='" + WxPtId + "' ");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string ak = Common.PageInput.GetHTML("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + dr["AppId"].ToString().Trim() + "&secret=" + dr["AppSecret"].ToString().Trim() + "");

                JObject obj = JObject.Parse(ak);

                ak = (string)obj["access_token"];
                return ak;
            }
            else
            {
                throw new Exception("WxPtId为" + WxPtId + "数据不存在!");
            }
        }

        public string GetAccessToken()
        {
            DateTime dt_100MintesAgo = DateTime.Now.AddMinutes(-100);

            DAL.TokenConfigDAL dal = new DAL.TokenConfigDAL();

            //大于等于一个小时之前
            string AccessToken = "";

            AccessToken = DAL.DalComm.ExStr(" select AccessToken from dbo.TokenConfig where  CreateTime	>= '" + dt_100MintesAgo.ToString("yyyy-MM-dd HH:mm:ss") + "'  ");

            if (AccessToken == "")
            {//应该刷新了
                return RefToken();
            }
            else
            {//不应该刷新 
                return AccessToken;
            }

        }

        public string RefToken()
        {
            string AccessToken = "";
            Model.TokenConfigModel model = new Model.TokenConfigModel();
            DAL.TokenConfigDAL dal = new DAL.TokenConfigDAL();
            AccessToken = Common.PageInput.TokenStr();
            model.AccessToken = AccessToken;
            model.CreateTime = DateTime.Now;
            model.Gzh = "nmj_yyinfo";
            model.Memo = "";
            model.TokenType = "all";
            dal.DeleteList(" Gzh='nmj_yyinfo' and TokenType='all' ");
            dal.Add(model);
            return AccessToken;
        }

        #endregion


        #endregion


        #region 企业微信

        #region 企业微信维护


        public void SaveQyWxPtInfo(Model.QyWxPtModel model)
        {
            DAL.QyWxPtDAL dal = new DAL.QyWxPtDAL();


            if (model.QyWxPtId == 0)
            {
                model.CreateTime = DateTime.Now;
                dal.Add(model);

            }
            else
            {
                dal.Update(model);
            }

        }

        public Model.QyWxPtModel GetQyWxPtModel(decimal QyWxPtId)
        {
            DAL.QyWxPtDAL dal = new DAL.QyWxPtDAL();
            return dal.GetModel(QyWxPtId);

        }

        public string QyWxUrl(string url, decimal MerId)
        {
            url = HttpContext.Current.Server .UrlEncode(url);

            if (Common.StringPlus.IsBaoHan(url, "?"))
            {

            }
            else
            {

            }
 

            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + GetCorpIdByMerId(MerId) + "&redirect_uri=" + url + "&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";
        }

        public string GetCorpIdByMerId(decimal MerId )
        {
            StringBuilder s = new StringBuilder();
            s.Append("SELECT TOP 1 CorpId FROM  dbo.QyWxPt WITH(NOLOCK) WHERE MerId='" + MerId + "' ");
            return DAL.DalComm.ExStr(s.ToString());
        }

        public DataSet GetQyWxPtInfoByMerId(decimal MerId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("SELECT * FROM dbo.QyWxPt WHERE MerId='" + MerId + "' ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        
        }

        public DataSet GetQyWxPtInfo(decimal QyWxPtId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("SELECT * FROM dbo.QyWxPt WHERE QyWxPtId='" + QyWxPtId + "' ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }


        public DataSet GetQyWxPtGorupInfo(decimal QyWxPtGroupId)
        {
            StringBuilder s = new StringBuilder();
            s.Append("SELECT * FROM dbo.QyWxPtGorupView WHERE QyWxPtGroupId='" + QyWxPtGroupId + "' ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;
        }

        public Model.QyWxPtGroupModel GetQyWxPtGroupModel(decimal QyWxPtGroupId)
        {
            DAL.QyWxPtGroupDAL dal = new DAL.QyWxPtGroupDAL();
            return dal.GetModel(QyWxPtGroupId);
        }

        public void SaveQyWxPtGroupInfo(Model.QyWxPtGroupModel model)
        {

            DAL.QyWxPtGroupDAL dal = new DAL.QyWxPtGroupDAL();
            if (model.QyWxPtGroupId == 0)
            {
                model.AccessTokenCreateTime = DateTime.Parse("1900-01-01");
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }

        }


        public void SaveQyWxPtAppInfo(Model.QyWxPtAppModel model)
        {
            DAL.QyWxPtAppDAL dal = new DAL.QyWxPtAppDAL();
            if (model.QyWxPtAppId == 0)
            {
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }

        }

        public DataSet GetQyWxPtAppInfo(decimal QyWxPtAppId)
        {

            StringBuilder s = new StringBuilder();
            s.Append("SELECT * FROM dbo.QyWxPtAppView WHERE QyWxPtAppId='" + QyWxPtAppId + "' ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());
            return ds;

        }

        public DataSet GetQyWxPtVsUserList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DAL.QyWxPtVsUserDAL dal = new DAL.QyWxPtVsUserDAL();
            return dal.GetPageList(strWhere, Order, currentpage, pagesize, col);

        }


        public string DefaltGroupSelHtml(decimal QyWxPtId)
        {


            StringBuilder w = new StringBuilder();

            DAL.QyWxPtGroupDAL dal = new DAL.QyWxPtGroupDAL();
            DataSet ds = dal.GetList(" QyWxPtId='" + QyWxPtId + "' ");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    w.Append("<option value='" + dr["QyWxPtGroupId"] + "' >");
                    w.Append(dr["QyWxPtGroupMemo"]);
                    w.Append("</option>");

                }
            }

            return w.ToString();

        }


        public void SaveWxPtGroupVsUser(Model.QyWxPtVsUserModel model)
        {

            DAL.QyWxPtVsUserDAL dal = new DAL.QyWxPtVsUserDAL();
            int i = dal.ExInt(" UserId='" + model.UserId + "' and QyWxPtId='" + model.QyWxPtId + "'  ");
            if (i > 0)
            {
                dal.Update(model);
            }
            else
            {
                dal.Add(model);
            }
        }

        #endregion


        #region 企业微信发布

        /// <summary>
        /// 获得企业微信平台的AccessToken
        /// </summary>
        /// <param name="QyWxPtGroupId"></param>
        /// <returns></returns>
        public string GetQyWxPtAccessToken(decimal QyWxPtGroupId)
        {
            string AccessToken = "";
            string Secret = "";
            string CorpId = "";
            StringBuilder s = new StringBuilder();
            s.Append("SELECT top 1 AccessTokenCreateTime,AccessToken,Secret,CorpId FROM dbo.QyWxPtGorupView with(nolock) WHERE QyWxPtGroupId='" + QyWxPtGroupId + "'");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("没有发现QyWxPtGroupId为'" + QyWxPtGroupId + "'的权限!缺少必要的Secret!");
            }
            DataRow dr = dt.Rows[0];
            var dtmStr = dr["AccessTokenCreateTime"].ToString();
            Secret = dr["Secret"].ToString();
            CorpId = dr["CorpId"].ToString();

            DateTime dtm;
            try
            {
                DateTime.Parse(dtmStr);
            }
            catch
            {

                throw new Exception("'" + dtmStr + "'不能被转换为时间对象!");
            }

            dtm = DateTime.Parse(dtmStr);
            TimeSpan ts = DateTime.Now - dtm;
            if (ts.TotalMinutes > 90)  //如果大于90分钟没有更新
            {
                string AccessTokenJson = Common.PageInput.GetHTML("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + CorpId + "&corpsecret=" + Secret + "");
                JObject obj = JObject.Parse(AccessTokenJson);
                AccessToken = (string)obj["access_token"];


                //更新AccessToken在服务器端的值
                DAL.DalComm.ExReInt("UPDATE dbo.QyWxPtGroup SET AccessToken='" + AccessToken + "' ,AccessTokenCreateTime='" + DateTime.Now + "' WHERE QyWxPtGroupId='" + QyWxPtGroupId + "'");

            }
            else
            {
                AccessToken = dr["AccessToken"].ToString();
            }

            return AccessToken;
        }

        public string SendQyTextMsg(List<string> ToUserList,string text,decimal QyWxPtAppId)
        {
            if (ToUserList.Count == 0)
            {
                return "";
            }

            StringBuilder s = new StringBuilder();

            s.Append(" SELECT AgentID,DefaultGroupId FROM dbo.QyWxPtAppView with(nolock) WHERE QyWxPtAppId='" + QyWxPtAppId + "' ");

            s.Append("     SELECT QyWxPtUserId FROM dbo.QyWxPtVsUser WHERE UserId IN ( 'fuckThisAll'");
            foreach (string UserId in ToUserList)
            {
                s.Append(",'"+ UserId + "'");

            }

            s.Append(")");
            s.Append(" and QyWxPtId =(SELECT TOP 1 QyWxPtId FROM dbo.QyWxPtApp WHERE QyWxPtAppId='"+QyWxPtAppId+ "') GROUP BY QyWxPtUserId ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("没有定位到QyWxPtAppId:" + QyWxPtAppId + ",的应用!");
            }
            DataRow dr=dt.Rows[0];
            string AgentID = dr["AgentID"].ToString();
            decimal DefaultGroupId = decimal.Parse(dr["DefaultGroupId"].ToString());

            string SendUrl = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + GetQyWxPtAccessToken(DefaultGroupId) + "";
            JObject obj = new JObject();

            DataTable dtQyWxUser = ds.Tables[1];
            if (dtQyWxUser.Rows.Count == 0)
            {
                return "";
            }

            List<string> ToQyWxUserList = new List<string>();

            foreach (DataRow drQyWxUser in dtQyWxUser.Rows)
            {

                ToQyWxUserList.Add(drQyWxUser["QyWxPtUserId"].ToString());
            }

            string UserIds= string.Join("|", ToQyWxUserList);
            obj["touser"] = UserIds;
            obj["toparty"] = "";
            obj["totag"] = "";
            obj["msgtype"] = "text";
            obj["agentid"] = AgentID;
            obj["text"] = JToken.Parse("{  \"content\": \""+text+"\"   }");
            obj["safe"] = "0";
            obj.ToString();

           return Common.PageInput.WxGetPage(SendUrl, obj.ToString());

        
        }

        #endregion

        #endregion

    }
}
