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
    //DingDanInfo
    public partial class DingDanInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.DingDanInfo ");
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
        public bool Add(DingDanInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.DingDanInfo (");
            strSql.Append("DingDanId,AddressId,UseJiFen,DingDanAttr,PeiSongTime1,PeiSongTime2,PeiSongTypeId,PaiSongUserId,PeiHuoUserId,PayTypeId,DingDanTitle,SourseTypeId,PeiHuoTime,BranchId,CreateMember,MerchantId,CreateTime,EnTime,Status,IsDone,Memo");
            strSql.Append(") values (");
            strSql.Append("@DingDanId,@AddressId,@UseJiFen,@DingDanAttr,@PeiSongTime1,@PeiSongTime2,@PeiSongTypeId,@PaiSongUserId,@PeiHuoUserId,@PayTypeId,@DingDanTitle,@SourseTypeId,@PeiHuoTime,@BranchId,@CreateMember,@MerchantId,@CreateTime,@EnTime,@Status,@IsDone,@Memo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AddressId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UseJiFen", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DingDanAttr", SqlDbType.Xml,-1) ,
                        new SqlParameter("@PeiSongTime1", SqlDbType.DateTime) ,
                        new SqlParameter("@PeiSongTime2", SqlDbType.DateTime) ,
                        new SqlParameter("@PeiSongTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PaiSongUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PeiHuoUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PayTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@DingDanTitle", SqlDbType.VarChar,200) ,
                        new SqlParameter("@SourseTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@PeiHuoTime", SqlDbType.DateTime) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateMember", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@EnTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@IsDone", SqlDbType.Bit,1) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000)

            };

            parameters[0].Value = model.DingDanId;
            parameters[1].Value = model.AddressId;
            parameters[2].Value = model.UseJiFen;
            parameters[3].Value = model.DingDanAttr;
            parameters[4].Value = model.PeiSongTime1;
            parameters[5].Value = model.PeiSongTime2;
            parameters[6].Value = model.PeiSongTypeId;
            parameters[7].Value = model.PaiSongUserId;
            parameters[8].Value = model.PeiHuoUserId;
            parameters[9].Value = model.PayTypeId;
            parameters[10].Value = model.DingDanTitle;
            parameters[11].Value = model.SourseTypeId;
            parameters[12].Value = model.PeiHuoTime;
            parameters[13].Value = model.BranchId;
            parameters[14].Value = model.CreateMember;
            parameters[15].Value = model.MerchantId;
            parameters[16].Value = model.CreateTime;
            parameters[17].Value = model.EnTime;
            parameters[18].Value = model.Status;
            parameters[19].Value = model.IsDone;
            parameters[20].Value = model.Memo;

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
        public bool Update(DingDanInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.DingDanInfo set ");

            strSql.Append(" DingDanId = @DingDanId , ");
            strSql.Append(" AddressId = @AddressId , ");
            strSql.Append(" UseJiFen = @UseJiFen , ");
            strSql.Append(" DingDanAttr = @DingDanAttr , ");
            strSql.Append(" PeiSongTime1 = @PeiSongTime1 , ");
            strSql.Append(" PeiSongTime2 = @PeiSongTime2 , ");
            strSql.Append(" PeiSongTypeId = @PeiSongTypeId , ");
            strSql.Append(" PaiSongUserId = @PaiSongUserId , ");
            strSql.Append(" PeiHuoUserId = @PeiHuoUserId , ");
            strSql.Append(" PayTypeId = @PayTypeId , ");
            strSql.Append(" DingDanTitle = @DingDanTitle , ");
            strSql.Append(" SourseTypeId = @SourseTypeId , ");
            strSql.Append(" PeiHuoTime = @PeiHuoTime , ");
            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" CreateMember = @CreateMember , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" EnTime = @EnTime , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" IsDone = @IsDone , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where DingDanId=@DingDanId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AddressId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UseJiFen", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DingDanAttr", SqlDbType.Xml,-1) ,
                        new SqlParameter("@PeiSongTime1", SqlDbType.DateTime) ,
                        new SqlParameter("@PeiSongTime2", SqlDbType.DateTime) ,
                        new SqlParameter("@PeiSongTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PaiSongUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PeiHuoUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PayTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@DingDanTitle", SqlDbType.VarChar,200) ,
                        new SqlParameter("@SourseTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@PeiHuoTime", SqlDbType.DateTime) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateMember", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@EnTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@IsDone", SqlDbType.Bit,1) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000)

            };

            parameters[0].Value = model.DingDanId;
            parameters[1].Value = model.AddressId;
            parameters[2].Value = model.UseJiFen;
            parameters[3].Value = model.DingDanAttr;
            parameters[4].Value = model.PeiSongTime1;
            parameters[5].Value = model.PeiSongTime2;
            parameters[6].Value = model.PeiSongTypeId;
            parameters[7].Value = model.PaiSongUserId;
            parameters[8].Value = model.PeiHuoUserId;
            parameters[9].Value = model.PayTypeId;
            parameters[10].Value = model.DingDanTitle;
            parameters[11].Value = model.SourseTypeId;
            parameters[12].Value = model.PeiHuoTime;
            parameters[13].Value = model.BranchId;
            parameters[14].Value = model.CreateMember;
            parameters[15].Value = model.MerchantId;
            parameters[16].Value = model.CreateTime;
            parameters[17].Value = model.EnTime;
            parameters[18].Value = model.Status;
            parameters[19].Value = model.IsDone;
            parameters[20].Value = model.Memo; try
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
        public DingDanInfoModel GetModel(string DingDanId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DingDanId, AddressId, UseJiFen, DingDanAttr, PeiSongTime1, PeiSongTime2, PeiSongTypeId, PaiSongUserId, PeiHuoUserId, PayTypeId, DingDanTitle, SourseTypeId, PeiHuoTime, BranchId, CreateMember, MerchantId, CreateTime, EnTime, Status, IsDone, Memo  ");
            strSql.Append("  from CORE.dbo.DingDanInfo ");
            strSql.Append(" where DingDanId=@DingDanId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DingDanId", SqlDbType.VarChar,50)            };
            parameters[0].Value = DingDanId;


            DingDanInfoModel model = new DingDanInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.DingDanId = ds.Tables[0].Rows[0]["DingDanId"].ToString();
                if (ds.Tables[0].Rows[0]["AddressId"].ToString() != "")
                {
                    model.AddressId = decimal.Parse(ds.Tables[0].Rows[0]["AddressId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UseJiFen"].ToString() != "")
                {
                    model.UseJiFen = decimal.Parse(ds.Tables[0].Rows[0]["UseJiFen"].ToString());
                }
                model.DingDanAttr = ds.Tables[0].Rows[0]["DingDanAttr"].ToString();
                if (ds.Tables[0].Rows[0]["PeiSongTime1"].ToString() != "")
                {
                    model.PeiSongTime1 = DateTime.Parse(ds.Tables[0].Rows[0]["PeiSongTime1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PeiSongTime2"].ToString() != "")
                {
                    model.PeiSongTime2 = DateTime.Parse(ds.Tables[0].Rows[0]["PeiSongTime2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PeiSongTypeId"].ToString() != "")
                {
                    model.PeiSongTypeId = decimal.Parse(ds.Tables[0].Rows[0]["PeiSongTypeId"].ToString());
                }
                model.PaiSongUserId = ds.Tables[0].Rows[0]["PaiSongUserId"].ToString();
                model.PeiHuoUserId = ds.Tables[0].Rows[0]["PeiHuoUserId"].ToString();
                if (ds.Tables[0].Rows[0]["PayTypeId"].ToString() != "")
                {
                    model.PayTypeId = int.Parse(ds.Tables[0].Rows[0]["PayTypeId"].ToString());
                }
                model.DingDanTitle = ds.Tables[0].Rows[0]["DingDanTitle"].ToString();
                if (ds.Tables[0].Rows[0]["SourseTypeId"].ToString() != "")
                {
                    model.SourseTypeId = int.Parse(ds.Tables[0].Rows[0]["SourseTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PeiHuoTime"].ToString() != "")
                {
                    model.PeiHuoTime = DateTime.Parse(ds.Tables[0].Rows[0]["PeiHuoTime"].ToString());
                }
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                if (ds.Tables[0].Rows[0]["CreateMember"].ToString() != "")
                {
                    model.CreateMember = decimal.Parse(ds.Tables[0].Rows[0]["CreateMember"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnTime"].ToString() != "")
                {
                    model.EnTime = DateTime.Parse(ds.Tables[0].Rows[0]["EnTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDone"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDone"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDone"].ToString().ToLower() == "true"))
                    {
                        model.IsDone = true;
                    }
                    else
                    {
                        model.IsDone = false;
                    }
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
            strSql.Append("delete from CORE.dbo.DingDanInfo ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.DingDanView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


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
            strSql.Append(" FROM CORE.dbo.DingDanView  WITH(NOLOCK)  ");
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


        public DataSet GetDingDanInfoByDingDanId(string DingDanId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.DingDanView  WITH(NOLOCK) where DingDanId='" + DingDanId + "' ");

            strSql.Append(" select * from CORE.dbo.DingDanDetailView  WITH(NOLOCK) where DingDanId='" + DingDanId + "'  ");
            strSql.Append(" SELECT * FROM CORE.dbo.DingDanLogView WHERE DingDanId='" + DingDanId + "' ORDER BY CreateTime ");

            return helper.ExecSqlReDs(strSql.ToString());

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.DingDanInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.DingDanView  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

