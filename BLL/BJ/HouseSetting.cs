using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Common;
namespace BLL.BJ
{
    public class HouseSetting:UserSetting
    {
        public DataSet DsHouseSetting = null;
        BLL.HouseBLL bll = new HouseBLL();
        public HouseSetting()
        {

            DsHouseSetting = bll.GetHouseSetting();
        }

        /// <summary>
        /// 房屋设备
        /// </summary>
        /// <returns></returns>
        protected string deviceListHtml()
        {
            StringBuilder w = new StringBuilder();
            #region 项目
            w.Append("<li>");
            w.Append("床铺");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("厨具");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("家具");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("有线电视");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("宽带网");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("防盗门");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("电话");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("热水器");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("电视机");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("空调");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("洗衣机");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("地板");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("冰箱");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("管道煤气");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("液化煤气");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("车库");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("院子");
            w.Append("</li>");
            w.Append("<li>");
            w.Append("电梯");
            w.Append("</li>");
            #endregion
          
            return w.ToString();
        }


        /// <summary>
        /// 物业类型
        /// </summary>
        /// <returns></returns>
        protected string PropetyTypeOpHtml()
        {
            DataTable dt = DsHouseSetting.Tables[0];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["PropertyTypeId"] + "' >");
                w.Append(dr["PropertyTypeName"]);
                w.Append("</option>");
               
            }
            return w.ToString();
        
        }


        /// <summary>
        /// 装修程度
        /// </summary>
        /// <returns></returns>
        protected string DecorationOpHtml()
        {
            DataTable dt = DsHouseSetting.Tables[1];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["DecorationId"] + "' >");
                w.Append(dr["DecorationName"]);
                w.Append("</option>");

            }
            return w.ToString();
           
        }


        /// <summary>
        /// 房屋朝向
        /// </summary>
        /// <returns></returns>
        protected string ChaoXiangOpHtml()
        {
            DataTable dt = DsHouseSetting.Tables[2];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["ChaoXiangId"] + "' >");
                w.Append(dr["ChaoXiangName"]);
                w.Append("</option>");

            }
            return w.ToString();

        }

        protected string HouseModelOpHtml()
        {
            DataTable dt = DsHouseSetting.Tables[3];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["HouseModelId"] + "' >");
                w.Append(dr["HouseModelName"]);
                w.Append("</option>");

            }
            return w.ToString();

        }
    
    }
}
