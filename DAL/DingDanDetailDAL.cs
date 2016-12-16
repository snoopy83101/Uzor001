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
    //DingDanDetail
    public partial class DingDanDetailDAL
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
            strSql.Append(" FROM  CORE.dbo.DingDanDetail ");
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
        public bool Add(DingDanDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.DingDanDetail (");
            strSql.Append("Hope,ProTeXing,MinZl,Zlbs,GetJifenNum,DingDanId,ProId,Quantity,Price,Memo,DingDanDetailAttr,JiFen,DingDanDetailTypeId");
            strSql.Append(") values (");
            strSql.Append("@Hope,@ProTeXing,@MinZl,@Zlbs,@GetJifenNum,@DingDanId,@ProId,@Quantity,@Price,@Memo,@DingDanDetailAttr,@JiFen,@DingDanDetailTypeId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Hope", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,            
                        new SqlParameter("@MinZl", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Zlbs", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GetJifenNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Quantity", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DingDanDetailAttr", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@JiFen", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DingDanDetailTypeId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Hope;
            parameters[1].Value = model.ProTeXing;
            parameters[2].Value = model.MinZl;
            parameters[3].Value = model.Zlbs;
            parameters[4].Value = model.GetJifenNum;
            parameters[5].Value = model.DingDanId;
            parameters[6].Value = model.ProId;
            parameters[7].Value = model.Quantity;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.Memo;
            parameters[10].Value = model.DingDanDetailAttr;
            parameters[11].Value = model.JiFen;
            parameters[12].Value = model.DingDanDetailTypeId;

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
        public bool Update(DingDanDetailModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.DingDanDetail set ");

            strSql.Append(" Hope = @Hope , ");
            strSql.Append(" ProTeXing = @ProTeXing , ");
            strSql.Append(" MinZl = @MinZl , ");
            strSql.Append(" Zlbs = @Zlbs , ");
            strSql.Append(" GetJifenNum = @GetJifenNum , ");
            strSql.Append(" DingDanId = @DingDanId , ");
            strSql.Append(" ProId = @ProId , ");
            strSql.Append(" Quantity = @Quantity , ");
            strSql.Append(" Price = @Price , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" DingDanDetailAttr = @DingDanDetailAttr , ");
            strSql.Append(" JiFen = @JiFen , ");
            strSql.Append(" DingDanDetailTypeId = @DingDanDetailTypeId  ");
            strSql.Append(" where DingDanDetailId=@DingDanDetailId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@DingDanDetailId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Hope", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,            
                        new SqlParameter("@MinZl", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Zlbs", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GetJifenNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Quantity", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DingDanDetailAttr", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@JiFen", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DingDanDetailTypeId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.DingDanDetailId;
            parameters[1].Value = model.Hope;
            parameters[2].Value = model.ProTeXing;
            parameters[3].Value = model.MinZl;
            parameters[4].Value = model.Zlbs;
            parameters[5].Value = model.GetJifenNum;
            parameters[6].Value = model.DingDanId;
            parameters[7].Value = model.ProId;
            parameters[8].Value = model.Quantity;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.Memo;
            parameters[11].Value = model.DingDanDetailAttr;
            parameters[12].Value = model.JiFen;
            parameters[13].Value = model.DingDanDetailTypeId; try
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
        public DingDanDetailModel GetModel(decimal DingDanDetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DingDanDetailId, Hope, ProTeXing, MinZl, Zlbs, GetJifenNum, DingDanId, ProId, Quantity, Price, Memo, DingDanDetailAttr, JiFen, DingDanDetailTypeId  ");
            strSql.Append("  from CORE.dbo.DingDanDetail ");
            strSql.Append(" where DingDanDetailId=@DingDanDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DingDanDetailId", SqlDbType.Decimal)
			};
            parameters[0].Value = DingDanDetailId;


            DingDanDetailModel model = new DingDanDetailModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DingDanDetailId"].ToString() != "")
                {
                    model.DingDanDetailId = decimal.Parse(ds.Tables[0].Rows[0]["DingDanDetailId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hope"].ToString() != "")
                {
                    model.Hope = decimal.Parse(ds.Tables[0].Rows[0]["Hope"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProTeXing"].ToString() != "")
                {
                    model.ProTeXing = int.Parse(ds.Tables[0].Rows[0]["ProTeXing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinZl"].ToString() != "")
                {
                    model.MinZl = decimal.Parse(ds.Tables[0].Rows[0]["MinZl"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zlbs"].ToString() != "")
                {
                    model.Zlbs = decimal.Parse(ds.Tables[0].Rows[0]["Zlbs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GetJifenNum"].ToString() != "")
                {
                    model.GetJifenNum = decimal.Parse(ds.Tables[0].Rows[0]["GetJifenNum"].ToString());
                }
                model.DingDanId = ds.Tables[0].Rows[0]["DingDanId"].ToString();
                model.ProId = ds.Tables[0].Rows[0]["ProId"].ToString();
                if (ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
                {
                    model.Quantity = decimal.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                model.DingDanDetailAttr = ds.Tables[0].Rows[0]["DingDanDetailAttr"].ToString();
                if (ds.Tables[0].Rows[0]["JiFen"].ToString() != "")
                {
                    model.JiFen = decimal.Parse(ds.Tables[0].Rows[0]["JiFen"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DingDanDetailTypeId"].ToString() != "")
                {
                    model.DingDanDetailTypeId = int.Parse(ds.Tables[0].Rows[0]["DingDanDetailTypeId"].ToString());
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
            strSql.Append("delete from CORE.dbo.DingDanDetail ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.DingDanDetailView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.DingDanDetailView  WITH(NOLOCK)  ");
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.DingDanDetailView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.DingDanDetail  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

