using DBTools;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    //AddressInfo
    public partial class AddressInfoDAL
    {
        #region //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();

        #endregion //数据操作

        /// <summary>
        /// 检查是否存在
        /// </summary>
        public int ExInt(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) ");
            strSql.Append(" FROM  CORE.dbo.AddressInfo ");
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
        public bool Add(AddressInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.AddressInfo (");
            strSql.Append("SiteId,Attach,LastTime,TownId,Memo,OrderNo,Tel,ContactName,Invalid,MemberId,IsDefault");
            strSql.Append(") values (");
            strSql.Append("@SiteId,@Attach,@LastTime,@TownId,@Memo,@OrderNo,@Tel,@ContactName,@Invalid,@MemberId,@IsDefault");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Attach", SqlDbType.VarChar,200) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Tel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@IsDefault", SqlDbType.Bit,1)

            };

            parameters[0].Value = model.SiteId;
            parameters[1].Value = model.Attach;
            parameters[2].Value = model.LastTime;
            parameters[3].Value = model.TownId;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.OrderNo;
            parameters[6].Value = model.Tel;
            parameters[7].Value = model.ContactName;
            parameters[8].Value = model.Invalid;
            parameters[9].Value = model.MemberId;
            parameters[10].Value = model.IsDefault;

            bool result = false;
            try
            {


                model.AddressId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "AddressId", parameters));


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
        public bool Update(AddressInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.AddressInfo set ");

            strSql.Append(" SiteId = @SiteId , ");
            strSql.Append(" Attach = @Attach , ");
            strSql.Append(" LastTime = @LastTime , ");
            strSql.Append(" TownId = @TownId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" Tel = @Tel , ");
            strSql.Append(" ContactName = @ContactName , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" IsDefault = @IsDefault  ");
            strSql.Append(" where AddressId=@AddressId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@AddressId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Attach", SqlDbType.VarChar,200) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Tel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@IsDefault", SqlDbType.Bit,1)

            };

            parameters[0].Value = model.AddressId;
            parameters[1].Value = model.SiteId;
            parameters[2].Value = model.Attach;
            parameters[3].Value = model.LastTime;
            parameters[4].Value = model.TownId;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.OrderNo;
            parameters[7].Value = model.Tel;
            parameters[8].Value = model.ContactName;
            parameters[9].Value = model.Invalid;
            parameters[10].Value = model.MemberId;
            parameters[11].Value = model.IsDefault; try
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
        public AddressInfoModel GetModel(decimal AddressId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AddressId, SiteId, Attach, LastTime, TownId, Memo, OrderNo, Tel, ContactName, Invalid, MemberId, IsDefault  ");
            strSql.Append("  from CORE.dbo.AddressInfo ");
            strSql.Append(" where AddressId=@AddressId");
            SqlParameter[] parameters = {
                    new SqlParameter("@AddressId", SqlDbType.Decimal)
            };
            parameters[0].Value = AddressId;


            AddressInfoModel model = new AddressInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AddressId"].ToString() != "")
                {
                    model.AddressId = decimal.Parse(ds.Tables[0].Rows[0]["AddressId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SiteId"].ToString() != "")
                {
                    model.SiteId = decimal.Parse(ds.Tables[0].Rows[0]["SiteId"].ToString());
                }
                model.Attach = ds.Tables[0].Rows[0]["Attach"].ToString();
                if (ds.Tables[0].Rows[0]["LastTime"].ToString() != "")
                {
                    model.LastTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TownId"].ToString() != "")
                {
                    model.TownId = decimal.Parse(ds.Tables[0].Rows[0]["TownId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                model.ContactName = ds.Tables[0].Rows[0]["ContactName"].ToString();
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
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDefault"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDefault"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDefault"].ToString().ToLower() == "true"))
                    {
                        model.IsDefault = true;
                    }
                    else
                    {
                        model.IsDefault = false;
                    }
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
            strSql.Append("delete from CORE.dbo.AddressInfo ");
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
            strSql.Append(" FROM CORE.dbo.AddressView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.AddressView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.AddressInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }

        public DataSet GetAddressInfo(decimal AddressId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.AddressView  WITH(NOLOCK) where AddressId='" + AddressId + "' ");

            return helper.ExecSqlReDs(strSql.ToString());
        }
    }
}