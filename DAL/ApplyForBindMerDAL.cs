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
    //ApplyForBindMer
    public partial class ApplyForBindMerDAL
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
        public bool Add(ApplyForBindMerModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ApplyForBindMer(");
            strSql.Append("OrganizationNo,Tell,qq,BusinessImgId,email,Memo,BindUserId,BindMerchantId,CreateTime,Status,ContactName,OrganizationName,BusinessNo,LegalName");
            strSql.Append(") values (");
            strSql.Append("@OrganizationNo,@Tell,@qq,@BusinessImgId,@email,@Memo,@BindUserId,@BindMerchantId,@CreateTime,@Status,@ContactName,@OrganizationName,@BusinessNo,@LegalName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@OrganizationNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Tell", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@qq", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusinessImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@email", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@BindUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BindMerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrganizationName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusinessNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LegalName", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.OrganizationNo;
            parameters[1].Value = model.Tell;
            parameters[2].Value = model.qq;
            parameters[3].Value = model.BusinessImgId;
            parameters[4].Value = model.email;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.BindUserId;
            parameters[7].Value = model.BindMerchantId;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.ContactName;
            parameters[11].Value = model.OrganizationName;
            parameters[12].Value = model.BusinessNo;
            parameters[13].Value = model.LegalName;

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
        public bool Update(ApplyForBindMerModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ApplyForBindMer set ");

            strSql.Append(" OrganizationNo = @OrganizationNo , ");
            strSql.Append(" Tell = @Tell , ");
            strSql.Append(" qq = @qq , ");
            strSql.Append(" BusinessImgId = @BusinessImgId , ");
            strSql.Append(" email = @email , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" BindUserId = @BindUserId , ");
            strSql.Append(" BindMerchantId = @BindMerchantId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" ContactName = @ContactName , ");
            strSql.Append(" OrganizationName = @OrganizationName , ");
            strSql.Append(" BusinessNo = @BusinessNo , ");
            strSql.Append(" LegalName = @LegalName  ");
            strSql.Append(" where AutoId=@AutoId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AutoId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@OrganizationNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Tell", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@qq", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusinessImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@email", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@BindUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BindMerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrganizationName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusinessNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LegalName", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.AutoId;
            parameters[1].Value = model.OrganizationNo;
            parameters[2].Value = model.Tell;
            parameters[3].Value = model.qq;
            parameters[4].Value = model.BusinessImgId;
            parameters[5].Value = model.email;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.BindUserId;
            parameters[8].Value = model.BindMerchantId;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.ContactName;
            parameters[12].Value = model.OrganizationName;
            parameters[13].Value = model.BusinessNo;
            parameters[14].Value = model.LegalName; try
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
        public ApplyForBindMerModel GetModel(decimal AutoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoId, OrganizationNo, Tell, qq, BusinessImgId, email, Memo, BindUserId, BindMerchantId, CreateTime, Status, ContactName, OrganizationName, BusinessNo, LegalName  ");
            strSql.Append("  from ApplyForBindMer ");
            strSql.Append(" where AutoId=@AutoId");
            SqlParameter[] parameters = {
					new SqlParameter("@AutoId", SqlDbType.Decimal)
			};
            parameters[0].Value = AutoId;


            ApplyForBindMerModel model = new ApplyForBindMerModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AutoId"].ToString() != "")
                {
                    model.AutoId = decimal.Parse(ds.Tables[0].Rows[0]["AutoId"].ToString());
                }
                model.OrganizationNo = ds.Tables[0].Rows[0]["OrganizationNo"].ToString();
                model.Tell = ds.Tables[0].Rows[0]["Tell"].ToString();
                model.qq = ds.Tables[0].Rows[0]["qq"].ToString();
                model.BusinessImgId = ds.Tables[0].Rows[0]["BusinessImgId"].ToString();
                model.email = ds.Tables[0].Rows[0]["email"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                model.BindUserId = ds.Tables[0].Rows[0]["BindUserId"].ToString();
                if (ds.Tables[0].Rows[0]["BindMerchantId"].ToString() != "")
                {
                    model.BindMerchantId = decimal.Parse(ds.Tables[0].Rows[0]["BindMerchantId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.ContactName = ds.Tables[0].Rows[0]["ContactName"].ToString();
                model.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();
                model.BusinessNo = ds.Tables[0].Rows[0]["BusinessNo"].ToString();
                model.LegalName = ds.Tables[0].Rows[0]["LegalName"].ToString();

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
            strSql.Append("delete from ApplyForBindMer ");
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
            strSql.Append(" FROM ApplyForBindMerView ");
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
            strSql.Append(" FROM ApplyForBindMerView ");
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
            strSql.Append(" FROM ApplyForBindMer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

