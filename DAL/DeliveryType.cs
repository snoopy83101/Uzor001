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
	 	//DeliveryType
		public partial class DeliveryTypeDAL
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
		public bool Add(DeliveryTypeModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DeliveryType(");			
            strSql.Append("DeliveryTypeId,DeliveryTypeName,Memo,OrderNo");
			strSql.Append(") values (");
            strSql.Append("@DeliveryTypeId,@DeliveryTypeName,@Memo,@OrderNo");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@DeliveryTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DeliveryTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.DeliveryTypeId;                        
            parameters[1].Value = model.DeliveryTypeName;                        
            parameters[2].Value = model.Memo;                        
            parameters[3].Value = model.OrderNo;                        
	
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
		public bool Update(DeliveryTypeModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DeliveryType set ");
			                        
            strSql.Append(" DeliveryTypeId = @DeliveryTypeId , ");                                    
            strSql.Append(" DeliveryTypeName = @DeliveryTypeName , ");                                    
            strSql.Append(" Memo = @Memo , ");                                    
            strSql.Append(" OrderNo = @OrderNo  ");            			
			strSql.Append(" where DeliveryTypeId=@DeliveryTypeId  ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@DeliveryTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DeliveryTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };
            			            
            parameters[0].Value = model.DeliveryTypeId;                        
            parameters[1].Value = model.DeliveryTypeName;                        
            parameters[2].Value = model.Memo;                        
            parameters[3].Value = model.OrderNo;                            try
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
			strSql.Append("delete from DeliveryType ");
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
            strSql.Append(" FROM DeliveryType ");
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
			strSql.Append(" FROM DeliveryType ");
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
			strSql.Append(" FROM DeliveryType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

