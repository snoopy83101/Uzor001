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
	 	//NewInfo
		public partial class NewInfoDAL
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
		public bool Add(NewInfoModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into NewInfo(");			
            strSql.Append("NewSummary,NewTitle,NewContent,CreateTime,CreateUser,NewSourse,HotLv,RecommendLv,ClickLv,NewClassId,FlagInvalid");
			strSql.Append(") values (");
            strSql.Append("@NewSummary,@NewTitle,@NewContent,@CreateTime,@CreateUser,@NewSourse,@HotLv,@RecommendLv,@ClickLv,@NewClassId,@FlagInvalid");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@NewSummary", SqlDbType.NVarChar,1000) ,            
                        new SqlParameter("@NewTitle", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@NewContent", SqlDbType.NVarChar) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@NewSourse", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HotLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClickLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@NewClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1)             
              
            };
			            
            parameters[0].Value = model.NewSummary;                        
            parameters[1].Value = model.NewTitle;                        
            parameters[2].Value = model.NewContent;                        
            parameters[3].Value = model.CreateTime;                        
            parameters[4].Value = model.CreateUser;                        
            parameters[5].Value = model.NewSourse;                        
            parameters[6].Value = model.HotLv;                        
            parameters[7].Value = model.RecommendLv;                        
            parameters[8].Value = model.ClickLv;                        
            parameters[9].Value = model.NewClassId;                        
            parameters[10].Value = model.FlagInvalid;                        
	
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
		public bool Update(NewInfoModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update NewInfo set ");
			                                                
            strSql.Append(" NewSummary = @NewSummary , ");                                    
            strSql.Append(" NewTitle = @NewTitle , ");                                    
            strSql.Append(" NewContent = @NewContent , ");                                    
            strSql.Append(" CreateTime = @CreateTime , ");                                    
            strSql.Append(" CreateUser = @CreateUser , ");                                    
            strSql.Append(" NewSourse = @NewSourse , ");                                    
            strSql.Append(" HotLv = @HotLv , ");                                    
            strSql.Append(" RecommendLv = @RecommendLv , ");                                    
            strSql.Append(" ClickLv = @ClickLv , ");                                    
            strSql.Append(" NewClassId = @NewClassId , ");                                    
            strSql.Append(" FlagInvalid = @FlagInvalid  ");            			
			strSql.Append(" where NewId=@NewId ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@NewId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@NewSummary", SqlDbType.NVarChar,1000) ,            
                        new SqlParameter("@NewTitle", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@NewContent", SqlDbType.NVarChar) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@NewSourse", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@HotLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClickLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@NewClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1)             
              
            };
            			            
            parameters[0].Value = model.NewId;                        
            parameters[1].Value = model.NewSummary;                        
            parameters[2].Value = model.NewTitle;                        
            parameters[3].Value = model.NewContent;                        
            parameters[4].Value = model.CreateTime;                        
            parameters[5].Value = model.CreateUser;                        
            parameters[6].Value = model.NewSourse;                        
            parameters[7].Value = model.HotLv;                        
            parameters[8].Value = model.RecommendLv;                        
            parameters[9].Value = model.ClickLv;                        
            parameters[10].Value = model.NewClassId;                        
            parameters[11].Value = model.FlagInvalid;                            try
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
		public bool DeleteList(string  strWhere )
		{
			    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from NewInfo ");
			strSql.Append(" where "+strWhere);
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
            strSql.Append(" FROM NewInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;
		
			
		}
     

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM NewInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
	       return helper.ExecSqlReDs(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM NewInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

