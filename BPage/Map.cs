using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BPage
{
    public class Map :Common.BPageSetting2
    {


        public void ProcessRequest(HttpContext context)
        {
            string para = ReStr("para");
            switch (para)
            {




                case "GetMerData":
                    GetMerData();
                    break;

                case "GetMerListData":
                    GetMerListData();
                    break;

            }
            context.Response.End();
        }
        private void GetMerListData()
        {
            BLL.MerchantBLL bllMer = new BLL.MerchantBLL();

            DataTable dt = bllMer.GetMerList(" FlagInvalid=0 ");
            ReDict.Add("MerListData", Common.JsonHelper.ToJson(dt));
            ReTrue();

        }

        private void GetMerData()
        {
            try
            {
                BLL.TownBLL bll = new BLL.TownBLL();
                BLL.MerchantBLL bllMer = new BLL.MerchantBLL();
                DataTable dt = bll.GetTownList();
                string json = Common.JsonHelper.ToJson(dt);
                DataTable dtMerType = bllMer.GetMerTypeList("");
                string jsonMerType = Common.JsonHelper.ToJson(dtMerType);
                ReDict.Add("TownList", json);   //乡镇类别
                ReDict.Add("jsonMerType", jsonMerType);  //商家类别

                ReTrue();

            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }
        }
    }
}
