/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：IDBHelper.cs
   // 文件功能描述:数据库帮助接口 所有的DBHelper通用接口
   // 接口IDBHelper
   //
   // 
   // 创建标识： 李涛 20101220 
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
using System.Data;

namespace DBTools
{

    /// <summary>
    /// 数据库帮助接口 所有的DBHelper通用接口
    /// </summary>
    public interface IDBHelper : IDisposable
    {
        /// <summary>
        /// 获取链接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 获取是否正在事务中
        /// </summary>
        bool IsOnTransaction { get; }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void Close();

        /// <summary>
        /// 开始一个事务
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 提交一个事务
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 回滚当前事务
        /// </summary>
        void RollbackTran();

        /// <summary>
        /// 执行sql语句返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExecSqlReInt(string sqlStr);

        /// <summary>
        /// 执行带参数的sql语句返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>受影响的行数</returns>
        int ExecSqlReInt(string sqlStr, IDataParameter[] para);
        

        /// <summary>
        /// 执行sql语句返回数据集
        /// </summary>
        /// <param name="sqlStr">执行sql语句</param>
        /// <returns>返回的数据集</returns>
        DataSet ExecSqlReDs(string sqlStr);

        /// <summary>
        /// 执行带参数sql语句返回数据集
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>返回的数据集</returns>
        DataSet ExecSqlReDs(string sqlStr, IDataParameter[] para);

        /// <summary>
        /// 返回指定字符串的实例
        /// </summary>
        /// <param name="connstr">链接字符串</param>
        /// <returns></returns>
        IDBHelper GetDBHelperInstance(string connstr);


    }
}
