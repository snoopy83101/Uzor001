/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：ITransfer.cs
   // 文件功能描述:连续业务事务传递对象接口；在执行事务方法时，可以用它来传递执行语句或者链接对象
   // 接口ITransfer
   //
   // 
   // 创建标识： 李涛 20101209 
   // 创建内容： 系统框架和注释
   //
   // 代码填写：李涛 20101220
   //
   // 修改标识：
   // 修改原因：
   //
   //

----------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Collections;

namespace DBTools
{

    /// <summary>
    /// 众阳HIS系统数据访问层连续业务事务传递对象接口；在执行事务方法时，可以用它来传递执行语句或者链接对象
    /// 当业务开始事物IsOnTransaction设为true，从开始事物到结束事物的期间使用一个Connection
    /// </summary>
    public interface ITransfer
    {
        /// <summary>
        /// 是否开始了分布式事务
        /// </summary>
        bool IsOnTransactionScope { get; set; } 

        /// <summary>
        /// 出错信息
        /// </summary>
        string ErrorMessage { get; set; }
            
        /// <summary>
        /// 获取当前使用的数据库连接 key:数据库连接字符串 value 数据库HELPER
        /// </summary>
        Dictionary<string,IDBHelper> ConnectionList { get;  }

        /// <summary>
        /// 获取或设置将要执行的sql语句组 key 数据库连接字符串
        /// </summary>
        Dictionary<string, Queue<ISqlScript>> SqlScripts { get; set; }

        /// <summary>
        /// 获取或设置上次执行的业务是否成功
        /// </summary>
        bool LastOperationSuccess { get; set; }

        /// <summary>
        /// 添加一个数据库连接helper
        /// </summary>
        /// <param name="key">链接对应的key 一般是链接字符串</param>
        /// <param name="helper">对应的helper</param>
        /// <returns></returns>
        bool AddConnHelper(string key, IDBHelper helper);

        /// <summary>
        /// 添加一个数据库连接helper
        /// </summary>
        /// <param name="helper">对应的helper</param>
        /// <returns></returns>
        bool AddConnHelper(IDBHelper helper);

        /// <summary>
        /// 获取 指定类型的 helper 如果没有则先创建在获取 如果是在此方法中创建的helper 则自动开始事务
        /// </summary>
        /// <typeparam name="T">helper的类型</typeparam>
        /// <param name="key">链接对应的key</param>
        /// <returns></returns>
        T GetWithAddHelper<T>(string key);

        /// <summary>
        /// 添加要执行的sql语句对象
        /// </summary>
        /// <param name="key">数据库连接字符串</param>
        /// <param name="sqlscript">要添加的sql语句对象</param>
        void AddSqlScript(string key,ISqlScript sqlscript);

        /// <summary>
        /// 开始分布式事务，返回是否成功
        /// </summary>
        /// <returns></returns>
        bool BeginTransactionScope();

        /// <summary>
        /// 提交分布式事务，如果提交失败则回滚
        /// </summary>
        /// <returns></returns>
        bool TryCommitTransactionScope();

        /// <summary>
        /// 回滚分布式事务
        /// </summary>
        /// <returns></returns>
        bool RollBackTransactionScope();

        /// <summary>
        /// 执行所有连接的所有存储语句只能是非select的DML 中间遇到错误则全部停止
        /// </summary>
        /// <param name="key">要执行sql语句的链接</param>
        /// <returns></returns>
        bool ExecuteSql(string key);

        /// <summary>
        /// 执行所有连接的所有存储语句只能是非select的DML
        /// </summary>
        /// <returns></returns>
        bool ExecuteSql();

        /// <summary>
        /// 开始某个链接的事务
        /// </summary>
        /// <param name="key">要开始事务的链接的key</param>
        /// <returns></returns>
        bool BeginTran(string key);

        /// <summary>
        /// 开始所有链接的事务
        /// </summary>
        /// <returns></returns>
        bool BeginTran();

        /// <summary>
        /// 尝试提交某个链接的事务，失败则回滚
        /// </summary>
        /// <param name="key">要提交事务的链接的key</param>
        /// <returns></returns>
        bool TryCommitTran(string key);

        /// <summary>
        /// 尝试提交所有链接的事务，失败则回滚
        /// </summary>
        /// <returns></returns>
        bool TryCommitTran();

        /// <summary>
        /// 回滚指定链接的事务
        /// </summary>
        /// <param name="key">要回滚事务的链接的key</param>
        /// <returns></returns>
        bool RollbackTran(string key);

        /// <summary>
        /// 回滚所有链接的事务
        /// </summary>
        /// <returns></returns>
        bool RollbackTran();

        /// <summary>
        /// 关闭某个链接
        /// </summary>
        /// <param name="key">关闭某个链接的key</param>
        /// <returns></returns>
        bool Close(string key);

        /// <summary>
        /// 关闭所有链接
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>
        /// 释放某个链接
        /// </summary>
        /// <param name="key">要释放的链接的key</param>
        /// <returns></returns>
        bool Dispose(string key);

        /// <summary>
        /// 释放所有的链接
        /// </summary>
        /// <returns></returns>
        bool Dispose();

        /// <summary>
        /// 执行所有未执行语句后如果全部成功则提交所有事务，然后关闭所有链接并释放
        /// </summary>
        /// <returns></returns>
        bool ExecuteAll();



    }

}
