using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dict
{
    /// <summary>
    /// 用户记录日志的类别
    /// </summary>
    public enum MemberLogType
    {
        用户注册 = 1,
        申请技能认证 = 10,
        通过技能认证 = 15,
        未实名认证 = 18,
        申请实名认证 = 20,
        通过实名认证 = 25,
        接单档期重置 = 30

    };



}
