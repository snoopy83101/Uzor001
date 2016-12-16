/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：SqlScript.cs
   // 文件功能描述：数据库sql语句封装类
   // 类SqlScript
   //
   // 
   // 创建标识： 李涛 20101209 
   // 创建内容： 系统框架和注释
   //
   // 代码填写：
   //
   // 修改标识：
   // 修改原因：
   //
   //

----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DBTools
{
    /// <summary>
    /// 数据库sql语句封装类
    /// </summary>
    public class SqlScript : ISqlScript
    {

        /// <summary>
        /// sql语句
        /// </summary>
        protected string sqlstr = "";

        /// <summary>
        /// 参数数组
        /// </summary>
        protected DbParameter[] paramArray;

        /// <summary>
        /// 是否是只有sql语句而没有参数
        /// </summary>
        protected bool isSqlStringOnly = false;

        protected SqlScript()
        { 
        
        }

        /// <summary>
        /// 数据库sql语句封装类构造函数(不带参数的sql语句 )
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        public SqlScript(string sqlString)
        {
            this.sqlstr = sqlString;
            this.isSqlStringOnly = true;
        }

        /// <summary>
        /// 数据库sql语句封装类构造函数（带参数的sql语句）
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        /// <param name="paramArray">参数数组</param>
        public SqlScript(string sqlString,params DbParameter[] paramArray)
        {
            this.sqlstr = sqlString;
            this.paramArray = paramArray;
            this.isSqlStringOnly = false;
        }

        /// <summary>
        /// 是否是只有sql语句而没有参数
        /// </summary>
        public bool IsSqlStringOnly
        {
            get 
            {
                return this.isSqlStringOnly;
            }
        }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        public string SqlString 
        {
            get 
            {
                return this.sqlstr;
            } 
        }

        /// <summary>
        /// 获取参数数据
        /// </summary>
        public DbParameter[] SqlParams
        {
            get
            {
                return this.paramArray;
            }
        }

    }
}
