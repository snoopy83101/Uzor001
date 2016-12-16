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
	 	//GroupBuy
		public partial class GroupBuyDAL
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
		public bool Add(GroupBuyModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GroupBuy(");			
            strSql.Append("GroupBuyId,GroupBuyName,TotalPrice,BeginTime,EndTime,OrderNo,GroupBuyTitle,GroupBuyMemo,StockId");
			strSql.Append(") values (");
            strSql.Append("@GroupBuyId,@GroupBuyName,@TotalPrice,@BeginTime,@EndTime,@OrderNo,@GroupBuyTitle,@GroupBuyMemo,@StockId");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@GroupBuyId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GroupBuyName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TotalPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BeginTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@GroupBuyTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@GroupBuyMemo", SqlDbType.VarChar) ,            
                        new SqlParameter("@StockId", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.GroupBuyId;                        
            parameters[1].Value = model.GroupBuyName;                        
            parameters[2].Value = model.TotalPrice;                        
            parameters[3].Value = model.BeginTime;                        
            parameters[4].Value = model.EndTime;                        
            parameters[5].Value = model.OrderNo;                        
            parameters[6].Value = model.GroupBuyTitle;                        
            parameters[7].Value = model.GroupBuyMemo;                        
            parameters[8].Value = model.StockId;                        
	
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
		public bool Update(GroupBuyModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GroupBuy set ");
			                        
            strSql.Append(" GroupBuyId = @GroupBuyId , ");                                    
            strSql.Append(" GroupBuyName = @GroupBuyName , ");                                    
            strSql.Append(" TotalPrice = @TotalPrice , ");                                    
            strSql.Append(" BeginTime = @BeginTime , ");                                    
            strSql.Append(" EndTime = @EndTime , ");                                    
            strSql.Append(" OrderNo = @OrderNo , ");                                    
            strSql.Append(" GroupBuyTitle = @GroupBuyTitle , ");                                    
            strSql.Append(" GroupBuyMemo = @GroupBuyMemo , ");                                    
            strSql.Append(" StockId = @StockId  ");            			
			strSql.Append(" where GroupBuyId=@GroupBuyId  ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@GroupBuyId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GroupBuyName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TotalPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BeginTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@GroupBuyTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@GroupBuyMemo", SqlDbType.VarChar) ,            
                        new SqlParameter("@StockId", SqlDbType.Decimal,9)             
              
            };
            			            
            parameters[0].Value = model.GroupBuyId;                        
            parameters[1].Value = model.GroupBuyName;                        
            parameters[2].Value = model.TotalPrice;                        
            parameters[3].Value = model.BeginTime;                        
            parameters[4].Value = model.EndTime;                        
            parameters[5].Value = model.OrderNo;                        
            parameters[6].Value = model.GroupBuyTitle;                        
            parameters[7].Value = model.GroupBuyMemo;                        
            parameters[8].Value = model.StockId;                            try
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
			strSql.Append("delete from GroupBuy ");
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
            strSql.Append(" FROM GroupBuy ");
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
			strSql.Append(" FROM GroupBuy ");
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
			strSql.Append(" FROM GroupBuy ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

