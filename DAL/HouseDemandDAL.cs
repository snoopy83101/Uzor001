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
    //HouseDemand
    public partial class HouseDemandDAL
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
        public bool Add(HouseDemandModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HouseDemand(");
            strSql.Append("HouseDemandId,Hyangtai,HouseDemandMemo,ContactName,ContactTell,ContactPhone,ContactEmail,ContactQQ,CommunityTitle,CommunityId,TownId,HouseDemandTitle,CreateTime,CreateUser,HouseDemandTypeId,BeginPrice,EndPrice,Hshi,Hting,Hchu,Hwei");
            strSql.Append(") values (");
            strSql.Append("@HouseDemandId,@Hyangtai,@HouseDemandMemo,@ContactName,@ContactTell,@ContactPhone,@ContactEmail,@ContactQQ,@CommunityTitle,@CommunityId,@TownId,@HouseDemandTitle,@CreateTime,@CreateUser,@HouseDemandTypeId,@BeginPrice,@EndPrice,@Hshi,@Hting,@Hchu,@Hwei");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HouseDemandId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Hyangtai", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseDemandMemo", SqlDbType.NVarChar,1000) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactPhone", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactEmail", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@ContactQQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityTitle", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HouseDemandTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HouseDemandTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@BeginPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@EndPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Hshi", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hting", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hchu", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hwei", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.HouseDemandId;
            parameters[1].Value = model.Hyangtai;
            parameters[2].Value = model.HouseDemandMemo;
            parameters[3].Value = model.ContactName;
            parameters[4].Value = model.ContactTell;
            parameters[5].Value = model.ContactPhone;
            parameters[6].Value = model.ContactEmail;
            parameters[7].Value = model.ContactQQ;
            parameters[8].Value = model.CommunityTitle;
            parameters[9].Value = model.CommunityId;
            parameters[10].Value = model.TownId;
            parameters[11].Value = model.HouseDemandTitle;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.CreateUser;
            parameters[14].Value = model.HouseDemandTypeId;
            parameters[15].Value = model.BeginPrice;
            parameters[16].Value = model.EndPrice;
            parameters[17].Value = model.Hshi;
            parameters[18].Value = model.Hting;
            parameters[19].Value = model.Hchu;
            parameters[20].Value = model.Hwei;

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
        public bool Update(HouseDemandModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HouseDemand set ");

            strSql.Append(" HouseDemandId = @HouseDemandId , ");
            strSql.Append(" Hyangtai = @Hyangtai , ");
            strSql.Append(" HouseDemandMemo = @HouseDemandMemo , ");
            strSql.Append(" ContactName = @ContactName , ");
            strSql.Append(" ContactTell = @ContactTell , ");
            strSql.Append(" ContactPhone = @ContactPhone , ");
            strSql.Append(" ContactEmail = @ContactEmail , ");
            strSql.Append(" ContactQQ = @ContactQQ , ");
            strSql.Append(" CommunityTitle = @CommunityTitle , ");
            strSql.Append(" CommunityId = @CommunityId , ");
            strSql.Append(" TownId = @TownId , ");
            strSql.Append(" HouseDemandTitle = @HouseDemandTitle , ");
        //    strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" HouseDemandTypeId = @HouseDemandTypeId , ");
            strSql.Append(" BeginPrice = @BeginPrice , ");
            strSql.Append(" EndPrice = @EndPrice , ");
            strSql.Append(" Hshi = @Hshi , ");
            strSql.Append(" Hting = @Hting , ");
            strSql.Append(" Hchu = @Hchu , ");
            strSql.Append(" Hwei = @Hwei  ");
            strSql.Append(" where HouseDemandId=@HouseDemandId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HouseDemandId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Hyangtai", SqlDbType.Int,4) ,            
                        new SqlParameter("@HouseDemandMemo", SqlDbType.NVarChar,1000) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactPhone", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactEmail", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@ContactQQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityTitle", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CommunityId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HouseDemandTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HouseDemandTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@BeginPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@EndPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Hshi", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hting", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hchu", SqlDbType.Int,4) ,            
                        new SqlParameter("@Hwei", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.HouseDemandId;
            parameters[1].Value = model.Hyangtai;
            parameters[2].Value = model.HouseDemandMemo;
            parameters[3].Value = model.ContactName;
            parameters[4].Value = model.ContactTell;
            parameters[5].Value = model.ContactPhone;
            parameters[6].Value = model.ContactEmail;
            parameters[7].Value = model.ContactQQ;
            parameters[8].Value = model.CommunityTitle;
            parameters[9].Value = model.CommunityId;
            parameters[10].Value = model.TownId;
            parameters[11].Value = model.HouseDemandTitle;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.CreateUser;
            parameters[14].Value = model.HouseDemandTypeId;
            parameters[15].Value = model.BeginPrice;
            parameters[16].Value = model.EndPrice;
            parameters[17].Value = model.Hshi;
            parameters[18].Value = model.Hting;
            parameters[19].Value = model.Hchu;
            parameters[20].Value = model.Hwei; try
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
            strSql.Append("delete from HouseDemand ");
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
            strSql.Append(" FROM HouseDemandView  WITH(NOLOCK)  ");
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


        public DataSet GetHouseDemandInfoById(string HouseDemandId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM HouseDemandView  WITH(NOLOCK)  where HouseDemandId='" + HouseDemandId + "' ");
  
            return helper.ExecSqlReDs(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM HouseDemandView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM HouseDemand ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

