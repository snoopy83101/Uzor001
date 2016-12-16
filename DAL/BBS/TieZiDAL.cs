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
    //TieZi
    public partial class TieZiDAL
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
            strSql.Append(" FROM  BBS.DBO.TieZi ");
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
        public bool Add(TieZiModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BBS.dbo.TieZi (");
            strSql.Append("RecommendLv,ForumId,TieZiType,TieZiClass,Ip,Source,ParentTieZiId,HideUser,RepCount,WxOpenId,TieZiTitle,Invalid,RepLastUser,HotCount,YueMingZhong,IsIndex,DingNum,JingHua,TieZiSummary,TieZiContent,CreateTime,UpdateTime,CreateUser,TieZiImgId,MiniImgUrl");
            strSql.Append(") values (");
            strSql.Append("@RecommendLv,@ForumId,@TieZiType,@TieZiClass,@Ip,@Source,@ParentTieZiId,@HideUser,@RepCount,@WxOpenId,@TieZiTitle,@Invalid,@RepLastUser,@HotCount,@YueMingZhong,@IsIndex,@DingNum,@JingHua,@TieZiSummary,@TieZiContent,@CreateTime,@UpdateTime,@CreateUser,@TieZiImgId,@MiniImgUrl");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@ForumId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@TieZiType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TieZiClass", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Ip", SqlDbType.VarChar,18) ,            
                        new SqlParameter("@Source", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ParentTieZiId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HideUser", SqlDbType.Bit,1) ,            
                        new SqlParameter("@RepCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxOpenId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TieZiTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@RepLastUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HotCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@YueMingZhong", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@IsIndex", SqlDbType.Bit,1) ,            
                        new SqlParameter("@DingNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@JingHua", SqlDbType.Int,4) ,            
                        new SqlParameter("@TieZiSummary", SqlDbType.VarChar,300) ,            
                        new SqlParameter("@TieZiContent", SqlDbType.NText) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TieZiImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MiniImgUrl", SqlDbType.VarChar,300)             
              
            };

            parameters[0].Value = model.RecommendLv;
            parameters[1].Value = model.ForumId;
            parameters[2].Value = model.TieZiType;
            parameters[3].Value = model.TieZiClass;
            parameters[4].Value = model.Ip;
            parameters[5].Value = model.Source;
            parameters[6].Value = model.ParentTieZiId;
            parameters[7].Value = model.HideUser;
            parameters[8].Value = model.RepCount;
            parameters[9].Value = model.WxOpenId;
            parameters[10].Value = model.TieZiTitle;
            parameters[11].Value = model.Invalid;
            parameters[12].Value = model.RepLastUser;
            parameters[13].Value = model.HotCount;
            parameters[14].Value = model.YueMingZhong;
            parameters[15].Value = model.IsIndex;
            parameters[16].Value = model.DingNum;
            parameters[17].Value = model.JingHua;
            parameters[18].Value = model.TieZiSummary;
            parameters[19].Value = model.TieZiContent;
            parameters[20].Value = model.CreateTime;
            parameters[21].Value = model.UpdateTime;
            parameters[22].Value = model.CreateUser;
            parameters[23].Value = model.TieZiImgId;
            parameters[24].Value = model.MiniImgUrl;                  

            bool result = false;
            try
            {
                model.TieZiId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "TieZiId", parameters));
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
        public bool Update(TieZiModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BBS.dbo.TieZi set ");

            strSql.Append(" RecommendLv = @RecommendLv , ");
            strSql.Append(" ForumId = @ForumId , ");
            strSql.Append(" TieZiType = @TieZiType , ");
            strSql.Append(" TieZiClass = @TieZiClass , ");
            strSql.Append(" Ip = @Ip , ");
            strSql.Append(" Source = @Source , ");
            strSql.Append(" ParentTieZiId = @ParentTieZiId , ");
            strSql.Append(" HideUser = @HideUser , ");
            strSql.Append(" RepCount = @RepCount , ");
            strSql.Append(" WxOpenId = @WxOpenId , ");
            strSql.Append(" TieZiTitle = @TieZiTitle , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" RepLastUser = @RepLastUser , ");
            strSql.Append(" HotCount = @HotCount , ");
            strSql.Append(" YueMingZhong = @YueMingZhong , ");
            strSql.Append(" IsIndex = @IsIndex , ");
            strSql.Append(" DingNum = @DingNum , ");
            strSql.Append(" JingHua = @JingHua , ");
            strSql.Append(" TieZiSummary = @TieZiSummary , ");
            strSql.Append(" TieZiContent = @TieZiContent , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" TieZiImgId = @TieZiImgId , ");
            strSql.Append(" MiniImgUrl = @MiniImgUrl  ");
            strSql.Append(" where TieZiId=@TieZiId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@TieZiId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@ForumId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@TieZiType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TieZiClass", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Ip", SqlDbType.VarChar,18) ,            
                        new SqlParameter("@Source", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ParentTieZiId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@HideUser", SqlDbType.Bit,1) ,            
                        new SqlParameter("@RepCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxOpenId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TieZiTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@RepLastUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HotCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@YueMingZhong", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@IsIndex", SqlDbType.Bit,1) ,            
                        new SqlParameter("@DingNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@JingHua", SqlDbType.Int,4) ,            
                        new SqlParameter("@TieZiSummary", SqlDbType.VarChar,300) ,            
                        new SqlParameter("@TieZiContent", SqlDbType.NText) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TieZiImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MiniImgUrl", SqlDbType.VarChar,300)             
              
            };

            parameters[0].Value = model.TieZiId;
            parameters[1].Value = model.RecommendLv;
            parameters[2].Value = model.ForumId;
            parameters[3].Value = model.TieZiType;
            parameters[4].Value = model.TieZiClass;
            parameters[5].Value = model.Ip;
            parameters[6].Value = model.Source;
            parameters[7].Value = model.ParentTieZiId;
            parameters[8].Value = model.HideUser;
            parameters[9].Value = model.RepCount;
            parameters[10].Value = model.WxOpenId;
            parameters[11].Value = model.TieZiTitle;
            parameters[12].Value = model.Invalid;
            parameters[13].Value = model.RepLastUser;
            parameters[14].Value = model.HotCount;
            parameters[15].Value = model.YueMingZhong;
            parameters[16].Value = model.IsIndex;
            parameters[17].Value = model.DingNum;
            parameters[18].Value = model.JingHua;
            parameters[19].Value = model.TieZiSummary;
            parameters[20].Value = model.TieZiContent;
            parameters[21].Value = model.CreateTime;
            parameters[22].Value = model.UpdateTime;
            parameters[23].Value = model.CreateUser;
            parameters[24].Value = model.TieZiImgId;
            parameters[25].Value = model.MiniImgUrl;
            try
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
        public TieZiModel GetModel(decimal TieZiId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TieZiId, RecommendLv, ForumId, TieZiType, TieZiClass, Ip, Source, ParentTieZiId, HideUser, RepCount, WxOpenId, TieZiTitle, Invalid, RepLastUser, HotCount, YueMingZhong, IsIndex, DingNum, JingHua, TieZiSummary, TieZiContent, CreateTime, UpdateTime, CreateUser, TieZiImgId, MiniImgUrl  ");
            strSql.Append("  from BBS.dbo.TieZi ");
            strSql.Append(" where TieZiId=@TieZiId");
            SqlParameter[] parameters = {
					new SqlParameter("@TieZiId", SqlDbType.Decimal)
			};
            parameters[0].Value = TieZiId;


            TieZiModel model = new TieZiModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TieZiId"].ToString() != "")
                {
                    model.TieZiId = decimal.Parse(ds.Tables[0].Rows[0]["TieZiId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RecommendLv"].ToString() != "")
                {
                    model.RecommendLv = int.Parse(ds.Tables[0].Rows[0]["RecommendLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ForumId"].ToString() != "")
                {
                    model.ForumId = decimal.Parse(ds.Tables[0].Rows[0]["ForumId"].ToString());
                }
                model.TieZiType = ds.Tables[0].Rows[0]["TieZiType"].ToString();
                model.TieZiClass = ds.Tables[0].Rows[0]["TieZiClass"].ToString();
                model.Ip = ds.Tables[0].Rows[0]["Ip"].ToString();
                model.Source = ds.Tables[0].Rows[0]["Source"].ToString();
                if (ds.Tables[0].Rows[0]["ParentTieZiId"].ToString() != "")
                {
                    model.ParentTieZiId = decimal.Parse(ds.Tables[0].Rows[0]["ParentTieZiId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HideUser"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["HideUser"].ToString() == "1") || (ds.Tables[0].Rows[0]["HideUser"].ToString().ToLower() == "true"))
                    {
                        model.HideUser = true;
                    }
                    else
                    {
                        model.HideUser = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["RepCount"].ToString() != "")
                {
                    model.RepCount = int.Parse(ds.Tables[0].Rows[0]["RepCount"].ToString());
                }
                model.WxOpenId = ds.Tables[0].Rows[0]["WxOpenId"].ToString();
                model.TieZiTitle = ds.Tables[0].Rows[0]["TieZiTitle"].ToString();
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
                model.RepLastUser = ds.Tables[0].Rows[0]["RepLastUser"].ToString();
                if (ds.Tables[0].Rows[0]["HotCount"].ToString() != "")
                {
                    model.HotCount = int.Parse(ds.Tables[0].Rows[0]["HotCount"].ToString());
                }
                model.YueMingZhong = ds.Tables[0].Rows[0]["YueMingZhong"].ToString();
                if (ds.Tables[0].Rows[0]["IsIndex"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsIndex"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsIndex"].ToString().ToLower() == "true"))
                    {
                        model.IsIndex = true;
                    }
                    else
                    {
                        model.IsIndex = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["DingNum"].ToString() != "")
                {
                    model.DingNum = int.Parse(ds.Tables[0].Rows[0]["DingNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JingHua"].ToString() != "")
                {
                    model.JingHua = int.Parse(ds.Tables[0].Rows[0]["JingHua"].ToString());
                }
                model.TieZiSummary = ds.Tables[0].Rows[0]["TieZiSummary"].ToString();
                model.TieZiContent = ds.Tables[0].Rows[0]["TieZiContent"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                model.TieZiImgId = ds.Tables[0].Rows[0]["TieZiImgId"].ToString();
                model.MiniImgUrl = ds.Tables[0].Rows[0]["MiniImgUrl"].ToString();

                return model;
            }
            else
            {
                return model;
            }
        }



        public void ChangeRepCount(decimal TieZiId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" update BBS.dbo.TieZi set RepCount=(select count(0) from BBS.dbo.TieZi where ParentTieZiId='" + TieZiId + "') where TieZiId='" + TieZiId + "' ");
            this.helper.ExecSqlReInt(s.ToString());
        }
        /// <summary>
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from  BBS.DBO.TieZi ");
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
            strSql.Append(" FROM  BBS.dbo.TieZiView WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("BBS.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }


        public DataSet GetTieZiInfoByTieZiId(decimal TieZiId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM BBS.dbo.TieZiView  WITH(NOLOCK) where TieZiId='" + TieZiId + "' ");

            strSql.Append(" select * from BBS.dbo.TieZiVsImg  WITH(NOLOCK)  where TieZiId='" + TieZiId + "' ");

            DataSet ds = helper.ExecSqlReDs(strSql.ToString());

            ds.Tables[0].TableName = "TieZiInfo";
            ds.Tables[1].TableName = "imgArray";
            return ds;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM  BBS.DBO.TieZi  WITH(NOLOCK) ");
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
            strSql.Append(" FROM TieZi  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

