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
    //整站动态表
    public partial class DynamicDAL
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
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(0) ");
			strSql.Append(" FROM Dynamic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
	        int i = int.Parse(helper.ExecuteSqlScalar(strSql.ToString()));
	        return i;
        }
		
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DynamicModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Dynamic(");			
            strSql.Append("JsonType,JsonMemo,DynamicTitle,DynamicType,CreateTime,FlagInvalid,DynamicUserId,DynamicMerId,DynamicLv");
			strSql.Append(") values (");
            strSql.Append("@JsonType,@JsonMemo,@DynamicTitle,@DynamicType,@CreateTime,@FlagInvalid,@DynamicUserId,@DynamicMerId,@DynamicLv");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@JsonType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@JsonMemo", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@DynamicTitle", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@DynamicType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@DynamicUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DynamicMerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DynamicLv", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.JsonType;                        
            parameters[1].Value = model.JsonMemo;                        
            parameters[2].Value = model.DynamicTitle;                        
            parameters[3].Value = model.DynamicType;                        
            parameters[4].Value = model.CreateTime;                        
            parameters[5].Value = model.FlagInvalid;                        
            parameters[6].Value = model.DynamicUserId;                        
            parameters[7].Value = model.DynamicMerId;                        
            parameters[8].Value = model.DynamicLv;                        
	
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
		public bool Update(DynamicModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Dynamic set ");
			                                                
            strSql.Append(" JsonType = @JsonType , ");                                    
            strSql.Append(" JsonMemo = @JsonMemo , ");                                    
            strSql.Append(" DynamicTitle = @DynamicTitle , ");                                    
            strSql.Append(" DynamicType = @DynamicType , ");                                    
            strSql.Append(" CreateTime = @CreateTime , ");                                    
            strSql.Append(" FlagInvalid = @FlagInvalid , ");                                    
            strSql.Append(" DynamicUserId = @DynamicUserId , ");                                    
            strSql.Append(" DynamicMerId = @DynamicMerId , ");                                    
            strSql.Append(" DynamicLv = @DynamicLv  ");            			
			strSql.Append(" where DynamicId=@DynamicId ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@DynamicId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JsonType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@JsonMemo", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@DynamicTitle", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@DynamicType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@DynamicUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DynamicMerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DynamicLv", SqlDbType.Int,4)             
              
            };
            			            
            parameters[0].Value = model.DynamicId;                        
            parameters[1].Value = model.JsonType;                        
            parameters[2].Value = model.JsonMemo;                        
            parameters[3].Value = model.DynamicTitle;                        
            parameters[4].Value = model.DynamicType;                        
            parameters[5].Value = model.CreateTime;                        
            parameters[6].Value = model.FlagInvalid;                        
            parameters[7].Value = model.DynamicUserId;                        
            parameters[8].Value = model.DynamicMerId;                        
            parameters[9].Value = model.DynamicLv;                            try
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
		public DynamicModel GetModel(decimal DynamicId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DynamicId, JsonType, JsonMemo, DynamicTitle, DynamicType, CreateTime, FlagInvalid, DynamicUserId, DynamicMerId, DynamicLv  ");			
			strSql.Append("  from Dynamic ");
			strSql.Append(" where DynamicId=@DynamicId");
						SqlParameter[] parameters = {
					new SqlParameter("@DynamicId", SqlDbType.Decimal)
			};
			parameters[0].Value = DynamicId;

			
			DynamicModel model=new DynamicModel();
			DataSet ds=helper.ExecSqlReDs(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["DynamicId"].ToString()!="")
				{
					model.DynamicId=decimal.Parse(ds.Tables[0].Rows[0]["DynamicId"].ToString());
				}
																																				model.JsonType= ds.Tables[0].Rows[0]["JsonType"].ToString();
																																model.JsonMemo= ds.Tables[0].Rows[0]["JsonMemo"].ToString();
																																model.DynamicTitle= ds.Tables[0].Rows[0]["DynamicTitle"].ToString();
																																model.DynamicType= ds.Tables[0].Rows[0]["DynamicType"].ToString();
																												if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
																																																if(ds.Tables[0].Rows[0]["FlagInvalid"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["FlagInvalid"].ToString()=="1")||(ds.Tables[0].Rows[0]["FlagInvalid"].ToString().ToLower()=="true"))
					{
					model.FlagInvalid= true;
					}
					else
					{
					model.FlagInvalid= false;
					}
				}
																				model.DynamicUserId= ds.Tables[0].Rows[0]["DynamicUserId"].ToString();
																												if(ds.Tables[0].Rows[0]["DynamicMerId"].ToString()!="")
				{
					model.DynamicMerId=decimal.Parse(ds.Tables[0].Rows[0]["DynamicMerId"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DynamicLv"].ToString()!="")
				{
					model.DynamicLv=int.Parse(ds.Tables[0].Rows[0]["DynamicLv"].ToString());
				}
																														
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
            strSql.Append("delete from Dynamic ");
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
            strSql.Append(" FROM Dynamic ");
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
            strSql.Append(" FROM Dynamic ");
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
            strSql.Append(" FROM Dynamic ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

