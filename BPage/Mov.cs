using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;
using System.Text;
using System.Xml;
using Common;

namespace BPage
{
    public class Mov : Common.BPageSetting2
    {


        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string para = ReStr("para");
                switch (para)
                {



                    case "CaijiMove":

                        CaijiMove();
                        break;

                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();
        }



        private void CaijiMove()
        {
            Model.MovInfoModel model = new Model.MovInfoModel();
            BLL.MovBLL bll = new BLL.MovBLL();


            model = bll.GetMovInfoModel(ReStr("MovId"));

            model.MovId = ReStr("MovId");  //这是必须的
            model.CreateTime = DateTime.Now;
            model.MovBiaoQian = ReStr("MovBiaoQian", "");
            model.MovContent = ReStr("MovContent", "");
            model.PianChang = ReInt("PianChang", 0);
            model.MovImgUrl = ImgHelper.downOneImg(ReStr("MovImgUrl"), "/upload/httpDown/Mov/");
            model.MovTitle = ReStr("MovTitle");
            model.MovType = ReStr("MovType");
            model.ShangYingTime = ReTime("ShangYingTime", DateTime.Now);
            model.ShiGuangId = ReStr("MovId");
            model.MovClass = ReStr("MovClass", "");
            DAL.MovInfoDAL dal = new DAL.MovInfoDAL();

            DataTable dtEv = ReTable("MovEventArrayStr");

            if (dtEv != null)
            {
                bll.DelMovEvent(" MovId='" + model.MovId + "' ");
                foreach (DataRow dr in dtEv.Rows)
                {
                    Model.MovEventModel EvModel = new Model.MovEventModel();
                    DateTime MovEventBgTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd " + dr["MovEventBgTime"] + ":00"));

                    EvModel.MovEventBgTime = MovEventBgTime;
                    EvModel.MovEventId = TimeString.GetNow_ff();
                    EvModel.MovEventMemo = dr["MovEventMemo"].ToString();
                    EvModel.MovEventRePrice = Convert.ToDecimal(dr["MovEventRePrice"]);
                    EvModel.MovId = model.MovId;
                    bll.AddMovEvent(EvModel);
                }
            }
            bll.SaveMovInfo(model);

            ReTrue();
        }
    }
}
