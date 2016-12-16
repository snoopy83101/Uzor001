using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Common;
using System.Transactions;

    public partial class HouseAjax : Common.BPageSetting
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string para = ReStr("para");
                switch (para)
                {
                    case "doInvalid":
                        doInvalid();
                        break;

                    case "GetMyHouse":
                        GetMyHouse();
                        break;
                    case "GetHouseDemandInfo":
                        GetHouseDemandInfo();
                        break;

                    case "GetHouseDemand":
                        GetHouseDemand();   //查询求租求购列表
                        break;

                    case "SaveHouseDemand":
                        SaveHouseDemand();
                        break;

                    case "GetHousePageList":
                        GetHousePageList();
                        break;


                    case "SaveHouse":
                        SaveHouse();  //保存一个房屋主体
                        break;

                    case "GetHouseInfo":
                        GetHouseInfo();  //获得一个房屋的主体数据(精简优化后)
                        break;

                    case "GetHouse":
                        GetHouse();   //获得一个房屋主体的数据
                        break;

                }
            }
            catch (Exception ex)
            {
                ReThrow(ex);
            }
            Response.End();

        }

        private void doInvalid()
        {
            bool Invalid = ReBool("Invalid", true);
            string HouseId = ReStr("HouseId","");
            DAL.DalComm.ExStr(" Update CORE.dbo.House set Invalid='" + Invalid + "'  where HouseId='" + HouseId + "' ");
            ReTrue();
        }

        private void GetMyHouse()
        {

            int CurrentPage = ReInt("CurrentPage");
            bool Invalid = ReBool("Invalid", false);
            BLL.HouseBLL bll = new BLL.HouseBLL();
            DataSet ds = bll.GetHousePageList(" CreateUser='" + Common.CookieSings.GetCurrentUserId() + "' and Invalid='" + Invalid.ToString() + "' order by createTime desc ", CurrentPage);
            RePage(ds);
        }

        private void GetHouseDemandInfo()
        {
            string HouseDemandId = ReStr("HouseDemandId");

            BLL.HouseBLL bll = new BLL.HouseBLL();

            DataSet ds = bll.GetHouseDemandInfoById(HouseDemandId);
            DataTable dt = ds.Tables[0];
            string HdJson = JsonHelper.ToJsonNo1(dt);
            ReDict.Add("HdJson", HdJson);
            ReTrue();


        }

        private void GetHouseDemand()
        {

            BLL.HouseBLL bll = new BLL.HouseBLL();

            int HouseDemandTypeId = ReInt("HouseDemandTypeId");
            string Price = ReStr("Price","all");
            string Rent = ReStr("Rent","all");
            int TownId = ReInt("TownId", 0);
            int c = ReInt("CurrentPage");


            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");

            switch (HouseDemandTypeId)
            {

                case 1:  //查询求租
                    if (Rent == "all")
                    {
                        break;
                    }

                    string[] r = Rent.Split('_');

                    decimal begin = decimal.Parse(r[0]);
                    decimal end = decimal.Parse(r[1]);
                    s.Append(" and  (BeginPrice >='" + begin + "' and EndPrice<='" + end + "') ");


                    break;
                case 2:  //查询求购
                    if (Price == "all")
                    {
                        break;
                    }

                    string[] r2 = Price.Split('_');

                    decimal begin2 = decimal.Parse(r2[0]);
                    decimal end2 = decimal.Parse(r2[1]);
                    s.Append(" and  (BeginPrice >='" + begin2 + "' and EndPrice<='" + end2 + "') ");
                    break;

                default:
                    break;
            }

            s.Append(" and HouseDemandTypeId='" + HouseDemandTypeId + "' ");

            if (TownId != 0)
            {

                s.Append(" and TownId='" + TownId + "' ");

            }
            s.Append(" order by createTime desc ");

            DataSet ds = bll.GetHouseDemandPageList(s.ToString(), c);
            RePage(ds);

        }

        private void SaveHouseDemand()
        {


            BLL.UserBLL ubll = new BLL.UserBLL();
            Model.HouseDemandModel model = new Model.HouseDemandModel();
            model.HouseDemandId = ReStr("HouseDemandId","");
            model.HouseDemandTitle = ReStr("HouseDemandTitle","");
            model.HouseDemandTypeId = ReInt("HouseDemandTypeId",0);
            model.BeginPrice = ReDecimal("BeginPrice",0);
            model.EndPrice = ReDecimal("EndPrice",0);
            model.Hshi = ReInt("Hshi",0);
            model.Hting = ReInt("Hting",0);
            model.Hchu = ReInt("Hchu",0);
            model.Hwei = ReInt("Hwei",0);
            model.Hyangtai = ReInt("Hyangtai",0);
            model.HouseDemandMemo = ReStr("HouseDemandMemo");
            model.ContactName = ReStr("ContactName");
            model.ContactTell = ReStr("ContactTell");
            model.ContactPhone = ReStr("ContactPhone");
            model.ContactEmail = ReStr("ContactEmail");
            model.ContactQQ = ReStr("ContactQQ");
            model.CommunityTitle = ReStr("CommunityTitle");
            model.CommunityId = ReInt("CommunityId",0);
            model.TownId = ReInt("TownId",0);

            model.CreateUser = ubll.CurrentUserId();


            BLL.HouseBLL bll = new BLL.HouseBLL();
            bll.SaveHouseDemand(model);

            ReDict2.Add("HouseDemandId", model.HouseDemandId);

            ReTrue();
        }

        private void GetHousePageList()
        {

            int HouseModelId = ReInt("HouseModelId", 0);
            int PropertyTypeId = ReInt("PropertyTypeId", 0);
            int DecorationId = ReInt("DecorationId", 0);
            string PingFang = ReStr("PingFang", "all");
            int ChaoXiangId = ReInt("ChaoXiangId", 0);
            string Rent = ReStr("Rent", "all");
            string Price = ReStr("Price", "all");
            string IsAgency = ReStr("IsAgency", "all");
            int HouseTypeId = ReInt("HouseTypeId", 0);
            decimal TownId = ReInt("TownId", 0);
            string inputStr = ReStr("inputStr", "");
            bool Invalid = ReBool("Invalid", false);
            StringBuilder s = new StringBuilder();
            s.Append(" 1 =1 ");


            s.Append(" and Invalid='" + Invalid + "' ");
            if (TownId != 0)
            {

                s.Append(" and TownId='" + TownId + "' ");
            }
            if (inputStr.Trim() != "")
            {
                s.Append(" and HouseTitle like '%" + inputStr + "%' ");

            }


            if (HouseTypeId != 0)
            {

                s.Append(" and HouseTypeId='" + HouseTypeId + "' ");
            }

            if (HouseModelId != 0)  //房屋产权
            {
                s.Append(" and HouseModelId='" + HouseModelId + "' ");

            }
            if (PropertyTypeId != 0)  //物业类型
            {

                s.Append(" and PropertyTypeId='" + PropertyTypeId + "' ");
            }

            if (DecorationId != 0) //装修程度
            {
                s.Append(" and DecorationId='" + DecorationId + "' ");
            }

            if (PingFang.ToLower().Trim() != "all") //平方
            {
                string[] pingfang = PingFang.Split('_');

                decimal begin = decimal.Parse(pingfang[0]);
                decimal end = decimal.Parse(pingfang[1]);

                s.Append(" and (PingFang >= '" + begin + "' and PingFang <= '" + end + "' )  ");

            }

            if (ChaoXiangId != 0)  //朝向
            {

                s.Append(" and ChaoXiangId='" + ChaoXiangId + "' ");

            }
            if (Rent.ToLower().Trim() != "all")  //租金
            {
                string[] r = Rent.Split('_');

                decimal begin = decimal.Parse(r[0]);
                decimal end = decimal.Parse(r[1]);
                s.Append(" and  (Rent >='" + begin + "' and Rent<='" + end + "') ");
            }

            if (Price.ToLower().Trim() != "all")  //售价
            {
                string[] P = Price.Split('_');

                decimal begin = decimal.Parse(P[0]);
                decimal end = decimal.Parse(P[1]);
                s.Append(" and  (Price >='" + begin + "' and Price<='" + end + "') ");
            }

            if (IsAgency.ToLower().Trim() != "all")
            {

                s.Append(" and IsAgency='" + IsAgency + "' ");

            }
            s.Append(" order by  RecommendLv desc,  createTime desc ");

            int CurrentPage = ReInt("CurrentPage");

            BLL.HouseBLL bll = new BLL.HouseBLL();
            DataSet ds = bll.GetHousePageList(s.ToString(), CurrentPage);

            RePage(ds);


        }

        private void GetHouse()
        {
            string HouseId = ReStr("HouseId");
            BLL.HouseBLL bll = new BLL.HouseBLL();
            DataSet ds = bll.GetHouseByHouseId(HouseId);
            DataTable dt = ds.Tables[0];
            string HouseJson = JsonHelper.ToJsonNo1(dt);
            DataTable dtImg = ds.Tables[1];
            string ImgJsonArray = JsonHelper.ToJson(dtImg);
            ReDict.Add("HouseJson", HouseJson);
            ReDict.Add("ImgJsonArray", ImgJsonArray);
            ReTrue();
        }


        private void GetHouseInfo()  //经过优化后的ajax
        {
            string HouseId = ReStr("HouseId");
            BLL.HouseBLL bll = new BLL.HouseBLL();
            DataSet ds = bll.GetHouseByHouseId(HouseId);
            ReDict.Add("HouseJson", JsonHelper.ToJson(ds));
            ReTrue();

        }

        private void SaveHouse()
        {
            Model.HouseModel model = new Model.HouseModel();
            BLL.HouseBLL bll = new BLL.HouseBLL();
            BLL.UserBLL ubll = new BLL.UserBLL();



            model.HouseId = ReStr("HouseId");
            if (model.HouseId != "")
            {

                model = bll.GetHouseModel(model.HouseId);
                model.CreateTime = DateTime.Now;
            }
            else
            {
                model.CreateUser = ubll.CurrentUserId();   //如果是新增, 则给CreateUser赋值
            }

            model.HouseTitle = ReStr("HouseTitle","");
            model.HouseAddress = ReStr("HouseAddress","");
            model.Hshi = ReInt("Hshi",0);
            model.Hting = ReInt("Hting",0);
            model.Hchu = ReInt("Hchu",0);
            model.Hwei = ReInt("Hwei",0);
            model.Hyangtai = ReInt("Hyangtai",0);
            model.HouseModelId = ReInt("HouseModelId",0);
            model.DecorationId = ReInt("DecorationId",0);
            model.PropertyTypeId = ReInt("PropertyTypeId",0);
            model.HouseClassId = ReInt("HouseClassId",0);
            model.Floor = ReInt("Floor",0);
            model.FloorALL = ReInt("FloorALL",0);
            model.HouseTypeId = ReInt("HouseTypeId",0);
            model.Rent = ReDecimal("Rent");
            model.Price = ReDecimal("Price");
            model.IsAgency = ReBool("IsAgency");
            model.Device = ReStr("Device");
            model.Memo = ReStr("Memo","");
            model.CreateTime = DateTime.Now;

            model.PingFang = ReDecimal("PingFang");
            model.ChaoXiangId = ReInt("ChaoXiangId",0);
            model.HouseImgId = ReStr("HouseImgId","");
            model.ContactName = ReStr("ContactName");
            model.ContactTell = ReStr("ContactTell");
            model.ContactPhone = ReStr("ContactPhone");
            model.ContactEmail = ReStr("ContactEmail");
            model.ContactQQ = ReStr("ContactQQ");
            model.CommunityTitle = ReStr("CommunityTitle","");
            model.CommunityId = ReInt("CommunityId", 0);
            model.TownId = ReDecimal("TownId",0);
            model.HouseLat = ReDecimal("HouseLat");
            model.HouseLng = ReDecimal("HouseLng");
            model.RecommendLv = ReInt("RecommendLv", 0);
            DataTable dt = DataSetting.CXmlToDatatTable(ReStr("HouseAllImgHtmlStr"));

            #region 事务开启
            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
            #endregion
                bll.SaveHouse(model);
             
                bll.DeleteHouseByHouseId(model.HouseId);
                if (dt != null)
                {
                    //有图片上传
                    Model.HouseVsImgModel HvI = new Model.HouseVsImgModel();

                    foreach (DataRow dr in dt.Rows)
                    {
                        HvI.HouseId = model.HouseId;
                        HvI.ImgId = dr["ImgId"].ToString();
                        HvI.Memo = "UE";
                        HvI.VsType = "房源图片";
                        bll.AddHouseVsImg(HvI);
                    }

                }

                //推广开始

                if (model.RecommendLv > 0)
                {

                }


                #region 事务关闭
                transactionScope.Complete();


            }
                #endregion
            ReDict2.Add("HouseId", model.HouseId);
            ReTrue();
        }
    }
