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
    //扩展属性表
    public partial class ProAttrDAL
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
            strSql.Append(" FROM  CORE.dbo.ProAttr ");
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
        public bool Add(ProAttrModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.ProAttr (");
            strSql.Append("ProClassId,ProAttrName,ProAttrMemo,Invalid,AttrUnit");
            strSql.Append(") values (");
            strSql.Append("@ProClassId,@ProAttrName,@ProAttrMemo,@Invalid,@AttrUnit");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ProClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProAttrName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ProAttrMemo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@AttrUnit", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.ProClassId;
            parameters[1].Value = model.ProAttrName;
            parameters[2].Value = model.ProAttrMemo;
            parameters[3].Value = model.Invalid;
            parameters[4].Value = model.AttrUnit;

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
        public bool Update(ProAttrModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.ProAttr set ");

            strSql.Append(" ProClassId = @ProClassId , ");
            strSql.Append(" ProAttrName = @ProAttrName , ");
            strSql.Append(" ProAttrMemo = @ProAttrMemo , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" AttrUnit = @AttrUnit  ");
            strSql.Append(" where ProAttrId=@ProAttrId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ProAttrId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProAttrName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ProAttrMemo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@AttrUnit", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.ProAttrId;
            parameters[1].Value = model.ProClassId;
            parameters[2].Value = model.ProAttrName;
            parameters[3].Value = model.ProAttrMemo;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.AttrUnit; try
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
        public ProAttrModel GetModel(decimal ProAttrId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProAttrId, ProClassId, ProAttrName, ProAttrMemo, Invalid, AttrUnit  ");
            strSql.Append("  from CORE.dbo.ProAttr ");
            strSql.Append(" where ProAttrId=@ProAttrId");
            SqlParameter[] parameters = {
					new SqlParameter("@ProAttrId", SqlDbType.Decimal)
			};
            parameters[0].Value = ProAttrId;


            ProAttrModel model = new ProAttrModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ProAttrId"].ToString() != "")
                {
                    model.ProAttrId = decimal.Parse(ds.Tables[0].Rows[0]["ProAttrId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProClassId"].ToString() != "")
                {
                    model.ProClassId = decimal.Parse(ds.Tables[0].Rows[0]["ProClassId"].ToString());
                }
                model.ProAttrName = ds.Tables[0].Rows[0]["ProAttrName"].ToString();
                model.ProAttrMemo = ds.Tables[0].Rows[0]["ProAttrMemo"].ToString();
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
                model.AttrUnit = ds.Tables[0].Rows[0]["AttrUnit"].ToString();

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
            strSql.Append("delete from CORE.dbo.ProAttr ");
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
            strSql.Append(" FROM CORE.dbo.ProAttr  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.ProAttr  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.ProAttr  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

