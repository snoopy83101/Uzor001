using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using LitJson;
namespace Manage.test
{
    public partial class Address : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            StringBuilder s = new StringBuilder();

            s.Append(" SELECT * FROM dbo.Province ");
            s.Append(" SELECT * FROM dbo.City ");
            s.Append(" SELECT * FROM dbo.Area ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());


            DataTable dtProvince = ds.Tables[0];
            DataTable dtCity = ds.Tables[1];
            DataTable dtArea = ds.Tables[2];



            List<JsonData> jpList = new List<JsonData>();


            for (int i = 0; i < dtProvince.Rows.Count; i++)
            {
                DataRow drProvince = dtProvince.Rows[i];
                JsonData jp = new JsonData();
                jp["ProvinceId"] = drProvince["ProvinceId"].ToString();
                jp["ProvinceName"] = drProvince["ProvinceName"].ToString();
                jp["name"] = drProvince["ProvinceName"].ToString();
                jp["line"] = i;


                List<JsonData> jcList = new List<JsonData>();
                DataTable MyCtiy = Common.DataSetting.TableSelect(" ProvinceId='" + drProvince["ProvinceId"].ToString() + "' ", dtCity);
                for (int ii = 0; ii < MyCtiy.Rows.Count; ii++)
                {

                    DataRow drCity = MyCtiy.Rows[ii];
                    JsonData jc = new JsonData();
                    jc["CityId"] = drCity["CityId"].ToString();
                    jc["CityName"] = drCity["CityName"].ToString();
                    jc["name"] = drCity["CityName"].ToString();
                    jc["ProvinceId"] = drCity["ProvinceId"].ToString();
                    jc["line"] = ii;


                    List<JsonData> jaList = new List<JsonData>();

                    DataTable MyArea = Common.DataSetting.TableSelect(" CityId='" + drCity["CityId"] + "' ", dtArea);
                    for (int iii = 0; iii < MyArea.Rows.Count; iii++)
                    {

                        DataRow drArea = MyArea.Rows[iii];

                        JsonData ja = new JsonData();
                        ja["AreaId"] = drArea["AreaId"].ToString();
                        ja["AreaName"] = drArea["AreaName"].ToString();
                        ja["name"] = drArea["AreaName"].ToString();
                        ja["ProvinceId"] = drProvince["ProvinceId"].ToString();
                        ja["CityId"] = drArea["CityId"].ToString();
                        ja["line"] = iii;


                        jaList.Add(ja);

                    }

                    jc["sub"] = JsonMapper.ToObject(JsonDataListToString(jaList));
                    jcList.Add(jc);




                }
                jp["sub"] = JsonMapper.ToObject(JsonDataListToString(jcList));
                jpList.Add(jp);
            }


            Response.Write(JsonDataListToString(jpList));
            Response.End();

        }


        string JsonDataListToString(List<JsonData> list)
        {
            StringBuilder w = new StringBuilder();
            w.Append("[");
            for (int i = 0; i < list.Count; i++)
            {
                JsonData j = list[i];

                //   string l = j["AreaName"].ToString();
                //  j["test"] = "中文";
                string js = j.ToJson();
                w.Append(js);
                if (i == list.Count - 1)
                {


                }
                else
                {
                    w.Append(",");
                }

            }
            w.Append("]");
            return w.ToString();
        }
    }
}