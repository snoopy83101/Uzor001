


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //UserVsMer

    public class UserVsMerModel
    {

        public UserVsMerModel()
        {

        }




        /// <summary>
        /// AutoId
        /// </summary>		
        public decimal AutoId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>		
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 商家ID
        /// </summary>		
        public decimal MerchantId
        {
            get;
            set;
        }
        /// <summary>
        /// 职位
        /// </summary>		
        public decimal PostId
        {
            get;
            set;
        }

    }
}


