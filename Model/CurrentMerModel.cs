using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CurrentMerModel
    {
        
        /// <summary>
        /// 当前登录的用户ID
        /// </summary>
        public string CurrentUserId
        {
            get;
            set;

        }

        /// <summary>
        /// 当前登录角色的商家ID
        /// </summary>
        public string CurrentMerName
        {
            get;
            set;
        }


        /// <summary>
        /// 当前登录角色的商家ID
        /// </summary>
        public decimal CurrentMerId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前登录的角色ID
        /// </summary>
        public decimal CurrentMerRoleId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前登录的角色名称
        /// </summary>
        public string CurrentMerRoleName
        {
            get;
            set;
        }


        /// <summary>
        /// 分部编号
        /// </summary>
        public string CurrentBranchId
        {
            get;
            set;
        }
    }
}
