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
    //WxSuCaiDetail
    public partial class WxSuCaiDetailDAL
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
            strSql.Append(" FROM  CORE.dbo.WxSuCaiDetail ");
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
        public bool Add(WxSuCaiDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.WxSuCaiDetail (");
            strSql.Append("OtherPara,OrderNo,WxSuCaiInfoId,WxSuCaiDetailTitle,WxSuCaiDetailMemo,WxSuCaiDetailContent,WxSuCaiDetailClassId,ImgId,Url,ReKey");
            strSql.Append(") values (");
            strSql.Append("@OtherPara,@OrderNo,@WxSuCaiInfoId,@WxSuCaiDetailTitle,@WxSuCaiDetailMemo,@WxSuCaiDetailContent,@WxSuCaiDetailClassId,@ImgId,@Url,@ReKey");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@OtherPara", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxSuCaiInfoId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@WxSuCaiDetailTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSuCaiDetailMemo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@WxSuCaiDetailContent", SqlDbType.NText) ,            
                        new SqlParameter("@WxSuCaiDetailClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Url", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.OtherPara;
            parameters[1].Value = model.OrderNo;
            parameters[2].Value = model.WxSuCaiInfoId;
            parameters[3].Value = model.WxSuCaiDetailTitle;
            parameters[4].Value = model.WxSuCaiDetailMemo;
            parameters[5].Value = model.WxSuCaiDetailContent;
            parameters[6].Value = model.WxSuCaiDetailClassId;
            parameters[7].Value = model.ImgId;
            parameters[8].Value = model.Url;
            parameters[9].Value = model.ReKey;

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
        public bool Update(WxSuCaiDetailModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.WxSuCaiDetail set ");

            strSql.Append(" OtherPara = @OtherPara , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" WxSuCaiInfoId = @WxSuCaiInfoId , ");
            strSql.Append(" WxSuCaiDetailTitle = @WxSuCaiDetailTitle , ");
            strSql.Append(" WxSuCaiDetailMemo = @WxSuCaiDetailMemo , ");
            strSql.Append(" WxSuCaiDetailContent = @WxSuCaiDetailContent , ");
            strSql.Append(" WxSuCaiDetailClassId = @WxSuCaiDetailClassId , ");
            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" Url = @Url , ");
            strSql.Append(" ReKey = @ReKey  ");
            strSql.Append(" where WxSuCaiDetailId=@WxSuCaiDetailId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@WxSuCaiDetailId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@OtherPara", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxSuCaiInfoId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@WxSuCaiDetailTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSuCaiDetailMemo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@WxSuCaiDetailContent", SqlDbType.NText) ,            
                        new SqlParameter("@WxSuCaiDetailClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Url", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.WxSuCaiDetailId;
            parameters[1].Value = model.OtherPara;
            parameters[2].Value = model.OrderNo;
            parameters[3].Value = model.WxSuCaiInfoId;
            parameters[4].Value = model.WxSuCaiDetailTitle;
            parameters[5].Value = model.WxSuCaiDetailMemo;
            parameters[6].Value = model.WxSuCaiDetailContent;
            parameters[7].Value = model.WxSuCaiDetailClassId;
            parameters[8].Value = model.ImgId;
            parameters[9].Value = model.Url;
            parameters[10].Value = model.ReKey; try
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
        public WxSuCaiDetailModel GetModel(decimal WxSuCaiDetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WxSuCaiDetailId, OtherPara, OrderNo, WxSuCaiInfoId, WxSuCaiDetailTitle, WxSuCaiDetailMemo, WxSuCaiDetailContent, WxSuCaiDetailClassId, ImgId, Url, ReKey  ");
            strSql.Append("  from CORE.dbo.WxSuCaiDetail ");
            strSql.Append(" where WxSuCaiDetailId=@WxSuCaiDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@WxSuCaiDetailId", SqlDbType.Decimal)
			};
            parameters[0].Value = WxSuCaiDetailId;


            WxSuCaiDetailModel model = new WxSuCaiDetailModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["WxSuCaiDetailId"].ToString() != "")
                {
                    model.WxSuCaiDetailId = decimal.Parse(ds.Tables[0].Rows[0]["WxSuCaiDetailId"].ToString());
                }
                model.OtherPara = ds.Tables[0].Rows[0]["OtherPara"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WxSuCaiInfoId"].ToString() != "")
                {
                    model.WxSuCaiInfoId = decimal.Parse(ds.Tables[0].Rows[0]["WxSuCaiInfoId"].ToString());
                }
                model.WxSuCaiDetailTitle = ds.Tables[0].Rows[0]["WxSuCaiDetailTitle"].ToString();
                model.WxSuCaiDetailMemo = ds.Tables[0].Rows[0]["WxSuCaiDetailMemo"].ToString();
                model.WxSuCaiDetailContent = ds.Tables[0].Rows[0]["WxSuCaiDetailContent"].ToString();
                if (ds.Tables[0].Rows[0]["WxSuCaiDetailClassId"].ToString() != "")
                {
                    model.WxSuCaiDetailClassId = int.Parse(ds.Tables[0].Rows[0]["WxSuCaiDetailClassId"].ToString());
                }
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();

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
            strSql.Append("delete from CORE.dbo.WxSuCaiDetail ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.WxSuCaiDetailView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.WxSuCaiDetailView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.WxSuCaiDetail  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

