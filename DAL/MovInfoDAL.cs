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
    //MovInfo
    public partial class MovInfoDAL
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
            strSql.Append(" FROM MovInfo ");
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
        public bool Add(MovInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MovInfo(");
            strSql.Append("MovId,PianChang,MovClass,Invalid,MovTitle,MovContent,CreateTime,MovType,MovBiaoQian,MovImgUrl,ShiGuangId,ShangYingTime");
            strSql.Append(") values (");
            strSql.Append("@MovId,@PianChang,@MovClass,@Invalid,@MovTitle,@MovContent,@CreateTime,@MovType,@MovBiaoQian,@MovImgUrl,@ShiGuangId,@ShangYingTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MovId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PianChang", SqlDbType.Int,4) ,            
                        new SqlParameter("@MovClass", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@MovTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@MovContent", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@MovType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@MovBiaoQian", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@MovImgUrl", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ShiGuangId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ShangYingTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.MovId;
            parameters[1].Value = model.PianChang;
            parameters[2].Value = model.MovClass;
            parameters[3].Value = model.Invalid;
            parameters[4].Value = model.MovTitle;
            parameters[5].Value = model.MovContent;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.MovType;
            parameters[8].Value = model.MovBiaoQian;
            parameters[9].Value = model.MovImgUrl;
            parameters[10].Value = model.ShiGuangId;
            parameters[11].Value = model.ShangYingTime;

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
        public bool Update(MovInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MovInfo set ");

            strSql.Append(" MovId = @MovId , ");
            strSql.Append(" PianChang = @PianChang , ");
            strSql.Append(" MovClass = @MovClass , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" MovTitle = @MovTitle , ");
            strSql.Append(" MovContent = @MovContent , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" MovType = @MovType , ");
            strSql.Append(" MovBiaoQian = @MovBiaoQian , ");
            strSql.Append(" MovImgUrl = @MovImgUrl , ");
            strSql.Append(" ShiGuangId = @ShiGuangId , ");
            strSql.Append(" ShangYingTime = @ShangYingTime  ");
            strSql.Append(" where MovId=@MovId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MovId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PianChang", SqlDbType.Int,4) ,            
                        new SqlParameter("@MovClass", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@MovTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@MovContent", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@MovType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@MovBiaoQian", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@MovImgUrl", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ShiGuangId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ShangYingTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.MovId;
            parameters[1].Value = model.PianChang;
            parameters[2].Value = model.MovClass;
            parameters[3].Value = model.Invalid;
            parameters[4].Value = model.MovTitle;
            parameters[5].Value = model.MovContent;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.MovType;
            parameters[8].Value = model.MovBiaoQian;
            parameters[9].Value = model.MovImgUrl;
            parameters[10].Value = model.ShiGuangId;
            parameters[11].Value = model.ShangYingTime; try
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
        public MovInfoModel GetModel(string MovId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MovId, PianChang, MovClass, Invalid, MovTitle, MovContent, CreateTime, MovType, MovBiaoQian, MovImgUrl, ShiGuangId, ShangYingTime  ");
            strSql.Append("  from MovInfo ");
            strSql.Append(" where MovId=@MovId ");
            SqlParameter[] parameters = {
					new SqlParameter("@MovId", SqlDbType.VarChar,50)			};
            parameters[0].Value = MovId;


            MovInfoModel model = new MovInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.MovId = ds.Tables[0].Rows[0]["MovId"].ToString();
                if (ds.Tables[0].Rows[0]["PianChang"].ToString() != "")
                {
                    model.PianChang = int.Parse(ds.Tables[0].Rows[0]["PianChang"].ToString());
                }
                model.MovClass = ds.Tables[0].Rows[0]["MovClass"].ToString();
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
                model.MovTitle = ds.Tables[0].Rows[0]["MovTitle"].ToString();
                model.MovContent = ds.Tables[0].Rows[0]["MovContent"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.MovType = ds.Tables[0].Rows[0]["MovType"].ToString();
                model.MovBiaoQian = ds.Tables[0].Rows[0]["MovBiaoQian"].ToString();
                model.MovImgUrl = ds.Tables[0].Rows[0]["MovImgUrl"].ToString();
                model.ShiGuangId = ds.Tables[0].Rows[0]["ShiGuangId"].ToString();
                if (ds.Tables[0].Rows[0]["ShangYingTime"].ToString() != "")
                {
                    model.ShangYingTime = DateTime.Parse(ds.Tables[0].Rows[0]["ShangYingTime"].ToString());
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
            strSql.Append("delete from MovInfo ");
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
            strSql.Append(" FROM MovInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM MovInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM MovInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

