


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //商家和用户关联表

    public class MerchantVsUserModel
    {

        public MerchantVsUserModel()
        {

        }




        /// <summary>
        /// MerchantId
        /// </summary>		
        public decimal MerchantId
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
        /// 用户在本店内的权限, 100表示总负责人,在本店享有最高权限
        /// </summary>		
        public int Power
        {
            get;
            set;
        }
        /// <summary>
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }

    }
}


