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
    //IndexData
    public partial class IndexDataDAL
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
            strSql.Append(" FROM IndexData ");
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
        public bool Add(IndexDataModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into IndexData(");
            strSql.Append("ReKey,Url,JsonMemo,EventName,ItemTitle,ItemContent,ItemType,ItemClass,OrderNo,CreateTime,CreateUser,ImgId");
            strSql.Append(") values (");
            strSql.Append("@ReKey,@Url,@JsonMemo,@EventName,@ItemTitle,@ItemContent,@ItemType,@ItemClass,@OrderNo,@CreateTime,@CreateUser,@ImgId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Url", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JsonMemo", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@EventName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ItemTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ItemContent", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@ItemType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ItemClass", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.ReKey;
            parameters[1].Value = model.Url;
            parameters[2].Value = model.JsonMemo;
            parameters[3].Value = model.EventName;
            parameters[4].Value = model.ItemTitle;
            parameters[5].Value = model.ItemContent;
            parameters[6].Value = model.ItemType;
            parameters[7].Value = model.ItemClass;
            parameters[8].Value = model.OrderNo;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;
            parameters[11].Value = model.ImgId;            

            bool result = false;
            try
            {
                model.AutoId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "AutoId", parameters));
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
        public bool Update(IndexDataModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IndexData set ");

            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" Url = @Url , ");
            strSql.Append(" JsonMemo = @JsonMemo , ");
            strSql.Append(" EventName = @EventName , ");
            strSql.Append(" ItemTitle = @ItemTitle , ");
            strSql.Append(" ItemContent = @ItemContent , ");
            strSql.Append(" ItemType = @ItemType , ");
            strSql.Append(" ItemClass = @ItemClass , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" ImgId = @ImgId  ");
            strSql.Append(" where AutoId=@AutoId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AutoId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Url", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JsonMemo", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@EventName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ItemTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ItemContent", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@ItemType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ItemClass", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.AutoId;
            parameters[1].Value = model.ReKey;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.JsonMemo;
            parameters[4].Value = model.EventName;
            parameters[5].Value = model.ItemTitle;
            parameters[6].Value = model.ItemContent;
            parameters[7].Value = model.ItemType;
            parameters[8].Value = model.ItemClass;
            parameters[9].Value = model.OrderNo;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.CreateUser;
            parameters[12].Value = model.ImgId; try
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
        public IndexDataModel GetModel(decimal AutoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoId, ReKey, Url, JsonMemo, EventName, ItemTitle, ItemContent, ItemType, ItemClass, OrderNo, CreateTime, CreateUser, ImgId  ");
            strSql.Append("  from IndexData ");
            strSql.Append(" where AutoId=@AutoId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AutoId", SqlDbType.Decimal,9)			};
            parameters[0].Value = AutoId;


            IndexDataModel model = new IndexDataModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AutoId"].ToString() != "")
                {
                    model.AutoId = decimal.Parse(ds.Tables[0].Rows[0]["AutoId"].ToString());
                }
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.JsonMemo = ds.Tables[0].Rows[0]["JsonMemo"].ToString();
                model.EventName = ds.Tables[0].Rows[0]["EventName"].ToString();
                model.ItemTitle = ds.Tables[0].Rows[0]["ItemTitle"].ToString();
                model.ItemContent = ds.Tables[0].Rows[0]["ItemContent"].ToString();
                model.ItemType = ds.Tables[0].Rows[0]["ItemType"].ToString();
                model.ItemClass = ds.Tables[0].Rows[0]["ItemClass"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();

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
            strSql.Append("delete from IndexData ");
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
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM IndexData  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM IndexData  WITH(NOLOCK) ");
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
            strSql.Append(" FROM IndexData  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

