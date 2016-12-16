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
using System.IO;
using Model;

namespace BPage
{
    public class Msg : Common.BPageSetting2
    {


        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string para = ReStr("para");
                switch (para)
                {

        

                    case "GetDevicePush":
                        GetDevicePush();
                        break;

                    case "RemoveDevicePush":
                        RemoveDevicePush();
                        break;

                    case "SaveDevicePush":
                        SaveDevicePush();
                        break;

                    case "CountMsg":
                        CountMsg();
                        break;

                    case "GetMsgInfo":
                        GetMsgInfo();
                        break;


                    case "GetMsgPageList":
                        GetMsgPageList();
                        break;

                    case "ReadOneMsg":
                        ReadOneMsg();
                        break;

                    case "ReadAllMsg":
                        ReadAllMsg();
                        break;

                    case "GetTopMsgList":
                        GetTopMsgList();
                        break;
                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();


        }

        private void GetDevicePush()
        {
            string DeviceId = ReStr("DeviceId", "");


            DataSet ds = DAL.DalComm.BackData(" SELECT * FROM DBMSG.dbo.DevicePushView with(nolock) where DeviceId='" + DeviceId + "' ");
            DataTable dt = ds.Tables[0];


            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReTrue();

        }

        private void RemoveDevicePush()
        {

            Model.DevicePushModel model = new DevicePushModel();
            model.DeviceId = ReStr("DeviceId", "");
            model.PushType = ReStr("PushType", "");
            if (model.DeviceId == "")
            {
                throw new Exception("DeviceId不能为空!");
            }

            if (model.PushType == "")
            {
                throw new Exception("PushType不能为空!");
            }

            DAL.DevicePushDAL dal = new DAL.DevicePushDAL();

            dal.DeleteList(" DeviceId='" + model.DeviceId + "' and  PushType='" + model.PushType + "' ");

            ReTrue();


             


        }

        private void SaveDevicePush()
        {
    
            Model.DevicePushModel model = new DevicePushModel();
            model.DeviceId = ReStr("DeviceId", "");
            model.PushType = ReStr("PushType", "");

            if (model.DeviceId == "")
            {
                throw new Exception("DeviceId不能为空!");
            }

            if (model.PushType == "")
            {
                throw new Exception("PushType不能为空!");
            }

            DAL.DevicePushDAL dal = new DAL.DevicePushDAL();
            dal.DeleteList(" DeviceId='" + model.DeviceId + "' and  PushType='" + model.PushType + "' ");
            dal.Add(model);
            ReTrue();


        }

        private void CountMsg()
        {
            string DeviceId = ReStr("DeviceId", "");

            if (DeviceId == "")
            {
                throw new Exception("编号不能为空!");
            }


            BLL.MsgBLL bll = new BLL.MsgBLL();
            DataSet ds = bll.CountMsg(DeviceId);
            DataTable dt = ds.Tables[0];


            ReDict.Add("Count", JsonHelper.ToJsonNo1(dt));
            ReDict.Add("ClassCount", JsonHelper.ToJson(ds.Tables[1]));



            ReTrue();

        }

        private void GetMsgInfo()
        {
            decimal MsgId = ReDecimal("MsgId", 0);
            string DeviceId = ReStr("DeviceId", "");
            if (DeviceId == "")
            {
                throw new Exception("DeviceId不能为空!");
            }
            bool read = ReBool("read", true);
            if (MsgId == 0)
            {
                throw new Exception("编号不能为空!");
            }
            BLL.MsgBLL bll = new BLL.MsgBLL();
            StringBuilder s = new StringBuilder();
            if (read)
            {
                bll.ReadOneMsg(MsgId, DeviceId);

            }
            s.Append(" SELECT * FROM DBMSG.dbo.MsgView WITH(NOLOCK) where  MsgId=" + MsgId + " ");


            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];

            ReDict.Add("info", JsonHelper.ToJsonNo1(dt));


            ReTrue();


        }




        private void GetMsgPageList()
        {
            int CurrentPage = ReInt("CurrentPage", 1);
            string col = ReStr("col", "*");
            int PageSize = ReInt("PageSize", 20);
            string Order = ReStr("Order", " MsgStatusId, MsgId DESC ");
            DAL.MsgDAL dal = new DAL.MsgDAL();
            StringBuilder s = new StringBuilder();

            string DeviceId = ReStr("DeviceId", "");

            if (DeviceId == "")
            {
                throw new Exception("用户编号不能为空!");
            }

            s.Append(" 1=1 ");

            s.Append(" and TargetDeviceId='" + DeviceId + "' ");

            DataSet ds = dal.GetPageList(s.ToString(), Order, CurrentPage, PageSize, col);
            RePage2(ds);
        }

        private void ReadOneMsg()
        {
            BLL.MsgBLL bll = new BLL.MsgBLL();
            string DeviceId = ReStr("DeviceId", "");
            decimal MsgId = ReDecimal("MsgId", 0);



            bll.ReadOneMsg(MsgId, DeviceId);


            //BLL.MsgBLL bll = new BLL.MsgBLL();
            //DataSet ds = bll.CountMsg(DeviceId);
            //DataTable dt = ds.Tables[0];


            //ReDict.Add("Count", JsonHelper.ToJsonNo1(dt));
            //ReDict.Add("ClassCount", JsonHelper.ToJson(ds.Tables[1]));
            ReTrue();
        }

        private void ReadAllMsg()
        {
            string DeviceId = ReStr("DeviceId", "");

            if (DeviceId == "")
            {
                throw new Exception("用户编号不能为0");
            }


            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE  DBMSG.dbo.Msg SET MsgStatusId=20 WHERE TargetDeviceId='" + DeviceId + "' ");

            s.Append(" UPDATE  DBMSG.dbo.Device SET MsgNum=0 WHERE DeviceId='" + DeviceId + "' ");

            s.Append(" INSERT INTO DBMSG.dbo.MsgReadLog ");
            s.Append(" ( DeviceId, MsgTextId, CreateTime ) ");
            s.Append(" SELECT m.TargetDeviceId,m.MsgTextId,GETDATE() FROM DBMSG.dbo.MsgView m WHERE m.TargetDeviceId='" + DeviceId + "' AND m.rd IS NULL and m.EndTime>Getdate()  ");

            DAL.DalComm.ExReInt(s.ToString());
            //BLL.MsgBLL bll = new BLL.MsgBLL();
            //DataSet ds = bll.CountMsg(DeviceId);
            //DataTable dt = ds.Tables[0];


            //ReDict.Add("Count", JsonHelper.ToJsonNo1(dt));
            //ReDict.Add("ClassCount", JsonHelper.ToJson(ds.Tables[1]));


            ReTrue();


        }

        private void GetTopMsgList()
        {
            StringBuilder s = new StringBuilder();
            int top = ReInt("top", 10);
            string TargetDeviceId = ReStr("TargetDeviceId", "");
            s.Append(" SELECT top " + top + "  * from DBMSG.DBO.msgView   ");
            s.Append(" where TargetDeviceId='" + TargetDeviceId + "' ");

            s.Append(" ORDER BY MsgStatusId, MsgId DESC ");


            s.Append(" SELECT MsgNum FROM   DBMSG.dbo.Device WHERE DeviceId='" + TargetDeviceId + "' ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dt = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2.Rows.Count == 0)
            {
                throw new Exception("没有编号为" + TargetDeviceId + "的用户!");
            }

            DataRow dr = dt2.Rows[0];
            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReDict2.Add("MsgNum", dr["MsgNum"].ToString());
            ReTrue();
        }



    }

}
