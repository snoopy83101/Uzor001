/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：Transfer.cs
   // 文件功能描述:连续业务事务传递对象类；在执行事务方法时，可以用它来传递执行语句或者链接对象
   // 类Transfer
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

namespace DBTools
{
    /// <summary>
    /// 连续业务事务传递对象类；在执行事务方法时，可以用它来传递执行语句或者链接对象
    /// </summary>
    public class Transfer : ITransfer
    {

        /// <summary>
        /// 构造传递对象
        /// </summary>
        public Transfer()
        {
        
        }

        /// <summary>
        /// 是否开始了分布式事务
        /// </summary>
        protected bool isOnTransactionScope = false;

        /// <summary>
        /// 当前使用的数据库连接 key:数据库连接字符串 value 数据库HELPER
        /// </summary>
        protected Dictionary<string, IDBHelper> connectionList = new Dictionary<string,IDBHelper>();

        /// <summary>
        /// 要执行的sql语句组 key 数据库连接字符串
        /// </summary>
        protected Dictionary<string, Queue<ISqlScript>> sqlScripts = new Dictionary<string, Queue<ISqlScript>>();

        /// <summary>
        /// 出错信息
        /// </summary>
        protected string errorMessage = "";

        /// <summary>
        /// 上次执行的业务是否成功
        /// </summary>
        protected bool lastOperationSuccess = true;

        /// <summary>
        /// 是否开始了分布式事务
        /// </summary>
        public bool IsOnTransactionScope
        {
            get 
            {
                return this.isOnTransactionScope;
            }
            set
            {
                this.isOnTransactionScope = value;
            }
        }

        /// <summary>
        /// 出错信息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }

        /// <summary>
        /// 获取当前使用的数据库连接 key:数据库连接字符串 value 数据库HELPER
        /// </summary>
        public Dictionary<string, IDBHelper> ConnectionList 
        {
            get
            {
                return this.connectionList;
            }
        }

        /// <summary>
        /// 获取或设置将要执行的sql语句组 key 数据库连接字符串
        /// </summary>
        public Dictionary<string, Queue<ISqlScript>> SqlScripts
        {
            get 
            {
                return this.sqlScripts;
            }
            set 
            {
                this.sqlScripts = value;
            }
        }

        /// <summary>
        /// 获取或设置上次执行的业务是否成功
        /// </summary>
        public bool LastOperationSuccess 
        {
            get
            {
                return this.lastOperationSuccess;
            }
            set
            {
                this.lastOperationSuccess = value & this.lastOperationSuccess;
            }
        }

        /// <summary>
        /// 添加一个数据库连接helper
        /// </summary>
        /// <param name="key">链接对应的key 一般是链接字符串</param>
        /// <param name="helper">对应的helper</param>
        /// <returns></returns>
        public bool AddConnHelper(string key, IDBHelper helper)
        {
            if (this.connectionList.ContainsKey(key))
            {
                return false;
            }
            this.connectionList.Add(key, helper);
            this.sqlScripts.Add(key, new Queue<ISqlScript>(16));
            return true;
        
        }

        /// <summary>
        /// 添加一个数据库连接helper
        /// </summary>
        /// <param name="helper">对应的helper</param>
        /// <returns></returns>
        public bool AddConnHelper(IDBHelper helper)
        {
            string key = helper.ConnectionString;
            return this.AddConnHelper(key,helper);
        }

        /// <summary>
        /// 获取 指定类型的 helper 如果没有则先创建在获取
        /// </summary>
        /// <typeparam name="T">helper的类型</typeparam>
        /// <param name="key">链接对应的key</param>
        /// <returns></returns>
        public T GetWithAddHelper<T>(string key)
        {
            if (this.connectionList.ContainsKey(key))
            {
                return (T)this.connectionList[key];
            }
            else
            { 
                Type type = typeof(T);
                if (type == typeof(MSSQLHelper))
                {
                    MSSQLHelper helper = new MSSQLHelper(key);
                    helper.BeginTran();
                    this.AddConnHelper(key, helper);
                    return (T)this.connectionList[key];
                }
                else
                {
                    throw new Exception("没有找到合适的类型 @ Transfer  GetWithAddHelper<T>");
                }
            }
        }

        /// <summary>
        /// 添加要执行的sql语句对象
        /// </summary>
        /// <param name="key">数据库连接字符串</param>
        /// <param name="sqlscript">要添加的sql语句对象</param>
        public void AddSqlScript(string key, ISqlScript sqlscript)
        {
            if (!this.sqlScripts.ContainsKey(key))
            {
                throw new Exception("不存在要添加sqlscript的connect的key");
            }
            this.sqlScripts[key].Enqueue(sqlscript);
            
        }

        /// <summary>
        /// 开始分布式事务，返回是否成功
        /// </summary>
        /// <returns></returns>
        public bool BeginTransactionScope()
        {
            throw new Exception("该方法尚未实现");
        }

        /// <summary>
        /// 提交分布式事务，如果提交失败则回滚
        /// </summary>
        /// <returns></returns>
        public bool TryCommitTransactionScope()
        {
            throw new Exception("该方法尚未实现");
        }

        /// <summary>
        /// 回滚分布式事务
        /// </summary>
        /// <returns></returns>
        public bool RollBackTransactionScope()
        {
            throw new Exception("该方法尚未实现");
        }

        /// <summary>
        /// 执行某个指定连接的所有存储语句只能是非select的DML
        /// </summary>
        /// <param name="key">要执行sql语句的链接</param>
        /// <returns></returns>
        public bool ExecuteSql(string key)
        {
            if(!this.connectionList.ContainsKey(key))
            {
                return false;
            }
            IDBHelper helper = this.connectionList[key];

            Queue<ISqlScript> list = this.sqlScripts[key];

            while (list.Count>0)
            {
                ISqlScript sqlscript = list.Dequeue(); 
                if (sqlscript.IsSqlStringOnly)
                {
                    try
                    {
                        helper.ExecSqlReInt(sqlscript.SqlString);
                    }
                    catch (Exception ex)
                    { 
                        string errDescribe = ex.Message + " 异常： " + ex.InnerException + "位置：" + ex.StackTrace + "对象名称:" + ex.Source + "引发异常的方法:" + ex.TargetSite;
                        this.errorMessage += errDescribe;
                        return false;
                    }
                    
                }
                else
                {
                    try
                    {
                        helper.ExecSqlReInt(sqlscript.SqlString, sqlscript.SqlParams);
                    }
                    catch (Exception ex)
                    {
                        string errDescribe = ex.Message + " 异常： " + ex.InnerException + "位置：" + ex.StackTrace + "对象名称:" + ex.Source + "引发异常的方法:" + ex.TargetSite;
                        this.errorMessage += errDescribe;
                        return false;
                    }
                   
                }

            }
            return true;

        }

        /// <summary>
        /// 执行所有连接的所有存储语句只能是非select的DML 中间遇到错误则全部停止
        /// </summary>
        /// <returns></returns>
        public bool ExecuteSql()
        {
            foreach (string key in this.connectionList.Keys)
            {
                if (!this.ExecuteSql(key))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 开始某个链接的事务
        /// </summary>
        /// <param name="key">要开始事务的链接的key</param>
        /// <returns></returns>
        public bool BeginTran(string key)
        {
            if (this.connectionList.ContainsKey(key))
            {
                IDBHelper helper = this.connectionList[key];
                if (!helper.IsOnTransaction)
                {
                    helper.BeginTran();
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 开始所有链接的事务
        /// </summary>
        /// <returns></returns>
        public bool BeginTran()
        {
            foreach (string key in this.connectionList.Keys)
            {
                IDBHelper helper = this.connectionList[key];
                if (!helper.IsOnTransaction)
                {
                    helper.BeginTran();
                }
            }
            return true;
        }

        /// <summary>
        /// 尝试提交某个链接的事务，失败则回滚
        /// </summary>
        /// <param name="key">要提交事务的链接的key</param>
        /// <returns></returns>
        public bool TryCommitTran(string key)
        {
            if (this.connectionList.ContainsKey(key))
            {
                IDBHelper helper = this.connectionList[key];
                if (helper.IsOnTransaction)
                {
                    try
                    {
                        helper.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        string errDescribe = ex.Message + " 异常： " + ex.InnerException + "位置：" + ex.StackTrace + "对象名称:" + ex.Source + "引发异常的方法:" + ex.TargetSite;
                        this.errorMessage += errDescribe;
                        helper.RollbackTran();
                        return false;
                    }
                    return true;
                }
                return true;//如果没有事务则返回成功
            }
            return false;
        }

        /// <summary>
        /// 尝试提交所有链接的事务，失败则回滚
        /// </summary>
        /// <returns></returns>
        public bool TryCommitTran()
        {

            bool isok = true;
            foreach (string key in this.connectionList.Keys)
            {
                if (!isok)
                {
                    this.connectionList[key].RollbackTran();
                    continue;
                }
                if (!this.TryCommitTran(key))
                {
                    isok = false;
                }

            }
            return isok;
        }


        public bool RollbackTran(string key)
        {
            if (this.connectionList.ContainsKey(key))
            {
                IDBHelper helper = this.connectionList[key];
                if (helper.IsOnTransaction)
                {
                    try
                    {
                        helper.RollbackTran();
                    }
                    catch (Exception ex)
                    {
                        string errDescribe = ex.Message + " 异常： " + ex.InnerException + "位置：" + ex.StackTrace + "对象名称:" + ex.Source + "引发异常的方法:" + ex.TargetSite;
                        this.errorMessage += errDescribe;
                        return false;
                    }
                    return true;
                }
                return true;//如果没有事务则返回成功
            }
            return false;
        }

        public bool RollbackTran()
        {
            return this.RoleBackAll();
        }

        /// <summary>
        /// 回滚所有
        /// </summary>
        /// <returns></returns>
        protected bool RoleBackAll()
        {
            foreach (string key in this.connectionList.Keys)
            {
                IDBHelper helper = this.connectionList[key];
                if (helper.IsOnTransaction)
                {
                    helper.RollbackTran();
                }
            }
            return true;
        }

        /// <summary>
        /// 关闭某个链接
        /// </summary>
        /// <param name="key">关闭某个链接的key</param>
        /// <returns></returns>
        public bool Close(string key)
        {
            if (!this.connectionList.ContainsKey(key))
            {
                return false;
            }
            this.connectionList[key].Close();
            return true;
        }


        /// <summary>
        /// 关闭所有链接
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            foreach (string key in this.connectionList.Keys)
            {
                    this.connectionList[key].Close();
            }
            return true;
        }

        /// <summary>
        /// 释放某个链接
        /// </summary>
        /// <param name="key">要释放的链接的key</param>
        /// <returns></returns>
        public bool Dispose(string key)
        {
            if (!this.connectionList.ContainsKey(key))
            {
                return false;
            }
            this.connectionList[key].Dispose();
            return true;
        }

        /// <summary>
        /// 释放所有的链接
        /// </summary>
        /// <returns></returns>
        public bool Dispose()
        {
            foreach (string key in this.connectionList.Keys)
            {
                this.connectionList[key].Dispose();
            }
            return true;
        }

        /// <summary>
        /// 执行所有未执行语句后如果全部成功则提交所有事务，然后关闭所有链接并释放
        /// </summary>
        /// <returns></returns>
        public bool ExecuteAll()
        {
            bool isok = true;
            if (!this.lastOperationSuccess)
            {
                this.RoleBackAll();
                return false;
            }
            isok = this.ExecuteSql();

            if (!isok)
            {
                this.RoleBackAll();
            }
            else
            {
                isok = this.TryCommitTran();
            }
            this.Close();
            this.Dispose();
            return isok;

        }



    }
}
