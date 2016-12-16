using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Common
{
    public class Tran
    {





        /// <summary>
        /// 事务的隔离级别,如果上面已经有了环境事务, 则引用环境事务的隔离级别, 如果没有引用. 可以用自己的隔离级别
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public static System.Transactions.IsolationLevel isolationLevel(System.Transactions.IsolationLevel isolationLevel)
        {


            if (Transaction.Current == null)
            {
                return isolationLevel;
            }
            else
            {
                return Transaction.Current.IsolationLevel;
            }

        }


        public static void ExecuteTransaction()
        {
            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                //DO YOUR METHODS here
                //写入方法
                //分布式事务的方法






                transactionScope.Complete();
            }
        }



    }
}
