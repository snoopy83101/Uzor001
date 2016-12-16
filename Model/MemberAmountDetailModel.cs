


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MemberAmountDetail

    public class MemberAmountDetailModel
    {

        public MemberAmountDetailModel()
        {

        }




        /// <summary>
        /// MemberAmountDetailId
        /// </summary>		
        public decimal MemberAmountDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// OldAmount
        /// </summary>		
        public decimal OldAmount
        {
            get;
            set;
        }
        /// <summary>
        /// NewAmount
        /// </summary>		
        public decimal NewAmount
        {
            get;
            set;
        }
        /// <summary>
        /// MemberId
        /// </summary>		
        public decimal MemberId
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
        /// ChangeAmount
        /// </summary>		
        public decimal ChangeAmount
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// ReKey
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// ReKey2
        /// </summary>		
        public string ReKey2
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
        /// <summary>
        /// MemberAmountChangeTypeId
        /// </summary>		
        public int MemberAmountChangeTypeId
        {
            get;
            set;
        }

    }
}


