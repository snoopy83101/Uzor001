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
	 	//DeliveryDetail
		public partial class DeliveryDetailDAL
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
		public bool Add(DeliveryDetailModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DeliveryDetail(");			
            strSql.Append("DeliveryDetailId,DeliveryId,StockId,ActualPrice,DeliveryDetailMemo");
			strSql.Append(") values (");
            strSql.Append("@DeliveryDetailId,@DeliveryId,@StockId,@ActualPrice,@DeliveryDetailMemo");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@DeliveryDetailId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DeliveryId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StockId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ActualPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DeliveryDetailMemo", SqlDbType.VarChar,200)             
              
            };
			            
            parameters[0].Value = model.DeliveryDetailId;                        
            parameters[1].Value = model.DeliveryId;                        
            parameters[2].Value = model.StockId;                        
            parameters[3].Value = model.ActualPrice;                        
            parameters[4].Value = model.DeliveryDetailMemo;                        
	
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
		public bool Update(DeliveryDetailModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DeliveryDetail set ");
			                        
            strSql.Append(" DeliveryDetailId = @DeliveryDetailId , ");                                    
            strSql.Append(" DeliveryId = @DeliveryId , ");                                    
            strSql.Append(" StockId = @StockId , ");                                    
            strSql.Append(" ActualPrice = @ActualPrice , ");                                    
            strSql.Append(" DeliveryDetailMemo = @DeliveryDetailMemo  ");            			
			strSql.Append(" where DeliveryDetailId=@DeliveryDetailId  ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@DeliveryDetailId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DeliveryId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StockId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ActualPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DeliveryDetailMemo", SqlDbType.VarChar,200)             
              
            };
            			            
            parameters[0].Value = model.DeliveryDetailId;                        
            parameters[1].Value = model.DeliveryId;                        
            parameters[2].Value = model.StockId;                        
            parameters[3].Value = model.ActualPrice;                        
            parameters[4].Value = model.DeliveryDetailMemo;                            try
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
			strSql.Append("delete from DeliveryDetail ");
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
            strSql.Append(" FROM DeliveryDetail ");
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
			strSql.Append(" FROM DeliveryDetail ");
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
			strSql.Append(" FROM DeliveryDetail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

