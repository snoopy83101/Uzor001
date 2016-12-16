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
using io.rong;
using LitJson;

namespace BLL
{
    public class MsgBLL
    {

        public void SendMsgToUser(string BranchId, Model.MsgTextModel MsgTextModel)
        {

            StringBuilder s = new StringBuilder();
            s.Append("  SELECT MerchantId FROM dbo.Branch WHERE BranchId='" + BranchId + "' ");

            s.Append(" SELECT DeviceId FROM DBMSG.dbo.DevicePush WHERE  PushType='" + MsgTextModel.MsgType + "' GROUP BY DeviceId  ");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtMer = ds.Tables[0];

            if (dtMer.Rows.Count != 1)
            {
                throw new Exception(" dtMer行数不能为" + dtMer.Rows.Count + " ");
            }

            DataRow drMer = dtMer.Rows[0];
            decimal MerId = decimal.Parse(drMer["MerchantId"].ToString());


            DataTable dtUser = ds.Tables[1];

            if (dtUser.Rows.Count == 0)
            {
                return;
            }

            List<string> UserList = new List<string>();
            foreach (DataRow drUser in dtUser.Rows)
            {

                UserList.Add(drUser["DeviceId"].ToString());

            }


            if (MsgTextModel.MsgTitle == "")
            {
                MsgTextModel.MsgTitle = Common.StringPlus.GetLeftStr(Common.StringPlus.OutHtmlText(MsgTextModel.MsgContent), 100);
            }


            MsgTextModel.MsgClassId = 100;//100为发送给后台用户的消息提醒

            SendMsgToDevice(UserList, MsgTextModel, "messager", MerId);

        }


        public void SendMsgToDevice(List<string> DeviceList, Model.MsgTextModel MsgTextModel, string SendDeviceId, decimal MerId)
        {



            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfigCache(MerId, 2000);
            DAL.MsgTextDAL dal = new DAL.MsgTextDAL();

            if (MsgTextModel.MsgClassId == 0)
            {
                throw new Exception("消息类型(MsgClassId)不能为0");
            }

            dal.Add(MsgTextModel);

            Model.MsgModel model = new Model.MsgModel();
            model.MsgTextId = MsgTextModel.MsgTextId;
            model.SendDeviceId = SendDeviceId;
            model.ZoneId = "1";
            model.MsgStatusId = 10;
             
        
            string reJson = "";




            DAL.MsgDAL MsgDal = new DAL.MsgDAL();
           
            
            foreach (string DeviceId in DeviceList)
            {

                model.TargetDeviceId = DeviceId;
                SaveMsg(model);



                StringBuilder s = new StringBuilder();
                
                s.Append(" DECLARE @MsgNum AS INT = (SELECT COUNT(0) FROM DBMSG.dbo.Msg WHERE TargetDeviceId='" + DeviceId + "' AND MsgStatusId=10) ");

                s.Append(" DECLARE @MsgTypeName AS VARCHAR(50) =(SELECT MsgTypeName FROM DBMSG.dbo.MsgType WHERE MsgTypeId='" + MsgTextModel.MsgType + "') ");

                s.Append(" UPDATE DBMSG.dbo.Device SET MsgNum=@MsgNum where DeviceId='" + DeviceId + "' ");
                s.Append(" SELECT @MsgNum as MsgNum, @MsgTypeName as MsgTypeName ");

                JsonData Extra = new JsonData();



                try
                {
                    Extra = JsonMapper.ToObject(MsgTextModel.Extra);
                }
                catch (Exception)
                {


                }
                finally
                {
                    DataSet ds = CountMsg(DeviceId) ;

                    DataTable dt = ds.Tables[0];

                    DataRow dr = dt.Rows[0];
                    Extra["MsgTypeId"] = MsgTextModel.MsgType;
                    Extra["MsgNum"] = int.Parse(dr["MsgNum"].ToString());
                   // Extra["MsgTypeName"] = dr["MsgTypeName"].ToString();

                }

                reJson = RongCloudServer.PublishMessage(MerConfig["RongAppKey"],
                   MerConfig["RongAppSecret"],
                   SendDeviceId,
                    DeviceId,
                   "RC:TxtMsg", //消息类型
                   " {\"content\":\"" + MsgTextModel.MsgTitle + "\",\"extra\":" + Extra.ToJson() + "}" //消息内容
                   , "");

            }

            //BLL.WxBLL wxBll = new WxBLL();
            //try
            //{
            //    wxBll.SendQyTextMsg(DeviceList, MsgTextModel.MsgTitle, 7);  //这个地方, 以后必须改为配置,这个7是写死的数字
            //}
            //catch (Exception)
            //{

            //    //如果微信推送不成功就算完吧.可能因为网络问题
            //}




        }

        public DataSet CountMsg(string DeviceId)
        {
            StringBuilder s = new StringBuilder();



            s.Append(" DELETE DBMSG.dbo.MsgClassRead WHERE DeviceId='" + DeviceId + "' ");  //删除所有类别统计


            s.Append("  INSERT INTO DBMSG.dbo.MsgClassRead ");   //新增类别统计
            s.Append(" ( DeviceId, MsgClassId, Num ) ");
            s.Append("  SELECT   '" + DeviceId + "',MsgClassId,COUNT(0) AS MsgNum ");
            s.Append("  FROM     DBMSG.dbo.MsgView ");
            s.Append(" WHERE    TargetDeviceId = '" + DeviceId + "' ");
            s.Append(" AND EndTime > GETDATE() ");
            s.Append(" AND rd IS NULL ");
            s.Append(" GROUP BY MsgClassId ");
            s.Append("  ");


            s.Append(" DECLARE @MsgNum AS int =(SELECT  ISNULL(  SUM(Num),0) AS MsgNum FROM  DBMSG.dbo.MsgClassRead WHERE DeviceId='" + DeviceId + "' )  "); //统计总数

            s.Append(" UPDATE  DBMSG.dbo.Device SET MsgNum=@MsgNum WHERE DeviceId='" + DeviceId + "' ");  //更改总数
            s.Append(" select @MsgNum as MsgNum ");  //返回总数
            s.Append(" SELECT * FROM DBMSG.dbo.MsgClassRead where  DeviceId='" + DeviceId + "' "); //返回分类统计


            DataSet ds = DAL.DalComm.BackData(s.ToString());

            return ds;
        }

        public void ReadOneMsg(decimal MsgId, string DeviceId)
        {
            if (DeviceId == "")
            {
                throw new Exception("用户编号不能为0");
            }
            if (MsgId == 0)
            {
                throw new Exception("MsgId不能为0!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(" UPDATE  DBMSG.dbo.Msg SET MsgStatusId=20  where MsgId=" + MsgId + " ");

            s.Append(" UPDATE  DBMSG.dbo.Device SET MsgNum=(SELECT COUNT(0) FROM DBMSG.dbo.Msg WHERE TargetDeviceId='" + DeviceId + "' AND MsgStatusId=10) WHERE DeviceId='" + DeviceId + "' ");


            s.Append(" INSERT INTO DBMSG.dbo.MsgReadLog ");
            s.Append(" ( DeviceId, MsgTextId, CreateTime ) ");
            s.Append(" SELECT m.TargetDeviceId,m.MsgTextId,GETDATE() FROM DBMSG.dbo.MsgView m WHERE m.TargetDeviceId='" + DeviceId + "' AND m.rd IS NULL and m.MsgId=" + MsgId + " and m.EndTime>Getdate()  ");
            DAL.DalComm.ExReInt(s.ToString());
        }

        public void SendMsgToDevice(int MsgClassId, string MsgContent, string MsgType, string TargetDeviceId, string SendDeviceId)
        {

            SendMsgToDevice(MsgClassId, MsgContent, MsgType, TargetDeviceId, SendDeviceId, "{}");
        }
        public void SendMsgToDevice(int MsgClassId, string MsgContent, string MsgType, string TargetDeviceId, string SendDeviceId, string Extra)
        {

            SendDeviceId = SendDeviceId.Trim();
            if (SendDeviceId == "")
            {
                SendDeviceId = "messager";
            }
            Model.MsgTextModel model = new Model.MsgTextModel();
            model.CreateTime = DateTime.Now;
            model.EndTime = DateTime.Now.AddDays(2);
            model.Extra = Extra;
            model.MsgContent = MsgContent;
            model.MsgTitle = Common.StringPlus.GetLeftStr(Common.StringPlus.OutHtmlText(MsgContent), 100);
            model.MsgType = MsgType;
            model.MsgClassId = MsgClassId;



            List<string> l = new List<string>();
            l.Add(TargetDeviceId);
            SendMsgToDevice(l, model, SendDeviceId, 1999);



        }



        public void SendMsgToDevice(Model.MsgTextModel model, string TargetDeviceId, string SendDeviceId)
        {


            List<string> l = new List<string>();
            l.Add(TargetDeviceId);
            SendMsgToDevice(l, model, SendDeviceId, 1999);

        }

        public void ReadMsg(decimal MsgId)
        {

            StringBuilder s = new StringBuilder();
            s.Append("   ");

        }


        public void SaveMsgText(Model.MsgTextModel model)
        {
            DAL.MsgTextDAL dal = new DAL.MsgTextDAL();
            if (model.MsgTextId == 0)
            {

                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }

        }

        public void SaveMsg(Model.MsgModel model)
        {
            DAL.MsgDAL dal = new DAL.MsgDAL();

            dal.DeleteList(" SendDeviceId='" + model.SendDeviceId + "' and TargetDeviceId='" + model.TargetDeviceId + "' and MsgTextId='" + model.MsgTextId + "'  ");

            dal.Add(model);
        }





    }
}
