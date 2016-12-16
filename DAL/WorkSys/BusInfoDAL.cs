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
    //BusInfo
    public partial class BusInfoDAL
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
            strSql.Append(" FROM  YYHD.dbo.BusInfo ");
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
        public bool Add(BusInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.BusInfo (");
            strSql.Append("BusId,BusCode,BusNo,BusName,Lng,Lat,Memo");
            strSql.Append(") values (");
            strSql.Append("@BusId,@BusCode,@BusNo,@BusName,@Lng,@Lat,@Memo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BusId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BusCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.BusId;
            parameters[1].Value = model.BusCode;
            parameters[2].Value = model.BusNo;
            parameters[3].Value = model.BusName;
            parameters[4].Value = model.Lng;
            parameters[5].Value = model.Lat;
            parameters[6].Value = model.Memo;

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
        public bool Update(BusInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.BusInfo set ");

            strSql.Append(" BusId = @BusId , ");
            strSql.Append(" BusCode = @BusCode , ");
            strSql.Append(" BusNo = @BusNo , ");
            strSql.Append(" BusName = @BusName , ");
            strSql.Append(" Lng = @Lng , ");
            strSql.Append(" Lat = @Lat , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where BusId=@BusId and BusCode=@BusCode and BusNo=@BusNo and BusName=@BusName and Lng=@Lng and Lat=@Lat and Memo=@Memo  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BusId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BusCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BusName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.BusId;
            parameters[1].Value = model.BusCode;
            parameters[2].Value = model.BusNo;
            parameters[3].Value = model.BusName;
            parameters[4].Value = model.Lng;
            parameters[5].Value = model.Lat;
            parameters[6].Value = model.Memo; try
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
        public BusInfoModel GetModel(decimal BusId, string BusCode, string BusNo, string BusName, decimal Lng, decimal Lat, string Memo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BusId, BusCode, BusNo, BusName, Lng, Lat, Memo  ");
            strSql.Append("  from YYHD.dbo.BusInfo ");
            strSql.Append(" where BusId=@BusId and BusCode=@BusCode and BusNo=@BusNo and BusName=@BusName and Lng=@Lng and Lat=@Lat and Memo=@Memo ");
            SqlParameter[] parameters = {
					new SqlParameter("@BusId", SqlDbType.Decimal,9),
					new SqlParameter("@BusCode", SqlDbType.VarChar,50),
					new SqlParameter("@BusNo", SqlDbType.VarChar,50),
					new SqlParameter("@BusName", SqlDbType.VarChar,50),
					new SqlParameter("@Lng", SqlDbType.Decimal,9),
					new SqlParameter("@Lat", SqlDbType.Decimal,9),
					new SqlParameter("@Memo", SqlDbType.VarChar,500)			};
            parameters[0].Value = BusId;
            parameters[1].Value = BusCode;
            parameters[2].Value = BusNo;
            parameters[3].Value = BusName;
            parameters[4].Value = Lng;
            parameters[5].Value = Lat;
            parameters[6].Value = Memo;


            BusInfoModel model = new BusInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["BusId"].ToString() != "")
                {
                    model.BusId = decimal.Parse(ds.Tables[0].Rows[0]["BusId"].ToString());
                }
                model.BusCode = ds.Tables[0].Rows[0]["BusCode"].ToString();
                model.BusNo = ds.Tables[0].Rows[0]["BusNo"].ToString();
                model.BusName = ds.Tables[0].Rows[0]["BusName"].ToString();
                if (ds.Tables[0].Rows[0]["Lng"].ToString() != "")
                {
                    model.Lng = decimal.Parse(ds.Tables[0].Rows[0]["Lng"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lat"].ToString() != "")
                {
                    model.Lat = decimal.Parse(ds.Tables[0].Rows[0]["Lat"].ToString());
                }
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
            strSql.Append("delete from YYHD.dbo.BusInfo ");
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
            strSql.Append(" FROM YYHD.dbo.BusInfo  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("YYHD.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM YYHD.dbo.BusInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.BusInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

