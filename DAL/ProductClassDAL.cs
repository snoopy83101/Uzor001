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
    //用户可以自定义的产品类型列表
    public partial class ProductClassDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ProductClassModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.ProductClass (");
            strSql.Append("Invalid,AttrSelXml,ProClassColor,ProClassKeyWord,InheritPeiSongType,InheritProTeXing,ProTeXing,GetJiFenNum,InheritJiFenNum,CldProClassIds,ParentProductClassId,InheritDiscount,Discount,ImgId,ProductClassName,ProductClassAppName,OrderNo,Memo,AppMemo,MerchantId,ProductClassImgId");
            strSql.Append(") values (");
            strSql.Append("@Invalid,@AttrSelXml,@ProClassColor,@ProClassKeyWord,@InheritPeiSongType,@InheritProTeXing,@ProTeXing,@GetJiFenNum,@InheritJiFenNum,@CldProClassIds,@ParentProductClassId,@InheritDiscount,@Discount,@ImgId,@ProductClassName,@ProductClassAppName,@OrderNo,@Memo,@AppMemo,@MerchantId,@ProductClassImgId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@AttrSelXml", SqlDbType.Xml,-1) ,
                        new SqlParameter("@ProClassColor", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProClassKeyWord", SqlDbType.VarChar,500) ,
                        new SqlParameter("@InheritPeiSongType", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritProTeXing", SqlDbType.Bit,1) ,
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@GetJiFenNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritJiFenNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@CldProClassIds", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@ParentProductClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritDiscount", SqlDbType.Bit,1) ,
                        new SqlParameter("@Discount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ImgId", SqlDbType.VarChar,500) ,
                        new SqlParameter("@ProductClassName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProductClassAppName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@AppMemo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProductClassImgId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.Invalid;
            parameters[1].Value = model.AttrSelXml;
            parameters[2].Value = model.ProClassColor;
            parameters[3].Value = model.ProClassKeyWord;
            parameters[4].Value = model.InheritPeiSongType;
            parameters[5].Value = model.InheritProTeXing;
            parameters[6].Value = model.ProTeXing;
            parameters[7].Value = model.GetJiFenNum;
            parameters[8].Value = model.InheritJiFenNum;
            parameters[9].Value = model.CldProClassIds;
            parameters[10].Value = model.ParentProductClassId;
            parameters[11].Value = model.InheritDiscount;
            parameters[12].Value = model.Discount;
            parameters[13].Value = model.ImgId;
            parameters[14].Value = model.ProductClassName;
            parameters[15].Value = model.ProductClassAppName;
            parameters[16].Value = model.OrderNo;
            parameters[17].Value = model.Memo;
            parameters[18].Value = model.AppMemo;
            parameters[19].Value = model.MerchantId;
            parameters[20].Value = model.ProductClassImgId;

            bool result = false;
            try
            {


                model.ProductClassId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "ProductClassId", parameters));


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
        public bool Update(ProductClassModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.ProductClass set ");

            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" AttrSelXml = @AttrSelXml , ");
            strSql.Append(" ProClassColor = @ProClassColor , ");
            strSql.Append(" ProClassKeyWord = @ProClassKeyWord , ");
            strSql.Append(" InheritPeiSongType = @InheritPeiSongType , ");
            strSql.Append(" InheritProTeXing = @InheritProTeXing , ");
            strSql.Append(" ProTeXing = @ProTeXing , ");
            strSql.Append(" GetJiFenNum = @GetJiFenNum , ");
            strSql.Append(" InheritJiFenNum = @InheritJiFenNum , ");
            strSql.Append(" CldProClassIds = @CldProClassIds , ");
            strSql.Append(" ParentProductClassId = @ParentProductClassId , ");
            strSql.Append(" InheritDiscount = @InheritDiscount , ");
            strSql.Append(" Discount = @Discount , ");
            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" ProductClassName = @ProductClassName , ");
            strSql.Append(" ProductClassAppName = @ProductClassAppName , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" AppMemo = @AppMemo , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" ProductClassImgId = @ProductClassImgId  ");
            strSql.Append(" where ProductClassId=@ProductClassId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ProductClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@AttrSelXml", SqlDbType.Xml,-1) ,
                        new SqlParameter("@ProClassColor", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProClassKeyWord", SqlDbType.VarChar,500) ,
                        new SqlParameter("@InheritPeiSongType", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritProTeXing", SqlDbType.Bit,1) ,
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@GetJiFenNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritJiFenNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@CldProClassIds", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@ParentProductClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritDiscount", SqlDbType.Bit,1) ,
                        new SqlParameter("@Discount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ImgId", SqlDbType.VarChar,500) ,
                        new SqlParameter("@ProductClassName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProductClassAppName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@AppMemo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProductClassImgId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.ProductClassId;
            parameters[1].Value = model.Invalid;
            parameters[2].Value = model.AttrSelXml;
            parameters[3].Value = model.ProClassColor;
            parameters[4].Value = model.ProClassKeyWord;
            parameters[5].Value = model.InheritPeiSongType;
            parameters[6].Value = model.InheritProTeXing;
            parameters[7].Value = model.ProTeXing;
            parameters[8].Value = model.GetJiFenNum;
            parameters[9].Value = model.InheritJiFenNum;
            parameters[10].Value = model.CldProClassIds;
            parameters[11].Value = model.ParentProductClassId;
            parameters[12].Value = model.InheritDiscount;
            parameters[13].Value = model.Discount;
            parameters[14].Value = model.ImgId;
            parameters[15].Value = model.ProductClassName;
            parameters[16].Value = model.ProductClassAppName;
            parameters[17].Value = model.OrderNo;
            parameters[18].Value = model.Memo;
            parameters[19].Value = model.AppMemo;
            parameters[20].Value = model.MerchantId;
            parameters[21].Value = model.ProductClassImgId; try
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
        public ProductClassModel GetModel(decimal ProductClassId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProductClassId, Invalid, AttrSelXml, ProClassColor, ProClassKeyWord, InheritPeiSongType, InheritProTeXing, ProTeXing, GetJiFenNum, InheritJiFenNum, CldProClassIds, ParentProductClassId, InheritDiscount, Discount, ImgId, ProductClassName, ProductClassAppName, OrderNo, Memo, AppMemo, MerchantId, ProductClassImgId  ");
            strSql.Append("  from CORE.dbo.ProductClass ");
            strSql.Append(" where ProductClassId=@ProductClassId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductClassId", SqlDbType.Decimal)
            };
            parameters[0].Value = ProductClassId;


            ProductClassModel model = new ProductClassModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ProductClassId"].ToString() != "")
                {
                    model.ProductClassId = decimal.Parse(ds.Tables[0].Rows[0]["ProductClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Invalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Invalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["Invalid"].ToString().ToLower() == "true"))
                    {
                        model.Invalid = true;
                    }
                    else
                    {
                        model.Invalid = false;
                    }
                }
                model.AttrSelXml = ds.Tables[0].Rows[0]["AttrSelXml"].ToString();
                model.ProClassColor = ds.Tables[0].Rows[0]["ProClassColor"].ToString();
                model.ProClassKeyWord = ds.Tables[0].Rows[0]["ProClassKeyWord"].ToString();
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
                if (ds.Tables[0].Rows[0]["GetJiFenNum"].ToString() != "")
                {
                    model.GetJiFenNum = decimal.Parse(ds.Tables[0].Rows[0]["GetJiFenNum"].ToString());
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
                model.CldProClassIds = ds.Tables[0].Rows[0]["CldProClassIds"].ToString();
                if (ds.Tables[0].Rows[0]["ParentProductClassId"].ToString() != "")
                {
                    model.ParentProductClassId = decimal.Parse(ds.Tables[0].Rows[0]["ParentProductClassId"].ToString());
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
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();
                model.ProductClassName = ds.Tables[0].Rows[0]["ProductClassName"].ToString();
                model.ProductClassAppName = ds.Tables[0].Rows[0]["ProductClassAppName"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                model.AppMemo = ds.Tables[0].Rows[0]["AppMemo"].ToString();
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                model.ProductClassImgId = ds.Tables[0].Rows[0]["ProductClassImgId"].ToString();

                return model;
            }
            else
            {
                return model;
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
            strSql.Append("delete from ProductClass ");
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
        public DataSet GetPageList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "CORE.dbo.ProductClass  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM ProductClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("CORE.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }

        /// <summary>
        /// 取得一个类别主体内容
        /// </summary>
        /// <param name="ProductClassId"></param>
        /// <returns></returns>
        public DataSet GetProClassInfo(decimal ProductClassId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ProClassView with(nolock) where ProductClassId='" + ProductClassId + "' ");

            strSql.Append(" select * from dbo.PeiSongTypeVsProductClassView where ProductClassId='" + ProductClassId + "' ");

            strSql.Append("select pvp.*,pp.PinPaiName from dbo.ProClassVsPinPai pvp with(nolock) LEFT JOIN dbo.PinPaiInfo pp  with(nolock) ON pp.PinPaiId = pvp.PinPaiId where ProClassId='" + ProductClassId + "' Order by pvp.OrderNo desc ");


            return helper.ExecSqlReDs(strSql.ToString());

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ProClassView ");
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
            strSql.Append(" FROM ProductClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

