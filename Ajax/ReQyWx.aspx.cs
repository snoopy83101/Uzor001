using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReQyWx : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {

        #region 是否为验证字符串
        string sToken = "wangli83";
        string sCorpID = "wx289bd60716320a43";
        string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";
        if (Request.Params["echostr"] != null)
        {

            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            // string sVerifyMsgSig = HttpUtils.ParseUrl("msg_signature");
            string sVerifyMsgSig = Request.Params["msg_signature"];
            // string sVerifyTimeStamp = HttpUtils.ParseUrl("timestamp");
            string sVerifyTimeStamp = Request.Params["timestamp"];
            // string sVerifyNonce = HttpUtils.ParseUrl("nonce");
            string sVerifyNonce = Request.Params["nonce"];
            // string sVerifyEchoStr = HttpUtils.ParseUrl("echostr");
            string sVerifyEchoStr = Request.Params["echostr"];
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            Response.Write(sEchoStr);
            Response.End();
        }
        #endregion

     
  


    }
}