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
    //Product
    public partial class ProductDAL
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
            strSql.Append(" FROM  CORE.dbo.Product ");
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
        public bool Add(ProductModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.Product (");
            strSql.Append("ProId,RecommendLv,HotLv,ClickLv,BuyLv,StreetId,FlagInvalid,OnLineLv,Units,IndexImgId,ProductImgId,ProName,RePrice,RePrice2,RePrice3,CommentCount,AuthorId,Spec,ProCode,Status,SendPara,Attr,MerchantId,PinPaiId,ProNum,GetJiFenNum,InheritPeiSongType,InheritProTeXing,ProTeXing,IsInfiniteNum,AllowPriceInterface,AllowProNumInterface,MinQuantity,CreateTime,Zl,MinZl,InheritJiFenNum,InterfaceBaoZhuangNum,InheritDiscount,Discount,KeyWord,PyCode,ProNumCode,CreateUser,ProTypeId,ProClassId,ProContent,ProTitle");
            strSql.Append(") values (");
            strSql.Append("@ProId,@RecommendLv,@HotLv,@ClickLv,@BuyLv,@StreetId,@FlagInvalid,@OnLineLv,@Units,@IndexImgId,@ProductImgId,@ProName,@RePrice,@RePrice2,@RePrice3,@CommentCount,@AuthorId,@Spec,@ProCode,@Status,@SendPara,@Attr,@MerchantId,@PinPaiId,@ProNum,@GetJiFenNum,@InheritPeiSongType,@InheritProTeXing,@ProTeXing,@IsInfiniteNum,@AllowPriceInterface,@AllowProNumInterface,@MinQuantity,@CreateTime,@Zl,@MinZl,@InheritJiFenNum,@InterfaceBaoZhuangNum,@InheritDiscount,@Discount,@KeyWord,@PyCode,@ProNumCode,@CreateUser,@ProTypeId,@ProClassId,@ProContent,@ProTitle");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,
                        new SqlParameter("@HotLv", SqlDbType.Int,4) ,
                        new SqlParameter("@ClickLv", SqlDbType.Int,4) ,
                        new SqlParameter("@BuyLv", SqlDbType.Int,4) ,
                        new SqlParameter("@StreetId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@OnLineLv", SqlDbType.Int,4) ,
                        new SqlParameter("@Units", SqlDbType.VarChar,20) ,
                        new SqlParameter("@IndexImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProductImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@RePrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice2", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice3", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CommentCount", SqlDbType.Int,4) ,
                        new SqlParameter("@AuthorId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Spec", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@SendPara", SqlDbType.Int,4) ,
                        new SqlParameter("@Attr", SqlDbType.Xml,-1) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PinPaiId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@GetJiFenNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritPeiSongType", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritProTeXing", SqlDbType.Bit,1) ,
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@IsInfiniteNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowPriceInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowProNumInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@MinQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Zl", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MinZl", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritJiFenNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@InterfaceBaoZhuangNum", SqlDbType.Int,4) ,
                        new SqlParameter("@InheritDiscount", SqlDbType.Bit,1) ,
                        new SqlParameter("@Discount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@KeyWord", SqlDbType.VarChar,1250) ,
                        new SqlParameter("@PyCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProNumCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProContent", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@ProTitle", SqlDbType.VarChar,300)

            };

            parameters[0].Value = model.ProId;
            parameters[1].Value = model.RecommendLv;
            parameters[2].Value = model.HotLv;
            parameters[3].Value = model.ClickLv;
            parameters[4].Value = model.BuyLv;
            parameters[5].Value = model.StreetId;
            parameters[6].Value = model.FlagInvalid;
            parameters[7].Value = model.OnLineLv;
            parameters[8].Value = model.Units;
            parameters[9].Value = model.IndexImgId;
            parameters[10].Value = model.ProductImgId;
            parameters[11].Value = model.ProName;
            parameters[12].Value = model.RePrice;
            parameters[13].Value = model.RePrice2;
            parameters[14].Value = model.RePrice3;
            parameters[15].Value = model.CommentCount;
            parameters[16].Value = model.AuthorId;
            parameters[17].Value = model.Spec;
            parameters[18].Value = model.ProCode;
            parameters[19].Value = model.Status;
            parameters[20].Value = model.SendPara;
            parameters[21].Value = model.Attr;
            parameters[22].Value = model.MerchantId;
            parameters[23].Value = model.PinPaiId;
            parameters[24].Value = model.ProNum;
            parameters[25].Value = model.GetJiFenNum;
            parameters[26].Value = model.InheritPeiSongType;
            parameters[27].Value = model.InheritProTeXing;
            parameters[28].Value = model.ProTeXing;
            parameters[29].Value = model.IsInfiniteNum;
            parameters[30].Value = model.AllowPriceInterface;
            parameters[31].Value = model.AllowProNumInterface;
            parameters[32].Value = model.MinQuantity;
            parameters[33].Value = model.CreateTime;
            parameters[34].Value = model.Zl;
            parameters[35].Value = model.MinZl;
            parameters[36].Value = model.InheritJiFenNum;
            parameters[37].Value = model.InterfaceBaoZhuangNum;
            parameters[38].Value = model.InheritDiscount;
            parameters[39].Value = model.Discount;
            parameters[40].Value = model.KeyWord;
            parameters[41].Value = model.PyCode;
            parameters[42].Value = model.ProNumCode;
            parameters[43].Value = model.CreateUser;
            parameters[44].Value = model.ProTypeId;
            parameters[45].Value = model.ProClassId;
            parameters[46].Value = model.ProContent;
            parameters[47].Value = model.ProTitle;

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
        public bool Update(ProductModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.Product set ");

            strSql.Append(" ProId = @ProId , ");
            strSql.Append(" RecommendLv = @RecommendLv , ");
            strSql.Append(" HotLv = @HotLv , ");
            strSql.Append(" ClickLv = @ClickLv , ");
            strSql.Append(" BuyLv = @BuyLv , ");
            strSql.Append(" StreetId = @StreetId , ");
            strSql.Append(" FlagInvalid = @FlagInvalid , ");
            strSql.Append(" OnLineLv = @OnLineLv , ");
            strSql.Append(" Units = @Units , ");
            strSql.Append(" IndexImgId = @IndexImgId , ");
            strSql.Append(" ProductImgId = @ProductImgId , ");
            strSql.Append(" ProName = @ProName , ");
            strSql.Append(" RePrice = @RePrice , ");
            strSql.Append(" RePrice2 = @RePrice2 , ");
            strSql.Append(" RePrice3 = @RePrice3 , ");
            strSql.Append(" CommentCount = @CommentCount , ");
            strSql.Append(" AuthorId = @AuthorId , ");
            strSql.Append(" Spec = @Spec , ");
            strSql.Append(" ProCode = @ProCode , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" SendPara = @SendPara , ");
            strSql.Append(" Attr = @Attr , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" PinPaiId = @PinPaiId , ");
            strSql.Append(" ProNum = @ProNum , ");
            strSql.Append(" GetJiFenNum = @GetJiFenNum , ");
            strSql.Append(" InheritPeiSongType = @InheritPeiSongType , ");
            strSql.Append(" InheritProTeXing = @InheritProTeXing , ");
            strSql.Append(" ProTeXing = @ProTeXing , ");
            strSql.Append(" IsInfiniteNum = @IsInfiniteNum , ");
            strSql.Append(" AllowPriceInterface = @AllowPriceInterface , ");
            strSql.Append(" AllowProNumInterface = @AllowProNumInterface , ");
            strSql.Append(" MinQuantity = @MinQuantity , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" Zl = @Zl , ");
            strSql.Append(" MinZl = @MinZl , ");
            strSql.Append(" InheritJiFenNum = @InheritJiFenNum , ");
            strSql.Append(" InterfaceBaoZhuangNum = @InterfaceBaoZhuangNum , ");
            strSql.Append(" InheritDiscount = @InheritDiscount , ");
            strSql.Append(" Discount = @Discount , ");
            strSql.Append(" KeyWord = @KeyWord , ");
            strSql.Append(" PyCode = @PyCode , ");
            strSql.Append(" ProNumCode = @ProNumCode , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" ProTypeId = @ProTypeId , ");
            strSql.Append(" ProClassId = @ProClassId , ");
            strSql.Append(" ProContent = @ProContent , ");
            strSql.Append(" ProTitle = @ProTitle  ");
            strSql.Append(" where ProId=@ProId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,
                        new SqlParameter("@HotLv", SqlDbType.Int,4) ,
                        new SqlParameter("@ClickLv", SqlDbType.Int,4) ,
                        new SqlParameter("@BuyLv", SqlDbType.Int,4) ,
                        new SqlParameter("@StreetId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@OnLineLv", SqlDbType.Int,4) ,
                        new SqlParameter("@Units", SqlDbType.VarChar,20) ,
                        new SqlParameter("@IndexImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProductImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@RePrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice2", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice3", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CommentCount", SqlDbType.Int,4) ,
                        new SqlParameter("@AuthorId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Spec", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@SendPara", SqlDbType.Int,4) ,
                        new SqlParameter("@Attr", SqlDbType.Xml,-1) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PinPaiId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@GetJiFenNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritPeiSongType", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritProTeXing", SqlDbType.Bit,1) ,
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@IsInfiniteNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowPriceInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowProNumInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@MinQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Zl", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MinZl", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritJiFenNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@InterfaceBaoZhuangNum", SqlDbType.Int,4) ,
                        new SqlParameter("@InheritDiscount", SqlDbType.Bit,1) ,
                        new SqlParameter("@Discount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@KeyWord", SqlDbType.VarChar,1250) ,
                        new SqlParameter("@PyCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProNumCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProContent", SqlDbType.NVarChar,-1) ,
                        new SqlParameter("@ProTitle", SqlDbType.VarChar,300)

            };

            parameters[0].Value = model.ProId;
            parameters[1].Value = model.RecommendLv;
            parameters[2].Value = model.HotLv;
            parameters[3].Value = model.ClickLv;
            parameters[4].Value = model.BuyLv;
            parameters[5].Value = model.StreetId;
            parameters[6].Value = model.FlagInvalid;
            parameters[7].Value = model.OnLineLv;
            parameters[8].Value = model.Units;
            parameters[9].Value = model.IndexImgId;
            parameters[10].Value = model.ProductImgId;
            parameters[11].Value = model.ProName;
            parameters[12].Value = model.RePrice;
            parameters[13].Value = model.RePrice2;
            parameters[14].Value = model.RePrice3;
            parameters[15].Value = model.CommentCount;
            parameters[16].Value = model.AuthorId;
            parameters[17].Value = model.Spec;
            parameters[18].Value = model.ProCode;
            parameters[19].Value = model.Status;
            parameters[20].Value = model.SendPara;
            parameters[21].Value = model.Attr;
            parameters[22].Value = model.MerchantId;
            parameters[23].Value = model.PinPaiId;
            parameters[24].Value = model.ProNum;
            parameters[25].Value = model.GetJiFenNum;
            parameters[26].Value = model.InheritPeiSongType;
            parameters[27].Value = model.InheritProTeXing;
            parameters[28].Value = model.ProTeXing;
            parameters[29].Value = model.IsInfiniteNum;
            parameters[30].Value = model.AllowPriceInterface;
            parameters[31].Value = model.AllowProNumInterface;
            parameters[32].Value = model.MinQuantity;
            parameters[33].Value = model.CreateTime;
            parameters[34].Value = model.Zl;
            parameters[35].Value = model.MinZl;
            parameters[36].Value = model.InheritJiFenNum;
            parameters[37].Value = model.InterfaceBaoZhuangNum;
            parameters[38].Value = model.InheritDiscount;
            parameters[39].Value = model.Discount;
            parameters[40].Value = model.KeyWord;
            parameters[41].Value = model.PyCode;
            parameters[42].Value = model.ProNumCode;
            parameters[43].Value = model.CreateUser;
            parameters[44].Value = model.ProTypeId;
            parameters[45].Value = model.ProClassId;
            parameters[46].Value = model.ProContent;
            parameters[47].Value = model.ProTitle; try
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
        public ProductModel GetModel(string ProId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProId, RecommendLv, HotLv, ClickLv, BuyLv, StreetId, FlagInvalid, OnLineLv, Units, IndexImgId, ProductImgId, ProName, RePrice, RePrice2, RePrice3, CommentCount, AuthorId, Spec, ProCode, Status, SendPara, Attr, MerchantId, PinPaiId, ProNum, GetJiFenNum, InheritPeiSongType, InheritProTeXing, ProTeXing, IsInfiniteNum, AllowPriceInterface, AllowProNumInterface, MinQuantity, CreateTime, Zl, MinZl, InheritJiFenNum, InterfaceBaoZhuangNum, InheritDiscount, Discount, KeyWord, PyCode, ProNumCode, CreateUser, ProTypeId, ProClassId, ProContent, ProTitle  ");
            strSql.Append("  from CORE.dbo.Product ");
            strSql.Append(" where ProId=@ProId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProId", SqlDbType.VarChar,50)            };
            parameters[0].Value = ProId;


            ProductModel model = new ProductModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ProId = ds.Tables[0].Rows[0]["ProId"].ToString();
                if (ds.Tables[0].Rows[0]["RecommendLv"].ToString() != "")
                {
                    model.RecommendLv = int.Parse(ds.Tables[0].Rows[0]["RecommendLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HotLv"].ToString() != "")
                {
                    model.HotLv = int.Parse(ds.Tables[0].Rows[0]["HotLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClickLv"].ToString() != "")
                {
                    model.ClickLv = int.Parse(ds.Tables[0].Rows[0]["ClickLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BuyLv"].ToString() != "")
                {
                    model.BuyLv = int.Parse(ds.Tables[0].Rows[0]["BuyLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StreetId"].ToString() != "")
                {
                    model.StreetId = decimal.Parse(ds.Tables[0].Rows[0]["StreetId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FlagInvalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FlagInvalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["FlagInvalid"].ToString().ToLower() == "true"))
                    {
                        model.FlagInvalid = true;
                    }
                    else
                    {
                        model.FlagInvalid = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["OnLineLv"].ToString() != "")
                {
                    model.OnLineLv = int.Parse(ds.Tables[0].Rows[0]["OnLineLv"].ToString());
                }
                model.Units = ds.Tables[0].Rows[0]["Units"].ToString();
                model.IndexImgId = ds.Tables[0].Rows[0]["IndexImgId"].ToString();
                model.ProductImgId = ds.Tables[0].Rows[0]["ProductImgId"].ToString();
                model.ProName = ds.Tables[0].Rows[0]["ProName"].ToString();
                if (ds.Tables[0].Rows[0]["RePrice"].ToString() != "")
                {
                    model.RePrice = decimal.Parse(ds.Tables[0].Rows[0]["RePrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RePrice2"].ToString() != "")
                {
                    model.RePrice2 = decimal.Parse(ds.Tables[0].Rows[0]["RePrice2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RePrice3"].ToString() != "")
                {
                    model.RePrice3 = decimal.Parse(ds.Tables[0].Rows[0]["RePrice3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommentCount"].ToString() != "")
                {
                    model.CommentCount = int.Parse(ds.Tables[0].Rows[0]["CommentCount"].ToString());
                }
                model.AuthorId = ds.Tables[0].Rows[0]["AuthorId"].ToString();
                model.Spec = ds.Tables[0].Rows[0]["Spec"].ToString();
                model.ProCode = ds.Tables[0].Rows[0]["ProCode"].ToString();
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SendPara"].ToString() != "")
                {
                    model.SendPara = int.Parse(ds.Tables[0].Rows[0]["SendPara"].ToString());
                }
                model.Attr = ds.Tables[0].Rows[0]["Attr"].ToString();
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PinPaiId"].ToString() != "")
                {
                    model.PinPaiId = decimal.Parse(ds.Tables[0].Rows[0]["PinPaiId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProNum"].ToString() != "")
                {
                    model.ProNum = decimal.Parse(ds.Tables[0].Rows[0]["ProNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GetJiFenNum"].ToString() != "")
                {
                    model.GetJiFenNum = decimal.Parse(ds.Tables[0].Rows[0]["GetJiFenNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InheritPeiSongType"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["InheritPeiSongType"].ToString() == "1") || (ds.Tables[0].Rows[0]["InheritPeiSongType"].ToString().ToLower() == "true"))
                    {
                        model.InheritPeiSongType = true;
                    }
                    else
                    {
                        model.InheritPeiSongType = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["InheritProTeXing"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["InheritProTeXing"].ToString() == "1") || (ds.Tables[0].Rows[0]["InheritProTeXing"].ToString().ToLower() == "true"))
                    {
                        model.InheritProTeXing = true;
                    }
                    else
                    {
                        model.InheritProTeXing = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ProTeXing"].ToString() != "")
                {
                    model.ProTeXing = int.Parse(ds.Tables[0].Rows[0]["ProTeXing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsInfiniteNum"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsInfiniteNum"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsInfiniteNum"].ToString().ToLower() == "true"))
                    {
                        model.IsInfiniteNum = true;
                    }
                    else
                    {
                        model.IsInfiniteNum = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AllowPriceInterface"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["AllowPriceInterface"].ToString() == "1") || (ds.Tables[0].Rows[0]["AllowPriceInterface"].ToString().ToLower() == "true"))
                    {
                        model.AllowPriceInterface = true;
                    }
                    else
                    {
                        model.AllowPriceInterface = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AllowProNumInterface"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["AllowProNumInterface"].ToString() == "1") || (ds.Tables[0].Rows[0]["AllowProNumInterface"].ToString().ToLower() == "true"))
                    {
                        model.AllowProNumInterface = true;
                    }
                    else
                    {
                        model.AllowProNumInterface = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["MinQuantity"].ToString() != "")
                {
                    model.MinQuantity = decimal.Parse(ds.Tables[0].Rows[0]["MinQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zl"].ToString() != "")
                {
                    model.Zl = decimal.Parse(ds.Tables[0].Rows[0]["Zl"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinZl"].ToString() != "")
                {
                    model.MinZl = decimal.Parse(ds.Tables[0].Rows[0]["MinZl"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InheritJiFenNum"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["InheritJiFenNum"].ToString() == "1") || (ds.Tables[0].Rows[0]["InheritJiFenNum"].ToString().ToLower() == "true"))
                    {
                        model.InheritJiFenNum = true;
                    }
                    else
                    {
                        model.InheritJiFenNum = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["InterfaceBaoZhuangNum"].ToString() != "")
                {
                    model.InterfaceBaoZhuangNum = int.Parse(ds.Tables[0].Rows[0]["InterfaceBaoZhuangNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InheritDiscount"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["InheritDiscount"].ToString() == "1") || (ds.Tables[0].Rows[0]["InheritDiscount"].ToString().ToLower() == "true"))
                    {
                        model.InheritDiscount = true;
                    }
                    else
                    {
                        model.InheritDiscount = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Discount"].ToString() != "")
                {
                    model.Discount = decimal.Parse(ds.Tables[0].Rows[0]["Discount"].ToString());
                }
                model.KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                model.PyCode = ds.Tables[0].Rows[0]["PyCode"].ToString();
                model.ProNumCode = ds.Tables[0].Rows[0]["ProNumCode"].ToString();
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                if (ds.Tables[0].Rows[0]["ProTypeId"].ToString() != "")
                {
                    model.ProTypeId = decimal.Parse(ds.Tables[0].Rows[0]["ProTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProClassId"].ToString() != "")
                {
                    model.ProClassId = decimal.Parse(ds.Tables[0].Rows[0]["ProClassId"].ToString());
                }
                model.ProContent = ds.Tables[0].Rows[0]["ProContent"].ToString();
                model.ProTitle = ds.Tables[0].Rows[0]["ProTitle"].ToString();

                return model;
            }
            else
            {
                return model;
            }
        }



        /// <summary>
        /// 更改默认产品图片
        /// </summary>
        /// <param name="ProId"></param>
        /// <param name="ImgId"></param>
        /// <returns></returns>
        public bool UpdateProDefaultImg(string ProId, string ImgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Product set ");
            strSql.Append(" ProductImgId='" + ImgId + "'  ");
            strSql.Append(" where ProId='" + ProId + "' ");
            try
            {//异常处理
                this.helper.ExecSqlReInt(strSql.ToString());
                return true;
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
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
            strSql.Append("delete from Product ");
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


        public DataSet GetPageList2(string strWhere, int currentpage, int pagesize)
        {

            StringBuilder s = new StringBuilder();

            #region 超级长的SQL语句
            s.Append(@"SELECT  pro.ProId ,
        pro.ProName ,
        pro.MerchantId ,
        pro.CreateTime ,
        pro.CreateUser ,
        pro.ProTypeId ,
        pro.ProClassId ,
        pro.Spec,
        pro.RePrice2,
        pro.RePrice3,
        pro.ProTitle ,
        pro.RecommendLv ,
        pro.HotLv ,
        pro.ClickLv ,
        pro.BuyLv ,
        pro.StreetId ,
        pro.FlagInvalid ,
        pro.OnLineLv ,
        pro.Units ,
        pro.ProductImgId ,
        pro.RePrice ,
        pc.ProductClassName ,
        pt.ProductTypeName ,
        img.ImgUrl AS ProductImgUrl ,
        mer.MerchantName ,
        mer.TownId ,
        town.TownName ");
            s.Append(@" FROM    dbo.Product AS pro WITH(NOLOCK)
        LEFT OUTER JOIN dbo.ProductClass AS pc WITH(NOLOCK) ON pro.ProClassId = pc.ProductClassId
        LEFT OUTER JOIN dbo.ProductType AS pt WITH(NOLOCK) ON pro.ProTypeId = pt.ProductTypeId
        LEFT OUTER JOIN dbo.ImageInfo AS img WITH(NOLOCK) ON pro.ProductImgId = img.ImgId
        LEFT OUTER JOIN dbo.Merchant AS mer WITH(NOLOCK) ON pro.MerchantId = mer.MerchantId and mer.FlagInvalid=0 and pro.FlagInvalid=0
        LEFT OUTER JOIN dbo.Town AS town WITH(NOLOCK) ON mer.TownId = town.TownId
        LEFT JOIN dbo.MerchantVsMerchantType AS mvmt WITH(NOLOCK) ON mer.MerchantId=mvmt.MerchantId 
        LEFT JOIN dbo.MerchantType AS mt WITH(NOLOCK) ON mvmt.MerchantTypeId = mt.MerchantTypeId  ");
            if (strWhere.Trim() != "")
            {
                s.Append(" where " + strWhere + " ");
            }
            s.Append(@" GROUP BY 
        pro.ProId ,
        pro.ProName ,
        pro.MerchantId ,
        pro.CreateTime ,
        pro.CreateUser ,
        pro.ProTypeId ,
        pro.ProClassId ,
         pro.Spec,
        pro.RePrice2,
        pro.RePrice3,
        pro.ProTitle ,
        pro.RecommendLv ,
        pro.HotLv ,
        pro.ClickLv ,
        pro.BuyLv ,
        pro.StreetId ,
        pro.FlagInvalid ,
        pro.OnLineLv ,
        pro.Units ,
        pro.ProductImgId ,
        pro.RePrice ,
        pc.ProductClassName ,
        pt.ProductTypeName ,
        img.ImgUrl  ,
        mer.MerchantName ,
        mer.TownId ,
        town.TownName ");
            s.Append("  order by pro.CreateTime desc  ");
            #endregion

            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { s.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);

            return ds;

        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {

            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "ProView with(nolock)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            DataTable dt = ds.Tables[0];

            try
            {
                dt.Columns.Add("起售价格");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            decimal 起售价格 = 0;
                            decimal RePrice = decimal.Parse(dr["RePrice"].ToString());
                            switch (dr["ProTeXing"].ToString())
                            {


                                case "1":
                                    //普通商品
                                    decimal MinQuantity = decimal.Parse(dr["MinQuantity"].ToString());
                                    起售价格 = RePrice * MinQuantity;
                                    break;
                                case "2":
                                    //生鲜商品

                                    decimal Zl = decimal.Parse(dr["Zl"].ToString());
                                    decimal MinZl = decimal.Parse(dr["MinZl"].ToString());
                                    起售价格 = RePrice * (Zl / MinZl);
                                    break;

                            }
                            dr["起售价格"] = 起售价格;
                        }
                        catch
                        { 
                        
                        }
                     
                    }
                }
            }
            catch
            {
                
   
            }
           

            return ds;


        }

        public DataSet GetBranchPageList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {

            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "ProVsBranchView with(nolock)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            DataTable dt = ds.Tables[0];

            try
            {
                dt.Columns.Add("起售价格");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            decimal 起售价格 = 0;
                            decimal RePrice = decimal.Parse(dr["RePrice"].ToString());
                            switch (dr["ProTeXing"].ToString())
                            {


                                case "1":
                                    //普通商品
                                    decimal MinQuantity = decimal.Parse(dr["MinQuantity"].ToString());
                                    起售价格 = RePrice * MinQuantity;
                                    break;
                                case "2":
                                    //生鲜商品

                                    decimal Zl = decimal.Parse(dr["Zl"].ToString());
                                    decimal MinZl = decimal.Parse(dr["MinZl"].ToString());
                                    起售价格 = RePrice * (Zl / MinZl);
                                    break;

                            }
                            dr["起售价格"] = 起售价格;
                        }
                        catch
                        {

                        }

                    }
                }
            }
            catch
            {


            }


            return ds;


        }
        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            int top = currentpage * pagesize;
            strSql.Append("select  AuthorName, ProNum,CreateTime,CreateUser,ProClassId,ProductClassName,Units,RePrice,RePrice2,RePrice3,Spec,Status, ProName,ProTitle,ProductImgUrl,ProductImgId,ProId,MerchantName, RecommendLv,");
            strSql.Append(" ProCode,GetJiFenNum,ProNum,PinPaiId ");

            strSql.Append(" FROM ProView WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }


            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);

            return ds;


        }

        /// <summary>
        /// 返回评论数
        /// </summary>
        /// <param name="ProDt"></param>
        /// <returns></returns>
        public DataTable GetNo1Comment(DataTable ProDt, int top)
        {



            // 第二套方案:    SELECT *
            //    FROM (
            //        SELECT *, Rank() over (Partition BY ProId ORDER BY CreateTime DESC ) AS Rank
            //        FROM dbo.ProVsComView
            //        ) rs WHERE Rank <= 3 


            if (ProDt.Rows.Count == 0)
            {

                return null;
            }
            else
            {

                List<string> s = new List<string>();


                foreach (DataRow dr in ProDt.Rows)
                {
                    s.Add(" SELECT TOP " + top + " * FROM  dbo.ProVsComView WHERE PROID='" + dr["ProId"] + "' ");

                }
                string sql = string.Join(" UNION ALL ", s);
                return helper.ExecSqlReDs(sql).Tables[0];
            }

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetProInfoById(string ProId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DECLARE @MerId AS DECIMAL= CONVERT(DECIMAL, (SELECT TOP 1 MerchantId FROM dbo.Product WHERE  ProId='" + ProId + "' )) ");
            strSql.Append("select * ");
            strSql.Append(" FROM ProView  WITH(NOLOCK) where ProId='" + ProId + "'  ");
            strSql.Append(" SELECT * FROM  dbo.ProVsImgView  WITH(NOLOCK) where ProId='" + ProId + "' ");
            strSql.Append(" SELECT mv.* FROM dbo.MerchantView mv  WITH(NOLOCK) WHERE mv.MerchantId=@MerId  ");
            strSql.Append(" SELECT * FROM dbo.MerchantVsUser  WITH(NOLOCK) WHERE MerchantId=@MerId ");
            strSql.Append(" SELECT * FROM dbo.PeiSongTypeVsProduct WITH(NOLOCK) WHERE ProId='" + ProId + "' ");
            return helper.ExecSqlReDs(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ProView  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" FROM ProView ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

