using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using Model;
using DBTools;
using Common;
using System.Transactions;
namespace DAL  
{
	 	//UserLv
		public partial class UserLvDAL
	{
	
	   #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion



        public static void ExecuteTransaction()
        {
            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                //DO YOUR METHODS here
                //写入方法
                transactionScope.Complete();
            }
        }
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(UserLvModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UserLv(");			
            strSql.Append("UserLv,UserlvName,UserlvMemo");
			strSql.Append(") values (");
            strSql.Append("@UserLv,@UserlvName,@UserlvMemo");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@UserLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserlvName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserlvMemo", SqlDbType.VarChar,200)             
              
            };
			            
            parameters[0].Value = model.UserLv;                        
            parameters[1].Value = model.UserlvName;                        
            parameters[2].Value = model.UserlvMemo;                        
	
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
		public bool Update(UserLvModel model)
		{
		    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UserLv set ");
			                        
            strSql.Append(" UserLv = @UserLv , ");                                    
            strSql.Append(" UserlvName = @UserlvName , ");                                    
            strSql.Append(" UserlvMemo = @UserlvMemo  ");            			
			strSql.Append(" where UserLv=@UserLv  ");
						
            SqlParameter[] parameters = {
			            new SqlParameter("@UserLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserlvName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserlvMemo", SqlDbType.VarChar,200)             
              
            };
            			            
            parameters[0].Value = model.UserLv;                        
            parameters[1].Value = model.UserlvName;                        
            parameters[2].Value = model.UserlvMemo;                            try
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
			strSql.Append("delete from UserLv ");
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
            strSql.Append(" FROM UserLv ");
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
			strSql.Append(" FROM UserLv ");
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
			strSql.Append(" FROM UserLv ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

