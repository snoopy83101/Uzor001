/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：ISqlScript.cs
   // 文件功能描述：数据库sql语句封装
   // 接口ISqlScript
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

using System.Data.Common;

namespace DBTools
{

    /// <summary>
    /// 数据库sql语句封装
    /// </summary>
    public interface ISqlScript
    {

        /// <summary>
        /// 是否是只有sql语句而没有参数
        /// </summary>
        bool IsSqlStringOnly { get; }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        string SqlString { get; }

        /// <summary>
        /// 获取参数数据
        /// </summary>
        DbParameter[] SqlParams{get;}

    }
}
