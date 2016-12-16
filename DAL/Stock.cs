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
	 	//Stock
		public partial class StockDAL
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
		public bool Add(StockModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Stock(");			
            strSql.Append("StockId,ProductId,Quatity,MerchantId,PurchasePrice,RetailPrice,GroupBuyPrice,WholesalePrice");
			strSql.Append(") values (");
            strSql.Append("@StockId,@ProductId,@Quatity,@MerchantId,@PurchasePrice,@RetailPrice,@GroupBuyPrice,@WholesalePrice");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@StockId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProductId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Quatity", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PurchasePrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RetailPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GroupBuyPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@WholesalePrice", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.StockId;                        
            parameters[1].Value = model.ProductId;                        
            parameters[2].Value = model.Quatity;                        
            parameters[3].Value = model.MerchantId;                        
            parameters[4].Value = model.PurchasePrice;                        
            parameters[5].Value = model.RetailPrice;                        
            parameters[6].Value = model.GroupBuyPrice;                        
            parameters[7].Value = model.WholesalePrice;                        
	
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
		public bool Update(StockModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Stock set ");
			                        
            strSql.Append(" StockId = @StockId , ");                                    
            strSql.Append(" ProductId = @ProductId , ");                                    
            strSql.Append(" Quatity = @Quatity , ");                                    
            strSql.Append(" MerchantId = @MerchantId , ");                                    
            strSql.Append(" PurchasePrice = @PurchasePrice , ");                                    
            strSql.Append(" RetailPrice = @RetailPrice , ");                                    
            strSql.Append(" GroupBuyPrice = @GroupBuyPrice , ");                                    
            strSql.Append(" WholesalePrice = @WholesalePrice  ");            			
			strSql.Append(" where  ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@StockId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProductId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Quatity", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PurchasePrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RetailPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GroupBuyPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@WholesalePrice", SqlDbType.Decimal,9)             
              
            };
            			            
            parameters[0].Value = model.StockId;                        
            parameters[1].Value = model.ProductId;                        
            parameters[2].Value = model.Quatity;                        
            parameters[3].Value = model.MerchantId;                        
            parameters[4].Value = model.PurchasePrice;                        
            parameters[5].Value = model.RetailPrice;                        
            parameters[6].Value = model.GroupBuyPrice;                        
            parameters[7].Value = model.WholesalePrice;                            try
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
			strSql.Append("delete from Stock ");
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
            strSql.Append(" FROM Stock ");
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
			strSql.Append(" FROM Stock ");
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
			strSql.Append(" FROM Stock ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

