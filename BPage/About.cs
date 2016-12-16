using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BPage
{
    public class About : Common.BPageSetting2
    {

        public void ProcessRequest(HttpContext context)
        {
            string para = ReStr("para");
            switch (para)
            {
                case "GetAboutList":
                    GetAboutList();
                    break;

                case "GetAboutInfo":
                    GetAboutInfo();
                    break;
                case "SaveAbout":
                    SaveAbout();
                    break;


            }
        }


        private void SaveAbout()
        {
            try
            {

                Model.AboutModel model = new Model.AboutModel();
                model.AboutId = ReDecimal("AboutId");
                model.AboutContent = ReStrDeCode("AboutContent");
                model.AboutTitle = ReStr("AboutTitle");

                BLL.AboutBLL bll = new BLL.AboutBLL();
                bll.SaveAbout(model);

                ReTrue();

            }
            catch (Exception ex)
            {
                ReThrow(ex);

            }
        }

        private void GetAboutInfo()
        {
            try
            {


                decimal AboutId = ReDecimal("AboutId");

                BLL.AboutBLL bll = new BLL.AboutBLL();
                DataTable dt = bll.GetAbout(AboutId);
                string json = Common.JsonHelper.ToJsonNo1(dt);
                ReDict.Add("info", json);
                ReTrue();

            }
            catch (Exception ex)
            {
                ReThrow(ex);

            }
        }


        /// <summary>
        /// 获得列表
        /// </summary>
        public void GetAboutList()
        {

            try
            {
                string AboutType = ReStr("AboutType");
                int CurrentPage = ReInt("CurrentPage", 1);
                BLL.AboutBLL bll = new BLL.AboutBLL();
                StringBuilder s = new StringBuilder();
                s.Append(" 1=1 ");
                s.Append("  ");
                DataSet ds = bll.GetAboutPageList(s.ToString(), CurrentPage);
                RePage(ds);
            }
            catch (Exception ex)
            {

                ReThrow(ex);


            }



        }
    }
}
