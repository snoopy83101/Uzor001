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
    //用户点评表
    public partial class CommentDAL
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
            strSql.Append(" FROM Comment ");
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
        public bool Add(CommentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Comment(");
            strSql.Append("RepCount,JsonMemo,CommentType,CommentTitle,CommentContent,CreateTime,CreateUser,ReceiveUser,FlagInvalid,ParentCommenId");
            strSql.Append(") values (");
            strSql.Append("@RepCount,@JsonMemo,@CommentType,@CommentTitle,@CommentContent,@CreateTime,@CreateUser,@ReceiveUser,@FlagInvalid,@ParentCommenId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@RepCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@JsonMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@CommentType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CommentTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CommentContent", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReceiveUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ParentCommenId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.RepCount;
            parameters[1].Value = model.JsonMemo;
            parameters[2].Value = model.CommentType;
            parameters[3].Value = model.CommentTitle;
            parameters[4].Value = model.CommentContent;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.CreateUser;
            parameters[7].Value = model.ReceiveUser;
            parameters[8].Value = model.FlagInvalid;
            parameters[9].Value = model.ParentCommenId;         

            bool result = false;
            try
            {
                if (model.ParentCommenId != 0)
                {
                    strSql.Append(Common.SqlStrHelper.BindCommentRep(model.ParentCommenId));
                }
                model.CommentId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "CommentId", parameters));
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
        public bool Update(CommentModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Comment set ");

            strSql.Append(" RepCount = @RepCount , ");
            strSql.Append(" JsonMemo = @JsonMemo , ");
            strSql.Append(" CommentType = @CommentType , ");
            strSql.Append(" CommentTitle = @CommentTitle , ");
            strSql.Append(" CommentContent = @CommentContent , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" ReceiveUser = @ReceiveUser , ");
            strSql.Append(" FlagInvalid = @FlagInvalid , ");
            strSql.Append(" ParentCommenId = @ParentCommenId  ");
            strSql.Append(" where CommentId=@CommentId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@CommentId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RepCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@JsonMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@CommentType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CommentTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CommentContent", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReceiveUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ParentCommenId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.CommentId;
            parameters[1].Value = model.RepCount;
            parameters[2].Value = model.JsonMemo;
            parameters[3].Value = model.CommentType;
            parameters[4].Value = model.CommentTitle;
            parameters[5].Value = model.CommentContent;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.CreateUser;
            parameters[8].Value = model.ReceiveUser;
            parameters[9].Value = model.FlagInvalid;
            parameters[10].Value = model.ParentCommenId; try
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
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Comment ");
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


        public DataSet GetProCommentPageList(string strWhere, int currentpage, int pagesize)
        { 
        
         StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  c.*,pvc.ProId ");
            strSql.Append(" FROM    dbo.ProVsComment pvc  WITH(NOLOCK) ");
            strSql.Append("    LEFT JOIN dbo.CommentView c WITH(NOLOCK) ON pvc.CommentId = c.CommentId ");
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
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM CommentView WITH(NOLOCK) ");
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

        public DataSet GetCommentRep(DataTable dtcomment)
        {

            List<string> s = new List<string>();
            foreach (DataRow dr in dtcomment.Rows)
            {
                s.Add(" select  * from  (select top 5 * from  dbo.CommentView WITH(NOLOCK)  where ParentCommenId='" + dr["CommentId"] + "' Order by CreateTime desc) as a ");
            }
            string strsql = string.Join(" UNION ALL ", s);
            return helper.ExecSqlReDs(strsql);

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Comment ");
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
            strSql.Append(" FROM Comment WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

