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

namespace BPage
{
    public class Bus:Common.BPageSetting2
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string para = ReStr("para");
                switch (para)
                {


                    case "UpdateBus":
                        UpdateBus();
                        break;

                    case "GetBusList":
                        GetBusList();
                        break;
                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
           context. Response.End();
        }

        private void UpdateBus()
        {
            string BusId = Common.PageInput.ReStr("BusId");
            decimal Lng = Common.PageInput.ReDecimal("Lng");
            decimal Lat = Common.PageInput.ReDecimal("Lat");

            StringBuilder s = new StringBuilder();

            StringBuilder w = new StringBuilder();

            try
            {

                s.Append(" update YYHD.dbo.BusInfo set  Lng='" + Lng + "' , Lat='" + Lat + "' where BusId='" + BusId + "' ");
                int i = DAL.DalComm.ExReInt(s.ToString());
                if (i > 0)
                {
                    ReTrue();
                }
                else
                {
                    throw new Exception("没有这辆车的信息!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        private void GetBusList()
        {
            DAL.BusInfoDAL dal = new DAL.BusInfoDAL();
            DataSet ds = dal.GetList("");
            DataTable dt = ds.Tables[0];

            string jsonArray = JsonHelper.ToJson(dt);
            ReDict.Add("jsonArray", jsonArray);
            ReTrue();

        }
    }
}
