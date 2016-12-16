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
    //PinPaiInfo
    public partial class PinPaiInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.PinPaiInfo ");
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
        public bool Add(PinPaiInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.PinPaiInfo (");
            strSql.Append("MerId,InputCode,PinPaiName,PinPaiJianJie,PinPaiMemo,PinPaiImgId,OrderNo,PinPaiClassId,PinPaiTypeId");
            strSql.Append(") values (");
            strSql.Append("@MerId,@InputCode,@PinPaiName,@PinPaiJianJie,@PinPaiMemo,@PinPaiImgId,@OrderNo,@PinPaiClassId,@PinPaiTypeId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PinPaiName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@PinPaiJianJie", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@PinPaiMemo", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@PinPaiImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@PinPaiClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PinPaiTypeId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.MerId;
            parameters[1].Value = model.InputCode;
            parameters[2].Value = model.PinPaiName;
            parameters[3].Value = model.PinPaiJianJie;
            parameters[4].Value = model.PinPaiMemo;
            parameters[5].Value = model.PinPaiImgId;
            parameters[6].Value = model.OrderNo;
            parameters[7].Value = model.PinPaiClassId;
            parameters[8].Value = model.PinPaiTypeId;         
	

            bool result = false;
            try
            {
                model.PinPaiId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "PinPaiId", parameters));
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
        public bool Update(PinPaiInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.PinPaiInfo set ");

            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" InputCode = @InputCode , ");
            strSql.Append(" PinPaiName = @PinPaiName , ");
            strSql.Append(" PinPaiJianJie = @PinPaiJianJie , ");
            strSql.Append(" PinPaiMemo = @PinPaiMemo , ");
            strSql.Append(" PinPaiImgId = @PinPaiImgId , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" PinPaiClassId = @PinPaiClassId , ");
            strSql.Append(" PinPaiTypeId = @PinPaiTypeId  ");
            strSql.Append(" where PinPaiId=@PinPaiId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@PinPaiId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PinPaiName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@PinPaiJianJie", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@PinPaiMemo", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@PinPaiImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@PinPaiClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PinPaiTypeId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.PinPaiId;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.InputCode;
            parameters[3].Value = model.PinPaiName;
            parameters[4].Value = model.PinPaiJianJie;
            parameters[5].Value = model.PinPaiMemo;
            parameters[6].Value = model.PinPaiImgId;
            parameters[7].Value = model.OrderNo;
            parameters[8].Value = model.PinPaiClassId;
            parameters[9].Value = model.PinPaiTypeId; try
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
        public PinPaiInfoModel GetModel(decimal PinPaiId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PinPaiId, MerId, InputCode, PinPaiName, PinPaiJianJie, PinPaiMemo, PinPaiImgId, OrderNo, PinPaiClassId, PinPaiTypeId  ");
            strSql.Append("  from CORE.dbo.PinPaiInfo ");
            strSql.Append(" where PinPaiId=@PinPaiId ");
            SqlParameter[] parameters = {
					new SqlParameter("@PinPaiId", SqlDbType.Decimal,9)			};
            parameters[0].Value = PinPaiId;


            PinPaiInfoModel model = new PinPaiInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PinPaiId"].ToString() != "")
                {
                    model.PinPaiId = decimal.Parse(ds.Tables[0].Rows[0]["PinPaiId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                model.InputCode = ds.Tables[0].Rows[0]["InputCode"].ToString();
                model.PinPaiName = ds.Tables[0].Rows[0]["PinPaiName"].ToString();
                model.PinPaiJianJie = ds.Tables[0].Rows[0]["PinPaiJianJie"].ToString();
                model.PinPaiMemo = ds.Tables[0].Rows[0]["PinPaiMemo"].ToString();
                model.PinPaiImgId = ds.Tables[0].Rows[0]["PinPaiImgId"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PinPaiClassId"].ToString() != "")
                {
                    model.PinPaiClassId = decimal.Parse(ds.Tables[0].Rows[0]["PinPaiClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PinPaiTypeId"].ToString() != "")
                {
                    model.PinPaiTypeId = decimal.Parse(ds.Tables[0].Rows[0]["PinPaiTypeId"].ToString());
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
            strSql.Append("delete from CORE.dbo.PinPaiInfo ");
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
            strSql.Append(" FROM CORE.dbo.PinPaiView  WITH(NOLOCK)  ");
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
        public DataSet GetPinPaiInfo(decimal PinPaiId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.PinPaiView  WITH(NOLOCK) ");

            strSql.Append(" where PinPaiId='" + PinPaiId + "' ");

            strSql.Append(" SELECT img.ImgId,img.ImgUrl,PinPaiId FROM dbo.PinPaivsImg  pvi WITH(NOLOCK) LEFT JOIN  dbo.ImageInfo img WITH(NOLOCK) ON pvi.ImgId=img.ImgId   where  PinPaiId='" + PinPaiId + "' ");

            return helper.ExecSqlReDs(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.PinPaiView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.PinPaiView  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

