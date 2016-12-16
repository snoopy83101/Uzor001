/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：IBLL.cs
   // 文件功能描述：需要有事务出现的业务类需要继承的接口
   // 接口IBLL
   //
   // 
   // 创建标识： 李涛 20110108
   // 创建内容： 系统框架和注释和代码
   //
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
    /// 需要有事务出现的业务类需要继承的接口
    /// </summary>
    public interface IBLL
    {
        /// <summary>
        /// 获取或设置事务传递对象
        /// </summary>
        ITransfer Transfer
        { get; set; }
    }
}
