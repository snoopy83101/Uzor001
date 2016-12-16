using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text;
using Model;
using Common;
namespace BLL.BJ
{
    public class MerSetting : System.Web.UI.Page
    {


        public decimal MerId = 0;

        public DataSet ds_Mer = null;

        public string MerTypeSelectOpHtml = string.Empty;

        public MerSetting(decimal MerId)
        {
      
            BLL.MerchantBLL bll = new MerchantBLL();
           // CheckBindMer(MerId);
            ds_Mer = bll.GetMerInfo(MerId);
            BindMerTypeSelectOpHtml();

        }



        public MerSetting()
        {

            BindMerTypeSelectOpHtml();

        }

        /// <summary>
        /// 检查商家和当前用户的绑定
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public bool CheckBindMer(decimal MerId)
        {
            BLL.UserBLL bll = new UserBLL();
            string UserId="";
            try
            {
                UserId = bll.CurrentUserId();
            }
            catch
            {

                Common.PageInput.ToLogin("");
               
            }

            BLL.MerchantBLL mbll = new MerchantBLL();
            DataSet ds = mbll.GetMerVsUser(" MerchantId='" + MerId + "' and UserId='" + UserId + "' ");

            if (ds.Tables[0].Rows.Count > 0)
            {

                return true;
            }
            else
            {
                return false;
            }



        }

        public void BindMerTypeSelectOpHtml()
        {
            BLL.MerchantBLL bll = new MerchantBLL();
            DataTable dt = bll.GetMerType(0).Tables[0];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["MerchantTypeId"] + "' >");
                w.Append(dr["MerchantTypeName"]);
                w.Append("</option>");
            }
            MerTypeSelectOpHtml = w.ToString();
        }
    }
}
