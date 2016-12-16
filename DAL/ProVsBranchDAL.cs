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
    //ProVsBranch
    public partial class ProVsBranchDAL
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
            strSql.Append(" FROM  CORE.dbo.ProVsBranch ");
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
        public bool Add(ProVsBranchModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.ProVsBranch (");
            strSql.Append("BranchId,OnLineLv,ProNum,GetJiFenNum,InheritPeiSongType,InheritProTeXing,IsInfiniteNum,AllowPriceInterface,AllowProNumInterface,InheritJiFenNum,InterfaceBaoZhuangNum,ProId,InheritDiscount,Discount,ProName,KeyWord,RePrice,RePrice2,RePrice3,Status,MinQuantity,Zl,MinZl");
            strSql.Append(") values (");
            strSql.Append("@BranchId,@OnLineLv,@ProNum,@GetJiFenNum,@InheritPeiSongType,@InheritProTeXing,@IsInfiniteNum,@AllowPriceInterface,@AllowProNumInterface,@InheritJiFenNum,@InterfaceBaoZhuangNum,@ProId,@InheritDiscount,@Discount,@ProName,@KeyWord,@RePrice,@RePrice2,@RePrice3,@Status,@MinQuantity,@Zl,@MinZl");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OnLineLv", SqlDbType.Int,4) ,
                        new SqlParameter("@ProNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@GetJiFenNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritPeiSongType", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritProTeXing", SqlDbType.Bit,1) ,
                        new SqlParameter("@IsInfiniteNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowPriceInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowProNumInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritJiFenNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@InterfaceBaoZhuangNum", SqlDbType.Int,4) ,
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@InheritDiscount", SqlDbType.Bit,1) ,
                        new SqlParameter("@Discount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@KeyWord", SqlDbType.VarChar,1250) ,
                        new SqlParameter("@RePrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice2", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice3", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@MinQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Zl", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MinZl", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.BranchId;
            parameters[1].Value = model.OnLineLv;
            parameters[2].Value = model.ProNum;
            parameters[3].Value = model.GetJiFenNum;
            parameters[4].Value = model.InheritPeiSongType;
            parameters[5].Value = model.InheritProTeXing;
            parameters[6].Value = model.IsInfiniteNum;
            parameters[7].Value = model.AllowPriceInterface;
            parameters[8].Value = model.AllowProNumInterface;
            parameters[9].Value = model.InheritJiFenNum;
            parameters[10].Value = model.InterfaceBaoZhuangNum;
            parameters[11].Value = model.ProId;
            parameters[12].Value = model.InheritDiscount;
            parameters[13].Value = model.Discount;
            parameters[14].Value = model.ProName;
            parameters[15].Value = model.KeyWord;
            parameters[16].Value = model.RePrice;
            parameters[17].Value = model.RePrice2;
            parameters[18].Value = model.RePrice3;
            parameters[19].Value = model.Status;
            parameters[20].Value = model.MinQuantity;
            parameters[21].Value = model.Zl;
            parameters[22].Value = model.MinZl;

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
        public bool Update(ProVsBranchModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.ProVsBranch set ");

            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" OnLineLv = @OnLineLv , ");
            strSql.Append(" ProNum = @ProNum , ");
            strSql.Append(" GetJiFenNum = @GetJiFenNum , ");
            strSql.Append(" InheritPeiSongType = @InheritPeiSongType , ");
            strSql.Append(" InheritProTeXing = @InheritProTeXing , ");
            strSql.Append(" IsInfiniteNum = @IsInfiniteNum , ");
            strSql.Append(" AllowPriceInterface = @AllowPriceInterface , ");
            strSql.Append(" AllowProNumInterface = @AllowProNumInterface , ");
            strSql.Append(" InheritJiFenNum = @InheritJiFenNum , ");
            strSql.Append(" InterfaceBaoZhuangNum = @InterfaceBaoZhuangNum , ");
            strSql.Append(" ProId = @ProId , ");
            strSql.Append(" InheritDiscount = @InheritDiscount , ");
            strSql.Append(" Discount = @Discount , ");
            strSql.Append(" ProName = @ProName , ");
            strSql.Append(" KeyWord = @KeyWord , ");
            strSql.Append(" RePrice = @RePrice , ");
            strSql.Append(" RePrice2 = @RePrice2 , ");
            strSql.Append(" RePrice3 = @RePrice3 , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" MinQuantity = @MinQuantity , ");
            strSql.Append(" Zl = @Zl , ");
            strSql.Append(" MinZl = @MinZl  ");
            strSql.Append(" where BranchId=@BranchId and ProId=@ProId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OnLineLv", SqlDbType.Int,4) ,
                        new SqlParameter("@ProNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@GetJiFenNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@InheritPeiSongType", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritProTeXing", SqlDbType.Bit,1) ,
                        new SqlParameter("@IsInfiniteNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowPriceInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@AllowProNumInterface", SqlDbType.Bit,1) ,
                        new SqlParameter("@InheritJiFenNum", SqlDbType.Bit,1) ,
                        new SqlParameter("@InterfaceBaoZhuangNum", SqlDbType.Int,4) ,
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@InheritDiscount", SqlDbType.Bit,1) ,
                        new SqlParameter("@Discount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@KeyWord", SqlDbType.VarChar,1250) ,
                        new SqlParameter("@RePrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice2", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RePrice3", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@MinQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Zl", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MinZl", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.BranchId;
            parameters[1].Value = model.OnLineLv;
            parameters[2].Value = model.ProNum;
            parameters[3].Value = model.GetJiFenNum;
            parameters[4].Value = model.InheritPeiSongType;
            parameters[5].Value = model.InheritProTeXing;
            parameters[6].Value = model.IsInfiniteNum;
            parameters[7].Value = model.AllowPriceInterface;
            parameters[8].Value = model.AllowProNumInterface;
            parameters[9].Value = model.InheritJiFenNum;
            parameters[10].Value = model.InterfaceBaoZhuangNum;
            parameters[11].Value = model.ProId;
            parameters[12].Value = model.InheritDiscount;
            parameters[13].Value = model.Discount;
            parameters[14].Value = model.ProName;
            parameters[15].Value = model.KeyWord;
            parameters[16].Value = model.RePrice;
            parameters[17].Value = model.RePrice2;
            parameters[18].Value = model.RePrice3;
            parameters[19].Value = model.Status;
            parameters[20].Value = model.MinQuantity;
            parameters[21].Value = model.Zl;
            parameters[22].Value = model.MinZl; try
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
        public ProVsBranchModel GetModel(string BranchId, string ProId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BranchId, OnLineLv, ProNum, GetJiFenNum, InheritPeiSongType, InheritProTeXing, IsInfiniteNum, AllowPriceInterface, AllowProNumInterface, InheritJiFenNum, InterfaceBaoZhuangNum, ProId, InheritDiscount, Discount, ProName, KeyWord, RePrice, RePrice2, RePrice3, Status, MinQuantity, Zl, MinZl  ");
            strSql.Append("  from CORE.dbo.ProVsBranch ");
            strSql.Append(" where BranchId=@BranchId and ProId=@ProId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@BranchId", SqlDbType.VarChar,50),
                    new SqlParameter("@ProId", SqlDbType.VarChar,50)            };
            parameters[0].Value = BranchId;
            parameters[1].Value = ProId;


            ProVsBranchModel model = new ProVsBranchModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                if (ds.Tables[0].Rows[0]["OnLineLv"].ToString() != "")
                {
                    model.OnLineLv = int.Parse(ds.Tables[0].Rows[0]["OnLineLv"].ToString());
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
                model.ProId = ds.Tables[0].Rows[0]["ProId"].ToString();
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
                model.ProName = ds.Tables[0].Rows[0]["ProName"].ToString();
                model.KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
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
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinQuantity"].ToString() != "")
                {
                    model.MinQuantity = decimal.Parse(ds.Tables[0].Rows[0]["MinQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zl"].ToString() != "")
                {
                    model.Zl = decimal.Parse(ds.Tables[0].Rows[0]["Zl"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinZl"].ToString() != "")
                {
                    model.MinZl = decimal.Parse(ds.Tables[0].Rows[0]["MinZl"].ToString());
                }

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
            strSql.Append("delete from CORE.dbo.ProVsBranch ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.ProVsBranchView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };



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
        /// 获得数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.ProVsBranch  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("CORE.dbo.fenye", fenyeParmName, fenyeParmValue);


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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.ProVsBranch  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.ProVsBranch  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

