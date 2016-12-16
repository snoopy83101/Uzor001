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
    //HouseVsComment
    public partial class HouseVsCommentDAL
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
            strSql.Append(" FROM HouseVsComment ");
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
        public bool Add(HouseVsCommentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HouseVsComment(");
            strSql.Append("HouseId,CommentId,VsType");
            strSql.Append(") values (");
            strSql.Append("@HouseId,@CommentId,@VsType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HouseId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CommentId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.HouseId;
            parameters[1].Value = model.CommentId;
            parameters[2].Value = model.VsType;

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
        public bool Update(HouseVsCommentModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HouseVsComment set ");

            strSql.Append(" HouseId = @HouseId , ");
            strSql.Append(" CommentId = @CommentId , ");
            strSql.Append(" VsType = @VsType  ");
            strSql.Append(" where HouseId=@HouseId and CommentId=@CommentId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HouseId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CommentId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.HouseId;
            parameters[1].Value = model.CommentId;
            parameters[2].Value = model.VsType; try
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
        public HouseVsCommentModel GetModel(string HouseId, decimal CommentId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HouseId, CommentId, VsType  ");
            strSql.Append("  from HouseVsComment ");
            strSql.Append(" where HouseId=@HouseId and CommentId=@CommentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@HouseId", SqlDbType.VarChar,50),
					new SqlParameter("@CommentId", SqlDbType.Decimal,9)			};
            parameters[0].Value = HouseId;
            parameters[1].Value = CommentId;


            HouseVsCommentModel model = new HouseVsCommentModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.HouseId = ds.Tables[0].Rows[0]["HouseId"].ToString();
                if (ds.Tables[0].Rows[0]["CommentId"].ToString() != "")
                {
                    model.CommentId = decimal.Parse(ds.Tables[0].Rows[0]["CommentId"].ToString());
                }
                model.VsType = ds.Tables[0].Rows[0]["VsType"].ToString();

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
            strSql.Append("delete from HouseVsComment ");
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
            strSql.Append(" FROM HouseVsCommentView ");
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
            strSql.Append(" FROM HouseVsComment ");
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
            strSql.Append(" FROM HouseVsComment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

