using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Common;

namespace Manage.Merchant
{
    public partial class MerRole : System.Web.UI.Page
    {


        protected string MerRoleListHtml = "";

        protected string AppMenuListHtml = "";
        protected string MenuListHtml = "";
        protected string MsgTypeHtml = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            Model.CurrentMerModel cm = BLL.MerchantBLL.CurrentModel();
            BLL.MerchantBLL mBll = new BLL.MerchantBLL();


            StringBuilder s = new StringBuilder();
            s.Append(" select * from dbo.MerRole with(nolock) where MerId='" + cm.CurrentMerId + "' ");//0
            s.Append(" select * from CORE.dbo.MenuVsMerRole with(nolock)   ");//1
            s.Append(" select * from CORE.dbo.MenuInfo with(nolock) where AdminPower!=100 ");//2
            s.Append(" select * from CORE.dbo.AppMenuVsMerRole with(nolock) ");//3
            s.Append(" select * from CORE.dbo.AppMenuInfo with(nolock) ");//4

            s.Append(" SELECT * FROM  DBMSG.dbo.MsgType with(nolock) WHERE TargetLv BETWEEN 10 AND 20  ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());


            DataTable dtMerRole = ds.Tables[0];

            DataTable dtMerMenuVsMerRole = ds.Tables[1];
            DataTable dtMernu = ds.Tables[2];
            DataTable dtAppMenu = ds.Tables[4];
            DataTable dtAppMerMenuVsMerRole = ds.Tables[3];

            DataTable dtMsgType = ds.Tables[5];

            StringBuilder w = new StringBuilder();

            foreach (DataRow dr in dtMerRole.Rows)
            {
                w.Append(" <li  MerRoleId='" + dr["MerRoleId"] + "'>" + dr["MerRoleName"] + "</li>");
            }

            MerRoleListHtml = w.ToString();
            w.Clear();
            #region PC菜单开始
            DataTable dtParentMenu = Common.DataSetting.TableSelect(" ParentMenuId='' ", dtMernu);



            foreach (DataRow dr in dtParentMenu.Rows)
            {

                string MenuId = dr["MenuId"].ToString();
                string MenuName = dr["MenuName"].ToString();
                w.Append("<dl>");
                w.Append("<dt MenuId='" + MenuId + "' >");
                w.Append("<input class='cb_sel' type='checkbox' />");
                w.Append(MenuName);
                w.Append("</dt>");

                DataTable dtCldMenu = Common.DataSetting.TableSelect(" ParentMenuId='" + MenuId + "'  ", dtMernu);
                if (dtCldMenu.Rows.Count > 0)
                {
                    foreach (DataRow drCld in dtCldMenu.Rows)
                    {

                        string CldMenuId = drCld["MenuId"].ToString();
                        string CldMenuName = drCld["MenuName"].ToString();

                        w.Append("<dd MenuId='" + CldMenuId + "'>");
                        w.Append("<input class='cb_sel' type='checkbox' />");
                        w.Append(CldMenuName);
                        w.Append("</dd>");

                    }

                }



                w.Append("</dl>");

            }

            MenuListHtml = w.ToString();
            w.Clear();
            #endregion
       
            #region 手机菜单开始
            //手机菜单开始
            DataTable dtParentAppMenu = Common.DataSetting.TableSelect(" ParentAppMenuId='' ", dtAppMenu);

            foreach (DataRow dr in dtParentAppMenu.Rows)
            {

                string AppMenuId = dr["AppMenuId"].ToString();
                string AppMenuName = dr["AppMenuName"].ToString();
                w.Append("<dl>");
                w.Append("<dt AppMenuId='" + AppMenuId + "' >");
                w.Append("<input class='cb_sel' type='checkbox' />");
                w.Append(AppMenuName);
                w.Append("</dt>");

                DataTable dtCldAppMenu = Common.DataSetting.TableSelect(" ParentAppMenuId='" + AppMenuId + "'  ", dtAppMenu);
                if (dtCldAppMenu.Rows.Count > 0)
                {
                    foreach (DataRow drCld in dtCldAppMenu.Rows)
                    {

                        string CldAppMenuId = drCld["AppMenuId"].ToString();
                        string CldAppMenuName = drCld["AppMenuName"].ToString();

                        w.Append("<dd AppMenuId='" + CldAppMenuId + "'>");
                        w.Append("<input class='cb_sel' type='checkbox' />");
                        w.Append(CldAppMenuName);
                        w.Append("</dd>");

                    }

                }



                w.Append("</dl>");

            }


            AppMenuListHtml = w.ToString();
            #endregion



            #region 消息侦听开始


            w.Clear();
            if (dtMsgType.Rows.Count > 0)
            {

                foreach (DataRow drMsgType in dtMsgType.Rows)
                {

                    w.Append("<a class='a_MsgType' id='a_"+drMsgType["MsgTypeId"] +"'   MsgTypeId='"+drMsgType["MsgTypeId"] +"'  >");
                    w.Append(drMsgType["MsgTypeName"].ToString());
                    w.Append("</a>");
                }
                MsgTypeHtml = w.ToString();
            }
            #endregion


        }
    }
}