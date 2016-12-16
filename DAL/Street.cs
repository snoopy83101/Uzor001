﻿using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using Model;
using DBTools;
using Common;
namespace DAL  
{
	 	//Street
		public partial class StreetDAL
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
		public bool Add(StreetModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Street(");			
            strSql.Append("StreetName,StreetTitle,StreetContent,OrderId,TownId");
			strSql.Append(") values (");
            strSql.Append("@StreetName,@StreetTitle,@StreetContent,@OrderId,@TownId");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@StreetName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@StreetTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@StreetContent", SqlDbType.VarChar) ,            
                        new SqlParameter("@OrderId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.StreetName;                        
            parameters[1].Value = model.StreetTitle;                        
            parameters[2].Value = model.StreetContent;                        
            parameters[3].Value = model.OrderId;                        
            parameters[4].Value = model.TownId;                        
	
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
		public bool Update(StreetModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Street set ");
			                                                
            strSql.Append(" StreetName = @StreetName , ");                                    
            strSql.Append(" StreetTitle = @StreetTitle , ");                                    
            strSql.Append(" StreetContent = @StreetContent , ");                                    
            strSql.Append(" OrderId = @OrderId , ");                                    
            strSql.Append(" TownId = @TownId  ");            			
			strSql.Append(" where StreetId=@StreetId ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@StreetId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@StreetName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@StreetTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@StreetContent", SqlDbType.VarChar) ,            
                        new SqlParameter("@OrderId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9)             
              
            };
            			            
            parameters[0].Value = model.StreetId;                        
            parameters[1].Value = model.StreetName;                        
            parameters[2].Value = model.StreetTitle;                        
            parameters[3].Value = model.StreetContent;                        
            parameters[4].Value = model.OrderId;                        
            parameters[5].Value = model.TownId;                            try
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
			strSql.Append("delete from Street ");
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
            strSql.Append(" FROM Street ");
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
			strSql.Append(" FROM Street ");
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
			strSql.Append(" FROM Street ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

