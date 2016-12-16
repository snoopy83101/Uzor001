using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class ajaxTest : System.Web.UI.Page
    {
        protected string artJson = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.WxBLL bll = new BLL.WxBLL();
            string str = bll.HttpPost();
        }


        string ToPost()
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
    }
