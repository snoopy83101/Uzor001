


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MerRoleVsUser

    public class MerRoleVsUserModel
    {

        public MerRoleVsUserModel()
        {

        }




        /// <summary>
        /// MerRoleId
        /// </summary>		
        public decimal MerRoleId
        {
            get;
            set;
        }
        /// <summary>
        /// UserId
        /// </summary>		
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// WxOpenId
        /// </summary>		
        public string WxOpenId
        {
            get;
            set;
        }
        /// <summary>
        /// 10在线,20忙碌,30暂离,100离线
        /// </summary>		
        public int WorkStatusId
        {
            get;
            set;
        }
        /// <summary>
        /// 10手机在线,20手机忙碌,30手机暂离,40手机离线
        /// </summary>		
        public int WordStatusPcId
        {
            get;
            set;
        }
        /// <summary>
        /// 分部编号
        /// </summary>		
        public string BranchId
        {
            get;
            set;
        }

    }
}


