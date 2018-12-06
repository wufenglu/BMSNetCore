using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace YK.Platform.Core
{
    /// <summary>
    /// 分布式事物
    /// </summary>
    public class TransactionScope
    {
        /// <summary>
        /// 获取事物
        /// </summary>
        /// <returns></returns>
        public static System.Transactions.TransactionScope GetTransactionScope()
        {
            TransactionOptions opts = new TransactionOptions();
            opts.IsolationLevel = IsolationLevel.ReadCommitted;
            opts.Timeout = new TimeSpan(0, 2, 0);
            return new System.Transactions.TransactionScope(TransactionScopeOption.Required);
        }
    }
}
