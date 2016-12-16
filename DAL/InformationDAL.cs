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
    //Information
    public partial class InformationDAL
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
            strSql.Append(" FROM Information ");
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
        public bool Add(InformationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Information(");
            strSql.Append("Email,QQ,Tel,InformationImgId,ContactName,InformationClassId,InformationTypeId,InformationTitle,InformationContent,InformationMemo,CreateTime,CreateUserId,Property");
            strSql.Append(") values (");
            strSql.Append("@Email,@QQ,@Tel,@InformationImgId,@ContactName,@InformationClassId,@InformationTypeId,@InformationTitle,@InformationContent,@InformationMemo,@CreateTime,@CreateUserId,@Property");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Email", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@QQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Tel", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InformationImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InformationClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InformationTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InformationTitle", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@InformationContent", SqlDbType.NVarChar,2500) ,            
                        new SqlParameter("@InformationMemo", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Property", SqlDbType.Xml,-1)             
              
            };

            parameters[0].Value = model.Email;
            parameters[1].Value = model.QQ;
            parameters[2].Value = model.Tel;
            parameters[3].Value = model.InformationImgId;
            parameters[4].Value = model.ContactName;
            parameters[5].Value = model.InformationClassId;
            parameters[6].Value = model.InformationTypeId;
            parameters[7].Value = model.InformationTitle;
            parameters[8].Value = model.InformationContent;
            parameters[9].Value = model.InformationMemo;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.CreateUserId;
            parameters[12].Value = model.Property;

            bool result = false;
            try
            {
                model.InformationId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "InformationId", parameters));
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
        public bool Update(InformationModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Information set ");

            strSql.Append(" Email = @Email , ");
            strSql.Append(" QQ = @QQ , ");
            strSql.Append(" Tel = @Tel , ");
            strSql.Append(" InformationImgId = @InformationImgId , ");
            strSql.Append(" ContactName = @ContactName , ");
            strSql.Append(" InformationClassId = @InformationClassId , ");
            strSql.Append(" InformationTypeId = @InformationTypeId , ");
            strSql.Append(" InformationTitle = @InformationTitle , ");
            strSql.Append(" InformationContent = @InformationContent , ");
            strSql.Append(" InformationMemo = @InformationMemo , ");
            strSql.Append(" CreateTime = @CreateTime , ");
          //  strSql.Append(" CreateUserId = @CreateUserId , ");
            strSql.Append(" Property = @Property  ");
            strSql.Append(" where InformationId=@InformationId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@InformationId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Email", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@QQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Tel", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InformationImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InformationClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InformationTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InformationTitle", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@InformationContent", SqlDbType.NVarChar,2500) ,            
                        new SqlParameter("@InformationMemo", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Property", SqlDbType.Xml,-1)             
              
            };

            parameters[0].Value = model.InformationId;
            parameters[1].Value = model.Email;
            parameters[2].Value = model.QQ;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.InformationImgId;
            parameters[5].Value = model.ContactName;
            parameters[6].Value = model.InformationClassId;
            parameters[7].Value = model.InformationTypeId;
            parameters[8].Value = model.InformationTitle;
            parameters[9].Value = model.InformationContent;
            parameters[10].Value = model.InformationMemo;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.CreateUserId;
            parameters[13].Value = model.Property; try
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
        public InformationModel GetModel(decimal InformationId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select InformationId, Email, QQ, Tel, InformationImgId, ContactName, InformationClassId, InformationTypeId, InformationTitle, InformationContent, InformationMemo, CreateTime, CreateUserId, Property  ");
            strSql.Append("  from Information ");
            strSql.Append(" where InformationId=@InformationId");
            SqlParameter[] parameters = {
					new SqlParameter("@InformationId", SqlDbType.Decimal)
			};
            parameters[0].Value = InformationId;


            InformationModel model = new InformationModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["InformationId"].ToString() != "")
                {
                    model.InformationId = decimal.Parse(ds.Tables[0].Rows[0]["InformationId"].ToString());
                }
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                model.InformationImgId = ds.Tables[0].Rows[0]["InformationImgId"].ToString();
                model.ContactName = ds.Tables[0].Rows[0]["ContactName"].ToString();
                if (ds.Tables[0].Rows[0]["InformationClassId"].ToString() != "")
                {
                    model.InformationClassId = decimal.Parse(ds.Tables[0].Rows[0]["InformationClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InformationTypeId"].ToString() != "")
                {
                    model.InformationTypeId = decimal.Parse(ds.Tables[0].Rows[0]["InformationTypeId"].ToString());
                }
                model.InformationTitle = ds.Tables[0].Rows[0]["InformationTitle"].ToString();
                model.InformationContent = ds.Tables[0].Rows[0]["InformationContent"].ToString();
                model.InformationMemo = ds.Tables[0].Rows[0]["InformationMemo"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUserId = ds.Tables[0].Rows[0]["CreateUserId"].ToString();
                model.Property = ds.Tables[0].Rows[0]["Property"].ToString();

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
            strSql.Append("delete from Information ");
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
            strSql.Append(" FROM InformationView  WITH(NOLOCK)  ");
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


        public DataSet GetInformationInfoById(decimal InformationId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" select * from dbo.InformationView  WITH(NOLOCK) where  InformationId='" + InformationId + "' ");

            s.Append(" select * from InformationVsImg where InformationId ='" + InformationId + "' ");

            s.Append(" select * from dbo.InformationVsKeyWord where  InformationId ='" + InformationId + "'  ");
            DataSet ds = helper.ExecSqlReDs(s.ToString());
            ds.Tables[0].TableName = "InformationInfo";
            return ds;



        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Information  WITH(NOLOCK) ");
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
            strSql.Append(" FROM Information  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

