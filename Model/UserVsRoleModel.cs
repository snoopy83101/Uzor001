


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //UserVsRole

    public class UserVsRoleModel
    {

        public UserVsRoleModel()
        {

        }




        /// <summary>
        /// 用户角色名称
        /// </summary>		
        public string RoleName
        {
            get;
            set;
        }
        /// <summary>
        /// 对应用户ID
        /// </summary>		
        public string RoleUserId
        {
            get;
            set;
        }
        /// <summary>
        /// Memo
        /// </summary>		
        public string RoleMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 用户角色级别
        /// </summary>		
        public int RoleLv
        {
            get;
            set;
        }

    }
}


