


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //SubjectCash

    public class SubjectCashModel
    {

        public SubjectCashModel()
        {

        }




        /// <summary>
        /// SubjectCashId
        /// </summary>		
        public decimal SubjectCashId
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
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// Amount
        /// </summary>		
        public decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 10为新发布, 20为已通过, -10为 未通过
        /// </summary>		
        public int SubjectCashStatusId
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
        /// DoneTime
        /// </summary>		
        public DateTime DoneTime
        {
            get;
            set;
        }
        /// <summary>
        /// MemberBankCardId
        /// </summary>		
        public decimal MemberBankCardId
        {
            get;
            set;
        }

    }
}


