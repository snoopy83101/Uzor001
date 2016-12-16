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
    //SlideShow
    public partial class SlideShowDAL
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
            strSql.Append(" FROM SlideShow ");
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
        public bool Add(SlideShowModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SlideShow(");
            strSql.Append("Url,Event,SlideshowTitle,SlideshowMemo,CreateTime,CreateUser,OrderNo,lv,SlideshowType,SlideshowImgId");
            strSql.Append(") values (");
            strSql.Append("@Url,@Event,@SlideshowTitle,@SlideshowMemo,@CreateTime,@CreateUser,@OrderNo,@lv,@SlideshowType,@SlideshowImgId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Url", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@Event", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SlideshowTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@SlideshowMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@lv", SqlDbType.Int,4) ,            
                        new SqlParameter("@SlideshowType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SlideshowImgId", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.Url;
            parameters[1].Value = model.Event;
            parameters[2].Value = model.SlideshowTitle;
            parameters[3].Value = model.SlideshowMemo;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreateUser;
            parameters[6].Value = model.OrderNo;
            parameters[7].Value = model.lv;
            parameters[8].Value = model.SlideshowType;
            parameters[9].Value = model.SlideshowImgId;

            bool result = false;
            try
            {
                model.SlideshowId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "SlideshowId", parameters));
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
        public bool Update(SlideShowModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SlideShow set ");

            strSql.Append(" Url = @Url , ");
            strSql.Append(" Event = @Event , ");
            strSql.Append(" SlideshowTitle = @SlideshowTitle , ");
            strSql.Append(" SlideshowMemo = @SlideshowMemo , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" lv = @lv , ");
            strSql.Append(" SlideshowType = @SlideshowType , ");
            strSql.Append(" SlideshowImgId = @SlideshowImgId  ");
            strSql.Append(" where SlideshowId=@SlideshowId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@SlideshowId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Url", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@Event", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SlideshowTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@SlideshowMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@lv", SqlDbType.Int,4) ,            
                        new SqlParameter("@SlideshowType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SlideshowImgId", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.SlideshowId;
            parameters[1].Value = model.Url;
            parameters[2].Value = model.Event;
            parameters[3].Value = model.SlideshowTitle;
            parameters[4].Value = model.SlideshowMemo;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.CreateUser;
            parameters[7].Value = model.OrderNo;
            parameters[8].Value = model.lv;
            parameters[9].Value = model.SlideshowType;
            parameters[10].Value = model.SlideshowImgId; try
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
        public SlideShowModel GetModel(decimal SlideshowId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SlideshowId, Url, Event, SlideshowTitle, SlideshowMemo, CreateTime, CreateUser, OrderNo, lv, SlideshowType, SlideshowImgId  ");
            strSql.Append("  from SlideShow ");
            strSql.Append(" where SlideshowId=@SlideshowId");
            SqlParameter[] parameters = {
					new SqlParameter("@SlideshowId", SqlDbType.Decimal)
			};
            parameters[0].Value = SlideshowId;


            SlideShowModel model = new SlideShowModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SlideshowId"].ToString() != "")
                {
                    model.SlideshowId = decimal.Parse(ds.Tables[0].Rows[0]["SlideshowId"].ToString());
                }
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.Event = ds.Tables[0].Rows[0]["Event"].ToString();
                model.SlideshowTitle = ds.Tables[0].Rows[0]["SlideshowTitle"].ToString();
                model.SlideshowMemo = ds.Tables[0].Rows[0]["SlideshowMemo"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["lv"].ToString() != "")
                {
                    model.lv = int.Parse(ds.Tables[0].Rows[0]["lv"].ToString());
                }
                model.SlideshowType = ds.Tables[0].Rows[0]["SlideshowType"].ToString();
                model.SlideshowImgId = ds.Tables[0].Rows[0]["SlideshowImgId"].ToString();

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
            strSql.Append("delete from SlideShow ");
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
            strSql.Append(" FROM SlideShow ");
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
            strSql.Append(" FROM SlideShow ");
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
            strSql.Append(" FROM SlideShow ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

