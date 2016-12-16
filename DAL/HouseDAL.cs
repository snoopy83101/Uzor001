using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Model;
using DBTools;
using Common;
namespace DAL
{
    //House
    public partial class HouseDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion
        /// <summary>
        /// 检查是否存在
        /// </summary>
        public int ExInt(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) ");
            strSql.Append(" FROM House ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            int i = int.Parse(helper.ExecuteSqlScalar(strSql.ToString()));
            return i;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HouseModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into House(");
            strSql.Append("HouseId,DecorationId,PropertyTypeId,HouseClassId,Floor,FloorALL,HouseTypeId,Rent,Price,IsAgency,Device,HouseTitle,Memo,CreateTime,CreateUser,PingFang,ChaoXiangId,HouseImgId,ContactName,ContactTell,ContactPhone,ContactEmail,HouseAddress,ContactQQ,CommunityTitle,CommunityId,TownId,MerchantId,HouseLng,HouseLat,RecommendLv,Hshi,Hting,Hchu,Hwei,Hyangtai,HouseModelId");
            strSql.Append(") values (");
            strSql.Append("@HouseId,@DecorationId,@PropertyTypeId,@HouseClassId,@Floor,@FloorALL,@HouseTypeId,@Rent,@Price,@IsAgency,@Device,@HouseTitle,@Memo,@CreateTime,@CreateUser,@PingFang,@ChaoXiangId,@HouseImgId,@ContactName,@ContactTell,@ContactPhone,@ContactEmail,@HouseAddress,@ContactQQ,@CommunityTitle,@CommunityId,@TownId,@MerchantId,@HouseLng,@HouseLat,@RecommendLv,@Hshi,@Hting,@Hchu,@Hwei,@Hyangtai,@HouseModelId");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HouseId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DecorationId", SqlDbType.Int,4) ,            
                        new SqlParameter("@PropertyTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Floor", SqlDbType.Int,4) ,            
                        new SqlParameter("@FloorALL", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Rent", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@IsAgency", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Device", SqlDbType.VarChar,300) ,            
                        new SqlParameter("@HouseTitle", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@Memo", SqlDbType.NVarChar,2000) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PingFang", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChaoXiangId", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactPhone", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactEmail", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@HouseAddress", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ContactQQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityTitle", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HouseLng", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HouseLat", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hshi", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hting", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hchu", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hwei", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hyangtai", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseModelId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.HouseId;
            parameters[1].Value = model.DecorationId;
            parameters[2].Value = model.PropertyTypeId;
            parameters[3].Value = model.HouseClassId;
            parameters[4].Value = model.Floor;
            parameters[5].Value = model.FloorALL;
            parameters[6].Value = model.HouseTypeId;
            parameters[7].Value = model.Rent;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.IsAgency;
            parameters[10].Value = model.Device;
            parameters[11].Value = model.HouseTitle;
            parameters[12].Value = model.Memo;
            parameters[13].Value = model.CreateTime;
            parameters[14].Value = model.CreateUser;
            parameters[15].Value = model.PingFang;
            parameters[16].Value = model.ChaoXiangId;
            parameters[17].Value = model.HouseImgId;
            parameters[18].Value = model.ContactName;
            parameters[19].Value = model.ContactTell;
            parameters[20].Value = model.ContactPhone;
            parameters[21].Value = model.ContactEmail;
            parameters[22].Value = model.HouseAddress;
            parameters[23].Value = model.ContactQQ;
            parameters[24].Value = model.CommunityTitle;
            parameters[25].Value = model.CommunityId;
            parameters[26].Value = model.TownId;
            parameters[27].Value = model.MerchantId;
            parameters[28].Value = model.HouseLng;
            parameters[29].Value = model.HouseLat;
            parameters[30].Value = model.RecommendLv;
            parameters[31].Value = model.Hshi;
            parameters[32].Value = model.Hting;
            parameters[33].Value = model.Hchu;
            parameters[34].Value = model.Hwei;
            parameters[35].Value = model.Hyangtai;
            parameters[36].Value = model.HouseModelId;                        
	

            bool result = false;
            try
            {
                helper.ExecSqlReInt(strSql.ToString(), parameters);
                result = true;
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            finally
            {

            }
            return result;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HouseModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update House set ");

            strSql.Append(" HouseId = @HouseId , ");
            strSql.Append(" DecorationId = @DecorationId , ");
            strSql.Append(" PropertyTypeId = @PropertyTypeId , ");
            strSql.Append(" HouseClassId = @HouseClassId , ");
            strSql.Append(" Floor = @Floor , ");
            strSql.Append(" FloorALL = @FloorALL , ");
            strSql.Append(" HouseTypeId = @HouseTypeId , ");
            strSql.Append(" Rent = @Rent , ");
            strSql.Append(" Price = @Price , ");
            strSql.Append(" IsAgency = @IsAgency , ");
            strSql.Append(" Device = @Device , ");
            strSql.Append(" HouseTitle = @HouseTitle , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" CreateTime = @CreateTime , ");
          //  strSql.Append(" CreateUser = @CreateUser , ");这是无论如何都不能修改的
            strSql.Append(" PingFang = @PingFang , ");
            strSql.Append(" ChaoXiangId = @ChaoXiangId , ");
            strSql.Append(" HouseImgId = @HouseImgId , ");
            strSql.Append(" ContactName = @ContactName , ");
            strSql.Append(" ContactTell = @ContactTell , ");
            strSql.Append(" ContactPhone = @ContactPhone , ");
            strSql.Append(" ContactEmail = @ContactEmail , ");
            strSql.Append(" HouseAddress = @HouseAddress , ");
            strSql.Append(" ContactQQ = @ContactQQ , ");
            strSql.Append(" CommunityTitle = @CommunityTitle , ");
            strSql.Append(" CommunityId = @CommunityId , ");
            strSql.Append(" TownId = @TownId , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" HouseLng = @HouseLng , ");
            strSql.Append(" HouseLat = @HouseLat , ");
            strSql.Append(" RecommendLv = @RecommendLv , ");
            strSql.Append(" Hshi = @Hshi , ");
            strSql.Append(" Hting = @Hting , ");
            strSql.Append(" Hchu = @Hchu , ");
            strSql.Append(" Hwei = @Hwei , ");
            strSql.Append(" Hyangtai = @Hyangtai , ");
            strSql.Append(" HouseModelId = @HouseModelId  ");
            strSql.Append(" where HouseId=@HouseId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HouseId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DecorationId", SqlDbType.Int,4) ,            
                        new SqlParameter("@PropertyTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Floor", SqlDbType.Int,4) ,            
                        new SqlParameter("@FloorALL", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Rent", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@IsAgency", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Device", SqlDbType.VarChar,300) ,            
                        new SqlParameter("@HouseTitle", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@Memo", SqlDbType.NVarChar,2000) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PingFang", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChaoXiangId", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactPhone", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactEmail", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@HouseAddress", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ContactQQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityTitle", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HouseLng", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HouseLat", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hshi", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hting", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hchu", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hwei", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hyangtai", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseModelId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.HouseId;
            parameters[1].Value = model.DecorationId;
            parameters[2].Value = model.PropertyTypeId;
            parameters[3].Value = model.HouseClassId;
            parameters[4].Value = model.Floor;
            parameters[5].Value = model.FloorALL;
            parameters[6].Value = model.HouseTypeId;
            parameters[7].Value = model.Rent;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.IsAgency;
            parameters[10].Value = model.Device;
            parameters[11].Value = model.HouseTitle;
            parameters[12].Value = model.Memo;
            parameters[13].Value = model.CreateTime;
            parameters[14].Value = model.CreateUser;
            parameters[15].Value = model.PingFang;
            parameters[16].Value = model.ChaoXiangId;
            parameters[17].Value = model.HouseImgId;
            parameters[18].Value = model.ContactName;
            parameters[19].Value = model.ContactTell;
            parameters[20].Value = model.ContactPhone;
            parameters[21].Value = model.ContactEmail;
            parameters[22].Value = model.HouseAddress;
            parameters[23].Value = model.ContactQQ;
            parameters[24].Value = model.CommunityTitle;
            parameters[25].Value = model.CommunityId;
            parameters[26].Value = model.TownId;
            parameters[27].Value = model.MerchantId;
            parameters[28].Value = model.HouseLng;
            parameters[29].Value = model.HouseLat;
            parameters[30].Value = model.RecommendLv;
            parameters[31].Value = model.Hshi;
            parameters[32].Value = model.Hting;
            parameters[33].Value = model.Hchu;
            parameters[34].Value = model.Hwei;
            parameters[35].Value = model.Hyangtai;
            parameters[36].Value = model.HouseModelId; 
            try
            {//异常处理
                reCount = this.helper.ExecSqlReInt(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            if (reCount <= 0)
            {
                reValue = false;
            }
            return reValue;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HouseModel GetModel(string HouseId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HouseId, DecorationId, PropertyTypeId, HouseClassId, Floor, FloorALL, HouseTypeId, Rent, Price, IsAgency, Device, HouseTitle, Memo, CreateTime, CreateUser, PingFang, ChaoXiangId, HouseImgId, ContactName, ContactTell, ContactPhone, ContactEmail, HouseAddress, ContactQQ, CommunityTitle, CommunityId, TownId, MerchantId, HouseLng, HouseLat, Hshi, Hting, Hchu, Hwei, Hyangtai, HouseModelId  ");
            strSql.Append("  from House  WITH(NOLOCK)  ");
            strSql.Append(" where HouseId=@HouseId ");
            SqlParameter[] parameters = {
					new SqlParameter("@HouseId", SqlDbType.VarChar,50)			};
            parameters[0].Value = HouseId;


            HouseModel model = new HouseModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.HouseId = ds.Tables[0].Rows[0]["HouseId"].ToString();
                if (ds.Tables[0].Rows[0]["DecorationId"].ToString() != "")
                {
                    model.DecorationId = int.Parse(ds.Tables[0].Rows[0]["DecorationId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PropertyTypeId"].ToString() != "")
                {
                    model.PropertyTypeId = int.Parse(ds.Tables[0].Rows[0]["PropertyTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HouseClassId"].ToString() != "")
                {
                    model.HouseClassId = int.Parse(ds.Tables[0].Rows[0]["HouseClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Floor"].ToString() != "")
                {
                    model.Floor = int.Parse(ds.Tables[0].Rows[0]["Floor"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FloorALL"].ToString() != "")
                {
                    model.FloorALL = int.Parse(ds.Tables[0].Rows[0]["FloorALL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HouseTypeId"].ToString() != "")
                {
                    model.HouseTypeId = int.Parse(ds.Tables[0].Rows[0]["HouseTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rent"].ToString() != "")
                {
                    model.Rent = decimal.Parse(ds.Tables[0].Rows[0]["Rent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsAgency"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsAgency"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsAgency"].ToString().ToLower() == "true"))
                    {
                        model.IsAgency = true;
                    }
                    else
                    {
                        model.IsAgency = false;
                    }
                }
                model.Device = ds.Tables[0].Rows[0]["Device"].ToString();
                model.HouseTitle = ds.Tables[0].Rows[0]["HouseTitle"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                if (ds.Tables[0].Rows[0]["PingFang"].ToString() != "")
                {
                    model.PingFang = decimal.Parse(ds.Tables[0].Rows[0]["PingFang"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChaoXiangId"].ToString() != "")
                {
                    model.ChaoXiangId = int.Parse(ds.Tables[0].Rows[0]["ChaoXiangId"].ToString());
                }
                model.HouseImgId = ds.Tables[0].Rows[0]["HouseImgId"].ToString();
                model.ContactName = ds.Tables[0].Rows[0]["ContactName"].ToString();
                model.ContactTell = ds.Tables[0].Rows[0]["ContactTell"].ToString();
                model.ContactPhone = ds.Tables[0].Rows[0]["ContactPhone"].ToString();
                model.ContactEmail = ds.Tables[0].Rows[0]["ContactEmail"].ToString();
                model.HouseAddress = ds.Tables[0].Rows[0]["HouseAddress"].ToString();
                model.ContactQQ = ds.Tables[0].Rows[0]["ContactQQ"].ToString();
                model.CommunityTitle = ds.Tables[0].Rows[0]["CommunityTitle"].ToString();
                if (ds.Tables[0].Rows[0]["CommunityId"].ToString() != "")
                {
                    model.CommunityId = int.Parse(ds.Tables[0].Rows[0]["CommunityId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TownId"].ToString() != "")
                {
                    model.TownId = decimal.Parse(ds.Tables[0].Rows[0]["TownId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HouseLng"].ToString() != "")
                {
                    model.HouseLng = decimal.Parse(ds.Tables[0].Rows[0]["HouseLng"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HouseLat"].ToString() != "")
                {
                    model.HouseLat = decimal.Parse(ds.Tables[0].Rows[0]["HouseLat"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hshi"].ToString() != "")
                {
                    model.Hshi = int.Parse(ds.Tables[0].Rows[0]["Hshi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hting"].ToString() != "")
                {
                    model.Hting = int.Parse(ds.Tables[0].Rows[0]["Hting"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hchu"].ToString() != "")
                {
                    model.Hchu = int.Parse(ds.Tables[0].Rows[0]["Hchu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hwei"].ToString() != "")
                {
                    model.Hwei = int.Parse(ds.Tables[0].Rows[0]["Hwei"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hyangtai"].ToString() != "")
                {
                    model.Hyangtai = int.Parse(ds.Tables[0].Rows[0]["Hyangtai"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HouseModelId"].ToString() != "")
                {
                    model.HouseModelId = int.Parse(ds.Tables[0].Rows[0]["HouseModelId"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from House ");
            strSql.Append(" where " + strWhere);
            try
            {//异常处理
                reCount = this.helper.ExecSqlReInt(strSql.ToString());
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            if (reCount <= 0)
            {
                reValue = false;
            }
            return reValue;
        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000  ");

            strSql.Append(@" HouseId ,
        HouseTitle ,
        HouseAddress ,
        Hshi ,
        Hting ,
        Hchu ,
        Hwei ,
        Hyangtai ,
        HouseModelId ,
        DecorationId ,
        PropertyTypeId ,
        HouseClassId ,
        Floor ,
        FloorALL ,
        HouseTypeId ,
        Rent ,
        Price ,
        IsAgency ,
        Device ,

        CreateTime ,
        CreateUser ,
        PingFang ,
        ChaoXiangId ,
        HouseImgId ,
        ContactName ,
        ContactTell ,
        ContactPhone ,
        ContactEmail ,
        ContactQQ ,
        CommunityTitle ,
        CommunityId ,
        TownId ,
        MerchantId ,
        HouseLng ,
        HouseLat ,
        RecommendLv ,
        HouseModelName ,
        PropertyTypeName ,
        DecorationName ,
        ChaoXiangName ,
        ImgUrl ,
        TownName  ");

            strSql.Append(" FROM dbo.HouseView  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList2(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1000  ");
            strSql.Append(@" HouseId ,
        HouseTitle ,
        HouseAddress ,
        Hshi ,
        Hting ,
        Hchu ,
        Hwei ,
        Hyangtai ,
        HouseModelId ,
        DecorationId ,
        PropertyTypeId ,
        HouseClassId ,
        Floor ,
        FloorALL ,
        HouseTypeId ,
        Rent ,
        Price ,
        IsAgency ,
        Device ,

        CreateTime ,
        CreateUser ,
        PingFang ,
        ChaoXiangId ,
        HouseImgId ,
        ContactName ,
        ContactTell ,
        ContactPhone ,
        ContactEmail ,
        ContactQQ ,
        CommunityTitle ,
        CommunityId ,
        TownId ,
        MerchantId ,
        HouseLng ,
        HouseLat ,
        RecommendLv ,
        HouseModelName ,
        PropertyTypeName ,
        DecorationName ,
        ChaoXiangName ,
        ImgUrl ,
        TownName  ");


            strSql.Append(" FROM dbo.HouseView  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }


        /// <summary>
        /// 获得一条房源信息的数据集
        /// </summary>
        /// <param name="HouseId"></param>
        /// <returns></returns>
        public DataSet GetHouseListByHouseId(string HouseId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM HouseView  WITH(NOLOCK)  where HouseId='" + HouseId + "' ");
            strSql.Append(" select * from dbo.HouseVsImgView  WITH(NOLOCK)  where HouseId='" + HouseId + "' ");
            DataSet ds = helper.ExecSqlReDs(strSql.ToString());

            ds.Tables[0].TableName = "HouseInfo";
            ds.Tables[1].TableName = "HouseVsImg";
            return ds;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM House ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return helper.ExecSqlReDs(strSql.ToString());
        }




        /// <summary>
        /// 获得房屋属性列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetHouseSetting()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM dbo.PropertyType  WITH(NOLOCK)  ORDER BY OrderNo ");
            strSql.Append(" SELECT * FROM dbo.Decoration  WITH(NOLOCK)  ORDER BY OrderNo ");
            strSql.Append(" SELECT * FROM dbo.ChaoXiang  WITH(NOLOCK)  ORDER BY OrderNo ");
            strSql.Append(" SELECT * FROM dbo.HouseModel  WITH(NOLOCK)  ORDER BY OrderNo ");

            return helper.ExecSqlReDs(strSql.ToString());

        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM House ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

