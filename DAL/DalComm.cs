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
    //Article
    public partial class DalComm
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>

        #endregion


        /// <summary>
        /// 检查是否存在
        /// </summary>
        public static string ExStr(string sql)
        {
            MSSQLHelper helper = new MSSQLHelper();
            StringBuilder strSql = new StringBuilder();

            string i = helper.ExecuteSqlScalar(sql);
            return i;
        }

        /// <summary>
        /// 返回首行, 例如是select count(0) 开始
        /// </summary>
        public static int ExInt(string sql)
        {
            MSSQLHelper helper = new MSSQLHelper();
            StringBuilder strSql = new StringBuilder();
            int i = 0;
            try
            {
                i = int.Parse(helper.ExecuteSqlScalar(sql));
            }

            catch(Exception ex)
            {

            }
            return i;
        }

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static decimal ExDecimal(string sql)
        {
            MSSQLHelper helper = new MSSQLHelper();
            StringBuilder strSql = new StringBuilder();
            decimal i = 0;
            try
            {
                i = decimal.Parse(helper.ExecuteSqlScalar(sql));
            }
            catch
            {

            }
            return i;
        }


        public static int ExReInt(string sql, int TimeOut)
        {
            if (sql.Trim() == "")
            {
                return 0;

            }

            MSSQLHelper helper = new MSSQLHelper();
            StringBuilder strSql = new StringBuilder();

            int i = helper.ExecSqlReInt(sql, TimeOut);
            return i;
        }


        /// <summary>
        /// 返回执行行数
        /// </summary>
        public static int ExReInt(string sql)
        {
            if (sql.Trim() == "")
            {
                return 0;

            }

            MSSQLHelper helper = new MSSQLHelper();
            StringBuilder strSql = new StringBuilder();

            int i = helper.ExecSqlReInt(sql);
            return i;
        }


        /// <summary>
        /// 检查是否存在
        /// </summary>
        public static bool ExBool(string sql)
        {
            MSSQLHelper helper = new MSSQLHelper();
            StringBuilder strSql = new StringBuilder();

            string i = helper.ExecuteSqlScalar(sql);
            if (i == "0" || i == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static DataSet BackData(string sql)
        {
            MSSQLHelper helper = new MSSQLHelper();
            return helper.ExecSqlReDs(sql);
        }


        public static Dictionary<string, string> ExecSqlReDr(string sqlstr)
        { 
             MSSQLHelper helper = new MSSQLHelper();
             return helper.ExecSqlReDr(sqlstr);
        }


        /// <summary>
        /// 自由分页
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="strWhere"></param>
        /// <param name="Order"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static DataSet GetPageList(string TableName, string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            MSSQLHelper helper = new MSSQLHelper();
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "YYHD.dbo.GetPro", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;

        }
    }
}

