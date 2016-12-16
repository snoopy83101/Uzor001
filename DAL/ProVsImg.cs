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
	 	//ProVsImg
		public partial class ProVsImgDAL
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
        public bool Add(ProVsImgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProVsImg(");
            strSql.Append("ImgId,ProId,VsType,OrderNo");
            strSql.Append(") values (");
            strSql.Append("@ImgId,@ProId,@VsType,@OrderNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ImgId;
            parameters[1].Value = model.ProId;
            parameters[2].Value = model.VsType;
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
        public bool Update(ProVsImgModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProVsImg set ");

            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" ProId = @ProId , ");
            strSql.Append(" VsType = @VsType , ");
            strSql.Append(" OrderNo = @OrderNo  ");
            strSql.Append(" where ImgId=@ImgId and ProId=@ProId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ImgId;
            parameters[1].Value = model.ProId;
            parameters[2].Value = model.VsType;
            parameters[3].Value = model.OrderNo; try
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
        public ProVsImgModel GetModel(string ImgId, string ProId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ImgId, ProId, VsType, OrderNo  ");
            strSql.Append("  from ProVsImg ");
            strSql.Append(" where ImgId=@ImgId and ProId=@ProId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ImgId", SqlDbType.VarChar,50),
					new SqlParameter("@ProId", SqlDbType.VarChar,50)			};
            parameters[0].Value = ImgId;
            parameters[1].Value = ProId;


            ProVsImgModel model = new ProVsImgModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();
                model.ProId = ds.Tables[0].Rows[0]["ProId"].ToString();
                model.VsType = ds.Tables[0].Rows[0]["VsType"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
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
		public bool DeleteList(string  strWhere )
		{
			    bool reValue = true;
            int reCount = 0;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ProVsImg ");
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
            strSql.Append(" FROM ProVsImgView ");
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
            strSql.Append(" FROM ProVsImgView ");
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
			strSql.Append(" FROM ProVsImg ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
	        return helper.ExecSqlReDs(strSql.ToString());
		}

   
	}
}

