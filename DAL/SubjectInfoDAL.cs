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
    //SubjectInfo
    public partial class SubjectInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.SubjectInfo ");
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
        public bool Add(SubjectInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.SubjectInfo (");
            strSql.Append("DoneTime,SubjectClassId,SubjectTitle,SubjectContent,CreateTime,SubjectStatusId,MemberId,ReKey,ReKey2,Memo");
            strSql.Append(") values (");
            strSql.Append("@DoneTime,@SubjectClassId,@SubjectTitle,@SubjectContent,@CreateTime,@SubjectStatusId,@MemberId,@ReKey,@ReKey2,@Memo");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@SubjectClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@SubjectTitle", SqlDbType.VarChar,500) ,
                        new SqlParameter("@SubjectContent", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@SubjectStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey2", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500)

            };

            parameters[0].Value = model.DoneTime;
            parameters[1].Value = model.SubjectClassId;
            parameters[2].Value = model.SubjectTitle;
            parameters[3].Value = model.SubjectContent;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.SubjectStatusId;
            parameters[6].Value = model.MemberId;
            parameters[7].Value = model.ReKey;
            parameters[8].Value = model.ReKey2;
            parameters[9].Value = model.Memo;

            bool result = false;
            try
            {


                model.SubjectId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "SubjectId", parameters));


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
        public bool Update(SubjectInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.SubjectInfo set ");

            strSql.Append(" DoneTime = @DoneTime , ");
            strSql.Append(" SubjectClassId = @SubjectClassId , ");
            strSql.Append(" SubjectTitle = @SubjectTitle , ");
            strSql.Append(" SubjectContent = @SubjectContent , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" SubjectStatusId = @SubjectStatusId , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" ReKey2 = @ReKey2 , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where SubjectId=@SubjectId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@SubjectId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@SubjectClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@SubjectTitle", SqlDbType.VarChar,500) ,
                        new SqlParameter("@SubjectContent", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@SubjectStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey2", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500)

            };

            parameters[0].Value = model.SubjectId;
            parameters[1].Value = model.DoneTime;
            parameters[2].Value = model.SubjectClassId;
            parameters[3].Value = model.SubjectTitle;
            parameters[4].Value = model.SubjectContent;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.SubjectStatusId;
            parameters[7].Value = model.MemberId;
            parameters[8].Value = model.ReKey;
            parameters[9].Value = model.ReKey2;
            parameters[10].Value = model.Memo; try
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
        public SubjectInfoModel GetModel(decimal SubjectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SubjectId, DoneTime, SubjectClassId, SubjectTitle, SubjectContent, CreateTime, SubjectStatusId, MemberId, ReKey, ReKey2, Memo  ");
            strSql.Append("  from CORE.dbo.SubjectInfo ");
            strSql.Append(" where SubjectId=@SubjectId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SubjectId", SqlDbType.Decimal)
            };
            parameters[0].Value = SubjectId;


            SubjectInfoModel model = new SubjectInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SubjectId"].ToString() != "")
                {
                    model.SubjectId = decimal.Parse(ds.Tables[0].Rows[0]["SubjectId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DoneTime"].ToString() != "")
                {
                    model.DoneTime = DateTime.Parse(ds.Tables[0].Rows[0]["DoneTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubjectClassId"].ToString() != "")
                {
                    model.SubjectClassId = int.Parse(ds.Tables[0].Rows[0]["SubjectClassId"].ToString());
                }
                model.SubjectTitle = ds.Tables[0].Rows[0]["SubjectTitle"].ToString();
                model.SubjectContent = ds.Tables[0].Rows[0]["SubjectContent"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubjectStatusId"].ToString() != "")
                {
                    model.SubjectStatusId = int.Parse(ds.Tables[0].Rows[0]["SubjectStatusId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                model.ReKey2 = ds.Tables[0].Rows[0]["ReKey2"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();

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
            strSql.Append("delete from CORE.dbo.SubjectInfo ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.SubjectInfo  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
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
            strSql.Append(" FROM CORE.dbo.SubjectInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.SubjectInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.SubjectInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

